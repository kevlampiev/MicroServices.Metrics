using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : ControllerBase
    {
        private readonly ICPUMetricsRepository _metricsRepository;
        private readonly ILogger<CPUMetricsController> _logger;
        private readonly IMapper _mapper;

        public CPUMetricsController(ICPUMetricsRepository metricsRepository, 
            ILogger<CPUMetricsController> logger,
            IMapper mapper)
        {
            this._metricsRepository = metricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение исторических данных о загрузке процессора на машине за период
        /// </summary>
        /// <param name="timeFrom">С какого времени</param>
        /// <param name="timeTo">По какое время </param>
        /// <returns>данные в формате json</returns>
        [HttpGet("usage/from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<CPUMetric>> GetCPUMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get CPU metrics by period.");
            return Ok(_mapper.Map<List<CPUMetricDTO>>(_metricsRepository.GetByTimePeriod(timeFrom, timeTo)));
        }

        /// <summary>
        /// Получение исторических данных о загрузке процессора на машине (все данные)
        /// </summary>
        /// <param name="timeFrom">С какого времени</param>
        /// <param name="timeTo">По какое время </param>
        /// <returns>данные в формате json</returns>
        [HttpGet("usage/all")]
        public ActionResult<IList<CPUMetric>> GetAllCPUMetrics()
        {
            _logger.LogInformation("Get all CPU metrics .");
            return Ok(_mapper.Map<List<CPUMetricDTO>>(_metricsRepository.GetAll()));
        }

        /// <summary>
        /// Получение записи о метрике по id
        /// </summary>
        /// <param name="id">идентификатор метрики</param>
        /// <returns>метрика с заданным id</returns>
        [HttpGet("usage/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            _logger.LogInformation($"Get CPU metric with id = {id} .");
            return Ok(_mapper.Map<CPUMetricDTO>(_metricsRepository.GetById(id)));
        }

        /// <summary>
        /// Создание новой записи о загрузке процессора
        /// </summary>
        /// <param name="request">запрос, содержащий данные о времени и загрузке в процентах</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] CPUMetricCreateRequest request)
        {
            _logger.LogInformation("Create CPU metric.");
            try
            {
                _metricsRepository.Create(_mapper.Map<CPUMetric>(request));

                    /*
                    new CPUMetric()
                {
                    Value = request.Value,
                    Time = (long)request.Time.TotalSeconds
                });
                    */
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with creating CPU metric.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="metric">модель метрики</param>
        /// <returns></returns>
        [HttpPut("update")]
        public IActionResult Update([FromBody] CPUMetric metric)
        {
            _logger.LogInformation("Update CPU metric.");
            try
            {
                _metricsRepository.Update(metric);
                return Ok("Запись обновлена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with updating CPU metric.");
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
            _logger.LogInformation("Delete CPU metric.");
            try
            {
                _metricsRepository.Delete(id);
                return Ok("Запись удалена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error with deleting CPU metric.");
                return BadRequest(ex.Message); //Лучше, чем ничего
            }
        }



    }
}
