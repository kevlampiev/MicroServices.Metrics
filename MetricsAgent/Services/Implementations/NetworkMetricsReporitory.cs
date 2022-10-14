using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class NetworkMetricsReporitory : INetworkMetricsRepository
    {

        #region privates
        //private const string ConnectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
        private readonly IOptions<DatabaseOptions> _databaseOption;
        #endregion

        #region Constructors
        public NetworkMetricsReporitory(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOption = databaseOptions;
        }
        #endregion

        public void Create(NetworkMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES (@value, @time)",
                new 
                { 
                    value = entity.Value,
                    time = entity.Time,
                    id = entity.Id
                });
        }

        public void Delete(int metricId)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("DELETE FROM networkmetrics WHERE id=@id", 
                new 
                { id = metricId});
        }

        public IList<NetworkMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics ORDER BY time").ToList();
        }

        public NetworkMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.QueryFirst<NetworkMetric>("SELECT * FROM networkmetrics WHERE id = @id",
                new
                {
                    id = id
                });
        }

        public IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.Query<NetworkMetric>(
                "SELECT * FROM networkmetrics WHERE time BETWEEN @timeFrom AND @timeTo ORDER BY time",
                new 
                { 
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(NetworkMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id=@id",
                new {
                    value = entity.Value,
                    time = entity.Time,
                    id = entity.Id

                });
        }
    }
}
