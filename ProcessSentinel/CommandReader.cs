using System;

namespace ProcessSentinel
{
    internal static class CommandReader
    {
        public static Command Read()
        {
            Command command = null;
            while (command == null || !command.IsSet())
            {
                var potentialCommand = Console.ReadLine();
                command = CommandParser.Parse(potentialCommand);
            }

            return command;
        }
    }
}