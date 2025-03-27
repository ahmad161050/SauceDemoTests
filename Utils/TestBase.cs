using Microsoft.Playwright;
using TechTalk.SpecFlow;
using SauceDemoTests.Utils;

namespace SauceDemoTests
{
    public class TestBase
    {
        protected IPage Page = null!;
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;
        private IBrowserContext _context = null!;

        [BeforeScenario]
        public async Task SetUp()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            _context = await _browser.NewContextAsync();
            Page = await _context.NewPageAsync();

            Logger.Info("Browser launched and page initialized.");
        }

        [AfterScenario]
        public async Task TearDown(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                var screenshotsDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "TestResults", "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var safeTitle = string.Join("_", scenarioContext.ScenarioInfo.Title.Split(Path.GetInvalidFileNameChars()));
                var screenshotPath = Path.Combine(screenshotsDir, $"{safeTitle}_{timestamp}.png");

                try
                {
                    // ✅ Wait for at least one visible element (e.g., body or html)
                    await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                    await Page.WaitForSelectorAsync("body", new PageWaitForSelectorOptions { Timeout = 5000 });

                    // ✅ Add a short pause to allow full rendering
                    await Task.Delay(1000);

                    await Page.ScreenshotAsync(new PageScreenshotOptions
                    {
                        Path = screenshotPath,
                        FullPage = true
                    });

                    Logger.Info($"📸 Screenshot saved at: {screenshotPath}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"❌ Screenshot capture failed: {ex.Message}");
                }

                Logger.Error($"Test failed: {scenarioContext.ScenarioInfo.Title}");
            }

            await _browser.CloseAsync();
            _playwright.Dispose();
            Logger.Info("Browser closed and resources disposed.");
        }
    }
}
