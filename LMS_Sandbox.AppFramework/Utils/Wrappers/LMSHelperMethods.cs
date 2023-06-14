using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
//using LMS.AppFramework.Constants__UAMS;

namespace LMS.AppFramework.LMSHelperMethods
{
    /// <summary>
    /// A class that consists of static methods which combine custom page methods, page object navigation conditions and also many different
    /// conditions to accomplish various tasks for this application that span multiple pages. These static methods can be called from
    /// your test class to seamlessly accomplish various tasks without having to worry about navigating the Page Object model when you 
    /// dont have a need to. This is our custom Cucumber/Specflow implementation. This class can also be used 
    /// when a tester is automating another application, and needs to also access this application to setup data or verify functionality. For
    /// example, we can call CME360 and use that project's methods which create activities for use within this project. 
    /// </summary>
    public class LMSHelperMethods
    {
        #region properties



        #endregion properties

        #region methods

        #region methods: called from other applications: general

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="customUserName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customEmailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customFirstName">(Optional) If needed, you can specify a first name of your choice. If not needed, leave this null and the first name will be generated for you</param>
        /// <param name="customLastName">(Optional) If needed, you can specify a last name of your choice. If not needed, leave this null and the last name will be generated for you</param>
        /// <returns>An object that contains all the user's information, such as username, full name (first and last name), email, etc.</returns>
        public UserModel CreateUser(Constants.SiteCodes siteCode, string customUserName = "", string customEmailAddress = "",
            string customFirstName = null, string customLastName = null)
        {
            return UserUtils.CreateUser(siteCode, customUserName, customEmailAddress, customFirstName, customLastName);

        }

        #endregion methods: called from other applications: general

        #region methods: called from other applications: CME360


        #endregion methods: called from other applications: CME360

        #region methods: called from within this application

        #region Activity Workflow

        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly. Enters the 
        /// activity ID into the URL. If 
        /// you need to register, it will register for the activity and go to the Overview page. It will also make payment if needed.
        /// If not, it will go straight to the Overview page. It will then continue through each activity page while passing each and 
        /// every assessment by querying the database for the correct answers, then finally arriving at the Certificate page, clicking
        /// Finish and landing at the Transcript page. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="activityTitle">The name of the activity</param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. Default = false</param>
        /// <param name="username">(Optional). If you want to specify a specific user to login with, enter the username. If not, leave it blank and this method will generate a username for you</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation users</param>
        /// <param name="actId">(PROD-ONLY). Pass the activity ID if you are running on Prod</param>
        /// <param name="assessmentQandAs">(PROD-ONLY). PROD-ONLY. Pass the questions and answers if you are running on Prod. See the TestData 
        /// class in LMS.UITest for examples of how to build this object</param>
        /// <param name="PIM">'true' if this is a PIM activity. PIM activities have a different workflow and rules, so conditions will be different</param>
        /// 
        public TranscriptPage CompleteActivity(IWebDriver Browser, Constants.SiteCodes siteCode, string activityTitle, bool isNewUser = false,
            string username = null, string password = null, string actId = null, List<Constants.Assessments> assessments = null,
            List<Constants.AssQAndAs> assessmentQandAs = null, string discountCode = null, bool PIM = false)
        {
            // Instantiate all the necessary page classes
            ActAssessmentPage AP = new ActAssessmentPage(Browser);
            ActCertificatePage CP = new ActCertificatePage(Browser);
            TranscriptPage TP = new TranscriptPage(Browser);
            ActPIMPage PIMPP = new ActPIMPage(Browser);
            ActClaimCreditPage CCP = new ActClaimCreditPage(Browser);

            // Get to the Assessment page
            GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, activityTitle, Constants.Pages_ActivityPage.Assessment, isNewUser,
                username, password, actId, discountCode, PIM);

            username = string.IsNullOrEmpty(username) ? AP.GetUserName() : username;

            // If we are on Prod, then the tester should have passed the assessmentCount, assessments and assessmentQandAs. If not, 
            // get the count from the DB. NOTE: Right now I do have read access to Prod. So for my tests, right now, I wont be passing 
            // anything to the assessmentCount parameter. UPDATE: For PIM, we are hard coding and passing the assessments
            if (assessments == null)
            {
                //numOfAssessments =  DBUtils.GetAssessmentsByActivityId(username, siteCode, activityTitle,
                //    returnDistinctAssessments: true).Count;
                assessments = DBUtils.GetAssessmentsByActivityId(username, siteCode, activityTitle, returnDistinctAssessments: true);
            }

            // If this is a PIM activity, then dont go to the for loop and instead use the custom PIM page method to complete the
            // activity
            if (PIM)
            {
                PIMPP.SubmitPIM(true, siteCode, assessments, assessmentQandAs);
            }
            else
            {
                foreach (var assessment in assessments)
                {
                    // NOTE: The order of the IF statements below should not change, because follow-up assessments come after Certificate page
                    // (If the activity contains a certificate).

                    // if the next click takes us to the certificate page, click Finish
                    if (Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription()))
                    {
                        // If the Finish button appears, then click it. Note that the Finish button will appear if there is no Follow Up test
                        if (Browser.Exists(Bys.ActCertificatePage.FinishBtn))
                        {
                            TP = CP.ClickAndWait(CP.FinishBtn);
                        }
                        else
                        {
                            AP = CP.ClickAndWait(CP.ContinueBtn);
                        }
                    }

                    // If the next click is another assessment, pass it
                    if (Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_PIM.GetDescription()))
                    //|| Browser.Url.Contains(Constants.PageURLs.Activity_Assessment.GetDescription()))
                    {
                        AP.PassAssessmentIfNotSubmittedAlreadyAndContinueOrReturnToSummary(assessment, assessmentQandAs: assessmentQandAs);
                    }
                }
            }

            // if the next click takes us to the Claim Credit page, claim credit then continue
            if (Browser.Url.Contains(Constants.PageURLs.Activity_Claim_Credit.GetDescription()))
            {
                CCP.ClaimAllCredit();
                CCP.ClickAndWait(CCP.ContinueBtn);
            }


            // if the next click takes us to the certificate page, click Finish
            if (Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription()))
            {
                // If the Finish button appears, then click it. Note that the Finish button will appear if there is no Follow Up test
                if (Browser.Exists(Bys.ActCertificatePage.FinishBtn))
                {
                    TP = CP.ClickAndWait(CP.FinishBtn);
                }
                else
                {
                    AP = CP.ClickAndWait(CP.ContinueBtn);
                }
            }

            // If the Finish button appears on the last completed Assessment page, then click it
            if (Browser.Exists(Bys.ActAssessmentPage.FinishBtn, ElementCriteria.IsVisible))
            {
                TP = AP.ClickAndWait(AP.FinishBtn);
            }

            return TP;
        }

        /// <summary>
        /// Chooses an answer
        /// </summary>
        /// <param name="questionAndAnswerInfo">
        /// <see cref="DBUtils.GetAssessmentQuestionsAndAnswers(string, string, string, bool?, bool?, bool?, bool?, bool)"/></param>
        public void ChooseAnswerUsingQueryResult(IWebDriver Browser, Constants.AssQAndAs questionAndAnswerInfo)
        {
            // Required questions have an asterisks after them.
            // 5/1/2020: Now doing this at the query level
            //string questionText = !questionAndAnswerInfo.IsRequired ? questionAndAnswerInfo.QuestionText : questionAndAnswerInfo.QuestionText + "*";
            string questionText = questionAndAnswerInfo.QuestionText;

            // Matrix required question text does not include an asterisks after it, so if the question is a matrix question, remove asterisks
            questionText = questionAndAnswerInfo.QuestionTypeName == "RatingScaleMatrix" ? questionText.TrimEnd('*') : questionText;

            string answerText = questionAndAnswerInfo.AnswerText;

            // Get the generic label elements for ALL questions:

            // If we dont find the generic label element, then that means the developers changed the HTML for these labels
            if (Browser.FindElements(Bys.ActAssessmentPage.QuestionTextLbls).Count == 0)
            {
                throw new Exception("The HTML has changed for the question labels. You will have to update the xpath in the " +
                    "xpathForQuestionLabelElements string above");
            }
            IList<IWebElement> QuestionLabelElements = Browser.FindElements(Bys.ActAssessmentPage.QuestionTextLbls);

            IWebElement QuestionContainer = GetQuestionContainer(QuestionLabelElements, questionAndAnswerInfo, questionText);

            // Dropdowns
            if (questionAndAnswerInfo.QuestionTypeName.Contains("DropDown") || questionAndAnswerInfo.QuestionTypeName == "Gender"
                || questionAndAnswerInfo.QuestionTypeName == "Profession"
                )
            {
                ChooseAnswer_DropDown(Browser, questionText, answerText, questionAndAnswerInfo.IsOther, QuestionContainer);
                return;
            }

            // TextLabel questions do not have answer choices
            else if (questionAndAnswerInfo.QuestionTypeName == "TextLabel")
            {
                return;
            }

            // Radio buttons
            else if (questionAndAnswerInfo.QuestionTypeName == "ChoiceOneAnswer" || questionAndAnswerInfo.QuestionTypeName == "TrueFalse"
                || questionAndAnswerInfo.QuestionTypeName == "YesNo" || questionAndAnswerInfo.QuestionTypeName == "RatingScaleOneAnswer")
            {
                // Whenever we have Matrix questions in our assessments, the database query returns these questions as RatingScaleMatrix
                // correctly, but for some reason, it returns extra rows other than the RatingScaleMatrix row. It returns ChoiceOneAnswer
                // rows, and inserts the answer text of each matrix answer to the QuestionText field. I think this may be a defect 
                // for DEV to fix. I have not looked into it that much and am putting a quick workaround in here now. Note this 
                // workaround will fail if any of the answer texts inside the matrix question match an answer from another question
                // 
                if (questionAndAnswerInfo.QuestionTypeName == "ChoiceOneAnswer")
                {
                    // If a matrix question is found from QuestionText, then its actually coming from the matrix questiontype, not the 
                    // ChoiceOneAnswer question type. If this is the case, skip it. See big comment a couple lines above this comment 
                    // for more info.
                    string xpathOfMatrixQuestion = string.Format("//div[text()='{0}']/ancestor::div[contains(@class, 'matrix')]",
                        questionAndAnswerInfo.QuestionText);
                    if (Browser.FindElements(By.XPath(xpathOfMatrixQuestion)).Count > 0)
                    {
                        return;
                    }
                }
                ChooseAnswer_RadioButton(Browser, questionText, answerText, questionAndAnswerInfo.IsOther, QuestionContainer);
                return;
            }

            else if (questionAndAnswerInfo.QuestionTypeName == "RatingScaleMatrix")
            {
                ChooseAnswer_RatingScaleMatrix(Browser, questionText, answerText);
                return;
            }

            // Check boxes
            else if (questionAndAnswerInfo.QuestionTypeName == "ChoiceMultipleAnswers")
            {
                // If you called this method, you used the GetAssessmentQuestionsAndAnswers to get all questions and answers. Which means 
                // you may have set the returnDistinctQuestionsWithStuffedAnswers parameter to true. If so, we would need to split the 
                // stuffed string into an array of answers. Doing this now...
                string[] answers = answerText.Split(new string[] { ", NextAnswer: " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var answer in answers)
                {
                    ChooseAnswer_CheckBox(Browser, questionText, answer, questionAndAnswerInfo.IsOther, QuestionContainer);
                }

                return;
            }

            // Date picker
            else if (questionAndAnswerInfo.QuestionTypeName == "DatePicker")
            {
                ChooseAnswer_DatePicker(questionText, answerText, QuestionContainer);
                return;
            }

            // Single line text field
            else if (questionAndAnswerInfo.QuestionTypeName == "TextOneAnswer")
            {
                ChooseAnswer_TextFieldSingleLine(questionText, answerText, QuestionContainer);
                return;
            }

            // Multi line text field
            else if (questionAndAnswerInfo.QuestionTypeName == "TextOneAnswerMultiLine")
            {
                ChooseAnswer_TextFieldMultiLine(questionText, answerText, QuestionContainer);
                return;
            }

        }

        /// <summary>
        /// Loops through all QuestionLabelElements until the question text that we are targetting is found within that labels
        /// innerText. Once found, returns the parent question container element that holds this label element
        /// </summary>
        /// <param name="questionText"></param>
        /// <returns></returns>
        public IWebElement GetQuestionContainer(IList<IWebElement> QuestionLabelElements,
            Constants.AssQAndAs questionAndAnswerInfo, string questionText)
        {
            IWebElement QuestionContainer = null;
            foreach (var QuestionLabelElement in QuestionLabelElements)
            {
                string blah = QuestionLabelElement.GetAttribute("innerText");
                if (QuestionLabelElement.GetAttribute("innerText") == questionText)
                {
                    QuestionContainer = QuestionLabelElement.FindElement(By.XPath("ancestor::div[@class='form-input-row-container']"));
                    break;
                }
            }

            // If QuestionContainer was not populated in the above loop (and the question type was not a text label or a rating scale matrix, 
            // then something went wrong, throw error
            if (questionAndAnswerInfo.QuestionTypeName != "TextLabel" &
                questionAndAnswerInfo.QuestionTypeName != "RatingScaleMatrix" &
                QuestionContainer == null)
            {
                throw new Exception(string.Format("The question container could not be found with question text: {0}. Something " +
                    "went wrong", questionText));
            }

            return QuestionContainer;
        }

        /// <summary>
        /// Chooses an answer
        /// </summary>
        /// <param name="questionAndAnswerInfo">
        /// <see cref="DBUtils.GetAssessmentQuestionsAndAnswers(string, string, string, bool?, bool?, bool?, bool?, bool)"/></param>
        public void ChooseAllAnswersUsingQueryResult(IWebDriver Browser, List<Constants.AssQAndAs> questionsAndAnswersInfo)
        {
            // Choose an answer for all questions
            foreach (Constants.AssQAndAs questionAndAnswerInfo in questionsAndAnswersInfo)
            {
                // Required questions have an asterisks after them.
                // 5/1/2020: Now doing this at the query level
                //string questionText = !questionAndAnswerInfo.IsRequired ? questionAndAnswerInfo.QuestionText : questionAndAnswerInfo.QuestionText + "*";
                string questionText = questionAndAnswerInfo.QuestionText;

                // Matrix requuired question text does not include an asterisks after it
                questionText = questionAndAnswerInfo.QuestionTypeName == "RatingScaleMatrix" ? questionText.TrimEnd('*') : questionText;

                string answerText = questionAndAnswerInfo.AnswerText;

                // Get the label element for all questions
                string xpathForQuestionLabelElements = "//div[contains(@class, 'form-input-label')]";
                IList<IWebElement> QuestionLabelElements = Browser.FindElements(By.XPath(xpathForQuestionLabelElements));

                if (QuestionLabelElements.Count == 0)
                {
                    throw new Exception("The HTML has changed for the question labels. You will have to update the xpath in the " +
                        "xpathForQuestionLabelElements string above");
                }

                // Next, loop through all QuestionLabelElements until the question text that we are targetting is found within that labels
                // innerText. Once found, find the parent question container element that holds this label element and assign to to the 
                // QuestionContainer element, so we can then pass it to the below ChooseAnswer method calls
                IWebElement QuestionContainer = null;
                foreach (var QuestionLabelElement in QuestionLabelElements)
                {
                    if (QuestionLabelElement.GetAttribute("innerText") == questionText)
                    {
                        QuestionContainer = QuestionLabelElement.FindElement(By.XPath("ancestor::div[@class='form-input-row-container']"));
                        break;
                    }
                }

                // If QuestionContainer was not populated in the above loop, then something went wrong, throw error
                if (questionAndAnswerInfo.QuestionTypeName != "TextLabel" &
                    questionAndAnswerInfo.QuestionTypeName != "RatingScaleMatrix" &
                    QuestionContainer == null)
                {
                    throw new Exception(string.Format("The question container could not be found with question text {0}, something " +
                        "went wrong", questionText));
                }

                // Dropdowns
                if (questionAndAnswerInfo.QuestionTypeName.Contains("DropDown") || questionAndAnswerInfo.QuestionTypeName == "Gender"
                    || questionAndAnswerInfo.QuestionTypeName == "Profession"
                    )
                {
                    ChooseAnswer_DropDown(Browser, questionText, answerText, questionAndAnswerInfo.IsOther, QuestionContainer);
                    continue;
                }

                // TextLabel questions do not have answer choices
                else if (questionAndAnswerInfo.QuestionTypeName == "TextLabel")
                {
                    continue;
                }

                // Radio buttons
                else if (questionAndAnswerInfo.QuestionTypeName == "ChoiceOneAnswer" || questionAndAnswerInfo.QuestionTypeName == "TrueFalse"
                    || questionAndAnswerInfo.QuestionTypeName == "YesNo" || questionAndAnswerInfo.QuestionTypeName == "RatingScaleOneAnswer")
                {
                    ChooseAnswer_RadioButton(Browser, questionText, answerText, questionAndAnswerInfo.IsOther, QuestionContainer);
                    continue;
                }

                else if (questionAndAnswerInfo.QuestionTypeName == "RatingScaleMatrix")
                {
                    ChooseAnswer_RatingScaleMatrix(Browser, questionText, answerText);
                    continue;
                }

                // Check boxes
                else if (questionAndAnswerInfo.QuestionTypeName == "ChoiceMultipleAnswers")
                {
                    ChooseAnswer_CheckBox(Browser, questionText, answerText, questionAndAnswerInfo.IsOther, QuestionContainer);
                    continue;
                }

                // Date picker
                else if (questionAndAnswerInfo.QuestionTypeName == "DatePicker")
                {
                    ChooseAnswer_DatePicker(questionText, answerText, QuestionContainer);
                    continue;
                }

                // Single line text field
                else if (questionAndAnswerInfo.QuestionTypeName == "TextOneAnswer")
                {
                    ChooseAnswer_TextFieldSingleLine(questionText, answerText, QuestionContainer);
                    continue;
                }

                // Multi line text field
                else if (questionAndAnswerInfo.QuestionTypeName == "TextOneAnswerMultiLine")
                {
                    ChooseAnswer_TextFieldMultiLine(questionText, answerText, QuestionContainer);
                    continue;
                }
            }
        }

        /// <summary>
        /// Chooses the answer you specify. If your answer is considered 'Other', it may have a text box associated to it. If so, it will 
        /// fill the text box with random text
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text of the answer</param>
        /// <param name="isAnswerOther">True or False If you chose an answer that is considered 'Other', then it may have a text box associated to it</param>
        public void ChooseAnswer_DropDown(IWebDriver Browser, string questionText, string answerText, bool isAnswerOther, IWebElement QuestionContainer)
        {
            // Selenium has issues selecting items in our custom Fireball dropdown element in Firefox
            //if (Browser.GetCapabilities().GetCapability("browserName").ToString().ToString() == BrowserNames.Firefox || Browser.MobileEnabled())
            //{
            IWebElement DropDownBtn = QuestionContainer.FindElement(By.XPath(string.Format("descendant::button", questionText)));
            ElemSet.DropdownSingle_Fireball_SelectByText(Browser, DropDownBtn, answerText);
            //}
            //else
            //{
            //    SelectElement SelectElem = new SelectElement(
            //        QuestionContainer.FindElement(By.XPath(string.Format("descendant::select", questionText))));
            //    SelectElem.SelectByText(answerText);
            //}

            if (isAnswerOther)
            {
                IWebElement Txt = QuestionContainer.FindElement(By.XPath(string.Format("descendant::input[@type='text' and contains(@style, 'display')]", questionText)));
                Txt.SendKeys("auto");
            }
        }

        /// <summary>
        /// Chooses the answer you specify. If your answer is considered 'Other', it may have a text box associated to it. If so, it will 
        /// fill the text box with random text
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text of the answer</param>
        /// <param name="isAnswerOther">True or False If you chose an answer that is considered 'Other', then it may have a text box associated to it</param>
        /// <param name="QuestionContainer"></param>
        public void ChooseAnswer_RadioButton(IWebDriver Browser, string questionText, string answerText, bool isAnswerOther,
            IWebElement QuestionContainer)
        {
            //IList<IWebElement> QuestionGroupingContainer = Browser.FindElements(By.XPath(string.Format("//div[text()='{0}']/..", questionText)));

            // First get the radio button label element
            IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtnByText(Browser, answerText, QuestionContainer);
            // Next get the input element and see if its disabled. If not disabled, then click it. If it is disabled, then dont do anything
            // Note that I added this line whenever we were coding the AssesmentAndConfiguration test for the new assessments that allow for 
            // disabling entire questions based on previous questions answers. This new test had an assessment where a question was disabled.
            // It would be complex and take some time to create a query right now to handle the condition below dynamically, so we are just 
            // going to skip if the element is disabled. Note that this wont work if we test other assessments
            // that have other question types disabled. And actually, this might result in us potentially not catching a defect if a question was
            // disabled when it was not supposed to be. Might have to revisit this.
            IWebElement ChildOfRadioButton = rdoBtn.FindElement(By.XPath("descendant::input"));
            if (ChildOfRadioButton.Enabled)
            {
                ElemSet.RdoBtn_ClickByText(Browser, answerText, QuestionContainer);

                if (isAnswerOther)
                {
                    if (QuestionContainer.FindElements(By.XPath(string.Format("descendant::input[@type='text']", questionText))).Count > 0)
                    {
                        IWebElement Txt = QuestionContainer.FindElement(By.XPath(string.Format("descendant::input[@type='text']", questionText)));
                        Txt.SendKeys("auto");
                    }
                }
            }
        }

        /// <summary>
        /// NOTE: This version of the method is for when you are not using a query to get questions 
        /// Chooses the answer you specify. If your answer is considered 'Other', it may have a text box associated to it. If so, it will 
        /// fill the text box with random text. 
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text of the answer</param>
        /// <param name="isAnswerOther">True or False If you chose an answer that is considered 'Other', then it may have a text box associated to it</param>
        public void ChooseAnswer_RadioButton(IWebDriver Browser, string questionText, string answerText, bool isAnswerOther)
        {
            // Get the specific label from the user-specified question text
            var QuestionLabelElement =
                Browser.FindElements(Bys.ActAssessmentPage.QuestionTextLbls).First(e => e.GetAttribute("innerText") == questionText);

            // Get the question container from the above user-specified question
            var QuestionContainer = QuestionLabelElement.FindElement(By.XPath("ancestor::div[@class='form-input-row-container']"));

            // First get the radio button label element
            IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtnByText(Browser, answerText, QuestionContainer);

            IWebElement ChildOfRadioButton = rdoBtn.FindElement(By.XPath("descendant::input"));
            if (ChildOfRadioButton.Enabled)
            {
                ElemSet.RdoBtn_ClickByText(Browser, answerText, QuestionContainer);

                if (isAnswerOther)
                {
                    if (QuestionContainer.FindElements(By.XPath(string.Format("descendant::input[@type='text']", questionText))).Count > 0)
                    {
                        IWebElement Txt = QuestionContainer.FindElement(By.XPath(string.Format("descendant::input[@type='text']", questionText)));
                        Txt.SendKeys("auto");
                    }
                }
            }
        }

        /// <summary>
        /// Chooses the answer you specify. 
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text of the answer</param>
        public void ChooseAnswer_RatingScaleMatrix(IWebDriver Browser, string questionText, string answerText)
        {
            // Get the generic label elements for ALL matrix questions
            string xpathForMatrixQuestionLabelElements = "//div[contains(@class, 'form-matrix-label')]";

            // If we dont find a generic label element, then that means the developers changed the HTML for these labels
            if (Browser.FindElements(By.XPath(xpathForMatrixQuestionLabelElements)).Count == 0)
            {
                throw new Exception("The HTML has changed for the question labels. You will have to update the xpath in the " +
                    "xpathForQuestionLabelElements string above");
            }

            IList<IWebElement> MatrixQuestionLabelElements = Browser.FindElements(By.XPath(xpathForMatrixQuestionLabelElements));

            // Next, loop through all MatrixQuestionLabelElements until the question text that we are targetting is found within that labels
            // innerText. Once found, find the parent question container element that holds this label element and assign to the 
            // QuestionContainer element
            IWebElement QuestionContainer = null;
            foreach (var QuestionLabelElement in MatrixQuestionLabelElements)
            {
                string blah = QuestionLabelElement.GetAttribute("innerText").Trim();
                if (blah == questionText)
                {
                    QuestionContainer = QuestionLabelElement.FindElement(By.XPath("ancestor::div[contains(@class, 'form-matrix')]"));
                    break;
                }
            }

            // Find each row inside the matrix question container, and click each row (which will click the first radio button of each row)
            List<IWebElement> Rows = QuestionContainer.FindElements(By.XPath("descendant::div[@class='form-input']/span")).ToList();

            foreach (var Row in Rows)
            {
                Row.FindElement(By.TagName("input")).Click();
            }
        }

        /// <summary>
        /// Chooses the answer you specify. If your answer is considered 'Other', it may have a text box associated to it. If so, it will 
        /// fill the text box with random text
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text of the answer</param>
        /// <param name="isAnswerOther">True or False If you chose an answer that is considered 'Other', then it may have a text box associated to it</param>
        public void ChooseAnswer_CheckBox(IWebDriver Browser, string questionText, string answerText, bool isAnswerOther, IWebElement QuestionContainer)
        {
            //IList<IWebElement> QuestionGroupingContainer = QuestionContainer.FindElements(By.XPath(string.Format("//div[text()='{0}']/..", questionText)));

            // If multiple answers have the same text, we will have to choose all of them
            IList<IWebElement> Chks = ElemGet.ChkBx_GetCheckBoxes(Browser, answerText, QuestionContainer);
            foreach (var Chk in Chks)
            {
                if (Chk.GetAttribute("checked") != "true")
                {
                    // MJ 4-28-21: On this day, the AssessmentConfiguration tests starting failing randomly when executing 
                    // all tests on the Grid in parallel. It was intermittent and it threw the below error. The screenshots 
                    // didnt really reveal anything. I think maybe the application was putting the checkbox label in front 
                    // of the checkbox for a brief amount of time but im not sure. Anyway, I am adding a sleep and a 
                    // Scroll before the click to see if this resolves it. If the pause does not work, we will have to 
                    // switch this to use a Javascript click, Chk.ClickJS()
                    // OpenQA.Selenium.ElementClickInterceptedException : element click intercepted:
                    // Element <input type="checkbox" name="4527170" id="activityPretestForm-4527170-2" value="3"> is
                    // not clickable at point (321, 564). Other element would receive the click:
                    // <label class="checkbox-input-label">...</label>
                    // MJ 4-28-21: Now using Javascript as the Sleep did not work
                    ElemSet.ScrollToElement(Browser, Chk);
                    //Thread.Sleep(1000);
                    Chk.Click(Browser);
                }
            }

            if (isAnswerOther)
            {
                IWebElement Txt = QuestionContainer.FindElement(By.XPath(string.Format("descendant::input[@type='text']", questionText)));
                Txt.SendKeys("auto");
            }
        }

        /// <summary>
        /// Chooses the answer you specify.
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text of the answer</param>
        public void ChooseAnswer_DatePicker(string questionText, string answerText, IWebElement QuestionContainer)
        {
            Thread.Sleep(1000);
            IWebElement Txt = QuestionContainer.FindElement(
                 By.XPath(string.Format("descendant::input[@data-attach-point='dateTimePickerInput']", questionText)));
            Txt.SendKeys("01/01/2025");
            Txt.SendKeys(Keys.Tab);
            return;
        }

        /// <summary>
        /// Chooses the answer you specify.
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text of the answer</param>
        public void ChooseAnswer_TextFieldSingleLine(string questionText, string answerText, IWebElement QuestionContainer)
        {
            IWebElement Txt = QuestionContainer.FindElement(
                 By.XPath(string.Format("descendant::input", questionText)));
            Txt.SendKeys("this is automation text!!!");
        }

        /// <summary>
        /// Chooses the answer you specify. 
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text of the answer</param>
        public void ChooseAnswer_TextFieldMultiLine(string questionText, string answerText, IWebElement QuestionContainer)
        {
            IWebElement Txt = QuestionContainer.FindElement(
                 By.XPath(string.Format("descendant::textarea", questionText)));
            Txt.SendKeys(string.Format("first line{0}second line", Environment.NewLine));
        }

        /// <summary>
        /// Returns true if the user-specified answer is the chosen answer from the user-specified question, 
        /// else false
        /// </summary>
        /// <param name="questionText">The exact text of the question</param>
        /// <param name="answerText">The exact text you expect the answer to be populated with/param>
        public bool IsAnswerSelected(IWebDriver Browser, string questionText, string answerText,
            Constants.QuestionTypeName questionTypeName)
        {
            bool answerSelected = false;

            // Get the specific label from the user-specified question text
            var QuestionLabelElement =
                Browser.FindElements(Bys.ActAssessmentPage.QuestionTextLbls).First(e => e.GetAttribute("innerText") == questionText);

            // Get the question container from the above user-specified question
            var QuestionContainer = QuestionLabelElement.FindElement(By.XPath("ancestor::div[@class='form-input-row-container']"));

            switch (questionTypeName)
            {
                case Constants.QuestionTypeName.DropDown:
                    IWebElement DropDownBtn = QuestionContainer.FindElement(By.XPath(string.Format("descendant::button", questionText)));
                    if (DropDownBtn.GetAttribute("title") == answerText)
                    {
                        answerSelected = true;
                    }
                    break;

                case Constants.QuestionTypeName.RadioButton:
                    // Get the radio button label element
                    IWebElement rdoBtn = ElemGet.RdoBtn_GetRdoBtnByText(Browser, answerText, QuestionContainer);
                    IWebElement ChildOfRadioButton = rdoBtn.FindElement(By.XPath("descendant::input"));
                    if (ChildOfRadioButton.Selected)
                    {
                        return true;
                    }
                    break;

                case Constants.QuestionTypeName.TextOneAnswer:
                    IWebElement Txt = QuestionContainer.FindElement(By.XPath(string.Format("descendant::input", questionText)));
                    if (Txt.GetAttribute("value") == answerText)
                    {
                        return true;
                    }
                    break;

                case Constants.QuestionTypeName.DatePicker:
                    IWebElement DatePickerTxt = QuestionContainer.FindElement(
                    By.XPath(string.Format("descendant::input[@data-attach-point='dateTimePickerInput']", questionText)));
                    if (DatePickerTxt.GetAttribute("value") == answerText)
                    {
                        return true;
                    }
                    break;

                case Constants.QuestionTypeName.TextLabel:
                    throw new Exception("This isnt coded yet. Add your code");
                    break;
                case Constants.QuestionTypeName.ChoiceOneAnswer:
                    throw new Exception("This isnt coded yet. Add your code");
                    break;
                case Constants.QuestionTypeName.RatingScaleMatrix:
                    throw new Exception("This isnt coded yet. Add your code");
                    break;
                case Constants.QuestionTypeName.ChoiceMultipleAnswers:
                    throw new Exception("This isnt coded yet. Add your code");
                    break;
                case Constants.QuestionTypeName.TextOneAnswerMultiLine:
                    throw new Exception("This isnt coded yet. Add your code");
                    break;
            }


            return answerSelected;
        }


        #endregion Activity Workflow

        #region environment

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public bool EnvironmentEquals(Constants.Environments environment)
        {
            if (Constants.CurrentEnvironment == environment.GetDescription())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateWhenEnvironmentIsReady"></param>
        public bool EnvironmentReadyAfterDate(DateTime DateWhenEnvironmentIsReady, Constants.Environments environment)
        {
            if (DateTime.Today < DateWhenEnvironmentIsReady && Constants.CurrentEnvironment == environment.GetDescription())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateWhenEnvironmentIsReady"></param>
        public bool ReturnTrueIfTodaysDateComesBefore(DateTime date)
        {
            if (DateTime.Today < date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// You can ignore a test on production until after a certain date
        /// </summary>
        /// <param name="DateWhenEnvironmentIsReady"></param>
        public void IgnoreTestOnEnvironmentUntilDate(DateTime DateWhenEnvironmentIsReady, Constants.Environments environment)
        {
            if (DateTime.Today < DateWhenEnvironmentIsReady && Constants.CurrentEnvironment == environment.GetDescription())
            {
                Assert.Ignore();
            }
        }

        /// <summary>
        /// You can ignore a test on production until after a certain date
        /// </summary>
        /// <param name="DateWhenEnvironmentIsReady"></param>
        public void IgnoreTestOnEnvironment(Constants.Environments environment)
        {
            if (Constants.CurrentEnvironment == environment.GetDescription())
            {
                Assert.Ignore();
            }
        }


        #endregion environment

        #region Custom pages

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tbl"></param>
        /// <param name="iframeWhereHdrLinksExist"></param>
        /// <returns></returns>
        public (int CountOfLinks, List<string> LinkText) GetFireballCustomPageTableLinkCountAndText(IWebDriver Browser, IWebElement Tbl)
        {
            IList<IWebElement> HeaderLinks = null;
            List<string> textOfAllLinks = new List<string>();
            int countOfAllLinks = 0;
            int countOfHeaderLinks = 0;

            // The custom header links are in iframes, so switch to the iframe and add them to the count and text
            // NOTE: Whenever the client does not add anything to the custom header in the Admin tools, then this 
            // iFrame wont exist and no links will be there. So we only switch to it if it exists
            if (Tbl.FindElements(By.XPath("ancestor::div[contains(@class, 'fireball-widget')][2]//iframe")).Count > 0)
            {
                IWebElement CustomHdrFrame = Tbl.FindElement(By.XPath("ancestor::div[contains(@class, 'fireball-widget')][2]//iframe"));
                Browser.SwitchTo().Frame(CustomHdrFrame);
                HeaderLinks = Browser.FindElements(By.XPath("descendant::a[@href]"));
                textOfAllLinks.AddRange(HeaderLinks.Select(elem => elem.GetAttribute("href")).ToList());
                countOfHeaderLinks = HeaderLinks.Count;
            }

            // Add all Activity Listing links to the count and text
            Browser.SwitchTo().DefaultContent();
            // MJ 4/4/19: Today tests failed. The reason is because the next line of code was finding more than usual H4 elements. After inspection
            // it was revealed that these tables now have hidden activities of some sort (H4 elements within Div elements that contain the class
            // attribute "activity-card-inner hiddenActivityRows". So we now have to exclude these hidden activity H4's. See the line after next 
            // for how I did that
            //List<IWebElement> ActivityListinginks = Tbl.FindElements(By.TagName("h4")).ToList();
            // 8/23/19: Added the second condition in the xpath due to DEV changing the HTML (the H4 is now inside a button element). 
            // Keeping the first condition here just in case it gets reverted back
            List<IWebElement> ActivityListinginks = Tbl.
                FindElements(By.XPath("descendant::div[not(@class='activity-card-inner hiddenActivityRows')]/div/h4 | descendant::div[not(@class='activity-card-inner hiddenActivityRows')]/div/button/h4")).ToList();

            textOfAllLinks.AddRange(ActivityListinginks.Select(elem => elem.Text.Trim()).ToList());
            countOfAllLinks = countOfHeaderLinks + ActivityListinginks.Count;

            var sortedTextOfAllLinks = textOfAllLinks.OrderBy(q => q).ToList();

            return (countOfAllLinks, sortedTextOfAllLinks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tbl"></param>
        /// <returns></returns>
        public (int CountOfLinks, List<string> LinkText) GetLegacyCustomPageTableLinkCountAndText(IWebDriver Browser, IWebElement Tbl,
            IList<IWebElement> HeaderLinks)
        {
            List<string> textOfAllLinks = new List<string>();
            int countOfAllLinks = 0;

            // Add the custom header link count and text
            textOfAllLinks.AddRange(HeaderLinks.Select(elem => elem.GetAttribute("href")).ToList());
            int countOfHeaderLinks = HeaderLinks.Count;

            // Add the Activity Listing link count and text
            List<IWebElement> ActivityListinginks = Tbl.FindElements(By.XPath("descendant::td/a")).ToList();
            textOfAllLinks.AddRange(ActivityListinginks.Select(elem => elem.Text.Trim()).ToList());
            countOfAllLinks = countOfHeaderLinks + ActivityListinginks.Count;

            var sortedTextOfAllLinks = textOfAllLinks.OrderBy(q => q).ToList();

            return (countOfAllLinks, sortedTextOfAllLinks);
        }


        #endregion Custom pages

        #region Go to page


        /// <summary>
        /// If not logged in and you specify a username in the parameter, this will log onto UAMS with your user. If logged in with 
        /// a different user, it will sign out then log in as your specified user. If logged in with your specified user, it will 
        /// stay logged in. If you do not specify a user, this will not log in at all. It will then enter the activity ID 
        /// into the URL to go to the registration page. NOTE: If you specify a username, then this user must NOT be registered to 
        /// the activity
        /// already
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="activityTitle">The title of the activity. To see a list of stored activities, <see cref="Constants_UAMS.ActTitle"/></param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has never logged in or not. Default = false</param>
        /// <param name="username">(Optional). Enter the username if you want to go to the registration page with this user. If not, leave it blank and this method will go to the registration page without logging in</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation users</param>
        public dynamic GoTo_ActivityNonWorkflow_PreviewPageViaSearch(IWebDriver Browser, Constants.SiteCodes siteCode, string activityTitle, bool isNewUser = false,
            string username = null, string password = null)
        {
            // Instantiate all the necessary page classes
            HomePage HP = new HomePage(Browser);
            SearchPage SP = new SearchPage(Browser);
            LoginPage LP = new LoginPage(Browser);

            // If the tester wants a user to be logged in before going to the registration page
            if (!string.IsNullOrEmpty(username))
            {
                // If not logged in already
                if (!Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
                {
                    // If not at the login page
                    if (!Browser.Exists(Bys.LoginPage.UserNameTxt))
                    {
                        Navigation.GoToLoginPage(Browser, siteCode);
                    }
                    LP.Login(username, password, isNewUser);
                }

                // Else if logged in already
                else if (Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
                {
                    // If not logged in with a user that the tester specified
                    if (!string.IsNullOrEmpty(username))
                    {
                        if (HP.GetUserName().ToLower() != username.ToLower())
                        {
                            HP.LogOut(siteCode);
                            Navigation.GoToLoginPage(Browser, siteCode);
                            LP.Login(username, password, isNewUser);
                        }
                    }
                }
            }

            // Search for the activity
            HP.Search(Constants.ActivitySearchType.All, activityTitle);

            // If the search did not return any results, throw exception to user
            if (Browser.Exists(Bys.SearchPage.NoDataAvailableLbl, ElementCriteria.IsVisible))
            {
                throw new Exception("Your search text did not return any activities");
            }

            // Click the activity link.
            return SP.ClickActivity(activityTitle);
        }

        /// <summary>
        /// Returns the activity ID from the URL when you are on the activity page
        /// </summary>
        public string GetActivityIDFromURL(IWebDriver Browser)
        {
            string url = Browser.Url;
            string actId = null;

            // There is a new thing where all activities show a string at the end of the URL that says 
            // &@activity.bundleActivityId=-1. Before the application never showed this. Per DEV, this
            // is now expected. So I am adding this condition to check for that, and if it is there
            // get the second to last index of '=', which would be the real activity ID.
            if (url.Contains("bundleActivityId"))
            {
                string actIdStart = url.Substring(url.Substring(0, url.LastIndexOf("=")).LastIndexOf("=") + 1);
                actId = actIdStart.Substring(0, actIdStart.IndexOf("&"));
            }
            else
            {
                actId = Browser.Url.Substring(Browser.Url.LastIndexOf('=') + 1);
            }

            return actId;
        }

        /// <summary>
        /// If not logged in and you specify a username in the parameter, this will log onto UAMS with your user. If logged in with 
        /// a different user, it will sign out then log in as your specified user. If logged in with your specified user, it will 
        /// stay logged in. If you do not specify a user, this will not log in at all. It will then get the activity ID by using 
        /// the activity title that you passed, then it will insert that into the URL and wait for the Front page to load. 
        /// NOTE: If you specify a username, then this user must NOT be registered to the activity already
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="activityTitle">The title of the activity. To see a list of stored activities, <see cref="Constants_UAMS.ActTitle"/></param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has never logged in or not. Default = false</param>
        /// <param name="username">(Optional). Enter the username if you want to go to the registration page with this user. If not, leave it blank and this method will go to the registration page without logging in</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation users</param>
        public dynamic GoTo_ActivityNonWorkflow_PreviewPageViaURL(IWebDriver Browser, Constants.SiteCodes siteCode,
            string activityTitle, bool isNewUser = false, string username = null, string password = null)
        {
            // Instantiate all the necessary page classes
            HomePage HP = new HomePage(Browser);
            SearchPage SP = new SearchPage(Browser);
            LoginPage LP = new LoginPage(Browser);
            ActPreviewPage APP = new ActPreviewPage(Browser);

            // If the tester wants a user to be logged in before going to the registration page
            if (!string.IsNullOrEmpty(username))
            {
                // If not logged in already
                if (!Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
                {
                    // If not at the login page
                    if (!Browser.Exists(Bys.LoginPage.UserNameTxt))
                    {
                        Navigation.GoToLoginPage(Browser, siteCode);
                    }

                    LP.Login(username, password, isNewUser);
                }

                // Else if logged in already
                else if (Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
                {
                    // If not logged in with a user that the tester specified
                    if (!string.IsNullOrEmpty(username))
                    {
                        if (HP.GetUserName().ToLower() != username.ToLower())
                        {
                            HP.LogOut(siteCode);
                            Navigation.GoToLoginPage(Browser, siteCode);
                            LP.Login(username, password, isNewUser);
                        }
                    }
                }

            }

            string urlWithSiteCodeWithActivityID = string.Format("{0}{1}{2}", GetBaseURLWithSiteCode(siteCode), "lms/activity?@activity.id=",
                DBUtils.GetActivityID(siteCode, activityTitle));
            Browser.Navigate().GoToUrl(urlWithSiteCodeWithActivityID);

            // Wait for the registration page. If it does not appear, then tell the tester he has already registered with this user,
            // or that there was an issue with the page load
            try
            {
                APP.WaitForInitialize();
            }
            catch
            {
                throw new Exception("The Preview page did not load. Either you already registered for this activity and so the " +
                    "Activity Overview/Bundle/Assessment/On Hold page was accessed instead. Or the Registration page was unable to load fully due to an " +
                    "Application defect or performance issues. See screenshot for further info");
            }
            Thread.Sleep(300);
            return APP;

        }

        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly. Enters the activity 
        /// ID into the URL. If  you need to register, it will register for the activity and go to the Overview page. It will also make 
        /// payment if needed. If not, it will go straight to the Overview page. Then it will complete the Overviewpage and click Continue 
        /// until it reaches the page you want to land on
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="siteCode"></param>
        /// <param name="activityTitle"></param>
        /// <param name="activityPage"><see cref="Constants.Pages_ActivityPage"/></param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has never logged in or not. Default = false</param>
        /// <param name="username">(Optional). If you want to specify a specific user to login with, enter the username. If not, leave it blank and this method will generate a username for you</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation users</param>
        /// <param name="actId">(PROD-ONLY). Pass the activity ID if you are running on Prod</param>
        /// <param name="PIM">'true' if this is a PIM activity. PIM activities have a different workflow and rules, so conditions will be different</param>
        /// 
        /// <returns></returns>
        public dynamic GoTo_ActivityWorkflow_SpecificPage(IWebDriver Browser, Constants.SiteCodes siteCode, string activityTitle,
            Constants.Pages_ActivityPage activityPage, bool isNewUser = false, string username = null, string password = null,
            string actId = null, string discountCode = null, bool PIM = false)
        {
            // Instantiate all the necessary page classes
            ActOverviewPage OP = new ActOverviewPage(Browser);
            ActMaterialPage MP = new ActMaterialPage(Browser);
            ActAssessmentPage AP = new ActAssessmentPage(Browser);
            ActBundlePage BP = new ActBundlePage(Browser);
            ActOnHoldPage AOHP = new ActOnHoldPage(Browser);
            ActSessionsPage ASP = new ActSessionsPage(Browser);
            ActPIMPage PIMPP = new ActPIMPage(Browser);
            ActClaimCreditPage CCP = new ActClaimCreditPage(Browser);
            ActCertificatePage CP = new ActCertificatePage(Browser);

            GoTo_ActivityWorkflow_AnyInProgressPage(Browser, siteCode, activityTitle, isNewUser, username, password, actId,
                discountCode, PIM);

            // If any of the above clicks take us to the Certificate page, click to the Overview page to reset
            if (Browser.Exists(Bys.ActCertificatePage.FinishBtn, ElementCriteria.IsVisible) ||
                Browser.Exists(Bys.ActCertificatePage.ContinueBtn, ElementCriteria.IsVisible))
            {
                CP.ClickAndWaitBasePage(CP.ActivitiesView_ActivityOverviewTab);
            }

            // If this is a PIM activity, get the user to the first assessment. PIM pages are all custom, but they do have at least 1 
            // assessment. 
            if (PIM)
            {
                if (activityPage != Constants.Pages_ActivityPage.Assessment)
                {
                    throw new Exception("You can only specify to go to the Assessment page for a PIM. Please update your test");
                }

                while (!Browser.Exists(By.XPath("//input[@type='radio']")))
                {
                    PIMPP.ClickAndWait(PIMPP.ContinueBtn);
                }

                return PIMPP;
            }

            // Sometimes (very rarely, mostly just testing data) activities dont have overview pages, so we will only 
            // perform the below block of code if we are on the overview page at this point.
            if (Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription()))
            {
                Thread.Sleep(1000);

                // If there is a consent checkbox that has not been checked yet, the Continue button will not appear.
                if (Browser.Exists(Bys.ActOverviewPage.ContinueBtn, ElementCriteria.IsVisible))
                {
                    OP.ClickAndWait(OP.ContinueBtn);
                }
                else
                {
                    OP.ClickAndWait(OP.ActivityOverviewChk);
                }
            }

            // If any of the above clicks take us to the Bundle page
            if (Browser.Exists(Bys.ActBundlePage.ContinueBtn, ElementCriteria.IsVisible))
            {
                // If the tester specified that he wanted to get to the Bundle page
                if (activityPage == Constants.Pages_ActivityPage.Bundle)
                {
                    return BP;
                }

                BP.ClickAndWait(BP.ContinueBtn);
            }

            // If any of the above clicks take us to the On Hold page
            if (Browser.Exists(Bys.ActOnHoldPage.NotificationWarnIcon, ElementCriteria.IsVisible))
            {
                // If the tester specified that he wanted to get to the On Hold page
                if (activityPage == Constants.Pages_ActivityPage.OnHold)
                {
                    return AOHP;
                }
            }

            // If any of the above clicks take us to the Sessions page
            if (Browser.Exists(Bys.ActSessionsPage.AvailableSessionsTbl, ElementCriteria.IsVisible))
            {
                // If the tester specified that he wanted to get to the Sessions page
                if (activityPage == Constants.Pages_ActivityPage.Sessions)
                {
                    return ASP;
                }
            }

            // If any of the above clicks take us to the Material page
            if (Browser.Exists(Bys.ActMaterialPage.PleaseClickTheNameLbl, ElementCriteria.IsVisible))
            {
                // If the tester specified that he wanted to get to the Material page
                if (activityPage == Constants.Pages_ActivityPage.Material)
                {
                    return MP;
                }

                // Check the pre-required check box (if there is one), and click Continue. If Continue button appears,
                // that means the checkbox has already been clicked, so in this case, just click Continue and move on

                // On IE, whenever this page has content rows expanded and we try to click on something at the bottom
                // of the page, a weird issue happens and the page freezes. Scrolling first fixes this
                ElemSet.ScrollToElement(Browser, MP.ActivityMaterialChk, false);

                if (Browser.Exists(Bys.ActMaterialPage.ContinueBtn, ElementCriteria.IsVisible))
                {
                    MP.ClickAndWait(MP.ContinueBtn);
                }
                else
                {
                    MP.ClickAndWait(MP.ActivityMaterialChk);
                }
            }

            // If any of the above clicks take us to the Assessment page
            if (Browser.Exists(Bys.ActAssessmentPage.ContinueBtn, ElementCriteria.IsVisible) ||
                Browser.Exists(Bys.ActAssessmentPage.SubmitBtn, ElementCriteria.IsVisible) ||
                Browser.Exists(Bys.ActAssessmentPage.NextBtn, ElementCriteria.IsVisible) ||
                Browser.Exists(Bys.ActAssessmentPage.LaunchBtn, ElementCriteria.IsVisible) ||
                Browser.Exists(Bys.ActAssessmentPage.SaveAndFinishLaterBtn, ElementCriteria.IsVisible))
            {
                // If the tester specified that he wanted to get to the Assessment page, else complete all assessments
                if (activityPage == Constants.Pages_ActivityPage.Assessment)
                {
                    return AP;
                }

                List<Constants.Assessments> assessments = DBUtils.GetAssessmentsByActivityId(
                    AP.GetUserName(), siteCode, activityTitle, returnDistinctAssessments: true);

                foreach (var assessment in assessments)
                {
                    // If the next click is another assessment, pass it
                    if (Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription())
                            || Browser.Url.Contains(Constants.PageURLs.Activity_PIM.GetDescription()))
                    //|| Browser.Url.Contains(Constants.PageURLs.Activity_Assessment.GetDescription()))
                    {
                        AP.PassAssessmentIfNotSubmittedAlreadyAndContinueOrReturnToSummary(assessment);
                    }
                }

            }

            // If any of the above clicks take us to the Claim Credit page
            if (Browser.Exists(Bys.ActClaimCreditPage.ContinueBtn, ElementCriteria.IsVisible))
            {
                // If the tester specified that he wanted to get to the Claim Credit page
                if (activityPage == Constants.Pages_ActivityPage.ClaimCredit)
                {
                    return CCP;
                }
            }

            //return AP;

            throw new Exception("The activity you are using does not have the specific page you specified, or if it does have the " +
                "page, then the activity is on hold so you are not allowed to get there");
        }

        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly. Enters the 
        /// activity ID into the URL. If you need to register, it will register for the activity and go to the Overview page. It 
        /// will also make payment if needed. If not, it will go straight to the Overview page. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="activityTitle">The name of the activity</param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. Default = false</param>
        /// <param name="username">(Optional). If you want to specify a specific user to login with, enter the username. If not, leave it blank and this method will generate a username for you</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation users</param>
        /// <param name="actId">(PROD-ONLY). Pass the activity ID if you are running on Prod</param>
        /// <param name="discountCode"></param>
        /// <param name="PIM">'true' if this is a PIM activity. PIM activities have a different workflow and rules, so conditions will be different</param>
        public dynamic GoTo_ActivityWorkflow_OverviewPage(IWebDriver Browser, Constants.SiteCodes siteCode, string activityTitle,
            bool isNewUser = false, string username = null, string password = null, string actId = null, string discountCode = null,
            bool PIM = false)
        {
            ActOverviewPage AOP = new ActOverviewPage(Browser);
            ActPIMPage PIMPP = new ActPIMPage(Browser);

            GoTo_ActivityWorkflow_AnyInProgressPage(Browser, siteCode, activityTitle, isNewUser, username, password, actId, discountCode, PIM);

            if (!PIM)
            {
                AOP.ClickAndWaitBasePage(AOP.ActivitiesView_ActivityOverviewTab);

                // If the above method (GoTo_ActivityWorkflow_AnyInProgressPage) did not get us to the overview page, then something went wrong, 
                // and there is most  likely a bug on the application, or the activity is not configured to have an Overview page.
                // I witnessed a bug once when the application took me back to the Preview page after I clicked the Submit button 
                // on the Activity Registration page, and that definitely should not have occured
                if (!Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription()))
                {
                    throw new Exception("Either your activity is not configured to have an Overview page, or the page redirection may " +
                        "have a defect. See your failed test's screenshot for further details");
                }
                return AOP;
            }
            else
            {
                return PIMPP;
            }
        }

        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly. Enters the 
        /// activity ID into the URL. If you need to register, it will register for the activity and then go to any one of the pages 
        /// on the In Progress view. It will also make payment if needed. If not, it will go straight to the In Progress view 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="activityTitle">The name of the activity</param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. Default = false</param>
        /// <param name="username">(Optional). If you want to specify a specific user to login with, enter the username. If not, leave it blank and this method will generate a username for you</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation users</param>
        /// <param name="actId">(PROD-ONLY). Pass the activity ID if you are running on Prod</param>
        /// <param name="discountCode"></param>
        /// <param name="PIM">'true' if this is a PIM activity. PIM activities have a different workflow and rules, so conditions will be different</param>
        public void GoTo_ActivityWorkflow_AnyInProgressPage(IWebDriver Browser, Constants.SiteCodes siteCode, string activityTitle,
            bool isNewUser = false, string username = null, string password = null, string actId = null, string discountCode = null,
            bool PIM = false)
        {
            // Instantiate all the necessary page classes
            HomePage HP = new HomePage(Browser);
            SearchPage SP = new SearchPage(Browser);
            ActOverviewPage AOP = new ActOverviewPage(Browser);
            ActPreviewPage APP = new ActPreviewPage(Browser);
            ActRegistrationPage RP = new ActRegistrationPage(Browser);
            ProfilePage PP = new ProfilePage(Browser);
            LoginPage LP = new LoginPage(Browser);
            ActAssessmentPage AP = new ActAssessmentPage(Browser);
            ActCertificatePage CP = new ActCertificatePage(Browser);
            ActBundlePage BP = new ActBundlePage(Browser);
            ActMaterialPage AMP = new ActMaterialPage(Browser);
            ActPaymentPage APayP = new ActPaymentPage(Browser);
            ActCPEMonitorPage CPEP = new ActCPEMonitorPage(Browser);
            ActOrderDetailsPage AODP = new ActOrderDetailsPage(Browser);
            ActOrderReceiptPage AORP = new ActOrderReceiptPage(Browser);
            ActeCommercePage AEP = new ActeCommercePage(Browser);
            ActPIMPage PIMPP = new ActPIMPage(Browser);

            // If this is a PIM activity and we are on production, then a different login page will need to be used
            if (PIM && EnvironmentEquals(Constants.Environments.Production) && !Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
            {
                PIM_Login(Browser, username, password);
            }

            // Create the url string. Note that I am not using the activity overview URL (activity_overview) inside the URL because that caused 
            // problems with the wait criteria below. Whenever you enter any random string in between 'lms/' and '?@activity', it will go to
            // wherever the user left off on that activity. For example, if I enter activity_overview, but the last time the user accessed 
            // this activity, he/she was on the post assessment page, or the certificate page, it will quickly show 'activity_overview' in 
            // the URL bar, but then redirect to the post assessment/certificate page. As you would imagine, this causes issues when we use
            // the URL string for wait conditions. So to combat this, we are just passing "AnythingCanGoHere" into that spot, and then 
            // waiting for each and every page it can get redirected to, then clicking to get to the Activity Overview Page.
            string urlWithSiteCodeWithActivityID = null;
            // If we are on Prod, then the tester should have passed the actId. If not, get the count from the DB
            // NOTE: Right now I do have read access to Prod. So for my tests, right now, I wont be passing anything to 
            // the actId parameter
            if (actId == null)
            {
                urlWithSiteCodeWithActivityID = string.Format("{0}{1}{2}", GetBaseURLWithSiteCode(siteCode),
                    "lms/activity?@activity.id=", DBUtils.GetActivityID(siteCode, activityTitle));
            }
            else
            {
                urlWithSiteCodeWithActivityID = string.Format("{0}{1}{2}", GetBaseURLWithSiteCode(siteCode),
                    "lms/activity?@activity.id=", actId);
            }


            // If not logged in already
            if (!Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
            {
                Navigation.GoToLoginPage(Browser, siteCode);

                // If the tester wants a specific user to login with, login with that user, else login with a new user from API
                if (!string.IsNullOrEmpty(username))
                {
                    LP.Login(username, password, isNewUser);
                }
                else
                {
                    UserModel newUser = UserUtils.CreateUser(siteCode);
                    LP.Login(newUser.Username, newUser.Password, true);

                }
            }

            // If not logged in with a user that the tester specified
            if (!string.IsNullOrEmpty(username))
            {
                if (HP.GetUserName().ToLower() != username.ToLower())
                {
                    HP.LogOut(siteCode);
                    Navigation.GoToLoginPage(Browser, siteCode);
                    LP.Login(username, password, isNewUser);
                }
            }

            // If not on any of the activity pages(preview/overview/assessment etc.) for the activity already, then get there
            if (!Browser.Exists(Bys.LMSPage.ActivityTitleLbl, ElementCriteria.Text(activityTitle)))
            {
                Browser.Navigate().GoToUrl(urlWithSiteCodeWithActivityID);

                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                // Wait until the page URL loads
                var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(180));
                wait.Until(driver =>
                {
                    return
                      Browser.Url.Contains(Constants.PageURLs.Activity_Preview.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Bundle.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Content.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Bundle.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription())
                    // Should this PIM URL be here? Monitor going forward. I put it here because the delete activity 
                    // was not working for PIM sometimes, so it would directly take the user to a PIM page
                        || Browser.Url.Contains(Constants.PageURLs.Activity_PIM.GetDescription());
                });
            }

            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

            // If the user being tested has this activity in progress at one of the non-overview pages, and we navigate to the activity URL, 
            // the URL still quickly shows activity_overview, then switches to pretest (or wherever the user left off). So we will wait for 
            // the load icon to disappear first. The URL is fully done with switching once the load icon disappears
            if (Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription()))
            {
                AOP.WaitUntil(Criteria.ActOverviewPage.LoadIconNotExists);
                Thread.Sleep(1000);
                AOP.WaitUntil(Criteria.ActOverviewPage.LoadIconNotExists);
            }

            // If the above navigation takes us to the On Hold page (meaning the user already registered to this activity and it was/is an activity
            // that is On Hold, then tell the user
            if (Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription()))
            {
                throw new Exception("You can not access the activity because this activity is On Hold");
            }

            // If the above navigation takes us to the Activity Overview page, meaning we dont need to register
            if (Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription()))
            {
                AOP.WaitForInitialize();
                Thread.Sleep(300);
            }

            // Else if the above navigation takes us to one of Assessment/Bundle/Certificate/Content page. 
            // NOTE: If the user has already accessed the activity and was on an assessment page or a certfiicate page instead of the 
            // activity overview page, then the application will take him to that page even if the URL we navigated to was the 
            // activity overview page URL.
            else if (Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Content.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Bundle.GetDescription()))
            {
                if (Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                        || Browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription()))
                {
                    AP.WaitForInitialize();
                    Thread.Sleep(300);
                }
                else if (Browser.Url.Contains(Constants.PageURLs.Certificate.GetDescription()))
                {
                    CP.WaitForInitialize();
                }
                else if (Browser.Url.Contains(Constants.PageURLs.Activity_Content.GetDescription()))
                {
                    AMP.WaitForInitialize();
                }
                else if (Browser.Url.Contains(Constants.PageURLs.Activity_Bundle.GetDescription()))
                {
                    BP.WaitForInitialize();
                }

                // If the above navigation takes us to the On Hold page (meaning the user already registered to this activity and it was/is an activity
                // that is On Hold, then tell the user
                else if (Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription()))
                {
                    throw new Exception("You can not access the activity because this activity is On Hold");
                }
            }

            // Else if the above navigation takes us to the Preview page
            else
            {
                APP.WaitForInitialize();
                Thread.Sleep(300);

                // If the activity is not available to the user, then tell him
                if (Browser.Exists(Bys.ActPreviewPage.NotAvailableBtn))
                {
                    throw new Exception("You dont have access to this activity");
                }

                // If not registered or paid or needs to enter access code, then do that now
                if (Browser.Exists(Bys.ActPreviewPage.LaunchOrRegisterOrResumeBtn, ElementCriteria.IsVisible))
                {
                    APP.ClickAndWait(APP.LaunchOrRegisterOrResumeBtn);

                    // Due to the new development of fireball still using community, there is a defect which redirects new users
                    // (from the new user API call) to the Profile page. When this is fixed, we can remove this next If statement
                    if (Browser.Exists(Bys.ProfilePage.SaveBtn, ElementCriteria.IsVisible))
                    {
                        PP.SaveBtn.Click(Browser);

                        // Temporary code for ONSLT. Next sprint CMECA will properly redirect to fireball page. 
                        // For now, it redirects to legacy community page Remove this when ONS is fixed
                        if (PP.GetSiteCodeFromURL() == "CMECA")
                        {
                            Browser.Navigate().GoToUrl(urlWithSiteCodeWithActivityID);
                        }

                        APP.WaitForInitialize();

                        // For certain activities (I havent looked into which type of activities yet), a Verify Your Profession button
                        // pops up. If so, click Submit. Note that this usually happens AFTER you click the Register button in the code
                        // below this block of code, and so this If statement will be be used again after that block. There is only 
                        // one instance where the Verify Profession window appears BEFORE, and it is on Client Release in Firefox. So
                        // this If statement will take care of that
                        if (Browser.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn))
                        {
                            RP.ClickAndWait(RP.VerifyYourProfessionFormSubmitBtn);
                            PP.SaveBtn.Click(Browser);
                            Browser.Navigate().GoToUrl(urlWithSiteCodeWithActivityID);
                            APP.WaitForInitialize();
                        }

                        if (Browser.Exists(Bys.ActPreviewPage.LaunchOrRegisterOrResumeBtn))
                        {
                            APP.ClickAndWait(APP.LaunchOrRegisterOrResumeBtn);
                        }

                        // For certain activities (I havent looked into which type of activities yet), a Verify Your Profession button
                        // pops up. If so, click Submit
                        if (Browser.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn))
                        {
                            RP.ClickAndWait(RP.VerifyYourProfessionFormSubmitBtn);
                        }
                    }

                    // If Access Code is required, enter it. 
                    if (Browser.Exists(Bys.ActPreviewPage.AccessCodeFormAccessCodeTxt, ElementCriteria.IsVisible))
                    {
                        APP.AccessCodeFormAccessCodeTxt.SendKeys(DBUtils.GetAccessCode(activityTitle));
                        APP.ClickAndWait(APP.AccessCodeFormContinueBtn);
                    }

                    // If not registered, then register
                    if (Browser.Exists(Bys.ActRegistrationPage.FirstNameTxt, ElementCriteria.IsVisible))
                    {
                        RP.RegisterForActivity(username);
                    }

                    // If CPE Monitor activity, fill it out and submit
                    if (Browser.Exists(Bys.ActCPEMonitorPage.NABPEProfileIDTxt, ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
                    {
                        CPEP.FillAndSubmitForm();
                    }

                    // If not paid, then pay. Or if we are an SNMMI member and dont have to pay, click Complete Order
                    if (Browser.Exists(Bys.ActOrderDetailsPage.DiscountCodeTxt, ElementCriteria.IsVisible))
                    {
                        // If the tester passed a discount code
                        if (!string.IsNullOrEmpty(discountCode))
                        {
                            AODP.SubmitDiscountCode(discountCode);
                        }
                        // If the tester passed a discount code
                        else if (Browser.Exists(Bys.ActOrderDetailsPage.CompleteOrderBtn))
                        {
                            AODP.ClickAndWait(AODP.CompleteOrderBtn);
                        }
                        else
                        {
                            AODP.ClickAndWait(AODP.ContinueToPaymentBtn);
                            AODP.ClickAndWait(AODP.ConfirmFormOkBtn);
                        }

                        // If the ecommerce page appears prompting payment, then pay
                        if (Browser.Url.Contains(Constants.PageURLs.eCommerce_cybersource.GetDescription()))
                        {
                            AEP.CompletePayment();
                        }

                        // If the receipts page appears after payment, click Exit
                        AORP.ClickAndWait(AORP.ExitBtn);
                    }

                    // If the above navigation takes us to the On Hold page (meaning the user already registered to this activity and it was/is an 
                    // activity that is On Hold, then tell the user
                    else if (Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription()))
                    {
                        throw new Exception("You can not access the activity because this activity is On Hold");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CustomPage_VerifyOrderOfHTMLComponents(IWebDriver Browser)
        {
            // Get the class name attribute values for the grouped HTML components and the non grouped HTML components
            // and store them in list<string> #1
            // NOTE: Non-group elements have 'group0' in their class name attribute value
            List<string> nonGroupedElements = Browser.FindElements(Bys.LMSPage.CustomPageHTMLComponentNonGroups).
                Select(e => e.GetAttribute("class")).ToList();
            List<string> groupedElements = Browser.FindElements(Bys.LMSPage.CustomPageHTMLComponentGroups).
                Select(e => e.GetAttribute("class")).ToList();

            // Make sure that at least 1 of either a non-grouped or grouped element exists (custom pages should ALWAYS 
            // have at least 1)
            if (nonGroupedElements.Count == 0 && groupedElements.Count == 0)
            {
                throw new Exception("Every custom page should have at least 1 of either a grouped HTML component, " +
                    "or a non-grouped HTML component. If you see this error, the page may have not loaded" +
                    "correctly");
            }

            // Remove the names of the elements within the class attribute value, so that the strings only include
            // 'group#-order#'
            string toBeSearched = "-group";
            nonGroupedElements = nonGroupedElements.Select(e => e.Substring(e.IndexOf(toBeSearched) + 1)).ToList();
            groupedElements = groupedElements.Select(e => e.Substring(e.IndexOf(toBeSearched) + 1)).ToList();

            // Make a copy of list<string> #1 and sort it
            List<string> sortedNonGroupedElements = nonGroupedElements.ToList();
            List<string> sortedGroupedElements = groupedElements.ToList();
            sortedNonGroupedElements.Sort();
            sortedGroupedElements.Sort();

            // Compare the two lists and if they are equal, then they were sorted to start with
            // NOTE: If there are more than 9 groups or orders (i.e. group10-order13) the sorting will not be what
            // we want because above we sort the strings by alpha order not numerical order, e.g. 1, 10, 2, ... 
            // instead of 1, 2, ... 10. Will have to revisit if this ever happens            
            CollectionAssert.AreEqual(nonGroupedElements, sortedNonGroupedElements, "Verify ordering of elements");
            CollectionAssert.AreEqual(groupedElements, sortedGroupedElements, "Verify ordering of elements");
        }


        /// <summary>
        /// If not logged in already and the page specified is not the login page, logs in with either a user-specified username or 
        /// creates a new user on the fly, then goes to the specified page 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. Default = false</param>
        /// <param name="username">(Optional). If you want to specify a specific user to login with, enter the username.</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation users</param>
        public dynamic GoTo_Page(IWebDriver Browser, Constants.SiteCodes siteCode, Constants.Page page, string username,
            string password = null, bool isNewUser = false)
        {
            // Instantiate all the necessary page classes
            HomePage HP = new HomePage(Browser);
            SearchPage SP = new SearchPage(Browser);
            TranscriptPage TP = new TranscriptPage(Browser);
            ActivitiesInProgressPage AIP = new ActivitiesInProgressPage(Browser);
            LoginPage LP = new LoginPage(Browser);

            // If user only wants to go to the Login page
            if (page == Constants.Page.Login)
            {
                return Navigation.GoToLoginPage(Browser, siteCode);
            }
            else // Login
            {
                // If the tester wants a specific user to be logged in
                if (!string.IsNullOrEmpty(username))
                {
                    // If not logged in already
                    if (!Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
                    {
                        // If not at the login page
                        if (!Browser.Exists(Bys.LoginPage.UserNameTxt))
                        {
                            Navigation.GoToLoginPage(Browser, siteCode);
                        }
                        LP.Login(username, password, isNewUser);
                    }

                    // Else if logged in already
                    else if (Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
                    {
                        // If not logged in with a user that the tester specified
                        if (!string.IsNullOrEmpty(username))
                        {
                            if (HP.GetUserName().ToLower() != username.ToLower())
                            {
                                HP.LogOut(siteCode);
                                Navigation.GoToLoginPage(Browser, siteCode);
                                LP.Login(username, password, isNewUser);
                            }
                        }
                    }
                }
            }

            // Go to the non login page
            switch (page)
            {
                case Constants.Page.ActivitiesInProgress:
                    return Navigation.GoToActivitiesInProgressPage(Browser, siteCode);

                case Constants.Page.Transcript:
                    return Navigation.GoToTranscriptPage(Browser, siteCode);
            }

            return null;
        }


        #endregion Go to page

        #region Waiting

        /// <summary>
        ///  Wait until the page URL loads. Depending on the condition, clicking a button could take us to one of 
        ///  many different pages. So we have to condition to wait for any of these pages. IMPORTANT: Do not add code 
        ///  to use this to wait for the Preview Page
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="existingPage">The page you are currently on</param>
        /// <returns></returns>
        public dynamic WaitForNextPage(IWebDriver Browser, Constants.PageURLs existingPageURL)
        {
            // Wait until the old pages URL is gone
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(90));

            // Sometimes LMS throws a generic error message saying we can not retrieve your data. Most of the time this occurs
            // when the activity becomes corrupt for whatever reason. To fix it, you have to republish the activity
            try
            {
                // If this is a PIM, then the URL never changes, so we can not wait until it changes. We will sleep instead
                if (Browser.Url.Contains(Constants.PageURLs.Activity_PIM.GetDescription().ToLower()))
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    wait.Until(browser =>
                    {
                        return !browser.Url.Contains(existingPageURL.GetDescription().ToLower());
                    });
                }
            }
            catch
            {
                if (Browser.Exists(Bys.LMSPage.NotificationPageNoDataErrorLbl, ElementCriteria.IsVisible))
                {
                    throw new Exception("The LMS application threw a 'we were unable to retrieve your data' error message. This usually means " +
                        "the activity became corrupted for some reason. Republishing the activity usually resolves this issue");
                }
                else
                {
                    // Only adding 1 second here, because we waited 60 seconds already in the above Try block. If we wait
                    // 60 seconds and the LMS 'Oop we were unable to retrieve your data' message does not appear,
                    // the most likely the page is taking too long to load (61 seconds in this case)
                    var wait2 = new WebDriverWait(Browser, TimeSpan.FromSeconds(1));
                    wait2.Until(browser =>
                    {
                        return !browser.Url.Contains(existingPageURL.GetDescription().ToLower());
                    });
                }
            }


            // Wait until the next pages URL appears
            wait.Until(browser =>
            {
                return browser.Url.ToLower().Contains(Constants.PageURLs.Registration.GetDescription().ToLower())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Registration.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Payment_Legacy.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Order_Details.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Order_Payment_Receipt.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.eCommerce_cybersource.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_CPEConsent.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Login.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Bundle.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Bundle.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Content.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Sessions.GetDescription())
                    || browser.Url.ToLower().Contains(Constants.PageURLs.Profile.GetDescription().ToLower())
                    || browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Test.GetDescription()) // Post Test
                    || browser.Url.Contains(Constants.PageURLs.Activity_Evaluation.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_Followup.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Curriculum.GetDescription())
                    || browser.Url.Contains(Constants.PageURLs.Activity_PIM.GetDescription())
                    // Temporary code for ONSLT. Soon ONS will properly redirect to fireball page. For now, it redirects to legacy 
                    // community Account page. Remove when fireball comes
                    || browser.Url.Contains(Constants.PageURLs.Account.GetDescription());
            });

            // If an activity does not have an overview page, the URL still quickly shows activity_overview, then switches to pretest
            // (or whatever other test the activity has). So we will wait for the load icon to disappear first. The URL is fully done
            // with switching once the load icon disappears
            ActOverviewPage AOP = new ActOverviewPage(Browser);
            if (Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription()))
            {
                AOP.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActOverviewPage.LoadIconNotExists);
                Thread.Sleep(2000);
                AOP.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActOverviewPage.LoadIconNotExists);
            }

            // If an activity has a hold, but that hold expired, the URL still quickly shows activity_hold, then switches to pretest
            // (or whatever other test the activity has). So we will wait for the load icon to disappear first. The URL is fully done
            // with switching once the load icon disappears
            if (Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription()))
            {
                AOP.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActOverviewPage.LoadIconNotExists);
                Thread.Sleep(1000);
                AOP.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActOverviewPage.LoadIconNotExists);
            }

            // If this click takes us to the Activity Overview page
            if (Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription()))
            {
                AOP = new ActOverviewPage(Browser);
                AOP.WaitForInitialize();
                Thread.Sleep(300);
                return AOP;
            }

            // If this click takes us to the Sessions page
            if (Browser.Url.Contains(Constants.PageURLs.Activity_Sessions.GetDescription()))
            {
                ActSessionsPage ASP = new ActSessionsPage(Browser);
                ASP.WaitForInitialize();
                return ASP;
            }

            // If this click takes us to the Activity Bundle page
            if (Browser.Url.Contains(Constants.PageURLs.Activity_Bundle.GetDescription()))
            {
                ActBundlePage Page = new ActBundlePage(Browser);
                Page.WaitForInitialize();
                return Page;
            }

            // If this click takes us to the Activity Content page
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

            // Else if this click takes us to the Activity Registration page page
            else if (Browser.Url.Contains(Constants.PageURLs.Activity_Registration.GetDescription()))
            {
                ActRegistrationPage Page = new ActRegistrationPage(Browser);
                Page.WaitForInitialize();
                return Page;
            }

            // Else if this click takes us to the CPE Monitor page
            else if (Browser.Url.ToLower().Contains(Constants.PageURLs.Activity_CPEConsent.GetDescription().ToLower()))
            {
                ActCPEMonitorPage RP = new ActCPEMonitorPage(Browser);
                RP.WaitForInitialize();
                return RP;
            }

            // Else if this click takes us to the Payment (Legacy) page
            else if (Browser.Url.Contains(Constants.PageURLs.Activity_Payment_Legacy.GetDescription()))
            {
                ActPaymentPage APAYP = new ActPaymentPage(Browser);
                APAYP.WaitForInitialize();
                return APAYP;
            }

            // Else if this click takes us to the Order Details page
            else if (Browser.Url.Contains(Constants.PageURLs.Activity_Order_Details.GetDescription()))
            {
                ActOrderDetailsPage AODP = new ActOrderDetailsPage(Browser);
                AODP.WaitForInitialize();
                return AODP;
            }

            // Else if this click takes us to the Order Receipt page
            else if (Browser.Url.Contains(Constants.PageURLs.Activity_Order_Payment_Receipt.GetDescription()))
            {
                ActOrderReceiptPage ARP = new ActOrderReceiptPage(Browser);
                ARP.WaitForInitialize();
                return ARP;
            }

            // Else if this click takes us to the Cybersource payment page (UAMS)
            else if (Browser.Url.Contains(Constants.PageURLs.eCommerce_cybersource.GetDescription()))
            {
                ActeCommercePage AEP = new ActeCommercePage(Browser);
                AEP.WaitForInitialize();
                return AEP;
            }

            // Else if this click takes us to the User Registration page
            else if (Browser.Url.ToLower().Contains(Constants.PageURLs.Registration.GetDescription().ToLower()))
            {
                RegistrationPage RP = new RegistrationPage(Browser);
                RP.WaitForInitialize();
                return RP;
            }

            // Else if this click takes us to the Profile page and EULA popup
            else if (Browser.Url.ToLower().Contains(Constants.PageURLs.Profile.GetDescription().ToLower())
                // Temporary code for ONSLT. Soon ONS will properly redirect to fireball page. For now, it redirects to legacy 
                // community Account page. Remove when fireball comes  
                || Browser.Url.Contains(Constants.PageURLs.Account.GetDescription()))
            {
                ProfilePage PP = new ProfilePage(Browser);
                PP.WaitForInitialize();
                return PP;
            }

            // Else if this click takes us to the login page
            else if (Browser.Url.Contains(Constants.PageURLs.Login.GetDescription()))
            {
                LoginPage LP = new LoginPage(Browser);
                LP.WaitForInitialize();
                return LP;
            }

            // Else if this click takes us to the On Hold page
            else if (Browser.Url.Contains(Constants.PageURLs.Activity_OnHold.GetDescription()))
            {
                ActOnHoldPage Page = new ActOnHoldPage(Browser);
                Page.WaitForInitialize();
                return Page;
            }

            // Else if this click takes us to the On Curriculum page
            else if (Browser.Url.Contains(Constants.PageURLs.Curriculum.GetDescription()))
            {
                ActivitiesInProgressPage Page = new ActivitiesInProgressPage(Browser);
                Page.WaitForInitialize();
                return Page;
            }

            // Else if this click takes us to the PIM page
            else if (Browser.Url.Contains(Constants.PageURLs.Activity_PIM.GetDescription()))
            {
                ActPIMPage Page = new ActPIMPage(Browser);
                Page.WaitForInitialize();
                return Page;
            }

            throw new Exception("Code should not have reached here. Check above code");
        }

        #endregion Waiting

        #region Misc

        /// <summary>
        /// When testing PIM, we have to go through the Order Details page. If we are on Prod and we login through the 
        /// non-vanity backdoor, then get to the Order Details page and click Complete Order, the application redirects 
        /// to the vanity backdoor login URL. Per DEV, to fix this, it would require a significant change. So instead 
        /// of doing that, we are just logging in through the vanity URL with this method first. Then we will navigate 
        /// to the non-vanity URL so that the <see cref="GoTo_ActivityWorkflow_AnyInProgressPage"/> will work
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void PIM_Login(IWebDriver Browser, string username, string password)
        {
            Browser.Navigate().GoToUrl("https://learn.heart.org/login.aspx?action=enablelogin");

            LoginPage page = new LoginPage(Browser);
            page.Login(username, password);

            Navigation.GoToLoginPage(Browser, Constants.SiteCodes.AHA);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteCode"></param>
        /// <returns></returns>
        public string GetBaseURLWithSiteCode(Constants.SiteCodes siteCode)
        {
            string urlWithoutSiteCode = string.Format("{0}", AppSettings.Config["urlwithoutsitecode"].ToString());
            string urlWithSiteCode = urlWithoutSiteCode.Insert(8, siteCode.GetDescription()).ToLower();

            return urlWithSiteCode;
        }


        /// <summary>
        /// Navigates to the login page, clicks the Click Here To Register link, fills in all required fields on the Registration form, 
        /// clicks Continue and fills in the Password, clicks I Agree, and finally clicks Continue and waits for the Home page
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="siteCode"><see cref="Constants_LMS.SiteCodes"/></param>
        /// <param name="email">(Optional). Will choose an email for you if blank</param>
        /// <param name="username">(Optional). Will choose an (username) for you if blank</param>
        /// <param name="profession"><see cref="Constants_UAMS.Profession"/>(Optional). Will choose a profession for you if blank</param>
        public HomePage RegisterUser(IWebDriver Browser, Constants.SiteCodes siteCode, string email = null, string username = null,
            Constants.Profession? profession = null)
        {
            LoginPage LP = new LoginPage(Browser);

            // if logged in already
            if (Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
            {
                LP.LogOut(siteCode);
            }

            Navigation.GoToLoginPage(Browser, siteCode);
            RegistrationPage RP = LP.ClickAndWait(LP.ClickHereToRegLnk);
            HomePage HP = RP.RegisterUser(email, username, profession);

            return HP;
        }

        /// <summary>
        /// Clicks a user-specified element that opens a new tab, then switches the drivers focus to that tab
        /// </summary>
        /// <param name="linkText">The link text of the file you want to download</param>
        /// <param name="xpathOfElementToWaitFor">An element to wait for on the new window/tab</param>
        /// <returns></returns>
        public void OpenAndSwitchToContentTypeInNewWindowOrTab(IWebDriver Browser, string linkText,
            string xpathOfElementToWaitFor = null, string URLToWaitFor = null)
        {
            ClickOnLinkAndSwitchTo(Browser, linkText, xpathOfElementToWaitFor);

            if (xpathOfElementToWaitFor != null)
            {
                try
                {
                    Browser.WaitForElement(By.XPath(xpathOfElementToWaitFor), TimeSpan.FromSeconds(20), ElementCriteria.IsVisible);

                }
                catch (Exception)
                {
                    ClickOnLinkAndSwitchTo(Browser, linkText, xpathOfElementToWaitFor);
                }
            }

            if (URLToWaitFor != null)
            {
                Browser.WaitForURLToContainString(URLToWaitFor, TimeSpan.FromSeconds(60));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        /// <param name="xpathOfElementToWaitFor"></param>
        private void ClickOnLinkAndSwitchTo(IWebDriver Browser, string linkText, string xpathOfElementToWaitFor)
        {
            IWebElement Link;

            // Sometimes Fireball links are contained within Input tags, not Anchor tags. i.e. The Impelsys content
            // type links on the Material page in the Activity Workflow (SNMMI). See test "BodyInteractImpelsys"
            try
            {
                Link = Browser.FindElement(By.XPath(string.Format("//a[text()='{0}']", linkText)));
            }
            catch (Exception)
            {
                Link = Browser.FindElement(By.XPath(string.Format("//input[@value='{0}']", linkText)));
            }

            // Might need to Javascript click for IE in the future
            //Link.ClickJS(Browser);
            Link.Click(Browser);
            Thread.Sleep(5000);

            // IE opens in a new window and not maximized
            if (Regex.Replace(Browser.GetCapabilities().GetCapability("browserName").ToString(), @"\s+", "") == Regex.Replace(BrowserNames.InternetExplorer, @"\s+", ""))
            {
                Browser.SwitchTo().Window(Browser.WindowHandles.Last());
                Browser.Manage().Window.Maximize();
            }
            else
            {
                Browser.SwitchTo().Window(Browser.WindowHandles.Last());
            }
        }


        #endregion Misc


        #endregion methods: called from within this application

        #region methods: internal


        #endregion methods: internal

        #endregion methods

    }
}
