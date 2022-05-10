using System;

namespace ProcessSentinel
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Command command = null;
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Enter command end press Enter.");
            }
            else
            {
                CommandParser.TryParse(args, out command);
            }

            command = command != null && command.IsSet() ? command : CommandReader.Read();
            
            var processSentinel = new ProcessSentinel(command);
            processSentinel.Start();
        }
    }
}