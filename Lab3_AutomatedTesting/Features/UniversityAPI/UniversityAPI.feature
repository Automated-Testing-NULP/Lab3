Feature: University API Testing
  As a data analyst
  I want to retrieve university information
  So that I can verify the API returns correct data about universities

Scenario: Successfully retrieve universities for Ukraine
  Given I have a RestSharp client for the Universities API
  When I send a GET request to search universities in "Ukraine"
  Then the API response status code should be 200
  And the response should contain a list of Ukrainian universities