using Browser.Core.Framework;
using LMS.Data;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using LMS.AppFramework.LMSHelperMethods;
using LOG4NET = log4net.ILog;
//using System.Runtime.Remoting;

namespace LMS.AppFramework
{
    public class ActAssessmentPage : LMSPage, IDisposable
    {
        #region constructors
        public ActAssessmentPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        LMSHelperMethods.LMSHelperMethods Help = new LMSHelperMethods.LMSHelperMethods();

        public override string PageUrl { get { return "lms/activity_pretest"; } }
        //public string assessmentNumOfAttemptsLabelXpath { get { return string.Format("lms/activity_pretest", ); } }

        #endregion properties

        #region elements

        public IWebElement SubmitBtn { get { return this.FindElement(Bys.ActAssessmentPage.SubmitBtn); } }
        public IWebElement BackBtn { get { return this.FindElement(Bys.ActAssessmentPage.BackBtn); } }
        public IList<IWebElement> QuestionTextHiddenLbls { get { return this.FindElements(Bys.ActAssessmentPage.QuestionTextHiddenLbls); } }
        public IList<IWebElement> QuestionTextLbls { get { return this.FindElements(Bys.ActAssessmentPage.QuestionTextLbls); } }
        public IList<IWebElement> QuestionTextNotHiddenLbls { get { return this.FindElements(Bys.ActAssessmentPage.QuestionTextNotHiddenLbls); } }
        public IList<IWebElement> StatusPassFailLbls { get { return this.FindElements(Bys.ActAssessmentPage.StatusPassFailLbls); } }
        public IWebElement NumberOfAttemptsRemainingLbl { get { return this.FindElement(Bys.ActAssessmentPage.NumberOfAttemptsRemainingLbl); } }
        public IWebElement AttemptValueLbl { get { return this.FindElement(Bys.ActAssessmentPage.AttemptValueLbl); } }
        public IWebElement YourScoreValueLbl { get { return this.FindElement(Bys.ActAssessmentPage.YourScoreValueLbl); } }
        public IWebElement YourStatusValueLbl { get { return this.FindElement(Bys.ActAssessmentPage.YourStatusValueLbl); } }
        public IList<IWebElement> AssessmentNameLbls { get { return this.FindElements(Bys.ActAssessmentPage.AssessmentNameLbl); } }
        public IWebElement RetakeBtn { get { return this.FindElement(Bys.ActAssessmentPage.RetakeBtn); } }
        public IWebElement ContinueBtn { get { return this.FindElement(Bys.ActAssessmentPage.ContinueBtn); } }
        public IWebElement FinishBtn { get { return this.FindElement(Bys.ActAssessmentPage.FinishBtn); } }
        public IWebElement Mobile_AssessmentListExpandBtn { get { return this.FindElement(Bys.ActAssessmentPage.Mobile_AssessmentListExpandBtn); } }
        public IWebElement LaunchBtn { get { return this.FindElement(Bys.ActAssessmentPage.LaunchBtn); } }
        public IWebElement NextBtn { get { return this.FindElement(Bys.ActAssessmentPage.NextBtn); } }
        public IWebElement SaveAndFinishLaterBtn { get { return this.FindElement(Bys.ActAssessmentPage.SaveAndFinishLaterBtn); } }
        public IWebElement ReturnToSummaryBtn { get { return this.FindElement(Bys.ActAssessmentPage.ReturnToSummaryBtn); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            ExpandForMobile();

            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActAssessmentPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

            // Weird issue in IE only. Whenever the browser click answers on the assessment for certain assessments, it has to scroll to them
            // first. When this happens in IE, the left-side
            // assessment navigator widget thing floats to the middle of the page and also the page cuts off the top half, rendering all
            // elements on the top half as being stale/not existing. Unable to reproduce manually. To see this, uncomment out the below and
            // runt he Bundle_HideFromTranscript test. As a workaround, we are forcing a scroll to the bottom of the page before, which 
            // resolves this issue. Monitor going forward if this fixes all future instances
            if (Browser.GetCapabilities().HasCapability("browserName"))
            {
                if (Browser.GetCapabilities().GetCapability("browserName").ToString() == "internet explorer")
                {
                    Browser.ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                }
            }
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActAssessmentPage.ContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    ContinueBtn.ClickJS(Browser);
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
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Claim_Credit.GetDescription())
                                || Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription());
                    });

                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    // If this click takes us to another assessment
                    if (Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription()))
                    {
                        this.WaitForInitialize();
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                        return null;
                    }

                    // Else if this click takes us to the Claim Credit page
                    else if (Browser.Url.Contains(Constants.PageURLs.Activity_Claim_Credit.GetDescription()))
                    {
                        ActClaimCreditPage Page = new ActClaimCreditPage(Browser);
                        Page.WaitForInitialize();
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                        return Page;
                    }

                    // Else if this click takes us to the Certificate page
                    else if (Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription()))
                    {
                        ActCertificatePage Page = new ActCertificatePage(Browser);
                        Page.WaitForInitialize();
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                        return Page;
                    }
                }
            }

            if (Browser.Exists(Bys.ActAssessmentPage.SubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == SubmitBtn.GetAttribute("outerHTML"))
                {
                    SubmitBtn.ClickJS(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    // If this is a single assessment, then we wait for the Assessment Name label to reload. If this is a multi-assessment,
                    // then the assessment details container (which stores the Assessment Name label) will not appear because multi-assessment 
                    // make you 'launch' into sub pages. So we have to wait for any of the below 2 conditions to come true
                    this.WaitUntilAny(Criteria.ActAssessmentPage.ReturnToSummaryBtnVisible, Criteria.ActAssessmentPage.AssessmentNameLblVisible);
                    ExpandForMobile();

                    // See above comment about multi vs single assessments causing the user to be inside different windows. Because of this
                    // we will condition below to do 2 different waits
                    if (Browser.Exists(Bys.ActAssessmentPage.ReturnToSummaryBtn, ElementCriteria.IsVisible))
                    {

                    }
                    else if (Browser.Exists(Bys.ActAssessmentPage.AssessmentNameLbl, ElementCriteria.IsVisible))
                    {
                        this.WaitForInitialize();
                    }

                    //this.WaitUntilAny(Criteria.ActAssessmentPage.RetakeBtnVisible, Criteria.ActAssessmentPage.ContinueBtnEnabled,
                    //    Criteria.ActAssessmentPage.YourStatusValueLblTextEqualsSubmitted, Criteria.ActAssessmentPage.FinishBtnVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentPage.RetakeBtn))
            {
                if (elem.GetAttribute("outerHTML") == RetakeBtn.GetAttribute("outerHTML"))
                {
                    RetakeBtn.ClickJS(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    Browser.WaitForElement(Bys.ActAssessmentPage.AssessmentNameLbl, TimeSpan.FromSeconds(90), ElementCriteria.IsVisible);
                    // Commenting next line out and using WaitForInitialize instead. I added more OR conditions in PageReady. See if that
                    // works out
                    //this.WaitUntilAny(Criteria.ActAssessmentPage.LoadIconNotExists, Criteria.ActAssessmentPage.SubmitBtnVisible);
                    this.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentPage.FinishBtn))
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

            if (Browser.Exists(Bys.ActAssessmentPage.LaunchBtn))
            {
                if (elem.GetAttribute("outerHTML") == LaunchBtn.GetAttribute("outerHTML"))
                {
                    LaunchBtn.Click(Browser);
                    this.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return this;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentPage.NextBtn))
            {
                if (elem.GetAttribute("outerHTML") == NextBtn.GetAttribute("outerHTML"))
                {
                    NextBtn.Click(Browser);
                    Thread.Sleep(500);
                    this.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return this;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentPage.ReturnToSummaryBtn))
            {
                if (elem.GetAttribute("outerHTML") == ReturnToSummaryBtn.GetAttribute("outerHTML"))
                {
                    ReturnToSummaryBtn.Click(Browser);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    this.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return this;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentPage.SaveAndFinishLaterBtn))
            {
                if (elem.GetAttribute("outerHTML") == SaveAndFinishLaterBtn.GetAttribute("outerHTML"))
                {
                    SaveAndFinishLaterBtn.Click(Browser);
                    Thread.Sleep(500);
                    this.WaitForInitialize();
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return this;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentPage.BackBtn))
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
                        return Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription());
                    });

                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    ActOverviewPage AOP = new ActOverviewPage(Browser);
                    AOP.WaitForInitialize();
                    return AOP;
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
        /// Checks to see if the user already submitted this assessment and has zero attempts left. If so, throw error
        /// </summary>
        public void ThrowErrorIfAttemptsExhausted(string assessmentName = null)
        {
            IWebElement AssessmentContainer = GetAssessmentContainer(assessmentName);

            dynamic numberOfAttemptsLeft = GetNumberOfAttemptsLeft(assessmentName);

            if (numberOfAttemptsLeft is int)
            {
                // If failed with zero attempts left
                if (AssessmentContainer.FindElement(Bys.ActAssessmentPage.YourStatusValueLbl).Text.ToLower() == "SUBMITTED".ToLower()
                && AssessmentContainer.FindElement(Bys.ActAssessmentPage.StatusPassFailLbls).Text.ToLower() == "FAILED".ToLower()
                && numberOfAttemptsLeft == 0)
                {
                    throw new Exception("You have failed this assessment already and have 0 attempts remaining");
                }

                // If passed with zero attempts left
                else if (AssessmentContainer.FindElement(Bys.ActAssessmentPage.YourStatusValueLbl).Text.ToLower() == "SUBMITTED".ToLower()
                    && numberOfAttemptsLeft == 0)
                {
                    if (AssessmentContainer.FindElement(Bys.ActAssessmentPage.StatusPassFailLbls).Text.ToLower() == "COMPLETED".ToLower()
                    && AssessmentContainer.FindElement(Bys.ActAssessmentPage.StatusPassFailLbls).Text.ToLower() == "PASSED".ToLower())
                    {
                        throw new Exception("You have passed/completed this assessment already and have 0 attempts remaining");
                    }
                }
            }
        }

        /// <summary>
        /// Returns the assessment container element
        /// </summary>
        /// <param name="assessmentName"></param>
        /// <returns></returns>
        public IWebElement GetAssessmentContainer(string assessmentName = null)
        {
            IWebElement assessmentContainer = null;

            if (string.IsNullOrEmpty(assessmentName))
            {
                assessmentContainer = Browser.FindElement(By.XPath("//h3/ancestor::div[contains(@class, 'assessment-list-item ')]"));
            }
            else
            {
                string xpath = string.Format("//h3[text()='{0}']/ancestor::div[contains(@class, 'assessment-list-item ')]", assessmentName);
                assessmentContainer = Browser.FindElement(
                    By.XPath(xpath));
            }

            return assessmentContainer;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="assessmentName"></param>
        ///// <returns></returns>
        //public IWebElement GetSpecificAssessmentChildElemXpath(string assessmentName, Constants.AssessmentElementLabels assessmentElementLabel)
        //{
        //    string baseXpath = string.Format("//h3[text()='{0}']/ancestor::div[contains(@class, 'assessment-list-item ')]", assessmentName);
        //    string finalXpath = null;

        //    switch (assessmentElementLabel)
        //    {
        //        case Constants.AssessmentElementLabels.StatusPassFailLbl:
        //            finalXpath = string.Format("{0}//div[@data-fe-repeat='assessmentCredits']/div[4]", baseXpath);
        //            break;

        //        case Constants.AssessmentElementLabels.YourStatusValueLbl:
        //            finalXpath = string.Format("{0}//div[text()='Your Status']/following-sibling::div", baseXpath);
        //            break;

        //        case Constants.AssessmentElementLabels.NumOfAttemptsLeftValueLbl:
        //            finalXpath = string.Format("{0}//span[text()='Attempt:']/following-sibling::span[contains(text(), 'attempts')]", baseXpath);
        //            break;

        //        case Constants.AssessmentElementLabels.LaunchBtn:
        //            finalXpath = string.Format("{0}//span[text()='Attempt:']/following-sibling::span[contains(text(), 'attempts')]", baseXpath);
        //            break;
        //    }

        //    return Browser.FindElement(By.XPath(finalXpath));
        //}

        /// <summary>
        /// 80, 70, 90
        /// </summary>
        /// <param name="percentageRequested_Target"></param>
        public string CompleteAssessmentWithSpecificPercentage(decimal percentageRequested_Target, decimal percentageRequested_Min,
            decimal percentageRequested_Max, string assessmentName = null)
        {
            ThrowErrorIfAttemptsExhausted(assessmentName);

            // If the tester previously failed an attempt, then we need to click Retake first
            if (Browser.Exists(Bys.ActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible))
            {
                ClickAndWait(RetakeBtn);
            }

            // Get any non-gradable required questions and answer them first
            var questionsAndAnswersInfo_NonGradable = DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(),
                Help.GetActivityIDFromURL(Browser), AssessmentNameLbls[0].Text, isQuestionGraded: false, isAnswerCorrect: null,
                isAnswerRequired: true, returnDistinctQuestionsWithOneAnswer: true);

            Help.ChooseAllAnswersUsingQueryResult(Browser, questionsAndAnswersInfo_NonGradable);

            // Get the distinct gradable questions with correct 'stuffed' answers (i.e. The query will return only 1 row 
            // per question, and if that question has multiple correct answers (i.e. the ChoiceMultipleAnswers questionType), 
            // the query result will have the AnswerText field combined with those answers)
            var questionsAndCorrectAnswersInfo_Gradable = DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(),
                Help.GetActivityIDFromURL(Browser), AssessmentNameLbls[0].Text, isQuestionGraded: true, isAnswerCorrect: true,
                isAnswerRequired: null, returnDistinctQuestionsWithStuffedAnswers: true);

            // Get the gradable questions and incorrect answers
            var questionsAndIncorrectAnswersInfo_Gradable = DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(),
                Help.GetActivityIDFromURL(Browser), AssessmentNameLbls[0].Text, isQuestionGraded: true, isAnswerCorrect: false,
                isAnswerRequired: null, returnDistinctQuestionsWithOneAnswer: true);

            if (questionsAndIncorrectAnswersInfo_Gradable is null)
            {
                throw new Exception("No questions were found with the query. Check to make sure your activity is configured properly " +
                    "in CME360");
            }

            // Get the number of correct answers needed to meet the tester-specified percentage. i.e. If there are 25 questions, and tester
            // specified he/she wants 80%, then the calc would be 80/100*25=20. 20 questions should be answered correctly
            decimal numberOfCorrectAnswersNeeded_Precise = percentageRequested_Target / 100 * questionsAndCorrectAnswersInfo_Gradable.Count;

            // If the number of correct answers resulting from the above calculation is a whole number, then choose this number of 
            // correct answers
            if ((numberOfCorrectAnswersNeeded_Precise % 1) == 0)
            {
                for (int i = 0; i < numberOfCorrectAnswersNeeded_Precise; i++)
                {
                    Help.ChooseAnswerUsingQueryResult(Browser, questionsAndCorrectAnswersInfo_Gradable[i]);
                }

                for (int i = Convert.ToInt32(numberOfCorrectAnswersNeeded_Precise); i < questionsAndIncorrectAnswersInfo_Gradable.Count; i++)
                {
                    Help.ChooseAnswerUsingQueryResult(Browser, questionsAndIncorrectAnswersInfo_Gradable[i]);
                }
            }

            // Else the number is not a whole number, so we have to round down and also round up, and then get the percentage of the rounded
            // numbers, then determine if either of those are between the tester-specifieds minimum or maximum required percentages
            else
            {
                // Get the number of correct answers after rounding down and also up
                decimal numberOfCorrectAnswersRounded_floor = Math.Floor(numberOfCorrectAnswersNeeded_Precise);
                decimal numberOfCorrectAnswersRounded_Ceiling = Math.Ceiling(numberOfCorrectAnswersNeeded_Precise);

                // Get the percentage that can be achieved from the rounded number of answers
                decimal MaximumPercentageObtainableAtFloor = numberOfCorrectAnswersRounded_floor / questionsAndCorrectAnswersInfo_Gradable.Count * 100;
                decimal MinimumPercentageObtainableAtCeiling = numberOfCorrectAnswersRounded_Ceiling / questionsAndCorrectAnswersInfo_Gradable.Count * 100;

                // If the percentage from the rounded down number of correct answers to select is above the minimum percentage allowed, then
                // choose this number of correct answers
                if (percentageRequested_Min <= MaximumPercentageObtainableAtFloor)
                {
                    for (int i = 0; i < numberOfCorrectAnswersRounded_floor; i++)
                    {
                        Help.ChooseAnswerUsingQueryResult(Browser, questionsAndCorrectAnswersInfo_Gradable[i]);
                    }

                    for (int i = Convert.ToInt32(numberOfCorrectAnswersRounded_floor); i < questionsAndIncorrectAnswersInfo_Gradable.Count; i++)
                    {
                        Help.ChooseAnswerUsingQueryResult(Browser, questionsAndIncorrectAnswersInfo_Gradable[i]);
                    }
                }
                // If the percentage from the rounded up number of correct answers to select is below the maximum percentage allowed, then 
                // choose this number of correct answers
                else if (percentageRequested_Max >= MinimumPercentageObtainableAtCeiling)
                {
                    for (int i = 0; i < numberOfCorrectAnswersRounded_Ceiling; i++)
                    {
                        Help.ChooseAnswerUsingQueryResult(Browser, questionsAndCorrectAnswersInfo_Gradable[i]);
                    }

                    for (int i = Convert.ToInt32(numberOfCorrectAnswersRounded_Ceiling); i < questionsAndIncorrectAnswersInfo_Gradable.Count; i++)
                    {
                        Help.ChooseAnswerUsingQueryResult(Browser, questionsAndIncorrectAnswersInfo_Gradable[i]);
                    }
                }
                else
                {
                    throw new Exception(string.Format("Your specified a target percentage of {0}, and you specified it can not be lower than {1}, " +
                        "or higher than {2}. Due to the number of graded questions, which is {3}, it is not possible to meet your percentage " +
                        "requirements. ",
                        percentageRequested_Target.ToString(), percentageRequested_Min.ToString(), percentageRequested_Max.ToString(),
                        questionsAndCorrectAnswersInfo_Gradable.Count.ToString()));
                }
            }

            ClickAndWait(SubmitBtn);

            return assessmentName;
        }

        /// <summary>
        /// Pass an assessment. Specificaly, this chooses the correct answers then clicks the Submit button.
        /// </summary>
        /// <param name="alsoAnswerNonGradedNonRequiredQuestions">If you want the non graded, non-required questions to be answered</param>
        /// <param name="attemptNumberToPass">(Optional). If you want to pass the test on a second or third or etc. attempt, then pass in the
        /// attempt number</param>
        /// <param name="assessmentQandAs">(PROD-ONLY). Pass all of the required questions and associated correct answers if you are
        /// running on Prod. If the question has multiple correct answers, inside the AnswerText field, pass those answers separated by a 
        /// comma-space. If you want the test to answer the non-graded non-required questions, pass those also. See the TestData 
        /// class in LMS.UITest for examples of how to build this object</param>
        public string PassAssessment(string assessmentName = null, bool alsoAnswerNonGradedNonRequiredQuestions = true, int attemptNumberToPass = 1,
            List<Constants.AssQAndAs> assessmentQandAs = null)
        {
            List<Constants.AssQAndAs> questionsAndAnswersInfo = null;

            // If the tester specified an assessment name, then use it, else set it to the first one on the page
            assessmentName = string.IsNullOrEmpty(assessmentName) ? AssessmentNameLbls[0].Text : assessmentName;

            ThrowErrorIfAttemptsExhausted(assessmentName);

            // if there are multiple asessments on this current test, then we have to click the Launch button to load the 
            // questions
            IWebElement AssessmentContainer = GetAssessmentContainer(assessmentName);
            if (AssessmentContainer.FindElements(Bys.ActAssessmentPage.LaunchBtn).Count > 0)
            {
                AssessmentContainer.FindElement(Bys.ActAssessmentPage.LaunchBtn).Click();
                this.WaitUntilAny(Criteria.ActAssessmentPage.ReturnToSummaryBtnVisible);
                this.WaitUntilAny(Criteria.ActAssessmentPage.NextBtnVisible, Criteria.ActAssessmentPage.SubmitBtnVisible);
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
            }

            // If we are in non-prod, then we will populate the question and answer info through the DB, else the tester will 
            // pass that info NOTE: Currently, we do have access to read the Prod DB, so we are not passing the Q and As 
            // manually as of now. If this changes in the future and we don thave access anymore, then we will use this If 
            // statement
            if (assessmentQandAs == null)
            {
                // Get the graded questions and the associated correct answers
                questionsAndAnswersInfo = DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(),
                    Help.GetActivityIDFromURL(Browser),
                    assessmentName, isQuestionGraded: true, isAnswerCorrect: true, isAnswerRequired: null,
                    returnDistinctQuestionsWithStuffedAnswers: true);

                // Get non-graded required questions
                questionsAndAnswersInfo.AddRange(DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(),
                    Help.GetActivityIDFromURL(Browser),
                    assessmentName, isQuestionGraded: false, isAnswerCorrect: null, isAnswerRequired: true,
                    returnDistinctQuestionsWithOneAnswer: true));

                // If the tester specified, get the non-graded non-required questions
                if (alsoAnswerNonGradedNonRequiredQuestions)
                {
                    questionsAndAnswersInfo.AddRange(DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(),
                        Help.GetActivityIDFromURL(Browser), assessmentName, isQuestionGraded: false, isAnswerCorrect: null,
                        isAnswerRequired: false, returnDistinctQuestionsWithOneAnswer: true));
                }
            }
            else
            {
                questionsAndAnswersInfo = assessmentQandAs;
            }

            if (questionsAndAnswersInfo is null)
            {
                throw new Exception("No questions were found with the query. Check to make sure your activity is configured properly " +
                    "in CME360");
            }

            // If the tester previously failed an attempt, then we need to click Retake first
            if (Browser.Exists(Bys.ActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible))
            {
                ClickAndWait(RetakeBtn);
            }

            // Sort the list based on QuestionOrderId
            questionsAndAnswersInfo.Sort((qoid1, qoid2) => qoid1.QuestionOrderId.CompareTo(qoid2.QuestionOrderId));

            // If the tester wants to pass the test on a certain attempt, then we have to fail the attempts before this attempt. 
            // Do that now
            for (int i = 1; i < attemptNumberToPass; i++)
            {
                FailAttempt(assessmentQandAs: assessmentQandAs);
                ClickAndWait(RetakeBtn);
            }

            // Choose an answer for all questions
            foreach (Constants.AssQAndAs questionAndAnswersInfo in questionsAndAnswersInfo)
            {
                // if the current questionAndAnswersInfo returns DynamicPageBreak for the QuestionText, then click the Next button,
                // else answer the current question
                if (questionAndAnswersInfo.QuestionText == "DynamicPageBreak")
                {
                    ClickAndWait(NextBtn);
                }
                else
                {
                    Help.ChooseAnswerUsingQueryResult(Browser, questionAndAnswersInfo);
                }
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
            // If PIM, then just pass it with no other conditions
            if (Browser.Url.ToLower().Contains(Constants.PageURLs.Activity_PIM.GetDescription().ToLower()))
            {
                PassAssessment(assessment.AssessmentName, alsoAnswerNonGradedNonRequiredQuestions, attemptNumberToPass, assessmentQandAs);
                return ClickAndWait(ContinueBtn);
            }
            else
            {
                // Get the status labels for the specified assessment
                IWebElement AssessmentContainer = GetAssessmentContainer(assessment.AssessmentName);
                string statusPassFailLblText = AssessmentContainer.FindElement(Bys.ActAssessmentPage.StatusPassFailLbls).Text.ToLower();
                string yourStatusLblValue = AssessmentContainer.FindElement(Bys.ActAssessmentPage.YourStatusValueLbl).Text.ToLower();

                // If there are attempts left and the user has not passed the top-most accreditation/scenario yet, then pass it. Note that for 
                // the Continue button to be enabled, then the user must pass the first/top accreditation/scenario (StatusPassFailLbls) in the list 
                if (!AttemptsExhaustedAfterFailingAssessment(assessment.AssessmentName))
                {
                    if (statusPassFailLblText == "FAILED".ToLower()
                        || yourStatusLblValue == "IN PROGRESS".ToLower()
                        || yourStatusLblValue == "NOT STARTED".ToLower())
                    {
                        PassAssessment(assessment.AssessmentName, alsoAnswerNonGradedNonRequiredQuestions, attemptNumberToPass, assessmentQandAs);
                    }
                }

                // Else if there are zero attempts left and the user has failed the top accreditation/scenario, throw error
                // Note that for the Continue button to be enabled, then the user must pass the first/top accreditation/scenario 
                // (StatusPassFailLbls) in the list
                else if (yourStatusLblValue == "FAILED".ToLower())
                {
                    throw new Exception("You have failed this assessment already and have 0 attempts remaining");
                }

                else
                {
                    throw new Exception("Something went wrong in the above If statements. If you see this error message, you may have to refactor " +
                        "the code to have better If statements. One time this happened when the mobile view did not load any Text into the" +
                        "StatusPassFailLbls");
                }

                // If we are in a multi-assessment test, then we will have to click to go back to the Summary page
                if (Browser.Exists(Bys.ActAssessmentPage.ReturnToSummaryBtn, ElementCriteria.IsVisible))
                {
                    return ClickAndWait(ReturnToSummaryBtn);
                }

                AssessmentContainer = GetAssessmentContainer(assessment.AssessmentName);
                statusPassFailLblText = AssessmentContainer.FindElement(Bys.ActAssessmentPage.StatusPassFailLbls).Text.ToLower();
                yourStatusLblValue = AssessmentContainer.FindElement(Bys.ActAssessmentPage.YourStatusValueLbl).Text.ToLower();

                // If the user passed the top accreditation/scenario for all assessments on this page
                // Note that for the Continue or Finish buttons to be enabled, then the user must pass 
                // the first/top accreditation/scenario for all assessments on the page 
                if (statusPassFailLblText == "PASSED".ToLower() || statusPassFailLblText == "COMPLETE".ToLower())
                {
                    // If this activity has multi-assessments, then the next activity would be on this same page. If so, then we dont have to 
                    // click the  Continue. Else click Next to go to next test assessment. Else click the Finish button because all tests 
                    // would be complete The order of the below statements should not change.

                    // If the continue button is enabled, click it to go to the next page
                    if (ContinueBtn.GetAttribute("aria-disabled") != "true")
                    {
                        return ClickAndWait(ContinueBtn);
                    }

                    // If there is no certificate and we are on the last assessment, then a Finish button will appear.
                    else if (Browser.Exists(Bys.ActAssessmentPage.FinishBtn, ElementCriteria.IsVisible))
                    {
                        return ClickAndWait(FinishBtn);
                    }
                }

                else
                {
                    throw new Exception("Something went wrong in the above If statement. If you see this error message, you may have to refactor " +
                        "the code to have better If statements. One time this happened when the mobile view did not load any Text into the" +
                        "StatusPassFailLbls");
                }

                // If the page did not load properly or something else unexpectedly happened and we couldnt get the assessment name labels, throw error
                throw new Exception("Couldnt get assessment names");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmentName"></param>
        /// <returns></returns>
        private bool AttemptsExhaustedAfterFailingAssessment(string assessmentName)
        {
            IWebElement AssessmentContainer = GetAssessmentContainer(assessmentName);

            dynamic numberOfAttemptsLeft = GetNumberOfAttemptsLeft(assessmentName);

            if (numberOfAttemptsLeft is int && numberOfAttemptsLeft != 0)
            {
                return false;
            }

            else if (numberOfAttemptsLeft is string)
            {
                return false;
            }

            else
            {
                return false;
            }
        }










        /// <summary>
        /// Gets the number of attempts remaining for a given assessment. If you dont specify an assessment, this will return the 
        /// first (or only) assessment on the page's number of attempts left.
        /// </summary>
        /// <returns></returns>
        private dynamic GetNumberOfAttemptsLeft(string assessmentName = null)
        {
            // Get the container element of the assessment
            IWebElement AssessmentContainer = GetAssessmentContainer(assessmentName);

            // Get the number of attempts label element
            string numOfAttemptsLabelXpath = AssessmentContainer.FindElement(Bys.ActAssessmentPage.NumberOfAttemptsRemainingLbl).Text.ToLower();

            // Get the number within the label, then convert it to an int
            int length = numOfAttemptsLabelXpath.IndexOf(" ");
            string numAttemptsRemainingLabelSubstring = NumberOfAttemptsRemainingLbl.Text.Substring(1, length - 1);

            int? numOfAttemptsRemainingNumber = null;

            if (numAttemptsRemainingLabelSubstring != "Unlimited")
            {
                numOfAttemptsRemainingNumber = Int32.Parse(Regex.Match(numAttemptsRemainingLabelSubstring, @"\d+").Value);
                return numOfAttemptsRemainingNumber;
            }
            else
            {
                return "Unlimited";
            }
        }

        /// <summary>
        /// Fails one attempt for an assessment. Specifically, this chooses all incorrect answers, and then clicks the Submit button,  
        /// </summary>
        /// <param name="alsoAnswerNonGradedNonRequiredQuestions">If you want the non graded, non-required questions to be answered</param>
        /// <param name="assessmentQandAs">(PROD-ONLY). Pass all of the required questions and associated correct answers if you are
        /// running on Prod. If the question has multiple correct answers, inside the AnswerText field, pass those answers separated by a 
        /// comma-space. If you want the test to answer the non-graded non-required questions, pass those also. See the TestData 
        /// class in LMS.UITest for examples of how to build this object</param>
        public void FailAttempt(string assessmentName = null, bool alsoAnswerNonGradedNonRequiredQuestions = true,
            List<Constants.AssQAndAs> assessmentQandAs = null)
        {
            ThrowErrorIfAttemptsExhausted(assessmentName);

            List<Constants.AssQAndAs> questionsAndAnswersInfo = null;

            // If we are in non-prod, then we will populate the question and answer info through the DB, else the tester will pass that info
            // NOTE: Currently, we do have access to read the Prod DB, so we are not passing the Q and As manually as of now. If this changes 
            /// in the future and we don thave access anymore, then we will use this If statement
            if (assessmentQandAs == null)
            {
                // Get the graded questions and the associated incorrect answers
                questionsAndAnswersInfo = DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(), Help.GetActivityIDFromURL(Browser),
                    AssessmentNameLbls[0].Text, isQuestionGraded: true, isAnswerCorrect: false, isAnswerRequired: null,
                    returnDistinctQuestionsWithOneAnswer: true);

                // Get non-graded required questions
                questionsAndAnswersInfo.AddRange(DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(), Help.GetActivityIDFromURL(Browser),
                    AssessmentNameLbls[0].Text, isQuestionGraded: false, isAnswerCorrect: null, isAnswerRequired: true,
                    returnDistinctQuestionsWithOneAnswer: true));

                // If the tester specified, get the non-graded non-required questions
                if (alsoAnswerNonGradedNonRequiredQuestions)
                {
                    questionsAndAnswersInfo.AddRange(DBUtils.GetAssessmentQuestionsAndAnswers(GetUserName(),
                        Help.GetActivityIDFromURL(Browser), AssessmentNameLbls[0].Text, isQuestionGraded: false, isAnswerCorrect: null,
                        isAnswerRequired: false, returnDistinctQuestionsWithOneAnswer: true));
                }
            }
            else
            {
                questionsAndAnswersInfo = assessmentQandAs;
            }

            if (questionsAndAnswersInfo is null)
            {
                throw new Exception("No questions were found with the query. Check to make sure your activity is configured properly " +
                    "in CME360");
            }

            // Sort the list based on QuestionOrderId
            questionsAndAnswersInfo.Sort((qoid1, qoid2) => qoid1.QuestionOrderId.CompareTo(qoid2.QuestionOrderId));

            // Choose an answer for all questions
            foreach (Constants.AssQAndAs questionAndAnswersInfo in questionsAndAnswersInfo)
            {
                Help.ChooseAnswerUsingQueryResult(Browser, questionAndAnswersInfo);
            }

            ClickAndWait(SubmitBtn);
        }

        /// <summary>
        /// Gets the amount of attempts available. Then loops through that amount and fails all attempts for an assessment. 
        /// Specifically, this chooses all incorrect answers, clicks the Submit button, clicks the retake if available, 
        /// and repeats until no more attempts are remaining
        /// </summary>
        /// <param name="alsoAnswerNonGradedNonRequiredQuestions">If you want the non graded, non-required questions to be answered</param>
        public void FailAssessment(string assessmentName = null, bool alsoAnswerNonGradedNonRequiredQuestions = true)
        {
            // Get the number of attemots remaining
            dynamic numberOfAttemptsLeft = GetNumberOfAttemptsLeft(assessmentName);

            if (numberOfAttemptsLeft is string)
            {
                if (numberOfAttemptsLeft == "Unlimited")
                {
                    throw new Exception("There are unlimited amount of attempts, you can not fail this assessment.");
                }
            }

            else
            {
                // If there are no attempts remaining
                if (numberOfAttemptsLeft == 0)
                {

                    throw new Exception("There are no attempts remaining. You may have already completed this assessment");
                }
            }

            // Loop through all attempts
            for (var i = 0; i < numberOfAttemptsLeft; i++)
            {
                //// If the current number of attempts remaining label is set to zero, then the retake button will not be available
                //if (Int32.Parse(Regex.Match(NumberOfAttemptsRemainingLbl.Text, @"\d+").Value) != 0)
                //{
                // If we (or another tester) have already failed an attempt, then we need to click the Retake button
                if (Browser.Exists(Bys.ActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible))
                {
                    ClickAndWait(RetakeBtn);
                }
                //}

                FailAttempt();
            }
        }

        /// <summary>
        /// If on mobile, click the Assessment List expand button, because if we dont click it, the text from the Your Status label and other
        /// labels within that window will not load their inner text properties, which messes with wait criteria
        /// </summary>
        private void ExpandForMobile()
        {
            if (Browser.MobileEnabled())
            {
                Browser.WaitForElement(Bys.ActAssessmentPage.Mobile_AssessmentListExpandBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                // Adding a small sleep here. One time (maybe once out of 100), it failed to click the expand button. I am thinking maybe it needs
                // a sleep. If it fails again, then I will have to make a dynamic wait, as the above line of code might not wait for the 
                // control/page to fully load
                Thread.Sleep(1000);
                if (!YourStatusValueLbl.Displayed)
                {
                    Mobile_AssessmentListExpandBtn.ClickJS(Browser);
                }
            }
        }

        #endregion methods: page specific

    }


}