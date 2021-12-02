using Microsoft.EntityFrameworkCore.Update;
using SantaBot.DbModel.Entities;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.Search
{
    public class MainVars
    {
        public static string Introduction = "Поиск";
        public static string Unexpected = "ЧТО#&!?";
        public static string DeletedFromFavouritesSuccessfully = "Удалено из избранных";
        public static string CancelDelete = "Удаление отменено";
        public static string NotProfiles = "Никого не найдено ☹️, попробуй изменить параметры поиска";
        public static string EmptyFavouritesList = "Список избранных пуст";
        public static string FavouritesList = "Список избранных";
        public static string YourProfileWasAddedToFavourites = "Твое письмо кто-то добавил в избранное 😘";
        
        public static string BtnSearch = "🎅";
        public static string BtnChosen = "❤️";
        public static string BtnEditProfile = "✒️✉️";
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

        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboardMarkup = new MarkupWrapper<ReplyKeyboardMarkup>()
            .NewRow()
            .Add(BtnSearch)
            .Add(BtnChosen)
            .Add(BtnEditProfile)
            .Add(BtnEditSearchParams);


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