using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMS.AppFramework
{
    /// <summary>
    /// This class represent the Home page for the UAMS website. It contains all elements and methods that appear on that page. 
    /// It extends the Base Page class (LMSPage), which allows us to use the Base page elements and methods as well whenever 
    /// we have an instance of this Page class object.
    /// </summary>
    public class HomePage : LMSPage, IDisposable
    {
        #region constructors
        /// <summary>
        /// This constructor is needed inside every Page class
        /// </summary>
        /// <param name="driver"></param>
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        // We dont use log4net, so this is just a placeholder in case we ever do
        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        // The Home page does not have a page-specific URL, so we can't define one, but the property needs to be here because that is how
        // the base Page class is defined. When the code navigates to the Home page, it uses the base URL from app.config (or appsettings.json in .NET Core)
        public override string PageUrl { get { return "lms\\home"; } }

        #endregion properties

        #region elements

        /// <summary>
        /// We are retreiving these elements from the PageBy class. Specifically, <see cref="HomePageBys"/>. That class is where we locate all elements
        /// by using the By type (xpath, id's, class name, linktext, etc.). Once you locate a new element inside a PageBy class, you then need to return
        /// it inside the respective Page class, as shown below.
        /// </summary>
        public IWebElement ActivityTable1 { get { return this.FindElement(Bys.HomePage.ActivityTable1); } }
        public IWebElement ActivityTable1_FirstLnk { get { return this.FindElement(Bys.HomePage.ActivityTable1_FirstLnk); } }


        #endregion elements

        #region methods: repeated per page

        /// <summary>
        /// This method should be included in every page class, except for the Base page class. It represents Wait Criteria that needs to be met for
        /// the entire page to load. <see cref="HomePageCriteria"/> for explanations of Wait Criteria, and to see what the PageReady
        /// property (the wait criteria defined in this method) represents.
        /// </summary>
        public override void WaitForInitialize()
        {
            // Sometimes LMS throws a generic error message saying we can not retrieve your data. Most of the time this occurs
            // when an activity becomes corrupt for whatever reason. To fix it, you have to republish the activity. 
            // However, if this happens on the Home page or another page, then it is usually just an intermittent 
            // issue and will pass the second time
            try
            {
                if (Browser.Url.Contains("cap"))
                {
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.HomePage.PageReady_SitesWithoutActivityLinks);
                }
                else
                {
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.HomePage.PageReady);
                }
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(180));
            }
            catch (Exception)
            {
                if (Browser.Exists(Bys.LMSPage.NotificationPageNoDataErrorLbl, ElementCriteria.IsVisible))
                {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
                }
                else
                {
                if (Browser.Url.Contains("cap"))
                {
                    this.WaitUntil(TimeSpan.FromSeconds(1), Criteria.HomePage.PageReady_SitesWithoutActivityLinks);
                }
                else
                {
                    this.WaitUntil(TimeSpan.FromSeconds(1), Criteria.HomePage.PageReady);
                }
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(1));
                }
            }
        }

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window/element to close or open, or a page to load,
        /// depending on the element that was clicked. Once the Wait Criteria is satisfied, the test continues, and the method returns
        /// either a new Page class instance or nothing at all (hence the 'dynamic' return type). For a thorough explanation of how this
        /// type of method works, and how to use this method <see cref="LMSPage.ClickAndWaitBasePage(IWebElement)"/>
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.LMSPage.LoginLnk))
            {
                if (elem.GetAttribute("outerHTML") == LoginLnk.GetAttribute("outerHTML"))
                {
                    LoginLnk.Click(Browser);
                    LoginPage Page = new LoginPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            if (Browser.Exists(Bys.LMSPage.RegisterLnk))
            {
                if (elem.GetAttribute("outerHTML") == RegisterLnk.GetAttribute("outerHTML"))
                {
                    RegisterLnk.Click(Browser);
                    RegistrationPage Page = new RegistrationPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }

            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        // Need to include this in every Page class
        public void Dispose()
        {
            Dispose(true);
        }

        // Need to include this in every Page class
        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose HomePage", activeRequests.Count, ex); }
        }


        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Clicks on a user-specified activity, then waits for the Activity Front or Overview page to load
        /// </summary>
        /// <param name="actName">The name of the activity</param>
        public dynamic GoToActivityPreviewPage(string actName)
        {
            IWebElement ActCard = Browser.FindElement(By.XPath(string.Format("//h4[text()='{0}']", actName)));
            ActCard.Click(Browser);

            // Wait until the page URL loads
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
            wait.Until(Browser => { return Browser.Url.Contains("activity_overview?") || Browser.Url.Contains("activity?"); });

            // If this click takes us to the Activity Overview page
            if (Browser.Url.Contains("activity_overview"))
            {
                ActOverviewPage OP = new ActOverviewPage(Browser);
                OP.WaitForInitialize();
                Thread.Sleep(300);
                return OP;
            }
            // Else if this click takes us to the Front page
            else
            {
                ActPreviewPage FP = new ActPreviewPage(Browser);
                FP.WaitForInitialize();
                Thread.Sleep(300);
                return FP;
            }
        }

        #endregion methods: page specific



    }
}