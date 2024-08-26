using System;
using CoursesSelectionAPI.Controllers;

namespace CoursesSelectionAPI.Models
{
	public class FakeCourseRepository : ICourseRepository
    {
        private List<Course> _courses = new List<Course>();

        public void CreateCourse(Course course)
        {
            _courses.Add(course);
        }

        public IEnumerable<Course> ListCourses()
        {
            return _courses;
        }

        public Course? GetCourse(Guid courseId)
        {
            foreach (var course in _courses)
            {
                if (course.id == courseId) return course;
            }

            return null;
        }

        public bool DeleteCourse(Guid courseId)
        {
            foreach(var course in _courses)
            {
                if (course.id == courseId)
                {
                    _courses.Remove(course);

                    return true;
                }
            }

            return false;

        }

        public void UpdateCourse(Course originalCourse, Course course)
        {
            if(course.classroomId != null)
            {
                originalCourse.classroomId = course.classroomId;
            }
        }
    }
}

