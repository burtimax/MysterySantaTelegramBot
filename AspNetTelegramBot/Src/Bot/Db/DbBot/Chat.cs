using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.Src.Bot.Code;

namespace AspNetTelegramBot.Src.DbModel.DbBot
{
    public class Chat : IBaseEntity<long>
    {
        /// <summary>
        /// TelegramId
        /// </summary>
        public long Id { get; set; }

        public bool SoftDelete { get; set; }

        ///// <summary>
        ///// Foreign key
        ///// </summary>
        //public long UserId { get; set; }

        ///// <summary>
        ///// Navigation property (User)
        ///// </summary>
        //public User User { get; set; }

        [NotMapped]
        private string _state = null;

        
        /// <summary>
        /// Info about Chat CurrentState in Bot
        /// </summary>
        public string State {
            get { return _state; }
            set { _state = value; }
        }


        /// <summary>
        /// Data for CurrentState using
        /// </summary>
        public string StateData 
        { 
            get { return GetData(); } 
            set { SetData(value); }
        }

        
        /// <summary>
        /// DateNow default
        /// </summary>
        public DateTime CreateTime { get; set; }


        private Regex dataRegex = new Regex(@"(?<pair>\[\s*(?<key>([^\]\s]+)|(([^\]]+[^\s])))\s*\]\s*\:\s*\[\s*(?<value>([^\]\s]+)|(([^\]]+[^\s]))|(\s*))\s*\]\s*)");

        /// <summary>
        /// Set CurrentState with hop type logic
        /// </summary>
        /// <param name="stateName">next CurrentState name</param>
        /// <param name="hopType">hop type</param>
        public void SetState(string nextStateName, HopType hopType)
        {
            HopStateManager.ChangeState(ref _state, nextStateName, hopType);
        }

        /// <summary>
        /// Get string data from dictionary data
        /// </summary>
        /// <returns></returns>
        private string GetData()
        {
            if (Data == null || Data.Count == 0) return "";

            string res = "";
            foreach (var item in Data)
            {
                res += $"[{item.Key}]:[{item.Value}]";
            }

            return res;
        }

        private void SetData(string data)
        {
            if(this.Data == null) this.Data = new Dictionary<string, string>();
            this.Data.Clear();

            if (data == null) data = "";

            var matches = dataRegex.Matches(data);

            foreach (Match match in matches)
            {
                this.Data.Add(match.Groups["key"].Value, match.Groups["value"].Value);
            }
        }

        private Dictionary<string, string> _data;
            /// <summary>
            /// Данные, которые передаются в состояние
            /// </summary>
        [NotMapped]
        public Dictionary<string, string> Data {
            get
            {
                if(_data == null) _data = new Dictionary<string, string>();
                return _data;
            }
            set { this._data = value; }
        }

        /// <summary>
        /// Safe adding data item with contains condition
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void AddDataItemSafe(string key, string data)
        {
            if (Data.ContainsKey(key))
            {
                Data[key] = data;
            }
            else
            {
                Data.Add(key, data);
            }
        }

        /// <summary>
        /// Safe remove item with contains condition
        /// </summary>
        /// <param name="key"></param>
        public void RemoveDataItemSafe(string key)
        {
            if (Data.ContainsKey(key))
            {
                Data.Remove(key);
            }
        }

        /// <summary>
        /// Получить имя текущего состояния из строки
        /// </summary>
        /// <returns></returns>
        public string GetCurrentStateName()
        {
            return HopStateManager.GetCurrentStateName(this.State);
        }

        public string GetPreviousStateName()
        {
            return HopStateManager.GetPreviousStateName(this.State);
        }
    }
}
