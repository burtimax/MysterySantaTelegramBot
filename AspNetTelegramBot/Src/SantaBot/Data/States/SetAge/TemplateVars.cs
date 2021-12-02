using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States._TEMPLATE_
{
    public class SetAgeVars
    {
        public static string Introduction = "Сколько тебе лет?";
        public static string Unexpected = "Сколько тебе лет?";

        

        public static MarkupWrapper<ReplyKeyboardMarkup> GetDefaultValueKeyboard(int age)
        {
            return new MarkupWrapper<ReplyKeyboardMarkup>()
                .NewRow()
                .Add(age.ToString());

        }
        
    }
}