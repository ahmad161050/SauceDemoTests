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

        public LogoutSteps(ScenarioContext context) : base(context)
        {
            homePage = new HomePage(Page); // Page comes from TestBase
        }

        [When(@"I open the menu and click logout")]
        public async Task WhenIOpenTheMenuAndClickLogout()
        {
            await homePage.LogoutAsync();
            Logger.Info("👋 Logout action triggered.");
        }

        [Then(@"I should be logged out and returned to the login screen")]
        public async Task ThenIShouldBeLoggedOut()
        {
            Logger.Info("🔍 Verifying logout success...");

            // ✅ URL check for inventory
            string currentUrl = Page.Url;
            Assert.IsFalse(currentUrl.Contains("inventory"), $"❌ Still on inventory page! URL: {currentUrl}");

            // ✅ Use LoginPage POM for login button check
            var loginPage = new LoginPage(Page);
            bool isLoginVisible = await loginPage.IsLoginButtonVisibleAsync();

            Assert.IsTrue(isLoginVisible, "❌ Login button not visible — logout may have failed.");
            Logger.Info("✅ Logout successful. Inventory access blocked and login screen visible.");
        }

    }
}
