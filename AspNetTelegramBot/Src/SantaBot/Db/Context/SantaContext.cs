using AspNetTelegramBot.Src.DbModel.DbBot;
using MarathonBot;
using SantaBot.DbModel.Entities;
using SantaBot.Db.Repositories;
using SantaBot.Interfaces;
using SantaBot.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace SantaBot.DbModel.Context
{
    public class SantaContext : DbContext, ISantaContext
    {
        private string schema = "santa";

        // public SantaContext(string connection)
        // {
        //     _connection = connection;
        // }

        public SantaContext()
        {
            
        }
        
        
        public DbSet<UserInfo> UsersInfo { get; set; }
        public DbSet<UserChoice> UserChoices { get; set; }
        public DbSet<ShowHistory> ShowHistory { get; set; }

        private IRepositoryHub _repositoryHub;
        public IRepositoryHub Repos
        {
            get
            {
                if (_repositoryHub == null)
                {
                    _repositoryHub = new RepositoryHub(this);
                }

                return _repositoryHub;
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(AppConstants.DbConnection); //for MS SQL SERVER
            optionsBuilder.UseNpgsql(AppConstants.DbConnection); //For Postgres
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ShowHistory>().ToTable("ShowHistory", schema);
            modelBuilder.Entity<ShowHistory>().Property(e => e.CreateTime).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            modelBuilder.Entity<ShowHistory>().HasQueryFilter(sh => sh.SoftDelete == false);
            
            modelBuilder.Entity<UserInfo>().ToTable("UsersInfo", schema);
            modelBuilder.Entity<UserInfo>().Property(e => e.CreateTime).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            modelBuilder.Entity<UserInfo>().HasQueryFilter(ui => ui.SoftDelete == false);
            
            modelBuilder.Entity<UserChoice>().ToTable("UsersChoice", schema);
            modelBuilder.Entity<UserChoice>().Property(e => e.CreateTime).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            modelBuilder.Entity<UserChoice>().HasQueryFilter(uc => uc.SoftDelete == false);
            base.OnModelCreating(modelBuilder);
        }
    }
}