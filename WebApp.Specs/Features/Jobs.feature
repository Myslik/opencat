Feature: Jobs

Background: 
	Given I authenticate with valid credentials

@WebUI
Scenario: Create new job
	When I create following jobs
		| name      | description      | words |
		| Butterfly | Beautiful animal | 123   |
	Then I can see following on jobs page
		| name      | description      | words |
		| Butterfly | Beautiful animal | 123   |

@WebUI
Scenario: Edit job
	When I create following jobs
		| name      | description      | words |
		| Elephant  | Biggest of all   | 999   |
	 And I visit job with name Elephant
	Then I can edit the job

@WebUI
Scenario: Upload file to job
	When I create following jobs
		| name      | description      | words |
		| Kangoroo  | Jumpy            | 456   |
	 And I visit job with name Kangoroo
	 And I upload "1.xlz" to job with name Kangoroo
	Then There is "1.xlz" in the attachments

@WebUI
Scenario: Delete file from job
	When I create following jobs
		| name      | description      | words |
		| Croco     | Sneaky           | 456   |
	 And I visit job with name Croco
	 And I upload "1.xlz" to job with name Croco
	Then There is "1.xlz" in the attachments
	When I remove attachment "1.xlz" from the job
	Then There is no "1.xlz" in the attachments

@WebUI
Scenario: Remove job
	When I create following jobs
		| name      | description      | words |
		| Cocroach  | Cannot be killed | 321   |
	 And I delete job with name Cocroach
	Then There is no job with name Cocroach on jobs page
