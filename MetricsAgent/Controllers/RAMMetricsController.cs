using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram/available")]
    [ApiController]
    public class RAMMetricsController : ControllerBase
    {
        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetRAMMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
