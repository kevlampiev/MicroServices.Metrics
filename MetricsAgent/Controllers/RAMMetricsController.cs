using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RAMMetricsController : ControllerBase
    {
        private readonly IRAMMetricsRepository _metricsRepository;
        private readonly ILogger<RAMMetricsController> _logger;

        public RAMMetricsController(IRAMMetricsRepository metricsRepository, ILogger<RAMMetricsController> logger)
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
        [HttpGet("usage/from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<RAMMetric>> GetRAMMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get RAM metrics by period.");
            return Ok(_metricsRepository.GetByTimePeriod(timeFrom, timeTo));
        }

        /// <summary>
        /// Получение исторических данных о загрузке процессора на машине (все данные)
        /// </summary>
        /// <param name="timeFrom">С какого времени</param>
        /// <param name="timeTo">По какое время </param>
        /// <returns>данные в формате json</returns>
        [HttpGet("usage/all")]
        public ActionResult<IList<RAMMetric>> GetAllRAMMetrics()
        {
            _logger.LogInformation("Get all RAM metrics .");
            return Ok(_metricsRepository.GetAll());
        }

        /// <summary>
        /// Создание новой записи о загрузке процессора
        /// </summary>
        /// <param name="request">запрос, содержащий данные о времени и загрузке в процентах</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] RAMMetricCreateRequest request)
        {
            _logger.LogInformation("Create RAM metric.");
            try
            {
                _metricsRepository.Create(new RAMMetric()
                {
                    Value = request.Value,
                    Time = (long)request.Time.TotalSeconds
                });
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with creating RAM metric.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="metric">модель метрики</param>
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult Update([FromBody] RAMMetric metric)
        {
            _logger.LogInformation("Update RAM metric.");
            try
            {
                _metricsRepository.Update(metric);
                return Ok("Запись обновлена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with updating RAM metric.");
                return BadRequest(ex.Message); //Лучше, чем ничего
            }
        }

        /// <summary>
        /// Удаление метрики
        /// </summary>
        /// <param name="id">Модель метрики</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult Delete([FromRoute] RAMMetric metric)
        {
            _logger.LogInformation("Delete RAM metric.");
            try
            {
                _metricsRepository.Delete(metric);
                return Ok("Запись удалена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with deleting RAM metric.");
                return BadRequest(ex.Message); //Лучше, чем ничего
            }
        }

    }
}
