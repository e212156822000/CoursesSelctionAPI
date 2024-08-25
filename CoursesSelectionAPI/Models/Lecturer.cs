using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSelectionAPI.Models
{
    public class Lecturer
    {
        [Key]
        public Guid LecturerId { set; get; }

        public string Title { set; get; } = null!;

        public string? Office { set; get; } = null;

        public int AcademicAttributesId { set; get; }

        public string Username { set; get; } = null!;

        public string Password { set; get; } = null!;

        public string EmailAddress { set; get; } = null!;

        public string Firstname { set; get; } = null!;

        public string Lastname { set; get; } = null!;

        public DateTime CreatedAt { set; get; }

        public DateTime LastUpdated { get; set; }
    }
}
