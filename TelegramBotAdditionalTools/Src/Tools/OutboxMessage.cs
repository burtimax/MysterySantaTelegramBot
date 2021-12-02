using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotAdditionalTools.Src.Enums;
using TelegramBotAdditionalTools.Src.MessageData;

namespace TelegramBotAdditionalTools.Src.Tools
{
    public class OutboxMessage
    {
        public OutboxMessageType Type { get; private set; }
        public object Data { get; private set; }
        public List<OutboxMessage> NestedElements { get; private set; }
        public IReplyMarkup ReplyMarkup { get; set; }
        public int ReplyToMessageId { get; set; }

        private ParseMode _parseMode = ParseMode.Html;
        public ParseMode ParseMode
        {
            get { return this._parseMode; }
            set { this._parseMode = value; }
        }

        #region Constructors

    
        public OutboxMessage()
        {
            this.NestedElements = new List<OutboxMessage>();
        }

        private OutboxMessage(object data) : this()
        {
            this.Data = data;
        }

        public OutboxMessage(MessagePhoto photo) : this((object) photo)
        {
            this.Type = OutboxMessageType.Photo;
        }

        public OutboxMessage(MessageMediaGroup mediaGroup) : this((object)mediaGroup)
        {
            this.Type = OutboxMessageType.MediaGroup;
        }

        public OutboxMessage(MessageDocument document) : this((object) document)
        {
            this.Type = OutboxMessageType.Document;
        }

        public OutboxMessage(MessageAudio Audio) : this((object) Audio)
        {
            this.Type = OutboxMessageType.Audio;
        }

        public OutboxMessage(MessageVoice Voice) : this((object)Voice)
        {
            this.Type = OutboxMessageType.Voice;
        }

        public OutboxMessage(string text) : this((object) text)
        {
            this.Type = OutboxMessageType.Text;
        }

        #endregion

        public OutboxMessage this[int index]
        {
            get
            {
                if (NestedElements.Count == 0) return null;

                if (index > NestedElements.Count || index<0)
                {
                    throw new Exception("Выход за границы списка!");
                }

                return this.NestedElements.ElementAt(index);
            }
            set
            {
                if (NestedElements == null || this.NestedElements.Count == 0)
                {
                    throw new Exception("Выход за границы списка!");
                }

                if (index > NestedElements.Count || index < 0)
                {
                    throw new Exception("Выход за границы списка!");
                }

                this.NestedElements[index] = value;
            }
        }



        public void AddMessageElement(OutboxMessage elem)
        {
            if (elem == null && elem.Data == null) return;
            
            if (NestedElements == null)
            {
                NestedElements = new List<OutboxMessage>();
            }

            this.NestedElements.Add(elem);
        }



        public void RemoveMessageElement(OutboxMessage elem)
        {
            if (NestedElements == null && NestedElements?.Count == 0) return;

            if (NestedElements.Contains(elem))
            {
                NestedElements.Remove(elem);
            }
        }


        public void SendOutboxMessage(TelegramBotClient bot, ChatId chatId)
        {
            SendOutboxMessageToChat(bot, chatId);
        }
        public void SendOutboxMessage(TelegramBotClient bot, long id)
        {
            SendOutboxMessageToChat(bot, new ChatId(id));
        }
        public void SendOutboxMessage(TelegramBotClient bot, string username)
        {
            SendOutboxMessageToChat(bot, new ChatId(username));
        }

        private void SendOutboxMessageToChat(TelegramBotClient bot, ChatId chatId)
        {

            if (bot == null ||
                ((chatId == null || chatId?.Identifier == 0) &&
                 string.IsNullOrEmpty(chatId?.Username)))
            {
                throw new ArgumentNullException();
            }

            switch (this.Type)
            {
                //Send Text
                case OutboxMessageType.Text:
                    bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: (string)this.Data,
                    replyMarkup: this.ReplyMarkup,
                    parseMode: this.ParseMode,
                    replyToMessageId: this.ReplyToMessageId);
                    break;

                //Send MessagePhoto Entity
                case OutboxMessageType.Photo:
                    MessagePhoto photo = (MessagePhoto)this.Data;
                    bot.SendPhotoAsync(
                        chatId: chatId,
                        photo: new InputOnlineFile(photo.File.Stream),
                        caption: photo.Caption,
                        replyToMessageId: this.ReplyToMessageId,
                        parseMode: this.ParseMode);
                    break;
                //Send MessageAudio Entity
                case OutboxMessageType.Audio:
                    MessageAudio audio = (MessageAudio)this.Data;
                    bot.SendAudioAsync(
                        chatId: chatId,
                        audio: new InputOnlineFile(audio.File.Stream),
                        caption: audio.Caption,
                        replyMarkup: this.ReplyMarkup,
                        replyToMessageId: this.ReplyToMessageId,
                        thumb: audio.Thumb,
                        title: audio.Title,
                        parseMode: this.ParseMode);
                    break;

                //Send MessageVoice Entity
                case OutboxMessageType.Voice:
                    MessageVoice voice = (MessageVoice)this.Data;
                    bot.SendVoiceAsync(
                        chatId: chatId,
                        voice: new InputOnlineFile(voice.File.Stream),
                        replyMarkup: this.ReplyMarkup,
                        replyToMessageId: this.ReplyToMessageId,
                        caption: voice.Caption,
                        parseMode: this.ParseMode);
                    break;
                //ToDo other Types of message!
                default:
                    throw new Exception("Не поддерживаемый тип отправки сообщений");
                    break;
            }

            //Рекурсивно вызываем отправку вложенных элементов сообщения.
            foreach (var item in this.NestedElements)
            {
                item.SendOutboxMessageToChat(bot, chatId);
            }
        }



    }
}