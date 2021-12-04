using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data.States;
using MarathonBot.SantaBot.Helpers;
using SantaBot.Data.States.SetDescription;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotAdditionalTools.Src.Enums;
using TelegramBotAdditionalTools.Src.Tools;

namespace SantaBot.Data.States.SetPhoto
{
    public class SetPhotoBotStateProcessor : BaseSantaStateProcessor
    {
        public SetPhotoBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Photo;
        }

        protected override async Task<Hop> TextMessageProcess(Message mes)
        {
            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);
            
            if (mes.Text == SetPhotoVars.BtnSetCurrent)
            {
                var hop = successHop;
                if (ui.Description != null)
                {
                    hop.PriorityKeyboard = SetDescriptionVars.GetDefaultValueKeyboard().Value;
                }

                return hop;
            }

            return defaultHop;
        }

        protected override async Task<Hop> PhotoMessageProcess(Message mes)
        {

            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);
            
            InboxMessage messageData = new InboxMessage(Bot, mes);
            var photo = await messageData.GetMessagePhotoAsync(PhotoQuality.High);

            var photoPath = PhotoHelper.GetPhotoFilePathForUser(CurrentUser.Id.ToString());
            
            photo.File.SaveFile(photoPath);
            
            ui.Photo = PhotoHelper.GetPhotoFileNameForUser(CurrentUser.Id.ToString());
            await _dbSanta.Repos.UserInfo.UpdateAsync(ui);

            //Переходим в состояние написание письма
            var hop = new Hop(new HopInfo("SetDescription", SetDescriptionVars.Introduction, HopType.CurrentLevelHop),
                CurrentState,
                StateStorage.Get("SetDescription"));
            
            if (ui.IsMale)
            {
                hop.PriorityIntroduction = SetDescriptionVars.IntroductionMale;
            }
            else
            {
                hop.PriorityIntroduction = SetDescriptionVars.IntroductionFemale;
            }
            if (ui.Description != null)
            {
                hop.PriorityKeyboard = SetDescriptionVars.GetDefaultValueKeyboard().Value;
            }

            return hop;
        }
    }
}