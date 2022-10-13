using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/DotNet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IDotNetMetricsRepository _metricsRepository;
        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(IDotNetMetricsRepository metricsRepository, ILogger<DotNetMetricsController> logger)
        {
            this._metricsRepository = metricsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Получение исторических данных о загрузке процессора на машине за период
        /// </summary>
        /// <param name="timeFrom">С какого времени</param>
        /// <param name="timeTo">По какое время </param>
        /// <returns>данные в формате json</returns>
        [HttpGet("error-count/from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<DotNetMetric>> GetDotNetMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get DotNet metrics by period.");
            return Ok(_metricsRepository.GetByTimePeriod(timeFrom, timeTo));
        }

        /// <summary>
        /// Получение исторических данных о загрузке процессора на машине (все данные)
        /// </summary>
        /// <param name="timeFrom">С какого времени</param>
        /// <param name="timeTo">По какое время </param>
        /// <returns>данные в формате json</returns>
        [HttpGet("error-count/all")]
        public ActionResult<IList<DotNetMetric>> GetAllDotNetMetrics()
        {
            _logger.LogInformation("Get all DotNet metrics .");
            return Ok(_metricsRepository.GetAll());
        }

        /// <summary>
        /// Создание новой записи о загрузке процессора
        /// </summary>
        /// <param name="request">запрос, содержащий данные о времени и загрузке в процентах</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _logger.LogInformation("Create DotNet metric.");
            try
            {
                _metricsRepository.Create(new DotNetMetric()
                {
                    Value = request.Value,
                    Time = (long)request.Time.TotalSeconds
                });
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with creating DotNet metric.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="metric">модель метрики</param>
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult Update([FromBody] DotNetMetric metric)
        {
            _logger.LogInformation("Update DotNet metric.");
            try
            {
                _metricsRepository.Update(metric);
                return Ok("Запись обновлена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with updating DotNet metric.");
                return BadRequest(ex.Message); //Лучше, чем ничего
            }
        }

        /// <summary>
        /// Удаление метрики
        /// </summary>
        /// <param name="id">Модель метрики</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("Delete DotNet metric.");
            try
            {
                _metricsRepository.Delete(id);
                return Ok("Запись удалена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with deleting DotNet metric.");
                return BadRequest(ex.Message); //Лучше, чем ничего
            }
        }


    }
}
