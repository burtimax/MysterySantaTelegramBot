using System;
using System.ComponentModel.DataAnnotations.Schema;
using AspNetTelegramBot.Src.DbModel.DbBot;

namespace SantaBot.DbModel.Entities
{
    public class ShowHistory : IBaseEntity<long>
    {
        public long Id { get; set; }
        public bool SoftDelete { get; set; }
        public DateTime CreateTime { get; set; }

        public long UserId { get; set; }

        public long ShownUserId { get; set; }

        public int? ShowCount { get; set; } = 0;

    }
}