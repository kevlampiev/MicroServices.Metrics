using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class HDDMetricsReporitory : IHDDMetricsRepository
    {

        #region privates
        //private const string ConnectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
        private readonly IOptions<DatabaseOptions> _databaseOption;
        #endregion

        #region Constructors
        public HDDMetricsReporitory(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOption = databaseOptions;
        }
        #endregion

        public void Create(HDDMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("INSERT INTO hddmetrics(value, time) VALUES (@value, @time)",
                new 
                { 
                    value = entity.Value,
                    time = entity.Time
                });

        }

        public void Delete(int metricId)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("DELETE FROM hddmetrics WHERE id=@id",
                new 
                { 
                    id = metricId
                });
        }

        public IList<HDDMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.Query<HDDMetric>("SELECT * FROM hddmetrics ORDER BY time").ToList();
        }

        public HDDMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.QueryFirst<HDDMetric>("SELECT * FROM hddmetrics WHERE id = @id",
                new 
                { 
                    id = id
                });
        }

        public IList<HDDMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            return connection.Query<HDDMetric>("SELECT * FROM hddmetrics WHERE time BETWEEN @timeFrom AND @timeTo ORDER BY time",
                new 
                { 
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(HDDMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOption.Value.ConnectionString);
            connection.Execute("UPDATE hddmetrics SET value = @value, time = @time WHERE id=@id",
                new 
                { 
                    value = entity.Value,
                    time = entity.Time,
                    id = entity.Id
                });
        }
    }
}
