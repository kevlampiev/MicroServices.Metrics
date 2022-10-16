using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class DotNetMetricsReporitory : IDotNetMetricsRepository
    {

        #region privates
        //private const string ConnectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
        private readonly IOptions<DatabaseOptions> _databaseOption;
        #endregion

        #region Constructors
        public DotNetMetricsReporitory(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOption = databaseOptions;
        }
        #endregion

        public void Create(DotNetMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES (@value, @time)",
                new { 
                    value = entity.Value,
                    time = entity.Time,
                });
        }

        public void Delete(int metricId)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
                new { id = metricId});
        }

        public IList<DotNetMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics ORDER BY time").ToList();
        }

        public DotNetMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.QueryFirst<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE id = @id",
                new { id = id});
        }

        public IList<DotNetMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE time BETWEEN @timeFrom AND @timeTo ORDER BY time",
                new { 
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(DotNetMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id=@id",
                new 
                { 
                    value = entity.Value,
                    time = entity.Time,
                    id = entity.Id
                });
        }
    }
}
