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
    public class SearchResultsPage : Page, IDisposable
    {
        #region constructors
        public SearchResultsPage(IWebDriver driver) : base(driver)
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

        public IWebElement ActivitiesTbl { get { return this.FindElement(Bys.SearchResultsPage.ActivitiesTbl); } }
        public IWebElement ActivitiesTblBody { get { return this.FindElement(Bys.SearchResultsPage.ActivitiesTblBody); } }
        public IWebElement ActivitiesTblBodyRow { get { return this.FindElement(Bys.SearchResultsPage.ActivitiesTblBodyRow); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
           this.WaitUntil(TimeSpan.FromSeconds(240), Criteria.SearchResultsPage.PageReady);
            Thread.Sleep(1500);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose SearchResultsPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Clicks on the pencil icon of a user-specified activity and then waits for the main activity page to looad
        /// </summary>
        /// <param name="activityName">The full name of the activity</param>
        /// <returns></returns>
        public ActMainPage GoToActivity(string activityName)
        {
            IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowName(ActivitiesTbl, Bys.SearchResultsPage.ActivitiesTblBodyRow,
                activityName, "td");

            ActMainPage page = new ActMainPage(Browser);

            // 8/10/18: Test failed. Screenshot showed it was still on the Search Results page after clicking the Edit button. Adding a Try Catch to
            // click twice, second time clicking with javascript
            try
            {
                ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "img", "Edit");
                page.WaitForInitialize();
            }
            catch (Exception)
            {
                IWebElement btn = ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "img", "Edit", true, Browser);
                btn.ClickJS(Browser);
                page.WaitForInitialize();
            }

            Thread.Sleep(500);
            return page;
        }


        #endregion methods: page specific



    }
}