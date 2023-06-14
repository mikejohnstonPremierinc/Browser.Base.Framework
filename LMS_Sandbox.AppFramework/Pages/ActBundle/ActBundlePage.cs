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

namespace LMS.AppFramework
{
    public class ActBundlePage : LMSPage, IDisposable
    {
        #region constructors
        public ActBundlePage(IWebDriver driver) : base(driver)
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
        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActBundlePage.ContinueBtn); } }
        public IWebElement ActivityTbl { get { return this.FindElement(Bys.ActBundlePage.ActivityTbl); } }
        public IWebElement ActivityTblBody { get { return this.FindElement(Bys.ActBundlePage.ActivityTblBody); } }
        public IWebElement FinishActivityTblBodyActivityLnksBtn { get { return this.FindElement(Bys.ActBundlePage.ActivityTblBodyActivityLnks); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActBundlePage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
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
        /// Clicks on a user-specified activity, then waits for the Activity Front or Overview page to load
        /// </summary>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public dynamic ClickActivity(string activityName)
        {
            //// Right now production doesnt match UAT. UAT now has H4 tags
            //if (Constants.CurrentEnvironment != Constants.Environments.Production.GetDescription())
            //{
                ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, ActivityTbl, Bys.ActBundlePage.ActivityTblBodyActivityLnks,
                    activityName, "h4", activityName, "h4");
            //}
            //else
            //{
            //    ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, ActivityTbl, Bys.ActBundlePage.ActivityTblBodyActivityLnks,
            //        activityName, "a", activityName, "a");
            //}

            // Wait until the page URL loads
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
            wait.Until(Browser => { return Browser.Url.Contains("activity_overview?") || Browser.Url.Contains("activity?"); });

            // If this click takes us to the Activity Overview page
            if (Browser.Url.Contains("activity_overview"))
            {
                ActOverviewPage OP = new ActOverviewPage(Browser);
                OP.WaitForInitialize();
                Thread.Sleep(300);
                return OP;
            }

            // Else if this click takes us to the Preview page
            else
            {
                ActPreviewPage FP = new ActPreviewPage(Browser);
                FP.WaitForInitialize();
                Thread.Sleep(300);
                return FP;
            }
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActBundlePage.ContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    // Using javascript click here for the following reason. When we use a regular click, IE then doesnt load 
                    // the page fully for some reason. This is not reproducable manually
                    ContinueBtn.ClickJS(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    // Wait until the page URL loads
                    var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
                    wait.Until(Browser =>
                    {
                        return Browser.Url.Contains("activity_pretest")
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription())
                        || Browser.Url.Contains("activity_content");
                    });

                    // If this click takes us to Activity Material
                    if (Browser.Url.Contains(Constants.PageURLs.Activity_Content.GetDescription()))
                    {
                        ActMaterialPage Page = new ActMaterialPage(Browser);
                        Page.WaitForInitialize();
                        return Page;
                    }

                    // If this click takes us to an assessment
                    if (Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription()))
                    {
                        ActAssessmentPage Page = new ActAssessmentPage(Browser);
                        Page.WaitForInitialize();
                        return Page;
                    }

                    // Else if this click takes us to the Certificate page
                    else if (Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription()))
                    {
                        ActCertificatePage Page = new ActCertificatePage(Browser);
                        Page.WaitForInitialize();
                        return Page;
                    }

                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        #endregion methods: page specific



    }
}