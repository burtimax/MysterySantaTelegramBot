using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using BotCommand = AspNetTelegramBot.Src.Bot.Code.BotCommand;

namespace MarathonBot.SantaBot.Data.Commands
{
    public class BotTestSendAllCommand : BotCommand
    {
        public BotTestSendAllCommand(string str) : base(str)
        {
            
        }

        public override async Task<Hop> ExecCommand(UpdateBotModel data)
        {
            if (data.user.Id.ToString() != AppConstants.SupportUserId &&
                data.user.Id.ToString() != AppConstants.ManagerUserId) return null;
            
            string text = data.update.Message.Text;

            string messageToAll = text.Substring(Instruction.Length).Trim(' ');

            if (string.IsNullOrWhiteSpace(messageToAll) == true)
            {
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, "Сообщение пустое");
                return null;
            }
            
            await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, messageToAll, ParseMode.Html);
            
            return null;
        }
    }
}