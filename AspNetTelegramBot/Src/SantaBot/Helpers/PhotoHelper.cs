using System;
using AspNetTelegramBot.Src.DbModel.DbBot;

namespace MarathonBot.SantaBot.Helpers
{
    public class PhotoHelper
    {
        public static string GetPhotoFilePathForUser(string userId)
        {
            return $"{AppConstants.RootDir}\\photo\\{userId}.jpg";
        }
        
        public static string GetPhotoFilePathByUserInfoPhoto(string photo)
        {
            return $"{AppConstants.RootDir}\\photo\\{photo}";
        }
        
        public static string GetPhotoFileNameForUser(string userId)
        {
            return $"{userId}.jpg";
        }
    }
}