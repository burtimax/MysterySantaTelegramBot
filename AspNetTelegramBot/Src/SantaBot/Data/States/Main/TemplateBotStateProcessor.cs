using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using AspNetTelegramBot.Src.DbModel.DbBot;
using BotLibrary.Classes.Helpers;
using MarathonBot;
using MarathonBot.SantaBot.Data.States;
using MarathonBot.SantaBot.Helpers;
using MarathonBot.SantaBot.Service;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using SantaBot.Data.States._TEMPLATE_;
using SantaBot.Data.States.Search;
using SantaBot.Data.States.Start;
using SantaBot.DbModel.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.MessageData;
using TelegramBotAdditionalTools.Src.Tools;
using File = Telegram.Bot.Types.File;
using Message = Telegram.Bot.Types.Message;

namespace SantaBot.Data.States.Main
{
    public class MainBotStateProcessor : BaseSantaStateProcessor
    {
        public MainBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
            RequiredMessageType = MessageType.Text;
        }

        protected override async Task<Hop> TextMessageProcess(Message mes)
        {
            var text = mes.Text;
            var myInfo = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);

            if (text == MainVars.BtnSearch)
            {
                //Показать профиль (поиск)
                return await ShowProfile(myInfo, mes.Chat.Id);
            }
            
            if (text == MainVars.BtnChosen)
            {
                //Показать избранные профили
                return await ShowFavourites(myInfo);
            }
            
            if (text == MainVars.BtnEditProfile)
            {
                //Перейти в состояние SetName
                ProfileService profileService = new ProfileService();
                await profileService.SendProfile(Bot, Chat.Id, myInfo);
                var hop = new Hop(new HopInfo("ConfirmProfile", ConfirmProfileVars.Introduction, HopType.CurrentLevelHop), 
                    CurrentState, 
                    StateStorage.Get("ConfirmProfile"));

                return hop;
            }
            
            if (text == MainVars.BtnEditSearchParams)
            {
                //Перейти в состояние настроек поиска.
                return new Hop(new HopInfo("SetSearchGender", SetSearchGenderVars.IntroductionShort, HopType.CurrentLevelHop), 
                    CurrentState, 
                    StateStorage.Get("SetSearchGender"));
            }

            return defaultHop;
        }
        
        protected override async Task<Hop> ProcessCallback(CallbackQuery callback)
        {
            var data = callback.Data;
            
            if (data == null)
            {
                await Bot.AnswerCallbackQueryAsync(callback.Id);
                return defaultHop.BlockAnswer();
            }
            
            //Нажал на кнопку Выбрать
            if (data.StartsWith(MainVars.ChoseInlineDataPrefix))
            {
                var userIdStr = data.Replace(MainVars.ChoseInlineDataPrefix, "");
                long chosenUserId = long.Parse(userIdStr);
                return await AddProfileToChosen(callback, chosenUserId);
            }
            
            
            //Нажал на кнопку Удалить
            if (data.StartsWith(MainVars.DeleteInlineDataPrefix))
            {
                var userIdStr = data.Replace(MainVars.DeleteInlineDataPrefix, "");
                long chosenUserId = long.Parse(userIdStr);
                return await DeleteProfileFromChosen(callback, chosenUserId);
            }
            
            // //Нажал на кнопку Удалить с подтверждением
            // if (data.StartsWith(MainVars.DeleteWithConfirmInlineDataPrefix))
            // {
            //     var userIdStr = data.Replace(MainVars.DeleteWithConfirmInlineDataPrefix, "");
            //     long chosenUserId = long.Parse(userIdStr);
            //     return await ShowConfirmationPanel(callback, chosenUserId);
            // }
            //
            // //Подтвердил удаление
            // if (data.StartsWith(MainVars.ConfirmedDeleteInlineDataPrefix))
            // {
            //     var userIdStr = data.Replace(MainVars.ConfirmedDeleteInlineDataPrefix, "");
            //     long chosenUserId = long.Parse(userIdStr);
            //     //Удалить из избранного
            //     ChoiceService choiceService = new ChoiceService();
            //     var res = await choiceService.DeleteProfileFromChosen(CurrentUser.Id, chosenUserId);
            //     //Показать сообщение, что удалено
            //     await Bot.DeleteMessageAsync(Chat.Id, callback.Message.MessageId);
            //     await Bot.SendTextMessageAsync(Chat.Id, MainVars.DeletedFromFavouritesSuccessfully);
            //     return defaultHop.BlockAnswer();
            // }
            //
            // //Отменил удаление
            // if (data.StartsWith(MainVars.CanceledDeleteInlineDataPrefix))
            // {
            //     //Удалить сообщение
            //     await Bot.DeleteMessageAsync(Chat.Id, callback.Message.MessageId);
            //     //Показать сообщение, что отменено
            //     await Bot.SendTextMessageAsync(Chat.Id, MainVars.CancelDelete);
            //     return defaultHop.BlockAnswer();
            // }

            return defaultHop;
        }
        

        private async Task<Hop> ShowProfile(UserInfo myInfo, long chatId, bool isDeleteButton = false)
        {
            var profileService = new ProfileService();
            var profileInfo = await profileService.GetRandomProfileForMe(_dbSanta, myInfo);

            if (profileInfo == null)
            {
                await Bot.SendTextMessageAsync(chatId, MainVars.NotProfiles);
                var hopdef = defaultHop;
                hopdef.BlockSendAnswer = true;
                return hopdef;
            }

            bool isProfileInChosen = await _dbSanta.Repos.UserChoice.IsProfileInMyChoice(myInfo.UserId, profileInfo.UserId);
            await profileService.SendProfile(Bot, chatId, profileInfo, true, isProfileInChosen);
            
            var hop = defaultHop;
            hop.BlockSendAnswer = true;
            return hop;
        }
        

        private async Task<Hop> AddProfileToChosen(CallbackQuery callback, long chosenUserId)
        {
            ChoiceService choiceService = new ChoiceService();
            var res = await choiceService.TryAddProfileToChosen(CurrentUser.Id, chosenUserId);
            
            if (res == ChoiceService.SUCCESS)
            {
                await Bot.EditMessageReplyMarkupAsync(new ChatId(Chat.Id), callback.Message.MessageId,
                    (InlineKeyboardMarkup) MainVars.InlineMarkUpDeleteProfile(chosenUserId).Value);
                //Отправить уведомление пользователю, которого выбрали
                //Обернем в Catch, потому что выскакивает ошибка если бот был заблокирован пользователем
                try
                {
                    await Bot.SendTextMessageAsync(chosenUserId, MainVars.YourProfileWasAddedToFavourites, ParseMode.Html);
                }
                catch
                {
                    
                }
                
            }
            else
            {
                //Посылаем, что нельзя добавить больше 3 человек
                await Bot.SendTextMessageAsync(Chat.Id, res);
            }

            
            await Bot.AnswerCallbackQueryAsync(callback.Id);
            return defaultHop.BlockAnswer();
        }
        
        
        private async Task<Hop> DeleteProfileFromChosen(CallbackQuery callback, long chosenUserId)
        {
            //Проверяем, нельзя удалить после даты раскрытия контактов
            if (DateTime.Now >= AppConstants.ShowDate)
            {
                await Bot.AnswerCallbackQueryAsync(callback.Id);
                return defaultHop.BlockAnswer();
            }
            
            ChoiceService choiceService = new ChoiceService();
            var res = await choiceService.DeleteProfileFromChosen(CurrentUser.Id, chosenUserId);
            
            if (res == ChoiceService.SUCCESS)
            {
                await Bot.EditMessageReplyMarkupAsync(new ChatId(Chat.Id), callback.Message.MessageId,
                    (InlineKeyboardMarkup) MainVars.InlineMarkUpChoseProfile(chosenUserId).Value);
            }

            await Bot.AnswerCallbackQueryAsync(callback.Id);
            return defaultHop.BlockAnswer();
        }

        
        private async Task<Hop> ShowConfirmationPanel(CallbackQuery callback, long chosenUserId)
        {
            await Bot.EditMessageReplyMarkupAsync(new ChatId(Chat.Id), callback.Message.MessageId,
                (InlineKeyboardMarkup) MainVars.GetDeleteConfirmationInline(chosenUserId).Value);
            
            await Bot.AnswerCallbackQueryAsync(callback.Id);
            return defaultHop.BlockAnswer();
        }

        private async Task<Hop> ShowFavourites(UserInfo myInfo)
        {
            List<UserInfo> favouriteProfiles = await _dbSanta.Repos.UserChoice.GetFavouritesProfiles(myInfo.UserId);

            if (favouriteProfiles == null || favouriteProfiles.Count == 0)
            {
                var hop = defaultHop;
                hop.PriorityIntroduction = MainVars.EmptyFavouritesList;
                return hop;
            }

            ProfileService _profileService = new ProfileService();
            await Bot.SendTextMessageAsync(Chat.Id, MainVars.FavouritesList);

            bool showContact = DateTime.Now >= AppConstants.ShowDate;
            
            foreach (var profile in favouriteProfiles)
            {
                await _profileService.SendProfile(Bot, Chat.Id, profile, true, true, showContact);
            }
            
            return defaultHop.BlockAnswer();
        }
    }
}