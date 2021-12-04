using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.DbModel.DbBot;
using Microsoft.EntityFrameworkCore;
using SantaBot.DbModel.Context;
using SantaBot.DbModel.Entities;

namespace MarathonBot
{
    public class Bootstrap
    {

        public Bootstrap()
        {
            
        }

        public async Task InitChosenByOtherCountForUsers()
        {
            using (var db = new SantaContext())
            {
                if (await db.UsersInfo.AnyAsync() == false) return;

                List<UserInfo> users = await db.UsersInfo.ToListAsync();

                foreach (var user in users)
                {
                    int chosenCount = await db.UserChoices.CountAsync(uc => uc.ChosenUserId == user.UserId);
                    if (chosenCount != user.ChosenByOthersCount)
                    {
                        user.ChosenByOthersCount = chosenCount;
                        db.UsersInfo.Update(user);
                    }
                }

                await db.SaveChangesAsync();
            }
        }
    }
}