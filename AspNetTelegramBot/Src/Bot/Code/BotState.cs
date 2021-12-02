using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace AspNetTelegramBot.Src.Bot.Code
{
    public class BotState
    {
        private int Id { get; set; }


        /// <summary>
        /// Name of CurrentState
        /// </summary>
        public string Name { get; set; }

        private string _className;
        /// <summary>
        /// Чтобы указать класс, который относится к состоянию.
        /// С помощью этого свойства можно делать разные урови доступа к одному состоянию.
        /// </summary>
        public string ClassName {
            get
            {
                if (_className == null)
                {
                    return this.Name;
                }

                return _className;
            }
            set { _className = value; }
        }

        /// <summary>
        /// Introduction for CurrentUser? when CurrentUser get into CurrentState
        /// </summary>
        public string DefaultIntroductionString { get; set; } = "";
        
        /// <summary>
        /// Если ожидали текст, а пришла фотка.
        /// </summary>
        public string UnexpectedUpdateTypeAnswer { get; set; } = "";

        /// <summary>
        /// State default keyboard (low priority)
        /// </summary>
        public IReplyMarkup DefaultKeyboard { get; set; } = new ReplyKeyboardRemove();

        public HopInfo HopOnSuccess { get; set; } = null;

        private HopInfo _hopCurrentState;
        public HopInfo HopCurrentState
        {
            get
            {
                if(_hopCurrentState == null)
                {
                    _hopCurrentState = new HopInfo(this.Name, this.GetIntroductionString(), HopType.CurrentLevelHop);
                }

                return _hopCurrentState;
            }

            set { _hopCurrentState = value; }
        }
        public HopInfo HopOnFailure { get; set; } = null;



        public virtual IReplyMarkup GetKeyboard()
        {
            return this.DefaultKeyboard;
        }

        public virtual string GetIntroductionString()
        {
            return DefaultIntroductionString;
        }
    }
}
