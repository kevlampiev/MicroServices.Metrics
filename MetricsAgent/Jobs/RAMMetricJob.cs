using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class RAMMetricJob : IJob
    {
        private readonly IRAMMetricsRepository _repository;
        private PerformanceCounter _performanceCounter;

        public RAMMetricJob(IRAMMetricsRepository repository)
        {
            _repository = repository;
            _performanceCounter = new PerformanceCounter("Memory", "Available MBytes"); ;
        }

        public Task Execute(IJobExecutionContext context)
        {
            //TODO наполнить

            float ramAvailable = _performanceCounter.NextValue();
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new RAMMetric() { Time = (long)time.TotalSeconds, Value = (int) ramAvailable }); ;

            return Task.CompletedTask;
        }
    }
}
