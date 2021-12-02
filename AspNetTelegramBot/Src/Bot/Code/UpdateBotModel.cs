using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.DbModel.DbBot;
using MarathonBot.Src.Bot.Code;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = AspNetTelegramBot.Src.DbModel.DbBot.Chat;
using User = AspNetTelegramBot.Src.DbModel.DbBot.User;


namespace AspNetTelegramBot.Src.Bot.Code
{
    public class UpdateBotModel
    {
        public User user { get; set; }
        public Chat chat { get; set; }
        public BotState state { get; set; }
        public BotContext dbBot { get; set; }
        public TelegramBotClient bot { get; set; }
        public Update update { get; set; }
        public BotStateStorage stateStorage { get; set; }
        public BotCommandStorage commandStorage { get; set; }
    }
}
