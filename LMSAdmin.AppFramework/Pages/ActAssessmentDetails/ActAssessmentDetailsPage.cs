using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using OpenQA.Selenium.Interactions;
using System.Globalization;

namespace LMSAdmin.AppFramework
{
    public class ActAssessmentDetailsPage : Page, IDisposable
    {
        #region constructors
        public ActAssessmentDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements
        public IWebElement BackToAssessmentsLnk { get { return this.FindElement(Bys.ActAssessmentDetailsPage.BackToAssessmentsLnk); } }

        public IWebElement QATabAnswerOptionsTxt { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATabAnswerOptionsTxt); } }
        public SelectElement QATabQuestTypeSelElem { get { return new SelectElement(this.FindElement(Bys.ActAssessmentDetailsPage.QATabQuestTypeSelElem)); } }
        public SelectElement DetailsTabAssTypeSelElem { get { return new SelectElement(this.FindElement(Bys.ActAssessmentDetailsPage.DetailsTabAssTypeSelElem)); } }
        public SelectElement DetailsTabAvailAudSelElem { get { return new SelectElement(this.FindElement(Bys.ActAssessmentDetailsPage.DetailsTabAvailAudSelElem)); } }
        public IWebElement QATabAddNewQuesBtn { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATabAddNewQuesBtn); } }
        public IWebElement QATabInsertNewQuesLnk { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATabInsertNewQuesLnk); } }
        public IWebElement QATab { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATab); } }
        public IWebElement DetailsTabAssTitleTxt { get { return this.FindElement(Bys.ActAssessmentDetailsPage.DetailsTabAssTitleTxt); } }
        public IWebElement DetailsTabAssDescTxt { get { return this.FindElement(Bys.ActAssessmentDetailsPage.DetailsTabAssDescTxt); } }
        public IWebElement QATabQuesNameTxt { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATabQuesNameTxt); } }
        public IWebElement QATabQuesText { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATabQuesText); } }
        public IWebElement QATabContinueBtn { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATabContinueBtn); } }
        public IWebElement DetailsTabSaveBtn { get { return this.FindElement(Bys.ActAssessmentDetailsPage.DetailsTabSaveBtn); } }
        public IWebElement DetailsTabAddAudBtn { get { return this.FindElement(Bys.ActAssessmentDetailsPage.DetailsTabAddAudBtn); } }
        public IWebElement DetailsTabChngsSavedLbl { get { return this.FindElement(Bys.ActAssessmentDetailsPage.DetailsTabChngsSavedLbl); } }
        public IWebElement QATabGradedQuestionRdoBtn { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATabGradedQuestionRdoBtn); } }
        public IWebElement QATabSaveandContinuebtn { get { return this.FindElement(Bys.ActAssessmentDetailsPage.QATabSaveandContinuebtn); } }
        public IWebElement AnswerKeyTab { get { return this.FindElement(Bys.ActAssessmentDetailsPage.AnswerKeyTab); } }
        public IWebElement AnswerKeyTabSaveBtn { get { return this.FindElement(Bys.ActAssessmentDetailsPage.AnswerKeyTabSaveBtn); } }
        public IWebElement AnswerKeyTabChangesSavedLbl { get { return this.FindElement(Bys.ActAssessmentDetailsPage.AnswerKeyTabChangesSavedLbl); } }




        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActAssessmentDetailsPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.ActAssessmentDetailsPage.DetailsTabSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabSaveBtn.GetAttribute("outerHTML"))
                {
                    DetailsTabSaveBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentDetailsPage.DetailsTabChngsSavedLbl, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.QATab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == QATab.GetAttribute("outerHTML"))
                {
                    QATab.ClickJS(Browser);
                    QATab.Click();
                    Browser.WaitForElement(Bys.ActAssessmentDetailsPage.QATabAddNewQuesBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.BackToAssessmentsLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == BackToAssessmentsLnk.GetAttribute("outerHTML"))
                {
                    BackToAssessmentsLnk.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentsPage.AddNewAssessmentLnk, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.DetailsTabAddAudBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabAddAudBtn.GetAttribute("outerHTML"))
                {
                    DetailsTabAddAudBtn.ClickJS(Browser);
                    WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.QATabAddNewQuesBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == QATabAddNewQuesBtn.GetAttribute("outerHTML"))
                {
                    QATabAddNewQuesBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentDetailsPage.QATabContinueBtn);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.QATabContinueBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == QATabContinueBtn.GetAttribute("outerHTML"))
                {
                    QATabContinueBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentDetailsPage.QATabQuesNameTxt);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.QATabSaveandContinuebtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == QATabSaveandContinuebtn.GetAttribute("outerHTML"))
                {
                    QATabSaveandContinuebtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentDetailsPage.QATabInsertNewQuesLnk, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.AnswerKeyTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AnswerKeyTab.GetAttribute("outerHTML"))
                {
                    AnswerKeyTab.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentDetailsPage.AnswerKeyTabSaveBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.AnswerKeyTabSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AnswerKeyTabSaveBtn.GetAttribute("outerHTML"))
                {
                    AnswerKeyTabSaveBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAssessmentDetailsPage.AnswerKeyTabChangesSavedLbl, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                buttonOrLinkElem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Selects an item in a Select Element, and then waits for a window to close or open, or a page to load, depending on the element that was 
        /// selected
        /// </summary>
        /// <param name="elem">The select element</param>
        public dynamic SelectAndWait(SelectElement Elem, string selection)
        {
            if (Browser.Exists(Bys.ActAssessmentDetailsPage.DetailsTabAssTypeSelElem))
            {
                if (Elem.AllSelectedOptions[0].GetAttribute("outerHTML") == DetailsTabAssTypeSelElem.AllSelectedOptions[0].GetAttribute("outerHTML"))
                {
                    DetailsTabAssTypeSelElem.SelectByText(selection);
                    WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAssessmentDetailsPage.QATabQuestTypeSelElem))
            {
                if (Elem.AllSelectedOptions[0].GetAttribute("outerHTML") == QATabQuestTypeSelElem.AllSelectedOptions[0].GetAttribute("outerHTML"))
                {
                    QATabQuestTypeSelElem.SelectByText(selection);
                    Thread.Sleep(0300);
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element"));
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

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Fills in all required fields on the Details tab and clicks Save
        /// </summary>
        /// <param name="assType"><see cref="Constants.AssessmentTypes"/></param>
        /// <param name="assTitle">(Optional). You can specify your own title</param>
        public void FillAndSaveDetailsTab(Constants.AssessmentTypes assType, string asstitle = null)
        {
            string timeStamp = string.Format("{0}", DateTime.UtcNow.ToString("MM-dd-yy HH:mm:ss.fff", CultureInfo.InvariantCulture));
            string assTitle = string.IsNullOrEmpty(asstitle) ? string.Format("AssessmentTitle {0}", timeStamp) : asstitle;

            SelectAndWait(DetailsTabAssTypeSelElem, assType.GetDescription());
            DetailsTabAssTitleTxt.SendKeys(assTitle);
            DetailsTabAssDescTxt.SendKeys(assTitle);

            for (int i = 0; i < DetailsTabAvailAudSelElem.Options.Count; i++)
            {
                DetailsTabAvailAudSelElem.SelectByIndex(i);
            }
            ClickAndWait(DetailsTabAddAudBtn);

            ClickAndWait(DetailsTabSaveBtn);
        }

        /// <summary>
        /// Adds questions to assessments. Specifically, this clicks on the QA tab, clicks the Add New Question button, clicks the Continue button, then
        /// fills in the required fields with hard-coded questions and answers. This will return the question and correct answer in a tuple list string string
        /// </summary>
        /// <param name="numberOfQuestionsPerQuestionType">Number of questions for each type</param>
        /// <param name="questionTypes">Right now this is only coded for the question types within that enum <see cref="Constants_LMSAdmin.QuestionTypes"/></param>
        public List<(string Question, string Answer)> Addquestions(int numberOfQuestionsPerQuestionType, Constants.QuestionTypes[] questionTypes)
        {
            // New tuple feature: https://stackoverflow.com/questions/8002455/how-to-easily-initialize-a-list-of-tuples. Since the build servers dont have C# 
            // 7.0 yet, we have to add the System.Value.Tuple reference for this to work
            List<(string Question, string Answer)> QuestionsAndAnswers = new List<(string Question, string Answer)>();

            string questionText, answers, rightAnswer;
            List<string> rightAnswers;

            foreach (var questionType in questionTypes)
            {
                for (int i = 1; i <= numberOfQuestionsPerQuestionType; i++)
                {
                    string timeStamp = string.Format("{0}", DateTime.UtcNow.ToString("MM-dd-yy HH:mm:ss.fff", CultureInfo.InvariantCulture));
                    string quesName = string.Format("AutoGenQuesName {0}", timeStamp);

                    // Go to the Add New Question form
                    ClickAndWait(QATab);
                    ClickAndWait(QATabAddNewQuesBtn);
                    ClickAndWait(QATabContinueBtn);

                    // If the question has a single select answer
                    if (questionType == Constants.QuestionTypes.MultipleChoiceSingleAnswer | 
                        questionType == Constants.QuestionTypes.MultipleChoicewithDropDown)
                    {
                        questionText = string.Format("{0} Ques #{1}", questionType, i);
                        answers = string.Format(" Right answer for {0}\r\n Wrong answer #1 for {0}\r\n Wrong answer #2 for {0}", questionText);
                        rightAnswer = string.Format("Right answer for {0}", questionText);

                        // Fill in the fields
                        QATabQuesNameTxt.Clear();
                        QATabQuesNameTxt.SendKeys(quesName);
                        SelectAndWait(QATabQuestTypeSelElem, questionType.GetDescription());
                        QATabQuesText.Clear();
                        QATabQuesText.SendKeys(questionText);
                        QATabGradedQuestionRdoBtn.Click();
                        QATabAnswerOptionsTxt.Clear();
                        QATabAnswerOptionsTxt.SendKeys(answers);

                        // Click continue, go to the Answer Key tab and choose the correct answers
                        ClickAndWait(QATabSaveandContinuebtn);
                        ClickAndWait(AnswerKeyTab);
                        ElemSet.RdoBtn_ClickByText(Browser, ("A. " + rightAnswer));
                        ClickAndWait(AnswerKeyTabSaveBtn);

                        QuestionsAndAnswers.Add((questionText, rightAnswer));
                    }
                    // If the question has a multi select answer
                    else if (questionType == Constants.QuestionTypes.MultipleChoicewithMultipleAnswers)
                    {
                        questionText = string.Format("{0} Ques #{1}", questionType, i);
                        answers = string.Format(" Right answer #1 for {0}\r\n Right answer #2 for {0}\r\n Wrong answer #1 for {0}\r\n Wrong answer #2 for {0}", questionText);
                        rightAnswers = new List<string>() { string.Format("Right answer #1 for {0}", questionText),
                            string.Format("Right answer #2 for {0}", questionText) };

                        // Fill in the fields
                        QATabQuesNameTxt.Clear();
                        QATabQuesNameTxt.SendKeys(quesName);
                        SelectAndWait(QATabQuestTypeSelElem, questionType.GetDescription());
                        QATabQuesText.Clear();
                        QATabQuesText.SendKeys(questionText);
                        QATabGradedQuestionRdoBtn.Click();
                        QATabAnswerOptionsTxt.Clear();
                        QATabAnswerOptionsTxt.SendKeys(answers);

                        // Click continue, go to the Answer Key tab and choose the correct answers
                        ClickAndWait(QATabSaveandContinuebtn);
                        ClickAndWait(AnswerKeyTab);
                        ElemSet.RdoBtn_ClickByText(Browser, ("A. " + rightAnswers[0]));
                        ElemSet.RdoBtn_ClickByText(Browser, ("B. " + rightAnswers[1]));
                        ClickAndWait(AnswerKeyTabSaveBtn);

                        QuestionsAndAnswers.Add((questionText, rightAnswers[0]));
                        QuestionsAndAnswers.Add((questionText, rightAnswers[1]));
                    }

                    else { throw new Exception("need to add more code for other types of questions"); }       
                }
            }

            return QuestionsAndAnswers;
        }

        #endregion methods: page specific



    }
}