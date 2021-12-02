using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.SetSearchAge
{
    public class SetSearchAgeVars
    {
        public static string Introduction = "Какого возраста (он/она)?\nВведи два числа.\n(Например <b>5</b>-<b>25</b>)";
        public static string Unexpected = "Какого возраста (он/она)?\nВведи два числа.\n(Например <b>5</b>-<b>25</b>)";
        
        public static string BtnAnyAge = "Любой возраст";

        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboardMarkup = new MarkupWrapper<ReplyKeyboardMarkup>()
            .NewRow()
            .Add(BtnAnyAge);
        
        public static MarkupWrapper<ReplyKeyboardMarkup> GetDefaultValueKeyboard(int previousMin, int previousMax)
        {
            return new MarkupWrapper<ReplyKeyboardMarkup>()
                .NewRow()
                .Add(previousMin.ToString() + " - " + previousMax.ToString())
                .NewRow()
                .Add(BtnAnyAge);

        }

    }
}