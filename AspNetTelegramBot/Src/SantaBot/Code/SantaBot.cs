using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data;
using Telegram.Bot;

namespace SantaBot.Code
{
    public class SantaBot : Bot
    {
        //private MarathonBotTaskProcessor TaskProcessor;


        public SantaBot(TelegramBotClient client)
            : base("Santa", client, new SantaBotStateStorage(),
                new SantaBotCommandStorage())
        {
            //TaskProcessor = new MarathonBotTaskProcessor(this, AppConstants.RootDir + "\\log\\task_log.txt");
            //TaskProcessor.Start();
        }

    }
}