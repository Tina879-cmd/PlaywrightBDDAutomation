using Microsoft.Playwright;

public class AccountActivityPage
{
    private readonly IPage _page;
    public AccountActivityPage(IPage page) => _page = page;

    public ILocator TransactionRows => _page.Locator("table#transactionTable tbody tr");
    public ILocator NoTransactionMessage => _page.Locator("p[id='noTransactions']");   
}