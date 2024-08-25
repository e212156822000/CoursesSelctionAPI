using System.ComponentModel.DataAnnotations;

namespace CourseSelectionAPI.Models
{
    public class User
    {
        [Key]
        public int id { set; get; }

        [Required]
        public string username { set; get; } = null!;

        [Required]
        public string password { set; get; } = null!;

        [Required]
        public string email { set; get; } = null!;

        [Required]
        public string firstname { set; get; } = null!;

        [Required]
        public string lastname { set; get; } = null!;

        [Required]
        public DateTime created_at { set; get; }

    }
}
