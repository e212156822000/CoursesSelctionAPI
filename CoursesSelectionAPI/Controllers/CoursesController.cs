using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoursesSelectionAPI.Models;
using Microsoft.AspNetCore.Http;

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
        Console.WriteLine(id);
        foreach (var course in _courseRepository.ListCourses())
        {
            if (course.id == id) return Ok(course);
        }
        return NotFound();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Course))]
    public IActionResult GetAllCourses()
    {
        return Ok(_courseRepository.ListCourses());
    }

    /// <summary>
    /// <para name = "name" />
    /// </ summary >
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    public IActionResult CreateCourse([FromBody] JObject data)
    {
        string name = data["name"].ToString();
        string description = data["description"].ToString();
        DateTime start_time = data["start_time"].ToObject<DateTime>();
        DateTime end_time = data["end_time"].ToObject<DateTime>();
        string rating_policy = data["rating_policy"].ToString();
        string credits = data["credits"].ToString();
        string classroomId = data["classroomId"].ToString();
        int credits_int = Tools.ParseStrToPostiveInt(credits);
        int classroomId_int = Tools.ParseStrToPostiveInt(credits);

        if (credits_int < 0 || classroomId_int < 0)
        {
            return BadRequest();
        }

        return CreateCourse(name, description, start_time, end_time, rating_policy, credits_int, classroomId_int);
    }

    //set to public for testing
    private IActionResult CreateCourse(
        string name,
        string description,
        DateTime start_time,
        DateTime end_time,
        string rating_policy,
        int credits,
        int classroomId)
    {
        Guid courseId = Guid.NewGuid();
        _courseRepository.CreateCourse(new Course
        {
            id = courseId,
            name = name,
            description = description,
            start_time = start_time,
            end_time = end_time,
            rating_policy = rating_policy,
            credits = credits,
            classroomId = classroomId
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

    static public int ParseStrToPostiveInt(string input)
    {
        if (int.TryParse(input, out int output))
        {
            return output;
        }

        return -1;
    }
}