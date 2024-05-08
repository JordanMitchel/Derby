using Derby.Domain.Models.DataModels;
using Derby.Domain.Models.Entities;

namespace Derby.API.Services
{
    public interface IDerebitService
    {
        public Task<Trade> GetLastTradeDataFromDerebitAsync(string instrumentName,long epochTime);

        public Task<Instruments> GetInstrumentsFromDerebitAsync();

        public IEnumerable<Instrument> GetInstruments(Instruments instruments);
    }
}