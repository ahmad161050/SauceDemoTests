Feature: Checkout Functionality
  As a logged-in user
  I want to add a product to the cart and complete checkout
  So that I can place an order successfully

  Scenario: User can complete the checkout process
    Given I am logged in as a standard user
    When I add a "Sauce Labs Backpack" to the cart
    And I proceed to the cart
    And I click the checkout button
    And I fill in the checkout information with "John", "Doe", and "12345"
    And I click the continue button
    And I click the finish button
    Then I should see the order confirmation message
