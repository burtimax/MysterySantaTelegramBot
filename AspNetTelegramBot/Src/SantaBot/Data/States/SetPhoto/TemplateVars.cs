using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.SetPhoto
{
    public class SetPhotoVars
    {
        public static string Introduction = "Теперь пришли свое фото (его будут видеть другие пользователи)";
        public static string Unexpected = "Пришли только фотографию";
        public static string BtnSetCurrent = "Оставить текущее фото";

        public static MarkupWrapper<ReplyKeyboardMarkup> GetDefaultValueKeyboard()
        {
            return new MarkupWrapper<ReplyKeyboardMarkup>()
                .NewRow()
                .Add(BtnSetCurrent);

        }

    }
}