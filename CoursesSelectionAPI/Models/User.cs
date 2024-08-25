using System.ComponentModel.DataAnnotations;

namespace CourseSelectionAPI.Models
{
    public class User
    {
        [Key]
        public int id { set; get; }

        [Required]
        public string username { set; get; }

        [Required]
        public string password { set; get; }

        [Required]
        public string email { set; get; }

        [Required]
        public string firstname { set; get; }

        [Required]
        public string lastname { set; get; }

        [Required]
        public DateTime created_at { set; get; }

    }
}
