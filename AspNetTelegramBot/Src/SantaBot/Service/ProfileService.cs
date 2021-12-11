using System;
using System.Text;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using AspNetTelegramBot.Src.DbModel.DbBot;
using BotLibrary.Classes.Helpers;
using MarathonBot.Bot.Helpers;
using MarathonBot.SantaBot.Helpers;
using SantaBot.Data.States.Search;
using SantaBot.DbModel.Context;
using SantaBot.DbModel.Entities;
using SantaBot.Interfaces.IServices;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.MessageData;
using TelegramBotAdditionalTools.Src.Tools;

namespace MarathonBot.SantaBot.Service
{
    public class ProfileService : IProfileService
    {
       
        
        public async Task<UserInfo> GetRandomProfileForMe(SantaContext db, UserInfo me)
        {
            var r = new Random(DateTime.Now.Millisecond);
            //Найти профиль

            //Показывать новые
            var profile = await db.Repos.Search.GetProfile(me, false);
            
            //Если нет новых, показать старые
            if (profile == null)
            {
                profile = await db.Repos.Search.GetProfile(me, true);
            }

            if (profile == null)
            {
                return null;
            }
            
            //Отметить в истории поиска
            var shownHistory = await db.Repos.ShowHistory.GetProfileHistory(me.UserId, profile.UserId);
            
            if(shownHistory == null)
            {
                shownHistory = new ShowHistory()
                {
                    UserId = me.UserId,
                    ShownUserId = profile.UserId,
                    ShowCount = 1,
                };
                await db.Repos.ShowHistory.AddAsync(shownHistory);
            }
            else
            {
                //shownHistory.ShowCount += 1;
                shownHistory.CreateTime = DateTime.Now;
                await db.Repos.ShowHistory.UpdateAsync(shownHistory);
            }
            
            //Вернуть профиль
            return profile;
        }

        public async Task SendProfile(TelegramBotClient bot, long chatId, UserInfo profile, bool choseInline = false, bool isDeleteButton = false, bool showContacts = false)
        {
            FileData fd = new FileData();
            var photoPath = PhotoHelper.GetPhotoFilePathByUserInfoPhoto(profile.Photo);
            fd.Data = await System.IO.File.ReadAllBytesAsync(photoPath);
            fd.Info = new File();
            MessagePhoto photo = new MessagePhoto(fd, GetCaptionForProfile(chatId,profile, showContacts));
            OutboxMessage outbox = new OutboxMessage(photo);

            if (choseInline == true)
            {
                if (!isDeleteButton)
                {
                    outbox.ReplyMarkup = MainVars.InlineMarkUpChoseProfile(profile.UserId).Value;
                }
                else
                {
                    outbox.ReplyMarkup = MainVars.InlineMarkUpDeleteProfile(profile.UserId).Value;
                }
            }

            if (DateTime.Now >= AppConstants.ShowDate)
            {
                outbox.ReplyMarkup = null;
            }
            
            outbox.ParseMode = ParseMode.Html;
            await bot.SendOutboxMessageAsync(chatId, outbox);

            await fd.Stream.DisposeAsync();
            fd = null;
            await photo.File.Stream.DisposeAsync();
            photo = null;
            outbox = null;
        }

        // public async Task SendFavouriteProfile(TelegramBotClient bot, long chatId, UserInfo profile, UserInfo nextFavProfile = null)
        // {
        //     FileData fd = new FileData();
        //     var photoPath = PhotoHelper.GetPhotoFilePathByUserInfoPhoto(profile.Photo);
        //     fd.Data = await System.IO.File.ReadAllBytesAsync(photoPath);
        //     fd.Info = new File();
        //     MessagePhoto photo = new MessagePhoto(fd, GetCaptionForProfile(profile));
        //     OutboxMessage outbox = new OutboxMessage(photo);
        //
        //     IReplyMarkup keyboard =
        //         MainVars.GetInlineForFavourite(profile.UserId, nextFavProfile?.UserId ?? null).Value;
        //     outbox.ReplyMarkup = keyboard;
        //     
        //     outbox.ParseMode = ParseMode.Html;
        //     await bot.SendOutboxMessageAsync(chatId, outbox);
        //
        //     await fd.Stream.DisposeAsync();
        //     fd = null;
        //     await photo.File.Stream.DisposeAsync();
        //     photo = null;
        //     outbox = null;
        // }

        private string GetCaptionForProfile(long chatId, UserInfo profile, bool showContacts)
        {
            //string sex = profile.IsMale == true ? MainVars : "Ж";
            StringBuilder sb = new StringBuilder();
            if (showContacts || chatId == AppConstants.SupportUserIdLong)
            {
                sb.AppendLine($"\nКонтакт : {profile.Contact}");
            }

            sb.AppendLine("\n");
            sb.AppendLine($"🎄 {profile.Name}, {profile.Age}, - {profile.Description}");
            
            return BotHelper.CropMediaCaptionAccurate(sb.ToString());
        }
    }
}