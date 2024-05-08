using System;
using Derby.API.Controllers;
using Derby.Domain.Models.Entities;

namespace Derby.API.Services
{
	public class SeedService
	{
        public static void Seed(IServiceProvider serviceProvider)
		{
            IInstrumentService instrumentService = serviceProvider.GetRequiredService<IInstrumentService>();
            ITradeService tradeService = serviceProvider.GetRequiredService<ITradeService>();
            IDerebitService derebitService = serviceProvider.GetRequiredService<IDerebitService>();
            ILogger logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(SeedService).Name);
            SeedInstruments(instrumentService, derebitService, logger);
			SeedTrades(tradeService, instrumentService, derebitService, logger);

		}

        private static async void SeedTrades(ITradeService tradeService, IInstrumentService instrumentService, IDerebitService derebitService, ILogger logger)
        {
            var instrumentList = await instrumentService.GetInstruments();
            if (instrumentList.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    var instrument = instrumentList.ToList() [i];
                    var lastTradeI = await derebitService.GetLastTradeDataFromDerebitAsync(instrument.InstrumentName);
                    if(lastTradeI.TradeId != null)
                    {
                        await tradeService.CreateAsync(lastTradeI);
                    }
                }
            }
            else
            {
                logger.LogWarning("no instruments, so cannot get last trades");
            }
        }

        private static async void SeedInstruments(IInstrumentService instrumentService,
            IDerebitService derebitService, ILogger logger)
        {
            var instrumentList = await instrumentService.GetInstruments();
            if (!instrumentList.Any())
            {
                var instruments = await derebitService.GetInstrumentsFromDerebitAsync();
                var instrumentNames = derebitService.GetInstruments(instruments);
                await instrumentService.CreateManyAsync(instrumentNames.ToList());
                logger.LogInformation("Database seeded successfully");

            }
            else
            {
                logger.LogWarning("DB not seeding, data present");
            }

        }
    }
}

