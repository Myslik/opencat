Feature: Authentication

@web
Scenario: Visit Login page
	Given I open the landing page
	Then I should see the login form

@web
Scenario: Valid Login
	When I authenticate with valid credentials
	Then I should be redirected to application

@web
Scenario: Invalid Login	
	When I authenticate with invalid credentials
	Then I should see invalid credentials message

@web
Scenario: Logout
	When I authenticate with valid credentials
	 And I log out
	Then I should see the login form
