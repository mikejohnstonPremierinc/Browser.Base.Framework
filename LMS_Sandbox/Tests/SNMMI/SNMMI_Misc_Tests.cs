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

namespace SNMMI.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class SNMMI_Misc : TestBase_SNMMI
    {
        #region Constructors

        public SNMMI_Misc(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SNMMI_Misc(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am on the Preview page of an activity that is On Hold, Then all indicators should appear, and When I register, " +
            "Then I should be taken to the On Hold tab and all Indicators should appear and the Continue button should be disabled")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityOnHold(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Registration_11_OnHold.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Preview page for an Activity that is On Hold, and assert that the cooresponding On Hold message appears
            ActPreviewPage APP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, actTitle, false, user.Username);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnIcon, ElementCriteria.IsVisible),
                "The warning icon did not appear after navigating to the Activity Preview page for an activity that is On Hold");
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
                "The warning message did not appear after navigating to the Activity Preview page for an activity that is On Hold");
            StringAssert.Contains("There is a hold on this activity till", APP.NotificationWarnMessageLbl.Text);

            /// 2. Click Register, then assert that the user is taken to the On Hold page in the Activity Workflow page and the On Hold 
            /// message appears
            ActOnHoldPage AOHP = APP.ClickAndWait(APP.RegisterBtn);
            Assert.True(Browser.Exists(Bys.ActOnHoldPage.NotificationWarnIcon, ElementCriteria.IsVisible),
                "The warning icon did not appear after navigating to the Activity Preview page for an activity that is On Hold");
            Assert.True(Browser.Exists(Bys.ActOnHoldPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
                "The warning message did not appear after navigating to the Activity Preview page for an activity that is On Hold");
            StringAssert.Contains("There is a hold on this activity till", APP.NotificationWarnMessageLbl.Text);
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am on the Preview page of an activity that is On Hold but the hold has expired, Then the On Hold indicators " +
            "should not appear, and When I register, Then I should be taken to the Overview page, and the On Hold tab should not appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityOnHoldExpired(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Registration_11_1_OnHold.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Preview page for an Activity that is On Hold, and assert that the On Hold message does not appear
            ActPreviewPage APP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, actTitle, false, user.Username);
            Assert.False(Browser.Exists(Bys.ActOnHoldPage.NotificationWarnIcon, ElementCriteria.IsVisible),
                "The warning icon appeared after navigating to the Activity Preview page for an activity that is On Hold");
            Assert.False(Browser.Exists(Bys.ActOnHoldPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
                "The warning message appeared after navigating to the Activity Preview page for an activity that is On Hold");

            /// 2. Click Register, then assert that the user is taken to the Overview tab, and that the On Hold tab does not exists
            ActOverviewPage AOP = APP.ClickAndWait(APP.RegisterBtn);
            Assert.False(Browser.Exists(Bys.ActOnHoldPage.NotificationWarnIcon), "The On Hold page appeared");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Asserting that there are no issues upon login and logout")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void LoginLogout(Constants.SiteCodes siteCode)
        {
            UserModel user = profession1User1;

            // The API to create users on SNMMI CMEQA in this code doesnt work for some reason. Dont have time to look at it now
            // Will have to revisit later
            if (Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                user.Username = "SriluMember2";
                user.Password = "password";
            }

            /// 1. Assert that a user can login and logout with any issue
            LoginPage LP = Navigation.GoToLoginPage(Browser, siteCode);
            LP.Login(user.Username, user.Password);
            LP.LogOut(siteCode);
        }
   
        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am on the Home page or Search page, When I click an activity link, Then the Preview page " +
            "should load properly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PreviewPageViaLinkNotLoggedIn(Constants.SiteCodes siteCode)
        {
            /// 1. Click on an activity link from the Search page and verify that the Preview page loads properly
            HomePage HP = new HomePage(Browser);
            SearchPage SP = Navigation.GoToSearchPage(Browser, siteCode);
            SP.SearchResultsTblBodyActivityLnks[0].Click();
            ActPreviewPage APP = new ActPreviewPage(Browser);
            APP.WaitForInitialize();
        }

        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am logged in and on the Transcript page, When I add  a custom activity, " +
    "then it should appear in the grid")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void HoldingArea(Constants.SiteCodes siteCode)
        {
            UserModel user = professionMember1User1;
            string actTitle = DataUtils.GetRandomString(15);

            /// 1. Log in, go to Transcript page, click the +My Activity button, fill in all fields then verify
            /// a new record is loaded on the table
            TranscriptPage TP = Help.GoTo_Page(Browser, siteCode, Constants.Page.Transcript, user.Username);
            TP.AddMyActivity(actTitle, "04/11/1984", "ACR", "23423", "2.00",
                fullFilePath: "C:\\SeleniumAutoIt\\book1.xlsx");
            Thread.Sleep(50);
            
            // See the comment inside TP.AddMyActivity, cant get the upload to work and that is a required field
            string blah = string.Format("//td[contains(., '{0}')]", actTitle);
            Assert.True(browser.Exists(By.XPath(string.Format("//td[contains(., '{0}')]", actTitle))),
                "The custom activity was not added to the Transcript table");

            /// 2. Verify the Delete button shows. Click on the Print button and verify the Print form appears
            Assert.True(browser.Exists(Bys.TranscriptPage.DeleteBtn), "The delete button did not appear");
            TP.ClickAndWait(TP.PrintHoldingAreaReportBtn);
            Browser.SwitchTo().Frame(TP.PrintHoldingAreaReportIFrame);
            Assert.True(browser.Exists(Bys.TranscriptPage.PrintHoldingAreaReportFormRunReportBtn),
                "The Print Holding area Report window did not appear");
        }

        #endregion tests
    }
}






