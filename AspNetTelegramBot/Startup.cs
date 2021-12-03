using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.DbModel.DbBot;
using MarathonBot;
using MarathonBot.Helpers;
using MarathonBot.SantaBot.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SantaBot.DbModel.Context;

namespace AspNetTelegramBot
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            AppConstants.RootDir = env.WebRootPath;
            AppConstants.DbConnection = Configuration["Database:ConnectionString"];
            AppConstants.BotToken = Configuration["Bot:Token"];
            AppConstants.BotWebhook = Configuration["Bot:Webhook"];
            AppConstants.SupportUserId = Configuration["Bot:SupportUserId"];
            AppConstants.ShowDate = DateTime.ParseExact(Configuration["Constants:ShowDate"], "dd.MM.yyyy", null);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("DbConnection - " + AppConstants.DbConnection);
            logger.LogInformation("RootDir - " + AppConstants.RootDir);
            logger.LogInformation("WebHook - " + AppConstants.BotWebhook);
            logger.LogInformation("PhotoPath - " + PhotoHelper.GetPhotoFilePathByUserInfoPhoto("image.jpg"));

            //CreateDatabase and Provide Migrations
            using (BotContext botContext = new BotContext())
            {
                botContext.Database.EnsureCreated();
            }
            
            using (SantaContext santaContext = new SantaContext())
            {
                santaContext.Database.MigrateAsync();
            }
            //
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var bot = BotSingleton.GetInstanceAsync().Result;
            logger.LogInformation("Bot start");
        }
    }
}
