using System;
namespace CoursesSelectionAPI.Models
{
	public interface ICourseRepository
	{
		public void CreateCourse(Course course);

		public IEnumerable<Course> ListCourses();

		public Course? GetCourse(Guid courseId);

		public bool DeleteCourse(Guid courseId);

	}
}

