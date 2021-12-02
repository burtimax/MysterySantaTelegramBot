using System.Threading.Tasks;
using Telegram.Bot;
using SantaBot.Code;

namespace MarathonBot.Helpers
{
    public class BotSingleton
    {
        private static global::SantaBot.Code.SantaBot _bot;

        public static async Task<global::SantaBot.Code.SantaBot> GetInstanceAsync()
        {
            if (_bot == null)
            {
                var telegramClient = new TelegramBotClient(AppConstants.BotToken);
                await telegramClient.SetWebhookAsync(AppConstants.BotWebhook);
                _bot = new global::SantaBot.Code.SantaBot(telegramClient);
            }
           
            return _bot;
        }
        
    }
}