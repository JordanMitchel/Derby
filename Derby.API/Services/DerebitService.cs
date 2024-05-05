using System;
using System.Collections.Generic;
using AutoMapper;
using Derby.Domain.Models.DataModels;
using Derby.Domain.Models.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace Derby.API.Services
{
	public class DerebitService: IDerebitService
	{
		private RestClientOptions _options;

		private RestClient _client;

        private IMapper _mapper;

        private ILogger _logger;

		public DerebitService(IMapper mapper, ILogger<DerebitService> logger)
		{
			_options = new RestClientOptions("https://test.deribit.com") { MaxTimeout = -1 };
			_client = new RestClient(_options);
            _mapper = mapper;
            _logger = logger;
		}

		public async Task<Trade> GetLastTradeDataFromDerebitAsync(string instrumentName)
		{
            var request = new RestRequest($"/api/v2/public/get_last_trades_by_instrument?instrument_name={instrumentName}&count=1", Method.Get);
            RestResponse response = await _client.ExecuteAsync(request);
            var derebitInstrumentData = JsonConvert.DeserializeObject<LastTrade>(response.Content);
            var tradeData = derebitInstrumentData.result.trades;
            if (!tradeData.Any())
            {
                _logger.LogWarning($"Trade data not found for the instrument: {instrumentName}");
                return new Trade();
            }
            List<TradeRequest> tradeRequestData = derebitInstrumentData.result.trades;

            var tradeRequest = _mapper.Map<Trade>(tradeRequestData.FirstOrDefault());

            return tradeRequest;
        }

        public async Task<Instruments> GetInstrumentsFromDerebitAsync()
        {
            var request = new RestRequest($"/api/v2/public/get_instruments", Method.Get);
			RestResponse response = await _client.ExecuteAsync(request);
            var derebitInstrumentData = JsonConvert.DeserializeObject<Instruments>(response.Content);
            derebitInstrumentData.Result.Take(10);
            return derebitInstrumentData;
        }


        public IEnumerable<Instrument> GetInstruments(Instruments instruments)
        {
            List<Instrument> instrumentdata = new List<Instrument>();
            foreach (var item in instruments.Result)
            {
                instrumentdata.Add(_mapper.Map<Instrument>(item));
            }
            return instrumentdata;
        }
    }
}

