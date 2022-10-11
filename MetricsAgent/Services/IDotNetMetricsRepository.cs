using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IDotNetMetricsRepository : IMetricsRepository<DotNetMetric>
    {
        IList<DotNetMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
    
}
