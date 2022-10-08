using MetricsManager.Models;
using MetricsManager.Sources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private AgentPool _agentPool;

        public AgentsController(AgentPool agentPool)
        {
            _agentPool = agentPool;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] Agent agent)
        {
            if (agent != null)
            {
                _agentPool.Add(agent);
                return Ok((_agentPool.Add(agent)) ? "Агент успешно добавлен" : "Агент с таким идентификатором уже есть в базе");
            }
            return BadRequest("Данные об агенте не переданы в теле запроса");
        }

        /// <summary>
        /// Делает агента активным
        /// </summary>
        /// <param name="agentId">Идентификатор агента</param>
        /// <returns>200 или 400 ответ (если агента с таким id не существует)</returns>
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgent([FromRoute] int agentId) 
        {
            if (_agentPool.Agents.ContainsKey(agentId))
            {
                _agentPool.Agents[agentId].Enable = true;
                return Ok($"Агент с идентификатором {agentId} активен");
            }
            else 
            {
                return BadRequest($"Агент с идентификатором {agentId} не существует");
            }
        }
        
        /// <summary>
        /// Отключает агента 
        /// </summary>
        /// <param name="agentId">Идентификатор агента</param>
        /// <returns>200 или 400 ответ (если агента с таким id не существует)</returns>
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgent([FromRoute] int agentId) 
        {
            if (_agentPool.Agents.ContainsKey(agentId))
            {
                _agentPool.Agents[agentId].Enable = false;
                return Ok($"Агент с идентификатором {agentId} больше не активен");
            }
            else 
            {
                return BadRequest($"Агент с идентификатором {agentId} не существует");
            }
        }

        /// <summary>
        /// Вернуть список всех агентов, зарегистрированных в системе
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/all")]
        public IActionResult GetAllAgents()
        { 
            return Ok(_agentPool.GetAll());
        }


        /// <summary>
        /// Вернуть список только активных агентов
        /// </summary>
        /// <returns>список в формате json</returns>
        [HttpGet("get/active")]
        public IActionResult GetActiveAgents()
        {
            return Ok(_agentPool.GetActive());
        }


    }
}

