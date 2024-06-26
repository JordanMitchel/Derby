﻿using System;
using Derby.Domain.Models;
using Derby.Domain.Models.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
            await _tradeCollection.Find(a => a.TradeId == tradeId).SingleOrDefaultAsync();

        public async Task<Trade> GetTradeByInstrumentName(string instrumentName)
        {
            var trades = await GetOrderedTradesByInstrumentName(instrumentName);
            return trades.FirstOrDefault();
        }

        public async Task<List<Trade>> GetLatestTradesByInstrumentName(string instrumentName, int count)
        {
            var trades = await GetOrderedTradesByInstrumentName(instrumentName);
            return trades.Take(count).ToList();
        }

        private async Task<List<Trade>> GetOrderedTradesByInstrumentName(string instrumentName)
        {
            var trades = await _tradeCollection.Find(a => a.InstrumentName == instrumentName).ToListAsync();
            return trades.OrderBy(x => x.TimeStamp).ToList();
        }

        public int returnNum() =>
            5;

        public async Task CreateAsync(Trade trade) =>
            await _tradeCollection.InsertOneAsync(trade);


        public async Task CreateManyAsync(List<Trade> trades) =>
            await _tradeCollection.InsertManyAsync(trades);

        public async Task Update(Trade trade) =>
            await _tradeCollection.FindOneAndReplaceAsync(a=> a.Id == trade.Id,trade);

        public async Task UpdateUpsert(Trade trade)
        {
            var filter = Builders<Trade>.Filter.Eq("TradeId", trade.TradeId);
            var update = Builders<Trade>.Update
                .Set("InstrumentName", trade.InstrumentName)
                .Set("TickDirection", trade.TickDirection)
                .Set("Amount", trade.Amount)
                .Set("MarkPrice", trade.MarkPrice)
                .Set("IndexPrice", trade.IndexPrice)
                .Set("Direction", trade.Direction)
                .Set("Price", trade.Price)
                .Set("TimeStamp", trade.TimeStamp);

            var options = new UpdateOptions { IsUpsert = true };

            await _tradeCollection.UpdateOneAsync(filter, update, options);
        }
    }
}

