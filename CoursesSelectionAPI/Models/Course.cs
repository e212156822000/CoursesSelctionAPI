using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesSelectionAPI.Models
{
    public class Course
    {
        [Required]
        public Guid id { set; get; }

        [Required]
        public string name { set; get; }
        
        public string description { set; get; }

        public int classroomId { set; get; }

        [Required]
        public DateTime start_time { set; get; }

        public DateTime end_time { set; get; }

        public int credits { set; get; }

        public string rating_policy { set; get; }

    }
}
