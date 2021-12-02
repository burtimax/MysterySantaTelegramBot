using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MarathonBot.Src.BotTask
{
    public class BotTaskProcessor
    {
        private AspNetTelegramBot.Src.Bot.Code.Bot bot;
        private List<BotTask> Tasks;
        System.Threading.Timer Timer;
        private object logObjLocker;
        private string logFilePath;

        public BotTaskProcessor(AspNetTelegramBot.Src.Bot.Code.Bot bot, string logFilePath)
        {
            this.logObjLocker = new object();
            this.bot = bot;
            this.Tasks = new List<BotTask>();
            if(File.Exists(logFilePath))
            {
                this.logFilePath = logFilePath;
            }
        }

        public void Start()
        {
            Timer = new System.Threading.Timer(TimerTick, null, 0, 5000);
        }

        public void Stop()
        {
            Timer.Dispose();
        }

        public void AddTask(BotTask task)
        {
            this.Tasks.Add(task);
            task.OnStart -= OnStartTask;
            task.OnStart += OnStartTask;
            task.OnFinish -= OnFinishTask;
            task.OnFinish += OnFinishTask;
        }

        public void TimerTick(object data)
        {
            var now = DateTime.Now;

            foreach (var task in Tasks)
            {
                if (task.Status == TaskStatus.Waiting &&
                    (now - task.LastWorkTime) > task.Interval)
                {
                    ThreadPool.QueueUserWorkItem((obj) => { task.Start(this.bot); });
                };
            }
        }

        private void OnStartTask(BotTask task, BotTaskEventArgs args)
        {
            if (task.Importance > TaskImportance.Low)
            {
                string taskLogStr = $"LOG [{DateTime.Now.ToString()}] : [{args.Bot.Name}] START [{task.Name}]\n";
                AppendLog(taskLogStr);
            }
           
        }

        private void OnFinishTask(BotTask task, BotTaskEventArgs args)
        {
            if (task.Importance > TaskImportance.Low)
            {
                string taskLogStr = $"LOG [{DateTime.Now.ToString()}] : [{args.Bot.Name}] FINISH [{task.Name}]\n";
                AppendLog(taskLogStr);
            }
           
        }

        private void AppendLog(string log)
        {
            lock (this.logObjLocker)
            {
                if (string.IsNullOrEmpty(logFilePath) == false)
                {
                    File.AppendAllText(this.logFilePath, log);
                }
            }
        }
    }
}
