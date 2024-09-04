using System;
using CourseSelectionAPI.Models;
using CoursesSelectionAPI.Controllers;

namespace CoursesSelectionAPI.Models
{
    public class ListCourseRepository : ICourseRepository
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

        public Task DeleteCourseAsync(Course course)
        {
            _courses.Remove(course);
            return Task.CompletedTask;
        }

        public Task<Course?> FindCourseByIdAsync(Guid courseId)
        {

            foreach (var course in _courses)
            {
                if (course.CourseId == courseId)
                {
                    return Task.FromResult<Course?>(course);
                }
            }

            return Task.FromResult<Course?>(null);
        }

        public Task UpdateCourse(Guid courseId, Course updatedCourse)
        {
            for (int i = 0; i < _courses.Count; i++)
            {
                if (_courses[i].CourseId == courseId)
                {
                    _courses[i] = updatedCourse;
                    return Task.CompletedTask;
                }
            }
            return Task.FromException(new Exception());
        }
    }
}

