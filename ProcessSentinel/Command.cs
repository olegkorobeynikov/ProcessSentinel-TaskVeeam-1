namespace ProcessSentinel
{
    public class Command
    {
        public readonly char ExitChar = 'q';
        public string ProcessName { get; set; }
        public int TimeToLiveMinutes { get; set; }
        public int TimePollingMinutes { get; set; }

        public bool IsSet()
        {
            return ProcessName != null && ProcessName.Length > 0 && TimeToLiveMinutes > 0 && TimePollingMinutes > 0;
        }
    }
}