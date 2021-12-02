using System.Threading.Tasks;
using SantaBot.DbModel.Entities;

namespace SantaBot.Interfaces.IRepositories
{
    public interface ISearchRepository
    {
        Task<UserInfo> GetProfile(UserInfo paramsSearch, bool shown);
        
    }

    
}