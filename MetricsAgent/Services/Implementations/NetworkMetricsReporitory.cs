using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class NetworkMetricsReporitory : INetworkMetricsRepository
    {

        private const string ConnectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";

        public void Create(NetworkMetric entity)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES (@value, @time)";
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
            command.CommandText = "DELETE FROM networkmetrics WHERE id=@id";
            command.Parameters.AddWithValue("@id", metricId);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        public IList<NetworkMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM networkmetrics ORDER BY time";
            var results = new List<NetworkMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(new NetworkMetric() 
                    { 
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return results;
        }

        public NetworkMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM networkmetrics WHERE id = @id";
            command.Parameters.AddWithValue("@id", id); //Думаю, это не будет лишним
            command.Prepare();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new NetworkMetric()
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

        public IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM networkmetrics WHERE time BETWEEN @timeFrom AND @timeTo ORDER BY time";
            command.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds); 
            command.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds); 
            command.Prepare();
            var results = new List<NetworkMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(new NetworkMetric()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return results;
        }

        public void Update(NetworkMetric entity)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "UPDATE networkmetrics SET value = @value, time = @time WHERE id=@id";
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@time", entity.Time);
            command.Parameters.AddWithValue("@value", entity.Value);
            command.Prepare();
            command.ExecuteNonQuery();
        }
    }
}
