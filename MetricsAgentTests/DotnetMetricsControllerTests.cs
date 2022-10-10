using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class DotnetMetricsControllerTests
    {
        private DotnetMetricsController _controller;

        public DotnetMetricsControllerTests()
        {
            _controller = new DotnetMetricsController();
        }

        [Fact]
        public void GetDotnetMetrics_RerurnOk()
        {
            //Данные
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(10);

            //Запрос
            var result = _controller.GetDotnetMetrics(timeFrom, timeTo);

            //Анализ
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
