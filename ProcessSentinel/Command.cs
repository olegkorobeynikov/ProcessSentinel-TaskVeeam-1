namespace ProcessSentinel
{
    public class Command
    {
        public readonly char ExitChar = 'q';
        public string ProcessName { get; set; }
        public int TimeToLive { get; set; }
        public int TimePolling { get; set; }

        public bool IsSet()
        {
            return ProcessName != null && ProcessName.Length > 0 && TimeToLive > 0 && TimePolling > 0;
        }
    }
}