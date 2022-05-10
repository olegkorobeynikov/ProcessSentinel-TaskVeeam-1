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
                    if (DateTime.Now.Subtract(process.StartTime).Minutes <= Command.TimeToLive) continue;
                    if (KillProcess(process))
                    {
                        Console.WriteLine(
                            $"Successful kill process with name = {process.ProcessName}, id = {process.Id}.");
                    }
                }
                await Task.Delay(Command.TimePolling, ct);
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