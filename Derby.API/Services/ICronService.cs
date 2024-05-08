using Hangfire.Server;

namespace Derby.API.Services
{
    public interface ICronService
    {
        void RecurringDailyInstrumentJob();

        void RecurringHourlyJob();

        void FireOnceJob(PerformContext context);
    }
}