using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data.States;
using MarathonBot.SantaBot.Service;
using SantaBot.Data.States._TEMPLATE_;
using SantaBot.Data.States.Search;
using SantaBot.DbModel.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SantaBot.Data.States.SetDescription
{
    public class SetDescriptionBotStateProcessor : BaseSantaStateProcessor
    {
        public SetDescriptionBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Text;
        }
        

        protected override async Task<Hop> TextMessageProcess(Message mes)
        {
            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);
            
            if (mes.Text == SetDescriptionVars.BtnSetCurrent)
            {
                await SendProfileForConfirmation(ui);
                Hop hopNext = new Hop(new HopInfo("Main", MainVars.RulesIntroduction, HopType.CurrentLevelHop), 
                    CurrentState, 
                    StateStorage.Get("Main"));
                return hopNext;
            }
            
            ui.Description = mes.Text;
            await _dbSanta.Repos.UserInfo.UpdateAsync(ui);

            await SendProfileForConfirmation(ui);
            var hop = new Hop(new HopInfo("Main", MainVars.RulesIntroduction, HopType.CurrentLevelHop), 
                CurrentState, 
                StateStorage.Get("Main"));
            return hop;
        }


        private async Task SendProfileForConfirmation(UserInfo myProfile)
        {
            ProfileService profileService = new ProfileService();
            await profileService.SendProfile(Bot, Chat.Id, myProfile);
        }
    }
}