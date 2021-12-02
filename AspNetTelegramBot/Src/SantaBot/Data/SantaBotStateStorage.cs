using System.Collections.Generic;
using AspNetTelegramBot.Src.Bot.Code;
using SantaBot.Data.States._TEMPLATE_;
using SantaBot.Data.States.Search;
using SantaBot.Data.States.SetContact;
using SantaBot.Data.States.SetDescription;
using SantaBot.Data.States.SetGender;
using SantaBot.Data.States.SetPhoto;
using SantaBot.Data.States.SetSearchAge;
using SantaBot.Data.States.Start;
using Telegram.Bot.Types.ReplyMarkups;

namespace MarathonBot.SantaBot.Data
{
    public class SantaBotStateStorage : BotStateStorage
    {
        protected override void InitStates()
        {
            this.States = new Dictionary<string, BotState>()
            {
                ["Start"] = new BotState()
                {
                    Name = "Start",
                    DefaultIntroductionString = "Привет",
                    HopOnSuccess = new HopInfo("SetName",
                        StartVars.Introduction),
                },
                ["SetName"] = new BotState()
                {
                    Name = "SetName",
                    DefaultIntroductionString = SetNameVars.Introduction,
                    HopOnSuccess = new HopInfo("SetAge",
                        SetAgeVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetNameVars.Unexpected,
                },
                ["SetAge"] = new BotState()
                {
                    Name = "SetAge",
                    DefaultIntroductionString = SetAgeVars.Introduction,
                    HopOnSuccess = new HopInfo("SetGender",
                        SetGenderVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetAgeVars.Unexpected,
                },
                ["SetGender"] = new BotState()
                {
                    Name = "SetGender",
                    DefaultIntroductionString = SetGenderVars.Introduction,
                    DefaultKeyboard = SetGenderVars.DefaultKeyboardMarkup.Value,
                    HopOnSuccess = new HopInfo("SetPhoto",
                        SetPhotoVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetGenderVars.Unexpected,
                },
                ["SetContact"] = new BotState()
                {
                    Name = "SetContact",
                    DefaultIntroductionString = SetContactVars.Introduction,
                    HopOnSuccess = new HopInfo("SetPhoto",
                        SetPhotoVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetContactVars.Unexpected,
                },
                ["SetPhoto"] = new BotState()
                {
                    Name = "SetPhoto",
                    DefaultIntroductionString = SetPhotoVars.Introduction,
                    HopOnSuccess = new HopInfo("SetDescription",
                        SetDescriptionVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetPhotoVars.Unexpected,
                },
                ["SetDescription"] = new BotState()
                {
                    Name = "SetDescription",
                    DefaultIntroductionString = SetDescriptionVars.Introduction,
                    HopOnSuccess = new HopInfo("ConfirmProfile",
                        ConfirmProfileVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetDescriptionVars.Unexpected,
                },
                ["ConfirmProfile"] = new BotState()
                {
                    Name = "ConfirmProfile",
                    DefaultIntroductionString = SetDescriptionVars.Introduction,
                    DefaultKeyboard = ConfirmProfileVars.DefaultKeyboardMarkup.Value,
                    HopOnSuccess = new HopInfo("SetSearchGender",
                        SetSearchGenderVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetDescriptionVars.Unexpected,
                },
                ["SetSearchGender"] = new BotState()
                {
                    Name = "SetSearchGender",
                    DefaultIntroductionString = SetSearchGenderVars.Introduction,
                    DefaultKeyboard = SetSearchGenderVars.DefaultKeyboardMarkup.Value,
                    HopOnSuccess = new HopInfo("SetSearchAge",
                        SetSearchAgeVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetSearchGenderVars.Unexpected,
                },
                ["SetSearchAge"] = new BotState()
                {
                    Name = "SetSearchAge",
                    DefaultIntroductionString = SetSearchAgeVars.Introduction,
                    DefaultKeyboard = SetSearchAgeVars.DefaultKeyboardMarkup.Value,
                    HopOnSuccess = new HopInfo("Main",
                        MainVars.Introduction),
                    UnexpectedUpdateTypeAnswer = SetSearchAgeVars.Unexpected,
                },
                ["Main"] = new BotState()
                {
                    Name = "Main",
                    DefaultIntroductionString = MainVars.Introduction,
                    DefaultKeyboard = MainVars.DefaultKeyboardMarkup.Value,
                    HopOnSuccess = new HopInfo("Main",
                        MainVars.Introduction),
                    UnexpectedUpdateTypeAnswer = MainVars.Unexpected,
                },

            };
        }
    }
}