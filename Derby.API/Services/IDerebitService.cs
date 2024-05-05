using Derby.Domain.Models.DataModels;

namespace Derby.API.Services
{
    public interface IDerebitService
    {
        public Task<LastTrade> GetLastTradeDataFromDerebitAsync(string instrumentName);

        public Task<Instruments> GetInstrumentsFromDerebitAsync();

        public List<string> GetInstrumentNames(Instruments instruments);
    }
}