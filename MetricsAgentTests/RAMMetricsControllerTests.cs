using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class RAMMetricsControllerTests
    {
        private RAMMetricsController _controller;

        public RAMMetricsControllerTests()
        {
            _controller = new RAMMetricsController();
        }

        [Fact]
        public void GetRAMMetrics_RerurnOk()
        {
            //Данные
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(10);

            //Запрос
            var result = _controller.GetRAMMetrics(timeFrom, timeTo);

            //Анализ
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
