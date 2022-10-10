using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : ControllerBase
    {
        /// <summary>
        /// Получить данные о нагрузке процессора по конкретному агенту
        /// </summary>
        /// <param name="agentId">Идентификатор агента</param>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("agent/{agentId}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetCPUMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }

        /// <summary>
        /// Получить данные о нагрузке процессоров всех агентов
        /// </summary>
        /// <param name="timeFrom">Начало времени для отбора статистики</param>
        /// <param name="timeTo">Окончание времени для отбора статистики</param>
        /// <returns>Статистика в формате json</returns>
        [HttpGet("agent/all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetCPUMetricsFromAllAgents([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
