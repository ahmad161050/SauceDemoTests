// Encapsulates interactions with the SauceDemo login page,
// including login actions, error validation, and visibility checks.

using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    /// Represents the login page and encapsulates user interactions with it.
    public class LoginPage
    {
        private readonly IPage page;
        private readonly string url = "https://www.saucedemo.com";

        // Locators
        private readonly ILocator usernameInput;
        private readonly ILocator passwordInput;
        private readonly ILocator loginButton;
        private readonly ILocator errorMessage;

        public LoginPage(IPage page)
        {
            this.page = page;

            usernameInput = page.Locator("#user-name");
            passwordInput = page.Locator("#password");
            loginButton = page.Locator("#login-button");
            errorMessage = page.Locator("h3[data-test='error']:has-text('Sorry, this user has been locked out')");
        }

        /// Navigates to the SauceDemo login page.
        public async Task GoToAsync() => await page.GotoAsync(url);

        /// Fills in the username field.
        public async Task EnterUsernameAsync(string username) =>
            await usernameInput.FillAsync(username);

        /// Fills in the password field.
        public async Task EnterPasswordAsync(string password) =>
            await passwordInput.FillAsync(password);

        /// Clicks the login button.
        public async Task ClickLoginAsync() =>
            await loginButton.ClickAsync();

        /// Returns the error message text after a failed login.
        public async Task<string> GetErrorMessageAsync() =>
            await errorMessage.InnerTextAsync();

        /// Checks if the login button is visible.
        public async Task<bool> IsLoginButtonVisibleAsync() =>
            await loginButton.IsVisibleAsync();
    }
}
