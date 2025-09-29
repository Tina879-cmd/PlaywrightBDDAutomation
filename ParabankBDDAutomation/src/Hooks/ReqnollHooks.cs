using ParabankBDDAutomation.src.Hooks;
using Reqnroll.BoDi;

[Binding]
public class SpecFlowHooks
{
    private readonly IObjectContainer _container;
    private TestContext _testContext;

    public SpecFlowHooks(IObjectContainer container) => _container = container;

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        var settings = ConfigLoader.Load();
        var driver = new PlaywrightDriver();
        _testContext = await driver.LaunchAsync(settings);

        if (!_container.IsRegistered<TestSettings>())
        {
            _container.RegisterInstanceAs(settings);
        }

        if (!_container.IsRegistered<TestContextWrapper>())
        {
            _container.RegisterInstanceAs(new TestContextWrapper { Context = _testContext });
        }

    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        await _testContext.BrowserContext.CloseAsync();
        await _testContext.Browser.CloseAsync();
    }
}
