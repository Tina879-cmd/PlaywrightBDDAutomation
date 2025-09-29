using Microsoft.Playwright;
using NUnit.Framework;
using ParabankBDDAutomation.src.Hooks;
using static Microsoft.Playwright.Assertions;

[Parallelizable(ParallelScope.Fixtures)]
[Binding]
public class LoginStepDefinitions
{
    private readonly IPage _page;
    private readonly TestSettings _settings;
    private readonly LoginPage _login;
    private readonly AccountsOverviewPage _overview;

    public LoginStepDefinitions(TestContextWrapper wrapper)
    {
        _page = wrapper.Context.Page;
        _settings = wrapper.Context.Settings;
        _login = new LoginPage(_page);
        _overview = new AccountsOverviewPage(_page);
    }

    [Given("I navigate to the login page")]
    public async Task GivenINavigateToLoginPage()
    {
        await _login.GotoAsync(_settings.BaseUrl);
        await Expect(_page).ToHaveURLAsync(_settings.BaseUrl);
    }

    [When(@"I log in with username ""(.*)"" and password ""(.*)""")]
    public async Task WhenILogInWithCredentials(string username, string password)
    {
        await _login.LoginAsync(username, password);
    }

    [Given("Logged in successfully with valid credentials")]
    [When("Logged in successfully with valid credentials")]
    public async Task ValidCredentialsLogin()
    {
        var username = _settings.Credentials.Username;
        var password = _settings.Credentials.Password;
        await _login.GotoAsync(_settings.BaseUrl);
        await _login.LoginAsync(username, password);
        await Expect(_overview.Header).ToBeVisibleAsync();
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Then("I should be able to view the accounts overview")]
    public async Task ThenIShouldBeRedirectedToAccountsOverview()
    {
        await Expect(_overview.AccountsTable).ToBeVisibleAsync();
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Then(@"I should be able to view a login error message ""(.*)""")]
    public async Task ThenSeeError(string expected)
    {
        await Expect(_login.ErrorMessage).ToContainTextAsync(expected);
    }
}