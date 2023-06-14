using Browser.Core.Framework;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMSAdmin.AppFramework
{
    public class LoginPage : Page, IDisposable
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

        public override string PageUrl { get { return "Apps/Authenticate/SignIn.aspx?"; } }

        #endregion properties

        #region elements

        public IWebElement UserNameTxt { get { return this.FindElement(Bys.LoginPage.UserNameTxt); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.LoginPage.PasswordTxt); } }
        public IWebElement LoginBtn { get { return this.FindElement(Bys.LoginPage.LoginBtn); } }

        public IWebElement IAcceptBtn { get { return this.FindElement(Bys.LoginPage.IAcceptBtn); } }
        public IWebElement MultiSessionContinueBtn { get { return this.FindElement(Bys.LoginPage.MultiSessionContinueBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(240), Criteria.LoginPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.LoginPage.LoginBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == LoginBtn.GetAttribute("outerHTML"))
                {
                    try
                    {
                        LoginBtn.Click();

                        try
                        {
                            Browser.WaitForElement(Bys.LoginPage.MultiSessionAlertMsg, TimeSpan.FromSeconds(10), ElementCriteria.IsVisible);
                            MultiSessionContinueBtn.Click(Browser);
                            MyDashboardPage page = new MyDashboardPage(Browser);
                            page.WaitForInitialize();
                            return page;
                        }
                        catch
                        {

                            try
                            {
                                Browser.WaitForElement(Bys.LoginPage.IAcceptBtn, TimeSpan.FromSeconds(10), ElementCriteria.IsVisible); IAcceptBtn.Click(Browser);
                                MyDashboardPage page = new MyDashboardPage(Browser);
                                page.WaitForInitialize();
                                return page;
                            }
                            catch
                            {
                                MyDashboardPage page = new MyDashboardPage(Browser);
                                page.WaitForInitialize();
                                return page;
                            }
                        }

                    }
                    catch
                    {
                        LoginBtn.Click();
                        try
                        {
                            Browser.WaitForElement(Bys.LoginPage.MultiSessionAlertMsg, TimeSpan.FromSeconds(10), ElementCriteria.IsVisible);
                            MultiSessionContinueBtn.Click(Browser);
                            MyDashboardPage page = new MyDashboardPage(Browser);
                            page.WaitForInitialize();
                            return page;
                        }
                        catch
                        {
                            try
                            {
                                Browser.WaitForElement(Bys.LoginPage.IAcceptBtn, TimeSpan.FromSeconds(10), ElementCriteria.IsVisible); IAcceptBtn.Click(Browser);
                                MyDashboardPage page = new MyDashboardPage(Browser);
                                page.WaitForInitialize();
                                return page;
                            }
                            catch
                            {
                                MyDashboardPage page = new MyDashboardPage(Browser);
                                page.WaitForInitialize();
                                return page;
                            }
                        }

                    }

                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                buttonOrLinkElem.GetAttribute("innerText")));
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

        #endregion methods: repeated per page

        #region methods: page specific


        /// <summary>
        /// Enters text in the username and password field, clicks the login button, then waits for the URL 
        /// of the Dashboard page to load
        /// </summary>
        /// <param name="userName"><see cref="Constants_LMSAdmin.LoginUserNames"/></param>
        /// <param name="password"></param>
        public MyDashboardPage Login(Constants_LMSAdmin.LoginUserNames userName, string password)
        {
            // Login with a valid user
            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(userName.GetDescription());
            PasswordTxt.SendKeys(password);
            PasswordTxt.SendKeys(Keys.Tab);
            MyDashboardPage page = ClickAndWait(LoginBtn);

            try
            {
                return page;
            }
            catch 
            {
                UserNameTxt.Clear();
                PasswordTxt.Clear();
                UserNameTxt.SendKeys(userName.GetDescription());
                PasswordTxt.SendKeys(password);
                PasswordTxt.SendKeys(Keys.Tab);
                return page;
            }         
        }

        /// <summary>
        /// Enters text in the username and password field, clicks the login button, then waits for the URL 
        /// of the Dashboard page to load
        /// </summary>
        /// <param name="userName">The LMSAdmin username</param>
        /// <param name="password"></param>
        public MyDashboardPage Login(string userName, string password)
        {
            // Login with a valid user
            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(userName);
            PasswordTxt.SendKeys(password);
            PasswordTxt.SendKeys(Keys.Tab);
            MyDashboardPage page = ClickAndWait(LoginBtn);

            try
            {
                return page;
            }
            catch
            {
                UserNameTxt.Clear();
                PasswordTxt.Clear();
                UserNameTxt.SendKeys(userName);
                PasswordTxt.SendKeys(password);
                PasswordTxt.SendKeys(Keys.Tab);
                return page;
            }

        }

        public void Login(object loginUserNames, string v1, string v2)
        {
            throw new NotImplementedException();
        }

        #endregion methods: page specific



    }
}