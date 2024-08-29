using Microsoft.EntityFrameworkCore;

namespace CoursesSelectionAPI.DataStore
{
    public class BaseDataStoreContext : DbContext
    {
        private readonly IDataStoreConfigurator _configurator;

        public BaseDataStoreContext(IDataStoreConfigurator configurator)
        {
            _configurator = configurator;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => _configurator.OnConfiguring(options);
    }
}
