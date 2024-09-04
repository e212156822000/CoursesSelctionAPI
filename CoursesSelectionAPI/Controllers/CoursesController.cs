using Microsoft.AspNetCore.Mvc;
using CoursesSelectionAPI.Models;
using CoursesSelectionAPI.Constants;
using Microsoft.AspNetCore.JsonPatch;

namespace CoursesSelectionAPI.Controllers;

[ApiController]
[Route(RouteContants.Courses)]
public class CoursesController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    public CoursesController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet("{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCourse(Guid courseId)
    {
        var course = await _courseRepository.FindCourseByIdAsync(courseId);

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
    public IActionResult CreateCourse([FromBody] CourseDto courseDto)
    {
        //TODO: Conflict Detetion

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var course = new Course(courseDto);

        _courseRepository.CreateCourseAsync(course);

        return CreatedAtAction(nameof(GetCourse), new { courseId = course.CourseId }, course.CourseId);
    }

    [HttpDelete("{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCourseAsync(Guid courseId)
    {
        var course = await _courseRepository.FindCourseByIdAsync(courseId);

        if (course == null)
        {
            return NotFound();
        }

        await _courseRepository.DeleteCourseAsync(course);

        return Ok();

    }

    [HttpPatch("{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Course))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCourseAsync(Guid courseId, [FromBody] JsonPatchDocument<Course?> patchDoc)
    {
        //TODO: Handle Schedule Conflict
        var existingCourse = await _courseRepository.FindCourseByIdAsync(courseId);

        if (existingCourse == null) return NotFound();

        patchDoc.ApplyTo(existingCourse);

        await _courseRepository.UpdateCourse(courseId, existingCourse);

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