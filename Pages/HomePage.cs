// Represents the SauceDemo inventory (home) page. 
// Supports adding items to cart, navigating to cart, and user logout.

using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class HomePage
    {
        private readonly IPage page;

        // Locators for key home page elements
        private readonly ILocator backpackAddToCartButton;
        private readonly ILocator cartLink;
        private readonly ILocator menuButton;
        private readonly ILocator logoutButton;

        // Constructor initializes locators once to improve performance
        public HomePage(IPage page)
        {
            this.page = page;

            backpackAddToCartButton = page.Locator("#add-to-cart-sauce-labs-backpack");
            cartLink = page.Locator(".shopping_cart_link");
            menuButton = page.Locator("#react-burger-menu-btn");
            logoutButton = page.Locator("#logout_sidebar_link");
        }

        // Clicks 'Add to Cart' button for Sauce Labs Backpack
        public async Task AddBackpackToCartAsync()
        {
            await backpackAddToCartButton.ClickAsync();
        }

        // Navigates to the shopping cart page
        public async Task GoToCartAsync()
        {
            await cartLink.ClickAsync();
        }

        // Opens the hamburger menu
        public async Task OpenMenuAsync()
        {
            await menuButton.ClickAsync();
        }

        // Logs the user out via the sidebar menu
        public async Task LogoutAsync()
        {
            await OpenMenuAsync();
            await logoutButton.ClickAsync();
        }
    }
}
