using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Services.Implementations
{
    public class HDDMetricsReporitory : IHDDMetricsRepository
    {

        private const string ConnectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";

        public void Create(HDDMetric entity)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES (@value, @time)";
            command.Parameters.AddWithValue("@value", entity.Value);
            command.Parameters.AddWithValue("@time", entity.Time);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        public void Delete(HDDMetric entity)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "DELETE FROM hddmetrics WHERE id=@id";
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        public IList<HDDMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM hddmetrics ORDER BY time";
            var results = new List<HDDMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(new HDDMetric() 
                    { 
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return results;
        }

        public HDDMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM hddmetrics WHERE id = @id";
            command.Parameters.AddWithValue("@id", id); //Думаю, это не будет лишним
            command.Prepare();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new HDDMetric()
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

        public IList<HDDMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM hddmetrics WHERE time BETWEEN @timeFrom AND @timeTo ORDER BY time";
            command.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds); 
            command.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds); 
            command.Prepare();
            var results = new List<HDDMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(new HDDMetric()
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return results;
        }

        public void Update(HDDMetric entity)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "UPDATE hddmetrics SET value = @value, time = @time WHERE id=@id";
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@time", entity.Time);
            command.Parameters.AddWithValue("@value", entity.Value);
            command.Prepare();
            command.ExecuteNonQuery();
        }
    }
}
