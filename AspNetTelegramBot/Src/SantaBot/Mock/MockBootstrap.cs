using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SantaBot.DbModel.Context;
using SantaBot.DbModel.Entities;

namespace MarathonBot.SantaBot.Mock
{
    public class MockBootstrap
    {
        public static async Task BootstrapDatabase()
        {
            var _db = new SantaContext();

            if (_db.UsersInfo.Any(ui => ui.Name == "user_1"))
            {
                return;
            }
            
            var mockUsers = new List<UserInfo>();
            for (var i = 1; i < 1000; i++)
            {
                UserInfo userInfo = new UserInfo()
                {
                    UserId = 1000 + i,
                    Contact = $"@user_{i}",
                    Age = Math.Max(i / 10, 1),
                    Name = $"user_{i}",
                    Photo = "user.jpg",
                    RandomNumber = new Random(i).Next(10000),
                    IsMale = (i % 10) < 5,
                    Description = $"Хочу робота серии RX-{i}",
                };
                mockUsers.Add(userInfo);
            }

            await _db.UsersInfo.AddRangeAsync(mockUsers);
            await _db.SaveChangesAsync();
        }
    }
}