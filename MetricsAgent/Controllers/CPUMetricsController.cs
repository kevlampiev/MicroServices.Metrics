using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : ControllerBase
    {
        [HttpGet("usage/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetCPUMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
