using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSelectionAPI.Models
{
    public class Student
    {
        [Key]
        public Guid StudentId { set; get; }

        public string EnrolledType { set; get; } = null!;

        public string Username { set; get; } = null!;

        public string Password { set; get; } = null!;

        public string EmailAddress { set; get; } = null!;

        public string Firstname { set; get; } = null!;

        public string Lastname { set; get; } = null!;

        public DateTime CreatedAt { set; get; }

        public DateTime LastUpdated { get; set; }
    }
}