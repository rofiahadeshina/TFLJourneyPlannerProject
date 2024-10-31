Feature: TFL Journey Planner UI Automation
  As a user, I want to plan journeys using the TFL widget
  so that I can validate different preferences, view journey details, and handle invalid journeys.


Background:
    Given I am on the TFL journey planner page

@PlanValidJourney
Scenario: 01_Verify that a valid journey can be planned 
	When a user inputs the start and destination location of their journey
	And the user clicks on the plan my journey button
	Then the journey results should be visible
    And the walking and cyling times should be visible


@EditPreferences
Scenario: 02_Verify that a user can edit journey preferences after planning a journey
    When a valid journey has been planned from "Leicester Square Underground Station" to "Covent Garden Underground Station"
    And the user clicks Edit preferences
    And select the route preference for least walking
    And update the journey plan
    Then the updated journey results should be visible and the journey time should be validated according to the "least walking" preference

@ViewDetails
Scenario: 03_View details and verify access information at Covent Garden
    Given a valid journey has been planned with the least walking preference
    When I click on "View Details" for the journey
    Then I should see complete access information for Covent Garden Underground Station

@Regression
Scenario: 04_Verify that a user cannot plan a journey with invalid location data
	When a user fill-in From and To field with Invalid location data
	And clicks on Plan a journey button
	Then the result page Journey results Must be displayed 
	And the result "Sorry, we can't find a journey matching your criteria" must be displayed


Scenario Outline: 05_Verify that a user cannot plan a journey with empty location fields
	When a user leaves the from field blank 
	And a user leaves the to field blank 
	And clicks on Plan a journey button
	Then an error message <ExpectedErrorMessage> should be displayed
	Examples:
	| FromField | ToField | ExpectedErrorMessage        |
	|           |         | The From field is required. |
	|           |         | The To field is required.   |


