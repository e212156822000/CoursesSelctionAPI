using System;
using System.ComponentModel.DataAnnotations;

namespace CourseSelectionAPI.Models
{
    public class Lecturer : User
    {
        [Required]
        public string title { set; get; }

        [Required]
        public string office { set; get; }

        [Required]
        public int academic_attributes_id { set; get; }

    }
}
