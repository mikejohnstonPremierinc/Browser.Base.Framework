using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMSAdmin.AppFramework
{
    public class MyDashboardPage : Page, IDisposable
    {
        #region constructors
        public MyDashboardPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Apps/CommandCenter/Dashboard/Dashboard.aspx"; } }

        #endregion properties

        #region elements

        public IWebElement MyDashboardsLbl { get { return this.FindElement(Bys.MyDashboardPage.MyDashboardsLbl); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(240), Criteria.MyDashboardPage.PageReady);
            Thread.Sleep(0300);
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
        /// Enters text into the top left hand Search field, clicks Go, then waits for the Search Results page to load
        /// </summary>
        /// <param name="textToEnter">The text you want to enter into the Search field</param>
        /// <returns></returns>
        public SearchResultsPage Search(string textToEnter)
        {
            SearchTxt.Clear();
            SearchTxt.SendKeys(textToEnter);
            SearchBtn.Click();

            SearchResultsPage page = new SearchResultsPage(Browser);

            // 2/15/18: Today the 'Publish' test failed on IE. I looked at the screenshot and it looked like the
            // Go button did not get clicked. So I am adding a try catch to click again if the first click failed
            try
            {
                page.WaitForInitialize();
            }
            catch
            {
                SearchBtn.Click();
                page.WaitForInitialize();
            }

            return page;
        }




        #endregion methods: page specific



    }
}