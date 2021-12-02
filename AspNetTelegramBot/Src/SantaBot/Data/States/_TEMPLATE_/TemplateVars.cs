using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States._TEMPLATE_
{
    public class _TEMPLATE_Vars
    {
        public static string Introduction = "Привет";
        public static string Unexpected = "ЧТО!#&?";

        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboardMarkup = new MarkupWrapper<ReplyKeyboardMarkup>()
            .NewRow()
            .Add("Hello")
            .Add("Bye");

    }
}