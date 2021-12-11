namespace MarathonBot.Bot.Helpers
{
    public class BotHelper
    {
        public static string CropMediaCaptionAccurate(string mediaCaption)
        {
            if (mediaCaption.Length < 1024)
            {
                return mediaCaption;
            }

            string s = mediaCaption.Substring(0, 1020);
            int lastSpaceIndex = s.LastIndexOf(" ");

            if (lastSpaceIndex == -1)
            {
                return $"{s}...";
            }

            s = s.Substring(0, lastSpaceIndex);
            s = s.TrimEnd(' ', ',', '.');
            return $"{s}...";

        }
    }
}