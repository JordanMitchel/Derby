using System;
using Derby.Domain.Models;
using Derby.Domain.Models.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Derby.API.Services
{
	public class TradeService :ITradeService
	{
        private readonly IMongoCollection<Trade> _tradeCollection;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public TradeService(IOptions<DatabaseSettings> dbSettings)
		{
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(_dbSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_dbSettings.Value.DatabaseName);
            _tradeCollection = mongoDb.GetCollection<Trade>
                (_dbSettings.Value.TradesCollectionName);
        }

        public async Task<IEnumerable<Trade>> GetAllAsyc() =>
             await _tradeCollection.Find(_ => true).ToListAsync();

        public async Task<Trade> GetById(string id) =>
            await _tradeCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task<Trade> GetByTradeId(string tradeId) =>
            await _tradeCollection.Find(a => a.TradeId == tradeId).FirstOrDefaultAsync();

        public async Task CreateAsync(Trade trade) =>
            await _tradeCollection.InsertOneAsync(trade);


        public async Task CreateManyAsync(List<Trade> trades) =>
            await _tradeCollection.InsertManyAsync(trades);

        public async Task Update(Trade trade) =>
            await _tradeCollection.FindOneAndReplaceAsync(a=> a.Id == trade.Id,trade);

    }
}

