using MarathonBot.SantaBot.Data.Commands;
using MarathonBot.Src.Bot.Code;

namespace MarathonBot.SantaBot.Data
{
    public class SantaBotCommandStorage : BotCommandStorage
    {
       
        public static string ActivityReport = "/get";
        public static string SendMe = "/sendme";
        public static string SendAll = "/sendall";
        public static string Hide = "/hide";
        public static string Unhide = "/unhide";
        public static string SendOne = "/sendone";

        protected override void InitCommands()
        {
            AddStaticCommand(new BotActivityReportCommand(ActivityReport));
            AddDynamicCommand(new BotTestSendAllCommand(SendMe));
            AddDynamicCommand(new BotSendAllCommand(SendAll));
            AddDynamicCommand(new HideParticipantCommand(Hide));
            AddDynamicCommand(new UnHideParticipantCommand(Unhide));
            AddDynamicCommand(new SendToOneUserCommand(SendOne));
        }
    }
}