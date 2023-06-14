using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMS.Data;

namespace Mainpro.AppFramework
{
    public class ReportsPage : MainproPage, IDisposable
    {
        #region constructors
        public ReportsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "cpd/reports"; } }

        #endregion properties

        #region elements
        public IWebElement ReportPDFEmbedElem { get { return this.FindElement(Bys.ReportsPage.ReportPDFEmbedElem); } }
        public IWebElement MyCreditSummaryRunReportBtn { get { return this.FindElement(Bys.ReportsPage.MyCreditSummaryRunReportBtn); } }
        public IWebElement MyCreditSummaryFormCreateReportBtn { get { return this.FindElement(Bys.ReportsPage.MyCreditSummaryFormCreateReportBtn); } }
        public IWebElement MyCreditSummaryFormDownloadReportBtn { get { return this.FindElement(Bys.ReportsPage.MyCreditSummaryFormDownloadReportBtn); } }
        public IWebElement MyCreditSummaryFormXBtn { get { return this.FindElement(Bys.ReportsPage.MyCreditSummaryFormXBtn); } }
        public SelectElement MyCreditSummaryFormCycleSelElem { get { return new SelectElement(this.FindElement(Bys.ReportsPage.MyCreditSummaryFormFormCycleSelElem)); } }
        public IWebElement MyCreditSummaryFormCycleSelElemBtn { get { return this.FindElement(Bys.ReportsPage.MyCreditSummaryFormCycleSelElemBtn); } }
        public IWebElement MyTranscriptOfCPDActsRunReportBtn { get { return this.FindElement(Bys.ReportsPage.MyTranscriptOfCPDActsRunReportBtn); } }
        public IWebElement MyTranscriptOfCPDActsFormCreateReportBtn { get { return this.FindElement(Bys.ReportsPage.MyTranscriptOfCPDActsFormCreateReportBtn); } }
        public IWebElement MyTranscriptOfCPDActsFormDownloadReportBtn { get { return this.FindElement(Bys.ReportsPage.MyTranscriptOfCPDActsFormDownloadReportBtn); } }
        public SelectElement MyTranscriptOfCPDActsFormCycleSelElem { get { return new SelectElement(this.FindElement(Bys.ReportsPage.MyTranscriptOfCPDActsFormCycleSelElem)); } }
        public IWebElement MyTranscriptOfCPDActsFormCycleSelElemBtn { get { return this.FindElement(Bys.ReportsPage.MyTranscriptOfCPDActsFormCycleSelElemBtn); } }
        public IWebElement MyTranscriptOfCPDActsFormXBtn { get { return this.FindElement(Bys.ReportsPage.MyTranscriptOfCPDActsFormXBtn); } }

        public IWebElement MyMainproCycleCompleteionCertRunReportBtn { get { return this.FindElement(Bys.ReportsPage.MyMainproCycleCompleteionCertRunReportBtn); } }
        public IWebElement MyMainproCycleCompleteionCertFormCreateReportBtn { get { return this.FindElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCreateReportBtn); } }
        public IWebElement MyMainproCycleCompleteionCertFormDownloadReportBtn { get { return this.FindElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormDownloadReportBtn); } }
        public SelectElement MyMainproCycleCompleteionCertFormCycleSelElem { get { return new SelectElement(this.FindElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCycleSelElem)); } }
        public IWebElement MyMainproCycleCompleteionCertFormCycleSelElemBtn { get { return this.FindElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCycleSelElemBtn); } }

        public IWebElement MyMainproCycleCompleteionCertFormXBtn { get { return this.FindElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormXBtn); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ReportsPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ReportsPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
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
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            // Sometimes this URL is zcpdapi/file/download/report/ and sometimes it is "zcpdapi/legacyfile/download"
            // so im just going to wait for zcpapi
            string urlToWaitFor = null;
            // If we are on Prod and have launched the user from LTST, then the URL will be the vanity URL, else if we are on 
            // UAT or QA or we have logged in on Prod, then we will be on the non-vanity URL
            if (Browser.Url.Contains("mainproplus"))
            {
                urlToWaitFor = AppSettings.Config["vanityurl"].ToString() + "zcpdapi";
            }
            else
            {
                urlToWaitFor = AppSettings.Config["url"].ToString() + "zcpdapi";
            }

            if (Browser.Exists(Bys.ReportsPage.MyCreditSummaryRunReportBtn))
            {
                if (elem.GetAttribute("outerHTML") == MyCreditSummaryRunReportBtn.GetAttribute("outerHTML"))
                {
                    string blah = elem.FindElement(By.XPath("ancestor::div[contains(@class, 'fireball-widget')]")).
                        GetAttribute("class");
                    // There are 3 Run Report buttons on this page, and they all have exactly the same outerHTML. There 
                    // is no attribute value that is unique to these buttons. So we have to do one more If statement. We 
                    // will get the parent div element of the element, and this div element has a unique class attribute
                    if (elem.FindElement(By.XPath("ancestor::div[contains(@class, 'fireball-widget')][1]")).
                        GetAttribute("class") ==
                        "fireball-widget reportCreditSummaryButton")
                    {
                        elem.Click();
                        Browser.WaitForElement(Bys.ReportsPage.MyCreditSummaryFormCreateReportBtn, ElementCriteria.IsVisible);
                        Browser.WaitForElement(Bys.ReportsPage.MyCreditSummaryFormFormCycleSelElem,
                            ElementCriteria.SelectElementHasAtLeast1Item);
                        Browser.WaitJSAndJQuery();
                        return null;
                    }
                }
            }

            // Adding IsVisible because there are 3 of these buttons that exist on this page with the same outerHTML, 
            // even though only 1 of them is visible at a time
            if (Browser.Exists(Bys.ReportsPage.MyCreditSummaryFormCreateReportBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == MyCreditSummaryFormCreateReportBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.ReportsPage.MyCreditSummaryFormDownloadReportBtn, TimeSpan.FromSeconds(180),
                        ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            // Adding IsVisible because there are 3 of these buttons that exist on this page with the same outerHTML, 
            // even though only 1 of them is visible at a time
            if (Browser.Exists(Bys.ReportsPage.MyCreditSummaryFormDownloadReportBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == MyCreditSummaryFormDownloadReportBtn.GetAttribute("outerHTML"))
                {
                    WindowAndFrameUtils.ClickOnLinkAndSwitchToWindow(Browser, MyCreditSummaryFormDownloadReportBtn,
                        URLToWaitFor: urlToWaitFor,
                        timeToWaitForURL: TimeSpan.FromSeconds(120));
                    return null;
                }
            }

            if (Browser.Exists(Bys.ReportsPage.MyCreditSummaryFormXBtn))
            {
                if (elem.GetAttribute("outerHTML") == MyCreditSummaryFormXBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitJSAndJQuery();
                    this.WaitUntil(Criteria.ReportsPage.MyCreditSummaryFormXBtnNotvisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ReportsPage.MyTranscriptOfCPDActsRunReportBtn))
            {
                if (elem.GetAttribute("outerHTML") == MyTranscriptOfCPDActsRunReportBtn.GetAttribute("outerHTML"))
                {
                    // There are 3 Run Report buttons on this page, and they all have exactly the same outerHTML. There 
                    // is no attribute value that is unique to these buttons. So we have to do one more If statement. We 
                    // will get the parent div element of the element, and this div element has a unique class attribute
                    if (elem.FindElement(By.XPath("ancestor::div[contains(@class, 'fireball-widget')][1]")).
                        GetAttribute("class") ==
                        "fireball-widget reportTranscriptButton")
                    {
                        elem.Click();
                        Browser.WaitForElement(Bys.ReportsPage.MyTranscriptOfCPDActsFormCycleSelElem,
                            ElementCriteria.SelectElementHasAtLeast1Item);
                        Browser.WaitForElement(Bys.ReportsPage.MyTranscriptOfCPDActsFormCreateReportBtn, ElementCriteria.IsVisible);
                        Browser.WaitJSAndJQuery();
                        return null;
                    }
                }
            }

            // Adding IsVisible because there are 3 of these buttons that exist on this page with the same outerHTML, 
            // even though only 1 of them is visible at a time
            if (Browser.Exists(Bys.ReportsPage.MyTranscriptOfCPDActsFormCreateReportBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == MyTranscriptOfCPDActsFormCreateReportBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.ReportsPage.MyTranscriptOfCPDActsFormDownloadReportBtn, TimeSpan.FromSeconds(120),
                        ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            // Adding IsVisible because there are 3 of these buttons that exist on this page with the same outerHTML, 
            // even though only 1 of them is visible at a time
            if (Browser.Exists(Bys.ReportsPage.MyTranscriptOfCPDActsFormDownloadReportBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == MyTranscriptOfCPDActsFormDownloadReportBtn.GetAttribute("outerHTML"))
                {
                    WindowAndFrameUtils.ClickOnLinkAndSwitchToWindow(Browser, MyTranscriptOfCPDActsFormDownloadReportBtn,
                        URLToWaitFor: urlToWaitFor,
                        timeToWaitForURL: TimeSpan.FromSeconds(120));
                    return null;
                }
            }

            if (Browser.Exists(Bys.ReportsPage.MyTranscriptOfCPDActsFormXBtn))
            {
                if (elem.GetAttribute("outerHTML") == MyTranscriptOfCPDActsFormXBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitJSAndJQuery();
                    this.WaitUntil(Criteria.ReportsPage.MyTranscriptOfCPDActsFormXBtnNotvisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ReportsPage.MyMainproCycleCompleteionCertRunReportBtn))
            {
                if (elem.GetAttribute("outerHTML") == MyMainproCycleCompleteionCertRunReportBtn.GetAttribute("outerHTML"))
                {
                    // There are 3 Run Report buttons on this page, and they all have exactly the same outerHTML. There 
                    // is no attribute value that is unique to these buttons. So we have to do one more If statement. We 
                    // will get the parent div element of the element, and this div element has a unique class attribute
                    if (elem.FindElement(By.XPath("ancestor::div[contains(@class, 'fireball-widget')][1]")).
                        GetAttribute("class") ==
                        "fireball-widget reportCycleCompletionButton")
                    {
                        elem.Click();
                        Browser.WaitForElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCycleSelElem,
                            ElementCriteria.SelectElementHasAtLeast1Item);
                        Browser.WaitForElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCreateReportBtn,
                            ElementCriteria.IsVisible);
                        Browser.WaitJSAndJQuery();
                        return null;
                    }
                }
            }

            // Adding IsVisible because there are 3 of these buttons that exist on this page with the same outerHTML, 
            // even though only 1 of them is visible at a time
            if (Browser.Exists(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCreateReportBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == MyMainproCycleCompleteionCertFormCreateReportBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormDownloadReportBtn,
                        TimeSpan.FromSeconds(240),
                        ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            // Adding IsVisible because there are 3 of these buttons that exist on this page with the same outerHTML, 
            // even though only 1 of them is visible at a time
            if (Browser.Exists(Bys.ReportsPage.MyMainproCycleCompleteionCertFormDownloadReportBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == MyMainproCycleCompleteionCertFormDownloadReportBtn.GetAttribute("outerHTML"))
                {
                    WindowAndFrameUtils.ClickOnLinkAndSwitchToWindow(Browser, MyMainproCycleCompleteionCertFormDownloadReportBtn,
                        URLToWaitFor: urlToWaitFor,
                        timeToWaitForURL: TimeSpan.FromSeconds(120));
                    return null;
                }
            }

            if (Browser.Exists(Bys.ReportsPage.MyMainproCycleCompleteionCertFormXBtn))
            {
                if (elem.GetAttribute("outerHTML") == MyMainproCycleCompleteionCertFormXBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitJSAndJQuery();
                    this.WaitUntil(Criteria.ReportsPage.MyMainproCycleCompleteionCertFormXBtnNotvisible);
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
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
            if (Browser.Exists(Bys.ReportsPage.MyCreditSummaryFormCreateReportBtn, ElementCriteria.IsVisible))
            {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownSingle_Fireball_SelectByText(Browser, MyCreditSummaryFormCycleSelElemBtn, selection);
                    }
                    else
                    {
                        MyCreditSummaryFormCycleSelElem.SelectByText(selection);
                    }
                    Browser.WaitJSAndJQuery();
                    return null;
            }

            if (Browser.Exists(Bys.ReportsPage.MyTranscriptOfCPDActsFormCreateReportBtn, ElementCriteria.IsVisible))
            {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownSingle_Fireball_SelectByText(Browser, MyTranscriptOfCPDActsFormCycleSelElemBtn, selection);
                    }
                    else
                    {
                        MyTranscriptOfCPDActsFormCycleSelElem.SelectByText(selection);
                    }
                    Browser.WaitJSAndJQuery();
                    return null;
            }

            if (Browser.Exists(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCreateReportBtn, ElementCriteria.IsVisible))
            {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownSingle_Fireball_SelectByText(Browser, MyMainproCycleCompleteionCertFormCycleSelElemBtn, selection);
                    }
                    else
                    {
                        MyMainproCycleCompleteionCertFormCycleSelElem.SelectByText(selection);
                    }
                    Browser.WaitJSAndJQuery();
                    return null;
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element."));
        }


        #endregion methods: repeated per page

        #region methods: page specific


        #endregion methods: page specific


    }

}
