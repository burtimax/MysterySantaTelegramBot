using MarathonBot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States._TEMPLATE_
{
    public class SetSearchGenderVars
    {
        public static string Introduction = "Твое письмо отправлено 👌\n\n" +
                                            "<b>Теперь ты тоже тайный санта</b>\n" +
                                            "Ты можешь выбрать максимум <b>3 человека</b>, кому ты сделаешь подарок.\n" +
                                            "После <b>27 декабря 2021</b> ты сможешь увидеть контакты тех, кого добавил в список избранных, чтобы связаться с ними и подарить подарок.\n" +
                                            "А пока читай письма, добавляй в избранное понравившиеся письма и жди, что тебя тоже кто-то добавит.\n\n" +
                                            " Давай определим, кому ты хочешь сделать подарок";
        public static string IntroductionShort = "Давай определим, кому ты хочешь сделать подарок";
        public static string Unexpected = "Выбирай, кому ты хочешь сделать подарок";

        public static string BtnMale = "Мальчику";
        public static string BtnFemale = "Девочке";
        public static string BtnBoth = "Все равно";
        
        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboardMarkup = new MarkupWrapper<ReplyKeyboardMarkup>()
            .NewRow()
            .Add(BtnMale)
            .Add(BtnFemale)
            .Add(BtnBoth);

    }
}