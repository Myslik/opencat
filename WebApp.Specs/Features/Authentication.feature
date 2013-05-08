Feature: Authentication

Scenario: Valid Login
	When I authenticate with valid credentials
	Then I should be redirected to application

Scenario: Invalid Login	
	When I authenticate with invalid credentials
	Then I should see invalid credentials message

Scenario: Logout
	When I authenticate with valid credentials
	 And I log out
	Then I should see the login form
