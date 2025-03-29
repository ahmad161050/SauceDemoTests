// -----------------------------------------------------------------------------
// This file contains global setup and teardown logic for each scenario in the
// test suite using SpecFlow's [BeforeScenario] and [AfterScenario] hooks.
// It handles launching the browser before each test and disposing it afterward.
// If a scenario fails, a screenshot is automatically captured and logged.
// -----------------------------------------------------------------------------

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using SauceDemoTests.Utils;

namespace SauceDemoTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;
        private IBrowserContext _context = null!;
        private IPage _page = null!;

        // Store SpecFlow's scenario context for dependency injection and scenario-level state
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// This method runs before every test scenario. It:
        /// - Launches Playwright Chromium browser
        /// - Opens a new context and page
        /// - Stores the page instance in ScenarioContext
        /// </summary>
        [BeforeScenario]
        public async Task SetUp()
        {
            _playwright = await Playwright.CreateAsync();

            // Launch browser in headed mode for debugging purposes
            _browser = await _playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { Headless = false });

            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();

            // Save the page instance for use in step definitions
            _scenarioContext["Page"] = _page;

            Logger.Info("✅ Browser launched and page initialized.");
        }

        /// <summary>
        /// This method runs after every scenario. It:
        /// - Captures screenshot on failure
        /// - Logs failure details
        /// - Closes the browser and disposes Playwright
        /// </summary>
        [AfterScenario]
        public async Task TearDown()
        {
            var page = _scenarioContext["Page"] as IPage;

            // Capture a screenshot if the scenario failed
            if (_scenarioContext.TestError != null && page != null)
            {
                var screenshotsDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "TestResults", "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var safeTitle = string.Join("_", _scenarioContext.ScenarioInfo.Title.Split(Path.GetInvalidFileNameChars()));
                var screenshotPath = Path.Combine(screenshotsDir, $"{safeTitle}_{timestamp}.png");

                try
                {
                    await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded); // Ensure content loaded
                    await Task.Delay(500); // Small wait for UI to settle
                    await page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                    Logger.Info($"📸 Screenshot saved at: {screenshotPath}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"❌ Screenshot capture failed: {ex.Message}");
                }

                Logger.Error($"❌ Test failed: {_scenarioContext.ScenarioInfo.Title}");
            }

            // Clean up browser resources after each test
            await _browser.CloseAsync();
            _playwright.Dispose();

            Logger.Info("🚪 Browser closed and resources disposed.");
        }
    }
}
