using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetTelegramBot.Src.DbModel.DbBot
{
    public class Message : IBaseEntity<Guid>
    {
        
        /// <summary>
        /// DB Id
        /// </summary>
        public Guid Id { get; set; }

        public bool SoftDelete { get; set; }

        /// <summary>
        /// Telegram ChatId
        /// </summary>
        public long ChatId { get; set; }

        public Chat Chat { get; set; }
        
        /// <summary>
        /// Telegram Message Id
        /// </summary>
        public long TelegramMessageId { get; set; }
        
        /// <summary>
        /// Message type (text, photo, audio, document)
        /// </summary>
        public string Type { get; set; }

        
        /// <summary>
        /// Inner message content (text, photopath, filepath, other)
        /// </summary>
        public string Content { get; set; }
        
        public DateTime CreateTime { get; set; }
    }
}
