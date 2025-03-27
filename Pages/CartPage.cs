using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class CartPage
    {
        private readonly IPage page;

        public CartPage(IPage page) => this.page = page;

        public async Task ClickCheckoutButtonAsync()
        {
            await page.ClickAsync("#checkout");
        }
    }
}

