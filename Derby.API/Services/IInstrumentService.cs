using System;
using Derby.Domain.Models.Entities;

namespace Derby.API.Services
{
	public interface IInstrumentService
	{
        Task<Instrument> GetById(string id);

        Task<Instrument> GetByName(string name);

        Task<IEnumerable<Instrument>> GetInstruments();

        Task CreateAsync(Instrument instrument);

        Task CreateManyAsync(List<Instrument> trades);
    }
}

