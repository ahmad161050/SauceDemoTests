using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SauceDemoTests.StepDefinitions
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class CheckoutSteps : LoginFixture
    {
        private HomePage homePage;
        private CartPage cartPage;
        private CheckoutPage checkoutPage;

        [Given(@"I am logged in as a standard user")]
        public async Task GivenIAmLoggedInAsAStandardUser()
        {
            Logger.Info("Logging in as standard_user...");
            await PerformLogin("standard_user");
        }

        [When(@"I add a ""(.*)"" to the cart")]
        public async Task WhenIAddProductToCart(string productName)
        {
            Logger.Info($"Adding product to cart: {productName}");
            homePage = new HomePage(Page);
            await homePage.AddBackpackToCartAsync();
        }

        [When(@"I proceed to the cart")]
        public async Task WhenIProceedToTheCart()
        {
            Logger.Info("Proceeding to cart...");
            await homePage.GoToCartAsync();
            cartPage = new CartPage(Page);
        }

        [When(@"I click the checkout button")]
        public async Task WhenIClickTheCheckoutButton()
        {
            Logger.Info("Clicking checkout...");
            await cartPage.ClickCheckoutButtonAsync();
            checkoutPage = new CheckoutPage(Page);
        }

        [When(@"I fill in the checkout information with ""(.*)"", ""(.*)"", and ""(.*)""")]
        public async Task WhenIFillInCheckoutInformation(string firstName, string lastName, string postalCode)
        {
            Logger.Info($"Filling checkout form: {firstName}, {lastName}, {postalCode}");
            await checkoutPage.FillCheckoutInfoAsync(firstName, lastName, postalCode);
        }

        [When(@"I click the continue button")]
        public async Task WhenIClickTheContinueButton()
        {
            Logger.Info("Clicking Continue button...");
            await checkoutPage.ClickContinueButtonAsync();
        }

        [When(@"I click the finish button")]
        public async Task WhenIClickTheFinishButton()
        {
            Logger.Info("Clicking Finish button...");
            await checkoutPage.ClickFinishButtonAsync();
        }

        [Then(@"I should see the order confirmation message")]
        public async Task ThenIShouldSeeOrderConfirmation()
        {
            Logger.Info("Validating order confirmation message...");
            Assert.IsTrue(await checkoutPage.IsOrderCompleteMessageVisibleAsync(), "Order confirmation message not found.");
            Logger.Info("Order successfully placed.");
        }
    }
}