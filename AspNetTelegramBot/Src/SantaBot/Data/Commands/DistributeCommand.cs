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

namespace MarathonBot.SantaBot.Data.Commands
{ 
    public class BotDistributeCommand : BotCommand
    {
        public BotDistributeCommand(string str) : base(str)
        {
        
        }

        public override async Task<Hop> ExecCommand(UpdateBotModel data)
        {
            if (data.user.Id.ToString() != AppConstants.SupportUserId) return null;

            using (SantaContext _db = new SantaContext())
            {
                var r = DateTime.Now.Millisecond * 10;
                List<UserInfo> users = await _db.UsersInfo.Where(ui=>ui.UserId != AppConstants.SupportUserIdLong).OrderBy(u=>r-u.RandomNumber).ToListAsync();

                if (users == null || users.Count == 0)
                {
                    await data.bot.SendTextMessageAsync(data.chat.Id, "Нет участников для распределения");
                    return null;
                }

                UserChoice checkExisted = await _db.UserChoices.FirstOrDefaultAsync(uc => uc.UserId == users[0].UserId);
                if (checkExisted != null)
                {
                    await data.bot.SendTextMessageAsync(data.chat.Id, "Участники уже распределены, нельзя повторно распределять!");
                    return null;
                }

                UserInfo lastAdded = new UserInfo();
                List<UserInfo> orderList = new List<UserInfo>();
                orderList.Add(users[0]);
                lastAdded = users[0];
                users.RemoveAt(0);

                while (users.Count > 0)
                {
                    int nextAdd = FindNextIndex(users, !lastAdded.IsMale);
                    orderList.Add(users[nextAdd]);
                    lastAdded = users[nextAdd];
                    users.RemoveAt(nextAdd);
                }

                for (int i = 0; i < orderList.Count; i++)
                {
                    int next = (i + 1) % orderList.Count;
                    UserChoice userChoice = new UserChoice(orderList[i].UserId, orderList[next].UserId);
                    await _db.UserChoices.AddAsync(userChoice);
                }

                await _db.SaveChangesAsync();
            }

            await data.bot.SendTextMessageAsync(data.chat.Id, "Участники распределены!");
            return null;
        }


        private int FindNextIndex(List<UserInfo> usersList, bool findMale)
        {
            for (int i = 0; i < usersList.Count; i++)
            {
                if (usersList[i].IsMale == findMale)
                {
                    return i;
                }
            }

            return 0;
        }
        
        
        
    }
}