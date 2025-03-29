// Step definitions for logout functionality in SauceDemo using SpecFlow.
// Validates the logout flow and post-logout UI state on the login screen.

using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SauceDemoTests.StepDefinitions
{
    [Binding]
    public class LogoutSteps : TestBase
    {
        private readonly HomePage homePage;

        // Initializes the HomePage using the shared browser context from TestBase.
        public LogoutSteps(ScenarioContext context) : base(context)
        {
            homePage = new HomePage(Page);
        }

        // Triggers logout via the application’s main menu.
        [When(@"I open the menu and click logout")]
        public async Task WhenIOpenTheMenuAndClickLogout()
        {
            await homePage.LogoutAsync();
            Logger.Info("Logout action triggered.");
        }

        // Validates that the user is successfully logged out and redirected to the login screen.
        [Then(@"I should be logged out and returned to the login screen")]
        public async Task ThenIShouldBeLoggedOut()
        {
            Logger.Info("Verifying logout success...");

            // Check that user is no longer on the inventory page.
            string currentUrl = Page.Url;
            Assert.IsFalse(currentUrl.Contains("inventory"), $"Unexpected URL after logout: {currentUrl}");

            // Ensure the login button is visible, confirming return to login screen.
            var loginPage = new LoginPage(Page);
            bool isLoginVisible = await loginPage.IsLoginButtonVisibleAsync();

            Assert.IsTrue(isLoginVisible, "Login button not visible — logout may have failed.");
            Logger.Info("Logout successful. Login screen is visible and inventory access is blocked.");
        }
    }
}
