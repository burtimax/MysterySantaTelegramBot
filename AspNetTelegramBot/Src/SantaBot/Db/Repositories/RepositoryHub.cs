using SantaBot.DbModel.Context;
using SantaBot.Interfaces.IRepositories;

namespace SantaBot.Db.Repositories
{
    public class RepositoryHub : IRepositoryHub
    {
        private SantaContext _db;
        
        public RepositoryHub(SantaContext db)
        {
            _db = db;
        }

        //----------------------------------------------
        private IUserInfoRepository _userInfoRepository;

        public IUserInfoRepository UserInfo
        {
            get
            {
                if (_userInfoRepository == null)
                {
                    _userInfoRepository = new UserInfoRepository(_db);
                }

                return _userInfoRepository;
            }
        }
        
        //----------------------------------------------
        private IShowHistoryRepository _showHistoryRepository;

        public IShowHistoryRepository ShowHistory
        {
            get
            {
                if (_showHistoryRepository == null)
                {
                    _showHistoryRepository = new ShowHistoryRepository(_db);
                }

                return _showHistoryRepository;
            }
        }
        
        //----------------------------------------------
        private IUserChoiceRepository _userChoiceRepository;

        public IUserChoiceRepository UserChoice
        {
            get
            {
                if (_userChoiceRepository == null)
                {
                    _userChoiceRepository = new UserChoiceRepository(_db);
                }

                return _userChoiceRepository;
            }
        }
        
        //----------------------------------------------
        private ISearchRepository _searchRepository;

        public ISearchRepository Search
        {
            get
            {
                if (_searchRepository == null)
                {
                    _searchRepository = new SearchRepository(_db);
                }

                return _searchRepository;
            }
        }
        
    }
}