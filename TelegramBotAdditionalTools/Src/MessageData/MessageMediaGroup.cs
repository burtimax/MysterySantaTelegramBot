using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Win32.SafeHandles;
using File = Telegram.Bot.Types.File;

namespace TelegramBotAdditionalTools.Src.MessageData
{
    public class MessageMediaGroup
    {
        public List<FileData> Files { get; set; }
        public string Caption { get; set; }

        public MessageMediaGroup()
        {
            this.Files = new List<FileData>();

        }

        public MessageMediaGroup(List<FileData> files)
        {
            this.Files = files;
        }


    }
}
