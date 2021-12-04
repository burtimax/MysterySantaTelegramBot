using System;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using MarathonBot.SantaBot.Data.States;
using SantaBot.DbModel.Entities;
using Telegram.Bot.Types;

namespace SantaBot.Data.States.Start
{
    public class StartBotStateProcessor : BaseSantaStateProcessor
    {
        public StartBotStateProcessor(UpdateBotModel dataForProcessing) : base(dataForProcessing)
        {
        }

        protected override async Task<Hop> ProcessMessage(Message mes)
        {
            //Создадим все сущности для пользователя.
            var ui = await _dbSanta.Repos.UserInfo.GetByUserId(CurrentUser.Id);
            if (ui == null)
            {
                await CreateDefaultUserInfo();
            }
            
            return new Hop(new HopInfo("SetName", answer: SetNameVars.Introduction), CurrentState,
                StateStorage.Get("SetName"));
        }


        private async Task CreateDefaultUserInfo()
        {
            Random r = new Random(DateTime.Now.Millisecond);

            string username = null;
            if (string.IsNullOrEmpty(CurrentUser.Username) == false)
            {
                username = $"@{CurrentUser.Username}";
            }

            UserInfo newUserInfo = new UserInfo()
            {
                Name = CurrentUser.TelegramFirstname ?? CurrentUser.Username ?? "",
                IsMale = false,
                Age = 0,
                Contact = username,
                UserId = CurrentUser.Id,
                RandomNumber = r.Next(10000),
                SearchMaxAge = 101,
            };
            await _dbSanta.Repos.UserInfo.AddAsync(newUserInfo);
        }
        
    }
}