
using CoursesSelectionAPI.DataStore;

namespace CoursesSelectionAPI.Models
{
    public class DbContextCourseRepositorycs : ICourseRepository
    {
        private readonly CourseSelectionDataContext _db;

        public DbContextCourseRepositorycs(CourseSelectionDataContext courseSelectionDataContext)
        {
            _db = courseSelectionDataContext;
        }

        public async Task CreateCourseAsync(Course course)
        {
            var now = DateTime.UtcNow;
            course.CreatedAt = now;
            course.LastUpdated = now;
            _db.Courses.Add(course);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(Course course)
        {
            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();
        }

        public async Task<Course?> FindCourseByIdAsync(Guid id)
        {
            return await _db.Courses.FindAsync(id);
        }

        public IEnumerable<Course> ListCourses()
        {
            return _db.Courses;
        }
    }
}
