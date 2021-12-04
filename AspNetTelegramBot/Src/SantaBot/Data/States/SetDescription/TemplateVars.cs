using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.SetDescription
{
    public class SetDescriptionVars
    {
        public static string Introduction = "Напиши письмо для тайного санты ✉️🧑‍🎄\n" +
                                                "Расскажи о своем поведении в этом году и что хочешь бы получить в подарок?"; 
        public static string IntroductionMale = "Напиши письмо для тайного санты ✉️🧑‍🎄\n" +
                                            "Расскажи как ты себя вел в этом году и что хотел бы получить в подарок?"; 
        public static string IntroductionFemale = "Напиши письмо для тайного санты ✉️🧑‍🎄\n" +
                                                "Расскажи как ты себя вела в этом году и что хотела бы получить в подарок?"; 
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