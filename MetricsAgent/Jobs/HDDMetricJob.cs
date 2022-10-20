using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class HDDMetricJob : IJob
    {
        private readonly IHDDMetricsRepository _repository;
        private PerformanceCounter _performanceCounter;

        public HDDMetricJob(IHDDMetricsRepository repository)
        {
            _repository = repository;
            _performanceCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            //TODO наполнить

            float hddUsage = _performanceCounter.NextValue();
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new HDDMetric() { Time = (long)time.TotalSeconds, Value = (int) hddUsage });

            return Task.CompletedTask;
        }
    }
}
