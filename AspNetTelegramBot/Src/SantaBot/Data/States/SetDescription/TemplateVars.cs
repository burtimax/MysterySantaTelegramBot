using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.SetDescription
{
    public class SetDescriptionVars
    {
        public static string Introduction = "Напиши письмо для тайного санты ✉️🧑‍🎄"; //ToDo иконка письма и деда мороза
        public static string Unexpected = "Напиши письмо для тайного санты ✉️🧑‍🎄";
        public static string BtnSetCurrent = "Оставить текущее письмо";

        public static MarkupWrapper<ReplyKeyboardMarkup> GetDefaultValueKeyboard()
        {
            return new MarkupWrapper<ReplyKeyboardMarkup>()
                .NewRow()
                .Add(BtnSetCurrent);
        }

    }
}