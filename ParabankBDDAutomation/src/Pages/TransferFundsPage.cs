using Microsoft.Playwright;

public class TransferFundsPage
{
    private readonly IPage _page;
    public TransferFundsPage(IPage page) => _page = page;

    private ILocator TransferFundsLink => _page.Locator("a[href*='transfer.htm']");
    private ILocator Amount => _page.Locator("input[id='amount']");
    private ILocator FromSelect => _page.Locator("select[id='fromAccountId']");
    private ILocator ToSelect => _page.Locator("select[id='toAccountId']");
    private ILocator TransferButton => _page.Locator("input[value='Transfer']");
    private ILocator Confirmation => _page.Locator("#showResult h1");

    public async Task NavigateAsync() => await TransferFundsLink.ClickAsync();
    public async Task TransferAsync(string amount, string fromAccount, string toAccount)
    {
        await Amount.FillAsync(amount);
        await FromSelect.SelectOptionAsync(fromAccount);
        await ToSelect.SelectOptionAsync(toAccount);
        await TransferButton.ClickAsync();
    }
    public ILocator ConfirmationMessage => Confirmation;
}
