using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace SauceDemoTests.Utils
{
    public class TestBase
    {
        protected readonly IPage Page;

        public TestBase(ScenarioContext context)
        {
            Page = (IPage)context["Page"];
        }

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
