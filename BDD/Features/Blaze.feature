Feature: Blaze Demo Feature

@mytag
Scenario: Sign up with valid user
	Given a random user with password "MTIzcGFzcw=="
	When the user sign's up
	Then the process completes wihtout errors

Scenario: Sign up with existing user
	Given user "test1122@hotmail.com" with password "MTIzcGFzcw=="
	When the user sign's up
	Then An error with text "This user already exist." shows

	Scenario: Log in with existing
	Given user "mauricioarroyave@gmail.com" with password "cGFzc3dvcmQx"
	When the user logs in
	Then A session token generates