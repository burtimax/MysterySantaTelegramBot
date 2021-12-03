using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SantaBot.DbModel.Entities;
using SantaBot.DbModel.Context;
using SantaBot.Interfaces.IRepositories;

namespace SantaBot.Db.Repositories
{
    public class ShowHistoryRepository : SantaBaseRepository<ShowHistory>, IShowHistoryRepository
    {
        public ShowHistoryRepository(SantaContext db) : base(db)
        {
        }

        public async Task<ShowHistory> GetProfileHistory(long searcherUserId, long shownUserId)
        {
            return await _db.ShowHistory.SingleOrDefaultAsync(s =>
                s.UserId == searcherUserId && s.ShownUserId == shownUserId);
        }
    }
}