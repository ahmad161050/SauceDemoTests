using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class LoginPage
    {
        private readonly IPage page;
        private readonly string url = "https://www.saucedemo.com";

        // ✅ Centralized locators
        private readonly ILocator usernameInput;
        private readonly ILocator passwordInput;
        private readonly ILocator loginButton;
        private readonly ILocator errorMessage;

        public LoginPage(IPage page)
        {
            this.page = page;

            // 🎯 Assign locators once
            usernameInput = page.Locator("#user-name");
            passwordInput = page.Locator("#password");
            loginButton = page.Locator("#login-button");
            errorMessage = page.Locator("[data-test='error']");
        }

        public async Task GoToAsync() => await page.GotoAsync(url);

        public async Task EnterUsernameAsync(string username) =>
            await usernameInput.FillAsync(username);

        public async Task EnterPasswordAsync(string password) =>
            await passwordInput.FillAsync(password);

        public async Task ClickLoginAsync() =>
            await loginButton.ClickAsync();

        public async Task<string> GetErrorMessageAsync() =>
            await errorMessage.InnerTextAsync();

        public async Task<bool> IsLoginButtonVisibleAsync() =>
            await loginButton.IsVisibleAsync();
    }
}
