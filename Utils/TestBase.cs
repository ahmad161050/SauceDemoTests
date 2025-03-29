// Base class for test steps, providing shared setup and login functionality.
// Retrieves the Playwright page instance from the SpecFlow scenario context.

using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace SauceDemoTests.Utils
{
    public class TestBase
    {
        // Playwright page instance used for browser interactions.
        protected readonly IPage Page;

        // Initializes the base with a Playwright page from the scenario context.
        public TestBase(ScenarioContext context)
        {
            Page = (IPage)context["Page"];
        }

        // Performs login using the specified username and default password.
        protected async Task PerformLoginAsync(string username)
        {
            var loginPage = new Pages.LoginPage(Page);
            await loginPage.GoToAsync();
            await loginPage.EnterUsernameAsync(username);
            await loginPage.EnterPasswordAsync("secret_sauce");
            await loginPage.ClickLoginAsync();
        }
    }
}
