using System.Threading.Tasks;
using SantaBot.DbModel.Entities;

namespace SantaBot.Interfaces.IServices
{
    public interface IChoiceService
    {
        Task<string> TryAddProfileToChosen(long userId, long chosenUserId);
        Task<string> DeleteProfileFromChosen(long userId, long chosenUserId);
    }
}