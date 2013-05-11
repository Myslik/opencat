﻿Feature: Jobs

Background: 
	Given I authenticate with valid credentials

@web
Scenario: Create new job
	When I create following jobs
		| name      | description      | words |
		| Butterfly | Beautiful animal | 123   |
	Then I can see following on jobs page
		| name      | description      | words |
		| Butterfly | Beautiful animal | 123   |

@web
Scenario: Edit job
	When I create following jobs
		| name      | description      | words |
		| Elephant  | Biggest of all   | 999   |
	 And I visit job with name Elephant
	Then I can edit the job

@web
Scenario: Upload file to job
	When I create following jobs
		| name      | description      | words |
		| Kangoroo  | Jumpy            | 456   |
	 And I visit job with name Kangoroo
	 And I upload "1.xlz" to job
	Then There is "1.xlz" in the attachments

@web
Scenario: Remove job
	When I create following jobs
		| name      | description      | words |
		| Cocroach  | Cannot be killed | 321   |
	 And I delete job with name Cocroach
	Then There is no job with name Cocroach on jobs page