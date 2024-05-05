using System;
using Derby.Domain.Models.Entities;
using MongoDB.Driver;

namespace Derby.API.Services
{
	public interface ITradeService
	{
        Task<IEnumerable<Trade>> GetAllAsyc();

        Task<Trade> GetById(string id);

        Task<Trade> GetByTradeId(string tradeId);

        Task<Trade> GetTradeByInstrumentName(string instrumentName);

        Task<List<Trade>> GetLatestTradesByInstrumentName(string instrumentName, int count);

        Task CreateAsync(Trade trade);

        Task CreateManyAsync(List<Trade> trades);

        Task Update(Trade trade);



    }
}

