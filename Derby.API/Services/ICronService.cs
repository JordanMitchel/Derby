using Derby.Domain.Models.Entities;
using Hangfire.Server;

namespace Derby.API.Services
{
    public interface ICronService
    {
        Task FireOnceInstrumentJob(IServiceProvider serviceProvider);

        Task GetParallelLastTradesFromDerebitAsync();

        Task<IEnumerable<Instrument>> RecurringDailyInstrumentJob();


    }
}