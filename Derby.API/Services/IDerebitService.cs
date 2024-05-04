namespace Derby.API.Services
{
    public interface IDerebitService
    {
        public Task<DerebitLastTradeInstrument> GetLastTradeDataFromDerebitAsync(string instrumentName);

        public Task<DerebitInstruments> GetInstrumentsFromDerebitAsync();

        public List<string> GetInstrumentNames(DerebitInstruments instruments);
    }
}