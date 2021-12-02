using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotAdditionalTools.Src.Tools
{
    public class MarkupWrapper<T>
    {
        private bool ResizeKeyboard = false;
        private IReplyMarkup MarkUp;
        private IReplyMarkup innerMarkUp;
        private IKeyboardButton[][] keyboard;
        private List<List<IKeyboardButton>> Keyboard;

        List<IKeyboardButton> LastRow
        {
            get
            {
                if(this.Keyboard.Count>0) return this.Keyboard.LastOrDefault();
                return null;
            }
        }

        public IReplyMarkup Value
        {
            get
            {
                ConfigureArray();
                return this.MarkUp;
            }
        }

        public MarkupWrapper(bool resizeKeyboard = true)
        {
            this.Keyboard = new List<List<IKeyboardButton>>();
            this.ResizeKeyboard = resizeKeyboard;
        }

        public MarkupWrapper<T> NewRow()
        {
            this.Keyboard.Add(new List<IKeyboardButton>());
            return this;
        }

        /// <summary>
        /// Добавляем кнопку в клавиатуру
        /// </summary>
        /// <param name="text">Имя кнопки</param>
        /// <param name="callBack">Используется только для InlineKeyboardMarkup</param>
        /// <returns></returns>
        public MarkupWrapper<T> Add(string text, string callBack = "default", string url = null)
        {

            if (typeof(T) == typeof(ReplyKeyboardMarkup)){
                KeyboardButton btn = new KeyboardButton(text);
                LastRow.Add(btn);
            }

            if (typeof(T) == typeof(InlineKeyboardMarkup))
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    InlineKeyboardButton btn = new InlineKeyboardButton(text);
                    btn.CallbackData = callBack;
                    LastRow.Add(btn);
                }
                else
                {
                    InlineKeyboardButton btn = InlineKeyboardButton.WithUrl(text, url);
                    LastRow.Add(btn);
                }
            }

            return this;
        }

        private void ConfigureArray()
        {
            if (typeof(T) == typeof(InlineKeyboardMarkup))
            {
                this.keyboard = new InlineKeyboardButton[Keyboard.Count][];
                for (int i = 0; i < this.keyboard.Length; i++)
                {
                    this.keyboard[i] = new InlineKeyboardButton[this.Keyboard[i].Count];
                }

                for (int i = 0; i < Keyboard.Count; i++)
                {
                    for (int j = 0; j < Keyboard[i].Count; j++)
                    {
                        this.keyboard[i][j] = this.Keyboard[i][j];
                    }
                }
            }


            if (typeof(T) == typeof(ReplyKeyboardMarkup))
            {
                this.keyboard = new KeyboardButton[Keyboard.Count][];
                for (int i = 0; i < this.keyboard.Length; i++)
                {
                    this.keyboard[i] = new KeyboardButton[this.Keyboard[i].Count];
                }

                for (int i = 0; i < Keyboard.Count; i++)
                {
                    for (int j = 0; j < Keyboard[i].Count; j++)
                    {
                        this.keyboard[i][j] = this.Keyboard[i][j];
                    }
                }
            }


            if (typeof(T) == typeof(InlineKeyboardMarkup))
            {
                this.MarkUp = new InlineKeyboardMarkup(this.keyboard as InlineKeyboardButton[][]);
            }

            if (typeof(T) == typeof(ReplyKeyboardMarkup))
            {
                string[][] btns = new string[keyboard.Length][];
                for (int i = 0; i < keyboard.Length; i++)
                {
                    btns[i] = new string[keyboard[i].Length];
                    for (int j = 0; j < keyboard[i].Length; j++)
                    {
                        btns[i][j] = keyboard[i][j].Text;
                    }
                }
                
                //this.MarkUp = new ReplyKeyboardMarkup(this.keyboard as KeyboardButton[][], this.ResizeKeyboard);
                var tmp = (ReplyKeyboardMarkup)btns;
                tmp.ResizeKeyboard = this.ResizeKeyboard;
                this.MarkUp = tmp;
            }


        }

        /// <summary>
        /// Вернуть строки всех кнопок;
        /// </summary>
        public List<string> ButtonTexts()
        {
            if (Equals(this.Keyboard, null) == true ||
                this.Keyboard?.Count == 0)
            {
                return null;
            }
            List<string> res = new List<string>();
            for(var i = 0; i < this.Keyboard.Count; i++)
            {
                for (var j = 0; j < this.Keyboard[i].Count; j++)
                {
                    res.Add(this.Keyboard[i][j].Text);
                }
            }

            return res;
        }
    }
}
