using Microsoft.Playwright;

public class LoginPage
   {
        private readonly IPage _page;
        public LoginPage(IPage page) => _page = page;

        private ILocator Username => _page.Locator("input[name='username']");
        private ILocator Password => _page.Locator("input[name='password']");
        private ILocator LoginButton => _page.Locator("input[value='Log In']");
        private ILocator Error => _page.Locator("#rightPanel .error");

        public async Task GotoAsync(string baseUrl) => await _page.GotoAsync(baseUrl);
        public async Task LoginAsync(string user, string pass)
        {
            await Username.FillAsync(user);
            await Password.FillAsync(pass);
            await LoginButton.ClickAsync();
        }
        public ILocator ErrorMessage => Error;
}

