using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface INetworkMetricsRepository : IMetricsRepository<NetworkMetric>
    {
        IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
    
}
