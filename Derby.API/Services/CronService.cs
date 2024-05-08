using System;
using System.Globalization;
using Hangfire;
using Hangfire.Server;

namespace Derby.API.Services
{
	public class CronService : ICronService
	{
		public CronService()
		{
		}

        [Queue("default")]
        [JobDisplayName("Display current date and time")]
        public void FireOnceJob(PerformContext context)
        {
            BackgroundJob.Enqueue("test_queue",() => Console.WriteLine($"The current date and time is: {DateTime.Now.ToString(CultureInfo.CurrentCulture)}"));
            BackgroundJob.Enqueue<TradeService>(x => x.returnNum());
            BackgroundJob.Enqueue<InstrumentService>("trade_queue", x => x.GetInstruments());
        }
    

        public void RecurringDailyInstrumentJob()
        {
            RecurringJob.AddOrUpdate<InstrumentService>(
                "firstRecurringJob",
                x => x.GetInstruments(),
                Cron.Daily(10));


        }

        public void RecurringHourlyJob()
        {
            RecurringJob.AddOrUpdate<TradeService>(
                "GoodMorning",
                x => x.returnNum(),
                Cron.Minutely());
        }
    }
}

