using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace AspNetTelegramBot.Src.Bot.Code
{
    public class Hop
    {
        public HopInfo hopInfo { get; private set; }
        public bool BlockSendAnswer { get; set; }
        public string PriorityNextStateName { get; set; }
        public string PriorityIntroduction{ get; set; }
        public HopType PriorityHopType{ get; set; }
        public IReplyMarkup PriorityKeyboard { get; set; }

        private string _priorityData { get; set; } = null;
        public string PriorityData { 
            get
            {
                return _priorityData;
            }
            set
            {
                //Can't set null to data? only empty ""
                if (value == null)
                {
                    _priorityData = "";
                }
                else
                {
                    _priorityData = value;
                }
            }
        }

        public Hop(HopInfo hopInfo, BotState fromState, BotState toState, string data = "")
        {
            this.BlockSendAnswer = false;
            this.PriorityHopType = hopInfo.Type;
            this.PriorityKeyboard = hopInfo.DynamicKeyboard ?? toState?.GetKeyboard();
            this.PriorityIntroduction = !string.IsNullOrWhiteSpace(hopInfo.Answer) ? hopInfo.Answer : toState?.GetIntroductionString();

            if (!string.IsNullOrWhiteSpace(data))
            {
                this.PriorityData = data;
            }
            else
            {
                if (string.IsNullOrEmpty(hopInfo.Data) == false)
                {
                    this.PriorityData = hopInfo.Data;
                }
            }

            if (string.IsNullOrWhiteSpace(hopInfo.NextStateName))
            {
                this.PriorityNextStateName = fromState.Name;
            }
            else
            {
                this.PriorityNextStateName = hopInfo.NextStateName;
            }
            
        }

        public Hop BlockAnswer()
        {
            this.BlockSendAnswer = true;
            return this;
        }


    }
}
