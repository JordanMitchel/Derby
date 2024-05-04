namespace Derby.API
{
    public class DerebitInstrumentData
    {
        public string JsonRpc { get; set; }
        public long usIn { get; set; }
        public long usOut { get; set; }
        public long usDiff { get; set; }
        public string testNet { get; set; }
    }
}