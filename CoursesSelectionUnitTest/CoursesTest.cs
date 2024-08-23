using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Linq;
using CoursesSelectionAPI.Controllers;
using CoursesSelectionAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CourseSelectionUnitTest
{
    [TestClass]
    public class Courses
    {
        [TestMethod]
        public void GetCourses_NoParams_Success()
        {
            //Arrange
            List<Course> initDb = Enumerable.Range(1, 5).Select(index => new Course
            {
                id = new Guid(),
                name = "Operating System " + index,
                description = "A fundamental course to introduce Operation System",
                start_time = Tools.CreateDayOfWeek(4, 9, 0),
                end_time = Tools.CreateDayOfWeek(4, 12, 0)
            })
            .ToList();

            var coursesController = new CoursesController(initDb);

            //Test 
            IActionResult actionResult = coursesController.GetCourses();
            var okResult = actionResult as OkObjectResult;
            var body = (IEnumerable<Course>)okResult.Value;
            Assert.AreEqual(5, body.Count<Course>());
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetCourses_ValidCourseId_Success()
        {
            //Arrange
            Guid courseId = Guid.NewGuid();
            List<Course> initDb = Enumerable.Range(1, 5).Select(index => new Course
            {
                id = Guid.NewGuid(),
                name = "Operating System " + index,
                description = "A fundamental course to introduce Operation System",
                start_time = Tools.CreateDayOfWeek(4, 9, 0),
                end_time = Tools.CreateDayOfWeek(4, 12, 0)
            })
            .ToList();

            initDb.Add(new Course
            {
                id = courseId,
                name = "Tested Course",
                description = "A Tested Course",
                start_time = Tools.CreateDayOfWeek(4, 9, 0),
                end_time = Tools.CreateDayOfWeek(4, 12, 0)
            });

            var coursesController = new CoursesController(initDb);

            IActionResult actionResult = coursesController.GetCourses(courseId.ToString());
            var okResult = actionResult as OkObjectResult;
            var body = (Course)okResult.Value;
            Assert.AreEqual(courseId, body.id);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [TestMethod]
        public void CreateCourse_SameNameDifferentStartTIme_Success()
        {
            //CreateCourse(string name, string description, int classroomId, DateTime start_time, DateTime end_time, int credits, string rating_policy);
            List<Course> initDb = Enumerable.Range(1, 2).Select(index => new Course
            {
                id = Guid.NewGuid(),
                name = "Operating System",
                description = "A fundamental course to introduce Operation System",
                start_time = Tools.CreateDayOfWeek(4, 9, 0),
                end_time = Tools.CreateDayOfWeek(4, 12, 0)
            })
            .ToList();

            var coursesController = new CoursesController(initDb);

            //int name = "Operating System";

            //IActionResult actionResult = coursesController.CreateCourse();
            //var okResult = actionResult as OkObjectResult;

            //Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}
