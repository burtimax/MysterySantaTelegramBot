using System;
using System.Globalization;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data.States;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SantaBot.Data.States.SetAge
{
    public class SetAgeBotStateProcessor : BaseSantaStateProcessor
    {
        public SetAgeBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Text;
        }

        protected override async Task<Hop> TextMessageProcess(Message mes)
        {
            if (IsAgeValid(mes.Text, out var age) == false)
            {
                return defaultHop;
            }

            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);
            ui.Age = age;
            await _dbSanta.Repos.UserInfo.UpdateAsync(ui);

            return successHop;
        }


        private bool IsAgeValid(string ageStr, out int age)
        {
            age = -1;
            
            if(int.TryParse(ageStr, out var a) && a > 0 && a < 100)
            {
                age = a;
                return true;
            }

            return false;
        }
        
    }
}