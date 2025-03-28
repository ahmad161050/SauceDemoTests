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
        private readonly ScenarioContext _scenarioContext;



        public CheckoutSteps(ScenarioContext context) : base(context)
        {
            _scenarioContext = context;
            _checkoutHelper = new CheckoutHelper(Page);
        }


        [Given(@"I am logged in as a standard user")]
        public async Task GivenIAmLoggedInAsAStandardUser()
        {
            Logger.Info("Logging in as standard_user...");
            var username = "standard_user";
            await PerformLoginAsync(username);

            _scenarioContext["LoggedInUsername"] = username;
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

            // 💡 Get the actual logged-in username saved earlier
            var username = _scenarioContext["LoggedInUsername"]?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                Logger.Info("⚠️ LoggedInUsername not found in ScenarioContext. Falling back to firstName.");
                username = firstName; // fallback, just in case
            }

            // 🧹 Clean previous test data
            OrderDatabaseHelper.DeleteOrdersForCustomer(username);

            // ✅ Run full checkout and capture total price BEFORE finish
            decimal totalPrice = await _checkoutHelper.CompleteCheckoutAsync(product, firstName, lastName, postalCode);

            var checkoutPage = new CheckoutPage(Page);
            bool isOrderConfirmed = await checkoutPage.IsOrderCompleteMessageVisibleAsync();

            if (isOrderConfirmed)
            {
                Logger.Info($"✅ UI checkout success confirmed. Inserting into DB with price: {totalPrice}");
                OrderDatabaseHelper.InsertOrder(username, product, totalPrice); // 👈 save correct user
            }
            else
            {
                Logger.Info("⚠️ UI checkout didn't confirm. Skipping DB insert.");
            }
        }

        [Then(@"the order should be saved in the database for product ""(.*)""")]
        public void ThenOrderShouldExistInDatabase(string productName)
        {
            Logger.Info("🔍 Verifying order in database with correct user and product...");

            var username = _scenarioContext["LoggedInUsername"]?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                Assert.Fail("❌ LoggedInUsername not found in ScenarioContext. Cannot verify order.");
            }

            var order = OrderRepository.GetLatestOrderForUser(username);

            Assert.IsNotNull(order, $"❌ No order found in DB for user '{username}'");
            Assert.AreEqual(productName, order.ProductName, $"❌ Product mismatch. Expected '{productName}' but got '{order.ProductName}'");

            Logger.Info($"✅ Order verified: User = {order.Username}, Product = {order.ProductName}, Price = {order.TotalPrice}, Date = {order.OrderDate}");
        }


    }
}
