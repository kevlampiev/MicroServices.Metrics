using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.DTO;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : ControllerBase
    {
        private readonly IHDDMetricsRepository _metricsRepository;
        private readonly ILogger<HDDMetricsController> _logger;
        private readonly IMapper _mapper;

        public HDDMetricsController(IHDDMetricsRepository metricsRepository, 
            ILogger<HDDMetricsController> logger,
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
        [HttpGet("left/from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<HDDMetric>> GetHDDMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get HDD metrics by period.");
            return Ok(_mapper.Map<List<HDDMetricDTO>>(_metricsRepository.GetByTimePeriod(timeFrom, timeTo)));
        }

        /// <summary>
        /// Получение исторических данных о загрузке процессора на машине (все данные)
        /// </summary>
        /// <param name="timeFrom">С какого времени</param>
        /// <param name="timeTo">По какое время </param>
        /// <returns>данные в формате json</returns>
        [HttpGet("left/all")]
        public ActionResult<IList<HDDMetric>> GetAllHDDMetrics()
        {
            _logger.LogInformation("Get all HDD metrics .");
            return Ok(_mapper.Map<List<HDDMetricDTO>>(_metricsRepository.GetAll()));
        }

        ///// <summary>
        ///// Получение записи о метрике по id
        ///// </summary>
        ///// <param name="id">идентификатор метрики</param>
        ///// <returns>метрика с заданным id</returns>
        //[HttpGet("left/{id}")]
        //public IActionResult GetById([FromRoute] int id)
        //{
        //    return Ok(_mapper.Map<HDDMetricDTO>(_metricsRepository.GetById(id)));
        //}

        ///// <summary>
        ///// Создание новой записи о загрузке процессора
        ///// </summary>
        ///// <param name="request">запрос, содержащий данные о времени и загрузке в процентах</param>
        ///// <returns></returns>
        //[HttpPost("create")]
        //public IActionResult Create([FromBody] HDDMetricCreateRequest request)
        //{
        //    _logger.LogInformation("Create HDD metric.");
        //    try
        //    {
        //        _metricsRepository.Create(_mapper.Map<HDDMetric>(request));
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation("Error with creating HDD metric.");
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Редактирование записи
        ///// </summary>
        ///// <param name="metric">модель метрики</param>
        ///// <returns></returns>
        //[HttpPut("update")]
        //public IActionResult Update([FromBody] HDDMetric metric)
        //{
        //    _logger.LogInformation("Update HDD metric.");
        //    try
        //    {
        //        _metricsRepository.Update(metric);
        //        return Ok("Запись обновлена");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation("Error with updating HDD metric.");
        //        return BadRequest(ex.Message); //Лучше, чем ничего
        //    }
        //}

        ///// <summary>
        ///// Удаление метрики
        ///// </summary>
        ///// <param name="id">Модель метрики</param>
        ///// <returns></returns>
        //[HttpDelete("delete/{id}")]
        //public IActionResult Delete([FromRoute] int id)
        //{
        //    _logger.LogInformation("Delete HDD metric.");
        //    try
        //    {
        //        _metricsRepository.Delete(id);
        //        return Ok("Запись удалена");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation("Error with deleting HDD metric.");
        //        return BadRequest(ex.Message); //Лучше, чем ничего
        //    }
        //}

    }
}
