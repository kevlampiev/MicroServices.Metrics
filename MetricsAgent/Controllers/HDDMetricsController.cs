using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : ControllerBase
    {
        [HttpGet("left/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetHDDMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
