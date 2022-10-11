using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface ICPUMetricsRepository : IMetricsRepository<CPUMetric>
    {
        IList<CPUMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
    
}
