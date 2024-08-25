using System;
using System.ComponentModel.DataAnnotations;

namespace CourseSelectionAPI.Models
{
    public class Student : User
    {
        [Required]
        public string enrolled_type { set; get; }

    }
}
