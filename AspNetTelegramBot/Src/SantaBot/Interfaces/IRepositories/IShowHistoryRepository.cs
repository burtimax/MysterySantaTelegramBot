using System.Threading.Tasks;
using AspNetTelegramBot.Src.DbModel.DbBot;
using SantaBot.DbModel.Entities;

namespace SantaBot.Interfaces.IRepositories
{
    public interface IShowHistoryRepository : ISantaBaseRepository<ShowHistory>
    {
        Task<ShowHistory> GetProfileHistory(long searcherUserId, long shownUserId);
    }
}