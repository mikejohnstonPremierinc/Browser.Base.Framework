using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LMS.AppFramework;
using LMS.AppFramework.Constants_;
using LMS.AppFramework.LMSHelperMethods;
//
using System.Configuration;
using System.Globalization;

namespace UAMS.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class UAMS_OverviewPage_Tests : TestBase_UAMS
    {
        #region Constructors

        public UAMS_OverviewPage_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_OverviewPage_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I register for an activity with only an overpage and a consent form, When I view various " +
            "pages, Then the activity (and the Finish button) should appear/not appear on the page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityOverview_ConsentForm_OnePage(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_12_ActivityOverview_with_ConsentForm_OnePage.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Overview page and assert that the Finish button does not exist
            ActOverviewPage AOP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username);
            Assert.False(Browser.Exists(Bys.ActOverviewPage.FinishBtn, ElementCriteria.IsVisible), "The Finish button " +
                "appeared on the Overview page");

            /// 2. Go to the Transcript page and assert that the activity shows
            TranscriptPage TP = Help.GoTo_Page(browser, siteCode, Constants.Page.Transcript, user.Username);
            Assert.True(Browser.Exists(By.XPath(string.Format("//a[text()='{0}']", actTitle)), ElementCriteria.IsVisible)
                , "The activity did not appear on the Transcript page");

            /// 3. Go to the Activites In Progress page and assert that the activity does not show
            Help.GoTo_Page(browser, siteCode, Constants.Page.ActivitiesInProgress, user.Username);
            Assert.False(Browser.Exists(By.XPath(string.Format("//a[text()='{0}']", actTitle)), ElementCriteria.IsVisible)
                , "The activity appeared on the Activities In Progress page");

            /// 4. Go to the Overview page, click on the checkbox and assert that it goes to the Transcript page
            Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username);
            AOP.ActivityOverviewChk.Click();
            TP.WaitForInitialize();

            /// 5. Click on the activity title on the Transcript page, assert that it goes back to the workflow and
            /// the Finish button now appears and is enabled
            TP.ClickActivity(actTitle);
            Assert.True(Browser.Exists(Bys.ActOverviewPage.FinishBtn, ElementCriteria.IsVisible, 
                ElementCriteria.AttributeValue("aria-disabled", "false")));
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I register for an activity with only an overpage with no consent form, When I view the " +
            "Overview page, Then the Finish button should appear on the page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityOverview_NoConsentForm_OnePage(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_12_ActivityOverview_NoConsentForm_OnePage.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Overview page and assert that the Finish button exists
            ActOverviewPage AOP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username);
            Assert.True(Browser.Exists(Bys.ActOverviewPage.FinishBtn, ElementCriteria.IsVisible), "The Finish button " +
                "did not appear on the Overview page");

            /// 2. Click the Finish button and assert that the user lands on the Transcript page
            TranscriptPage TP = AOP.ClickAndWait(AOP.FinishBtn);
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I register for an activity with an overpage and a workflow, When I view and click the " +
            "elements on the Overview page, Then various elements should be enabled/disabled depending on what was " +
            "clicked")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityOverview_ConsentText(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_16_0_Live_Meeting_in_San_Diego.GetDescription();
            UserModel user = profession1User2;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Overview page and assert that the checkbox with Consent text exists and the Continue 
            /// button does not exist
            ActOverviewPage AOP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username);
            Assert.True(Browser.Exists(Bys.ActOverviewPage.ConfirmWithCheckBoxLbl, ElementCriteria.IsVisible), 
                "The checkbox with Consent text did not appear on the Overview page");
            Assert.False(Browser.Exists(Bys.ActOverviewPage.ContinueBtn, ElementCriteria.IsVisible), "The Continue button " +
                "appeared on the Overview page");

            /// 2. Click on the checkbox and assert that the application lands on the next page in the workflow
            ActAssessmentPage AP = AOP.ClickAndWait(AOP.ActivityOverviewChk);

            /// 3. Click on the Back button then assert that the Continue button appears and the check box is disabled
            AP.ClickAndWait(AP.BackBtn);
            Assert.True(Browser.Exists(Bys.ActOverviewPage.ContinueBtn, ElementCriteria.IsVisible), "The Continue button " +
                "appeared on the Overview page");
            Assert.True(Browser.Exists(Bys.ActOverviewPage.ActivityOverviewChk), "asfe");
            Assert.False(Browser.Exists(Bys.ActOverviewPage.ActivityOverviewChk, ElementCriteria.IsEnabled), 
                "The checkbox was not disabled");
        }

        #endregion tests
    }

}






