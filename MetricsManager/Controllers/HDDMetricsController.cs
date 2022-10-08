using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : ControllerBase
    {
        /// <summary>
        /// Получить данные о свободном месте на диске по конкретному агенту
        /// </summary>
        /// <param name="agentId">Идентификатор агента</param>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("left/agent/{agentId}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetHDDLetfMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok(agentId);
        }

        /// <summary>
        /// Получить данные о свободном месте на диске всех агентов
        /// </summary>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("left/agent/all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetHDDLetfMetricsFromAllAgents([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok("All");
        }
    }
}
