Feature: Authentication

@WebUI
Scenario: Visit Login page
	Given I open the landing page
	Then I should see the login form

@WebUI
Scenario: Valid Login
	When I authenticate with valid credentials
	Then I should be redirected to application

@WebUI
Scenario: Invalid Login	
	When I authenticate with invalid credentials
	Then I should see invalid credentials message

@WebUI
Scenario: Logout
	When I authenticate with valid credentials
	 And I log out
	Then I should see the login form
