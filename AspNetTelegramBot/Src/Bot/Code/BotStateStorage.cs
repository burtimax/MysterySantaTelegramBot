using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.DbModel.DbBot;

namespace AspNetTelegramBot.Src.Bot.Code
{
    public abstract class BotStateStorage
    {
        public Dictionary<string, BotState> States = null;

        protected BotStateStorage()
        {
            InitStates();
        }

        protected abstract void InitStates();

        /// <summary>
        /// Get BotState object By State Name.
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public BotState GetStateByChatData(Chat chat)
        {
            if (States == null || States.Count == 0) return null;

            var tmpChatName = chat.GetCurrentStateName();
            if (States.ContainsKey(tmpChatName))
            {
                return States[tmpChatName];
            }

            return null;
        }

        /// <summary>
        /// Get BotState object By State Name and put Data
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public BotState Get(string stateName)
        {
            if (States == null || States.Count == 0) return null;

            if (States.ContainsKey(stateName))
            {
                return States[stateName];
            }

            return null;
        }
    }
}
