﻿using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class CPUMetricsReporitory : ICPUMetricsRepository
    {

        //private const string ConnectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public CPUMetricsReporitory(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(CPUMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES (@value, @time)", new 
            { 
                value = entity.Value,
                time = entity.Time,
            });
        }

        public void Delete(int metricId)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM cpumetrics WHERE id=@id", new { id = metricId});

        }

        public IList<CPUMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<CPUMetric>("SELECT * FROM cpumetrics ORDER BY time").ToList();
        }

        public CPUMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString );
            return connection.QuerySingle<CPUMetric>("SELECT * FROM cpumetrics WHERE id = @id", new { Id = id});
        }

        public IList<CPUMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<CPUMetric>("SELECT * FROM cpumetrics WHERE time BETWEEN @tFrom AND @tTo ORDER BY time", 
                new { tFrom = timeFrom.TotalSeconds, tTo = timeTo.TotalSeconds}).ToList();
            /*
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM cpumetrics WHERE time BETWEEN @timeFrom AND @timeTo ORDER BY time";
            command.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds); 
            command.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds); 
            command.Prepare();
            var results = new List<CPUMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(new CPUMetric()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return results;
            */
        }

        public void Update(CPUMetric entity)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id",
                new { 
                    id = entity.Id,
                    value = entity.Value,
                    time = entity.Time
                });
        }
    }
}
