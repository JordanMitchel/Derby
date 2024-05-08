using System;
using Derby.Domain.Models;
using Derby.Domain.Models.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Derby.API.Services
{
	public class InstrumentService :IInstrumentService
	{
        private readonly IMongoCollection<Instrument> _instrumentCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public InstrumentService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(_dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_dbSettings.Value.DatabaseName);
            _instrumentCollection = mongoDb.GetCollection<Instrument>
                (_dbSettings.Value.InstrumentsCollectionName);
        }

        public async Task CreateAsync(Instrument trade) =>
            await _instrumentCollection.InsertOneAsync(trade);

        public async Task CreateManyAsync(List<Instrument> trades) =>
            await _instrumentCollection.InsertManyAsync(trades);

        public async Task<Instrument> GetById(string id) =>
            await _instrumentCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task<Instrument> GetByName(string name) =>
            await _instrumentCollection.Find(a => a.InstrumentName == name).FirstOrDefaultAsync();

        public async Task<IEnumerable<Instrument>> GetInstruments() =>
            await _instrumentCollection.Find(_=>true).ToListAsync();
    }
}

