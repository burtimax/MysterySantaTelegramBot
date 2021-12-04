using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States._TEMPLATE_
{
    public class ConfirmProfileVars
    {
        public static string Introduction = "Это твое письмо тайному санте.\n(Его видят другие пользователи)\n\nХочешь изменить письмо?";
        public static string DefaultIntroduction = "Хочешь изменить письмо?";
        public static string Unexpected = "ЧТО!#&?";
        
        public static string BtnNorm = "Мне нравится";
        public static string BtnEdit = "Изменить";

        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboardMarkup = new MarkupWrapper<ReplyKeyboardMarkup>()
            .NewRow()
            .Add(BtnNorm)
            .Add(BtnEdit);

    }
}