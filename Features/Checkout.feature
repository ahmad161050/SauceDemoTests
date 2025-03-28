Feature: Checkout Functionality
  As a logged-in user
  I want to add a product to the cart and complete checkout
  So that I can place an order and have it recorded in the database

  Scenario: User can complete the checkout process
    Given I am logged in as a standard user
    When I add a "Sauce Labs Backpack" to the cart
    And I proceed to the cart
    And I click the checkout button
    And I fill in the checkout information with "John", "Doe", and "12345"
    And I click the continue button
    And I click the finish button
    Then I should see the order confirmation message

  Scenario: Checkout inserts accurate order in the database
    Given I am logged in as a standard user
    When I complete the checkout with product "Sauce Labs Backpack" and user "John", "Doe", "12345"
    Then the order should be saved in the database for product "Sauce Labs Backpack"

