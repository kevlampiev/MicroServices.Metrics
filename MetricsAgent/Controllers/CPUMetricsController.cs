﻿using MetricsAgent.Models;
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

        public CPUMetricsController(ICPUMetricsRepository metricsRepository, ILogger<CPUMetricsController> logger)
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
        public ActionResult<IList<CPUMetric>> GetCPUMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get CPU metrics by period.");
            return Ok(_metricsRepository.GetByTimePeriod(timeFrom, timeTo));
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
            return Ok(_metricsRepository.GetAll());
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
                _metricsRepository.Create(new CPUMetric()
                {
                    Value = request.Value,
                    Time = (long)request.Time.TotalSeconds
                });
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
        [HttpDelete("delete")]
        public IActionResult Delete([FromRoute] CPUMetric metric)
        {
            _logger.LogInformation("Delete CPU metric.");
            try
            {
                _metricsRepository.Delete(metric);
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
