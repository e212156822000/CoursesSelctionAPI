using CoursesSelectionAPI.DataStore;
using CoursesSelectionAPI.Models;

namespace CoursesSelectionAPI.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services
                .AddDataStoreServices();
        }

        public static IServiceCollection AddDataStoreServices(this IServiceCollection services)
        {
            return services
                .Configure<SqliteDbOptions>(options => options.DbPath = SqliteDbOptions.DefaulDbPath)
                .AddSingleton<IDataStoreConfigurator, SqliteDataStoreConfigurator>()
                .AddDbContext<CourseSelectionDataContext>()
                .AddScoped<ICourseRepository, DbContextCourseRepositorycs>();
        }

        public static IServiceCollection AddListServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICourseRepository, ListCourseRepository>()
                .AddSingleton<ILecturerRepository, ListLecturerRepository>();
        }
    }
}
