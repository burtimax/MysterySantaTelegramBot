using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbModels = AspNetTelegramBot.Src.DbModel.DbBot;
using TelegramModel = Telegram.Bot.Types;

namespace AspNetTelegramBot.Src.Bot.Abstract
{
    public interface IBotControllerAdditionalMethods
    {
        /// <summary>
        /// Has User with Id in Db?
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> IsUserInBot(DbModels.BotContext db, long userId);

        /// <summary>
        /// Has Chat with Id in Db?
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        Task<bool> IsChatInBot(DbModels.BotContext db, long chatId);

        /// <summary>
        /// Add Telegram type User to DB
        /// </summary>
        /// <param name="user">Telegram CurrentUser object</param>
        Task AddUserToBot(DbModels.BotContext db, TelegramModel.User user);

        /// <summary>
        /// Add Telegram type Chat to DB
        /// </summary>
        /// <param name="chat">Telegram Chat object</param>
        Task AddChatToBot(DbModels.BotContext db, TelegramModel.Chat chat);

        /// <summary>
        /// Add Telegram Message type to DB
        /// </summary>
        /// <param name="mes">Telegram Message object</param>
        Task SaveMessage(DbModels.BotContext db, TelegramModel.Message mes);

        /// <summary>
        /// Get User model data from DB
        /// </summary>
        /// <param name="userId">Id</param>
        /// <returns>User model</returns>
        Task<DbModels.User> GetUser(DbModels.BotContext db, long userId);

        /// <summary>
        /// Get Chat model data from Db
        /// </summary>
        /// <param name="chatId">Id</param>
        /// <returns>Chat model</returns>
        Task<DbModels.Chat> GetChat(DbModels.BotContext db, long chatId);

        /// <summary>
        /// Set Chat model data to Db (can change CurrentState and chatData)
        /// </summary>
        /// <param name="chat">Chat model object</param>
        Task SetChatData(DbModels.BotContext db, DbModels.Chat chat);
    }
}
