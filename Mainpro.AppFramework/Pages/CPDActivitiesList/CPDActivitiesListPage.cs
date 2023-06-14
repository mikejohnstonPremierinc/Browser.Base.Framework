using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public class CPDActivitiesListPage : MainproPage, IDisposable
    {
        #region constructors
        public CPDActivitiesListPage(IWebDriver driver) : base(driver)
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

        public IWebElement ActTbl { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTbl); } }
        public IWebElement ActTblHdr { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblHdr); } }
        public IWebElement ActTblBody { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblBody); } }
        public IWebElement ActTblBodyFirstRow { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblBodyFirstRow); } }
        public SelectElement ActTblActivityDateSelElem { get { return new SelectElement(this.FindElement(Bys.CPDActivitiesListPage.ActTblActivityDateSelElem)); } }
        public SelectElement ActTblCycleSelElem { get { return new SelectElement(this.FindElement(Bys.CPDActivitiesListPage.ActTblCycleSelElem)); } }
        public SelectElement ActTblStatusSelElem { get { return new SelectElement(this.FindElement(Bys.CPDActivitiesListPage.ActTblStatusSelElem)); } }
        public SelectElement ActTblPerformanceGoalSelElem { get { return new SelectElement(this.FindElement(Bys.CPDActivitiesListPage.ActTblPerformanceGoalSelElem)); } }

        public IWebElement ActTblActivityDateSelElemBtn { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblActivityDateSelElemBtn); } }
        public IWebElement ActTblCycleSelElemBtn { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblCycleSelElemBtn); } }
        public IWebElement ActTblStatusSelElemBtn { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblStatusSelElemBtn); } }
        public IWebElement ActTblPerformanceGoalSelElemBtn { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblPerformanceGoalSelElemBtn); } }
        public IWebElement ActTblActivityColHdr { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblActivityColHdr); } }
        public IWebElement ActTblCreditsReportedColHdr { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblCreditsReportedColHdr); } }
        public IWebElement ActTblLastUpdatedColHdr { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblLastUpdatedColHdr); } }
        public IWebElement ActTblActivityDateColHdr { get { return this.FindElement(Bys.CPDActivitiesListPage.ActTblActivityDateColHdr); } }


        #endregion elements

        #region methods: per page

        public override void WaitForInitialize()
        {
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CPDActivitiesListPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CPDActivitiesListPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
        }

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.CPDActivitiesListPage.ActTblActivityColHdr))
            {
                if (elem.GetAttribute("outerHTML") == ActTblActivityColHdr.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
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

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="SelectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement SelectElement, string selection)
        {
            if (ActTblActivityDateSelElem.Options[1].Text == SelectElement.Options[1].Text)
            {
                if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                    BrowserNames.Firefox)
                {
                    ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ActTblActivityDateSelElemBtn, selection);
                }
                else
                {
                    ActTblActivityDateSelElem.SelectByText(selection);
                }
                this.WaitForInitialize();
                return null;
            }

            if (ActTblCycleSelElem.Options[1].Text == SelectElement.Options[1].Text)
            {
                if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                    BrowserNames.Firefox)
                {
                    ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ActTblCycleSelElemBtn, selection);
                }
                else
                {
                    ActTblCycleSelElem.SelectByText(selection);
                }
                this.WaitForInitialize();
                return null;
            }

            if (ActTblStatusSelElem.Options[1].Text == SelectElement.Options[1].Text)
            {
                if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                    BrowserNames.Firefox)
                {
                    ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ActTblStatusSelElemBtn, selection);
                }
                else
                {
                    ActTblStatusSelElem.SelectByText(selection);
                }
                this.WaitForInitialize();
                return null;
            }

            if (ActTblPerformanceGoalSelElem.Options[1].Text == SelectElement.Options[1].Text)
            {
                if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                    BrowserNames.Firefox)
                {
                    ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ActTblPerformanceGoalSelElemBtn, selection);
                }
                else
                {
                    ActTblPerformanceGoalSelElem.SelectByText(selection);
                }
                this.WaitForInitialize();
                return null;
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element."));
        }


        #endregion methods: per page

        #region methods: page specific

        //This function gets the row count for the peble on the CPD Activities List page
        public int getRowCount()
        {
             
            int rowCount = ActTblBody.FindElements(By.TagName("tr")).Count;
            //because each row contains 2 TR elements we must divide this by 2 
            //to get the exact count
            rowCount = rowCount / 2;
            return rowCount;
        }

        public void SelectItemInCustomDropDownIE(IWebElement ItemToSelect, string textOfItem)
        {
            IWebElement itemToSelectInDropdown = null;
            //ElemSet.ClickAfterScroll(Browser, cycleSelArrw);
            ElemSet.ScrollToElement(Browser,ItemToSelect);
            Browser.ExecuteScript("arguments[0].click();", ItemToSelect);
            Thread.Sleep(1500);
            string xpathStringForSpanTag = string.Format("//span[text()='{0}']", textOfItem);
            string xpathStringForBTag = string.Format("//b[text()='{0}']", textOfItem);
            string xpathStringForLiTag = string.Format("//li[text()='{0}']", textOfItem);

            if (Browser.FindElements(By.XPath(xpathStringForSpanTag)).Count > 0)
            {
                itemToSelectInDropdown = Browser.WaitForElement(By.XPath(xpathStringForSpanTag), ElementCriteria.IsEnabled);
            }
            else if (Browser.FindElements(By.XPath(xpathStringForBTag)).Count > 0)
            {

                itemToSelectInDropdown = Browser.WaitForElement(By.XPath(xpathStringForBTag), ElementCriteria.IsEnabled);
            }
            else if (Browser.FindElements(By.XPath(xpathStringForLiTag)).Count > 0)
            {

                itemToSelectInDropdown = Browser.WaitForElement(By.XPath(xpathStringForLiTag), ElementCriteria.IsEnabled);

            }

            Browser.ExecuteScript("arguments[0].click();", itemToSelectInDropdown);

        }

        //this function determines if it is the first week of the month
        public bool IsFirstWeekOfTheMonth()
        {
            if (DateTime.Today.Day > 7) {
                return false;
            }
            DateTime dt = DateTime.Today;
            while (dt.DayOfWeek != DayOfWeek.Saturday) {
                dt = dt.AddDays(1);
            }
            if (dt.Day <= 7) {
                return true;
            }


            return false;
        }



        #endregion methods: page specific
    }
}
