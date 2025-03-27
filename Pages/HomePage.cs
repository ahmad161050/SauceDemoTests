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
    }
}

