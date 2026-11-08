Feature: Create Booking
  As a tester
  I want to create a new booking
  So that I can verify the POST operation of the API

Scenario: Successfully create a new booking
  Given I have a RestSharp client configured for Restful Booker
  When I send a POST request to "/booking" with valid booking data
  Then the response status code should be 200
  And the response should contain a valid booking ID and matching data