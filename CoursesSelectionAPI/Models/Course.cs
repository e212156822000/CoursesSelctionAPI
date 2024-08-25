using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoursesSelectionAPI.Models
{
    public class Course
    {
        public Course() { }

        public Course(CourseDto courseDto)
        {
            Name = courseDto.Name;
            Description = courseDto.Description;
            ClassroomId = courseDto.ClassroomId;
            StartTime = courseDto.StartTime;
            EndTime = courseDto.EndTime;
            Credits = courseDto.Credits;
            RatingPolicy = courseDto.RatingPolicy;
        }

        [Key]
        [JsonInclude]
        public Guid CourseId { internal set; get; }

        public string Name { set; get; } = null!;

        public string? Description { set; get; }

        public int ClassroomId { set; get; }

        public DateTime StartTime { set; get; }

        public DateTime EndTime { set; get; }

        public int Credits { set; get; }

        public string RatingPolicy { set; get; } = "";

        public DateTime CreatedAt { set; get; }

        public DateTime LastUpdated { get; set; }
    }
}
