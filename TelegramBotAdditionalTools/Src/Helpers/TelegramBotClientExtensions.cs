using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBotAdditionalTools.Src.Helpers
{
    public static class TelegramBotClientExtensions
    {
        /// <summary>
        /// Get UserId or ChatId from Update (Обрати внимание, ели чат приватный, то UserId == ChatId, иначе не так)
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static ChatId GetChatId(this TelegramBotClient bot, Update update)
        {
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    return update.Message.Chat.Id;
                    break;

                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.From.Id;
                    break;

                case UpdateType.ChosenInlineResult:
                    return update.ChosenInlineResult.From.Id;
                    break;

                case UpdateType.ChannelPost:
                    return update.ChannelPost.Chat.Id;
                    break;

                case UpdateType.EditedChannelPost:
                    return update.EditedChannelPost.Chat.Id;
                    break;

                case UpdateType.EditedMessage:
                    return update.EditedMessage.Chat.Id;
                    break;

                case UpdateType.InlineQuery:
                    return update.InlineQuery.From.Id;
                    break;

                case UpdateType.PollAnswer:
                    return update.PollAnswer.User.Id;
                    break;

                case UpdateType.PreCheckoutQuery:
                    return update.PreCheckoutQuery.From.Id;
                    break;

                case UpdateType.ShippingQuery:
                    return update.ShippingQuery.From.Id;
                    break;
            }

            return null;
        }
    }
}
