namespace MetricsAgent.Models.Requests
{
    public class CPUMetricCreateRequest
    {
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
