using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data.States;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SantaBot.Data.States.SetSearchAge
{
    public class SetSearchAgeBotStateProcessor : BaseSantaStateProcessor
    {
        private Regex numbersRegex = new Regex(@"(\s*(?<min>\d+)((\s*[-\s]+\s*)|(\D+))(?<max>\d+)\s*)|(\s*(?<single>\d+)\s*)", RegexOptions.Multiline); 
        
        public SetSearchAgeBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Text;
        }

        protected override async Task<Hop> TextMessageProcess(Message mes)
        {
            //Проверить если нажали кнопку Любой возраст
            if (mes.Text == SetSearchAgeVars.BtnAnyAge)
            {
                await SaveAges(0, 100);
                return successHop;
            }
            
            //Проверить и получить числа
            if (TryGetMinMax(mes.Text, out var min, out var max) == false)
            {
                return defaultHop;
            }

            //Сохранить числа и перейти в следующее состояние...
            await SaveAges(min, max);
            
            return successHop;
        }

        private async Task SaveAges(int min, int max)
        {
            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);
            ui.SearchMinAge = min;
            ui.SearchMaxAge = max;
            await _dbSanta.Repos.UserInfo.UpdateAsync(ui);
        }

        private bool TryGetMinMax(string str, out int min, out int max)
        {
            min = 0;
            max = 100;

            var res = numbersRegex.Match(str);
            if (res.Success == false)
            {
                return false;
            }

            var n1Str = res.Groups["min"].Value;
            var n2Str = res.Groups["max"].Value;

            if (string.IsNullOrEmpty(n2Str))
            {
                n2Str = n1Str;
            }

            var single = res.Groups["single"].Value;
            if (string.IsNullOrEmpty(single) == false)
            {
                n1Str = single;
                n2Str = single;
            }
            
            if (int.TryParse(n1Str, out var n1) == false |
                int.TryParse(n2Str, out var n2) == false)
            {
                return false;
            }

            min = Math.Max(Math.Min(n1, n2), 0);
            max = Math.Min(Math.Max(n1, n2), 100);
            return true;
        }
    }
}