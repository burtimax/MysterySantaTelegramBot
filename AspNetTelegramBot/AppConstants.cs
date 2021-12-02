using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MarathonBot
{
    public class AppConstants
    {
        public static string RootDir { get; set; }

        private static string _dbConnection;
        public static string DbConnection { get; set; }
        public static string BotToken { get; set; }
        public static string BotWebhook { get; set; }
        public static string SupportUserId { get; set; }
        public static DateTime ShowDate { get; set; }

    }
}
