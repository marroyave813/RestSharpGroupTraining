Feature: PetStore
	Simple calculator for adding two numbers

@mytag

Scenario: Update a product (PUT)
	Given a pet with an 1 that needs to be updated
	When the user updates the category with tony and not available
	Then the result should be ok
	
Scenario Outline: Search pet by valid status
	Given pets with status <status>
	When user search with status
	Then process is executed succesfully
	And a list of pets with the selected status
	
	Examples:

	| status    |
	| available |
	| pending   |
	| sold      |

	Scenario Outline: Search pet by status invalid status
	Given pets with status <status>
	When user search with status
	Then process is executed with errors
	And an error message with text "invalid status" shows

	Examples:

	| status  |
	| new     |
	| alive   |
	| adopted |

	Scenario: Search pet without status
	Given pets with no status
	When user search with status
	Then process is executed with errors
	And an error message with text "invalid status" shows



Scenario: Create a new Pet
	Given i'm gonna create a Pet with Pet Name: "Tony Quiceno v4", Type Name: "doggie", photoUrls: "string" and status: "available"
	When i add the Pet to the shelter
	Then the Pet is now present in the shelter with Name: "Tony Quiceno v4"
