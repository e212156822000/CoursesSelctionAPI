using System;
namespace CoursesSelectionAPI.Models
{
	public interface ICourseRepository
	{
		public Task CreateCourseAsync(Course course);

		public IEnumerable<Course> ListCourses();

        public List<Course> GetCourseByLecturerId(string lecturerId);

        public bool DeleteCourse(Guid courseId);

		public void UpdateCourse(Course originalCourse, Course course);

		public Task<Course?> FindCourseByIdAsync(Guid id);

	}
}

