using Derby.Domain.Models.DataModels;
using Derby.Domain.Models.Entities;

namespace Derby.API.Services
{
    public interface IDerebitService
    {
        public Task<LastTrade> GetLastTradeDataFromDerebitAsync(string instrumentName);

        public Task<Instruments> GetInstrumentsFromDerebitAsync();

        public IEnumerable<Instrument> GetInstruments(Instruments instruments);
    }
}