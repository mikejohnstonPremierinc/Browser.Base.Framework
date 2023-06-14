using Browser.Core.Framework;
using LMS.Data;
using LMS.AppFramework.LMSHelperMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace LMS.AppFramework
{
    public class ActPIMPage : LMSPage, IDisposable
    {
        #region constructors
        public ActPIMPage(IWebDriver driver) : base(driver)
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

        public IWebElement ThisFieldIsRequiredLbls { get { return this.FindElement(Bys.ActPIMPage.ThisFieldIsRequiredLbls); } }
        public IWebElement PatientScorePercentageLbl { get { return this.FindElement(Bys.ActPIMPage.PatientScorePercentageLbl); } }
        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActPIMPage.ContinueBtn); } }
        public IWebElement SubmitBtn { get { return this.FindElement(Bys.ActPIMPage.SubmitBtn); } }
        public IWebElement FinishBtn { get { return this.FindElement(Bys.ActPIMPage.FinishBtn); } }
        public IWebElement FrontMatterLbl { get { return this.FindElement(Bys.ActPIMPage.ContentHdr); } }
        public IWebElement AssessmentNameLbl { get { return this.FindElement(Bys.ActPIMPage.AssessmentNameLbl); } }
        public IWebElement YourScoreLbl { get { return this.FindElement(Bys.ActPIMPage.YourScoreLbl); } }
        public IWebElement YesIAmSatisfiedRdo { get { return this.FindElement(Bys.ActPIMPage.YesIAmSatisfiedRdo); } }
        public IWebElement SubmitDataBtn { get { return this.FindElement(Bys.ActPIMPage.SubmitDataBtn); } }
        public IList<IWebElement> EvaluationRdoBtnGroups { get { return this.FindElements(Bys.ActPIMPage.EvaluationRdoBtnGroups); } }
        public IList<IWebElement> ClaimCreditChks { get { return this.FindElements(Bys.ActPIMPage.ClaimCreditChks); } }
        public SelectElement SelectCreditSelElem { get { return new SelectElement(this.FindElement(Bys.ActPIMPage.SelectCreditSelElem)); } }
        public IWebElement SelectCreditSelElemBtn { get { return this.FindElement(Bys.ActPIMPage.SelectCreditSelElemBtn); } }
        public IWebElement CalculationsInProgressLbl { get { return this.FindElement(Bys.ActPIMPage.CalculationsInProgressLbl); } }
        public IWebElement OverallTestGroupScoreLbl { get { return this.FindElement(Bys.ActPIMPage.OverallTestGroupScoreLbl); } }
        public IWebElement SectionNameLbl { get { return this.FindElement(Bys.ActPIMPage.SectionNameLbl); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActPIMPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActPIMPage.ContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    bool onLastPageBeforeEval = false;

                    if (Browser.Exists(By.XPath("//*[contains(text(),'has been submitted')]")))
                    {
                        onLastPageBeforeEval = true;
                    }

                    ContinueBtn.ClickJS(Browser);
                    WaitAfterContinue(onLastPageBeforeEval);

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_PIM);
                    // Browser.WaitForElement(Bys.ActPIMPage.YourScoreLbl, ElementCriteria.IsVisible);
                }
            }

            if (Browser.Exists(Bys.ActPIMPage.SubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == SubmitBtn.GetAttribute("outerHTML"))
                {
                    SubmitBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActPIMPage.ContinueBtn, TimeSpan.FromSeconds(180),
                        ElementCriteria.AttributeValue("aria-disabled", "false"));
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_PIM);
                }
            }

            if (Browser.Exists(Bys.ActPIMPage.SubmitDataBtn))
            {
                if (elem.GetAttribute("outerHTML") == SubmitDataBtn.GetAttribute("outerHTML"))
                {
                    SubmitDataBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActPIMPage.DataSubmittedBtn, TimeSpan.FromSeconds(180), ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_PIM);

                }
            }

            if (Browser.Exists(Bys.ActPIMPage.FinishBtn))
            {
                if (elem.GetAttribute("outerHTML") == FinishBtn.GetAttribute("outerHTML"))
                {
                    FinishBtn.Click(Browser);
                    TranscriptPage Page = new TranscriptPage(Browser);
                    Page.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return Page;
                }
            }

            if (Browser.Exists(Bys.ActPIMPage.YesIAmSatisfiedRdo))
            {
                if (elem.GetAttribute("outerHTML") == YesIAmSatisfiedRdo.GetAttribute("outerHTML"))
                {
                    YesIAmSatisfiedRdo.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActPIMPage.ContinueBtn, TimeSpan.FromSeconds(180),
                        ElementCriteria.AttributeValue("aria-disabled", "false"));
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_PIM);

                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// 
        /// </summary>
        private void WaitAfterContinue(bool onLastPageBeforeEval)
        {
            Thread.Sleep(500);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
            Thread.Sleep(500);

            // If we are on the last page before the Evaluation (the Overall Test Group Score page), then we need to wait for the
            // "Calculations in progress" label to disappear and the Continue button to enable
            if (onLastPageBeforeEval)
            {
                Browser.WaitForElement(Bys.ActPIMPage.OverallTestGroupScoreViewCumulativeScoreLbl, TimeSpan.FromSeconds(180), ElementCriteria.IsVisible);
                Browser.WaitForElement(Bys.ActPIMPage.ContinueBtn, TimeSpan.FromSeconds(240), ElementCriteria.AttributeValue("aria-disabled", "false"));
            }

            // Sometimes an Oops error appears, but the code keeps going. We will not catch this Oops
            // error and throw an error
            if (Browser.Exists(Bys.LMSPage.NotificationErrorMessageLbl, ElementCriteria.IsVisible))
            {
                throw new Exception("The PIM page you were on threw an Oops error. See screenshot");
            }
        }

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <param name="selElemBtn">Send the button version of the Select Element only if you encounter an issue where there are 2 Select Elements with the same exact options on the same page</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string selection, IWebElement selElemBtn = null)
        {
            if (Browser.Exists(Bys.ActPIMPage.SelectCreditSelElem))
            {
                if (selectElement.Options[1].Text == SelectCreditSelElem.Options[1].Text
                    && selElemBtn.Text == SelectCreditSelElemBtn.Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, SelectCreditSelElemBtn, selection);
                    }
                    else
                    {
                        SelectCreditSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element."));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActPIMPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Continues through each PIM assessment page either passing or failing every assessment, then arrives at the Certificate 
        /// or Claim Credits page, clicking Finish and landing at the Transcript page.
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="assessmentQandAs">Pass the questions and answers. See the TestData class in LMS.UITest for examples of 
        /// how to build this object</param>
        /// <param name="passPIM">'true' if you ant to pass, else false</param>
        public TranscriptPage SubmitPIM(bool passPIM, Constants.SiteCodes siteCode,
            List<Constants.Assessments> assessments, List<Constants.AssQAndAs> assessmentQandAs)
        {
            TestData TD = new TestData();

            foreach (var assessment in assessments)
            {
                List<Constants.AssQAndAs> assessmentQandCorrectAs = TD.GetAnswerSet(assessmentQandAs, passPIM,
                    ActivityTitleLbl.Text, assessment.AssessmentName);

                PassAssessment(assessment.AssessmentName, assessmentQandAs: assessmentQandCorrectAs);

                if (Browser.Exists(Bys.ActPIMPage.YourScoreLbl, ElementCriteria.IsVisible))
                {
                    if (Browser.Exists(Bys.ActPIMPage.YesIAmSatisfiedRdo, ElementCriteria.IsVisible))
                    {
                        ClickAndWait(YesIAmSatisfiedRdo);
                    }

                    ClickAndWait(ContinueBtn);
                }
            }

            // Click continue on the Overall Test Group Score view
            if (Browser.Exists(Bys.ActPIMPage.SubmitDataBtn, ElementCriteria.IsVisible))
            {
                ClickAndWait(SubmitDataBtn);
                ClickAndWait(ContinueBtn);
                ClickAndWait(ContinueBtn);
            }

            // Complete the evaluation
            // Loop through all radio button groups, Click the first one
            foreach (var rdoBtnGroup in EvaluationRdoBtnGroups)
            {
                rdoBtnGroup.FindElement(By.XPath("descendant::input[@type='radio']")).Click();
            }
            ClickAndWait(SubmitBtn);
            ClickAndWait(ContinueBtn);

            // Claim credit
            // Loop through all check boxes and click each one
            foreach (var ClaimCreditChk in ClaimCreditChks)
            {
                ClaimCreditChk.Click();
                WaitForInitialize();
            }

            // Select the credit amount in the dropdown
            SelectAndWait(SelectCreditSelElem, SelectCreditSelElem.Options[1].Text, SelectCreditSelElemBtn);

            ClickAndWait(ContinueBtn);
            return ClickAndWait(FinishBtn);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="section">The exact text inside the DIV tag from the label of the section you want to expand</param>
        /// <param name="subSection">The exact text inside the span tag from the label of the sub section you want to go to</param>
        public dynamic GoToSubSection(string section, string subSection)
        {
            ExpandOrCollapsePIMSection(section, "Expand");

            string xpath = string.Format("//div[@class='progress-step']//span[text()='{0}']",
                subSection);
            Browser.FindElement(By.XPath(xpath)).Click();

            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

            // Sometimes an Oops error appears, but the code keeps going. We will not catch this Oops
            // error and throw an error
            if (Browser.Exists(Bys.LMSPage.NotificationErrorMessageLbl, ElementCriteria.IsVisible))
            {
                throw new Exception("The PIM page you were on threw an Oops error. See screenshot");
            }

            return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_PIM);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="section">The exact text inside the DIV tag from the label of the section you want to expand</param>
        /// <param name="expandOrCollapse">"Expand" or "Collapse"</param>
        public void ExpandOrCollapsePIMSection(string section, string expandOrCollapse)
        {
            string xpath = string.Format("//div[@class='progress-bar-section-title' and text()='{0}']", section);
            IWebElement expansionCollapseIcon = Browser.FindElement(By.XPath(xpath));
            By parentOfExpandCollapseElem =
                By.XPath(string.Format("//div[@class='progress-bar-section-title' and text()='{0}']/..", section));

            // If tester wants to expand, and its not already expanded
            if (expandOrCollapse == "Expand" &&
                !Browser.Exists(parentOfExpandCollapseElem, ElementCriteria.AttributeValueContains("class", "expanded")))
            {
                expansionCollapseIcon.Click();
                // Wait for the section to be expanded to collapsed (The parent of the element will change to have a class
                // of "expanded"
                Browser.WaitForElement(parentOfExpandCollapseElem, ElementCriteria.AttributeValueContains("class", "expanded"));
            }
            // Else if the tester wants to collapse and the section is expanded
            else if (expandOrCollapse == "Collapse" &&
                Browser.Exists(parentOfExpandCollapseElem, ElementCriteria.AttributeValueContains("class", "expanded")))
            {
                expansionCollapseIcon.Click();
                // Wait for the section to be expanded to collapsed (The parent of the element will change to have a class
                // of "expanded"
                Browser.WaitForElement(parentOfExpandCollapseElem, ElementCriteria.AttributeValueNotContains("class", "expanded"));
            }

            Thread.Sleep(300);
        }

        /// <summary>
        /// Passes an assessment. Specificaly, this chooses the correct answers, clicks the Submit button, then clicks Continue twice
        /// </summary>
        /// <param name="assessmentQandAs">Pass all of the required questions and associated correct answers. To get 
        /// these answer <see cref="TestData.GetAnswerSet(List{Constants.AssQAndAs}, bool, string, string)"/></param>
        public string PassAssessment(string assessmentName, List<Constants.AssQAndAs> assessmentQandAs = null)
        {
            SubmitAssesment(assessmentName, assessmentQandAs);

            ClickAndWait(ContinueBtn);

            return assessmentName;
        }

        /// <summary>
        /// Pass an assessment. Specificaly, this chooses answers of your choice then clicks the Submit button.
        /// </summary>
        /// <param name="assessmentQandAs">Pass all of the required questions and associated answers (correct or incorrect). To get 
        /// these answer <see cref="TestData.GetAnswerSet(List{Constants.AssQAndAs}, bool, string, string)"/></param>
        public string SubmitAssesment(string assessmentName, List<Constants.AssQAndAs> assessmentQandAs = null)
        {
            // Choose an answer for all questions
            foreach (Constants.AssQAndAs questionAndAnswers in assessmentQandAs)
            {
                Help.ChooseAnswerUsingQueryResult(Browser, questionAndAnswers);
            }

            ClickAndWait(SubmitBtn);

            if (Browser.Exists(Bys.ActAssessmentPage.ThisFieldIsRequiredLbls, ElementCriteria.IsVisible))
            {
                throw new Exception("Something went wrong. After clicking the submit button above, there should be no 'This field is " +
                    "required' label, because this method's purpose is to fill in all required answers then submit those answers. " +
                    "So if you got this error, most likely someone updated our activity! Go to CME360 and then make sure the Answer Key " +
                    "tab for all answers is correctly filled in. I have had this occur in the past for multiple choice check boxes. " +
                    "When I checked CME360, someone unchecked the check boxes in the Answer Key");
            }

            ClickAndWait(ContinueBtn);

            return assessmentName;
        }

        /// <summary>
        /// Passes and submits an assessment (if neccessary), then continues to the next assessment or transcript page. Specifically, 
        /// if the assessment is not already passed and submitted, this chooses the correct answers, clicks the Submit button, then 
        /// clicks the Continue (or Finish) button. If the assessment was already submitted and passed, then it will just click the 
        /// Continue (Or Finish) button. It will then wait for the Assessment or Transcript page to load (Depending on if the 
        /// passed/submitted assessment resulted in being the last assessment or not. If last assessment, will go to Transcript page, 
        /// else to next assessment)
        /// </summary>
        /// <param name="alsoAnswerNonGradedNonRequiredQuestions">If you want the non graded, non-required questions to be answered</param>
        /// <param name="attemptNumberRequested">(Optional). If you want to pass the test on a second or third or etc. attempt, then pass in the attempt number</param>
        /// <param name="assessmentQandAs">(PROD-ONLY). PROD-ONLY. Pass the questions and answers if you are running on Prod. See the TestData 
        /// class in LMS.UITest for examples of how to build this object</param>
        public dynamic PassAssessmentIfNotSubmittedAlreadyAndContinueOrReturnToSummary(Constants.Assessments assessment,
            bool alsoAnswerNonGradedNonRequiredQuestions = true, int attemptNumberToPass = 1,
            List<Constants.AssQAndAs> assessmentQandAs = null)
        {
            PassAssessment(assessment.AssessmentName, assessmentQandAs);
            return ClickAndWait(ContinueBtn);
        }

        #endregion methods: page specific



    }
}