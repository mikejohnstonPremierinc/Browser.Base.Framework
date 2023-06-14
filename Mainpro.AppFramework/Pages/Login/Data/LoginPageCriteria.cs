using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class LoginPageCriteria
    {
        public readonly ICriteria<LoginPage> UsernameTxtVisible = new Criteria<LoginPage>(p =>
        {
            return p.Exists(Bys.LoginPage.UserNameTxt, ElementCriteria.IsVisible);

        }, "Username text box visible");

        public readonly ICriteria<LoginPage> PasswordTxtEnabled = new Criteria<LoginPage>(p =>
        {
            return p.Exists(Bys.LoginPage.PasswordTxt, ElementCriteria.IsEnabled);

        }, "Password text box enabled");

        public readonly ICriteria<LoginPage> PageReady;

        public LoginPageCriteria()
        {
            PageReady = UsernameTxtVisible.AND(PasswordTxtEnabled);
        }
    }
}
