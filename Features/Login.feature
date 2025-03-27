Feature: Login Functionality
  As a user of SauceDemo
  I want to log in using valid credentials
  So that I can access the inventory page
  And I want to see proper errors when credentials are invalid

  Scenario: Login fails for locked out user
    Given I am on the SauceDemo login page
    When I enter username "locked_out_user"
    And I enter the password
    And I click the login button
    Then I should see an error message "Sorry, this user has been locked out."

  Scenario: Successful login with standard user
    Given I am on the SauceDemo login page
    When I enter username "standard_user"
    And I enter the password
    And I click the login button
    Then I should be redirected to the inventory page

  ##########################
  # OPTIONAL TEST SCENARIOS
  # Not required by the task, for future improvements
  ##########################

  # Scenario: Login with problem user shows broken product images
  #   Given I am on the SauceDemo login page
  #   When I enter username "problem_user"
  #   And I enter the password
  #   And I click the login button
  #   Then I should be redirected to the inventory page
  #   And I should see broken product images

  # Scenario: Login with performance glitch user is slow
  #   Given I am on the SauceDemo login page
  #   When I enter username "performance_glitch_user"
  #   And I enter the password
  #   And I click the login button
  #   Then I should be redirected to the inventory page
  #   But the login process should take noticeably longer than usual

  # Scenario: Login with error user causes item page failure
  #   Given I am on the SauceDemo login page
  #   When I enter username "error_user"
  #   And I enter the password
  #   And I click the login button
  #   Then I should be redirected to the inventory page
  #   And clicking on a product should not work as expected

  # Scenario: Login with visual user shows UI layout issues
  #   Given I am on the SauceDemo login page
  #   When I enter username "visual_user"
  #   And I enter the password
  #   And I click the login button
  #   Then I should be redirected to the inventory page
  #   And the layout of the page should appear visually broken
