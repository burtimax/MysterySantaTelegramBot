using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;

namespace MarathonBot.Src.Bot.Code
{
    /// <summary>
    /// Управляет логикой переходов в новое состояние.
    /// </summary>
    public class HopStateManager
    {
        
        public static void ChangeState(ref string chatState, string nextStateName, HopType hopType)
        {
            switch (hopType)
            {
                case HopType.CurrentLevelHop:
                    ChangeStateByCurrentLevelType(ref chatState, nextStateName);
                    break;

                case HopType.RootLevelHop:
                    ChangeStateByRootLevelType(ref chatState, nextStateName);
                    break;

                case HopType.NextLevelHop:
                    ChangeStateByNextLevelType(ref chatState, nextStateName);
                    break;

                case HopType.BackToPreviosLevelHop:
                    ChangeStateByBackPreviosLevelType(ref chatState, nextStateName);
                    break;
            }
        }

        private static void ChangeStateByRootLevelType(ref string state, string nextStateName)
        {
            state = nextStateName;
        }

        private static void ChangeStateByNextLevelType(ref string state, string nextStateName)
        {
            state = state + "/" + nextStateName;
        }

        private static void ChangeStateByBackPreviosLevelType(ref string state, string nextStateName)
        {
            string t1, t2;
            var indexLastSlash = state.LastIndexOf('/');
            if (indexLastSlash > -1)
            {
                t1 = state.Substring(0, state.LastIndexOf('/'));

                var indexPrevSlash = t1.LastIndexOf('/');

                if(indexPrevSlash > -1)
                {
                    t2 = t1?.Substring(0, indexPrevSlash);

                    state = t2 + '/' + nextStateName;
                    return;
                }
            }

            ChangeStateByRootLevelType(ref state, nextStateName);
        }

        private static void ChangeStateByCurrentLevelType(ref string state, string nextStateName)
        {
            var indexSlash = state.LastIndexOf('/');


            if (indexSlash == -1)
            {
                ChangeStateByRootLevelType(ref state, nextStateName);
                return;
            }
            var t1 = state.Substring(0, indexSlash);
            state = t1 + '/' + nextStateName;
        }

        public static string GetCurrentStateName(string chatState)
        {
            //this.State по типу "FirstState/SecondState/ThirdState"
            //Нам нужно вернуть последнее состояние

            var tmpSlashPos = chatState.LastIndexOf('/');

            //Нет слэша в строке
            if (tmpSlashPos == 0)
            {
                return chatState;
            }

            return chatState.Substring(tmpSlashPos + 1);
        }

        /// <summary>
        /// Get Previous State Name or NULL
        /// </summary>
        /// <returns></returns>
        public static string GetPreviousStateName(string chatState)
        {
            var lastSlash = chatState.LastIndexOf('/');

            if (lastSlash == -1) return null;

            var tempStr = chatState.Substring(0, lastSlash);

            var prevSlash = tempStr.LastIndexOf('/'); ;

            return tempStr.Substring(prevSlash+1);
        }
    }
}
