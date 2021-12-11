using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Enums;
using TelegramBotAdditionalTools.Src.MessageData;
using TelegramBotAdditionalTools.Src.Tools;

namespace BotLibrary.Classes.Helpers
{
    public static class TelegramBotClientExtensionMethods
    {
        public static Task SendOutboxMessageAsync(this TelegramBotClient bot, ChatId chatId, OutboxMessage message)
        {
            return SendOutboxMessageToChat(bot, chatId, message);
        }
        public static Task SendOutboxMessageAsync(this TelegramBotClient bot, long id, OutboxMessage message)
        {
            return SendOutboxMessageToChat(bot, new ChatId(id), message);
        }
        public static Task SendOutboxMessageAsync(this TelegramBotClient bot, string username, OutboxMessage message)
        {
            return SendOutboxMessageToChat(bot, new ChatId(username), message);
        }

        private static async Task SendOutboxMessageToChat(this TelegramBotClient bot, ChatId chatId, OutboxMessage message)
        {
            
            if (bot == null ||
                ((chatId == null || chatId?.Identifier == 0) && 
                 string.IsNullOrEmpty(chatId?.Username)))
            {
                throw new ArgumentNullException();
            }

            switch (message.Type)
            {
                //Send Text
                case OutboxMessageType.Text:
                        await bot.SendTextMessageAsync(
                        chatId: chatId, 
                        text: (string) message.Data, 
                        replyMarkup:message.ReplyMarkup,
                        parseMode: /*message.ParseMode,*/ ParseMode.Html,
                        replyToMessageId: message.ReplyToMessageId);
                    break;

                //Send MessagePhoto Entity
                case OutboxMessageType.Photo:
                    MessagePhoto photo = (MessagePhoto) message.Data;
                    var file = new InputOnlineFile(photo.File.Stream);
                    await bot.SendPhotoAsync(
                        chatId: chatId,
                        photo: file,
                        caption: photo.Caption,
                        replyToMessageId: message.ReplyToMessageId,
                        parseMode: message.ParseMode,
                        replyMarkup: message.ReplyMarkup);
                    break;
                //Send MessageAudio Entity
                case OutboxMessageType.Audio:
                    MessageAudio audio = (MessageAudio)message.Data;
                    await bot.SendAudioAsync(
                        chatId: chatId, 
                        audio: new InputOnlineFile(audio.File.Stream),
                        caption: audio.Caption,
                        replyMarkup: message.ReplyMarkup,
                        replyToMessageId: message.ReplyToMessageId,
                        thumb: audio.Thumb,
                        title: audio.Title,
                        parseMode: message.ParseMode);
                    break;

                //Send MessageVoice Entity
                case OutboxMessageType.Voice:
                    MessageVoice voice = (MessageVoice)message.Data; 
                    await bot.SendVoiceAsync(
                        chatId: chatId, 
                        voice: new InputOnlineFile(voice.File.Stream), 
                        replyMarkup: message.ReplyMarkup,
                        replyToMessageId: message.ReplyToMessageId,
                        caption: voice.Caption,
                        parseMode: message.ParseMode);
                    break;

                case OutboxMessageType.MediaGroup:
                    MessageMediaGroup media = (MessageMediaGroup) message.Data;
                    List<IAlbumInputMedia> mediaList = new List<IAlbumInputMedia>();
                    
                    for (var i = 0; i < media.Files.Count(); i++)
                    {
                        var inputMedia = new InputMediaPhoto(new InputMedia(media.Files[i].Stream,
                            $"file{new Random((int)DateTime.Now.Ticks).Next(0, Int32.MaxValue).ToString()}.jpg"));
                        if (i == 0 && string.IsNullOrEmpty(media.Caption) == false)
                        {
                            inputMedia.Caption = media.Caption;
                        }
                        mediaList.Add(inputMedia);
                    }

                    await bot.SendMediaGroupAsync(chatId, mediaList);
                    break;
                //Send Document Type Message
                case OutboxMessageType.Document:
                    MessageDocument document = (MessageDocument)message.Data;
                    InputOnlineFile iof = new InputOnlineFile(document.File.Stream);
                    if(String.IsNullOrEmpty(document.FileName) == false)
                    {
                        iof.FileName = document.FileName;
                    }

                    await bot.SendDocumentAsync(chatId,
                        document: iof,
                        caption: document.Caption,
                        parseMode: message.ParseMode,
                        replyMarkup: message.ReplyMarkup,
                        replyToMessageId: message.ReplyToMessageId);
                    break;

                //ToDo other Types of message!
                default:
                    throw new Exception("Не поддерживаемый тип отправки сообщений");
                    break;
            }
            
            //Рекурсивно вызываем отправку вложенных элементов сообщения.
            foreach (var item in message.NestedElements)
            {
                await SendOutboxMessageToChat(bot, chatId, item);
            }
        }

    }
}
