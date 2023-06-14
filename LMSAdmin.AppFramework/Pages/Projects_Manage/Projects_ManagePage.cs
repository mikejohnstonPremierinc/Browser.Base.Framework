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
    public class Projects_ManagePage : Page, IDisposable
    {
        #region constructors
        public Projects_ManagePage(IWebDriver driver) : base(driver)
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

        public IWebElement ManageSearchBtn { get { return this.FindElement(Bys.Projects_ManagePage.ManageSearchBtn); } }
        public IWebElement ManageSearchTxt { get { return this.FindElement(Bys.Projects_ManagePage.ManageSearchTxt); } }
        public IWebElement ManageActivitiesTbl { get { return this.FindElement(Bys.Projects_ManagePage.ManageTbl); } }
        public IWebElement AddNewProjectLnk { get { return this.FindElement(Bys.Projects_ManagePage.AddNewProjectLnk); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.Projects_ManagePage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.Projects_ManagePage.ManageSearchBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SearchBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    // Adding a static wait here because LMSAdmin technology is ancient so there is nothing to wait for
                    // dynamically inside the HTML
                    Thread.Sleep(3000);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Projects_ManagePage.AddNewProjectLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddNewProjectLnk.GetAttribute("outerHTML"))
                {
                    AddNewProjectLnk.Click();
                    Browser.WaitForElement(Bys.Projects_AddEditPage.AddProjectLbl, ElementCriteria.IsVisible);
                    return null;
                }
            }

            throw new Exception("No button or link was found with your passed parameter. You either need to add this button to a new If statement, " +
                "or if the button is already added, then the page you were on did not contain the button.");
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
        /// Enters user-specified text into the Search text box, clicks Search, click the Pencil icon
        /// and waits for the Activity page to load
        /// </summary>
        /// <param name="activitySearchText">The text you want to enter in the search text box. Note that LMSAdmin has a bug where a lot of searches dont work. So you have to make your search text short</param>
        /// <param name="activityName">The full name of the activity</param>
        /// <returns></returns>
        public ActMainPage GoToEditActivity(string activitySearchText, string activityName)
        {
            ManageSearchTxt.SendKeys(activitySearchText);
            ClickAndWait(SearchBtn);

            IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowName(ManageActivitiesTbl, Bys.Projects_ManagePage.ManageTblBodyRow,
                activityName, "td");

            ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "img", "Edit");

            ActMainPage page = new ActMainPage(Browser);
            page.WaitForInitialize();

            return page;
        }

        /// <summary>
        /// Clicks the Add New Project link, selects a project type, then returns the AddEditProject page
        /// </summary>
        /// <param name="projectType">The exqact text from any of the radio buttons</param>
        /// <returns></returns>
        public Projects_AddEditPage GoToAddNewProjectPage(string projectType)
        {
            ClickAndWait(AddNewProjectLnk);
            ElemSet.RdoBtn_ClickByText(Browser, projectType);

            Projects_AddEditPage AEP = new Projects_AddEditPage(Browser);
            AEP.WaitForInitialize();
            return AEP;
        }


        #endregion methods: page specific



    }
}