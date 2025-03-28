using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class HomePage
    {
        private readonly IPage page;

        public HomePage(IPage page) => this.page = page;

        public async Task AddBackpackToCartAsync()
        {
            await page.ClickAsync("#add-to-cart-sauce-labs-backpack");
        }

        public async Task GoToCartAsync()
        {
            await page.ClickAsync(".shopping_cart_link");
        }

        // ✅ Opens the hamburger menu
        public async Task OpenMenuAsync()
        {
            await page.ClickAsync("#react-burger-menu-btn");
        }

        // ✅ Clicks the logout option in sidebar
        public async Task LogoutAsync()
        {
            await OpenMenuAsync(); // Ensure menu is open
            await page.ClickAsync("#logout_sidebar_link");
        }
    }
}
