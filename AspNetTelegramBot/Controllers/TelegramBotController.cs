using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Abstract;
using AspNetTelegramBot.Src.Bot.Code;
using AspNetTelegramBot.Src.Bot.ExtensionsMethods;
using AspNetTelegramBot.Src.DbModel.DbBot;
using MarathonBot;
using MarathonBot.Helpers;
using MarathonBot.SantaBot.Code;
using MarathonBot.SantaBot.Mock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SantaBot.DbModel.Context;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetTelegramBot.Controllers
{
    [ApiController]
    public class TelegramBotController : ControllerBase
    {
        private static BotProcessController _controller;

        private static BotProcessController Controller
        {
            get
            {
                if (_controller == null)
                {
                    _controller = new SantaBotProcessController(BotSingleton.GetInstanceAsync().Result);
                }

                return _controller;
            }
        }


        private ILogger<TelegramBotController> _logger;
        public TelegramBotController(ILogger<TelegramBotController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("bot")]
        public async Task<IActionResult> GetBot()
        {
            _logger.LogInformation($"GET /bot - {DateTime.Now.ToString("G")}");
            //Get Bot client
            return await Task.FromResult<IActionResult>(Ok("BOT INDEX PAGE"));
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            _logger.LogInformation($"GET /test - {DateTime.Now.ToString("G")}");
            return await Task.FromResult<IActionResult>(Ok("TEST PAGE"));
        }

        [HttpGet("dbinit")]
        public async Task<ActionResult> DbTest()
        {
            _logger.LogInformation($"GET /db - {DateTime.Now.ToString("G")}");
            try
            {
                //CreateDatabase and Provide Migrations
                BotContext botContext = new BotContext();
                botContext.Database.EnsureCreated();
                await botContext.DisposeAsync();
            
                SantaContext santaContext = new SantaContext();
                await santaContext.Database.MigrateAsync();
                await santaContext.DisposeAsync();

                Bootstrap bootstrap = new Bootstrap();
                bootstrap.InitChosenByOtherCountForUsers().Wait();

                GC.Collect();
                return Ok("DB INIT");
            }
            catch
            {
                return Ok("DB ERROR");
            }
        }

        
        [HttpGet("mock")]
        public async Task<ActionResult> BootstrapMockDataToDatabase()
        {
            await MockBootstrap.BootstrapDatabase();
            return Ok("Database updated");
        }
        
        [HttpPost("/")]
        public async Task<IActionResult> Post(Telegram.Bot.Types.Update update)
        {
            if (update == null) return Ok();

            SantaBot.Code.SantaBot bot = await BotSingleton.GetInstanceAsync();

            if (update.IsAllowableUpdate())
            {
                //На каждый запрос создаем отдельный контекст
                using (var botContext = new BotContext())
                {
                    await Controller.ProcessUpdate(botContext, bot.client, update);
                }
                
            }

            //Собираем мусор
            GC.Collect();
            return Ok();
        }
    }
}
