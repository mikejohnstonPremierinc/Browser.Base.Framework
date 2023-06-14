using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// This class represent the Home page for the HTML website. It contains all elements and methods that appear on that page. 
    /// It extends the Base Page class (WikipediaBasePage), which allows us to use the Base page elements and methods as well whenever 
    /// we have an instance of this Page class object.
    /// </summary>
    public class HomePage : WikipediaBasePage, IDisposable
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

        // Each page has a page-specific URL, so we need to define it here. When the code navigates to
        // each page, it uses the base URL from App.Config->Wikipedia_Environment_1.config (or appsettings.json in .NET Core), then
        // appends the page-specific URL onto the end of it
        public override string PageUrl { get { return "Main_Page"; } }

        #region elements

        /// <summary>
        /// We are retreiving these elements from the PageBy class. Specifically, <see cref="HomePageBys"/>. That class is where we 
        /// locate all elements by using the By type (xpath, id's, class name, linktext, etc.). Once you locate a new element inside 
        /// a PageBy class, you then need to return it inside the respective Page class, as shown below.
        /// </summary>
        public IWebElement MainHeaderLbl { get { return this.FindElement(Bys.HomePage.WikipediaLnk); } }

        #endregion elements

        #endregion properties


        #region methods: added to all pages

        /// <summary>
        /// This method should be included in every page class, except for the Base page class. It represents Wait Criteria that needs to be met for
        /// the entire page to load. <see cref="HomePageCriteria"/> for explanations of Wait Criteria, and to see what the PageReady
        /// property (the wait criteria defined in this method) represents.
        /// </summary>
        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.HomePage.PageReady);
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

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window/element to close or open, or a page to load,
        /// depending on the element that was clicked. Once the Wait Criteria is satisfied, the test continues, and the method returns
        /// either a new Page class instance or nothing at all (hence the 'dynamic' return type). For a thorough explanation of how 
        /// this type of method works, and how to use this method <see cref="WikipediaBasePage.ClickAndWaitBasePage(IWebElement)"/>
        /// </summary>
        /// <param name="elemToClick">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elemToClick)
        {
            if (Browser.Exists(Bys.WikipediaBasePage.HelpLnk))
            {
                if (elemToClick.GetAttribute("outerHTML") == HelpLnk.GetAttribute("outerHTML"))
                {
                    // Sometimes elements cant be clicked on unless we scroll to them.
                    // We can use the Browser.Core.Framework's static method ScrollToElement inside the ElemSet class to accomplish
                    // this. Again, this is a good example of how there is so much shared code in this framework that we can use,
                    // instead of creating a bunch of new code that would only result in duplication, which is not needed. Feel
                    // free to create/add any methods inside Browser.Core.Framework that you feel would make sense in terms of
                    // being compatible with multiple different applications. If you cant figure out exactly how to do that,
                    // ask me (Mike Johnston) and we can work on it together.
                    ElemSet.ScrollToElement(Browser, HelpLnk);
                    HelpLnk.Click();
                    HelpPage Page = new HelpPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You" +
                " either need to add this element to a new If statement, or if the element is already added, then the page you " +
                " were on did not contain the element.",
                elemToClick.GetAttribute("innerText")));

        }

        #endregion methods: added to all pages

        #region methods: page specific

        #endregion methods: page specific



    }
}