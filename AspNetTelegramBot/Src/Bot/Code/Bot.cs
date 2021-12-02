using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.DbModel.DbBot;
using MarathonBot.Src.Bot.Code;
using Telegram.Bot;

namespace AspNetTelegramBot.Src.Bot.Code
{
    public abstract class Bot
    {
        public Bot(string botName, TelegramBotClient client, BotStateStorage states, BotCommandStorage commands)
        {
            this.client = client;
            this.StateStorage = states;
            this.CommandStorage = commands;
            this.Name = botName;
        }

        /// <summary>
        /// Name should be contains in states namespace
        /// </summary>
        public string Name { get; set; }
        public TelegramBotClient client { get; set; }
        public BotStateStorage StateStorage { get; set; }
        public BotCommandStorage CommandStorage { get; set; }
        
        public BotContext BotDbContext { get; set; }

        public BotContext GetNewBotContext()
        {
            return BotDbContext;
        }


    }
}
