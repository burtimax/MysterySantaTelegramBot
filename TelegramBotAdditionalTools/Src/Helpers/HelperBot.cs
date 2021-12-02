using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotAdditionalTools.Src.Enums;
using TelegramBotAdditionalTools.Src.MessageData;

namespace TelegramBotAdditionalTools.Src.Helpers
{
    public class HelperBot
    {
        public static async Task<MessagePhoto> GetPhotoAsync(TelegramBotClient bot, Message mes, PhotoQuality quality = PhotoQuality.High)
        {
            if (bot == null ||
                mes == null ||
                mes.Type != MessageType.Photo) return null;

            int qualityIndex = (int) Math.Round(((int)quality) / ((double)PhotoQuality.High) * mes.Photo.Length-1);
            string fileId = null;
            fileId = mes.Photo[qualityIndex].FileId;
            MessagePhoto photo = new MessagePhoto();
            photo.File = await GetFile(bot, mes, fileId) ;
            return photo;
        }


        public static async Task<MessageAudio> GetAudioAsync(TelegramBotClient bot, Message mes)
        {
            if (bot == null ||
                mes == null ||
                mes.Type != MessageType.Audio) return null;

            MessageAudio audio = new MessageAudio();
            audio.File = await GetFile(bot, mes, mes.Audio.FileId);
            return audio;
        }

        public static async Task<MessageVoice> GetVoiceAsync(TelegramBotClient bot, Message mes)
        {
            if (bot == null || 
                mes == null ||
                mes.Type != MessageType.Voice) return null;

            MessageVoice voice = new MessageVoice();
            var fileId = mes.Voice.FileId;
            voice.File = await GetFile(bot, mes, fileId);
            return voice;
        }

        private static async Task<FileData> GetFile(TelegramBotClient bot, Message mes, string fileId)
        {
            if (bot == null || 
                mes == null ||
                string.IsNullOrEmpty(fileId)) return null ;

            FileData fileData = null;
            MemoryStream ms = new MemoryStream();
            fileData = new FileData();
            fileData.Info = await bot.GetInfoAndDownloadFileAsync(fileId, ms);
            fileData.Data = ms.ToArray();
            return fileData;
        }
    }
}
