using SauceDemoTests.Helpers;
using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using SauceDemoTests.Database;
using TechTalk.SpecFlow;
using NUnit.Framework;


namespace SauceDemoTests.StepDefinitions
{
    [Binding]
    public class CheckoutSteps : TestBase
    {
        private HomePage homePage;
        private CartPage cartPage;
        private CheckoutPage checkoutPage;
        private readonly CheckoutHelper _checkoutHelper;


        public CheckoutSteps(ScenarioContext context) : base(context)
        {
            _checkoutHelper = new CheckoutHelper(Page);
        }

        [Given(@"I am logged in as a standard user")]
        public async Task GivenIAmLoggedInAsAStandardUser()
        {
            Logger.Info("Logging in as standard_user...");
            await PerformLoginAsync("standard_user");
        }

        [When(@"I add a ""(.*)"" to the cart")]
        public async Task WhenIAddProductToCart(string productName)
        {
            Logger.Info($"Adding product to cart: {productName}");
            homePage = new HomePage(Page);
            await homePage.AddBackpackToCartAsync(); // For now, fixed item
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
            Logger.Info($"Filling checkout info: {firstName}, {lastName}, {postalCode}");
            await checkoutPage.FillCheckoutInfoAsync(firstName, lastName, postalCode);
        }

        [When(@"I click the continue button")]
        public async Task WhenIClickTheContinueButton()
        {
            Logger.Info("Clicking Continue...");
            await checkoutPage.ClickContinueButtonAsync();
        }

        [When(@"I click the finish button")]
        public async Task WhenIClickTheFinishButton()
        {
            Logger.Info("Clicking Finish...");
            await checkoutPage.ClickFinishButtonAsync();
        }

        [Then(@"I should see the order confirmation message")]
        public async Task ThenIShouldSeeOrderConfirmation()
        {
            Logger.Info("Validating order confirmation...");
            Assert.IsTrue(await checkoutPage.IsOrderCompleteMessageVisibleAsync(), "❌ Order confirmation not found");
            Logger.Info("✅ Order placed successfully.");
        }

        // ✅ NEW: For Task 3 — DB insert validation
        [When(@"I complete the checkout with product ""(.*)"" and user ""(.*)"", ""(.*)"", ""(.*)""")]
        public async Task WhenICompleteCheckout(string product, string firstName, string lastName, string postalCode)
        {
            Logger.Info($"🛒 Performing complete checkout for {product} / {firstName} {lastName}");
            await _checkoutHelper.CompleteCheckoutAsync(product, firstName, lastName, postalCode);

            // ✅ Insert into database
            OrderDatabaseHelper.InsertOrder(product, firstName, lastName, postalCode);
        }

        [Then(@"the order should be saved in the database for ""(.*)""")]
        public void ThenOrderShouldExistInDatabase(string firstName)
        {
            Logger.Info("🔍 Verifying DB order insert...");
            bool exists = OrderRepository.IsOrderPresentForCustomer(firstName);
            Assert.IsTrue(exists, $"❌ No order found in DB for '{firstName}'");
            Logger.Info("✅ Order confirmed in DB.");
        }
    }
}
