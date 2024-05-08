using System;
using Derby.API.Controllers;
using Derby.Domain.Models.Entities;

namespace Derby.API.Services
{
	public class SeedService
	{
        public IEnumerable<Instrument> _instruments = new List<Instrument>();
        public static async Task Seed(IServiceProvider serviceProvider)
		{
            IInstrumentService instrumentService = serviceProvider.GetRequiredService<IInstrumentService>();
            IDerebitService derebitService = serviceProvider.GetRequiredService<IDerebitService>();
            ILogger logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(SeedService).Name);
            await SeedInstruments(instrumentService, derebitService, logger);
		}

        private static async Task SeedInstruments(IInstrumentService instrumentService,
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

