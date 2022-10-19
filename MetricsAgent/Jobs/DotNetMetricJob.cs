using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _repository;
        private PerformanceCounter _performanceCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _performanceCounter = new PerformanceCounter(".NET CLR Exceptions", "# of Exceps Thrown", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            //TODO наполнить

            float exceptionCount = _performanceCounter.NextValue();
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new DotNetMetric() { Time = (long)time.TotalSeconds, Value = (int) exceptionCount });

            return Task.CompletedTask;
        }
    }
}
