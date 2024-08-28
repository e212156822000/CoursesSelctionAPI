using System;
using CourseSelectionAPI.Models;
using CoursesSelectionAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoursesSelectionAPI.Controllers;

[ApiController]
[Route("lectuers/")]
public class LecturersController : ControllerBase
{
    private ILecturerRepository _lecturerRepository;

    private ICourseRepository _courseRepository;

    public LecturersController(ILecturerRepository lecturerRepository, ICourseRepository courseRepository)
    {
        _lecturerRepository = lecturerRepository;
        _courseRepository = courseRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LecturerResponseDto>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllLecturers(
        [FromQuery] int offset,
        [FromQuery] int limit,
        [FromQuery] string dept,
        [FromQuery] string college)
    {
        return Ok(_lecturerRepository.ListLecturers());
    }

    [HttpGet("{lecturerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LecturerResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetLecturer(string lecturerId)
    {
        var lecturer = _lecturerRepository.GetLecturerById(lecturerId);

        return lecturer != null ? Ok(lecturer) : NotFound();
    }


    [HttpPut("{lecturerId}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LecturerResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateLecturer(string lecturerId, [FromBody] LecturerDto lecturer)
    {
        _lecturerRepository.CreateLecturer(new Lecturer
        {
            LecturerId = lecturerId,
            Office = lecturer.Office
        });

        return CreatedAtAction(nameof(GetLecturer), "lecturers/", new { id = lecturerId }, lecturer);

    }


    [HttpGet("{lecturerId}/courses")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CourseDto>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCoursesByLecturerId(
        string lecturerId,
        [FromQuery] int offset,
        [FromQuery] int limit,
        [FromQuery] string dept,
        [FromQuery] string college)
    {
        var lecturer = _lecturerRepository.GetLecturerById(lecturerId);

        if (lecturer == null) return NotFound();

        var courseList = _courseRepository.GetCourseByLecturerId(lecturerId);

        return courseList.Count == 0 ? NoContent() : Ok(courseList);
    }
}

