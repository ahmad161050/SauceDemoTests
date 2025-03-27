using System.Threading.Tasks;
using SauceDemoTests.Pages;

namespace SauceDemoTests.Utils
{
    public class LoginFixture : TestBase
    {
        public async Task PerformLogin(string username)
        {
            var loginPage = new LoginPage(Page);
            await loginPage.GoToAsync();
            await loginPage.EnterUsernameAsync(username);
            await loginPage.EnterPasswordAsync("secret_sauce");
            await loginPage.ClickLoginAsync();
        }
    }
}
