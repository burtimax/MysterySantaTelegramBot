using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Abstract;
using AspNetTelegramBot.Src.Bot.DbModel.DbMethods;
using MarathonBot;
using Microsoft.EntityFrameworkCore;

namespace AspNetTelegramBot.Src.DbModel.DbBot
{
    public class BotContext : DbContext
    {
        private string schema = "bot";
        
        

        public BotContext()
        {
            
        }

        public DbSet<User> User { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Message { get; set; }

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
            optionsBuilder.UseNpgsql(AppConstants.DbConnection, options =>
            {
                options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3),null);
                options.CommandTimeout(5);
            }); //For Postgres);
        }
    }
}
