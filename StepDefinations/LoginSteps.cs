// Step definitions for login functionality in SauceDemo using SpecFlow.
// Covers user login actions and corresponding success or error validations.

using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SauceDemoTests.StepDefinitions
{
    [Binding]
    public class LoginSteps : TestBase
    {
        private LoginPage loginPage;

        // Initializes the scenario context and base test functionality.
        public LoginSteps(ScenarioContext context) : base(context) { }

        // Navigates to the SauceDemo login page.
        [Given(@"I am on the SauceDemo login page")]
        public async Task GivenIAmOnTheSauceDemoLoginPage()
        {
            Logger.Info("Navigating to the SauceDemo login page.");
            loginPage = new LoginPage(Page);
            await loginPage.GoToAsync();
        }

        // Enters the specified username into the login form.
        [When(@"I enter username ""(.*)""")]
        public async Task WhenIEnterUsername(string username)
        {
            Logger.Info($"Entering username: {username}");
            await loginPage.EnterUsernameAsync(username);
        }

        // Enters the default password into the login form.
        [When(@"I enter the password")]
        public async Task WhenIEnterThePassword()
        {
            Logger.Info("Entering password: [HIDDEN]");
            await loginPage.EnterPasswordAsync("secret_sauce");
        }

        // Clicks the login button to attempt login.
        [When(@"I click the login button")]
        public async Task WhenIClickTheLoginButton()
        {
            Logger.Info("Clicking the login button.");
            await loginPage.ClickLoginAsync();
        }

        // Verifies that login was successful by checking URL redirection.
        [Then(@"I should be redirected to the inventory page")]
        public async Task ThenIShouldBeRedirectedToTheInventoryPage()
        {
            Logger.Info("Verifying redirection to the inventory page.");
            Assert.That(Page.Url, Does.Contain("/inventory.html"));
            Logger.Info("Redirection successful.");
        }

        // Asserts that the correct error message appears on failed login.
        [Then(@"I should see an error message ""(.*)""")]
        public async Task ThenIShouldSeeAnErrorMessage(string expectedMessage)
        {
            Logger.Info("Verifying error message.");
            var actualMessage = await loginPage.GetErrorMessageAsync();
            Assert.That(actualMessage, Does.Contain(expectedMessage));
            Logger.Info("Error message validated.");
        }
    }
}
