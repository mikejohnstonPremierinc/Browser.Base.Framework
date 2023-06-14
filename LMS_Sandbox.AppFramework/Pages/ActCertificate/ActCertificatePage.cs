using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMS.AppFramework
{
    public class ActCertificatePage : LMSPage, IDisposable
    {
        #region constructors
        public ActCertificatePage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "lms/transcript"; } }

        #endregion properties

        #region elements

        public IWebElement CertificateNameLbl { get { return this.FindElement(Bys.ActCertificatePage.CertificateNameLbl); } }
        public IWebElement CertificateObject { get { return this.FindElement(Bys.ActCertificatePage.CertificateObject); } }
        public IWebElement FinishBtn { get { return this.FindElement(Bys.ActCertificatePage.FinishBtn); } }
        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActCertificatePage.ContinueBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.ActCertificatePage.BackBtn); } }




        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActCertificatePage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActCertificatePage.FinishBtn))
            {
                if (elem.GetAttribute("outerHTML") == FinishBtn.GetAttribute("outerHTML"))
                {
                    // 12/18/18: The build showed failed tests failing to wait for the Transcript page. The screenshot showed that it was still
                    // on the Certificate page. 2 reasons this may have happened. Maybe we just need a Javascript click here instead of regular
                    // click. Going to add that now. If this doesnt work, then maybe we need a longer wait after clicking the Continue button
                    // inside the Assessment page (It is currently set at 2 seconds).
                    FinishBtn.ClickJS(Browser);
                    TranscriptPage Page = new TranscriptPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
            {
                // Using javascript click here for the following reason. When we use a regular click after choosing incorrect
                // answers, IE then doesnt load the page fully for some reason. This is not reproducable manually
                ContinueBtn.ClickJS(Browser);
                // Think we may have to sleep here, because if not, sometimes the below dynamic waits will think the assessment that we 
                // were on before the button was clicked is the page to wait for
                Thread.Sleep(3000);

                // Wait until the page URL loads
                var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
                wait.Until(Browser =>
                {
                    return Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription());
                });

                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                ActAssessmentPage Page = new ActAssessmentPage(Browser);
                Page.WaitForInitialize();
                return Page;
            }

            if (Browser.Exists(Bys.ActCertificatePage.BackBtn))
            {
                if (elem.GetAttribute("outerHTML") == BackBtn.GetAttribute("outerHTML"))
                {
                    // Using javascript click here for the following reason. When we use a regular click after choosing incorrect
                    // answers, IE then doesnt load the page fully for some reason. This is not reproducable manually
                    BackBtn.ClickJS(Browser);
                    // Think we may have to sleep here, because if not, sometimes the below dynamic waits will think the assessment that we 
                    // were on before the button was clicked is the page to wait for
                    Thread.Sleep(3000);

                    // Wait until the page URL loads
                    var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
                    wait.Until(Browser =>
                    {
                    return Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                    || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription())
                    || Browser.Url.Contains(Constants.PageURLs.Activity_Claim_Credit.GetDescription());
                    });

                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));


                }

                // If this click takes us to the Claim Credit page
                if (Browser.Url.Contains(Constants.PageURLs.Activity_Claim_Credit.GetDescription()))
                {
                    ActClaimCreditPage Page = new ActClaimCreditPage(Browser);
                    Page.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return Page;
                }

                // Else if this click takes us to an assessment
                else if (Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription()))
                {
                    ActAssessmentPage Page = new ActAssessmentPage(Browser);
                    Page.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return Page;
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


        #endregion methods: page specific



    }
}