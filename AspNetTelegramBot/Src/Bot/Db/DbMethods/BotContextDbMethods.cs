using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Abstract;
using AspNetTelegramBot.Src.DbModel.DbBot;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.Enums;


namespace AspNetTelegramBot.Src.Bot.DbModel.DbMethods
{
    public class BotContextDbMethods : IBotControllerAdditionalMethods
    {
        

        public BotContextDbMethods()
        {
           
        }

        public async Task<bool> IsUserInBot(BotContext db, long userId)
        {
            return (await db.User.FirstOrDefaultAsync(u => u.Id == userId)) != null;
        }

        public async Task<bool> IsChatInBot(BotContext db, long chatId)
        {
            return (await db.Chat.FirstOrDefaultAsync(ch=>ch.Id == chatId) != null);
        }

        public async Task AddUserToBot(BotContext db, Telegram.Bot.Types.User user)
        {
            User u = new User()
            {
                TelegramFirstname = user.FirstName,
                TelegramLastname = user.LastName,
                Username = user.Username,
                Id = user.Id,
                Role = "CurrentUser",
                Status = "active",
            };

            await db.User.AddAsync(u);
            await db.SaveChangesAsync();
        }

        public async Task AddChatToBot(BotContext db, Telegram.Bot.Types.Chat chat)
        {
            Chat c = new Chat()
            {
                Id = chat.Id,
                State = "Start",
            };

            await db.Chat.AddAsync(c);
            await db.SaveChangesAsync();
        }

        public async Task SaveMessage(BotContext db, Telegram.Bot.Types.Message mes)
        {
            //save only text messages;

            if (mes.Type != MessageType.Text) return;
            Message m = new Message()
            {
                ChatId = mes.Chat.Id,
                TelegramMessageId = mes.MessageId,
                Type = mes.Type.ToString(),
                Content = mes.Text,
            };

            await db.Message.AddAsync(m);
            await db.SaveChangesAsync();

        }

        public async Task<User> GetUser(BotContext db, long userId)
        {
            return await db.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<Chat> GetChat(BotContext db, long chatId)
        {
            return await db.Chat.FirstOrDefaultAsync(ch => ch.Id == chatId);
        }

        public async Task SetChatData(BotContext db, Chat chat)
        {
            Chat c = await GetChat(db, chat.Id);
            c.State = chat.State;
            c.StateData = chat.StateData;
            await db.SaveChangesAsync();
        }
    }
}
