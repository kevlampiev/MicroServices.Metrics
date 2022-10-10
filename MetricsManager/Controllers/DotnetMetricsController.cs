using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotnetMetricsController : ControllerBase
    {
        /// <summary>
        /// Получить данные об ошибках .net по конкретному агенту
        /// </summary>
        /// <param name="agentId">Идентификатор агента</param>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("errors-count/agent/{agentId}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetDotNetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok(agentId);
        }

        /// <summary>
        /// Получить данные об ошибках .net всех агентов
        /// </summary>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("errors-count/agent/all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetDontNetMetricsFromAllAgents([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok("All");
        }
    }
}
