using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class CPUMetricJob : IJob
    {
        private readonly ICPUMetricsRepository _repository;
        private PerformanceCounter _performanceCounter;

        public CPUMetricJob(ICPUMetricsRepository repository)
        {
            _repository = repository;
            _performanceCounter = new PerformanceCounter("Processor", "% Processor Time" , "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            //TODO наполнить

            float cpuUsageInPercents = _performanceCounter.NextValue();
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new CPUMetric() {Time = (long) time.TotalSeconds, Value = (int) cpuUsageInPercents });
            
            return Task.CompletedTask;
        }
    }
}
