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
    /// This class represent the Search Results page for the HTML website. It contains all elements and methods that appear on that page. 
    /// It extends the Base Page class (WikipediaBasePage), which allows us to use the Base page elements and methods as well whenever 
    /// we have an instance of this Page class object.
    /// </summary>
    public class SearchResultsPage : WikipediaBasePage, IDisposable
    {
        #region constructors
        /// <summary>
        /// This constructor is needed inside every Page class
        /// </summary>
        /// <param name="driver"></param>
        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        // We dont use log4net, so this is just a placeholder in case we ever do
        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        // Some pages do not have a static page URL, so we can't define one, but the property needs to be
        // here because that is how the base Page class is defined
        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements

        /// <summary>
        /// We are retreiving these elements from the PageBy class. Specifically, <see cref="SearchResultsPageBys"/>. That class is where we locate all elements
        /// by using the By type (xpath, id's, class name, linktext, etc.). Once you locate a new element inside a PageBy class, you then need to return it inside
        /// the respective Page class, as shown below.
        /// </summary>
        public IWebElement SearchResultsForLbl { get { return this.FindElement(Bys.SearchResultsPage.HelpContentsLnk); } }
        public IWebElement MySpreadsheetTbl { get { return this.FindElement(Bys.SearchResultsPage.MySpreadsheetTbl); } }
        public IWebElement MySpreadsheetTblHdr { get { return this.FindElement(Bys.SearchResultsPage.MySpreadsheetTblHdr); } }
        public IWebElement MySpreadsheetTblBody { get { return this.FindElement(Bys.SearchResultsPage.MySpreadsheetTblBody); } }
        public IWebElement MySpreadsheetTblBodyRow { get { return this.FindElement(Bys.SearchResultsPage.MySpreadsheetTblBodyRow); } }



        #endregion elements

        #region methods: added to all pages

        /// <summary>
        /// This method should be included in every page class, except for the Base page class. It represents Wait Criteria that needs to be met for
        /// the entire page to load. <see cref="SearchResultsPageCriteria"/> for explanations of Wait Criteria, and to see what the PageReady
        /// property (the wait criteria defined in this method) represents.
        /// </summary>
        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.SearchResultsPage.PageReady);
            // Ignore this Sleep, this is only temporary. It is not good practice to rely on sleeps
            Thread.Sleep(500);
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose SearchResultsPage", activeRequests.Count, ex); }
        }

        #endregion methods: added to all pages

        #region methods: page specific



        #endregion methods: page specific



    }
}