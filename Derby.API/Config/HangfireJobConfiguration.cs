using System;
using Derby.API.Services;
using Hangfire;

namespace Derby.API.Config
{
	public static class HangfireJobConfiguration
	{
        public static void ConfigureJobs(this WebApplication app)
        {
            //app.Services.CreateScope();
            BackgroundJob.Enqueue(() => app.Services.GetService<ICronService>().RecurringDailyInstrumentJob());
            RecurringJob.AddOrUpdate("InsertTrades",() => app.Services.GetService<ICronService>().GetParallelLastTradesFromDerebitAsync(),
            " 0/1 * * * * *");



        }
    }
}

