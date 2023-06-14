using Browser.Core.Framework;
using LMS.Data;
using LMS.AppFramework.LMSHelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMS.AppFramework
{
    public class ActClaimCreditPage : LMSPage, IDisposable
    {
        #region constructors
        public ActClaimCreditPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/#/activity"; } }

        LMSHelperMethods.LMSHelperMethods Help = new LMSHelperMethods.LMSHelperMethods();
        #endregion properties

        #region elements

        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActClaimCreditPage.ContinueBtn); } }
        public IList<IWebElement> ClaimCreditBtns { get { return this.FindElements(Bys.ActClaimCreditPage.ClaimCreditBtns); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActClaimCreditPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActClaimCreditPage.ContinueBtn))
            {
                // Using javascript click here for the following reason. When we use a regular click after choosing incorrect
                // answers, IE then doesnt load the page fully for some reason. This is not reproducable manually
                ContinueBtn.ClickJS(Browser);

                // Wait until the page URL loads
                var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
                wait.Until(Browser =>
                {
                    return Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription());
                });

                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                ActCertificatePage Page = new ActCertificatePage(Browser);
                Page.WaitForInitialize();
                return Page;
            }


            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        public void GetDropDownElem(IWebDriver browser, string v)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActClaimCreditPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Gets the IWebelement of the tester specified Provider Claim Credit dropdown
        /// </summary>
        /// <param name="accreditingBody">The exact text of the Accrediting Body label. If the Provider is NONACCR, leave this parameter blank</param>
        /// <param name="Provider">The exact text of the Provider label</param>
        public IWebElement GetClaimCreditSelectElementButton(string accreditingBody, string Provider)
        {
            if (string.IsNullOrEmpty(accreditingBody))
            {
                return Browser.FindElement(By.XPath(
                string.Format("//span[contains(text(), 'NONACCR')]/ancestor::form/descendant::button")));
            }
            else
            {
                return Browser.FindElement(By.XPath(
                string.Format("//div[text()='{0}']/ancestor::form//span[contains(text(), '{1}')]/ancestor::form/descendant::button",
                accreditingBody, Provider)));
            }
        }

        /// <summary>
        /// Gets the SelectElement of the tester specified Provider Claim Credit dropdown
        /// </summary>
        /// <param name="accreditingBody">The exact text of the Accrediting Body label</param>
        /// <param name="Provider">The exact text of the Provider label</param>
        public SelectElement GetClaimCreditSelElem(string accreditingBody, string Provider)
        {
            if (string.IsNullOrEmpty(accreditingBody))
            {
                return new SelectElement(Browser.FindElement(By.XPath(
                string.Format("//span[contains(text(), 'NONACCR')]/ancestor::form/descendant::select"))));
            }
            else
            {
                return new SelectElement(Browser.FindElement(By.XPath(
                string.Format("//div[text()='{0}']/ancestor::form//span[contains(text(), '{1}')]/ancestor::form/descendant::select",
                accreditingBody, Provider))));
            }

        }

        /// <summary>
        /// Gets the IWebelement of the tester specified Provider Claim button
        /// </summary>
        /// <param name="accreditingBody">The exact text of the Accrediting Body label</param>
        /// <param name="Provider">The exact text of the Provider label</param>
        public IWebElement GetClaimButton(string accreditingBody, string Provider)
        {
            if (string.IsNullOrEmpty(accreditingBody))
            {
                return Browser.FindElement(By.XPath(
                string.Format("//span[contains(text(), 'NONACCR')]/ancestor::form/descendant::span[text()='CLAIM']")));
            }
            else
            {
                return Browser.FindElement(By.XPath(
                string.Format("//div[text()='{0}']/ancestor::form//span[contains(text(), '{1}')]/ancestor::form/descendant::span[text()='CLAIM']",
                accreditingBody, Provider)));
            }
        }

        /// <summary>
        /// Clicks the Claim button for all of the available Providers
        /// </summary>
        public void ClaimAllCredit()
        {
            for (int i = 0; i < ClaimCreditBtns.Count; i++)
            {
                ClaimCreditBtns[0].Click();
                Thread.Sleep(0200);
                this.WaitUntil(Criteria.ActClaimCreditPage.LoadIconNotExists);
            }
        }

        /// <summary>
        /// Clicks the Claim button for a specific Provider
        /// </summary>
        /// <param name="provider">The exact text from the Provider label</param>
        public void ClaimCredit(string accreditingBody, string provider)
        {
            GetClaimButton(accreditingBody, provider).Click();
            Thread.Sleep(0200);
            this.WaitUntil(Criteria.ActClaimCreditPage.LoadIconNotExists);
        }

        #endregion methods: page specific



    }
}