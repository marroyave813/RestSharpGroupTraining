Feature: PetStore
	Simple calculator for adding two numbers

@mytag
Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120



Scenario: Create a new Pet
	Given i'm gonna create a Pet with Pet Name: "Tony Quiceno v4", Type Name: "doggie", photoUrls: "string" and status: "available"
	When i add the Pet to the shelter
	Then the Pet is now present in the shelter with Name: "Tony Quiceno v4"