Feature: Valid Email Address
	Validate email addresses in various formats

Scenario: Valid email in username
	When I enter "joe@abc.co.uk"
	Then the email address should be valid

Scenario: Valid email with period in username
	When I enter "joe.bloggs@abc.co.uk"
	Then the email address should be valid

Scenario: Valid email with underscore in username
	When I enter "joe_bloggs@abc.co.uk"
	Then the email address should be valid

Scenario: Valid email with hyphen in username
	When I enter "joe-bloggs@abc.co.uk"
	Then the email address should be valid
