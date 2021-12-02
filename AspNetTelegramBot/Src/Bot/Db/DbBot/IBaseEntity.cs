using System;

namespace AspNetTelegramBot.Src.DbModel.DbBot
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
        public bool SoftDelete { get; set; }
        DateTime CreateTime { get; set; }
    }
}