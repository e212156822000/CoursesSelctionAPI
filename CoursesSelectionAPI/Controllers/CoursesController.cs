using CoursesSelectionAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Course))]
    public IActionResult GetCourse(Guid id)
    {
        var course = _courseRepository.FindCourseByIdAsync(id);
        return course != null ? Ok(course) : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Course>))]
    public IActionResult GetAllCourses()
    {
        return Ok(_courseRepository.ListCourses());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    public async Task<IActionResult> CreateCourse([FromBody] CourseDto courseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var course = new Course(courseDto);
        await _courseRepository.CreateCourseAsync(course);

        return CreatedAtAction(nameof(GetCourse), RouteContants.Courses, new { id = course.CourseId }, course);
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