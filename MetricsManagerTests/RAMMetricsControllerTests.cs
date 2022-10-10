using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests
{
    public class RAMMetricsControllerTests
    {
        private RAMMetricsController _controller;

        public RAMMetricsControllerTests()
        {
            _controller = new RAMMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            //Подготовка данных
            int agentId = 0;
            TimeSpan timeFrom = TimeSpan.FromSeconds(1);
            TimeSpan timeTo = TimeSpan.FromSeconds(2);

            //Тестирование 
            var result = _controller.GetRAMMetricsFromAgent(agentId, timeFrom, timeTo);

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
            var result = _controller.GetRAMMetricsFromAllAgents(timeFrom, timeTo);

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
