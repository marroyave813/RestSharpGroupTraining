Feature: PetStore
	Simple calculator for adding two numbers

@mytag

Scenario: Update a product (PUT)
	Given a pet with an 1 that needs to be updated
	When the user updates the category with tony and not available
	Then the result should be ok



Scenario: Create a new Pet
	Given i'm gonna create a Pet with Pet Name: "Tony Quiceno v4", Type Name: "doggie", photoUrls: "string" and status: "available"
	When i add the Pet to the shelter
	Then the Pet is now present in the shelter with Name: "Tony Quiceno v4"