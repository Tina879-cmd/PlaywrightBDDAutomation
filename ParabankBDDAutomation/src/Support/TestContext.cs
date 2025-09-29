using AventStack.ExtentReports;
using Microsoft.Playwright;

public class TestContext
{
    public IPage Page { get; set; }
    public IBrowser Browser { get; set; }
    public IBrowserContext BrowserContext { get; set; }
    public TestSettings Settings { get; set; }
}
