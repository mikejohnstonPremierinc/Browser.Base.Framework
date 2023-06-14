using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using LMS.AppFramework;
using LMS.AppFramework.LMSHelperMethods;
using LMS.AppFramework.Constants_;
//
//
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace UAMS.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_AssAndFeedback1 : TestBase_UAMS
    {
        #region Constructors

        public UAMS_AssAndFeedback1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_AssAndFeedback1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("blah"), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Testing various requirements of assessments which are configured to perform differently based on their CME360 " +
            "configurations, such as: " +
            "1. Number of accreditations and their required pass percentages. " +
            "2. Required vs not required assessments" +
            "3. Number of attempts allowed" +
            "4. Feedback" +
            "5. Showing correct answers")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void AssessmentConfigurationTests1(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Assessment_1_One_Page_No_Gender_Or_Profession.GetDescription();
            UserModel user = profession1User1;

            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            // Skipping this test in IE because elements are disappearing (top of page gets cut off) during automation in IE browser after 
            // typing text into the question text fields. Dont Looked into it for about hour and stopped
            // cause I dont have time ot look into it right now. IE may be phased out anyway for Edge soon. UPDATE: Fixed this. See inside
            // ActAssessmentPage.WaitForInitialize
            if (BrowserName != BrowserNames.InternetExplorer)
            {
                /// 1. Pass a 2-attempt Pretest assessment on the first attempt, then assert various configurations for this assessment: 
                /// A. Assessment displays feedback after this attempt. Feedback is configured to show after 'final' attempt, and 
                /// since we passed the first assessment, this should be considered the final attempt. 
                /// B. Displays feedback directly underneath last answer, as configured to do so.
                /// C. Shows correct answers, as configured to do so.
                ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Assessment, false,
                user.Username);
                AP.PassAssessment();
                Assert.Greater(Browser.FindElements(Bys.ActAssessmentPage.FeedbackLbls).Count, 0, "Feedback did not display on final passed " +
                    "attempt");
                Assert.True(Browser.Exists(By.XPath(@"//div/following-sibling::div//div[contains(text(), 'You Chose Wisely')] | //div/following-sibling::div//span/ancestor::label[1]/following-sibling::div/span[contains(text(), 'You Chose Wisely')]")), "The feedback did not display directly underneath the " +
                    "cooresponding answer");
                Assert.Greater(Browser.FindElements(By.XPath("//label[contains(@class, 'correct-answer')]")).Count, 0,
                    "Correct answers did not appear");

                /// 2. Continue to the next assessment (Post-Test), answer all questions incorrectly, then assert various different configurations 
                /// for this assessment:
                /// A. Assert that the Continue button is enabled, as the accreditations only require zero percent.
                /// B. Assert that the the feedback displays under the cooresponding answer, as it is configured to do so.
                AP.ClickAndWait(AP.ContinueBtn);
                AP.FailAttempt(alsoAnswerNonGradedNonRequiredQuestions: true);
                Assert.True(Browser.Exists(By.XPath(@"//div/following-sibling::div//div[contains(text(), 'You Chose Poorly')] | //div/following-sibling::div//span/ancestor::label[1]/following-sibling::div/span[contains(text(), 'You Chose Poorly')]")), "The feedback did not display after the last answer");

                /// 3. Continue to the next assessment (Evaluation), fail the 1st attempt, then assert various configurations for this assessment:
                /// A. Assert that the feedback shows, since it is configured to do so after every attempt
                /// B. Assert that the correct answers are not displayed, as it is configured to not show correct answers
                /// C. Assert that the Continue button is enabled, since this assessment is not Required.
                AP.ClickAndWait(AP.ContinueBtn);
                // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-717
                AP.FailAttempt();
                Assert.Greater(Browser.FindElements(Bys.ActAssessmentPage.FeedbackLbls).Count, 0, "Feedback did not display on final passed " +
                    "attempt");
                StringAssert.DoesNotContain("disabled", AP.ContinueBtn.GetAttribute("class"), "Continue button became disabled after a " +
                    "failed attempt on " +
                    "a non-required assessment");
                Assert.AreEqual(Browser.FindElements(By.XPath("//label[contains(@class, 'correct-answer')]")).Count, 0, "Correct answers " +
                    "did not appear");

                /// 4. Pass the second attempt (Evaluation), then assert various configurations for this assessment:
                /// A. Assert that the feedback shows, since it is configured to do so after every attempt
                AP.PassAssessment();
                Assert.Greater(Browser.FindElements(Bys.ActAssessmentPage.FeedbackLbls).Count, 0, "Feedback did not display on final passed " +
                    "attempt");

                /// 5. Continue to the next assessment (Follow-Up), pass it, then assert various configurations for this assessment:
                /// A. Assert that the feedback does not show, as it is configured to not show feedback for any attempt
                AP.ClickAndWait(AP.ContinueBtn);
                AP.FailAttempt();
                Assert.AreEqual(0, Browser.FindElements(Bys.ActAssessmentPage.FeedbackLbls).Count, "Feedback displayed");
            }
        }
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_AssAndFeedback2 : TestBase_UAMS
    {
        #region Constructors

        public UAMS_AssAndFeedback2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_AssAndFeedback2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Testing remaining requirements of assessments which are configured to perform differently based on their CME360 " +
          "configurations, such as: " +
          "1. Required questions" +
          "2. Number of accreditations and their percentages. " +
          "2. Required vs not required accreditations" +
          "3. Number of attempts allowed" +
          "4. Feedback" +
          "5. Showing correct answers")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void AssessmentConfigurationTests2(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Assessment_1_One_Page_No_Gender_Or_Profession.GetDescription();
            UserModel user = profession1User2;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            // Skipping this test in IE because elements are disappearing (top of page gets cur off) during automation in IE browser after 
            // typing text into the question text fields. Dont Looked into it for about hour and stopped
            // cause I dont have time ot look into it right now. IE may be phased out anyway for Edge soon. UPDATE: Fixed this. See inside
            // ActAssessmentPage.WaitForInitialize
            if (BrowserName != BrowserNames.InternetExplorer)
            {
                /// 1. Click the Submit button and assert that the required and unanswered questions throw an error message 
                /// "This field is required"
                ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Assessment, false,
                    user.Username);
                AP.SubmitBtn.Click(Browser);
                Assert.Greater(Browser.FindElements(Bys.ActAssessmentPage.ThisFieldIsRequiredLbls).Count, 0, "Required labels did not display after " +
                    "clicking the Submit without answering any questions");

                /// 2. For an assessment that has multiple accreditations with 2 attempts, with the accreditations requiring 100%, 
                /// fail the first attempt achieving only 80%, assert that the accreditations are labeled as passed/failed accordingly, 
                /// assert that the Retake button appears
                AP.CompleteAssessmentWithSpecificPercentage(80, 80, 80);
                Assert.True(Browser.Exists(Bys.ActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible), "Retake button did not become available " +
                    "after the first failed attempt for an assessment that has 2 attempts");
                List<string> AccredStatusExpected = new List<string>() { "failed", "failed" };
                CollectionAssert.AreEqual(AccredStatusExpected,
                    Browser.FindElements(Bys.ActAssessmentPage.StatusPassFailLbls).Select(t => t.Text.ToLower().ToList()));

                /// 2. The assessment being tested is configured to show feedback after the final attempt, so assert that it does not show it yet
                Assert.AreEqual(0, Browser.FindElements(Bys.ActAssessmentPage.FeedbackLbls).Count, "Feedback displayed on the non-final " +
                    "non-passed attempt");

                /// 3. Fail the second and final attempt, assert that the feedback now appears. 
                AP.FailAssessment();
                Assert.Greater(Browser.FindElements(Bys.ActAssessmentPage.FeedbackLbls).Count, 0, "Feedback did not display on final passed " +
                    "attempt");

                /// 4. Assert that the Retake button does not appear and that the Continue button does not become enabled since we 
                /// failed the final attempt and this assessment is required.
                Assert.False(Browser.Exists(Bys.ActAssessmentPage.RetakeBtn, ElementCriteria.IsVisible), "Retake button became available " +
                    "after the last and final failed attempt for an assessment");
                StringAssert.Contains("disabled", AP.ContinueBtn.GetAttribute("class"), "Continue button became enabled after a failed attempt");
            }
        }

    }
}






