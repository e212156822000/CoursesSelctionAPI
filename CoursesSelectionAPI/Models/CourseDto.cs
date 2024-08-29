namespace CoursesSelectionAPI.Models
{
    public class CourseDto
    {

        public string Name { set; get; } = null!;

        public string? Description { set; get; }

        public int ClassroomId { set; get; }

        public DateTime StartTime { set; get; }

        public DateTime EndTime { set; get; }

        public int Credits { set; get; }

        public string RatingPolicy { set; get; } = "";
    }
}
