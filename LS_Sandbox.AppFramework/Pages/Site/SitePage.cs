using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LS.AppFramework
{
    public class SitePage : Page, IDisposable
    {
        #region constructors
        public SitePage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements
        public IWebElement ActivityUploadLnk { get { return this.FindElement(Bys.SitePage.ActivityUploadLnk); } }

        #endregion elements

        #region methods: repeated per page

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.SitePage.ActivityUploadLnk))
            {
                if (elem.GetAttribute("outerHTML") == ActivityUploadLnk.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    ActivityUploadPage AUP = new ActivityUploadPage(Browser);
                    AUP.WaitForInitialize();
                    return AUP;
                }
            }

            //if (Browser.Exists(Bys.EnterACPDActivityPage.DoYouKnowYourSessionIDContinueBtn))
            //{
            //    if (elem.GetAttribute("outerHTML") == DoYouKnowYourSessionIDContinueBtn.GetAttribute("outerHTML"))
            //    {
            //        elem.Click();
            //        EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
            //        EADP.WaitForInitialize();
            //        return EADP;
            //    }
            //}

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        public override void WaitForInitialize()
        {
            try
            {
                this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.SitePage.PageReady);
            }
            catch
            {
                RefreshPage();
            }

        }

        /// Refreshes the page and then uses the wait criteria that is found within WaitForInitialize to wait for the page to load.
        /// This is used as a catch block inside WaitForInitialize, in case the page doesnt load initially. Can also be used to 
        /// randomly refresh the page
        /// </summary>
        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.SitePage.PageReady);
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


        #endregion methods: page specific



    }
}