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
    /// This class represent the Help page for the HTML website. It contains all elements and methods that appear on that page. 
    /// It extends the Base Page class (WikipediaBasePage), which allows us to use the Base page elements and methods as well whenever 
    /// we have an instance of this Page class object.
    /// </summary>
    public class HelpPage : WikipediaBasePage, IDisposable
    {
        #region constructors
        /// <summary>
        /// This constructor is needed inside every Page class
        /// </summary>
        /// <param name="driver"></param>
        public HelpPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        // We dont use log4net, so this is just a placeholder in case we ever do
        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        // Each page has a page-specific URL, so we need to define it here. When the code navigates to
        // each page, it uses the base URL from App.Config-><environmentname>.config (or appsettings.<environmentname>.json in .NET Core), then
        // appends the page-specific URL onto the end of it
        public override string PageUrl { get { return "/Help:Contents"; } }

        #endregion properties

        #region elements

        /// <summary>
        /// We are retreiving these elements from the PageBy class. Specifically, <see cref="HelpPageBys"/>. That class is where we locate all elements
        /// by using the By type (xpath, id's, class name, linktext, etc.). Once you locate a new element inside a PageBy class, you then need to return
        /// it inside the respective Page class, as shown below.
        /// </summary>
        public IWebElement TableOfContentsTblBdy { get { return this.FindElement(Bys.HelpPage.TableOfContentsTblBdy); } }
        public IWebElement TableOfContentsTbl { get { return this.FindElement(Bys.HelpPage.TableOfContentsTbl); } }
        public IWebElement TableOfContentsTblHdr { get { return this.FindElement(Bys.HelpPage.TableOfContentsTblHdr); } }
        public IWebElement TableOfContentsTblFirstRow { get { return this.FindElement(Bys.HelpPage.TableOfContentsTblFirstRow); } }

        #endregion elements

        #region methods: added to all pages

        /// <summary>
        /// This method should be included in every page class, except for the Base page class. It represents Wait Criteria that needs to be met for
        /// the entire page to load. <see cref="HelpPageCriteria"/> for explanations of Wait Criteria, and to see what the PageReady
        /// property (the wait criteria defined in this method) represents.
        /// </summary>
        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(15), Criteria.HelpPage.PageReady);
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose HelpPage", activeRequests.Count, ex); }
        }

        #endregion methods: added to all pages

        #region methods: page specific


        #endregion methods: page specific



    }
}