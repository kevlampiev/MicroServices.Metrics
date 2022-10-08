using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotnetMetricsController : ControllerBase
    {
        [HttpGet("errors-count/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetDotnetMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
