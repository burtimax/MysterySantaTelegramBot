using System.Collections.Generic;
using System.Threading.Tasks;
using SantaBot.DbModel.Entities;

namespace SantaBot.Interfaces.IRepositories
{
    public interface IUserChoiceRepository : ISantaBaseRepository<UserChoice>
    {
        Task<int> GetChosenCount(long userId);

        Task<UserChoice> GetUserChoice(long userId, long chosenUserId);

        Task<List<UserInfo>> GetFavouritesProfiles(long userId);

        Task<bool> IsProfileInMyChoice(long meUserId, long chosenUserId);
    }
}