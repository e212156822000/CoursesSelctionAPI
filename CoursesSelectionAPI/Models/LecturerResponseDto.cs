using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesSelectionAPI.Models
{
	public class LecturerResponseDto
	{
        [Key]
        public string LecturerId { set; get; } = null!;

        public string Title { set; get; } = null!;

        public string? Office { set; get; } = null;

        public int AcademicAttributesId { set; get; }

        public string EmailAddress { set; get; } = null!;

        public string Firstname { set; get; } = null!;

        public string Lastname { set; get; } = null!;
    }
}

