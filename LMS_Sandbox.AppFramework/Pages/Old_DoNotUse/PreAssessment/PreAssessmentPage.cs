//using Browser.Core.Framework;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Text.RegularExpressions;
//using System.Threading;
//using LMS.AppFramework.HelperMethods;
//using LOG4NET = log4net.ILog;

//namespace LMS.AppFramework
//{
//    public class PreActAssessmentPage : LMSPage, IDisposable
//    {
//        #region constructors
//        public PreActAssessmentPage(IWebDriver driver) : base(driver)
//        {
//        }

//        #endregion constructors

//        #region properties

//        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

//        // Keep track of the requests that WE start so we can clean them up at the end.
//        private List<string> activeRequests = new List<string>();

//        UAMSHelperMethods Help = new UAMSHelperMethods();

//        public override string PageUrl { get { return "lms/activity_pretest"; } }

//        #endregion properties

//        #region elements

//        public IWebElement SubmitBtn { get { return this.FindElement(Bys.PreActAssessmentPage.SubmitBtn); } }
//        public IWebElement FirstQuestionLbl { get { return this.FindElement(Bys.PreActAssessmentPage.FirstQuestionLbl); } }
//        public IWebElement StatusPassFailLbl { get { return this.FindElement(Bys.PreActAssessmentPage.StatusPassFailLbl); } }
//        public IWebElement NumberOfAttemptsRemainingLbl { get { return this.FindElement(Bys.PreActAssessmentPage.NumberOfAttemptsRemainingLbl); } }
//        public IWebElement AttemptValueLbl { get { return this.FindElement(Bys.PreActAssessmentPage.AttemptValueLbl); } }
//        public IWebElement YourScoreValueLbl { get { return this.FindElement(Bys.PreActAssessmentPage.YourScoreValueLbl); } }
//        public IWebElement YourStatusValueLbl { get { return this.FindElement(Bys.PreActAssessmentPage.YourStatusValueLbl); } }
//        public IWebElement AssessmentNameLbl { get { return this.FindElement(Bys.PreActAssessmentPage.AssessmentNameLbl); } }
//        public IWebElement RetakeBtn { get { return this.FindElement(Bys.PreActAssessmentPage.RetakeBtn); } }
//        public IWebElement ContinueBtn { get { return this.FindElement(Bys.PreActAssessmentPage.ContinueBtn); } }




//        #endregion elements

//        #region methods: repeated per page

//        public override void WaitForInitialize()
//        {
//            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.PreActAssessmentPage.PageReady);
//            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
//        }

//        /// <summary>
//        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
//        /// depending on the element that was clicked
//        /// </summary>
//        /// <param name="elem">The element to click on</param>
//        public dynamic ClickAndWait(IWebElement elem)
//        {
//            if (Browser.Exists(Bys.PreActAssessmentPage.ContinueBtn))
//            {
//                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
//                {
//                    // Using javascript click here for the following reason. When we use a regular click after choosing incorrect
//                    // answers, IE then doesnt load the page fully for some reason. This is not reproducable manually
//                    ContinueBtn.ClickJS(Browser);

//                    // Wait until the page URL loads
//                    var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
//                    wait.Until(Browser =>
//                    {
//                    return Browser.Url.Contains(Constants_LMS.PageURLs.activity_test.GetDescription())
//                            || Browser.Url.Contains(Constants_LMS.PageURLs.activity_evaluation.GetDescription()) 
//                            || Browser.Url.Contains(Constants_LMS.PageURLs.certificate.GetDescription());
//                    });

//                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

//                    // If this click takes us to a Post-Assessment
//                    if (Browser.Url.Contains(Constants_LMS.PageURLs.activity_test.GetDescription()))
//                    {
//                        PostActAssessmentPage Page = new PostActAssessmentPage(Browser);
//                        Page.WaitForInitialize();
//                        return Page;
//                    }
//                    // Else if this click takes us to an Evaluation
//                    else if (Browser.Url.Contains(Constants_LMS.PageURLs.activity_evaluation.GetDescription()))
//                    {
//                        EvaluationPage Page = new EvaluationPage(Browser);
//                        Page.WaitForInitialize();
//                        return Page;
//                    }
//                    // Else if this click takes us to the Certificate page
//                    else if (Browser.Url.Contains(Constants_LMS.PageURLs.certificate.GetDescription()))
//                    {
//                        ActCertificatePage Page = new ActCertificatePage(Browser);
//                        Page.WaitForInitialize();
//                        return Page;
//                    }
//                }
//            }

//            if (Browser.Exists(Bys.PreActAssessmentPage.SubmitBtn))
//            {
//                if (elem.GetAttribute("outerHTML") == SubmitBtn.GetAttribute("outerHTML"))
//                {
//                    // Using javascript click here for the following reason. When we use a regular click after choosing incorrect
//                    // answers, IE then doesnt load the page fully for some reason. This is not reproducable manually
//                    SubmitBtn.ClickJS(Browser);
//                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
//                    Browser.WaitForElement(Bys.PreActAssessmentPage.AssessmentNameLbl, TimeSpan.FromSeconds(90), ElementCriteria.IsVisible);
//                    this.WaitUntilAny(Criteria.PreActAssessmentPage.RetakeBtnVisible, Criteria.PreActAssessmentPage.ContinueBtnEnabled,
//                        Criteria.PreActAssessmentPage.YourStatusValueLblTextEqualsSubmitted);
//                    return null;
//                }
//            }

//            if (Browser.Exists(Bys.PreActAssessmentPage.RetakeBtn))
//            {
//                if (elem.GetAttribute("outerHTML") == RetakeBtn.GetAttribute("outerHTML"))
//                {
//                    // Using javascript click here for the following reason. When we use a regular click after choosing incorrect
//                    // answers, IE then doesnt load the page fully for some reason. This is not reproducable manually
//                    RetakeBtn.ClickJS(Browser);
//                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
//                    Browser.WaitForElement(Bys.PreActAssessmentPage.AssessmentNameLbl, TimeSpan.FromSeconds(90), ElementCriteria.IsVisible);
//                    this.WaitUntilAny(Criteria.PreActAssessmentPage.LoadIconNotExists, Criteria.PreActAssessmentPage.SubmitBtnVisible);
//                    return null;
//                }
//            }

//            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
//                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
//                elem.GetAttribute("innerText")));
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//        }

//        protected virtual void Dispose(bool isDisposing)
//        {
//            try { activeRequests.Clear(); }
//            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActivityPreviewPage", activeRequests.Count, ex); }
//        }

//        #endregion methods: repeated per page

//        #region methods: page specific
//        /// <summary>
//        /// Pass an assessment. Specificaly, this chooses the correct answers then clicks the Submit button.
//        /// NOTE: This is in its early stages, meaning it is only compatible with radio buttons, check boxes and dropdowns right now. I am 
//        /// just starting on this method and page so this will look different in the future
//        /// </summary>
//        /// <param name="attemptNumberRequested">(Optional). If you want to pass the test on a second or third or etc. attempt, then pass in the
//        /// attempt number</param>
//        public void PassAssessment(bool alsoAnswerNonGradedQuestions = false, int attemptNumberRequested = 1)
//        {
//            string userName = Regex.Replace(UserNameLbl.GetAttribute("textContent"), @"\s+", "");

//            // Get graded and required questions and the associated correct answers
//            var questionsAndAnswersInfo = DBUtils_LMS.GetAssessmentQuestionsAndAnswers(userName, Help.GetActivityIDFromURL(Browser),
//                AssessmentNameLbl.Text, isQuestionGraded: true, isAnswerCorrect: true, isAnswerRequired: true, returnDistinctQuestions: false);

//            // If the tester specified, get the non-graded questions
//            if (alsoAnswerNonGradedQuestions)
//            {
//                questionsAndAnswersInfo.AddRange(DBUtils_LMS.GetAssessmentQuestionsAndAnswers(userName, Help.GetActivityIDFromURL(Browser),
//                    AssessmentNameLbl.Text, isQuestionGraded: false, isAnswerCorrect: false, isAnswerRequired: false, returnDistinctQuestions: true));
//            }

//            // If the tester previously failed an attempt, then we need to click Retake first
//            if (Browser.Exists(Bys.ActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible))
//            {
//                ClickAndWait(RetakeBtn);
//            }

//            // If the tester wants to pass the test on a certain attempt, then we have to fail the attempts before this attempt. 
//            // Do that now
//            for (int i = 1; i < attemptNumberRequested; i++)
//            {
//                FailAttempt();
//                ClickAndWait(RetakeBtn);
//            }

//            // Choose an answer for all questions
//            foreach (Constants_LMS.QuestionsAndAnswersInfo questionAndAnswersInfo in questionsAndAnswersInfo)
//            {
//                ChooseAnswer(questionAndAnswersInfo);
//            }

//            ClickAndWait(SubmitBtn);
//        }

//        /// <summary>
//        /// Passes an assessment, then clicks continue. Specificaly, this chooses the correct answers, clicks the Submit button, then 
//        /// then clicks the Continue button.
//        /// NOTE: This is in its early stages, meaning it is only compatible with radio buttons, check boxes and dropdowns right now. I am 
//        /// just starting on this method and page so this will look different in the future
//        /// </summary>
//        /// <param name="attemptNumberRequested">(Optional). If you want to pass the test on a second or third or etc. attempt, then pass in the attempt number</param>
//        public dynamic PassAssessmentAndContinueToNextPage(bool alsoAnswerNonGradedQuestions = false, int attemptNumberRequested = 1)
//        {
//            PassAssessment(alsoAnswerNonGradedQuestions, attemptNumberRequested);
//            return ClickAndWait(ContinueBtn);
//        }

//        /// <summary>
//        /// Fails one attempt for an assessment. Specifically, this chooses all incorrect answers, and then clicks the Submit button,  
//        /// NOTE: This is in its very early stages, meaning it is only compatible with radio buttons and check boxes right now, etc. 
//        /// I am just starting on this method and page so this will look a lot different in the future
//        /// </summary>
//        public void FailAttempt()
//        {
//            string userName = Regex.Replace(UserNameLbl.GetAttribute("textContent"), @"\s+", "");

//            // Get all questions (distinct) and the associated incorrect answers
//            var questionsAndIncorrectAnswersInfo = DBUtils_LMS.GetAssessmentQuestionsAndAnswers(userName, Help.GetActivityIDFromURL(Browser),
//                AssessmentNameLbl.Text, isAnswerCorrect: false);

//            // Choose an answer for all questions
//            foreach (Constants_LMS.QuestionsAndAnswersInfo questionAndAnswersInfo in questionsAndIncorrectAnswersInfo)
//            {
//                ChooseAnswer(questionAndAnswersInfo);
//            }

//            ClickAndWait(SubmitBtn);
//        }

//        /// <summary>
//        /// Gets the amount of attempts available. Then loops through that amount and fails all attempts for an assessment. 
//        /// Specifically, this chooses all incorrect answers, clicks the Submit button, clicks the retake if available, 
//        ///  and repeats until no more attempts are remaining
//        /// NOTE: This is in its very early stages, meaning it is only compatible with radio buttons and check boxes right now, etc. 
//        /// I am just starting on this method and page so this will look a lot different in the future
//        /// </summary>
//        public void FailAssessment()
//        {
//            string userName = Regex.Replace(UserNameLbl.GetAttribute("textContent"), @"\s+", "");

//            // Get all questions (distinct) and the associated incorrect answers
//            var questionsAndIncorrectAnswersInfo = DBUtils_LMS.GetAssessmentQuestionsAndAnswers(userName, Help.GetActivityIDFromURL(Browser),
//                AssessmentNameLbl.Text, isAnswerCorrect: false);

//            int numOfAttemptsRemaining = Int32.Parse(Regex.Match(NumberOfAttemptsRemainingLbl.Text, @"\d+").Value);

//            // Loop through all attempts
//            for (var i = 0; i < numOfAttemptsRemaining; i++)
//            {
//                // Choose an answer for all questions
//                foreach (Constants_LMS.QuestionsAndAnswersInfo questionAndAnswersInfo in questionsAndIncorrectAnswersInfo)
//                {
//                    ChooseAnswer(questionAndAnswersInfo);
//                }

//                ClickAndWait(SubmitBtn);

//                // If the current number of attempts remaining label is set to zero, then the retake button will not be available
//                if (Int32.Parse(Regex.Match(NumberOfAttemptsRemainingLbl.Text, @"\d+").Value) != 0)
//                {
//                    ClickAndWait(RetakeBtn);
//                }
//            }
//        }

//        /// <summary>
//        /// Chooses an answer
//        /// </summary>
//        /// <param name="questionAndAnswerInfo"></param>
//        /// <see cref="DBUtils_LMS.GetAssessmentQuestionsAndAnswers(string, string, string, string)"/></param>
//        private void ChooseAnswer(Constants_LMS.QuestionsAndAnswersInfo questionAndAnswerInfo)
//        {
//            string answerText = questionAndAnswerInfo.AnswerText;
//            // Required questions have an asterisks after them
//            string questionText = !questionAndAnswerInfo.IsRequired ? questionAndAnswerInfo.QuestionText : questionAndAnswerInfo.QuestionText + "*";

//            // Get the container element that holds all the questions and answers. When the question is required, it will add an 
//            // asterisks. See above line of code for how we conditioned to insert the asterisks in that case
//            IWebElement QuestionGroupingContainer = Browser.FindElement(By.XPath(string.Format("//div[text()='{0}']/..", questionText)));

//            // Dropdowns
//            if (questionAndAnswerInfo.QuestionTypeName.Contains("DropDown"))
//            {
//                // Selenium has issues selecting items in our custom Fireball dropdown element in Firefox
//                if (Browser.GetCapabilities().GetCapability("browserName").ToString().ToString() == BrowserNames.Firefox)
//                {
//                    IWebElement DropDownBtn = Browser.FindElement(By.XPath(string.Format("//div[text()='{0}']/..//button", questionText)));
//                    ElemSet.DropdownSingle_Fireball_SelectByText(Browser, DropDownBtn, answerText);
//                }
//                else
//                {
//                    SelectElement SelectElem = new SelectElement(
//                        Browser.FindElement(By.XPath(string.Format("//div[text()='{0}']/..//select", questionText))));
//                    SelectElem.SelectByText(answerText);
//                }
//            }
//            // Radio buttons
//            else if (questionAndAnswerInfo.QuestionTypeName == "ChoiceOneAnswer"
//                || questionAndAnswerInfo.QuestionTypeName == "TrueFalse"
//                || questionAndAnswerInfo.QuestionTypeName == "YesNo")
//            {
//                ElemSet.RdoBtn_ClickByText(Browser, answerText, QuestionGroupingContainer);
//            }
//            // Check boxes
//            else if (questionAndAnswerInfo.QuestionTypeName == "ChoiceMultipleAnswers")
//            {
//                ElemSet.ChkBx_ClickByText(Browser, answerText, QuestionGroupingContainer);
//            }
//        }

//        #endregion methods: page specific



//    }
//}