using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public class Step1Page : MainproPage, IDisposable
    {
        #region constructors
        public Step1Page(IWebDriver driver) : base(driver)
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
        
        public IWebElement TimeBreakdownGraphLabel { get { return this.FindElement(Bys.Step1Page.TimeBreakdownGraphLabel); } }
        public IWebElement TimeBreakdownGraphGreyColorLabel { get { return this.FindElement(Bys.Step1Page.TimeBreakdownGraphGreyColorLabel); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.Step1Page.NextBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.Step1Page.BackBtn); } }
        public IWebElement ExpandAllBtn { get { return this.FindElement(Bys.Step1Page.ExpandAllBtn); } }
        public IWebElement CollapseAllBtn { get { return this.FindElement(Bys.Step1Page.CollapseAllBtn); } }
        public IWebElement ClinicalCareMinusBtn { get { return this.FindElement(Bys.Step1Page.ClinicalCareMinusBtn); } }
        public IWebElement ResearchMinusBtn { get { return this.FindElement(Bys.Step1Page.ResearchMinusBtn); } }
        public IWebElement ScholarshipQualityMinusBtn { get { return this.FindElement(Bys.Step1Page.ScholarshipQualityMinusBtn); } }
        public IWebElement ScholarshipEduMinusBtn { get { return this.FindElement(Bys.Step1Page.ScholarshipEduMinusBtn); } }
        public IWebElement LeaderAdminMinusBtn { get { return this.FindElement(Bys.Step1Page.LeaderAdminMinusBtn); } }
        public IWebElement ClinicalCarePlusBtn { get { return this.FindElement(Bys.Step1Page.ClinicalCarePlusBtn); } }
        public IWebElement ResearchPlusBtn { get { return this.FindElement(Bys.Step1Page.ResearchPlusBtn); } }
        public IWebElement ScholarshipQualityPlusBtn { get { return this.FindElement(Bys.Step1Page.ScholarshipQualityPlusBtn); } }
        public IWebElement ScholarshipEduPlusBtn { get { return this.FindElement(Bys.Step1Page.ScholarshipEduPlusBtn); } }
        public IWebElement LeaderAdminPlusBtn { get { return this.FindElement(Bys.Step1Page.LeaderAdminPlusBtn); } }
        public IWebElement GoToBottomBtn { get { return this.FindElement(Bys.Step1Page.GoToBottomBtn); } }


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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step1Page.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.Step1Page.PageReady);
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
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.Step1Page.NextBtn))
            {
                if (elem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    NextBtn.Click();
                    this.WaitForInitialize();
                    //Thread.Sleep(500);
                    //Browser.WaitJSAndJQuery();
                    //this.WaitUntilAny(Criteria.Step1Page.StepNumberLabelVisibleAndTextContainsStep);

                    if (PLP_Header_StepNumberLabel.Text.Contains("Step 1"))
                    {
                        Browser.Exists(Bys.Step1Page.NextBtn, ElementCriteria.AttributeValue("aria-disabled", "true"));
                        return this;
                    }
                    else
                    {
                        Step2Page page = new Step2Page(Browser);
                        page.WaitForInitialize();
                        return page;
                    }
                }
            }

            if (Browser.Exists(Bys.Step1Page.ExpandAllBtn))
            {
                if (elem.GetAttribute("outerHTML") == ExpandAllBtn.GetAttribute("outerHTML"))
                {
                    ExpandAllBtn.ClickJS(Browser);
                    this.WaitForInitialize();
                    Browser.WaitForElement(Bys.Step1Page.CollapseAllBtn, ElementCriteria.IsVisible);
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
