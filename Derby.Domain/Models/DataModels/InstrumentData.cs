namespace Derby.Domain.Models.DataModels
{
    public abstract class InstrumentData
    {
        public string JsonRpc { get; set; }
        public long usIn { get; set; }
        public long usOut { get; set; }
        public long usDiff { get; set; }
        public string testNet { get; set; }
    }
}