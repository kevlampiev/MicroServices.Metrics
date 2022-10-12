namespace MetricsAgent.Models.Requests
{
    public class RAMMetricCreateRequest
    {
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
