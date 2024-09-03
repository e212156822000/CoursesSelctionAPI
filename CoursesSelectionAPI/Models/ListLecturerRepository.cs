using System;
using CourseSelectionAPI.Models;

namespace CoursesSelectionAPI.Models
{
	public class ListLecturerRepository : ILecturerRepository
	{
        private List<Lecturer> _lecturers = new List<Lecturer>();

        public void CreateLecturer(Lecturer lecturer)
        {
            _lecturers.Add(lecturer);
        }

        public Lecturer? GetLecturerById(string lecturerId)
        {
            foreach (var lecturer in _lecturers)
            {
                if (lecturer.LecturerId == lecturerId) return lecturer;
            }

            return null;
        }

        public IEnumerable<Lecturer> ListLecturers()
        {
            return _lecturers;
        }
    }
}

