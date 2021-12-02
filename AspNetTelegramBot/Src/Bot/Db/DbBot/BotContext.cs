using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.DbModel.DbMethods;
using MarathonBot;
using Microsoft.EntityFrameworkCore;

namespace AspNetTelegramBot.Src.DbModel.DbBot
{
    public class BotContext : DbContext
    {
        // "Server=(localdb)\\mssqllocaldb;Database=MarathonTelegramBot;Trusted_Connection=True;"

        private string schema = "bot";
        
        //Lazy load Methods object
        public IBotContextDbMethods _methods;
        public IBotContextDbMethods Methods
        {
            get
            {
                if (this._methods == null)
                {
                    this._methods = new BotContextDbMethods(this);
                }

                return this._methods;
            }
            private set { this._methods = value; }
        }


        public DbSet<User> User { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Message { get; set; }

        private string _connection;

        // public BotContext(string connection)
        // {
        //     _connection = connection;
        //     //Database.EnsureCreated();
        // }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>().ToTable("Chats", schema);
            modelBuilder.Entity<Chat>().Property(e => e.CreateTime).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Message>().ToTable("Messages", schema);
            modelBuilder.Entity<Message>().Property(e => e.CreateTime).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            
            modelBuilder.Entity<User>().ToTable("Users", schema);
            modelBuilder.Entity<User>().Property(e => e.CreateTime).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connection
            optionsBuilder.UseNpgsql(AppConstants.DbConnection);
        }
    }
}
