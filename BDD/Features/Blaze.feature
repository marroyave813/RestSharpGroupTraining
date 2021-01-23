Feature: Blaze Demo Feature

@mytag
Scenario: Sign up with valid user
	Given a random user with password "MTIzcGFzcw=="
	When the user sign's up
	Then the process completes wihtout errors

Scenario: Sign up with existing user in Blaze meter
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

	Scenario: Log in with another non existing user
	Given user "mauricioarroyave@yimail.com" with password "1234567"
	When the user logs in
	Then An error with text "User does not exist." shows

	Scenario: Log in with existing user again
	Given user "mauricioarroyave@gmail.com" with password "cGFzc3dvcmQx"
	When the user logs in
	Then A session token generates
	And the token is valid for user "mauricioarroyave@gmail.com"

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

	Scenario: Log in with another possible existing user
	Given user "silvanaperez@houmail.com" with password "481216"
	When the user logs in
	Then An error with text "User could exist." shows nothing

Scenario: Log in with 4 users
	Given user "elzapatico@houmail.com" with password "639852"
	And user "elzapatico2@houmail.com" with password "639852"
	And user "elzapatic3o@houmail.com" with password "639852"

	Scenario: this is a text to test
	Given user "elzapatico@houmail.com" with password "aaa111"
	And user "elzapatico2@other mail" with password "111222333"
	And user "elzapatic3o@houmail.com" with password "ccc333"

	Scenario: Log in with 10 users - silavana pérez rojas
	Given user "silvanaperez@houmail.com" with password "111aaaa"
	Given user "elzapatico@houmail.com" with password "478"
	And user "elzapatico2@houmail.com" with password "986"
	And user "elzapatico2@houmail.com" without pwd
	And user "elzapatic3o@houmail.com" with password "0287"
	When the user logs in
	Then An error with text "User could exist." shows nothing
	
Scenario: Log in with all users
	Given user "elzapatico@houmail.com" with password "639852"
	And user "elzapatico2@houmail.com" with password "639852"

Scenario: Log in nobody
	Given user "elzapatico@houmail.com" with password "639852"
	And user "elzapatico2@houmail.com" with password "639852"


