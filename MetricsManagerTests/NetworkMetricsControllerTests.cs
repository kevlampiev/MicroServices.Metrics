using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController _controller;

        public NetworkMetricsControllerTests()
        {
            _controller = new NetworkMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            //Подготовка данных
            int agentId = 0;
            TimeSpan timeFrom = TimeSpan.FromSeconds(1);
            TimeSpan timeTo = TimeSpan.FromSeconds(2);

            //Тестирование 
            var result = _controller.GetNetworkMetricsFromAgent(agentId, timeFrom, timeTo);

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllAgents_ReturnOk()
        {
            //Подготовка данных
            TimeSpan timeFrom = TimeSpan.FromSeconds(1);
            TimeSpan timeTo = TimeSpan.FromSeconds(2);

            //Тестирование 
            var result = _controller.GetNetworkMetricsFromAllAgents(timeFrom, timeTo);

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
