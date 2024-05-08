using System;
using System.Globalization;
using Derby.Domain.Models.Entities;
using Hangfire;
using Hangfire.Server;

namespace Derby.API.Services
{
	public class CronService : ICronService
	{
        private readonly ILogger _logger;
        private readonly IDerebitService _derebitService;
        private readonly IInstrumentService _instrumentService;
        private readonly ITradeService _tradeService;
        private static int _counter = 0;

        public CronService(ILogger<CronService> logger,IDerebitService derebitService, IInstrumentService instrumentService,
            ITradeService tradeService)
		{
            _logger = logger;
            _derebitService = derebitService;
            _instrumentService = instrumentService;
            _tradeService = tradeService;
        }

        [Queue("default")]
        [JobDisplayName("Display current date and time")]
        public async Task FireOnceInstrumentJob(IServiceProvider serviceProvider)
        {
            await SeedService.Seed(serviceProvider);
        }
    

        public async Task<IEnumerable<Instrument>> RecurringDailyInstrumentJob() =>
            await _instrumentService.GetInstruments();
        

        public async Task GetParallelLastTradesFromDerebitAsync()
        {
            //get list of instruments
            var instruments = await _instrumentService.GetInstruments();


            if(_counter >= 200 || _counter * 20> instruments.Count())
            {
                _counter = 0;
                _logger.LogInformation("One full loop of instruments, starting at first instrument");
            }
            var loopInstruments = instruments.Skip(_counter * 20).Take(20);
            _logger.LogInformation($"INSTRUMENT COUNT : {_counter}");
            Parallel.ForEach(loopInstruments, async instrument =>
            {
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.75) * 2.0))

                };

                var windowTime = DateTime.UtcNow.AddSeconds(-300);
                DateTimeOffset dto = new DateTimeOffset(windowTime);
                var timeRequestWindow = dto.ToUnixTimeMilliseconds();

                var tradeData =
                    await _derebitService.GetLastTradeDataFromDerebitAsync(instrument.InstrumentName, timeRequestWindow);
                if (tradeData.TradeId == null)
                {
                    _logger.LogWarning($"Couldn't find trade for instrument: {instrument.InstrumentName}");
                }
                else
                {
                    _logger.LogInformation($"New trade for instrument: {instrument.InstrumentName}");
                    await _tradeService.UpdateUpsert(tradeData);
                }

            });
            _counter +=1;
            _logger.LogInformation($"-------- counter value {_counter}");
        }
    }
}

