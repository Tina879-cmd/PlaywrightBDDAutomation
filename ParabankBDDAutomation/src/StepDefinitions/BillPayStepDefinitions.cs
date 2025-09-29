using Microsoft.Playwright;
using NUnit.Framework;
using ParabankBDDAutomation.src.Hooks;
using static Microsoft.Playwright.Assertions;

[Parallelizable(ParallelScope.Fixtures)]
[Binding]
public class BillPaySteps
{
    private readonly IPage _page;
    private readonly BillPayPage _bill;
    private readonly LoginPage _login;
    private readonly TestSettings _settings;

    public BillPaySteps(TestContextWrapper wrapper)
    {
        _page = wrapper.Context.Page;
        _settings = wrapper.Context.Settings;
        _bill = new BillPayPage(_page);
        _login = new LoginPage(_page);
    }

    [Given("I navigate to the Bill Pay page")]
    public async Task GivenOnBillPay()
    {
        await _bill.NavigateAsync();
    }

    [Given("I fill payee details:")]
    public async Task GivenFillPayee(Table table)
    {
        var r = table.Rows[0];
        await _bill.FillPayeeAsync(r["Name"], r["Address"], r["City"], r["State"], r["Zip"], r["Phone"], r["Account"]);
    }

    [When(@"I pay an amount of (.*) from account ""(.*)""")]
    public async Task WhenPay(string amount, string from) => await _bill.PayAsync(amount, from);

    [Then(@"I should be able to view a confirmation ""(.*)""")]
    public async Task ThenSeeConfirmation(string expected)
    {
        await Expect(_bill.ConfirmationMessage).ToContainTextAsync(expected);
    }
}