using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public class StepPRPage : MainproPage, IDisposable
    {
        #region constructors
        public StepPRPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        MainproHelperMethods Help = new MainproHelperMethods();
        public override string PageUrl { get { return "login.aspx?action=enablelogin"; } }

        #endregion properties

        #region elements            
        public IWebElement NextBtn { get { return this.FindElement(Bys.StepPRPage.NextBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.StepPRPage.BackBtn); } }
        public IWebElement ExpandAllBtn { get { return this.FindElement(Bys.StepPRPage.ExpandAllBtn); } }
        public IWebElement CollapseAllBtn { get { return this.FindElement(Bys.StepPRPage.CollapseAllBtn); } }        
        public IWebElement GoToBottomBtn { get { return this.FindElement(Bys.StepPRPage.GoToBottomBtn); } }
        public IWebElement SubmitBtn { get { return this.FindElement(Bys.StepPRPage.SubmitBtn); } }
        public IWebElement PLPCertificateBtn { get { return this.FindElement(Bys.StepPRPage.PLPCertificateBtn); } }
        public IWebElement printPLPCertCloseBtn { get { return this.FindElement(Bys.StepPRPage.printPLPCertCloseBtn); } }
        public IWebElement printPLPCompleteCloseButton { get { return this.FindElement(Bys.StepPRPage.printPLPCompleteCloseButton); } }
        public IWebElement PrintPLPCertificateDownloadBtn { get { return this.FindElement(Bys.StepPRPage.PrintPLPCertificateDownloadBtn); } }
        public IWebElement PrintmycompletedPLPDownloadBtn { get { return this.FindElement(Bys.StepPRPage.PrintmycompletedPLPDownloadBtn); } }
        public IWebElement StartanewPLPBtn { get { return this.FindElement(Bys.StepPRPage.StartanewPLPBtn); } }
        public IWebElement RecommendPLPtoacolleagueBtn { get { return this.FindElement(Bys.StepPRPage.RecommendPLPtoacolleagueBtn); } }
        public IWebElement PrintmycompletedPLPBtn { get { return this.FindElement(Bys.StepPRPage.PrintmycompletedPLPBtn); } }
        public IWebElement ContactUsImg { get { return this.FindElement(Bys.StepPRPage.ContactUsImg); } }
        public IWebElement ExitPLPBtn { get { return this.FindElement(Bys.StepPRPage.ExitPLPBtn); } }
         public IWebElement RecommendMailIconBtn { get { return this.FindElement(Bys.StepPRPage.RecommendMailIconBtn); } }
        public IWebElement EmailToColleagueModalPopupCloseBtn { get { return this.FindElement(Bys.StepPRPage.EmailToColleagueModalPopupCloseBtn); } }
        public IWebElement EmailToColleagueModalPopupTxt { get { return this.FindElement(Bys.StepPRPage.EmailToColleagueModalPopupTxt); } }
        public IWebElement AdditionalFeedbackDetailTxt { get { return this.FindElement(Bys.StepPRPage.AdditionalFeedbackDetailTxt); } }
         public IWebElement SubmissionContentTxt { get { return this.FindElement(Bys.StepPRPage.SubmissionContentTxt); } }

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
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.StepPRPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.StepPRPage.PageReady);
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
            if (Browser.Exists(Bys.StepPRPage.NextBtn))
            {
                if (elem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    NextBtn.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.BackBtn))
            {
                if (elem.GetAttribute("outerHTML") == BackBtn.GetAttribute("outerHTML"))
                {
                    BackBtn.Click();
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.SubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == SubmitBtn.GetAttribute("outerHTML"))
                {
                    SubmitBtn.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.RecommendMailIconBtn))
            {
                if (elem.GetAttribute("outerHTML") == RecommendMailIconBtn.GetAttribute("outerHTML"))
                {
                    Browser.WaitJSAndJQuery();
                    Browser.WaitForElement(Bys.StepPRPage.RecommendMailIconBtn,
                        TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                    RecommendMailIconBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.StepPRPage.EmailToColleagueModalPopupTxt,
                        TimeSpan.FromSeconds(30),
                        ElementCriteria.TextContains("used a new CPD resource called the Professional Learning Plan"));
                    EmailToColleagueModalPopupCloseBtn.Click();
                    Browser.WaitForElement(Bys.StepPRPage.RecommendMailIconBtn,
                        TimeSpan.FromSeconds(60), ElementCriteria.IsEnabled);
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.RecommendPLPtoacolleagueBtn))
            {
                if (elem.GetAttribute("outerHTML") == RecommendPLPtoacolleagueBtn.GetAttribute("outerHTML"))
                {
                    Browser.WaitJSAndJQuery();
                    elem.ClickJS(Browser);
                    Browser.WaitForElement(Bys.StepPRPage.EmailToColleagueModalPopupTxt,
                        TimeSpan.FromSeconds(30),
                        ElementCriteria.TextContains("used a new CPD resource called the Professional Learning Plan"));
                    EmailToColleagueModalPopupCloseBtn.Click();
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.ContactUsImg))
            {
                if (elem.GetAttribute("outerHTML") ==ContactUsImg.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.MainproPage.SupportInfoFormSupportInfoLbl,
                        ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            
            if (Browser.Exists(Bys.StepPRPage.PrintmycompletedPLPBtn))
            {
                if (elem.GetAttribute("outerHTML") == PrintmycompletedPLPBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.StepPRPage.PrintmycompletedPLPDownloadBtn, TimeSpan.FromSeconds(180),
                         ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.MainproPage.PLP_Menu_PrintCompletedPLP))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Menu_PrintCompletedPLP.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.StepPRPage.PrintmycompletedPLPDownloadBtn, TimeSpan.FromSeconds(180),
                         ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.PrintmycompletedPLPDownloadBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == PrintmycompletedPLPDownloadBtn.GetAttribute("outerHTML"))
                {
                    WindowAndFrameUtils.ClickOnLinkAndSwitchToWindow(Browser, PrintmycompletedPLPDownloadBtn,
                        URLToWaitFor: urlToWaitFor,
                        timeToWaitForURL: TimeSpan.FromSeconds(120));
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.printPLPCompleteCloseButton))
            {
                if (elem.GetAttribute("outerHTML") == printPLPCompleteCloseButton.GetAttribute("outerHTML"))
                {
                    elem.ClickJS(Browser);
                    Browser.WaitForElement(Bys.StepPRPage.printPLPCompleteCloseButton, TimeSpan.FromSeconds(180),
                         ElementCriteria.IsNotVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.MainproPage.PLP_Menu_PrintPLPCertificate))
            {
                if (elem.GetAttribute("outerHTML") == PLP_Menu_PrintPLPCertificate.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.StepPRPage.PrintPLPCertificateDownloadBtn, TimeSpan.FromSeconds(180),
                         ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.PLPCertificateBtn))
            {
                if (elem.GetAttribute("outerHTML") == PLPCertificateBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.StepPRPage.PrintPLPCertificateDownloadBtn, TimeSpan.FromSeconds(180),
                         ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.PrintPLPCertificateDownloadBtn, ElementCriteria.IsVisible))
            {
                if (elem.GetAttribute("outerHTML") == PrintPLPCertificateDownloadBtn.GetAttribute("outerHTML"))
                {
                    WindowAndFrameUtils.ClickOnLinkAndSwitchToWindow(Browser, PrintPLPCertificateDownloadBtn,
                        URLToWaitFor: urlToWaitFor,
                        timeToWaitForURL: TimeSpan.FromSeconds(120));
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.printPLPCertCloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == printPLPCertCloseBtn.GetAttribute("outerHTML"))
                {
                    elem.ClickJS(Browser);
                    Browser.WaitForElement(Bys.StepPRPage.printPLPCertCloseBtn, TimeSpan.FromSeconds(180),
                         ElementCriteria.IsNotVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }
            if (Browser.Exists(Bys.StepPRPage.StartanewPLPBtn))
            {
                if (elem.GetAttribute("outerHTML") == StartanewPLPBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    DashboardPage DP = new DashboardPage(Browser);
                    DP.WaitForInitialize();
                    return DP;
                }
            } if (Browser.Exists(Bys.StepPRPage.ExitPLPBtn))
            {
                if (elem.GetAttribute("outerHTML") == ExitPLPBtn.GetAttribute("outerHTML"))
                {
                    ExitPLPBtn.Click();
                    DashboardPage DP = new DashboardPage(Browser);
                    DP.WaitForInitialize();
                    return DP;
                }
            }


            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }



        #endregion methods: per page

        #region methods: page specific

        /// <summary>
        /// Fill Step6 all screens by choosing Yes and adding comments
        /// </summary>
        /// <param name="Browser"></param>
        public dynamic FillStep6_YesFlow(IWebDriver Browser, bool isReturnFromOverallCompletionScreen= false)
        {
            ClickAndWait(NextBtn);
            FillImpactOnPractice(Browser);
            ClickAndWait(NextBtn);

            //Did your PLP help you achieve your learning goal(s)?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            ClickAndWait(NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            ClickAndWait(NextBtn);

            //Please elaborate and describe in detail.
            FillElborateDetail(Browser);
            ClickAndWait(NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            FillOutcomeModifications(Browser);
            ClickAndWait(NextBtn);

            //Would you recommend this learning activity to a colleague?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            ClickAndWait(RecommendMailIconBtn);
            ClickAndWait(NextBtn);

            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            ElemSet.TextBox_EnterText(Browser, AdditionalFeedbackDetailTxt,true, "Yes Feedback");
            ClickAndWait(NextBtn);

            //Congratulations! You have completed your PLP!
            ClickAndWait(SubmitBtn);

            //overallplpcompletion
            if (isReturnFromOverallCompletionScreen)
            {
                return null;
            }
            else
            {
                ClickAndWait(ExitPLPBtn);
                return null;
            }

        }

        public void FillImpactOnPractice(IWebDriver Browser)
        {
            Assert.True(Browser.Url.Contains("impactonpractice"), "Step 6> Impact on practice > screen not loaded");
            Help.PLP_AddFormattedText(Browser,  "Testing Impact on Practice",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritecolonmax1000Txt);
        }
        public void FillElborateDetail(IWebDriver Browser)
        {
            Assert.True(Browser.Url.Contains("elaborateimprovementsindata"), "Step 6> Elaborate your improvements in Detail > screen not loaded");
            Help.PLP_AddFormattedText(Browser,  "Testing Elborate Detail",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritecolonmax1000Txt);
        }
        public void FillOutcomeModifications(IWebDriver Browser)
        {
            Assert.True(Browser.Url.Contains("outcomemodifications"), "Step 6> Outcome modifications required for future PLP planing > screen not loaded");
            Help.PLP_AddFormattedText(Browser, "Testing Outcome Modifications",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritecolonmax1000Txt);
        }
        public void FillDescribeWhatHelpedFollowGoals(IWebDriver Browser)
        {
            Assert.True(Browser.Url.Contains("followthroughdescription"), "Step 6> what would have helped you follow through with your learning goal(s) > screen not loaded");
            Help.PLP_AddFormattedText(Browser, "Testing Helped follow Learning Goals",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritecolonmax1000Txt);
        }
        public void FillImproveOutcomeDetail(IWebDriver Browser)
        {
            Assert.True(Browser.Url.Contains("improveoutcomesdetail"), "Step 6> Describe what helped improve your outcomes > screen not loaded");
            Help.PLP_AddFormattedText(Browser,"Testing Helped follow Learning Goals",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritecolonmax1000Txt);
        }
        public void Selectif_PLPHelpedAchievedGoal(String OptionToBeSelected) {
            Assert.True(Browser.Url.Contains("activityhelpfollowgoals"), "Step 6> PLP helped achieved learning Goals > screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, OptionToBeSelected);
        }
        public void Selectif_ImprovementsInData(String OptionToBeSelected)
        {
            Assert.True(Browser.Url.Contains("improvementsindata"), "Step 6> PLP improvements in Data > screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, OptionToBeSelected);
        }
        public void Selectif_RecommendToCollegue(String OptionToBeSelected)
        {
            Assert.True(Browser.Url.Contains("colleaguerecommendation"), "Step 6> Recommend to colleugue > screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, OptionToBeSelected);
            if (OptionToBeSelected == "Yes") { ClickAndWait(RecommendMailIconBtn); }
        }
        public void Selectif_AnyAdditionalComments(String OptionToBeSelected) {
            Assert.True(Browser.Url.Contains("generalfeedback"), "Step 6>Additional Comments > screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, OptionToBeSelected);
            if (OptionToBeSelected == "Yes") {
                ElemSet.TextBox_EnterText(Browser, AdditionalFeedbackDetailTxt, true, "Testing Feedback Comments");
            }
            
        }
        #endregion methods: page specific



    }
}
