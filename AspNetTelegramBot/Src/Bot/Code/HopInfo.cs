using System;
using System.Collections.Generic;
using System.Text;
using AspNetTelegramBot.Src.Bot.Code;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace AspNetTelegramBot.Src.Bot.Code
{
    public class HopInfo
    {
        public readonly string NextStateName;

        /// <summary>
        /// Type of Hop (Root, CurrentLevel, NextLevel, BackToPreviosHopLevel)
        /// </summary>
        public readonly HopType Type = HopType.RootLevelHop;

        /// <summary>
        /// Introduction string for next CurrentState
        /// </summary>
        public readonly string Answer;

        /// <summary>
        /// Data - для передачи данных при переходе в другое состояние.
        /// </summary>
        public readonly string Data;

        public readonly IReplyMarkup DynamicKeyboard;

        public HopInfo( string nextStateName, 
                        string answer = "", 
                        HopType type = HopType.RootLevelHop, 
                        String data = "",
                        IReplyMarkup dynamicKeyboard = null)
        {
            this.NextStateName = nextStateName;
            this.Answer = answer;
            this.Type = type;
            this.Data = data;
            this.DynamicKeyboard = dynamicKeyboard;
        }

        //public Hop GetHopFromInfo(BotState fromState)
        //{
        //    Hop hop = new Hop(fromState)
        //    {
        //        NextStateName = this.NextStateName,
        //        IntroductionString = this.Answer,
        //        Type = this.Type,
        //    };
        //    return hop;
        //}
    }

    /// <summary>
    /// Тип перехода на новое состояние
    /// </summary>
    public enum HopType
    {
        /// <summary>
        /// Установить новое состояние в корень строки состояний
        /// </summary>
        RootLevelHop,

        /// <summary>
        /// Создать следующий уровень состояния
        /// </summary>
        NextLevelHop,

        /// <summary>
        /// Изменить текущий уровень состояния
        /// </summary>
        CurrentLevelHop,

        /// <summary>
        /// Установить предыдущий уровень состояния
        /// </summary>
        BackToPreviosLevelHop
    }
}
