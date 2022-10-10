using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController _controller;

        public NetworkMetricsControllerTests()
        {
            _controller = new NetworkMetricsController();
        }

        [Fact]
        public void GetNetworkMetrics_RerurnOk()
        {
            //Данные
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(10);

            //Запрос
            var result = _controller.GetNetworkMetrics(timeFrom, timeTo);

            //Анализ
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
