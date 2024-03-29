using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.DbModel.DbBot;
using MarathonBot;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SantaBot.DbModel.Context;

namespace AspNetTelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();

            using (SantaContext _db = new SantaContext())
            {
                _db.Database.MigrateAsync().Wait();
            }
           
            
            //
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

