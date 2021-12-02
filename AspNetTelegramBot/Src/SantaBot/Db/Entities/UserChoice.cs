using System;
using AspNetTelegramBot.Src.DbModel.DbBot;

namespace SantaBot.DbModel.Entities
{
    public class UserChoice : IBaseEntity<long>
    {
        public long Id { get; set; }
        public bool SoftDelete { get; set; }
        public DateTime CreateTime { get; set; }

        public DateTime? DeleteTime { get; set; }
        
        /// <summary>
        /// Shosen CurrentUser for present
        /// </summary>
        public long ChosenUserId { get; set; }
        
        public long UserId { get; set; }


        public UserChoice(long userId, long chosenUserId)
        {
            this.UserId = userId;
            this.ChosenUserId = chosenUserId;
        }
        
        public UserChoice(){}

        
    }
}