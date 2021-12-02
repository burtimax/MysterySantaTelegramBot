using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.DbModel.DbBot;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.Enums;


namespace AspNetTelegramBot.Src.Bot.DbModel.DbMethods
{
    public class BotContextDbMethods : IBotContextDbMethods
    {
        private BotContext db;

        public BotContextDbMethods(BotContext db)
        {
            this.db = db;
        }

        public async Task<bool> IsUserInBot(long userId)
        {
            return (await db.User.FirstOrDefaultAsync(u => u.Id == userId)) != null;
        }

        public async Task<bool> IsChatInBot(long chatId)
        {
            return (await db.Chat.FirstOrDefaultAsync(ch=>ch.Id == chatId) != null);
        }

        public async Task AddUserToBot(Telegram.Bot.Types.User user)
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

        public async Task AddChatToBot(Telegram.Bot.Types.Chat chat)
        {
            Chat c = new Chat()
            {
                Id = chat.Id,
                State = "Start",
            };

            await db.Chat.AddAsync(c);
            await db.SaveChangesAsync();
        }

        public async Task SaveMessage(Telegram.Bot.Types.Message mes)
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

        public async Task<User> GetUser(long userId)
        {
            return await db.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<Chat> GetChat(long chatId)
        {
            return await db.Chat.FirstOrDefaultAsync(ch => ch.Id == chatId);
        }

        public async Task SetChatData(Chat chat)
        {
            Chat c = await db.Methods.GetChat(chat.Id);
            c.State = chat.State;
            c.StateData = chat.StateData;
            await db.SaveChangesAsync();
        }
    }
}
