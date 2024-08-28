using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CoursesSelectionUnitTest
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly WebApplicationFactory<Program> _factory = new WebApplicationFactory<Program>();

        private readonly HttpClient _client;

        public HttpClientFactory(){

            _client = _factory.CreateClient();

        }
        //doese not have DB, so reuse client
        public HttpClient CreateClient(string name)
        {
            return _client;
        }

	}
}

