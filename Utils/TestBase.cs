using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace SauceDemoTests
{
    public class TestBase
    {
        protected IPage Page;
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;

        [BeforeScenario]
        public async Task SetUp()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _context = await _browser.NewContextAsync();
            Page = await _context.NewPageAsync();
        }

        [AfterScenario]
        public async Task TearDown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
