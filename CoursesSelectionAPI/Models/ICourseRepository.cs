using System;
namespace CoursesSelectionAPI.Models
{
	public interface ICourseRepository
	{
		public Task CreateCourseAsync(Course course);

		public IEnumerable<Course> ListCourses();

        //public List<Course?> FindCourseByLecturerId(string lecturerId);

        public Task DeleteCourseAsync(Course course);

        public Task UpdateCourse(Guid courseId, Course course);

        public Task<Course?> FindCourseByIdAsync(Guid id);

	}
}

