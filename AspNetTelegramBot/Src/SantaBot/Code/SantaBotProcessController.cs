using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Abstract;
using AspNetTelegramBot.Src.Bot.Code;
using AspNetTelegramBot.Src.Bot.DbModel.DbMethods;
using AspNetTelegramBot.Src.DbModel.DbBot;
using Telegram.Bot.Types;

namespace MarathonBot.SantaBot.Code
{
    public class SantaBotProcessController : BotProcessController
    {
        public SantaBotProcessController(AspNetTelegramBot.Src.Bot.Code.Bot bot) : base(bot)
        {
        }

        public SantaBotProcessController(AspNetTelegramBot.Src.Bot.Code.Bot bot, BotContextDbMethods additionalMethods) : base(bot, additionalMethods)
        {
        }

        public override Task ProcessUpdate(BotContext botContext, object sender, Update update)
        {
            //My Work
            return base.ProcessUpdate(botContext, sender, update);
        }
    }
}