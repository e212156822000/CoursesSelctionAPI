﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using CoursesSelectionAPI.Models;
using CoursesSelectionAPI.Controllers;
using System.Text.Json;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace CoursesSelectionUnitTest
{
	[TestClass]
	public class APITests
	{
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly HttpClient _client;

        private List<Guid> _initializedIds = new List<Guid>();

        public APITests()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            var serviceProvider = services.BuildServiceProvider();

            _httpClientFactory = serviceProvider.GetService<IHttpClientFactory>() ?? throw new ArgumentException(nameof(IHttpClientFactory));

            _client = _httpClientFactory.CreateClient();
        }

        [TestInitialize]
        public async Task CoursesTestInitialize()
        {
            var client = _client;

            List<Course> courses = Enumerable.Range(0, 5).Select(index => new Course
            {
                name = "Operating System " + index,
                description = "A fundamental course to introduce Operation System",
                credits = 3,
                rating_policy = "Homework 100%",
                start_time = Tools.CreateDayOfWeek(4, 9, 0),
                end_time = Tools.CreateDayOfWeek(4, 12, 0)
            })
            .ToList();

            foreach(var course in courses){

                string requestBody = JsonSerializer.Serialize(course);

                var response = await client.PutAsync("courses/", new StringContent(requestBody, Encoding.UTF8, "application/json"));

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                Assert.IsNotNull(response);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                Guid returnedCoursesId = JsonSerializer.Deserialize<Guid>(jsonResponse);

                Assert.IsNotNull(returnedCoursesId);

                _initializedIds.Add(returnedCoursesId);

            }

        }

        [TestMethod]
        public async Task GetCourses_NoParams_Success()
        {
            var client = _client;

            var response = await client.GetAsync("courses/");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            List<Course>? courses = JsonSerializer.Deserialize<List<Course>>(jsonResponse);

            Assert.IsNotNull(courses);

            Assert.AreEqual(5, courses.Count);

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(_initializedIds[i], courses[i].id);
            }
        }

        [TestMethod]
        public async Task GetCourses_ValidCourseId_Success()
        {
            //Arrange
            var client = _client;

            var response = await client.GetAsync("courses/"+_initializedIds.First());

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            Course? course = JsonSerializer.Deserialize<Course>(jsonResponse);

            Assert.IsNotNull(course);

            Assert.AreEqual(_initializedIds.First(), course.id);
        }

        [TestMethod]
        public async Task GetCourses_InvalidCourseId_NotFound()
        {
            //Arrange
            var client = _client;

            var invalidCourseId = Guid.NewGuid();

            var response = await client.GetAsync("courses/" + invalidCourseId);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}

