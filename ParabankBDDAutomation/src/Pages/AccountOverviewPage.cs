using Microsoft.Playwright;

public class AccountsOverviewPage
{
    private readonly IPage _page;
    public AccountsOverviewPage(IPage page) => _page = page;

    public ILocator Header => _page.Locator("a[href*='overview.htm']");
    public ILocator AccountsTable => _page.Locator("#showOverview h1");

    public async Task<bool> IsLoadedAsync()
        => await AccountsTable.IsVisibleAsync();

    public async Task OpenAccountAsync(string accountId)
        => await _page.ClickAsync($"a[href*='activity.htm?id={accountId}']");
}
