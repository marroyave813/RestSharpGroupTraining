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

Scenario: Log in with existing user
	Given user "mauricioarroyave@gmail.com" with password "cGFzc3dvcmQx"
	When the user logs in
	Then A session token generates
	And the token is valid for user "mauricioarroyave@gmail.com"

Scenario: Log in with non existing user
	Given user "mauricioarroyave@yimail.com" with password "1234567"
	When the user logs in
	Then An error with text "User does not exist." shows

Scenario: Log in with a possible existing user
	Given user "silvanaperez@houmail.com" with password "481216"
	When the user logs in
	Then An error with text "User could exist." shows nothing

Scenario: Log in with 3 users
	Given user "elzapatico@houmail.com" with password "639852"
	And user "elzapatico2@houmail.com" with password "639852"
	And user "elzapatic3o@houmail.com" with password "639852"
	When the user logs in
	Then An error with text "User could exist." shows nothing