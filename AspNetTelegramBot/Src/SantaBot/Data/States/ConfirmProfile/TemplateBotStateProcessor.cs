using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot;
using MarathonBot.SantaBot.Data.States;
using MarathonBot.SantaBot.Service;
using SantaBot.Data.States._TEMPLATE_;
using SantaBot.Data.States.Search;
using SantaBot.Data.States.Start;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SantaBot.Data.States.ConfirmProfile
{
    public class ConfirmProfileBotStateProcessor : BaseSantaStateProcessor
    {
        public ConfirmProfileBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Text;
        }
        

        protected override async Task<Hop> TextMessageProcess(Message mes)
        {
            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);
            var ans = mes.Text;

            if (mes.Text == ConfirmProfileVars.BtnNorm)
            {
                var hop = successHop;
                
                //Если пользователь уже проходил регистрацию и указал параметры поиска, то вернуть его в главное меню.
                if (ui.SearchMaxAge != 101)
                {
                    hop =  new Hop(new HopInfo("Main", MainVars.Introduction, HopType.CurrentLevelHop), 
                        CurrentState, 
                        StateStorage.Get("Main"));
                }
                else
                {
                    //Если проходит регистрацию (отправить админу новое письмо)
                    ProfileService profileService = new ProfileService();
                    long supportId = long.Parse(AppConstants.SupportUserId);
                    await profileService.SendProfile(Bot, supportId, ui);
                }
                
                return hop;
            }

            if (mes.Text == ConfirmProfileVars.BtnEdit)
            {
                var hop =  new Hop(new HopInfo("SetName", SetNameVars.Introduction, HopType.CurrentLevelHop), 
                    CurrentState, 
                    StateStorage.Get("SetName"));
                if (ui.Name != default)
                {
                    hop.PriorityKeyboard = SetNameVars.GetDefaultValueKeyboard(ui.Name).Value;
                }

                return hop;
            }

            return defaultHop;
        }
    }
}