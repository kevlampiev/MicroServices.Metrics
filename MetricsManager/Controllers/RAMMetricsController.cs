using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RAMMetricsController : ControllerBase
    {
        /// <summary>
        /// Получить данные о доступной памяти по конкретному агенту
        /// </summary>
        /// <param name="agentId">Идентификатор агента</param>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("available/agent/{agentId}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetRAMMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok($"RAM {agentId}");
        }

        /// <summary>
        /// Получить данные о доступной памяти всех агентов
        /// </summary>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("available/agent/all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetRAMMetricsFromAllAgents([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok("RAM.All");
        }

    }
}
