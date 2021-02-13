Feature: PetStore
	Simple calculator for adding two numbers

@mytag

Scenario: Update a product (PUT)
	Given a pet with an 1 that needs to be updated
	When the user updates the category with tony and not available
	Then the result should be ok



	Scenario Outline: Log in with non existing user
	Given user <email> with password <password>
	When the user logs in
	Then An error with text "User does not exist." shows

	Examples: 
	| email                    | password      |
	| correonoexiste@gmail.com | 3498758fgioh  |
	| noimporta@gmail.com      | 309t8hsdfiosh |
	| nomedejemorir@gmail.com  | e0498jf       |
