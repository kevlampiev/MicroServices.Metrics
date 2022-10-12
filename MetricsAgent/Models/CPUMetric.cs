using System.Diagnostics.Eventing.Reader;

namespace MetricsAgent.Models
{
    public class CPUMetric
    {
        public int Id { get; set; }
        public long Time { get; set; }
        public int Value { get; set; }
    }
}
