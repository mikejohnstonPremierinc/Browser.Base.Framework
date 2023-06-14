using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMS.AppFramework.LMSHelperMethods;
using System.Text.RegularExpressions;

namespace LMS.AppFramework
{
    public class ActMaterialPage : LMSPage, IDisposable
    {
        #region constructors
        public ActMaterialPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/transcript"; } }
        LMSHelperMethods.LMSHelperMethods Help = new LMSHelperMethods.LMSHelperMethods();
        #endregion properties

        #region elements

        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActMaterialPage.ContinueBtn); } }
        public IWebElement FinishBtn { get { return this.FindElement(Bys.ActMaterialPage.FinishBtn); } }
        public IList<IWebElement> ActivityMaterialFileExtensionLnks { get { return this.FindElements(Bys.ActMaterialPage.ActivityMaterialFileExtensionLnks); } }
        public IWebElement ConfirmWithCheckBoxChk { get { return this.FindElement(Bys.ActMaterialPage.ConfirmWithCheckBoxChk); } }
        public IWebElement ConfirmWithCheckBoxLbl { get { return this.FindElement(Bys.ActMaterialPage.ConfirmWithCheckBoxLbl); } }
        public IWebElement ActivityMaterialChk { get { return this.FindElement(Bys.ActMaterialPage.ActivityMaterialChk); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(45), Criteria.ActivityMaterial.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(45));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            // Represents the first required checkbox for any checkboxes on the Material page
            if (Browser.Exists(Bys.ActMaterialPage.ConfirmWithCheckBoxChk))
            {
                if (elem.GetAttribute("outerHTML") == ConfirmWithCheckBoxChk.GetAttribute("outerHTML"))
                {
                    // On IE< whenever this page has content rows expanded and we try to click on something at the bottom of the page, a weird
                    // issue happens and the page freezes. Scrolling first fixes this
                    ConfirmWithCheckBoxChk.Click(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    Browser.WaitForElement(Bys.ActMaterialPage.ContinueBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMaterialPage.ContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    // On IE< whenever this page has content rows expanded and we try to click on something at the bottom of the page, a weird
                    // issue happens and the page freezes. Scrolling first fixes this
                    ElemSet.ScrollToElement(Browser, ContinueBtn, false);

                    // Using javascript click here for the following reason. When we use a regular click, IE then doesnt load 
                    // the page fully for some reason. This is not reproducable manually
                    ContinueBtn.ClickJS(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Content);
                }

                if (elem.GetAttribute("outerHTML") == ActivityMaterialChk.GetAttribute("outerHTML"))
                {
                    // On IE< whenever this page has content rows expanded and we try to click on something at the bottom of the page, a weird
                    // issue happens and the page freezes. Scrolling first fixes this
                    ElemSet.ScrollToElement(Browser, ActivityMaterialChk, false);

                    // Using javascript click here for the following reason. When we use a regular click, IE then doesnt load 
                    // the page fully for some reason. This is not reproducable manually
                    ActivityMaterialChk.ClickJS(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Content);
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActivityPreviewPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Clicks a user-specified element that invokes a download, waits for the file with a user-specified file name to exist in the default
        /// download directory, which is defined in <see cref="SeleniumCoreSettings"/>, then return the filepath as a string
        /// </summary>
        /// <param name="linkText">The link text of the file you want to download</param>
        /// <param name="expectedFileName">Your expected file name of the downloaded file</param>
        /// <param name="deleteFirstIfExists">if set to <c>true</c> and the file already exists (prior to download), it will automatically be deleted.</param>
        /// <returns></returns>
        public string DownloadFile(string linkText, string expectedFileName, bool deleteFirstIfExists)
        {
            IWebElement DownloadLink = Browser.FindElement(By.XPath(string.Format("//a[text()='{0}']", linkText)));
            return DownloadLink.ClickAndWaitForDownload(Browser, expectedFileName, deleteFirstIfExists);
        }

        /// <summary>
        /// Downloads the embedded file by different means depending on the type of embedded file, then return the filepath as a string
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="linkText">The link text of the file you want to download</param>
        /// <param name="contentType"><see cref="Constants.ActContentType"/></param>
        /// <param name="expectedFileName">Your expected file name of the downloaded file</param>
        /// <param name="deleteFirstIfExists">if set to <c>true</c> and the file already exists (prior to download), it will automatically be deleted.</param>
        /// <param name="fileWaitTimeout">The timeout for this operation to keep trying in milliseconds.  Default is 10000 (10 seconds).</param>
        public string DownloadEmbeddedFile(Constants.SiteCodes siteCode, string linkText, Constants.ActContentType contentType,
            string expectedFileName, bool deleteFirstIfExists, double fileWaitTimeout = 80000)
        {
            IWebElement DownloadLink = null;
            string xpath = "";

            ExpandContentRegion(linkText);

            switch (contentType)
            {
                case Constants.ActContentType.JPEG:
                    xpath = string.Format("//span[text()='{0}']/ancestor::tr/following-sibling::tr[1]//a", linkText);
                    DownloadLink = Browser.FindElement(By.XPath(xpath));
                    break;

                case Constants.ActContentType.PDF:
                    xpath = string.Format("(//span[text()='{0}']/ancestor::tr/following-sibling::tr[1]//a)[1]", linkText);
                    DownloadLink = Browser.FindElement(By.XPath(xpath));
                    break;

                case Constants.ActContentType.Video:
                case Constants.ActContentType.Audio:
                    xpath = string.Format("(//span[text()='{0}']/ancestor::tr/following-sibling::tr[1]//a)[2]", linkText);
                    DownloadLink = Browser.FindElement(By.XPath(xpath));
                    break;

                case Constants.ActContentType.HTML:
                case Constants.ActContentType.URL:
                case Constants.ActContentType.Word:
                case Constants.ActContentType.Excel:
                case Constants.ActContentType.PPT:
                case Constants.ActContentType.TEXT:
                    throw new Exception("The content type you passed is not an embedded content type");
            }

            //CollapseContentRegion(linkText, contentType);

            return DownloadLink.ClickAndWaitForDownload(Browser, expectedFileName, deleteFirstIfExists, fileWaitTimeout);
        }

        /// <summary>
        /// If not already expanded, expands the content region for the tester-specified content
        /// </summary>
        /// <param name="linkText"></param>
        public void ExpandContentRegion(string linkText)
        {
            IWebElement expansionRow = Browser.FindElement(By.XPath(string.Format("//*[text()='{0}']/ancestor::tr", linkText)));

            // If not already expanded, expand it
            if (!expansionRow.GetAttribute("class").Contains("expanded"))
            {
                expansionRow.Click(Browser);
                Thread.Sleep(1000);
            }

            // Had to do this twice because sometimes it doesnt open on the first click
            if (!expansionRow.GetAttribute("class").Contains("expanded"))
            {
                expansionRow.Click(Browser);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// If not already collapses, collapses the content region for the tester-specified content
        /// </summary>
        /// <param name="linkText"></param>
        public void CollapseContentRegion(string linkText)
        {
            IWebElement expansionRow = Browser.FindElement(By.XPath(string.Format("//*[text()='{0}']/ancestor::tr", linkText)));

            // If not already expanded, expand it
            if (expansionRow.GetAttribute("class").Contains("expanded"))
            {
                expansionRow.Click(Browser);
                Thread.Sleep(1000);
            }
        }

        #endregion methods: page specific



    }
}