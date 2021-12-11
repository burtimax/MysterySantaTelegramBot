using MarathonBot;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.VisualBasic;
using SantaBot.DbModel.Entities;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.Search
{
    public class MainVars
    {
        public static string RulesIntroduction = "Твое письмо отправлено 👌\n\n" +
                                                 "<b>Теперь ты тоже тайный санта</b>\n" +
                                                 $"После <b>{AppConstants.StringDate} 2021</b> ты сможешь увидеть кого выбрал для тебя бот.\n" +
                                                 "А пока читай письма других участников и думай, что ты подаришь своему тайному избраннику.";

        public static string Introduction = "Смотри другие письма 🎅";
        public static string Unexpected = "ЧТО#&!?";
        public static string DeletedFromFavouritesSuccessfully = "Удалено из избранных";
        public static string CancelDelete = "Удаление отменено";
        public static string NotProfiles = "Никого не найдено ☹️";
        public static string EmptyFavouritesList = $"Подожди немного, я покажу тебе твоего избранника {AppConstants.StringDate}, а пока читай письма участников.";
        public static string FavouritesList = "Я выбрал тебе избранника)";
        public static string YourProfileWasAddedToFavourites = "<b>Оповещение</b>\nТвое письмо кто-то добавил в избранное 😘";
        
        public static string BtnSearch = "🎅";
        public static string BtnChosen = "❤️";
        public static string BtnEditProfile = "️✉️";
        public static string BtnEditSearchParams = "🔍";

        public static string BtnInlineChoice = "Выбрать";
        public static string BtnInlineDelete = "Удалить";
        public static string BtnInlineDeleteWithConfirm = "Удалить";
        public static string BtnInlineShowNext = "Показать следующего";
        public static string BtnInlineConfirmDeleteYes = "Да, удалить";
        public static string BtnInlineConfirmDeleteNo = "Отмена";
        
        
        
        public static string ChoseInlineDataPrefix = "ChoseProfile";
        public static string DeleteInlineDataPrefix = "DeleteProfile";
        public static string ShowNextInlineDataPrefix = "ShowNextProfile";
        public static string DeleteWithConfirmInlineDataPrefix = "ConfirmDeleteProfile";
        public static string ConfirmedDeleteInlineDataPrefix = "ConfirmedDeleteProfile";
        public static string CanceledDeleteInlineDataPrefix = "CanceledDeleteProfile";

        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboardMarkup =
            new MarkupWrapper<ReplyKeyboardMarkup>()
                .NewRow()
                .Add(BtnSearch)
                .Add(BtnChosen)
                .Add(BtnEditProfile);



        public static MarkupWrapper<InlineKeyboardMarkup> InlineMarkUpChoseProfile(long userId)
        {
            return new MarkupWrapper<InlineKeyboardMarkup>()
                .NewRow()
                .Add(BtnInlineChoice, $"{ChoseInlineDataPrefix}{userId}");
        }

        public static MarkupWrapper<InlineKeyboardMarkup> InlineMarkUpDeleteProfile(long userId)
        {
            return new MarkupWrapper<InlineKeyboardMarkup>()
                .NewRow()
                .Add(BtnInlineDelete, $"{DeleteInlineDataPrefix}{userId}");
        }
        
        
        public static MarkupWrapper<InlineKeyboardMarkup> GetInlineForFavourite(long favUserId, long? favNextUserId)
        {
            var markup = new MarkupWrapper<InlineKeyboardMarkup>()
                .NewRow()
                .Add(BtnInlineDeleteWithConfirm, $"{DeleteWithConfirmInlineDataPrefix}{favUserId}");

            if (favNextUserId != null)
            {
                markup = markup.NewRow().Add(BtnInlineShowNext, $"{ShowNextInlineDataPrefix}{favNextUserId}");
            }

            return markup;
        }
        
        public static MarkupWrapper<InlineKeyboardMarkup> GetDeleteConfirmationInline(long chosenUserId)
        {
            var markup = new MarkupWrapper<InlineKeyboardMarkup>()
                .NewRow()
                .Add(BtnInlineConfirmDeleteYes, $"{ConfirmedDeleteInlineDataPrefix}{chosenUserId}")
                .Add(BtnInlineConfirmDeleteNo, $"{CanceledDeleteInlineDataPrefix}{chosenUserId}");

            return markup;
        }
        
    }
}