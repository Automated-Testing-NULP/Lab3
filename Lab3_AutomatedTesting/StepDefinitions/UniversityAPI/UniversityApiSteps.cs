using Lab3_AutomatedTesting.Models.UniversityAPI;
using Reqnroll;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab3_AutomatedTesting.StepDefinitions.CountryAPI
{
    [Binding]
    public class UniversityApiSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;

        [Given(@"I have a RestSharp client for the Universities API")]
        public void GivenIHaveARestSharpClientForTheUniversitiesAPI()
        {
            _client = new RestClient("http://universities.hipolabs.com");
        }

        [When(@"I send a GET request to search universities in ""(.*)""")]
        public void WhenISendAGETRequestToSearchUniversitiesIn(string countryName)
        {
            _request = new RestRequest("/search", Method.Get);
            _request.AddQueryParameter("country", countryName);
            _response = _client.Execute(_request);
        }

        [Then(@"the API response status code should be (.*)")]
        public void ThenTheAPIResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)_response.StatusCode);
        }

        [Then(@"the response should contain a list of Ukrainian universities")]
        public void ThenTheResponseShouldContainAListOfUkrainianUniversities()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var universities = JsonSerializer.Deserialize<List<UniversityModel>>(_response.Content, options);

            Assert.IsNotNull(universities, "Universities list is null.");
            Assert.IsTrue(universities.Count > 0, "No universities were found.");

            Assert.AreEqual("Ukraine", universities[0].Country);

            Console.WriteLine($"Found universities: {universities.Count}");
            Console.WriteLine($"Example: {string.Join(", ", universities.FindAll(u => u.Name.Contains("Lviv")).Select(u => u.Name))}");
        }
    }
}
