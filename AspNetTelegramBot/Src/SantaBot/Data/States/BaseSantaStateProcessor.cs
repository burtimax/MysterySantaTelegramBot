using AspNetTelegramBot.Src.Bot.Abstract;
using AspNetTelegramBot.Src.Bot.Code;
using SantaBot.DbModel.Context;

namespace MarathonBot.SantaBot.Data.States
{
    public class BaseSantaStateProcessor : BotUpdateProcessor
    {
        protected SantaContext _dbSanta;
        
        public BaseSantaStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            this._dbSanta = new SantaContext();
        }
    }
}