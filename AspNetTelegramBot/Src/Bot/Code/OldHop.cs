//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Telegram.Bot.Types.ReplyMarkups;
//using TelegramBotAdditionalTools.Src.Tools;

//namespace AspNetTelegramBot.Src.Bot.Code
//{
//    public class Hop
//    {
//        public string NextStateName { get; set; }

//        private BotState FromState { get; set; }
//        private BotState ToState { get; set; }

//        /// <summary>
//        /// Type of Hop (Root, CurrentLevel, NextLevel, BackToPreviosHopLevel)
//        /// </summary>
//        public HopType Type { get; set; } = HopType.RootLevelHop;

//        /// <summary>
//        /// Introduction string for next CurrentState
//        /// </summary>
//        public string IntroductionString { get; set; }

//        public Hop(BotState fromState)
//        {
//            this.FromState = fromState;
//        }

//        /// <summary>
//        /// Data - для передачи данных при переходе в другое состояние.
//        /// </summary>
//        public string Data { get; set; }

//        public MarkupWrapper<ReplyKeyboardMarkup> DynamicKeyboard { get; set; }

//        public Hop GetCopy()
//        {
//            Hop hop = new Hop(this.FromState)
//            {
//                NextStateName = this.NextStateName,
//                IntroductionString = this.IntroductionString,
//                Type = this.Type,
//            };
//            return hop;
//        }
//    }
//}