using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;


namespace Mainpro.AppFramework
{
    public class PLPHubPage : MainproPage, IDisposable
    {
        #region constructors
        public PLPHubPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Default.aspx"; } }

        MainproHelperMethods Help = new MainproHelperMethods();

        #endregion properties

        #region elements
        public IWebElement PlpHubPlpEnterBtn { get { return this.FindElement(Bys.PLPHubPage.PlpHubPlpEnterBtn); } }
        public IWebElement LearnMoreBtn { get { return this.FindElement(Bys.PLPHubPage.LearnMoreBtn); } }
        public IWebElement printPLPCertCloseBtn { get { return this.FindElement(Bys.PLPHubPage.printPLPCertCloseBtn); } }
        public IWebElement printPLPCompleteCloseButton { get { return this.FindElement(Bys.PLPHubPage.printPLPCompleteCloseButton); } }
        public IWebElement PrintPLPCertificateDownloadBtn { get { return this.FindElement(Bys.PLPHubPage.PrintPLPCertificateDownloadBtn); } }
        public IWebElement PrintmycompletedPLPDownloadBtn { get { return this.FindElement(Bys.PLPHubPage.PrintmycompletedPLPDownloadBtn); } }
        public IWebElement ViewCompletedPlpLnk { get { return this.FindElement(Bys.PLPHubPage.ViewCompletedPlpLnk); } }
        public IWebElement printCompletedPlpLnk { get { return this.FindElement(Bys.PLPHubPage.printCompletedPlpLnk); } }
        public IWebElement printPlpCertificateLnk { get { return this.FindElement(Bys.PLPHubPage.printPlpCertificateLnk); } }
        public IWebElement PLPHubCompletedPLPTbl { get { return this.FindElement(Bys.PLPHubPage.PLPHubCompletedPLPTbl); } }
        public IWebElement PLPHubCompletedPLPTblBody { get { return this.FindElement(Bys.PLPHubPage.PLPHubCompletedPLPTblBody); } }
        public IWebElement PLPHubCompletedPLPTblBodyFirstRow { get { return this.FindElement(Bys.PLPHubPage.PLPHubCompletedPLPTblBodyFirstRow); } }
        public IWebElement PLPHubCompletedPLPTblHdr { get { return this.FindElement(Bys.PLPHubPage.PLPHubCompletedPLPTblHdr); } }

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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.PLPHubPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.PLPHubPage.PageReady);
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose CreditSummaryPge", activeRequests.Count, ex); }
        }
        
        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
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
           
            if (Browser.Exists(Bys.PLPHubPage.printCompletedPlpLnk))
            {
                if (elem.GetAttribute("outerHTML") == printCompletedPlpLnk.GetAttribute("outerHTML"))
                {
                    elem.ClickJS(Browser);
                    Browser.WaitForElement(Bys.PLPHubPage.PrintmycompletedPLPDownloadBtn, TimeSpan.FromSeconds(120),
                         ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.PLPHubPage.ViewCompletedPlpLnk))
            {
                if (elem.GetAttribute("outerHTML") == ViewCompletedPlpLnk.GetAttribute("outerHTML"))
                {
                    elem.ClickJS(Browser);
                    StepPRPage PR = new StepPRPage(Browser);
                    Browser.WaitForElement(Bys.StepPRPage.ExitPLPBtn, TimeSpan.FromSeconds(120),
                         ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return PR;
                }
            }
            if (Browser.Exists(Bys.PLPHubPage.printPlpCertificateLnk))
            {
                if (elem.GetAttribute("outerHTML") == printPlpCertificateLnk.GetAttribute("outerHTML"))
                {
                    elem.ClickJS(Browser);
                    Browser.WaitForElement(Bys.PLPHubPage.PrintPLPCertificateDownloadBtn, TimeSpan.FromSeconds(120),
                         ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.PLPHubPage.printPLPCompleteCloseButton))
            {
                if (elem.GetAttribute("outerHTML") == printPLPCompleteCloseButton.GetAttribute("outerHTML"))
                {
                    elem.ClickJS(Browser);
                    Browser.WaitForElement(Bys.PLPHubPage.printPLPCompleteCloseButton, TimeSpan.FromSeconds(60),
                         ElementCriteria.IsNotVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            
            if (Browser.Exists(Bys.PLPHubPage.PrintPLPCertificateDownloadBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == PrintPLPCertificateDownloadBtn.GetAttribute("outerHTML"))
                {
                    WindowAndFrameUtils.ClickOnLinkAndSwitchToWindow(Browser, PrintPLPCertificateDownloadBtn,
                        URLToWaitFor: urlToWaitFor,
                        timeToWaitForURL: TimeSpan.FromSeconds(120));
                    return null;
                }
            }
            if (Browser.Exists(Bys.PLPHubPage.PrintmycompletedPLPDownloadBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == PrintmycompletedPLPDownloadBtn.GetAttribute("outerHTML"))
                {
                    WindowAndFrameUtils.ClickOnLinkAndSwitchToWindow(Browser, PrintmycompletedPLPDownloadBtn,
                        URLToWaitFor: urlToWaitFor,
                        timeToWaitForURL: TimeSpan.FromSeconds(120));
                    return null;
                }
            }
            if (Browser.Exists(Bys.PLPHubPage.printPLPCertCloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == printPLPCertCloseBtn.GetAttribute("outerHTML"))
                {
                    elem.ClickJS(Browser);
                    Browser.WaitForElement(Bys.PLPHubPage.printPLPCertCloseBtn, TimeSpan.FromSeconds(60),
                         ElementCriteria.IsNotVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }
        #endregion methods: repeated per page

        #region    methods: page specific

        #endregion


    }
}
