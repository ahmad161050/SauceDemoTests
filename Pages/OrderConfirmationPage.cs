// Represents the order confirmation page displayed after a successful checkout.
// Provides access to key confirmation messages for validation.

using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SauceDemoTests.Pages
{
    /// Encapsulates behavior and assertions for the order confirmation screen.
    public class OrderConfirmationPage
    {
        private readonly IPage page;

        // Locators
        private readonly ILocator checkoutCompleteHeader;
        private readonly ILocator thankYouMessage;

        /// Initializes the page and element locators.
        public OrderConfirmationPage(IPage page)
        {
            this.page = page;

            checkoutCompleteHeader = page.Locator("span.title");              
            thankYouMessage = page.Locator("h2.complete-header");            
        }

        /// Checks if the checkout header is visible.
        public async Task<bool> IsCheckoutHeaderVisibleAsync()
        {
            return await checkoutCompleteHeader.IsVisibleAsync();
        }

        /// Checks if the thank you message is visible.
        public async Task<bool> IsThankYouMessageVisibleAsync()
        {
            return await thankYouMessage.IsVisibleAsync();
        }

        /// Gets the text of the thank you message.
        public async Task<string> GetThankYouMessageTextAsync()
        {
            return await thankYouMessage.InnerTextAsync();
        }
    }
}
