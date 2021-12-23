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
    public class UnHideParticipantCommand : BotCommand
    {
        public UnHideParticipantCommand(string str) : base(str)
        {
            
        }

        public override async Task<Hop> ExecCommand(UpdateBotModel data)
        {
            if (data.user.Id.ToString() != AppConstants.SupportUserId) return null;

            string text = data.update.Message.Text;

            string userIdToHide = text.Substring(Instruction.Length).Trim(' ');

            if (string.IsNullOrWhiteSpace(userIdToHide) == true)
            {
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, "Укажите идентификатор пользователя");
                return null;
            }

            if (long.TryParse(userIdToHide, out var userHideId) == false)
            {
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, "Не могу парсить Id пользователя");
                return null;
            }
            
            using (SantaContext _db = new SantaContext())
            {
                UserInfo userToHideInfo =await _db.Repos.UserInfo.GetByUserId(userHideId);

                if (userToHideInfo == null)
                {
                    await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, $"Нет в БД пользователя с Id = {userHideId}");
                    return null;
                }

                userToHideInfo.Invisible = false;
                await _db.Repos.UserInfo.UpdateAsync(userToHideInfo);
                await data.bot.SendTextMessageAsync(AppConstants.SupportUserId, $"Пользователь с Id = {userHideId} восстановлен в показах");
            }
            
            return null;
        }
    }
}