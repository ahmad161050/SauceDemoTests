// Represents the cart page in the SauceDemo site.
// Responsible for interactions like proceeding to checkout.

using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class CartPage
    {
        private readonly IPage page;

        // Locator for the checkout button
        private readonly ILocator checkoutButton;

        // Initializes the locator once during object construction
        public CartPage(IPage page)
        {
            this.page = page;
            checkoutButton = page.Locator("#checkout");
        }

        // Clicks the checkout button to proceed to checkout
        public async Task ClickCheckoutButtonAsync()
        {
            await checkoutButton.ClickAsync();
        }
    }
}
