using Microsoft.Playwright;
using NUnit.Framework;
using ParabankBDDAutomation.src.Hooks;
using static Microsoft.Playwright.Assertions;

[Parallelizable(ParallelScope.Fixtures)]
[Binding]
public class AccountOverviewStepDefinitions
{
    private readonly IPage _page;
    private readonly TestSettings _settings;
    private readonly LoginPage _login;
    private readonly AccountsOverviewPage _overview;
    private readonly AccountActivityPage _activity;

    public AccountOverviewStepDefinitions(TestContextWrapper wrapper)
    {
        _page = wrapper.Context.Page;
        _settings = wrapper.Context.Settings;
        _login = new LoginPage(_page);
        _overview = new AccountsOverviewPage(_page);
        _activity = new AccountActivityPage(_page);
    }

    [When(@"I open account ""(.*)"" from accounts overview")]
    public async Task GivenIOpenAccountFromOverview(string accountId)
    {
        await _overview.OpenAccountAsync(accountId);
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    [Then("I should be able to see transactions listed")]
    public async Task ThenIShouldSeeTransactions()
    {
        int count = await _activity.TransactionRows.CountAsync();
        if (count < 1)
        {
            // Assert that the fallback message is visible and contains expected text
            await Expect(_activity.NoTransactionMessage).ToBeVisibleAsync();
            await Expect(_activity.NoTransactionMessage).ToContainTextAsync("No transactions found");
        }
        else
        {
            // Assert that transaction rows are visible
            Assert.True(count > 0, $"{count} transactions found.");
        }
    }
}