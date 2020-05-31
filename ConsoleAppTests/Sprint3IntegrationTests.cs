using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourWebApp;
using Xunit;

namespace ConsoleAppTests
{
    public class Sprint3IntegrationTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory; //factory from TourWebApp start up

        public Sprint3IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_EditTypeLabel()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var editResponse = await client.PostAsync("TourTypes/Edit/5", new JsonContent(new { ID = 5, Label = "testing" }));

            // Edit succesfully
            Assert.Equal(HttpStatusCode.OK, editResponse.StatusCode);
      
        }

        [Fact]
        public async Task Post_EditTourName()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var editResponse = await client.PostAsync("Tours/Edit/5", new JsonContent(new {  Name = "new" }));

            // Edit succesfully
            Assert.Equal(HttpStatusCode.OK, editResponse.StatusCode);

        }


        [Fact]
        public async Task Post_CopyLocations()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var copyResponse = await client.PostAsync("Tours/Copy/5", new JsonContent(new { ID = "5" }));

            // Edit succesfully
            Assert.Equal(HttpStatusCode.OK, copyResponse.StatusCode);

        }
        class JsonContent : StringContent
        {
            public JsonContent(object obj) :
                base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }
    }
}
