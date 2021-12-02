using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotAdditionalTools.Src.Controller
{
    public class UpdateProcessController : IUpdateProcessController
    {
        public TelegramBotClient bot { get; set; }

        public UpdateProcessController(TelegramBotClient _bot)
        {
            if (_bot == null) throw new Exception("Bot parameter can't be NULL!");

            this.bot = _bot;
        }

        

        public async Task ProcessUpdateAsync()
        {
            
        }
    }
}
