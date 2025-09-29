using Microsoft.Playwright;

public class PlaywrightDriver
{
    public async Task<TestContext> LaunchAsync(TestSettings settings)
    {
        var playwright = await Playwright.CreateAsync();
        var browser = settings.Browser.ToLower() switch
        {
            "firefox" => await playwright.Firefox.LaunchAsync(new() { Headless = true }),
            "webkit" => await playwright.Webkit.LaunchAsync(new() { Headless = true }),
            _ => await playwright.Chromium.LaunchAsync(new() { Headless = true })
        };

        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        return new TestContext
        {
            Page = page,
            Browser = browser,
            BrowserContext = context,
            Settings = settings
        };
    }
}
