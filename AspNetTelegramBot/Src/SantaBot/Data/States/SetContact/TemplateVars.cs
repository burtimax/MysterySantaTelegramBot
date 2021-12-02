using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.SetContact
{
    public class SetContactVars
    {
        public static string Introduction = "У тебя нет Nickname в телеграме ☹️\nОставь ссылку на твой instagram, ВК или номер телефона. \nНапиши как с тобой связаться, чтобы твой тайный санта смог тебя найти.";
        public static string Unexpected = "У тебя нет Nickname в телеграме ☹️\nОставь ссылку на твой instagram, ВК или номер телефона. \nНапиши как с тобой связаться, чтобы твой тайный санта смог тебя найти.";
        public static string BtnSetCurrent = "Оставить прежние контакты";

        
        
        public static MarkupWrapper<ReplyKeyboardMarkup> GetDefaultValueKeyboard()
        {
            return new MarkupWrapper<ReplyKeyboardMarkup>()
                .NewRow()
                .Add(BtnSetCurrent);

        }
    }
}