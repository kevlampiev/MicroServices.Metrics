using MetricsManager.Models;

namespace MetricsManager.Sources
{
/// <summary>
/// Хранилище информации об агентах
/// </summary>
    public class AgentPool
    {
        private Dictionary<int, Agent> _agents;

        public AgentPool() 
        { 
            _agents = new Dictionary<int, Agent>();
        }

        /// <summary>
        /// Добавить агента
        /// </summary>
        /// <param name="agent"> объект класса agent</param>
        /// <returns> ИСТИНА, если вставка прошла успешно (агента с таким id нет в базе), ЛОЖЬ - в противном случае</returns>
        public bool Add(Agent agent) 
        {
            if (_agents.ContainsKey(agent.AgentId))
            {
                return false;
            }
            else
            {
                _agents.Add(agent.AgentId, agent);
                return true;
            }
        }

        /// <summary>
        /// Получить список всех агентов в виде массива
        /// </summary>
        /// <returns>массив агентов Agents[] </returns>
        public Agent[] GetAll()
        { 
            return _agents.Values.ToArray();
        }

        /// <summary>
        /// Получить массив всех активных агенов
        /// </summary>
        /// <returns>массив агентов с Enable == true </returns>
        public Agent[] GetActive()
        { 
            return _agents.Values.Where( x => x.Enable).ToArray();
        }

        public Dictionary<int, Agent> Agents
        {
            get { return _agents; }
            set { _agents = value; }
        }


    }
}
