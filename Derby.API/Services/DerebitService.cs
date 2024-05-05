﻿using System;
using Derby.Domain.Models.DataModels;
using Newtonsoft.Json;
using RestSharp;

namespace Derby.API.Services
{
	public class DerebitService: IDerebitService
	{
		private RestClientOptions _options;

		private RestClient _client;

		public DerebitService()
		{
			_options = new RestClientOptions("https://test.deribit.com") { MaxTimeout = -1 };
			_client = new RestClient(_options);
		}

		public async Task<LastTrade> GetLastTradeDataFromDerebitAsync(string instrumentName)
		{
            var request = new RestRequest($"/api/v2/public/get_last_trades_by_instrument?instrument_name={instrumentName}&count=1", Method.Get);
            RestResponse response = await _client.ExecuteAsync(request);
            var derebitInstrumentData = JsonConvert.DeserializeObject<LastTrade>(response.Content);
			return derebitInstrumentData;
        }

        public async Task<Instruments> GetInstrumentsFromDerebitAsync()
        {
            var request = new RestRequest($"/api/v2/public/get_instruments", Method.Get);
			RestResponse response = await _client.ExecuteAsync(request);
            var derebitInstrumentData = JsonConvert.DeserializeObject<Instruments>(response.Content);
            return derebitInstrumentData;
        }

		public List<string> GetInstrumentNames(Instruments instruments)
		{
            var instrumentNames = new HashSet<string>();
            instruments.Result.Select(x => instrumentNames.Add(x.InstrumentName));

            foreach (var instrumentName in instruments.Result.Select(x => x.InstrumentName))
            {
                instrumentNames.Add(instrumentName);
            }
            return instrumentNames.ToList();
        }
    }
}
