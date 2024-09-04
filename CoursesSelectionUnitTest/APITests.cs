using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using CoursesSelectionAPI.Models;
using CoursesSelectionAPI.Controllers;
using CoursesSelectionAPI.Constants;
using System.Text.Json;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using CoursesSelectionUnitTest.Utils;
using Microsoft.AspNetCore.JsonPatch;

namespace CoursesSelectionUnitTest
{
	[TestClass]
	public class APITests
	{
        private readonly IHttpClientFactory _httpClientFactory;

        private List<Guid> _initializedIds = new List<Guid>();

        public APITests()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            var serviceProvider = services.BuildServiceProvider();

            _httpClientFactory = serviceProvider.GetService<IHttpClientFactory>() ?? throw new ArgumentException(nameof(IHttpClientFactory));

        }

        [TestInitialize]
        public async Task CoursesTestInitialize()
        {
            var client = _httpClientFactory.CreateClient();

            List<CourseDto> courses = Enumerable.Range(0, 5).Select(index => new CourseDto
            {
                Name = "Operating System " + index,
                Description = "A fundamental course to introduce Operation System",
                Credits = 3,
                RatingPolicy = "Homework 100%",
                StartTime = Tools.CreateDayOfWeek(4, 9, 0),
                EndTime = Tools.CreateDayOfWeek(4, 12, 0)
            })
            .ToList();

            foreach(var course in courses){

                string requestBody = JsonSerializer.Serialize(course);

                var response = await client.PutAsync(RouteContants.CoursesPath, new StringContent(requestBody, Encoding.UTF8, "application/json"));

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

                Assert.IsNotNull(response);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                Guid returnedCoursesId = JsonSerializer.Deserialize<Guid>(jsonResponse);

                Assert.IsNotNull(returnedCoursesId);

                _initializedIds.Add(returnedCoursesId);

            }

        }

        [TestCleanup]
        public async Task CoursesTestCleanUp()
        {
            var client = _httpClientFactory.CreateClient();

            foreach (var _initializedId in _initializedIds)
            {
                var response = await client.DeleteAsync(RouteContants.CoursesPath + _initializedId);

                Assert.IsNotNull(response);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            _initializedIds.Clear();
        }

        [TestMethod]
        public async Task DeleteCourse_ValidCourseId_Success()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.DeleteAsync(RouteContants.CoursesPath + _initializedIds.Last());

            Assert.IsNotNull(response);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            _initializedIds.Remove(_initializedIds.Last());
        }

        //[TestMethod]
        //public async Task GetCourses_ValidLecturerId_Success()
        //{
            //var client = _httpClientFactory.CreateClient();

            //var response = await client.GetAsync("courses/" + _initializedIds.First());

            //Assert.IsNotNull(response);

            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            //var jsonResponse = await response.Content.ReadAsStringAsync();

            //Course? course = JsonSerializer.Deserialize<Course>(jsonResponse);

            //Assert.IsNotNull(course);

            //Assert.AreEqual(_initializedIds.First(), course.CourseId);
        //}

        [TestMethod]
        public async Task UpdateCourse_ValidCourseId_Success()
        {
            var client = _httpClientFactory.CreateClient();

            var updatedName = "Updated Course Name";

            JsonPatchDocument<Course> patchDoc = new JsonPatchDocument<Course>();

            patchDoc.Replace(p => p.Name, updatedName);

            string requestBody = JsonSerializer.Serialize(patchDoc.Operations);

            var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json-patch+json");

            var response = await client.PatchAsync(RouteContants.CoursesPath + _initializedIds.First(), httpContent);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var course = await response.ReadJsonResponseAsync<Course>();

            Assert.IsNotNull(course);

            Assert.AreEqual(course.Name, updatedName);

        }

        [TestMethod]
        public async Task GetCourses_NoParams_Success()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(RouteContants.CoursesPath);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var courses = await response.ReadJsonResponseAsync<List<Course>>();
            Assert.IsNotNull(courses);
            Assert.AreEqual(5, courses.Count);

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(_initializedIds[i], courses[i].CourseId);
            }
        }

        [TestMethod]
        public async Task GetCourses_ValidCourseId_Success()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(RouteContants.CoursesPath + _initializedIds.First());

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var course = await response.ReadJsonResponseAsync<Course>();

            Assert.IsNotNull(course);

            Assert.AreEqual(_initializedIds.First(), course.CourseId);
        }

        [TestMethod]
        public async Task GetCourses_InvalidCourseId_NotFound()
        {
            //Arrange
            var client = _httpClientFactory.CreateClient();

            var invalidCourseId = Guid.NewGuid();

            var response = await client.GetAsync(RouteContants.CoursesPath + invalidCourseId);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}

