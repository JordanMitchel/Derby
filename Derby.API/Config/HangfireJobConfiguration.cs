using System;
using Derby.API.Services;
using Hangfire;

namespace Derby.API.Config
{
	public static class HangfireJobConfiguration
	{
        public static void ConfigureJobs(this WebApplication app)
        {
            app.Services.CreateScope();

            app.Services.GetService<ICronService>().FireOnceJob(null);
            app.Services.GetService<ICronService>().RecurringDailyInstrumentJob();
            //app.Services.GetService<ICronService>().FireOnceJob(null);
            //app.Services.GetService<ICronService>().FireOnceJob(null);

        }
    }
}

