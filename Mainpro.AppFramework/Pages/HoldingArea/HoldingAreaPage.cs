using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;


namespace Mainpro.AppFramework
{
    public class HoldingAreaPage : MainproPage, IDisposable
    {
        #region constructors
        public HoldingAreaPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Default.aspx"; } }

        #endregion properties

        #region elements
        public IWebElement SummTabIncompActTblTblFirstRowDeleteBtn { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTblFirstRowDeleteBtn); } }
        public IWebElement SummTabIncompActTblFirstRowActivityCellLnk { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTblFirstRowActivityCellLnk); } }

        public IWebElement SummTabIncompActTblActivityColHdr { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTblActivityColHdr); } }
        public IWebElement SummTabIncompActTblCreditsColHdr { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTblCreditsColHdr); } }
        public IWebElement SummTabIncompActTblLastUpdatedColHdr { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTblLastUpdatedColHdr); } }
        public IWebElement SummTabActPendApprTblActivityColHdr { get { return this.FindElement(Bys.HoldingAreaPage.SummTabActPendApprTblActivityColHdr); } }
        public IWebElement SummTabActPendApprTblCreditsColHdr { get { return this.FindElement(Bys.HoldingAreaPage.SummTabActPendApprTblCreditsColHdr); } }
        public IWebElement SummTabActPendApprTblLastUpdatedColHdr { get { return this.FindElement(Bys.HoldingAreaPage.SummTabActPendApprTblLastUpdatedColHdr); } }

        public IWebElement IncActTab { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTab); } }

        public IWebElement IncompleteTabActTblActivityColHdr { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTabActTblActivityColHdr); } }
        public IWebElement IncompleteTabActTblCreditsColHdr { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTabActTblCreditsColHdr); } }
        public IWebElement IncompleteTabActTblLastUpdatedColHdr { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTabActTblLastUpdatedColHdr); } }
        public IWebElement CredValTab { get { return this.FindElement(Bys.HoldingAreaPage.CredValTab); } }

        public IWebElement CredValTabActTblActivityColHdr { get { return this.FindElement(Bys.HoldingAreaPage.CredValTabActTblActivityColHdr); } }
        public IWebElement CredValTabActTblCreditsColHdr { get { return this.FindElement(Bys.HoldingAreaPage.CredValTabActTblCreditsColHdr); } }
        public IWebElement CredValTabActTblLastUpdatedColHdr { get { return this.FindElement(Bys.HoldingAreaPage.CredValTabActTblLastUpdatedColHdr); } }
        public IWebElement CredValTabActTblBodyFirstRow { get { return this.FindElement(Bys.HoldingAreaPage.CredValTabActTblBodyFirstRow); } }
        public IWebElement CredValTabActTblBody { get { return this.FindElement(Bys.HoldingAreaPage.CredValTabActTblBody); } }
        public IWebElement CredValTabActTblHdr { get { return this.FindElement(Bys.HoldingAreaPage.CredValTabActTblHdr); } }
        public IWebElement CredValTabActTbl { get { return this.FindElement(Bys.HoldingAreaPage.CredValTabActTbl); } }
        public IWebElement IncompleteTabActTblBodyFirstRow { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTabActTblBodyFirstRow); } }
        public IWebElement IncompleteTabActTblBody { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTabActTblBody); } }
        public IWebElement IncompleteTabActTblHdr { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTabActTblHdr); } }
        public IWebElement IncompleteTabActTbl { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTabActTbl); } }
        public IWebElement SummTabActPendApprTblBodyFirstRow { get { return this.FindElement(Bys.HoldingAreaPage.SummTabActPendApprTblBodyFirstRow); } }
        public IWebElement SummTabActPendApprTblBody { get { return this.FindElement(Bys.HoldingAreaPage.SummTabActPendApprTblBody); } }
        public IWebElement SummTabActPendApprTblHdr { get { return this.FindElement(Bys.HoldingAreaPage.SummTabActPendApprTblHdr); } }
        public IWebElement SummTabActPendApprTbl { get { return this.FindElement(Bys.HoldingAreaPage.SummTabActPendApprTbl); } }
        public IWebElement SummTabIncompActTblBodyFirstRow { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTblBodyFirstRow); } }
        public IWebElement SummTabIncompActTblBody { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTblBody); } }
        public IWebElement SummTabIncompActTblHdr { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTblHdr); } }
        public IWebElement SummTabIncompActTbl { get { return this.FindElement(Bys.HoldingAreaPage.SummTabIncompActTbl); } }
        public IWebElement SummTab { get { return this.FindElement(Bys.HoldingAreaPage.SummTab); } }
        public IWebElement IncompleteTab { get { return this.FindElement(Bys.HoldingAreaPage.IncompleteTab); } }


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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.HoldingAreaPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.HoldingAreaPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
        }
        public void Wait(int time)
        {
            System.Threading.Thread.Sleep(time);
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose CreditSummaryPge", activeRequests.Count, ex); }
        }

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.MainproPage.DeleteFormYesBtn))
            {
                if (elem.GetAttribute("outerHTML") == DeleteFormYesBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.HoldingAreaPage.SummTabIncompActTblActivityColHdr))
            {
                if (elem.GetAttribute("outerHTML") == SummTabIncompActTblActivityColHdr.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.HoldingAreaPage.IncompleteTab))
            {
                if (elem.GetAttribute("outerHTML") == IncActTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.HoldingAreaPage.IncompleteTabActTbl, ElementCriteria.IsVisible);
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.HoldingAreaPage.CredValTab))
            {
                if (elem.GetAttribute("outerHTML") == CredValTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.HoldingAreaPage.CredValTabActTbl, ElementCriteria.IsVisible);
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.HoldingAreaPage.SummTab))
            {
                if (elem.GetAttribute("outerHTML") == SummTab.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.HoldingAreaPage.SummTabActPendApprTbl, ElementCriteria.IsVisible);
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }



        #endregion methods: per page

        #region methods: page specific


        #endregion methods: page specific


    }
}
