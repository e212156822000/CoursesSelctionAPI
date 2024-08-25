
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

        public void CreateCourse(Course course)
        {
            var now = DateTime.UtcNow;
            course.CreatedAt = now;
            course.LastUpdated = now;
            _db.Courses.Add(course);
            _db.SaveChanges();
        }

        public IEnumerable<Course> ListCourses()
        {
            return _db.Courses;
        }
    }
}
