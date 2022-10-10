using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class HDDMetricsControllerTests
    {
        private HDDMetricsController _controller;

        public HDDMetricsControllerTests()
        {
            _controller = new HDDMetricsController();
        }

        [Fact]
        public void GetHDDMetrics_RerurnOk()
        {
            //Данные
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(10);

            //Запрос
            var result = _controller.GetHDDMetrics(timeFrom, timeTo);

            //Анализ
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
