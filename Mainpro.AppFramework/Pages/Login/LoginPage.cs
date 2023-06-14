using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public class LoginPage : MainproPage, IDisposable
    {
        #region constructors
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        #endregion properties

        #region elements

        public IWebElement UserNameTxt { get { return this.FindElement(Bys.LoginPage.UserNameTxt); } }
        public IWebElement UserNameWarningLbl { get { return this.FindElement(Bys.LoginPage.UserNameWarningLbl); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.LoginPage.PasswordTxt); } }
        public IWebElement LoginBtn { get { return this.FindElement(Bys.LoginPage.LoginBtn); } }

        public IWebElement iAcceptBtn { get { return this.FindElement(Bys.LoginPage.iAcceptBtn); } }
        public IWebElement PasswordWarningLbl { get { return this.FindElement(Bys.LoginPage.PasswordWarningLbl); } }
        public IWebElement RememberMeChk { get { return this.FindElement(Bys.LoginPage.RememberMeChk); } }
        public IWebElement ForgotPasswordLnk { get { return this.FindElement(Bys.LoginPage.ForgotPasswordLnk); } }
        public IWebElement LoginUnsuccessfullWarningLbl { get { return this.FindElement(Bys.LoginPage.LoginUnsuccessfullWarningLbl); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.LoginPage.PageReady);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LoginPage", activeRequests.Count, ex); }
        }

        #endregion methods: per page

        #region methods: page specific

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.LoginPage.LoginBtn))
            {
                if (elem.GetAttribute("outerHTML") == LoginBtn.GetAttribute("outerHTML"))
                {
                    throw new Exception("You can not use this button in this method, as there are different waiting conditions for a new " +
                        "user versus an existing user. Please use the Login method on this page");
                }
            }

            if (Browser.Exists(Bys.LoginPage.iAcceptBtn))
            {
                if (elem.GetAttribute("outerHTML") == iAcceptBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    DashboardPage Page = new DashboardPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Enters text in the username and password field, clicks the login button, and waits for the home page to load
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password">(Optional). Default = test</param>
        /// <param name="isNewUser">(Optional) Default = false</param>
        public DashboardPage Login(string username, string password = null, bool isNewUser = false)
        {
            MainproHelperMethods Help = new MainproHelperMethods();

            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(username);
            password = string.IsNullOrEmpty(password) ? "test" : password;
            PasswordTxt.SendKeys(password);
            PasswordTxt.SendKeys(Keys.Tab);
            LoginBtn.Click(Browser);

            if (isNewUser)
            {
                Browser.WaitForElement(Bys.LoginPage.iAcceptBtn, ElementCriteria.IsVisible);
                Thread.Sleep(1000);
                Browser.WaitForElement(Bys.LoginPage.iAcceptBtn, ElementCriteria.IsVisible);
                ClickAndWait(iAcceptBtn);
            }

            DashboardPage Page = new DashboardPage(Browser);
            Page.WaitForInitialize();
            Help.SwitchToRewriteAfterLoggingIn(Browser);

            DashboardPage page = new DashboardPage(Browser);
            page.WaitForInitialize();

            Console.Out.WriteLine(String.Format
                ("Tested User Details : [Username:{0}] [Password: {1}]", username, password));
            return page;
        }
 
        #endregion methods: page specific



    }
}
