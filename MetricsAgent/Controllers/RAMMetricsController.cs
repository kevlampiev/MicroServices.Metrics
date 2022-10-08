using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RAMMetricsController : ControllerBase
    {
        [HttpGet("available/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetRAMMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
