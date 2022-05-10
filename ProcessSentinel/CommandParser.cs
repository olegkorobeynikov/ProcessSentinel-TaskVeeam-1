using System;

namespace ProcessSentinel
{
    public static class CommandParser
    { 
        private const int CountOfArguments = 3;

        public static Command Parse(string command)
        {
            var args = command.Split(' ');
            return Parse(args);
        }

        public static Command Parse(string[] args)
        {
            if (args.Length != CountOfArguments)
            {
                Console.WriteLine(
                    "Incorrect arguments. Please insert command like <ProcessName> <TimeToLive> <PollingTime> and press Enter. Example: notepad 5 1");
                return null;
            }

            if (!int.TryParse(args[1], out var timeToLive) && timeToLive > 0)
            {
                Console.WriteLine("Incorrect arguments. The second parameter is expected as an int and should be > 0. Enter command again.");
                return null;
            }

            if (!int.TryParse(args[2], out var timePolling))
            {
                Console.WriteLine("Incorrect arguments. The third parameter is expected as an int and should be > 0. Enter command again.");
                return null;
            }

            return new Command
            {
                ProcessName = args[0],
                TimeToLive = timeToLive,
                TimePolling = timePolling
            };
        }

        public static bool TryParse(string potentialCommand, out Command command)
        {
            var args = potentialCommand.Split(' ');
            TryParse(args, out command);
            return command?.IsSet() ?? false;
        }

        public static bool TryParse(string[] args, out Command command)
        {
            command = Parse(args);
            return command?.IsSet() ?? false;
        }
    }
}