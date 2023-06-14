using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;


namespace Mainpro.AppFramework
{
    public class DashboardPage : MainproPage, IDisposable
    {
        #region constructors
        public DashboardPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "cpd/dashboard"; } }

        #endregion properties

        #region elements       

        public IWebElement LearnMoreBtn { get { return this.FindElement(Bys.DashboardPage.LearnMoreBtn); } }
        public IWebElement EnterBtn { get { return this.FindElement(Bys.DashboardPage.EnterBtn); } }
        public IWebElement MainproDashboardPLPCompletePercentageLbl { get { return this.FindElement(Bys.DashboardPage.MainproDashboardPLPCompletePercentageLbl); } }
        public IWebElement MainproDashboardPLPChartWhiteareaLbl { get { return this.FindElement(Bys.DashboardPage.MainproDashboardPLPChartWhiteareaLbl); } }
        public IWebElement MainproDashboardPLPChartGreenareaLbl { get { return this.FindElement(Bys.DashboardPage.MainproDashboardPLPChartGreenareaLbl); } }
        public IWebElement ClickHereToViewAMARCPCreditsLnk { get { return this.FindElement(Bys.DashboardPage.ClickHereToViewAMARCPCreditsLnk); } }

        public IWebElement CredSummaryCurrYrTblTotalReqMetLbl { get { return this.FindElement(Bys.DashboardPage.CredSummaryCurrYrTblTotalReqMetLbl); } }

        public IWebElement CredSummaryCycleTblTotalAppliedLbl { get { return this.FindElement(Bys.DashboardPage.CredSummaryCycleTblTotalAppliedLbl); } }

        public IWebElement CredSummaryCycleTblCertReqLbl { get { return this.FindElement(Bys.DashboardPage.CredSummaryCycleTblCertReqLbl); } }
        public IWebElement CredSummaryCycleTblTotalReqLbl { get { return this.FindElement(Bys.DashboardPage.CredSummaryCycleTblTotalReqLbl); } }

        public IWebElement CredSummaryCycleTblCertReqMetLbl { get { return this.FindElement(Bys.DashboardPage.CredSummaryCycleTblCertReqMetLbl); } }
        public IWebElement CredSummaryCycleTblTotalReqMetLbl { get { return this.FindElement(Bys.DashboardPage.CredSummaryCycleTblTotalReqMetLbl); } }
        public IWebElement AMARCPFormCycleTblRCPReportedCell { get { return this.FindElement(Bys.DashboardPage.AMARCPFormCycleTblRCPReportedCell); } }
        public IWebElement AMARCPFormCycleTblRCPAppliedCell { get { return this.FindElement(Bys.DashboardPage.AMARCPFormCycleTblRCPAppliedCell); } }

        public IWebElement IncompleteActivitiesTblFirstRowActivityCell { get { return this.FindElement(Bys.DashboardPage.IncompleteActivitiesTblFirstRowActivityCell); } }

        public IWebElement AtivitiesNeedCreditApprTblFirstRowActivityCell { get { return this.FindElement(Bys.DashboardPage.AtivitiesNeedCreditApprTblFirstRowActivityCell); } }

        public IWebElement CredSummaryCurrYrTblTotalAppliedLbl { get { return this.FindElement(Bys.DashboardPage.CredSummaryCurrYrTblTotalAppliedLbl); } }
        public IWebElement IncompleteActivitiesTbl { get { return this.FindElement(Bys.DashboardPage.IncompleteActivitiesTbl); } }
        public IWebElement IncompleteActivitiesTblHdr { get { return this.FindElement(Bys.DashboardPage.IncompleteActivitiesTblHdr); } }
        public IWebElement IncompleteActivitiesTblBody { get { return this.FindElement(Bys.DashboardPage.IncompleteActivitiesTblBody); } }
        public IWebElement ActivitiesNeedCreditApprovalTbl { get { return this.FindElement(Bys.DashboardPage.ActivitiesNeedCreditApprovalTbl); } }
        public IWebElement ActivitiesNeedCreditApprovalTblHdr { get { return this.FindElement(Bys.DashboardPage.ActivitiesNeedCreditApprovalTblHdr); } }
        public IWebElement ActivitiesNeedCreditApprovalTblBody { get { return this.FindElement(Bys.DashboardPage.ActivitiesNeedCreditApprovalTblBody); } }
        public IWebElement CredSummaryCycleTblCertAppliedLbl { get { return this.FindElement(Bys.DashboardPage.CredSummaryCycleTblCertAppliedLbl); } }
        public IWebElement PersonalLearnPlanTbl { get { return this.FindElement(Bys.DashboardPage.PersonalLearnPlanTbl); } }
        public IWebElement PersonalLearnPlanTblHdr { get { return this.FindElement(Bys.DashboardPage.PersonalLearnPlanTblHdr); } }
        public IWebElement PersonalLearnPlanTblBody { get { return this.FindElement(Bys.DashboardPage.PersonalLearnPlanTblBody); } }
        public IWebElement PersonalLearnPlanTblFirstRowActivityCell { get { return this.FindElement(Bys.DashboardPage.PersonalLearnPlanTblFirstRowActivityCell); } }
        public IWebElement NoCycleCloseBtn { get { return this.FindElement(Bys.DashboardPage.NoCycleCloseBtn); } }



        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
            this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.DashboardPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
            Thread.Sleep(2000);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.DashboardPage.PageReady);
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
                if(Browser.Exists(Bys.MainproPage.ZendeskChatMinimizeBtn, ElementCriteria.IsVisible))
                ZendeskChatMinimizeBtn.Click();
                Browser.SwitchTo().DefaultContent();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose DashboardPge", activeRequests.Count, ex); }
        }

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.DashboardPage.EnterBtn))
            {
                if (elem.GetAttribute("outerHTML") == EnterBtn.GetAttribute("outerHTML"))
                {
                    EnterBtn.Click();
                    EntryCarouselPathwayPage page = new EntryCarouselPathwayPage(Browser);
                    // Clicking Enter on the Dashboard page for PLP results in a conditional pathway where if the user
                    // never registered to PLP, then the first page (Entry page) of PLP will be shown. If the user already 
                    // registered, it will land the user on the page he was last on. So we have to condition our code for this
                    page.WaitForInitialize();
                    if (Browser.Exists(Bys.EntryCarouselPathwayPage.EnterBtn, ElementCriteria.IsVisible))
                    {
                        return page;
                    }
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
