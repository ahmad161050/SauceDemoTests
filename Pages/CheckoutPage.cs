using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class CheckoutPage
    {
        private readonly IPage page;

        public CheckoutPage(IPage page) => this.page = page;

        public async Task FillCheckoutInfoAsync(string firstName, string lastName, string postalCode)
        {
            await page.FillAsync("#first-name", firstName);
            await page.FillAsync("#last-name", lastName);
            await page.FillAsync("#postal-code", postalCode);
        }

        public async Task ClickContinueButtonAsync()
        {
            await page.ClickAsync("#continue");
        }

        public async Task ClickFinishButtonAsync()
        {
            await page.ClickAsync("#finish");
        }

        public async Task<bool> IsOrderCompleteMessageVisibleAsync()
        {
            var locator = page.Locator(".complete-header");
            if (!await locator.IsVisibleAsync())
                return false;

            var text = await locator.InnerTextAsync();
            return text.Trim() == "Thank you for your order!";
        }

    }
}
