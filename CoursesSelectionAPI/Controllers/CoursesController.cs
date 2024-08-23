using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoursesSelectionAPI.Models;
using Microsoft.AspNetCore.Http;

namespace CoursesSelectionAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private List<Course> _fakeDB = Enumerable.Range(1, 5).Select(index => new Course
        {
            id = Guid.NewGuid(),
            name = "Operating System " + index,
            description = "A fundamental course to introduce Operation System",
            start_time = Tools.CreateDayOfWeek(4, 9, 0),
            end_time = Tools.CreateDayOfWeek(4, 12, 0)
        })
        .ToList();

        public CoursesController(List<Course> db)
        {
            _fakeDB = db;
        }

        //public CoursesController(ILogger<CoursesController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        [Route("/{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Course))]
        public IActionResult GetCourses([FromRoute]string? id = null)
        {
            Console.WriteLine("id = ", id);

            if (string.IsNullOrEmpty(id))
            {
                return Ok(_fakeDB);
            }

            if (Guid.TryParse(id, out var courseId))
            {
                foreach (var course in _fakeDB)
                {
                    if (course.id == courseId) return Ok(course);
                }
                return NotFound();
            }
            else
            {
                Console.WriteLine($"Unable to convert {id} to a Guid");
                return BadRequest();
            }

        }

        /// <summary>
        /// <para name="name" />
        /// </summary>
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

            if(credits_int < 0 || classroomId_int < 0)
            {
                return BadRequest();
            }

            return CreateCourse(name, description, start_time, end_time, rating_policy, credits_int, classroomId_int);
        }

        //set to public for testing
        public IActionResult CreateCourse(
            string name,
            string description,
            DateTime start_time,
            DateTime end_time,
            string rating_policy,
            int credits,
            int classroomId)
        {
            Guid courseId = Guid.NewGuid();
            _fakeDB.Append(new Course
            {
                id = courseId,
                name = name,
                description = description,
                start_time = start_time,
                end_time = end_time,
                rating_policy = rating_policy,
                credits = credits,
                classroomId = classroomId
            }); ;

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
            if (int.TryParse(input, out int output)){
                return output;
            }

            return -1;
        }
    }
}