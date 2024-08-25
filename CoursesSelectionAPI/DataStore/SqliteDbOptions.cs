namespace CoursesSelectionAPI.DataStore
{
    public class SqliteDbOptions
    {
        private const string DefaultDbFileName = "course_selection.db";
        public static readonly string DefaulDbPath;
        public static readonly string TemporaryDbPath = string.Empty;
        public static readonly string InMemoryDbPath = ":memory:";

        private string _dbPath = string.Empty;
        private string _addintionalConnectionInfo = string.Empty;

        static SqliteDbOptions()
        {
            // The following configures EF to create a Sqlite database file in the
            // special "local" folder for your platform.
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var folderPath = Environment.GetFolderPath(folder);
            DefaulDbPath = Path.Join(folderPath, DefaultDbFileName);
        }

        public string DbPath
        {
            get => _dbPath;
            set
            {
                _dbPath = value ?? string.Empty;
            }
        }

        public string AddintionalConnectionInfo
        {
            get => _addintionalConnectionInfo;
            set
            {
                _addintionalConnectionInfo = value ?? string.Empty;
            }
        }

        public string DbConnectionString => _addintionalConnectionInfo != null
                ? $"Data Source={_dbPath}"
                : $"Data Source={_dbPath};{_addintionalConnectionInfo}";
    }
}
