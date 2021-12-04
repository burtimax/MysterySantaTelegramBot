using System;
using System.Threading.Tasks;
using SantaBot.DbModel.Context;
using SantaBot.DbModel.Entities;
using SantaBot.Interfaces.IServices;
using Telegram.Bot.Types.Payments;

namespace MarathonBot.SantaBot.Service
{
    public class ChoiceService : IChoiceService
    {
        public const string SUCCESS = "";
        public const string MAX_CHOSEN = "Максимально можно выбрать только 3 человек";
        
        private SantaContext _db;
        
        public ChoiceService()
        {
            _db = new SantaContext();
        }
        
        public async Task<string> TryAddProfileToChosen(long userId, long chosenUserId)
        {
            var count = await _db.Repos.UserChoice.GetChosenCount(userId);

            if (count > AppConstants.MaxChoice)
            {
                return MAX_CHOSEN;
            }

            UserChoice newChoice = new UserChoice(userId, chosenUserId);
            await _db.Repos.UserChoice.AddAsync(newChoice);

            var chosenUserInfo = await _db.Repos.UserInfo.GetByUserId(chosenUserId);
            if (chosenUserInfo != null)
            {
                chosenUserInfo.ChosenByOthersCount += 1;
                await _db.Repos.UserInfo.UpdateAsync(chosenUserInfo);
            }

            return SUCCESS;
        }

        public async Task<string> DeleteProfileFromChosen(long userId, long chosenUserId)
        {
            var choiceItem = await _db.Repos.UserChoice.GetUserChoice(userId, chosenUserId);

            if (choiceItem == null)
            {
                return SUCCESS;
            }

            choiceItem.SoftDelete = true;
            await _db.Repos.UserChoice.UpdateAsync(choiceItem);
            
            var chosenUserInfo = await _db.Repos.UserInfo.GetByUserId(chosenUserId);
            if (chosenUserInfo != null)
            {
                chosenUserInfo.ChosenByOthersCount = Math.Max(chosenUserInfo.ChosenByOthersCount - 1, 0);
                await _db.Repos.UserInfo.UpdateAsync(chosenUserInfo);
            }
            
            return SUCCESS;
        }
    }
}