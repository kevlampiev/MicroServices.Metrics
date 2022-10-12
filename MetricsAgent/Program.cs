using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.Implementations;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Congigure logging

            builder.Host.ConfigureLogging(
                logging => { 
                    logging.ClearProviders();
                    logging.AddConsole();
                }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true});
            builder.Services.AddHttpLogging(
                logging => { 
                    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestQuery;
                    logging.RequestBodyLogLimit = 4096;
                    logging.ResponseBodyLogLimit = 4096;
                    logging.RequestHeaders.Add("Authorization");
                    logging.RequestHeaders.Add("X-Real-IP");
                    logging.RequestHeaders.Add("X-Forwarded-For");
                });

            #endregion
            
            // Add services to the container.

            #region MetricsRepositories
            builder.Services.AddScoped<ICPUMetricsRepository, CPUMetricsReporitory>();
            builder.Services.AddScoped<IDotNetMetricsRepository, DotNetMetricsReporitory>();
            builder.Services.AddScoped<IHDDMetricsRepository, HDDMetricsReporitory>();
            builder.Services.AddScoped<INetworkMetricsRepository, NetworkMetricsReporitory>();
            builder.Services.AddScoped<IRAMMetricsRepository, RAMMetricsReporitory>();
            #endregion
            
            ConfigureSqlLiteConnection();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }

        public static void ConfigureSqlLiteConnection()
        {
           // if (File.Exists("metrics.db")) 
           // {
           //     return;
           // }
            const string connectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private static void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                //command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                //command.ExecuteNonQuery();

                command.CommandText = GetCreateTableCommand("cpumetrics");
                command.ExecuteNonQuery(); 
                command.CommandText = GetCreateTableCommand("dotnetmetrics");
                command.ExecuteNonQuery(); 
                command.CommandText = GetCreateTableCommand("hddmetrics");
                command.ExecuteNonQuery(); 
                command.CommandText = GetCreateTableCommand("networkmetrics");
                command.ExecuteNonQuery(); 
                command.CommandText = GetCreateTableCommand("rammetrics");
                command.ExecuteNonQuery(); 

/*                
                command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS dotnetmetrics(id INTEGER
                      PRIMARY KEY,
                        value INT, time INT)";
                command.ExecuteNonQuery();
*/
            }
        
        }

        private static string GetCreateTableCommand(string tabName) 
        {
            return $"CREATE TABLE IF NOT EXISTS {tabName} (id INTEGER PRIMARY KEY, value INT, time INT)";
        }
            
    }
}