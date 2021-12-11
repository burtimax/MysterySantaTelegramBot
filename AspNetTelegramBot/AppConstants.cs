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
        public static string DbConnection { get; set; }
        public static string BotToken { get; set; }
        public static string BotWebhook { get; set; }
        public static string SupportUserId { get; set; }
        public static string ManagerUserId { get; set; }
        public static DateTime ShowDate { get; set; }
        public static string StringDate { get; set; }
        public static int MaxBeChosen { get; set; }
        public static int MaxChoice { get; set; }
    }
}
