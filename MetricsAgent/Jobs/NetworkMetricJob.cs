using MetricsAgent.Models;
using MetricsAgent.Services;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        //private PerformanceCounter _performanceCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            //_performanceCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec",
            //    "http://localhost:5002/swagger/index.html");
        }

        public Task Execute(IJobExecutionContext context)
        {
            
            float network = 0;
            
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //    Сбор по всем сетевым адаптерам
            PerformanceCounterCategory category =
                new PerformanceCounterCategory("Network Interface");
            foreach (string cn in category.GetInstanceNames()) {
                PerformanceCounter networkCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec", cn);
                network += networkCounter.NextValue();
            }
                _repository.Create(new NetworkMetric() { Time = (long)time.TotalSeconds, Value = (int) network });

            return Task.CompletedTask;
        }
    }
}
