using System;
namespace CoursesSelectionAPI.Models
{
	public interface ICourseRepository
	{
		public Task CreateCourseAsync(Course course);

		public IEnumerable<Course> ListCourses();
		public Task<Course?> FindCourseByIdAsync(Guid id);
	}
}

