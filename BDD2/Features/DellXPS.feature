Feature: Amazon Buy Dell XPS

Scenario: Buy a Dell XPS Laptop in Amazon website
	Given I open Amazon website
        And I search by Dell XPS
        And I press enter key for submit
        And I choose the first item of list
    When I click in Add to Cart button
    Then Must have one item in cart