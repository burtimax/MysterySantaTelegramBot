using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data.States;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SantaBot.Data.States._TEMPLATE_
{
    public class _TEMPLATE_BotStateProcessor : BaseSantaStateProcessor
    {
        public _TEMPLATE_BotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Text;
        }

        protected override Task<Hop> ProcessMessage(Message mes)
        {
            return Task.FromResult(successHop);
        }

        protected override Task<Hop> TextMessageProcess(Message mes)
        {
            return base.TextMessageProcess(mes);
        }
    }
}