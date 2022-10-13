using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly INetworkMetricsRepository _metricsRepository;
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(INetworkMetricsRepository metricsRepository, ILogger<NetworkMetricsController> logger)
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
        public ActionResult<IList<NetworkMetric>> GetNetworkMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get Network metrics by period.");
            return Ok(_metricsRepository.GetByTimePeriod(timeFrom, timeTo));
        }

        /// <summary>
        /// Получение исторических данных о загрузке процессора на машине (все данные)
        /// </summary>
        /// <param name="timeFrom">С какого времени</param>
        /// <param name="timeTo">По какое время </param>
        /// <returns>данные в формате json</returns>
        [HttpGet("usage/all")]
        public ActionResult<IList<NetworkMetric>> GetAllNetworkMetrics()
        {
            _logger.LogInformation("Get all Network metrics .");
            return Ok(_metricsRepository.GetAll());
        }

        /// <summary>
        /// Создание новой записи о загрузке процессора
        /// </summary>
        /// <param name="request">запрос, содержащий данные о времени и загрузке в процентах</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation("Create Network metric.");
            try
            {
                _metricsRepository.Create(new NetworkMetric()
                {
                    Value = request.Value,
                    Time = (long)request.Time.TotalSeconds
                });
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with creating Network metric.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="metric">модель метрики</param>
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult Update([FromBody] NetworkMetric metric)
        {
            _logger.LogInformation("Update Network metric.");
            try
            {
                _metricsRepository.Update(metric);
                return Ok("Запись обновлена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with updating Network metric.");
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
            _logger.LogInformation("Delete Network metric.");
            try
            {
                _metricsRepository.Delete(id);
                return Ok("Запись удалена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with deleting Network metric.");
                return BadRequest(ex.Message); //Лучше, чем ничего
            }
        }

    }
}
