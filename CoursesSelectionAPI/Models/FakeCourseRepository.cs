using System;
using CoursesSelectionAPI.Controllers;

namespace CoursesSelectionAPI.Models
{
	public class FakeCourseRepository : ICourseRepository
    {
        private List<Course> _courses = Enumerable.Range(1, 5).Select(index => new Course
        {
            id = Guid.NewGuid(),
            name = "Operating System " + index,
            description = "A fundamental course to introduce Operation System",
            start_time = Tools.CreateDayOfWeek(4, 9, 0),
            end_time = Tools.CreateDayOfWeek(4, 12, 0)
        })
        .ToList();

        public void CreateCourse(Course course)
        {
            _courses.Add(course);
        }

        public IEnumerable<Course> ListCourses()
        {
            return _courses;
        }
    }
}

