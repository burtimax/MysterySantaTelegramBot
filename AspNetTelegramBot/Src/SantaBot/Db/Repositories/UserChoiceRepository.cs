using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SantaBot.DbModel.Entities;
using SantaBot.DbModel.Context;
using SantaBot.Interfaces.IRepositories;

namespace SantaBot.Db.Repositories
{
    public class UserChoiceRepository : SantaBaseRepository<UserChoice>, IUserChoiceRepository
    {
        public UserChoiceRepository(SantaContext db) : base(db)
        {
        }

        public async Task<int> GetChosenCount(long userId)
        {
            return await _db.UserChoices.CountAsync(c => c.UserId == userId);
        }

        public async Task<UserChoice> GetUserChoice(long userId, long chosenUserId)
        {
            return await _db.UserChoices.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChosenUserId == chosenUserId);
        }

        public async Task<List<UserInfo>> GetFavouritesProfiles(long userId)
        {
            List<long> chosenIdList = (await _db.UserChoices.Where(c => c.UserId == userId).ToListAsync()).Select(c=>c.ChosenUserId).ToList();

            if (chosenIdList?.Count == 0)
            {
                return null;
            }

            return await _db.UsersInfo.Where(ui => chosenIdList.Contains(ui.UserId)).ToListAsync();
        }
    }
}