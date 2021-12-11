using System;
using System.Text;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using AspNetTelegramBot.Src.DbModel.DbBot;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using BotCommand = AspNetTelegramBot.Src.Bot.Code.BotCommand;

namespace MarathonBot.SantaBot.Data.Commands
{
    public class BotActivityReportCommand : BotCommand
    {
        public BotActivityReportCommand(string str) : base(str)
        {
            
        }

        public override async Task<Hop> ExecCommand(UpdateBotModel data)
        {
            if (data.user.Id.ToString() != AppConstants.SupportUserId) return null;

            using (BotContext _db = new BotContext())
            {
                StringBuilder sb = new StringBuilder();
                var usersCount = await _db.User.CountAsync();
                var activityCount = await _db.Message.CountAsync();
                var todayNewUsers = await _db.User.CountAsync(u => u.CreateTime >= DateTime.Today);
                var todayActivity = await _db.Message.CountAsync(m => m.CreateTime >= DateTime.Today);

                sb.AppendLine($"<b>Пользователи</b> : {usersCount}");
                sb.AppendLine($"<b>Активность</b> : {activityCount}");
                sb.AppendLine($"<b>Новые пользователи</b> : {todayNewUsers}");
                sb.AppendLine($"<b>Активность сегодня</b> : {todayActivity}");
                await data.bot.SendTextMessageAsync(data.chat.Id, sb.ToString(), ParseMode.Html);
                return null;
            }
           
            
        }
    }
}