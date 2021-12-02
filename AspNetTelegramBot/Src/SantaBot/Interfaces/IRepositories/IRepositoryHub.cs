namespace SantaBot.Interfaces.IRepositories
{
    public interface IRepositoryHub
    {
        IUserInfoRepository UserInfo { get; }
        IUserChoiceRepository UserChoice { get; }
        IShowHistoryRepository ShowHistory { get; }
        ISearchRepository Search { get; }
    }
}