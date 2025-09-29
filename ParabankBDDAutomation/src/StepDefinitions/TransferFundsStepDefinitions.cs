using Microsoft.Playwright;
using NUnit.Framework;
using ParabankBDDAutomation.src.Hooks;
using static Microsoft.Playwright.Assertions;

[Parallelizable(ParallelScope.Fixtures)]
[Binding]
public class TransferFundsSteps
{
    private readonly IPage _page;
    private readonly TestSettings _settings;
    private readonly LoginPage _login;
    private readonly TransferFundsPage _transfer;

    public TransferFundsSteps(TestContextWrapper wrapper)
    {
        _page = wrapper.Context.Page;
        _settings = wrapper.Context.Settings;
        _login = new LoginPage(_page);
        _transfer = new TransferFundsPage(_page);
    }

    [Given("I navigate to the transfer funds page")]
    public async Task GivenOnTransferPage() => await _transfer.NavigateAsync();

    [When(@"I transfer (.*) from account ""(.*)"" to account ""(.*)""")]
    public async Task WhenTransfer(string amount, string fromAccount, string toAccount)
        => await _transfer.TransferAsync(amount, fromAccount, toAccount);

    [Then(@"I should be able to view a confirmation message ""(.*)""")]
    public async Task ThenConfirm(string expected)
    {
       await Expect(_transfer.ConfirmationMessage).ToContainTextAsync(expected);
    }
}