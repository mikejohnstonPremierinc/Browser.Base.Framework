using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMS.AppFramework
{
    public class SearchPage : LMSPage, IDisposable
    {
        #region constructors
        public SearchPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/#/activity"; } }

        #endregion properties

        #region elements

        public IWebElement NoDataAvailableLbl { get { return this.FindElement(Bys.SearchPage.NoDataAvailableLbl); } }
        public IWebElement SearchResultsTbl { get { return this.FindElement(Bys.SearchPage.SearchResultsTbl); } }
        public IWebElement SearchResultsTblBody { get { return this.FindElement(Bys.SearchPage.SearchResultsTblBody); } }
        public IList<IWebElement> SearchResultsTblBodyActivityLnks { get { return this.FindElements(Bys.SearchPage.SearchResultsTblBodyActivityLnks); } }

        public SelectElement CreditTypeSelElem { get { return new SelectElement(this.FindElement(Bys.SearchPage.CreditTypeSelElem)); } }
        public IWebElement CreditTypeBtn { get { return this.FindElement(Bys.SearchPage.CreditTypeBtn); } }
        public IWebElement Mobile_ShowHideFiltersBtn { get { return this.FindElement(Bys.SearchPage.Mobile_ShowHideFiltersBtn); } }



        #endregion elements

        #region methods: repeated per page

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.SearchPage.Mobile_ShowHideFiltersBtn))
            {
                if (elem.GetAttribute("outerHTML") == Mobile_ShowHideFiltersBtn.GetAttribute("outerHTML"))
                {
                    Mobile_ShowHideFiltersBtn.Click(Browser);
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string selection)
        {
            if (Browser.Exists(Bys.SearchPage.CreditTypeSelElem))
            {
                if (selectElement.Options[1].Text == CreditTypeSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, CreditTypeBtn, selection);
                    }
                    else
                    {
                        CreditTypeSelElem.SelectByValue(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, int index)
        {
            if (Browser.Exists(Bys.SearchPage.CreditTypeSelElem))
            {
                if (selectElement.Options[1].Text == CreditTypeSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByIndex(Browser, CreditTypeBtn, index);
                    }
                    else
                    {
                        CreditTypeSelElem.SelectByIndex(index);
                    }
                    this.WaitForInitialize();

                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Deselects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic DeselectAndWait(SelectElement selectElement, string selection)
        {
            if (Browser.Exists(Bys.SearchPage.CreditTypeSelElem))
            {
                if (selectElement.Options[1].Text == CreditTypeSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_DeselectByText(Browser, CreditTypeBtn, selection);
                    }
                    else
                    {
                        CreditTypeSelElem.DeselectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic DeselectAndWait(SelectElement selectElement, int index)
        {
            if (Browser.Exists(Bys.SearchPage.CreditTypeSelElem))
            {
                if (selectElement.Options[1].Text == CreditTypeSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_DeselectByIndex(Browser, CreditTypeBtn, index);
                    }
                    else
                    {
                        CreditTypeSelElem.DeselectByIndex(index);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }
            return null;
        }

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.SearchPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

            // So this is weird. Whenever some items are chosen in the Bins select element, the activity elements become stale. I have 
            // to hover over an activity for them to become unstale. You can see this occur in the test BinsLoadResults if you comment 
            // the below line of code out. It occurs after selecting ANCC in the Credit Type select element. The activities that get 
            // loaded after selecting that item were activities that were already loaded when selecting an item in the loop that comes 
            // before ANCC
            Actions action = new Actions(Browser);
            if (Browser.Exists(Bys.SearchPage.SearchResultsTblBodyActivityLnks))
            {
                action.MoveToElement(SearchResultsTblBodyActivityLnks[0]).Perform();
            }
            else
            {
                action.MoveToElement(NoDataAvailableLbl).Perform();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose SearchPage", activeRequests.Count, ex); }
        }




        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Clicks on a user-specified activity, then waits for the Activity Front or Overview page to load
        /// </summary>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public dynamic ClickActivity(string activityName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, SearchResultsTbl, Bys.SearchPage.SearchResultsTblBodyActivityLnks,
                activityName, "h4", activityName, "h4");

            // Wait until the page URL loads
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
            wait.Until(Browser => { return Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription()) 
                                        || Browser.Url.Contains(Constants.PageURLs.Activity_Preview.GetDescription())
                                        || Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription())
;
            });

            // If this click takes us to the Activity Overview page
            if (Browser.Url.Contains("activity_overview"))
            {
                ActOverviewPage OP = new ActOverviewPage(Browser);
                OP.WaitForInitialize();
                Thread.Sleep(300);
                return OP;
            }
            // Else if this click takes us to the Front page
            else if(Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription()))
            {
                ActPreviewPage FP = new ActPreviewPage(Browser);
                FP.WaitForInitialize();
                Thread.Sleep(300);
                return FP;
            }
            // Else if this click takes us to the On Hold page
            else
            {
                ActOnHoldPage Page = new ActOnHoldPage(Browser);
                Page.WaitForInitialize();
                Thread.Sleep(300);
                return Page;
            }
        }


        /// <summary>
        /// Retreives all activity's title label text, sorts them and returns them.
        /// </summary>
        public List<string> GetActivityTitles()
        {
            List<string> titles = new List<string>();

            IList<IWebElement> titleLbls = Browser.FindElements(By.XPath("//h4[@class='activity-title hyperlink']"));
            titles.AddRange(titleLbls.Select(elem => elem.Text).ToList());

            List<string> titlesSorted = DataUtils.CustomSortListWithDashes(titles);

            return titlesSorted;
        }



        #endregion methods: page specific



    }
}