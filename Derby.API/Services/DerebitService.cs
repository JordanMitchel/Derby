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

		public DerebitService(IMapper mapper)
		{
			_options = new RestClientOptions("https://test.deribit.com") { MaxTimeout = -1 };
			_client = new RestClient(_options);
            _mapper = mapper;
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
            derebitInstrumentData.Result.Take(10);
            return derebitInstrumentData;
        }

        //public List<string> GetInstruments(Instruments instruments)
        //{
        //    var instrumentNames = new HashSet<string>();
        //    instruments.Result.Select(x => instrumentNames.Add(x.InstrumentName));

        //    foreach (var instrumentName in instruments.Result.Select(x => x.InstrumentName))
        //    {
        //        instrumentNames.Add(instrumentName);
        //    }
        //    return instrumentNames.ToList();
        //}
        public IEnumerable<Instrument> GetInstruments(Instruments instruments)
        {
            //var instrumentList = instruments.Result.Select(x => _mapper.Map<Instrument>(x));
            ////var formattedInstruments = _mapper.Map<IEnumerable<Instrument>>(instruments.Result);
            //return instrumentList;
            var from = instruments.Result[0];
            var to = _mapper.Map<Instrument>(from);
            List<Instrument> instrumentdata = new List<Instrument>();
            foreach (var item in instruments.Result)
            {
                instrumentdata.Add(_mapper.Map<Instrument>(item));
            }
            return instrumentdata;
        }
    }
}

