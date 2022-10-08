using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        /// <summary>
        /// Получить данные о нагрузке на сеть по конкретному агенту
        /// </summary>
        /// <param name="agentId">Идентификатор агента</param>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("agent/{agentId}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetNetworkMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok(agentId);
        }

        /// <summary>
        /// Получить данные о нагрузке на сеть всех агентов
        /// </summary>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("agent/all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetNetworkMetricsFromAllAgents([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok("All");
        }
    }
}
