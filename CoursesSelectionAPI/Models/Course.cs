using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoursesSelectionAPI.Models
{
    public class Course
    {
        [Key]
        [Required]
        [JsonInclude]
        public Guid id { internal set; get; }

        [Required]
        public string name { set; get; } = "";

        public string description { set; get; } = "";

        public int classroomId { set; get; }

        [Required]
        public DateTime start_time { set; get; }

        public DateTime end_time { set; get; }

        public int credits { set; get; }

        public string rating_policy { set; get; } = "";

    }
}
