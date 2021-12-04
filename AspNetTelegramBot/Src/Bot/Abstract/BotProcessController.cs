using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using AspNetTelegramBot.Src.Bot.ExtensionsMethods;
using BotLibrary.Classes.Helpers;
using MarathonBot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotAdditionalTools.Src.Tools;
using DbModels = AspNetTelegramBot.Src.DbModel.DbBot;

namespace AspNetTelegramBot.Src.Bot.Abstract
{
    public abstract class BotProcessController
    {
        public Code.Bot bot;
        private BotControllerAdditionalMethods additionalMethods;

        protected BotProcessController(Code.Bot bot)
        {
            this.bot = bot;
            this.additionalMethods = new BotControllerAdditionalMethods();
        }

        protected BotProcessController(Code.Bot bot, BotControllerAdditionalMethods additionalMethods):this(bot)
        {
            this.additionalMethods = additionalMethods;
        }

        

        public virtual async Task ProcessUpdate(object sender, Update update)
        {
            try
            {
                //Create BotContext concrete object
                DbModels.BotContext dbBotContext = bot.GetNewBotContext();
                this.additionalMethods.Db = dbBotContext;

                //Get objects from Update
                User telegramUser = update.GetUser();
                Chat telegramChat = update.GetChat();

                //
                if (update == null)
                {
                    throw new Exception($"Update is NULL\n{JsonConvert.SerializeObject(update)}");
                }
                
                if (telegramUser == null)
                {
                    throw new Exception($"TelegramUser is NULL\n{JsonConvert.SerializeObject(update)}");
                }
                
                if (telegramChat == null)
                {
                    throw new Exception($"TelegramChat is NULL\n{JsonConvert.SerializeObject(update)}");
                }

                await AddUserIfNeed(telegramUser);
                await AddChatIfNeed(telegramChat);
                await SaveMessageIfNeed(update);
                
                
                //Get Chat model from DataBase to process Update
                DbModels.Chat chatModel = await additionalMethods.GetChat(telegramChat.Id);
                DbModels.User userModel = await additionalMethods.GetUser(telegramUser.Id);
                BotState curState = bot.StateStorage.Get(chatModel.GetCurrentStateName());
                string curStateName = curState.ClassName;
                //---Process Update---
                //Find BotStateProcessor Class
                string processorClassName = $"{bot.Name}Bot.Data.States.{curStateName}.{curStateName}BotStateProcessor";
                Type processorType = Assembly.GetExecutingAssembly().GetType(processorClassName, true, true);

                //Collect Data for Processing
                UpdateBotModel data = new UpdateBotModel()
                {
                    bot = this.bot.client,
                    dbBot = dbBotContext,
                    stateStorage = bot.StateStorage,
                    commandStorage = bot.CommandStorage,
                    state = curState,
                    update = update,
                    chat = chatModel,
                    user = userModel
                };
                
                //Это мой костыль (нужен пока здесь) //ToDo потом убрать
                #region Костыль
                if (userModel != null)
                {
                    userModel.Username = telegramUser?.Username;
                    dbBotContext.User.Update(userModel);
                    await dbBotContext.SaveChangesAsync();
                }
                #endregion

                //Create Instance of Found BotStateProcessorClass
                var processor = Activator.CreateInstance(processorType, data);
                MethodInfo methodProcessor = processorType.GetMethod("ProcessUpdate");

                if (methodProcessor == null) throw new Exception($"Method [ProcessUpdate] don't exists in processor type {processorType.Name}!");

                //Invoke Process method and Get Hop Data
                Hop hop = await (methodProcessor.Invoke(processor, null) as Task<Hop>);

                if (hop == null) throw new Exception("Hop can't be NULL");

                //Get hop, get State, Update CurrentState in Db

                //Change Chat CurrentState by hop type
                chatModel.SetState(hop.PriorityNextStateName, hop.PriorityHopType);

                DbModels.Chat c = new DbModels.Chat()
                {
                    Id = chatModel.Id,
                    State= chatModel.State,
                    StateData = hop.PriorityData != null ? hop.PriorityData : chatModel.StateData,
                };
                await additionalMethods.SetChatData(c);

                if (hop.BlockSendAnswer == false)
                {
                    OutboxMessage outboxMessage = new OutboxMessage(hop.PriorityIntroduction);
                    if (hop.PriorityKeyboard != null)
                    {
                        outboxMessage.ReplyMarkup = hop.PriorityKeyboard;
                    }

                    outboxMessage.ParseMode = ParseMode.Html;
                    await bot.client.SendOutboxMessageAsync(chatModel.Id, outboxMessage);
                    //outboxMessage.SendOutboxMessage(Bot.client, chatModel.Id);
                }
                
            }
            catch (Exception exception)
            {
                await bot.client.SendTextMessageAsync(new ChatId(AppConstants.SupportUserId), $"<b>ERROR</b>\n\n" +
                                                                       $"<b>MESSAGE</b>\n{exception.Message}\n\n" +
                                                                       $"<b>STACK TRACE</b>{exception.StackTrace}\n\n",
                                                                        ParseMode.Html);
            }
            finally
            {
                //if wasn't exception save changes
                if(this.additionalMethods.Db != null)
                {
                    await this.additionalMethods.Db.SaveChangesAsync();
                }
            }
        }

        private async Task AddUserIfNeed(User user)
        {
            if (user == null) throw new Exception("User is NULL");

            if (await additionalMethods.IsUserInBot(user.Id) == false)
            {
                await additionalMethods.AddUserToBot(user);
            }
        }

        private async Task AddChatIfNeed(Chat chat)
        {
            if (chat == null) throw new Exception("Chat is NULL");

            if (await additionalMethods.IsChatInBot(chat.Id) == false)
            {
                await additionalMethods.AddChatToBot(chat);
            }
        }

        private async Task SaveMessageIfNeed(Update update)
        {
            if (update == null) return;

            if (update.Type == UpdateType.Message)
            {
                await additionalMethods.SaveMessage(update.Message);
            }
        }
    }
}
