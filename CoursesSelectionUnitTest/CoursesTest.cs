/*
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
                CourseId = new Guid(),
                Name = "Operating System " + index,
                Description = "A fundamental course to introduce Operation System",
                StartTime = Tools.CreateDayOfWeek(4, 9, 0),
                EndTime = Tools.CreateDayOfWeek(4, 12, 0)
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
                CourseId = Guid.NewGuid(),
                Name = "Operating System " + index,
                Description = "A fundamental course to introduce Operation System",
                StartTime = Tools.CreateDayOfWeek(4, 9, 0),
                EndTime = Tools.CreateDayOfWeek(4, 12, 0)
            })
            .ToList();

            initDb.Add(new Course
            {
                CourseId = courseId,
                Name = "Tested Course",
                Description = "A Tested Course",
                StartTime = Tools.CreateDayOfWeek(4, 9, 0),
                EndTime = Tools.CreateDayOfWeek(4, 12, 0)
            });

            var coursesController = new CoursesController(initDb);

            IActionResult actionResult = coursesController.GetCourses(courseId.ToString());
            var okResult = actionResult as OkObjectResult;
            var body = (Course)okResult.Value;
            Assert.AreEqual(courseId, body.CourseId);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [TestMethod]
        public void CreateCourse_SameNameDifferentStartTIme_Success()
        {
            //CreateCourseAsync(string Name, string Description, int ClassroomId, DateTime StartTime, DateTime EndTime, int Credits, string RatingPolicy);
            List<Course> initDb = Enumerable.Range(1, 2).Select(index => new Course
            {
                CourseId = Guid.NewGuid(),
                Name = "Operating System",
                Description = "A fundamental course to introduce Operation System",
                StartTime = Tools.CreateDayOfWeek(4, 9, 0),
                EndTime = Tools.CreateDayOfWeek(4, 12, 0)
            })
            .ToList();

            var coursesController = new CoursesController(initDb);

            //int Name = "Operating System";

            //IActionResult actionResult = coursesController.CreateCourseAsync();
            //var okResult = actionResult as OkObjectResult;

            //Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}
*/