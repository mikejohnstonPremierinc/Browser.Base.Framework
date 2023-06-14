﻿using Browser.Core.Framework;

namespace LMSAdmin.AppFramework
{
    public class LoginPageCriteria
    {
        public readonly ICriteria<LoginPage> UsernameVisible = new Criteria<LoginPage>(p =>
        {
            return p.Exists(Bys.LoginPage.UserNameTxt, ElementCriteria.IsVisible);

        }, "Username text box  visible");

        public readonly ICriteria<LoginPage> PasswordEnabled = new Criteria<LoginPage>(p =>
        {
            return p.Exists(Bys.LoginPage.PasswordTxt, ElementCriteria.IsEnabled);

        }, "Password is enabled");

        public readonly ICriteria<LoginPage> PageReady;

        public LoginPageCriteria()
        {
            PageReady = UsernameVisible.AND(PasswordEnabled);
        }
    }
}
