using SantaBot.DbModel.Entities;
using SantaBot.DbModel.Entities;
using SantaBot.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace SantaBot.Interfaces
{
    public interface ISantaContext
    {
        DbSet<UserInfo> UsersInfo { get; set; }
        DbSet<ShowHistory> ShowHistory { get; set; }
        DbSet<UserChoice> UserChoices { get; set; }
        
        IRepositoryHub Repos { get; }
    }
}