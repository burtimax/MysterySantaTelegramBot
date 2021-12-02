using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Abstract;
using AspNetTelegramBot.Src.Bot.Code;
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
        public async Task<ActionResult> GetBot()
        {
            _logger.LogInformation($"GET /bot - {DateTime.Now.ToString("G")}");
            //Get Bot client
            return Ok("BOT INDEX PAGE");
        }

        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            _logger.LogInformation($"GET /test - {DateTime.Now.ToString("G")}");
            return Ok("TEST PAGE");
        }

        [HttpGet("db")]
        public async Task<ActionResult> DbTest()
        {
            _logger.LogInformation($"GET /db - {DateTime.Now.ToString("G")}");
            try
            {
                using (var db = new BotContext())
                {
                    if (db.Database.CanConnect())
                    {
                        return Ok("DB TRUE");
                    }
                    else
                    {
                        return Ok("DB FALSE");
                    }
                }
            }
            catch (Exception e)
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
            
            //_logger.LogInformation($"POST (update) - {DateTime.Now.ToString("G")}");
            
            SantaBot.Code.SantaBot bot = await BotSingleton.GetInstanceAsync();
            
            //На каждый запрос создаем отдельный контекст
            using (var botContext = new BotContext())
            {
                bot.BotDbContext = botContext;
                await Controller.ProcessUpdate(bot.client, update);
            }
            
            //Собираем мусор
            GC.Collect();
            return Ok();
        }
    }
}
