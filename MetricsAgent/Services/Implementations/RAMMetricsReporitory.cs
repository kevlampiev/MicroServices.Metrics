using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class RAMMetricsReporitory : IRAMMetricsRepository
    {

        private const string ConnectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";

        public void Create(RAMMetric entity)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "INSERT INTO rammetrics(value, time) VALUES (@value, @time)";
            command.Parameters.AddWithValue("@value", entity.Value);
            command.Parameters.AddWithValue("@time", entity.Time);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        public void Delete(int metricId)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "DELETE FROM rammetrics WHERE id=@id";
            command.Parameters.AddWithValue("@id", metricId);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        public IList<RAMMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM rammetrics ORDER BY time";
            var results = new List<RAMMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(new RAMMetric() 
                    { 
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return results;
        }

        public RAMMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM rammetrics WHERE id = @id";
            command.Parameters.AddWithValue("@id", id); //Думаю, это не будет лишним
            command.Prepare();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new RAMMetric()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<RAMMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM rammetrics WHERE time BETWEEN @timeFrom AND @timeTo ORDER BY time";
            command.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds); 
            command.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds); 
            command.Prepare();
            var results = new List<RAMMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(new RAMMetric()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return results;
        }

        public void Update(RAMMetric entity)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "UPDATE rammetrics SET value = @value, time = @time WHERE id=@id";
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@time", entity.Time);
            command.Parameters.AddWithValue("@value", entity.Value);
            command.Prepare();
            command.ExecuteNonQuery();
        }
    }
}
