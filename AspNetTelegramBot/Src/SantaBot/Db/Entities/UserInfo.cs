using System;
using AspNetTelegramBot.Src.DbModel.DbBot;

namespace SantaBot.DbModel.Entities
{
    public class UserInfo : IBaseEntity<long>
    {

        public long Id { get; set; }
        public bool SoftDelete { get; set; }
        public DateTime CreateTime { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMale { get; set; }
        public string Photo { get; set; }
        
        public int ChosenByOthersCount { get; set; }
        
        /// <summary>
        /// true - male, false - female, null - no matter
        /// </summary>
        public bool? SearchMale { get; set; }
       
        public int SearchMinAge { get; set; }
        
        public int SearchMaxAge { get; set; }

        /// <summary>
        /// Santa message description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Phone number or instagram account
        /// </summary>
        public string Contact { get; set; }
        
        /// <summary>
        /// foreign key
        /// </summary>
        public long UserId { get; set; }

        public int RandomNumber { get; set; }
    }
}