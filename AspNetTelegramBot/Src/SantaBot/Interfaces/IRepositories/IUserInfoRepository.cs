using System.Threading.Tasks;
using SantaBot.DbModel.Entities;

namespace SantaBot.Interfaces.IRepositories
{
    public interface IUserInfoRepository : ISantaBaseRepository<UserInfo>
    {
        Task<UserInfo> GetByUserId(long userId);
    }
}