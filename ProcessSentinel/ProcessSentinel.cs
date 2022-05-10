using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessSentinel
{
    public class ProcessSentinel
    {
        public ProcessSentinel(Command command)
        {
            Command = command;
        }

        private Command Command { get; }

        public void Start()
        {
            var cts = new CancellationTokenSource();
            Task.Run(() => SentinelWorker(cts.Token), cts.Token);
            SentinelController(cts);
        }

        private async Task SentinelWorker(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Console.WriteLine("Check processes...");
                var processArr = Process.GetProcessesByName(Command.ProcessName);
                foreach (var process in processArr)
                {
                    if (DateTime.Now.Subtract(process.StartTime).Minutes < Command.TimeToLiveMinutes) continue;
                    if (KillProcess(process))
                    {
                        Console.WriteLine(
                            $"Successful kill process with name = {process.ProcessName}, id = {process.Id}, TTL = {DateTime.Now.Subtract(process.StartTime).Minutes}.");
                    }
                }
                await Task.Delay(ToMilliseconds(Command.TimePollingMinutes), ct);
            }
        }

        private static bool KillProcess(Process process)
        {
            try
            {
                process.Kill();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error during kill process with name = {process.ProcessName}; id = {process.Id}. Message = {ex.Message}");
            }

            return false;
        }

        private static int ToMilliseconds(int time)
        {
            return time * 60000;
        }

        private void SentinelController(CancellationTokenSource cts)
        {
            while (ShouldQuit())
            {
            }

            cts.Cancel();
        }

        private bool ShouldQuit()
        {
            return Console.ReadKey().KeyChar != Command.ExitChar;
        }
    }
}