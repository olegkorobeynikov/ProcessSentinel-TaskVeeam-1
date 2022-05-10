using System;

namespace ProcessSentinel
{
    public static class CommandParser
    { 
        private const int CountOfArguments = 3;

        public static Command Parse(string command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command), "Command can't be null.");
            
            var args = command.Trim().Split(' ');
            return Parse(args);
        }

        public static Command Parse(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args), "Command args can't be null.");

            if (args.Length != CountOfArguments)
            {
                Console.WriteLine(
                    "Incorrect arguments. Please insert command like <ProcessName> <TimeToLiveMinutes> <PollingTime> and press Enter. Example: notepad 5 1");
                return null;
            }

            if (!int.TryParse(args[1], out var timeToLive) || timeToLive <= 0)
            {
                Console.WriteLine("Incorrect arguments. The second parameter is expected as an int and should be > 0. Enter command again.");
                return null;
            }

            if (!int.TryParse(args[2], out var timePolling) || timePolling <= 0)
            {
                Console.WriteLine("Incorrect arguments. The third parameter is expected as an int and should be > 0. Enter command again.");
                return null;
            }

            return new Command
            {
                ProcessName = args[0],
                TimeToLiveMinutes = timeToLive,
                TimePollingMinutes = timePolling
            };
        }

        public static bool TryParse(string potentialCommand, out Command command)
        {
            if (potentialCommand == null)
                throw new ArgumentNullException(nameof(potentialCommand), "Command can't be null.");

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