using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotAdditionalTools.Src.Enums
{
    public enum PhotoQuality
    {
        Low = 0,
        Middle = 1,
        High = 2,
    }

    public enum OutboxMessageType
    {
        Text,
        Photo,
        Audio,
        Voice,
        Document,
        MediaGroup,
    }
}
