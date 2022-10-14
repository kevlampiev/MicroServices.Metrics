using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class RAMMetricsReporitory : IRAMMetricsRepository
    {

        #region privates
        //private const string ConnectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
        private readonly IOptions<DatabaseOptions> _databaseOption;
        #endregion

        #region Constructors
        public RAMMetricsReporitory(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOption = databaseOptions;
        }
        #endregion

        public void Create(RAMMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("INSERT INTO rammetrics(value, time) VALUES (@value, @time)",
                new { 
                    value = entity.Value,
                    time = entity.Time,
                });
        }

        public void Delete(int metricId)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("DELETE FROM rammetrics WHERE id=@id", new { id = metricId});
        }

        public IList<RAMMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.Query<RAMMetric>("SELECT * FROM rammetrics ORDER BY time").ToList();
        }


        //Я ТУТ
        public RAMMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.QueryFirst<RAMMetric>("SELECT * FROM rammetrics WHERE id = @id",
                new
                { 
                    id = id
                });
        }

        public IList<RAMMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.Query<RAMMetric>("SELECT * FROM rammetrics WHERE time BETWEEN @timeFrom AND @timeTo ORDER BY time",
                new 
                { 
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
            
        }

        public void Update(RAMMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id=@id",
                new 
                { 
                    value = entity.Value,
                    time = entity.Time,
                    id = entity.Id
                });
        }
    }
}
