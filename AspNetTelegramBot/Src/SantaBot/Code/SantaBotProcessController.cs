using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Abstract;
using AspNetTelegramBot.Src.Bot.Code;
using Telegram.Bot.Types;

namespace MarathonBot.SantaBot.Code
{
    public class SantaBotProcessController : BotProcessController
    {
        public SantaBotProcessController(Bot bot) : base(bot)
        {
        }

        public SantaBotProcessController(Bot bot, BotControllerAdditionalMethods additionalMethods) : base(bot, additionalMethods)
        {
        }

        public override Task ProcessUpdate(object sender, Update update)
        {
            //My Work
            return base.ProcessUpdate(sender, update);
        }
    }
}