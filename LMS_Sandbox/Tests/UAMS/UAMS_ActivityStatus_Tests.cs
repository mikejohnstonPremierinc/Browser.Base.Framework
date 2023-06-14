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
    // Local
    [BrowserMode(BrowserMode.Reuse)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]


    [TestFixture]
    public class UAMS_ActivityStatus_Tests : TestBase_UAMS
    {
        #region Constructors

        public UAMS_ActivityStatus_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_ActivityStatus_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [BrowserMode(BrowserMode.Reuse)]
        [Description("Given I only reach the Activity Overview page, When I view the Overview page, Transcript page, and " +
            "Activities In Progress page, various buttons and links should appear/not appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityStatus_Overview_OneStep(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_12_ActivityOverviewWithConsentForm_OnePage.GetDescription();

            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Activity Overview page and verify that the Finish button does not exist
            ActOverviewPage AOP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, 
                false, user.Username);
            Assert.False(browser.Exists(Bys.ActOverviewPage.FinishBtn, ElementCriteria.IsVisible), 
                "The Finish button did not appear on the Overview page");
            
            ///// 2. Go to the Transcript page and verify the activity appears
            //AOP.ClickAndWaitBasePage(AOP.TranscriptTab);
            //Assert.True(browser.Exists(By.XPath(string.Format("//*[text()='{0}']", actTitle)), ElementCriteria.IsVisible),
            //    "The Activity link did not appear on the Transcript page");

            /// 3. Go to the Activities In Progress page and verify the activity does not appear
            AOP.ClickAndWaitBasePage(AOP.ActivitiesInProgressTab);
            Assert.False(browser.Exists(By.XPath(string.Format("//*[text()='{0}']", actTitle)), ElementCriteria.IsVisible),
                "The Activity link appeared on the Activities In Progress page");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [BrowserMode(BrowserMode.Reuse)]
        [Description("Given I only reach the Preassessment page, When I view the Transcript page, and " +
    "Activities In Progress page, various buttons and links should appear/not appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityStatus_PreAssessmentPageBreak_OneStep(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Assessment_12_PreAssessment_PageBreak.GetDescription();

            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Pre-Assessment page and verify that the Finish button exists and is disabled
            /// Also verify the Back button does not exists
            ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, 
                Constants.Pages_ActivityPage.Assessment, false, user.Username);
            Assert.True(browser.Exists(Bys.ActAssessmentPage.FinishBtn, 
                ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Finish button did not appear on the Overview page");
            Assert.False(browser.Exists(
                By.XPath("//body[contains(@class, 'activity_')]//span[text()='Back']/.. | //span[text()='Back']/.."), 
                ElementCriteria.IsVisible), "The Back button exists and is visible");

            /// 2. Go to the Activities In Progress page and verify the activity appears
            AP.ClickAndWaitBasePage(AP.ActivitiesInProgressTab);
            Assert.True(browser.Exists(By.XPath(string.Format("//*[text()='{0}']", actTitle)), ElementCriteria.IsVisible),
                "The Activity link appeared on the Activities In Progress page");

            /// 3. Go to the Transcript page and verify the activity does not appear
            AP.ClickAndWaitBasePage(AP.TranscriptTab);
            Assert.False(browser.Exists(By.XPath(string.Format("//*[text()='{0}']", actTitle)), ElementCriteria.IsVisible),
                "The Activity link did not appear on the Transcript page");

            /// 4. Complete the activity and...
            // Can not code this step right now because there are outstanding bugs that prevent from updating
            // this activity in CME360
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [BrowserMode(BrowserMode.Reuse)]
        [Description("Given I only reach the Post-Assessment page, When I view the Transcript page, and " +
            "Activities In Progress page, various buttons and links should appear/not appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityStatus_PostAssessment_OneStep(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Assessment_12_PostAssessment_OnePage.GetDescription();

            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Post-Assessment page and verify that the Finish button exists and is disabled
            /// Also verify the Back button does not exists
            ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle,
                Constants.Pages_ActivityPage.Assessment, false, user.Username);
            Assert.True(browser.Exists(Bys.ActAssessmentPage.FinishBtn,
                ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Finish button did not appear on the Overview page");
            //// Per Pavi, this is not a true requirement, so I am commenting it
            //Assert.False(browser.Exists(
            //    By.XPath("//body[contains(@class, 'activity_')]//span[text()='Back']/.. | //span[text()='Back']/.."),
            //    ElementCriteria.IsVisible), "The Back button exists and is visible");

            /// 2. Go to the Activities In Progress page and verify the activity appears
            AP.ClickAndWaitBasePage(AP.ActivitiesInProgressTab);
            Assert.True(browser.Exists(By.XPath(string.Format("//*[text()='{0}']", actTitle)), ElementCriteria.IsVisible),
                "The Activity link appeared on the Activities In Progress page");

            /// 3. Go to the Transcript page and verify the activity does not appear
            AP.ClickAndWaitBasePage(AP.TranscriptTab);
            Assert.False(browser.Exists(By.XPath(string.Format("//*[text()='{0}']", actTitle)), ElementCriteria.IsVisible),
                "The Activity link did not appear on the Transcript page");

            /// 4. Complete the activity and verify the Finish button is active
            Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Assessment, 
                false, user.Username);
            AP.PassAssessment();
            Assert.False(browser.Exists(Bys.ActAssessmentPage.FinishBtn,
                ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Finish button did not appear or was not enabled after completing the assessment");
        }

        // Open defect: https://code.premierinc.com/issues/browse/LMSREW-2345. Uncommented and execute when fixed
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [BrowserMode(BrowserMode.Reuse)]
        [Description("Given I only reach the Evaluation page, When I view the Transcript page, and " +
            "Activities In Progress page, various buttons and links should appear/not appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityStatus_Evaluation_OneStep(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Assessment_12_Evaluation__Multiple.GetDescription();

            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Evaluation page and verify that the Finish button exists and is disabled
            /// Also verify the Back button does not exists
            ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle,
                Constants.Pages_ActivityPage.Assessment, false, user.Username);
            Assert.True(browser.Exists(Bys.ActAssessmentPage.FinishBtn,
                ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Finish button did not appear on the Overview page");
            Assert.False(browser.Exists(
                By.XPath("//body[contains(@class, 'activity_')]//span[text()='Back']/.. | //span[text()='Back']/.."),
                ElementCriteria.IsVisible), "The Back button exists and is visible");

            /// 2. Go to the Activities In Progress page and verify the activity appears
            AP.ClickAndWaitBasePage(AP.ActivitiesInProgressTab);
            Assert.True(browser.Exists(By.XPath(string.Format("//*[text()='{0}']", actTitle)), ElementCriteria.IsVisible),
                "The Activity link appeared on the Activities In Progress page");

            /// 3. Go to the Transcript page and verify the activity does not appear
            AP.ClickAndWaitBasePage(AP.TranscriptTab);
            Assert.False(browser.Exists(By.XPath(string.Format("//*[text()='{0}']", actTitle)), ElementCriteria.IsVisible),
                "The Activity link did not appear on the Transcript page");

            /// 4. Complete the activity and verify the Finish button is active
            Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Assessment,
                false, user.Username);
            AP.PassAssessment("Srilu Min Ques");
            AP.PassAssessment("Srilu Min Ques 2");
            Assert.False(browser.Exists(Bys.ActAssessmentPage.FinishBtn,
                ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Finish button did not appear or was not enabled after completing the assessment");
        }

        #endregion tests
    }
}






