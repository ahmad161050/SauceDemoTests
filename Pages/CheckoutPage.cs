// Encapsulates actions and validations on the SauceDemo checkout page.
// Supports input filling, total price capture, and final order confirmation.

using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace SauceDemoTests.Pages
{
    public class CheckoutPage
    {
        private readonly IPage page;

        // Locators for checkout form fields and controls
        private readonly ILocator firstNameInput;
        private readonly ILocator lastNameInput;
        private readonly ILocator postalCodeInput;
        private readonly ILocator continueButton;
        private readonly ILocator finishButton;
        private readonly ILocator orderCompleteHeader;
        private readonly ILocator totalPriceLabel;

        // Constructor initializes all necessary locators once
        public CheckoutPage(IPage page)
        {
            this.page = page;

            firstNameInput = page.Locator("#first-name");
            lastNameInput = page.Locator("#last-name");
            postalCodeInput = page.Locator("#postal-code");
            continueButton = page.Locator("#continue");
            finishButton = page.Locator("#finish");
            orderCompleteHeader = page.Locator(".complete-header");
            totalPriceLabel = page.Locator(".summary_total_label");
        }

        // Fills in the checkout form fields
        public async Task FillCheckoutInfoAsync(string firstName, string lastName, string postalCode)
        {
            await firstNameInput.FillAsync(firstName);
            await lastNameInput.FillAsync(lastName);
            await postalCodeInput.FillAsync(postalCode);
        }

        // Clicks the Continue button
        public async Task ClickContinueButtonAsync()
        {
            await continueButton.ClickAsync();
        }

        // Clicks the Finish button to complete checkout
        public async Task ClickFinishButtonAsync()
        {
            await finishButton.ClickAsync();
        }

        // Validates if the order confirmation message is visible and correct
        public async Task<bool> IsOrderCompleteMessageVisibleAsync()
        {
            if (!await orderCompleteHeader.IsVisibleAsync())
                return false;

            var text = await orderCompleteHeader.InnerTextAsync();
            return text.Trim() == "Thank you for your order!";
        }

        // Extracts the total price displayed during checkout
        public async Task<decimal> GetTotalPriceAsync()
        {
            var totalText = await totalPriceLabel.InnerTextAsync(); // e.g., "Total: $32.39"
            var match = Regex.Match(totalText, @"Total:\s*\$(\d+\.\d{2})");

            return match.Success ? decimal.Parse(match.Groups[1].Value) : 0;
        }
    }
}
