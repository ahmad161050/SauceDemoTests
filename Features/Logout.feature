Feature: Logout Functionality
        Ensure users can securely log out of the application.

        As a logged-in user,
        I want to be able to open the navigation menu
        And successfully log out,
    So that my session ends and I return to the login page.

    Scenario: User can log out and be redirected to login screen
        Given I am logged in as a standard user
        When I open the menu and click logout
        Then I should be logged out and returned to the login screen
