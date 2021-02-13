Feature: PetStore
	Simple calculator for adding two numbers

@mytag

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


