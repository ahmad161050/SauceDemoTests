using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SauceDemoTests.Pages
{
    public class OrderConfirmationPage
    {
        private readonly IPage page;

        // ✅ Locators
        private readonly ILocator checkoutCompleteHeader;
        private readonly ILocator thankYouMessage;

        public OrderConfirmationPage(IPage page)
        {
            this.page = page;
            checkoutCompleteHeader = page.Locator("span.title"); // e.g., "Checkout: Complete!"
            thankYouMessage = page.Locator("h2.complete-header"); // e.g., "Thank you for your order!"
        }

        public async Task<bool> IsCheckoutHeaderVisibleAsync()
        {
            return await checkoutCompleteHeader.IsVisibleAsync();
        }

        public async Task<bool> IsThankYouMessageVisibleAsync()
        {
            return await thankYouMessage.IsVisibleAsync();
        }

        public async Task<string> GetThankYouMessageTextAsync()
        {
            return await thankYouMessage.InnerTextAsync();
        }
    }
}
