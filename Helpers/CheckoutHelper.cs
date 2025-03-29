// -----------------------------------------------------------------------------
// This helper class encapsulates the full end-to-end checkout flow for a given
// product on the SauceDemo site. It interacts with the page objects and performs
// actions like adding an item to cart, filling user info, and finishing checkout.
// The goal is to reduce redundancy in tests and centralize common checkout logic.
// -----------------------------------------------------------------------------

using Microsoft.Playwright;
using SauceDemoTests.Pages;

namespace SauceDemoTests.Helpers
{
    public class CheckoutHelper
    {
        private readonly IPage _page;

        // Constructor receives Playwright's IPage instance and stores it
        public CheckoutHelper(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Executes the full checkout process:
        /// - Adds specified product to cart
        /// - Navigates to cart and proceeds through checkout steps
        /// - Fills user details
        /// - Retrieves the total price before finishing
        /// - Completes the checkout
        /// </summary>
        /// <param name="productName">Product to add (e.g. "Sauce Labs Backpack")</param>
        /// <param name="firstName">Customer first name</param>
        /// <param name="lastName">Customer last name</param>
        /// <param name="postalCode">Customer postal code</param>
        /// <returns>Total price shown before finishing checkout</returns>
        public async Task<decimal> CompleteCheckoutAsync(string productName, string firstName, string lastName, string postalCode)
        {
            var homePage = new HomePage(_page);
            var cartPage = new CartPage(_page);
            var checkoutPage = new CheckoutPage(_page);

            // Step 1: Add product to cart and go to cart
            await homePage.AddBackpackToCartAsync();      
            await homePage.GoToCartAsync();

            // Step 2: Proceed to checkout
            await cartPage.ClickCheckoutButtonAsync();

            // Step 3: Fill out customer information
            await checkoutPage.FillCheckoutInfoAsync(firstName, lastName, postalCode);
            await checkoutPage.ClickContinueButtonAsync();

            // Step 4: Capture the total price before finalizing
            decimal price = await checkoutPage.GetTotalPriceAsync();

            // Step 5: Finalize the order
            await checkoutPage.ClickFinishButtonAsync();

            // Return the price for assertion/logging
            return price;
        }
    }
}
