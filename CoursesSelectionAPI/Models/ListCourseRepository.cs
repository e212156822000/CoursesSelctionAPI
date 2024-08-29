using System;
using CoursesSelectionAPI.Controllers;

namespace CoursesSelectionAPI.Models
{
    public class ListCourseRepository : ICourseRepository
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
                if (course.CourseId == courseId) return course;
            }

            return null;
        }
        public List<Course> GetCourseByLecturerId(string lecturerId)
        {
            List<Course> _courseForLecturer = new List<Course>();

            foreach (var course in _courses)
            {
                if (course.LecturerId == lecturerId)
                {
                    _courseForLecturer.Add(course);
                }
            }

            return _courseForLecturer;
        }

        public bool DeleteCourse(Guid courseId)
        {
            foreach(var course in _courses)
            {
                if (course.courseId == courseId)
                {
                    _courses.Remove(course);

                    return true;
                }
            }

            return false;

        }

        public void UpdateCourse(Course originalCourse, Course course)
        {
            
        }
    }
}

