using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SantaBot.DbModel.Context;
using SantaBot.DbModel.Entities;
using SantaBot.Interfaces.IRepositories;

namespace SantaBot.Db.Repositories
{
    public class UserInfoRepository : SantaBaseRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(SantaContext db) : base(db)
        {
        }

        public async Task<UserInfo> GetByUserId(long userId)
        {
            return await _db.UsersInfo.FirstOrDefaultAsync(ui => ui.UserId == userId);
        }
    }
}