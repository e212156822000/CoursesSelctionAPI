using System;
using System.ComponentModel.DataAnnotations;

namespace CourseSelectionAPI.Models
{
    public class Lecturer : User
    {
        public string title { set; get; }

        public string office { set; get; }

        public int academic_attributes_id { set; get; }

    }
}
