using Microsoft.Playwright;
using SauceDemoTests.Pages;

namespace SauceDemoTests.Helpers
{
    public class CheckoutHelper
    {
        private readonly IPage _page;

        public CheckoutHelper(IPage page)
        {
            _page = page;
        }

        public async Task<decimal> CompleteCheckoutAsync(string productName, string firstName, string lastName, string postalCode)
        {
            var homePage = new HomePage(_page);
            var cartPage = new CartPage(_page);
            var checkoutPage = new CheckoutPage(_page);

            await homePage.AddBackpackToCartAsync();
            await homePage.GoToCartAsync();

            await cartPage.ClickCheckoutButtonAsync();

            await checkoutPage.FillCheckoutInfoAsync(firstName, lastName, postalCode);
            await checkoutPage.ClickContinueButtonAsync();

            // Get total price BEFORE clicking Finish
            decimal price = await checkoutPage.GetTotalPriceAsync();

            await checkoutPage.ClickFinishButtonAsync();

            return price;
        }

    }
}
