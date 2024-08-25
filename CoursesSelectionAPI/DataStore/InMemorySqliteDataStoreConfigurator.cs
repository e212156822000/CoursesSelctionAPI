using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoursesSelectionAPI.DataStore
{
    public class InMemorySqliteDataStoreConfigurator : IDataStoreConfigurator
    {

        private readonly SqliteConnection _connection;

        public InMemorySqliteDataStoreConfigurator(IOptions<SqliteDbOptions> dbOptions, ILogger<Program> logger)
        {
            var dbOptionsValue = dbOptions.Value;

            var sqliteOptions = new SqliteDbOptions()
            {
                DbPath = SqliteDbOptions.InMemoryDbPath,
                AddintionalConnectionInfo = dbOptionsValue.AddintionalConnectionInfo,
            };

            _connection = new SqliteConnection(sqliteOptions.DbConnectionString);
            logger.LogInformation("Init DataContext, In-memory SQLite DB");
        }

        public void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(_connection);
    }
}
