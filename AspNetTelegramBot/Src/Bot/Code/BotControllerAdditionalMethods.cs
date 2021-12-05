// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using AspNetTelegramBot.Src.Bot.Abstract;
// using AspNetTelegramBot.Src.DbModel.DbBot;
// using Chat = Telegram.Bot.Types.Chat;
// using Message = Telegram.Bot.Types.Message;
// using User = Telegram.Bot.Types.User;
//
// namespace AspNetTelegramBot.Src.Bot.Code
// {
//     public class BotControllerAdditionalMethods : IBotControllerAdditionalMethods
//     {
//         public BotContext Db { get; set; }
//
//
//         public virtual async Task<bool> IsUserInBot(long userId)
//         {
//             return await Db.Methods.IsUserInBot(userId);
//         }
//
//         public virtual async Task<bool> IsChatInBot(long chatId)
//         {
//             return await Db.Methods.IsChatInBot(chatId);
//         }
//
//         public virtual async Task AddUserToBot(User user)
//         {
//             await Db.Methods.AddUserToBot(user);
//         }
//
//         public virtual async Task AddChatToBot(Chat chat)
//         {
//             await Db.Methods.AddChatToBot(chat);
//         }
//
//         public virtual async Task SaveMessage(Message mes)
//         {
//             await Db.Methods.SaveMessage(mes);
//         }
//
//         public Task UpdateUserData(long userId, string userName)
//         {
//             throw new NotImplementedException();
//         }
//
//         public virtual async Task<Src.DbModel.DbBot.User> GetUser(long userId)
//         {
//             return await Db.Methods.GetUser(userId);
//         }
//
//         public virtual async Task<Src.DbModel.DbBot.Chat> GetChat(long chatId)
//         {
//             return await Db.Methods.GetChat(chatId);
//         }
//
//         public async Task SetChatData(Src.DbModel.DbBot.Chat chat)
//         {
//             await Db.Methods.SetChatData(chat);
//         }
//
//     }
// }
