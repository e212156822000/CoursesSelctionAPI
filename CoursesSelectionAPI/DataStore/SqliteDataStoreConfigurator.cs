using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoursesSelectionAPI.DataStore
{
    public class SqliteDataStoreConfigurator : IDataStoreConfigurator
    {
        private readonly string _connectionString;

        public SqliteDataStoreConfigurator(IOptions<SqliteDbOptions> dbOptions, ILogger<Program> logger)
        {
            var dbOptionsValue = dbOptions.Value;
            logger.LogInformation("Init DataContext, path: {DbPath}", dbOptionsValue.DbPath);
            _connectionString = dbOptionsValue.DbConnectionString;
        }

        public void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(_connectionString);
    }
}
