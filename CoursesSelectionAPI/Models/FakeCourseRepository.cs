using System;
using CoursesSelectionAPI.Controllers;

namespace CoursesSelectionAPI.Models
{
    public class FakeCourseRepository : ICourseRepository
    {
        private List<Course> _courses = new List<Course>();

        public Task CreateCourseAsync(Course course)
        {
            _courses.Add(course);
            return Task.CompletedTask;
        }

        public IEnumerable<Course> ListCourses()
        {
            return _courses;
        }

        Task<Course?> ICourseRepository.FindCourseByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

