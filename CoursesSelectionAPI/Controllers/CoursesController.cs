using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoursesSelectionAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;


namespace CoursesSelectionAPI.Controllers;

[ApiController]
[Route("courses")]
public class CoursesController : ControllerBase
{
    private ICourseRepository _courseRepository;


    public CoursesController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Course))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCourses(Guid id)
    {
        var course = _courseRepository.GetCourse(id);

        return course != null ? Ok(course) : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Course>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
            id = courseId,
            name = course.name,
            description = course.description,
            start_time = course.start_time,
            end_time = course.end_time,
            rating_policy = course.rating_policy,
            credits = course.credits,
            classroomId = course.classroomId
        });

        return Ok(courseId);

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteCourse(Guid id)
    {
        if(_courseRepository.DeleteCourse(id))
        {
            return Ok();
        }

        return NotFound();

    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateCourse(Guid id, [FromBody] JsonPatchDocument<Course?> patchDoc)
    {

        var existingCourse = _courseRepository.GetCourse(id);

        if (existingCourse == null) return NotFound();

        patchDoc.ApplyTo(existingCourse);

        //存入DB 還需要一步

        return Ok(existingCourse);

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