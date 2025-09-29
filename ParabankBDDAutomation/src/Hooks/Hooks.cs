using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using ParabankBDDAutomation.src.Hooks;

[Reqnroll.Binding]
public class Hooks
{
    private static ExtentReports _extent;
    private static ExtentTest _feature;
    private static ExtentTest _scenario;
    private readonly Reqnroll.ScenarioContext _scenarioContext;
    private readonly Reqnroll.FeatureContext _featureContext;
    private TestContext _testContext;

    public Hooks(Reqnroll.ScenarioContext scenarioContext, Reqnroll.FeatureContext featureContext)
    {
        _scenarioContext = scenarioContext;
        _featureContext = featureContext;
    }

    [Reqnroll.BeforeTestRun]
    public static void BeforeTestRun()
    {
        var rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\.."));
        var reportDir = Path.Combine(rootPath, "Reports");
        Directory.CreateDirectory(reportDir);

        var reportPath = Path.Combine(reportDir, "ExtentReport.html");
        var reporter = new ExtentSparkReporter(reportPath);

        reporter.Config.DocumentTitle = "ParaBank Automation Report";
        reporter.Config.ReportName = "BDD Execution";
        reporter.Config.Theme = Theme.Standard;

        _extent = new ExtentReports();
        _extent.AttachReporter(reporter);
        _extent.AddSystemInfo("Tester", "Tina R. Patil");
        _extent.AddSystemInfo("Environment", "QA");
        _extent.AddSystemInfo("Browser", "Chromium");
    }

    [Reqnroll.BeforeFeature]
    public static void BeforeFeature(Reqnroll.FeatureContext featureContext)
    {
        _feature = _extent.CreateTest(featureContext.FeatureInfo.Title);
    }

    [Reqnroll.BeforeScenario]
    public void BeforeScenario()
    {
        _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);
    }

    [Reqnroll.AfterStep]
    public async Task AfterStep()
    {
        try
        {
            if (_testContext == null)
            {
                if (_scenarioContext.ScenarioContainer.IsRegistered<TestContextWrapper>())
                {
                    var wrapper = _scenarioContext.ScenarioContainer.Resolve<TestContextWrapper>();
                    _testContext = wrapper?.Context;
                }
            }

            if (_testContext == null)
            {
                Console.WriteLine("TestContext is still null in AfterStep — skipping screenshot.");
                return;
            }

            var stepText = _scenarioContext.StepContext.StepInfo.Text;
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            string screenshotPath = await CaptureScreenshot(stepText);

            if (_scenarioContext.TestError == null)
            {
                _scenario.CreateNode(stepType, stepText)
                         .Pass("Step passed")
                         .AddScreenCaptureFromPath(screenshotPath);
            }
            else
            {
                _scenario.CreateNode(stepType, stepText)
                         .Fail(_scenarioContext.TestError.Message)
                         .AddScreenCaptureFromPath(screenshotPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AfterStep: {ex.Message}");
        }
    }


    [Reqnroll.AfterScenario]
    public void AfterScenario()
    {
        _testContext.BrowserContext?.CloseAsync();
    }

    [Reqnroll.AfterTestRun]
    public static void AfterTestRun()
    {
        _extent.Flush();
    }

    private async Task<string> CaptureScreenshot(string stepName)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var safeStepName = string.Join("_", stepName.Split(Path.GetInvalidFileNameChars()));
        var scenarioName = string.Join("_", _scenarioContext.ScenarioInfo.Title.Split(Path.GetInvalidFileNameChars()));

        var rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\.."));
        var screenshotDir = Path.Combine(rootPath, "Reports", "Screenshots");
        Directory.CreateDirectory(screenshotDir);

        var screenshotFileName = $"{scenarioName}_{safeStepName}_{timestamp}.png";
        var screenshotPath = Path.Combine(screenshotDir, screenshotFileName);

        // Wait for error message if scenario failed
        if (_scenarioContext.TestError != null)
        {
            try
            {
                await _testContext.Page.WaitForSelectorAsync("#rightPanel .error", new() { Timeout = 3000 });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error message not found before screenshot: {ex.Message}");
            }
        }

        await _testContext.Page.ScreenshotAsync(new() { Path = screenshotPath });

        return Path.Combine("Screenshots", screenshotFileName); // relative path for ExtentReport
    }



}
