using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using AspNetTelegramBot.Src.DbModel.DbBot;
using Microsoft.EntityFrameworkCore;
using SantaBot.DbModel.Context;
using SantaBot.DbModel.Entities;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using BotCommand = AspNetTelegramBot.Src.Bot.Code.BotCommand;

namespace MarathonBot.SantaBot.Data.Commands
{
    public class SendToOneUserCommand : BotCommand
    {
        public SendToOneUserCommand(string str) : base(str)
        {
            
        }

        public override async Task<Hop> ExecCommand(UpdateBotModel data)
        {
            if (data.user.Id.ToString() != AppConstants.SupportUserId) return null;

           

            string text = data.update.Message.Text;

            string str = text.Substring(Instruction.Length).Trim(' ');

            int indexOfDelim = str.IndexOf(' ');

            if (indexOfDelim == -1)
            {
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, "Wrong format command(");
                return null;
            }
            
            string userIdStr = str.Substring(0, indexOfDelim).Trim(' ');
            string message = str.Substring(indexOfDelim).Trim(' ');

            if (string.IsNullOrWhiteSpace(userIdStr) || string.IsNullOrWhiteSpace(message))
            {
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, "wrong format cammand");
                return null;
            }
            
            if (string.IsNullOrWhiteSpace(userIdStr) == true)
            {
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, "Укажите идентификатор пользователя");
                return null;
            }

            if (long.TryParse(userIdStr, out var userSendId) == false)
            {
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, "Не могу парсить Id пользователя");
                return null;
            }

            if (string.IsNullOrWhiteSpace(message) == true)
            {
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, "Сообщение пустое");
                return null;
            }

            using (SantaContext _db = new SantaContext())
            {
                UserInfo userToSendInfo = await _db.Repos.UserInfo.GetByUserId(userSendId);
                if (userToSendInfo == null)
                {
                    await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, $"Not Found user in db with Id = {userSendId}");
                    return null;
                }
                
                //Обернем, потому что если пользователь удалил бота, выкинет ошибку
                try
                {
                    await data.bot.SendTextMessageAsync(userSendId, message, ParseMode.Html);
                    await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, $"Отправлено [{userToSendInfo.Name}]:\n{message}");
                
                }
                catch
                {
            
                }
            }
            
           
                
            return null;
        }
    }
}