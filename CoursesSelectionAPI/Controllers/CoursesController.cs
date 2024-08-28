using Microsoft.AspNetCore.Mvc;
using CoursesSelectionAPI.Models;
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

    [HttpGet("{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCourse(Guid courseId)
    {
        var course = _courseRepository.GetCourse(courseId);

        return course != null ? Ok(course) : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CourseDto>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllCourses(
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        [FromQuery] string? dept,
        [FromQuery] string? college)
    {
        return Ok(_courseRepository.ListCourses());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //[SwaggerResponse(StatusCodes.Status409Conflict, Description = "The same course name and start time already exists under the same lecturer.")]
    public IActionResult CreateCourse([FromBody] CourseDto course)
    {
        //TODO: Conflict Detetion
        
        Guid courseId = Guid.NewGuid();

        _courseRepository.CreateCourse(new Course
        {
            courseId = courseId,
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

    [HttpDelete("{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteCourse(Guid courseId)
    {
        if(_courseRepository.DeleteCourse(courseId))
        {
            return Ok();
        }

        return NotFound();

    }

    [HttpPatch("{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult UpdateCourse(Guid courseId, [FromBody] JsonPatchDocument<Course?> patchDoc)
    {

        var existingCourse = _courseRepository.GetCourse(courseId);

        if (existingCourse == null) return NotFound();

        patchDoc.ApplyTo(existingCourse);

        //TODO: Implement and save the updated data.

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