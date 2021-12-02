using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.SetGender
{
    public class SetGenderVars
    {
        public static string Introduction = "Кто ты?";
        public static string Unexpected = "Кто ты?";
        
        public static string BtnMale = "Я мальчик";
        public static string BtnFemale = "Я девочка";

        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboardMarkup = new MarkupWrapper<ReplyKeyboardMarkup>()
            .NewRow()
            .Add(BtnMale)
            .Add(BtnFemale);

    }
}