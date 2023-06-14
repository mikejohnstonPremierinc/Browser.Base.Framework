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

namespace CAP.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class CAP_MCF : TestBase_CAP
    {
        #region Constructors

        public CAP_MCF(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CAP_MCF(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Verifying all MCF rules as they pertain to hiding and showing questions")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void MCF_InitialStateHidden(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.
                AutomationAssessment_1_1_InitialStage1Hide2Show3Disable4Enable5DefaultAnswer6DisableAnswer7EnableAnswer.
                GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Pre-Assessment and verify that:
            /// a. All questions that are configured in CME360 to not Hide shows. Currently, these 4 questions are:
            /// i. Text label, ii. Rating Scale (apple, banana, orange), iii. Yes/No.
            /// iv. Rating Scale Other (apple, banana, orange).  Note that these Rating Scale questions
            /// are essentially 3 questions per. So 8 questions should not be hidden
            /// b. All questions that are configured in CME360 to Hide do not show. Currently, there are 17 configured in
            /// CME360 to hise
            /// c. NOTE: Rating Scale Matrix is set to hide, but Fireball currently does not support hiding this 
            /// question type, so it will show up
            ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle,
                Constants.Pages_ActivityPage.Assessment, false, user.Username);
            Assert.AreEqual(8, AP.QuestionTextNotHiddenLbls.Count, string.Format("There should be 8 non-hidden questions. Found {0}",
                AP.QuestionTextNotHiddenLbls.Count));
            Assert.AreEqual(13, AP.QuestionTextHiddenLbls.Count, string.Format("There should be 13 hidden questions. Found {0}",
                AP.QuestionTextNotHiddenLbls.Count));

            // Verify that the code is actually visually hiding/not hiding the elements. The HTML for these elements has a 
            // 'form-input-skip' attribute value, so we will use that to locate these elements
            foreach (var question in AP.QuestionTextNotHiddenLbls)
            {
                Assert.True(question.Displayed, string.Format("The question '{0}' was supposed to be displayed, " +
                    "but was hidden", question.Text));
            }
            foreach (var question in AP.QuestionTextHiddenLbls)
            {
                Assert.False(question.Displayed, string.Format("The question '{0}' was supposed to be hidden, " +
                    "but was displayed", question.Text));
            }

            /// 2. For question *Yes / No* , select *YES* then verify all questions show except for "True or False with other" 
            Help.ChooseAnswer_RadioButton(Browser, "Yes / No. Gradable Question - Graded*", "Yes", false);
            Assert.AreEqual(20, AP.QuestionTextNotHiddenLbls.Count, string.Format("There should be 20 non-hidden questions. Found {0}",
                AP.QuestionTextNotHiddenLbls.Count));
            Assert.AreEqual(1, AP.QuestionTextHiddenLbls.Count, string.Format("There should be 1 hidden questions. Found {0}",
                AP.QuestionTextNotHiddenLbls.Count));
            Assert.AreEqual("True / False with Other. Gradable question - Not Graded",
                AP.QuestionTextHiddenLbls[0].GetAttribute("innerText"));

            /// 3. For question *True / False* , select *True* then verify the "True or False with other" question shows
            Help.ChooseAnswer_RadioButton(Browser, "True / False. Gradable Question - Graded*", "True", false);
            Assert.AreEqual(21, AP.QuestionTextNotHiddenLbls.Count, string.Format("There should be 21 non-hidden questions. Found {0}",
                AP.QuestionTextNotHiddenLbls.Count));
            Assert.AreEqual(0, AP.QuestionTextHiddenLbls.Count, string.Format("There should be 0 hidden questions. Found {0}",
                AP.QuestionTextNotHiddenLbls.Count));

            /// 4. Click *Save and Finish Later* then verify all showing questions stay open. They do not hide back
            AP.ClickAndWait(AP.SaveAndFinishLaterBtn);
            Assert.AreEqual(21, AP.QuestionTextNotHiddenLbls.Count, string.Format("There should be 21 non-hidden questions. Found {0}",
                AP.QuestionTextNotHiddenLbls.Count));
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Verifying all MCF rules as they pertain to hiding and showing questions")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void MCF_InitialStateShowEnable(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.
                AutomationAssessment_1_1_InitialStage1Hide2Show3Disable4Enable5DefaultAnswer6DisableAnswer7EnableAnswer.
                GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Post-Assessment and verify that all questions are enabled 
            ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle,
                Constants.Pages_ActivityPage.Assessment, false, user.Username);
            AP.ClickAndWait(AP.ContinueBtn);
            Assert.AreEqual(3, browser.FindElements(Bys.ActAssessmentPage.DisabledElems).Count, 
                "There should only be 3 disabled elements on this page, which are the text field elements that get " +
                "enabled after the user selects their cooresponding radio button. Found: " +
                browser.FindElements(Bys.ActAssessmentPage.DisabledElems).Count);
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Verifying all MCF rules as they pertain to default answers")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void MCF_DefaultAnswer(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.
                AutomationAssessment_1_1_InitialStage1Hide2Show3Disable4Enable5DefaultAnswer6DisableAnswer7EnableAnswer.
                GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Evaluation page
            ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle,
                Constants.Pages_ActivityPage.Assessment, false, user.Username);
            AP.ClickAndWait(AP.ContinueBtn);
            AP.ClickAndWait(AP.ContinueBtn);

            /// 2. Launch the assessment 'Srilu Eval - Default Answer', then verify all default answers for each type of 
            /// control are correct (defaulted to the answer per to their CME360 configurations)
            AP.ClickAndWait(AP.LaunchBtn);
            Assert.True(Help.IsAnswerSelected(Browser,
                "Multiple Choice Single Answer. Gradable Question - Graded*", "Correct", Constants.QuestionTypeName.RadioButton),
                "The radio button titled 'Multiple Choice Single Answer. Gradable Question - Graded*' did not have the correct " +
                "default answer of 'Correct'");

            Assert.True(Help.IsAnswerSelected(Browser,
                "Multiple Choice with Drop Down. Gradable - Graded*", "Correct", Constants.QuestionTypeName.DropDown),
                "The dropdown titled 'Multiple Choice with Drop Down. Gradable - Graded*' did not have the correct " +
                "default answer of 'Correct'");

            Assert.True(Help.IsAnswerSelected(Browser,
                    "Text with Single Line. Text Field - Not Gradable", "Srilu Wrote this answer so you can relax now ",
                    Constants.QuestionTypeName.TextOneAnswer),
                    "The textbox titled 'Text with Single Line. Text Field - Not Gradable' did not have the correct " +
                    "default answer of 'Srilu Wrote this answer so you can relax now '");

            Assert.True(Help.IsAnswerSelected(Browser,
                "Date Picker - Select Today's Date or any date. Not Gradable.", "2/14/2020", Constants.QuestionTypeName.DatePicker),
                "The date picker titled '' did not have the correct default answer of '2/14/2020'");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Verifying all MCF rules as they pertain to default state")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void MCF_DefaultState(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.
                AutomationAssessment_1_1_InitialStage1Hide2Show3Disable4Enable5DefaultAnswer6DisableAnswer7EnableAnswer.
                GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Post Assessment page
            ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle,
                Constants.Pages_ActivityPage.Assessment, false, user.Username);
            AP.ClickAndWait(AP.ContinueBtn);
            AP.ClickAndWait(AP.ContinueBtn);
            AP.ClickAndWait(AP.ContinueBtn);

            /// 2. Verify all questions types that should be disabled (per their CME360 configuration) are disabled
            Assert.AreEqual(30, browser.FindElements(Bys.ActAssessmentPage.DisabledElems).Count, 
                "There should be 30 disabled elements on this page per the CME360 configuration. Found: " +
                browser.FindElements(Bys.ActAssessmentPage.DisabledElems).Count);
        }

        #endregion tests
    }
}






