namespace MetricsManager.Models
{
    /// <summary>
    /// Модель информации об агенте
    /// </summary>
    public class Agent
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public Uri Uri { get; set; }
        public bool Enable { get; set;   }
    }
}
