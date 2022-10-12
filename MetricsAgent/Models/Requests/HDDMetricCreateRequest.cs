namespace MetricsAgent.Models.Requests
{
    public class HDDMetricCreateRequest
    {
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
