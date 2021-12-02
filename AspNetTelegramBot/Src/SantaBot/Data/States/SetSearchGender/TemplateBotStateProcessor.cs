using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data.States;
using SantaBot.Data.States._TEMPLATE_;
using SantaBot.Data.States.SetSearchAge;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SantaBot.Data.States.SetSearchGender
{
    public class SetSearchGenderBotStateProcessor : BaseSantaStateProcessor
    {
        public SetSearchGenderBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Text;
        }

        protected override async Task<Hop> TextMessageProcess(Message mes)
        {
            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);

            bool isBtn = false;

            if (mes.Text == SetSearchGenderVars.BtnMale)
            {
                ui.SearchMale = true;
                isBtn = true;
            }
            
            if (mes.Text == SetSearchGenderVars.BtnFemale)
            {
                ui.SearchMale = false;
                isBtn = true;
            }
            
            if (mes.Text == SetSearchGenderVars.BtnBoth)
            {
                ui.SearchMale = null;
                isBtn = true;
            }

            if (isBtn)
            {
                await _dbSanta.Repos.UserInfo.UpdateAsync(ui);
                
                var hop = new Hop(new HopInfo("SetSearchAge", SetSearchAgeVars.Introduction, HopType.CurrentLevelHop), 
                    CurrentState, 
                    StateStorage.Get("SetSearchAge"));
                
                //Сделаем клавиатуру с выбором прошлых значений для быстроты настроек
                if (ui.SearchMinAge != 0 || (ui.SearchMaxAge != 100 && ui.SearchMaxAge !=0))
                {
                    hop.PriorityKeyboard =
                        SetSearchAgeVars.GetDefaultValueKeyboard(ui.SearchMinAge, ui.SearchMaxAge).Value;
                }

                return hop;
            }
            
            return defaultHop;
        }
    }
}