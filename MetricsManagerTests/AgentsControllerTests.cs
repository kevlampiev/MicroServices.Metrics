using MetricsManager.Controllers;
using MetricsManager.Sources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests
{
    public class AgentsControllerTests
    {
        private AgentsController _agentsController;

        public AgentsControllerTests() 
        {
            //Что-то тут глубоко неправильно, но как правильно, пока непонятно 
            AgentPool agentPool = new AgentPool();
            _agentsController = new AgentsController(agentPool); 
            //Общие данные
            agentPool.Add(new MetricsManager.Models.Agent() { AgentId = 0, AgentName = "1", Enable = true, Uri = new Uri("http://agent1.ru") });
            agentPool.Add(new MetricsManager.Models.Agent() { AgentId = 1, AgentName = "2", Enable = false, Uri = new Uri("http://agent2.ru") });
            agentPool.Add(new MetricsManager.Models.Agent() { AgentId = 2, AgentName = "3", Enable = true, Uri = new Uri("http://agent3.ru") });
        }

        [Fact]
        public void GetAllAgents_ReturnOk() 
        { 
            //Подготовка данных

            //Исполнение 
            var result = _agentsController.GetAllAgents();

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetActiveAgents_ReturnOk()
        {
            //Подготовка данных

            //Исполнение 
            var result = _agentsController.GetActiveAgents();

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(25)]
        [InlineData(15)]
        public void RegisterAgent_ReturnOk(int agentId)
        {
            //Подготовка данных
            MetricsManager.Models.Agent agent = new MetricsManager.Models.Agent() 
            { AgentId = agentId, AgentName = (agentId+1).ToString(), Enable = true, Uri = new Uri("http://agent"+agentId+".ru") };

            //Исполнение 
            var result = _agentsController.RegisterAgent(agent);

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        } 
        
        [Fact]
        public void EnableAgent_ReturnOk()
        {
            //Подготовка данных
            int agentId = 2;

            //Исполнение 
            var result = _agentsController.EnableAgent(agentId);

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }
        
        [Fact]
        public void DisableAgent_ReturnOk()
        {
            //Подготовка данных
            int agentId = 1;

            //Исполнение 
            var result = _agentsController.DisableAgent(agentId);

            //Анализ результата
            Assert.IsAssignableFrom<IActionResult>(result);
        }

    }
}
