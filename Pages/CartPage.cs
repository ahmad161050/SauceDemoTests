using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class CartPage
    {
        private readonly IPage page;

        // 🔒 Locators
        private readonly ILocator checkoutButton;

        // 🧠 Constructor initializes locators once
        public CartPage(IPage page)
        {
            this.page = page;
            checkoutButton = page.Locator("#checkout");
        }

        public async Task ClickCheckoutButtonAsync()
        {
            await checkoutButton.ClickAsync();
        }
    }
}
