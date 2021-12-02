using AspNetTelegramBot.Src.Bot.Code;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.Start
{
    public class SetNameVars
    {
        public static string Introduction = "Как тебя зовут?";
        public static string Unexpected = "Напиши свое имя пожалуйста)";
        
        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboardMarkup = new MarkupWrapper<ReplyKeyboardMarkup>()
            .NewRow()
            .Add("Tima")
            .Add("No name", "no_name");

        public static MarkupWrapper<ReplyKeyboardMarkup> GetDefaultValueKeyboard(string defaultName)
        {
            return new MarkupWrapper<ReplyKeyboardMarkup>()
                .NewRow()
                .Add(defaultName);
            
        }

        
    }
}