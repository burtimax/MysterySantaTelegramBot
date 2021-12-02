using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using AspNetTelegramBot.Src.DbModel.DbBot;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using BotCommand = AspNetTelegramBot.Src.Bot.Code.BotCommand;
using Chat = AspNetTelegramBot.Src.DbModel.DbBot.Chat;
using Message = Telegram.Bot.Types.Message;
using User = AspNetTelegramBot.Src.DbModel.DbBot.User;

namespace AspNetTelegramBot.Src.Bot.Abstract
{
    public class BotUpdateProcessor
    {
        public delegate void StartUpdate();
        public event StartUpdate OnStartProcessUpdate;

        public delegate void EndUpdate(Hop hopAfterProcess);
        public event EndUpdate OnEndProcessUpdate;

        private UpdateBotModel _updateData;
        protected BotContext DbBot { get; private set; }
        protected TelegramBotClient Bot { get; private set; }
        protected Chat Chat { get; private set; }
        protected User CurrentUser { get; private set; }
        protected Update Update { get; private set; }
        protected BotState CurrentState { get; private set; }
        protected BotStateStorage StateStorage { get; private set; }

        protected MessageType? RequiredMessageType;
        
        /// <summary>
        /// Всегда возвращает новый экземпляр перехода по умолчанию.
        /// </summary>
        protected Hop defaultHop
        {
            get
            {
                return new Hop(CurrentState.HopCurrentState, CurrentState, StateStorage.Get(CurrentState.HopCurrentState.NextStateName));
            }
        }
        
        protected Hop successHop { 
            get
            {
                return new Hop(CurrentState.HopOnSuccess, CurrentState, StateStorage.Get(CurrentState.HopOnSuccess.NextStateName));
            }
        }

        protected Hop failHop
        {
            get
            {
                return new Hop(CurrentState.HopOnFailure, CurrentState, StateStorage.Get(CurrentState.HopOnFailure.NextStateName));
            }
        }

        public BotUpdateProcessor(UpdateBotModel dataForProcessing)
        {
            _updateData = dataForProcessing;
            this.DbBot = dataForProcessing.dbBot;
            this.Bot = dataForProcessing.bot;
            this.Chat = dataForProcessing.chat;
            this.CurrentUser = dataForProcessing.user;
            this.Update = dataForProcessing.update;
            this.CurrentState = dataForProcessing.state;
            this.StateStorage = dataForProcessing.stateStorage;
            this.RequiredMessageType = null;
        }

        public virtual async Task<Hop> ProcessUpdate()
        {
            //Можем добавить свой метод, который будет работать перед обработкой обновления
            OnStartProcessUpdate?.Invoke();

            Hop res = defaultHop;

            switch (Update.Type)
            {
                case UpdateType.Message:
                    //Запускаем проверку, если нужно обработать команду бота, она в приоритете.
                    if (IsCommand(out var command))
                    {
                        return await command.ExecCommand(_updateData) ?? defaultHop;
                    }

                    //Затем обрабатываем сообщение
                    res = await ProcessMessage(Update.Message);
                    break;

                case UpdateType.CallbackQuery:
                    res = await ProcessCallback(Update.CallbackQuery);
                    break;

                default:
                    res = await ProcessOtherQuery(Update);
                    break;
            }

            //Можем добавить свой метод, который будет работать после обработки обновления
            OnEndProcessUpdate?.Invoke(res);

            return res;
        }

        protected virtual async Task<Hop> ProcessMessage(Message mes)
        {
            Hop result = null;
            
            if (RequiredMessageType != mes.Type)
            {
                result = await UnexpectedTypeMessageProcess(mes);
            }
            
            switch (mes.Type)
            {
                case MessageType.Text:
                    result = await TextMessageProcess(mes);
                    break;
                case MessageType.Photo:
                    result = await PhotoMessageProcess(mes);
                    break;
            }
            
            if (result == null)
            {
                result = defaultHop;
            }
            return result;
        }

        protected virtual async Task<Hop> TextMessageProcess(Message mes)
        {
            return null;
        }
        
        protected virtual async Task<Hop> PhotoMessageProcess(Message mes)
        {
            return null;
        }
        
        protected virtual async Task<Hop> UnexpectedTypeMessageProcess(Message mes)
        {
            var res = defaultHop;
            res.PriorityIntroduction = CurrentState.UnexpectedUpdateTypeAnswer ?? "Не понимаю";
            return res;
        }
        
        protected virtual async Task<Hop> ProcessCallback(CallbackQuery callback)
        {
            await Bot.AnswerCallbackQueryAsync(callback.Id);
            return defaultHop;
        }

        protected virtual async Task<Hop> ProcessOtherQuery(Update update)
        {
            return defaultHop;
        }

        
        /// <summary>
        /// Команда в виде [/command param01 param02 param...]
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool IsCommand(out BotCommand command)
        {
            if (Update.Message.Type == MessageType.Text)
            {
                string temp = Update.Message.Text.Trim(' ');
                int spacePos = temp.IndexOf(' ');
                string commandStr = spacePos == -1 ? temp : temp.Substring(0, spacePos);
                return _updateData.commandStorage.HasCommand(commandStr, out command);
            }

            command = null;
            return false;
        }

    }
}
