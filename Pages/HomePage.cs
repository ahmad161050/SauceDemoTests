using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class HomePage
    {
        private readonly IPage page;

        // 🔒 Locators
        private readonly ILocator backpackAddToCartButton;
        private readonly ILocator cartLink;
        private readonly ILocator menuButton;
        private readonly ILocator logoutButton;

        // 🧠 Constructor initializes locators once
        public HomePage(IPage page)
        {
            this.page = page;

            backpackAddToCartButton = page.Locator("#add-to-cart-sauce-labs-backpack");
            cartLink = page.Locator(".shopping_cart_link");
            menuButton = page.Locator("#react-burger-menu-btn");
            logoutButton = page.Locator("#logout_sidebar_link");
        }

        public async Task AddBackpackToCartAsync()
        {
            await backpackAddToCartButton.ClickAsync();
        }

        public async Task GoToCartAsync()
        {
            await cartLink.ClickAsync();
        }

        public async Task OpenMenuAsync()
        {
            await menuButton.ClickAsync();
        }

        public async Task LogoutAsync()
        {
            await OpenMenuAsync();
            await logoutButton.ClickAsync();
        }
    }
}
