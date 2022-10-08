using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet/errors-count")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetDotnetMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
