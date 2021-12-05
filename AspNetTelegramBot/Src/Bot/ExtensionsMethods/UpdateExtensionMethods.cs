using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AspNetTelegramBot.Src.Bot.ExtensionsMethods
{
    public static class UpdateExtensionMethods
    {
        /// <summary>
        /// Is Update allow to process
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public static bool IsAllowableUpdate(this Update update)
        {
            if (update.Type == UpdateType.Message ||
                update.Type == UpdateType.CallbackQuery)
            {
                return true;
            }

            return false;
        }

        public static Chat GetChat(this Update update)
        {
            Chat chat = null;
            switch (update.Type)
            {
                case UpdateType.Message:
                    chat = update.Message.Chat;
                    break;
                case UpdateType.CallbackQuery:
                    chat = update.CallbackQuery.Message.Chat;
                    break;
                case UpdateType.ChannelPost:
                    chat = update.ChannelPost.Chat;
                    break;
                case UpdateType.EditedChannelPost:
                    chat = update.EditedChannelPost.Chat;
                    break;
                case UpdateType.EditedMessage:
                    chat = update.EditedMessage.Chat;
                    break;
                case UpdateType.ChatMember:
                    chat = update.ChatMember.Chat;
                    break;
                case UpdateType.MyChatMember:
                    chat = update.MyChatMember.Chat;
                    break;
                case UpdateType.ChatJoinRequest:
                    chat = update.ChatJoinRequest.Chat;
                    break;
            }

            return chat;
        }

        /// <summary>
        /// Get User object from Telegram Update
        /// </summary>
        /// <param name="update">Update</param>
        /// <returns>User object</returns>
        public static User GetUser(this Update update)
        {
            User user = null;
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    user = update.Message.From;
                    break;
                case UpdateType.CallbackQuery:
                    user = update.CallbackQuery.From;
                    break;
                case UpdateType.ChosenInlineResult:
                    user = update.ChosenInlineResult.From;
                    break;
                case UpdateType.ChannelPost:
                    user = update.ChannelPost.From;
                    break;
                case UpdateType.EditedChannelPost:
                    user = update.EditedChannelPost.From;
                    break;
                case UpdateType.EditedMessage:
                    user = update.EditedMessage.From;
                    break;
                case UpdateType.InlineQuery:
                    user = update.InlineQuery.From;
                    break;
                case UpdateType.PollAnswer:
                    user = update.PollAnswer.User;
                    break;
                case UpdateType.PreCheckoutQuery:
                    user = update.PreCheckoutQuery.From;
                    break;
                case UpdateType.ShippingQuery:
                    user = update.ShippingQuery.From;
                    break;
                case UpdateType.ChatMember:
                    user = update.ChatMember.From;
                    break;
                case UpdateType.MyChatMember:
                    user = update.MyChatMember.From;
                    break;
                case UpdateType.ChatJoinRequest:
                    user = update.ChatJoinRequest.From;
                    break;
            }

            return user;
        }
    }
}
