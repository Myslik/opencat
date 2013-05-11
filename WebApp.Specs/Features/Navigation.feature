Feature: Navigation

Background: 
	Given I authenticate with valid credentials

@web
Scenario: Navigate to all sections
	Then I can visit all sections
