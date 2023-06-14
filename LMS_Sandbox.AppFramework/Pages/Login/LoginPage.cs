using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMS.AppFramework.LMSHelperMethods;
using System.Globalization;
using AventStack.ExtentReports;

namespace LMS.AppFramework
{
    public class LoginPage : LMSPage, IDisposable
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

        public IWebElement UserNameWarningLbl { get { return this.FindElement(Bys.LoginPage.UserNameWarningLbl); } }
        public IWebElement PasswordWarningLbl { get { return this.FindElement(Bys.LoginPage.PasswordWarningLbl); } }
        public IWebElement LoginUnsuccessfullWarningLbl { get { return this.FindElement(Bys.LoginPage.LoginUnsuccessfullWarningLbl); } }
        public IWebElement UserNameTxt { get { return this.FindElement(Bys.LoginPage.UserNameTxt); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.LoginPage.PasswordTxt); } }
        public IWebElement LoginBtn { get { return this.FindElement(Bys.LoginPage.LoginBtn); } }
        public IWebElement RememberMeChk { get { return this.FindElement(Bys.LoginPage.RememberMeChk); } }
        public IWebElement ClickHereToRegLnk { get { return this.FindElement(Bys.LoginPage.ClickHereToRegLnk); } }
        public SelectElement SelectASecurityQuesFormNewSecQuesSelElem { get { return new SelectElement(this.FindElement(Bys.LoginPage.SelectASecurityQuesFormNewSecQuesSelElem)); } }
        public IWebElement SelectASecurityQuesFormNewSecAnsTxt { get { return this.FindElement(Bys.LoginPage.SelectASecurityQuesFormNewSecAnsTxt); } }
        public IWebElement SelectASecurityQuesFormSubmitBtn { get { return this.FindElement(Bys.LoginPage.SelectASecurityQuesFormSubmitBtn); } }






        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.LoginPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
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

        #endregion methods: repeated per page

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

            if (Browser.Exists(Bys.LoginPage.SelectASecurityQuesFormSubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == SelectASecurityQuesFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    throw new Exception("You can not use this button in this method, as there are different waiting conditions for a new " +
                        "user versus an existing user. Please use the Login method on this page");
                }
            }

            if (Browser.Exists(Bys.LoginPage.ClickHereToRegLnk))
            {
                if (elem.GetAttribute("outerHTML") == ClickHereToRegLnk.GetAttribute("outerHTML"))
                {
                    ClickHereToRegLnk.Click(Browser);
                    RegistrationPage Page = new RegistrationPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Enters text in the username and password field, clicks the login button, and waits for the home page to load
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="newUser">(Default = false). If set to true, will complete Security Question form</param>
        public dynamic Login(string username, string password = null, bool newUser = false,
            bool removePasswordAndSecurityQuestionPrompt = false)
        {
            LMSHelperMethods.LMSHelperMethods Help = new LMSHelperMethods.LMSHelperMethods();

            // Set the password
            if (GetSiteCodeFromURL() == "cmeca")
            {
                password = string.IsNullOrEmpty(password) ? UserUtils.Password_AllCaps : password;
            }
            else
            {
                password = string.IsNullOrEmpty(password) ? UserUtils.Password : password;
            }

            UserNameTxt.Clear();
            PasswordTxt.Clear();
            UserNameTxt.SendKeys(username);
            PasswordTxt.SendKeys(password);
            PasswordTxt.SendKeys(Keys.Tab);

            // If its a new user, then we have to disable the security question, which sometimes appears in different environments.
            // ToDo: Get DEV to turn this off site-wide instead of running this query all the time. We also are setting the EULA to
            // not pop up for new users
            // MJ 9/13/19: Havent needed this in a long time since these popups are not appearing anymore. Probably can remove this
            // MJ 5/28/20: I needed this in DHMC. 
            if (newUser & removePasswordAndSecurityQuestionPrompt)
            {
                if (Help.EnvironmentEquals(Constants.Environments.Production))
                {
                    throw new Exception("This only works on UAT, because we dont have WRITE access on Prod DBs. If you need " +
                        "to create users on a portal in Prod which prompts you to change password, then you need to " +
                        "manually change the password to your desired password. If you are creating the standard " +
                        "automation users for this new portal, then make sure to initially set the coded password " +
                        "to something else non-standard, so that you can manually set it to the standard.");
                }
                DBUtils.SetSecurityQuestionAndPasswordToFalse(username, Constants.SiteCodes.DHMC);
                DBUtils.SetEULAToAccepted(username, Constants.SiteCodes.DHMC);
            }

            LoginBtn.ClickJS(Browser);
            // MJ : I dont know why I added this, but some AHA tests dont work when this is added, so commenting now
            // Browser.WaitForURLToContainString("lms/home", TimeSpan.FromSeconds(30));

            // Temporary for CAP/SNMMI development:
            if (Browser.Url.Contains("cap") & Help.EnvironmentEquals(Constants.Environments.Production)
                & Help.ReturnTrueIfTodaysDateComesBefore(DateTime.ParseExact("06/15/2020", "MM/dd/yyyy",
                CultureInfo.InvariantCulture)))
            {
                Thread.Sleep(2000);
                Browser.Navigate().GoToUrl("https://cap.community360.net/lms/home");
            }

            // MJ 5 21 19: This is only temporary code. I had to execute in Prod Culpepper and had to create new users and didnt have
            // database udate priveleges, so the change password window came up. Handling that window here. Can remove this next week
            // UPDATE. This actually can be used whenever we create users in Prod for new portals or existing portals, keep this.
            // 9/23/21: Firefox doesnt login immediately after Login button is clicked and so the below If statement gets 
            // executed in Firefox regardless, so it doesnt work in Firefox
            if (Browser.GetCapabilities().GetCapability("browserName").ToString() != BrowserNames.Firefox &&
            Browser.Exists(By.XPath("//td[contains(text(), 'New Password')]/..//input[@class='LoginTextBox']")))
            {
                Browser.FindElement(
                    By.XPath("//td[contains(text(), 'New Password')]/..//input[@class='LoginTextBox']")).
                    SendKeys(UserUtils.Password_AllCaps);
                Browser.FindElement(
                    By.XPath("//td[contains(text(), 'Confirm New')]/..//input[@class='LoginTextBox']")).
                    SendKeys(UserUtils.Password_AllCaps);

                if (Browser.FindElements(By.XPath("//td[contains(text(), 'New security answer')]/..//input[@type='text']")).
                    Count > 0)
                {
                    Browser.FindElement(By.XPath("//td[contains(text(), 'New security answer')]/..//input[@type='text']")).
                        SendKeys("test");
                }
                Browser.FindElement(
                    By.XPath("//input[@value=' Submit ']")).Click();
            }

            //Wait until the page URL loads
            // NOTE: When a person clicks on the Launch button from the Activity Front page without logging in, it will redirect the
            // user to the login page. When that person then logs in, he gets directed back to the Activity Front page.Because
            //of this, we have to wait for the Home page OR the Activity Front page.
            //UPDATE: AHA lands on the search results page after login, so adding that in here now
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
            wait.Until(Browser =>
            {
                return Browser.Url.Contains("activity") || Browser.Url.Contains("home") || Browser.Url.Contains("search");
            });

            Thread.Sleep(500);

            // if clicking login takes us to the Activity Front page (When a user clicks Launch without being logged in from 
            // the Preview page, this occurs.
            if (Browser.Url.Contains("activity"))
            {
                ActPreviewPage Page = new ActPreviewPage(Browser);
                Page.WaitForInitialize();
                return Page;
            }
            // Some sites go to Home page after login, some go to Search page
            // Search page redirection sites:
            else if (Constants.SitesWith_PostLoginPageIsCatalog.Any(site => GetSiteCodeFromURL().Contains(site)))
            {
                SearchPage Page = new SearchPage(Browser);
                Page.WaitForInitialize();
                return Page;
            }
            // Home page redirection sites:
            else if (Constants.SitesWith_PostLoginPageIsHome.Any(site => GetSiteCodeFromURL().Contains(site)))
            {
                HomePage HP = new HomePage(Browser);
                HP.WaitForInitialize();
                return HP;
            }

            // Custom page redirection sites:
            else if (Constants.SitesWith_PostLoginPageIsCustom.Any(site => GetSiteCodeFromURL().Contains(site)))
            {
                // Do we need a sleep here? Monitor going forward
                //Thread.Sleep(500);
                return null;
            }

            return null;
        }

        #endregion methods: page specific



    }
}