using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests
{
    public class HDDMetricsControllerTests
    {
        private HDDMetricsController _controller;

        public HDDMetricsControllerTests()
        {
            _controller = new HDDMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            //Подготовка данных
            int agentId = 0;
            TimeSpan timeFrom = TimeSpan.FromSeconds(1);
            TimeSpan timeTo = TimeSpan.FromSeconds(2);

            //Тестирование 
            var result = _controller.GetHDDLetfMetricsFromAgent(agentId, timeFrom, timeTo);

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
            var result = _controller.GetHDDLetfMetricsFromAllAgents(timeFrom, timeTo);

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
