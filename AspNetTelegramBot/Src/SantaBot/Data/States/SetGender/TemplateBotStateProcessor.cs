using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data.States;
using SantaBot.Data.States.SetContact;
using SantaBot.Data.States.SetPhoto;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SantaBot.Data.States.SetGender
{
    public class SetGenderBotStateProcessor : BaseSantaStateProcessor
    {
        public SetGenderBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Text;
        }

        protected override async Task<Hop> ProcessMessage(Message mes)
        {
            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);

            bool getData = false;
            
            if (mes.Text == SetGenderVars.BtnMale)
            {
                ui.IsMale = true;
                getData = true;
            }

            if (mes.Text == SetGenderVars.BtnFemale)
            {
                ui.IsMale = false;
                getData = true;
            }

            //Если получили пол
            if (getData)
            {
                await _dbSanta.Repos.UserInfo.UpdateAsync(ui);
                
                //Если нет username, то перейдем в состояние получения Телефона
                if (string.IsNullOrEmpty(CurrentUser.Username) == true)
                {
                    var contactHop =  new Hop(new HopInfo("SetContact", SetContactVars.Introduction, HopType.CurrentLevelHop), 
                        CurrentState, 
                        StateStorage.Get("SetContact"));
                    
                    if (ui.Contact != null)
                    {
                        contactHop.PriorityKeyboard = SetContactVars.GetDefaultValueKeyboard().Value;
                    }
                    
                    return contactHop;
                }
                
                //Перейти в состояние вставки фото
                var hop =  new Hop(new HopInfo("SetPhoto", SetPhotoVars.Introduction, HopType.CurrentLevelHop), 
                    CurrentState, 
                    StateStorage.Get("SetPhoto"));
                if (ui.Photo != null)
                {
                    hop.PriorityKeyboard = SetPhotoVars.GetDefaultValueKeyboard().Value;
                }

                return hop;
            }
            
            return defaultHop;
        }
    }
}