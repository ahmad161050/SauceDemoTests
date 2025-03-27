using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using TechTalk.SpecFlow;
using NUnit.Framework;


namespace SauceDemoTests.StepDefinitions
{
    [Binding]
    [Parallelizable(ParallelScope.All)] // ✅ Enable full parallelism for this test class
    public class LoginSteps : TestBase
    {
        private LoginPage loginPage;

        [Given(@"I am on the SauceDemo login page")]
        public async Task GivenIAmOnTheSauceDemoLoginPage()
        {
            Logger.Info("Navigating to the SauceDemo login page.");
            loginPage = new LoginPage(Page);
            await loginPage.GoToAsync();
        }

        [When(@"I enter username ""(.*)""")]
        public async Task WhenIEnterUsername(string username)
        {
            Logger.Info($"Entering username: {username}");
            await loginPage.EnterUsernameAsync(username);
        }

        [When(@"I enter the password")]
        public async Task WhenIEnterThePassword()
        {
            Logger.Info("Entering password: [HIDDEN]");
            await loginPage.EnterPasswordAsync("secret_sauce");
        }

        [When(@"I click the login button")]
        public async Task WhenIClickTheLoginButton()
        {
            Logger.Info("Clicking the login button.");
            await loginPage.ClickLoginAsync();
        }

        [Then(@"I should be redirected to the inventory page")]
        public async Task ThenIShouldBeRedirectedToTheInventoryPage()
        {
            Logger.Info("Verifying redirection to the inventory page.");
            Assert.That(Page.Url, Does.Contain("/inventory.html"));
            Logger.Info("Redirection successful.");
        }

        [Then(@"I should see an error message ""(.*)""")]
        public async Task ThenIShouldSeeAnErrorMessage(string expectedMessage)
        {
            Logger.Info("Verifying error message.");
            string actualMessage = await loginPage.GetErrorMessageAsync();
            Logger.Info($"Actual error message: \"{actualMessage}\"");

            Assert.That(actualMessage, Does.Contain(expectedMessage));
            Logger.Info("Error message validated successfully.");
        }

    }
}