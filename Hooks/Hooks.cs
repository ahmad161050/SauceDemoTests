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

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task SetUp()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();

            _scenarioContext["Page"] = _page;
            Logger.Info("✅ Browser launched and page initialized.");
        }

        [AfterScenario]
        public async Task TearDown()
        {
            var page = _scenarioContext["Page"] as IPage;

            if (_scenarioContext.TestError != null && page != null)
            {
                var screenshotsDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "TestResults", "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var safeTitle = string.Join("_", _scenarioContext.ScenarioInfo.Title.Split(Path.GetInvalidFileNameChars()));
                var screenshotPath = Path.Combine(screenshotsDir, $"{safeTitle}_{timestamp}.png");

                try
                {
                    await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                    await Task.Delay(500);
                    await page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
                    Logger.Info($"📸 Screenshot saved at: {screenshotPath}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"❌ Screenshot capture failed: {ex.Message}");
                }

                Logger.Error($"❌ Test failed: {_scenarioContext.ScenarioInfo.Title}");
            }

            await _browser.CloseAsync();
            _playwright.Dispose();
            Logger.Info("🚪 Browser closed and resources disposed.");
        }
    }
}
