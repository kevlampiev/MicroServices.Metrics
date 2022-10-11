using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IHDDMetricsRepository : IMetricsRepository<HDDMetric>
    {
        IList<HDDMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
    
}
