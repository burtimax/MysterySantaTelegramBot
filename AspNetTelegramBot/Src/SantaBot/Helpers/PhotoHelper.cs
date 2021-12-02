using System;
using System.IO;
using AspNetTelegramBot.Src.DbModel.DbBot;

namespace MarathonBot.SantaBot.Helpers
{
    public class PhotoHelper
    {
        public static string GetPhotoFilePathForUser(string userId)
        {
            string p = Path.Combine(AppConstants.RootDir, "photo");
            p = Path.Combine(p, $"{userId}.jpg");
            return p;
        }
        
        public static string GetPhotoFilePathByUserInfoPhoto(string photo)
        {
            string p = Path.Combine(AppConstants.RootDir, "photo");
            p = Path.Combine(p, photo);
            return p;
        }
        
        public static string GetPhotoFileNameForUser(string userId)
        {
            return $"{userId}.jpg";
        }
    }
}