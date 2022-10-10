using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class CPUMetricsControllerTests
    {
        private CPUMetricsController _controller;

        public CPUMetricsControllerTests() 
        { 
            _controller = new CPUMetricsController();
        }

        [Fact]
        public void GetCPUMetrics_RerurnOk()
        { 
            //Данные
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(10);

            //Запрос
            var result = _controller.GetCPUMetrics(timeFrom, timeTo);

            //Анализ
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
