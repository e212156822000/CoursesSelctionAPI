﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoursesSelectionAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace CoursesSelectionAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private ICourseRepository _courseRepository;


    public CoursesController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Course))]
    public IActionResult GetCourses(Guid id)
    {
        foreach (var course in _courseRepository.ListCourses())
        {
            if (course.CourseId == id) return Ok(course);
        }
        return NotFound();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Course>))]
    public IActionResult GetAllCourses()
    {
        return Ok(_courseRepository.ListCourses());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    public IActionResult CreateCourse([FromBody] Course course)
    {
        Guid courseId = Guid.NewGuid();

        _courseRepository.CreateCourse(new Course
        {
            CourseId = courseId,
            Name = course.Name,
            Description = course.Description,
            StartTime = course.StartTime,
            EndTime = course.EndTime,
            RatingPolicy = course.RatingPolicy,
            Credits = course.Credits,
            ClassroomId = course.ClassroomId
        });

        return Ok(courseId);

    }

}

public class Tools
{
    static public DateTime CreateDayOfWeek(int DayOfWeek, int hour, int min)
    {
        DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, min, 0);

        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysUntilTuesday = (DayOfWeek - (int)dt.DayOfWeek + 7) % 7;
        //  DateTime nextTuesday = today.AddDays(daysUntilTuesday);

        dt = dt.AddDays(daysUntilTuesday);

        return dt;
    }
}