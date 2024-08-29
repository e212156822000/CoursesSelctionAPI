using Microsoft.EntityFrameworkCore;

namespace CoursesSelectionAPI.DataStore
{
    public interface IDataStoreConfigurator
    {
        public void OnConfiguring(DbContextOptionsBuilder options);
    }
}
