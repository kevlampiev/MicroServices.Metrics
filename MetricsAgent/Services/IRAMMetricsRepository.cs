using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IRAMMetricsRepository : IMetricsRepository<RAMMetric>
    {
        IList<RAMMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
    
}
