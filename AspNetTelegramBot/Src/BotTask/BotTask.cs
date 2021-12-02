using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MarathonBot.Src.BotTask
{
    public class BotTaskEventArgs : EventArgs
    {
        public AspNetTelegramBot.Src.Bot.Code.Bot Bot;
    }

    public enum TaskStatus
    {
        Waiting,
        Working,
    }

    public enum TaskImportance
    {
        Low,
        Middle,
        High,
    }

    public abstract class BotTask
    {
        public delegate void StartTaskDelegate(BotTask sender, BotTaskEventArgs args);
        public event StartTaskDelegate OnStart;

        public delegate void FinishTaskDelegate(BotTask sender, BotTaskEventArgs args);
        public event FinishTaskDelegate OnFinish;

        public string Name;
        public TimeSpan Interval;
        public DateTime LastWorkTime;
        public TaskStatus Status;
        public TaskImportance Importance;

        public BotTask(String name, DateTime startTime, TimeSpan interval)
        {
            this.Name = name;
            this.Status = TaskStatus.Waiting;
            this.Importance = TaskImportance.Middle;
            this.LastWorkTime = startTime - interval;
            this.Interval = interval;
        }

        public void Start(AspNetTelegramBot.Src.Bot.Code.Bot bot)
        {
            BotTaskEventArgs args = new BotTaskEventArgs()
            {
                Bot = bot,
            };

            this.Status = TaskStatus.Working;
            OnStart?.Invoke(this, args);
            Do(bot);
            OnFinish?.Invoke(this, args);
            this.LastWorkTime = DateTime.Now;
            this.Status = TaskStatus.Waiting;
        }

        protected abstract void Do(AspNetTelegramBot.Src.Bot.Code.Bot bot);

    }
}
