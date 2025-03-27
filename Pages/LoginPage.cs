using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class LoginPage
    {
        private readonly IPage page;
        private readonly string url = "https://www.saucedemo.com";

        public LoginPage(IPage page)
        {
            this.page = page;
        }

        public async Task GoToAsync() => await page.GotoAsync(url);

        public async Task EnterUsernameAsync(string username)
        {
            await page.FillAsync("#user-name", username);
        }

        public async Task EnterPasswordAsync(string password)
        {
            await page.FillAsync("#password", password);
        }

        public async Task ClickLoginAsync()
        {
            await page.ClickAsync("#login-button");
        }

        public async Task<string> GetErrorMessageAsync()
        {
            return await page.TextContentAsync("[data-test='error']");
        }
    }
}
