using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SantaBot.DbModel.Context;
using SantaBot.DbModel.Entities;
using SantaBot.Interfaces.IRepositories;
using SantaBot.Interfaces.IServices;

namespace SantaBot.Db.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private SantaContext _db;
        
        public SearchRepository(SantaContext db)
        {
            _db = db;
        }

        public async Task<UserInfo> GetProfile(UserInfo paramsSearch, bool shown)
        {
            int medianeAgeX10 = GetAgeMedianeX10(paramsSearch.SearchMinAge, paramsSearch.SearchMaxAge);
            int minAge = paramsSearch.SearchMinAge;
            int maxAge = paramsSearch.SearchMaxAge;
            bool? isMale = paramsSearch.SearchMale;
            int outBoundAgeMax = 0;
            int num = DateTime.Now.Millisecond * 10;
            
                
            var profiles = await (from ui in _db.UsersInfo
                    select new 
                    {
                        UserInfo = ui,
                        ShowCount = _db.ShowHistory.Where(s=>s.ShownUserId == ui.UserId && s.UserId == paramsSearch.UserId).Select(s=>s.ShowCount).FirstOrDefault(),
                        Rand = ui.RandomNumber,
                    }).Where(item =>
                    (item.UserInfo.Photo != null) &&
                    (item.UserInfo.UserId != paramsSearch.UserId) &&
                    ((item.ShowCount > 0 && shown) || ((item.ShowCount==null || item.ShowCount == 0) && !shown)) &&
                    item.UserInfo.Age >= minAge - outBoundAgeMax &&
                    item.UserInfo.Age <= maxAge + outBoundAgeMax &&
                    (item.UserInfo.IsMale == isMale || isMale == null))
                .OrderBy(item=>item.ShowCount)
                .ThenBy(item=>Math.Abs(item.UserInfo.Age*10-medianeAgeX10))
                .ThenBy(item => item.Rand - num).ToListAsync();

         

            foreach (var profile1 in profiles)
            {
                if (profile1.UserInfo.Contact != null)
                {
                    int t = 1;
                }
            }
            
            var profile = profiles.FirstOrDefault();
            if (profile == null && shown == true)
            {
                var another = await _db.UsersInfo.Where(ui => ui.IsMale == isMale)
                    .OrderBy(ui => ui.RandomNumber - num).FirstOrDefaultAsync();
                if (another == null)
                {
                    return await _db.UsersInfo.FirstOrDefaultAsync();
                }
            }
            
            return profile?.UserInfo ?? null;
        }
        
        
        
        private int GetAgeMedianeX10(int minAge, int maxAge)
        {
            int myMedianeX10 = Convert.ToInt32(Math.Round((double)(minAge+maxAge)/0.2));

            //Пусть медианой будет молодежь
            if (myMedianeX10/10.0f - 20 > 5)
            {
                myMedianeX10 = 20 * 10;
            }

            //Малыши тоже здесь не нужны.
            if (myMedianeX10/10.0f < 12)
            {
                myMedianeX10 = 12 * 10;
            }

            return myMedianeX10;
        }
    }
}