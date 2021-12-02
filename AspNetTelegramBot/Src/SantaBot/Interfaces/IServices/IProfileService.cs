using System.Threading.Tasks;
using SantaBot.DbModel.Context;
using SantaBot.DbModel.Entities;
using Telegram.Bot;

namespace SantaBot.Interfaces.IServices
{
    public interface IProfileService
    {
        Task<UserInfo> GetRandomProfileForMe(SantaContext db, UserInfo me);
        Task SendProfile(TelegramBotClient bot, long chatId, UserInfo profile, bool choseInline = false, bool isDeleteButton = false, bool showContact = false);
    }
}