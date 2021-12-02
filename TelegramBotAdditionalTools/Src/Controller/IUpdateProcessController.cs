using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBotAdditionalTools.Src.Controller
{
    interface IUpdateProcessController
    {
        public TelegramBotClient bot { get; set; }

        Task ProcessUpdateAsync();
    }
}
