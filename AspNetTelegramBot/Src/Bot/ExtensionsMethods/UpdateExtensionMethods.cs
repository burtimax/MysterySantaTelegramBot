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
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update.Message.Chat;
                    break;

                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.Message.Chat;
                    break;
            }

            return null;
        }

        /// <summary>
        /// Get User object from Telegram Update
        /// </summary>
        /// <param name="update">Update</param>
        /// <returns>User object</returns>
        public static User GetUser(this Update update)
        {
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    return update.Message.From;
                    break;

                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.From;
                    break;

                case UpdateType.ChosenInlineResult:
                    return update.ChosenInlineResult.From;
                    break;

                case UpdateType.ChannelPost:
                    return update.ChannelPost.From;
                    break;

                case UpdateType.EditedChannelPost:
                    return update.EditedChannelPost.From;
                    break;

                case UpdateType.EditedMessage:
                    return update.EditedMessage.From;
                    break;

                case UpdateType.InlineQuery:
                    return update.InlineQuery.From;
                    break;

                case UpdateType.PollAnswer:
                    return update.PollAnswer.User;
                    break;

                case UpdateType.PreCheckoutQuery:
                    return update.PreCheckoutQuery.From;
                    break;

                case UpdateType.ShippingQuery:
                    return update.ShippingQuery.From;
                    break;
            }

            return null;
        }
    }
}
