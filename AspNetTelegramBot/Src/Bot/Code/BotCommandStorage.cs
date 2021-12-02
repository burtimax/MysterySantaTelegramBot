using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetTelegramBot.Src.Bot.Code;
using Microsoft.EntityFrameworkCore;

namespace MarathonBot.Src.Bot.Code
{
    public abstract class BotCommandStorage
    {
        /// <summary>
        /// Статические команды. Содержат только имя команды. Эти команды неизменны и с их помощью нельзя передавать данные. Эти команды сопоставляются по имени
        /// </summary>
        protected Dictionary<string, BotCommand> StaticCommands;

        /// <summary>
        /// Динамические команды. Они формируются в процессе программы и передают дополнительные данные. Например [/join123321] сопоставление команд StartsWith...
        /// </summary>
        protected List<BotCommand> DynamicCommands;

        public BotCommandStorage()
        {
            StaticCommands = new Dictionary<string, BotCommand>();
            DynamicCommands = new List<BotCommand>();
            InitCommands();
        }

        protected abstract void InitCommands();

        /// <summary>
        /// Добавляем статические команды.
        /// </summary>
        /// <param name="command"></param>
        public void AddStaticCommand(BotCommand command)
        {
            if (command == null)
            {
                throw new Exception("Command can't be NULL!");
            }

            if (string.IsNullOrEmpty(command?.Instruction))
            {
                throw new Exception("Command instruction can't be NULL!");
            }

            if (StaticCommands.ContainsKey(command.Instruction))
            {
                StaticCommands[command.Instruction] = command;
            }
            else
            {
                StaticCommands.Add(command.Instruction, command);
            }
        }

        public void AddDynamicCommand(BotCommand command)
        {
            if (this.DynamicCommands.Contains(command) == false)
            {
                this.DynamicCommands.Add(command);
            }
        }

        public bool HasCommand(string instruction, out BotCommand command)
        {
            command = null;

            instruction = instruction.Trim(' ');

            if (StaticCommands.ContainsKey(instruction))
            {
                command = StaticCommands[instruction];
                return true;
            }

            foreach (var c in DynamicCommands)
            {
                if (instruction.StartsWith(c.Instruction))
                {
                    command = c;
                    return true;
                }
            }

            return false;
        }
    }
}
