using System;
using CourseSelectionAPI.Models;

namespace CoursesSelectionAPI.Models
{
	public interface ILecturerRepository
	{
        public void CreateLecturer(Lecturer lecturer);

        public IEnumerable<Lecturer> ListLecturers();

        public Lecturer GetLecturerById(string LecturerId);

    }
}

