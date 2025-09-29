using Microsoft.Playwright;

public class BillPayPage
{
    private readonly IPage _page;
    public BillPayPage(IPage page) => _page = page;

    private ILocator BillPayLink => _page.Locator("a[href*='billpay.htm']");
    private ILocator Name => _page.Locator("input[name='payee.name']");
    private ILocator Address => _page.Locator("input[name='payee.address.street']");
    private ILocator City => _page.Locator("input[name='payee.address.city']");
    private ILocator State => _page.Locator("input[name='payee.address.state']");
    private ILocator Zip => _page.Locator("input[name='payee.address.zipCode']");
    private ILocator Phone => _page.Locator("input[name='payee.phoneNumber']");
    private ILocator Account => _page.Locator("input[name='payee.accountNumber']");
    private ILocator VerifyAccount => _page.Locator("input[name='verifyAccount']");
    private ILocator Amount => _page.Locator("input[name='amount']");
    private ILocator FromAccount => _page.Locator("select[name='fromAccountId']");
    private ILocator SendPayment => _page.Locator("input[value='Send Payment']");
    private ILocator Confirmation => _page.Locator("#billpayResult h1");
    public ILocator ConfirmationMessage => Confirmation;

    public async Task NavigateAsync() => await BillPayLink.ClickAsync();
    public async Task FillPayeeAsync(string name, string address, string city, string state, string zip, string phone, string account)
    {
        await Name.FillAsync(name);
        await Address.FillAsync(address);
        await City.FillAsync(city);
        await State.FillAsync(state);
        await Zip.FillAsync(zip);
        await Phone.FillAsync(phone);
        await Account.FillAsync(account);
        await VerifyAccount.FillAsync(account);
    }
    public async Task PayAsync(string amount, string fromAccount)
    {
        await Amount.FillAsync(amount);
        await FromAccount.SelectOptionAsync(fromAccount);
        await SendPayment.ClickAsync();
    }
  
}
