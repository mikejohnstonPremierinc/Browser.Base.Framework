using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;


namespace Mainpro.AppFramework
{
    public class CreditSummaryPage : MainproPage, IDisposable
    {
        #region constructors
        public CreditSummaryPage(IWebDriver driver) : base(driver)
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
        public IWebElement ViewAllCyclesBtn { get { return this.FindElement(Bys.CreditSummaryPage.ViewAllCyclesBtn); } }
        public IWebElement YouAreCurrentlyViewingPreviousCycleDate { get { return this.FindElement(Bys.CreditSummaryPage.YouAreCurrentlyViewingPreviousCycleDate); } }
        public IWebElement SelfLearningTblBodyRow { get { return this.FindElement(Bys.CreditSummaryPage.SelfLearningTblBodyRow); } }
        public IWebElement SelfLearningTblBody { get { return this.FindElement(Bys.CreditSummaryPage.SelfLearningTblBody); } }
        public IWebElement SelfLearningTblHdr { get { return this.FindElement(Bys.CreditSummaryPage.SelfLearningTblHdr); } }
        public IWebElement SelfLearningTbl { get { return this.FindElement(Bys.CreditSummaryPage.SelfLearningTbl); } }
        public IWebElement GroupLearningTblBodyRow { get { return this.FindElement(Bys.CreditSummaryPage.GroupLearningTblBodyRow); } }
        public IWebElement GroupLearningTblBody { get { return this.FindElement(Bys.CreditSummaryPage.GroupLearningTblBody); } }
        public IWebElement GroupLearningTblHdr { get { return this.FindElement(Bys.CreditSummaryPage.GroupLearningTblHdr); } }
        public IWebElement GroupLearningTbl { get { return this.FindElement(Bys.CreditSummaryPage.GroupLearningTbl); } }
        public IWebElement AnnualRequirementsTblBodyRow { get { return this.FindElement(Bys.CreditSummaryPage.AnnualRequirementsTblBodyRow); } }
        public IWebElement AnnualRequirementsTblBody { get { return this.FindElement(Bys.CreditSummaryPage.AnnualRequirementsTblBody); } }
        public IWebElement AnnualRequirementsTblHdr { get { return this.FindElement(Bys.CreditSummaryPage.AnnualRequirementsTblHdr); } }
        public IWebElement AnnualRequirementsTbl { get { return this.FindElement(Bys.CreditSummaryPage.AnnualRequirementsTbl); } }
        public IWebElement OtherTblBodyRow { get { return this.FindElement(Bys.CreditSummaryPage.OtherTblBodyRow); } }
        public IWebElement OtherTblBody { get { return this.FindElement(Bys.CreditSummaryPage.OtherTblBody); } }
        public IWebElement OtherTblHdr { get { return this.FindElement(Bys.CreditSummaryPage.OtherTblHdr); } }
        public IWebElement OtherTbl { get { return this.FindElement(Bys.CreditSummaryPage.OtherTbl); } }
        public IWebElement AssessmentTblBodyRow { get { return this.FindElement(Bys.CreditSummaryPage.AssessmentTblBodyRow); } }
        public IWebElement AssessmentTblBody { get { return this.FindElement(Bys.CreditSummaryPage.AssessmentTblBody); } }
        public IWebElement AssessmentTblHdr { get { return this.FindElement(Bys.CreditSummaryPage.AssessmentTblHdr); } }
        public IWebElement AssessmentTbl { get { return this.FindElement(Bys.CreditSummaryPage.AssessmentTbl); } }
        public IWebElement ViewAllCyclesFormCyclesTblBodyRow { get { return this.FindElement(Bys.CreditSummaryPage.ViewAllCyclesFormCyclesTblBodyRow); } }
        public IWebElement ViewAllCyclesFormCyclesTblBody { get { return this.FindElement(Bys.CreditSummaryPage.ViewAllCyclesFormCyclesTblBody); } }
        public IWebElement ViewAllCyclesFormCyclesTblHdr { get { return this.FindElement(Bys.CreditSummaryPage.ViewAllCyclesFormCyclesTblHdr); } }
        public IWebElement ViewAllCyclesFormCyclesTbl { get { return this.FindElement(Bys.CreditSummaryPage.ViewAllCyclesFormCyclesTbl); } }
        public IWebElement ViewAllCyclesFormCloseBtn { get { return this.FindElement(Bys.CreditSummaryPage.ViewAllCyclesFormCloseBtn); } }
        public IWebElement CSViewFormViewActivitiesTbl { get { return this.FindElement(Bys.CreditSummaryPage.CSViewFormViewActivitiesTbl); } }
        public IWebElement CSViewFormViewActivitiesTblBody { get { return this.FindElement(Bys.CreditSummaryPage.CSViewFormViewActivitiesTblBody); } }
        public IWebElement CSViewFormViewActivitiesTblBodyRow { get { return this.FindElement(Bys.CreditSummaryPage.CSViewFormViewActivitiesTblBodyRow); } }
        public IWebElement CSViewFormViewActivitiesTblHdr { get { return this.FindElement(Bys.CreditSummaryPage.CSViewFormViewActivitiesTblHdr); } }
        public IWebElement CSViewFormViewActivitiesCloseBtn { get { return this.FindElement(Bys.CreditSummaryPage.CSViewFormViewActivitiesCloseBtn); } }

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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CreditSummaryPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.CreditSummaryPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
            if (Browser.Exists(Bys.MainproPage.ZendeskChatFrame, ElementCriteria.IsVisible))
            {
                Browser.SwitchTo().Frame(ZendeskChatFrame);
                if (Browser.Exists(Bys.MainproPage.ZendeskChatMinimizeBtn, ElementCriteria.IsVisible))
                    ZendeskChatMinimizeBtn.Click();
                Browser.SwitchTo().DefaultContent();
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
            if (Browser.Exists(Bys.CreditSummaryPage.ViewAllCyclesBtn))
            {
                if (elem.GetAttribute("outerHTML") == ViewAllCyclesBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.CreditSummaryPage.ViewAllCyclesFormCloseBtn, ElementCriteria.IsVisible);
                    Browser.WaitForElement(Bys.CreditSummaryPage.ViewAllCyclesFormCyclesTbl, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.CreditSummaryPage.CSViewFormViewActivitiesCloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == CSViewFormViewActivitiesCloseBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.RefreshPage(true);
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        #endregion methods: repeated per page

        #region methods: page specific



        #endregion methods: page specific


    }
}
