using Lab3_AutomatedTesting.Models.CrudBooking;
using Reqnroll;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab3_AutomatedTesting.StepDefinitions.CrudBooking
{
    [Binding]
    public class CreateBookingSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private const string BaseUrl = "https://restful-booker.herokuapp.com";

        [Given(@"I have a RestSharp client configured for Restful Booker")]
        public void GivenIHaveARestSharpClientConfiguredForRestfulBooker()
        {
            _client = new RestClient(BaseUrl);
        }

        [When(@"I send a POST request to ""(.*)"" with valid booking data")]
        public void WhenISendAPOSTRequestToWithValidBookingData(string endpoint)
        {
            _request = new RestRequest(endpoint, Method.Post);
            _request.AddHeader("Accept", "application/json");

            // Підготовка тестових даних
            var newBooking = new Booking
            {
                Firstname = "John",
                Lastname = "Doe",
                Totalprice = 150,
                Depositpaid = true,
                Bookingdates = new BookingDates
                {
                    Checkin = "2026-12-01",
                    Checkout = "2026-12-10"
                },
                Additionalneeds = "Breakfast"
            };

            // RestSharp автоматично серіалізує об'єкт у JSON
            _request.AddJsonBody(newBooking);

            // Виконання запиту
            _response = _client.Execute(_request);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            int actualStatusCode = (int)_response.StatusCode;
            Assert.AreEqual(expectedStatusCode, actualStatusCode, "Status code does not match.");
        }

        [Then(@"the response should contain a valid booking ID and matching data")]
        public void ThenTheResponseShouldContainAValidBookingIDAndMatchingData()
        {
            // Десеріалізація відповіді від сервера
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var responseData = JsonSerializer.Deserialize<BookingResponse>(_response.Content, options);

            Assert.IsNotNull(responseData, "Response content is null.");
            Assert.IsTrue(responseData.Bookingid > 0, "Booking ID was not generated.");

            // Перевірка, чи дані збереглися коректно
            Assert.AreEqual("John", responseData.Booking.Firstname);
            Assert.AreEqual("Doe", responseData.Booking.Lastname);
            Console.WriteLine($"Generated Booking ID: {responseData.Bookingid}");
        }
    }
}
