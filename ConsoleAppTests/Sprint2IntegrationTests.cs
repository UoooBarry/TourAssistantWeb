using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourWebApp;
using Xunit;

namespace ConsoleAppTests
{
    public class Sprint2IntegrationTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory; //factory from TourWebApp start up

        public Sprint2IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_SuccessfulLogIn()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var logoutResponse = await client.GetAsync("/LogoutNow");

            // Log out and redirect back to log in page
            Assert.Equal(HttpStatusCode.OK, logoutResponse.StatusCode);
            Assert.StartsWith("http://localhost/",
                logoutResponse.RequestMessage.RequestUri.AbsoluteUri);
        }


        [Fact]
        public async Task Post_CreateAccount()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var createResponse = await client.PostAsync("/Users/Create", new JsonContent(new { LoginId = "87654321", Password = "123456" }));
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

          
        }


        [Fact]
        public async Task Get_RemoveLocation()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var removeResponse = await client.GetAsync("Locations/Delete/5");
            Assert.Equal(HttpStatusCode.OK, removeResponse.StatusCode);


        }

        [Fact]
        public async Task Get_EditLocation()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var editResponse = await client.GetAsync("Locations/Edit/5");
            Assert.Equal(HttpStatusCode.OK, editResponse.StatusCode);


        }

        [Fact]
        public async Task Get_TourType()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            var createResponse = await client.PostAsync("TourTypes/Create", new JsonContent(new { ID = 1 , Label = "testing"}));
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);


            var deleteResponse = await client.PostAsync("TourTypes/Delete/", new JsonContent(new { ID = 1 }));
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

            var editResponse = await client.GetAsync("TourTypes/Edit/5");
            Assert.Equal(HttpStatusCode.OK, editResponse.StatusCode);


        }

        [Fact]
        public async Task Get_Tour()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Login with correct user
            var response = await client.PostAsync("/Locations", new JsonContent(new { loginID = 12345678, password = "abc123" }));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            var createResponse = await client.PostAsync("Tours/Create", new JsonContent(new { ID = 1, Label = "testing" }));
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);



            var viewResponse = await client.GetAsync("Tours/Details/1");
            Assert.Equal(HttpStatusCode.OK, viewResponse.StatusCode);


        }

        class JsonContent : StringContent
        {
            public JsonContent(object obj) :
                base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }

    }
}
