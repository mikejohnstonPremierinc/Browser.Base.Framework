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

namespace CAP.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    /// <summary>
    /// Running only 1 test in all browsers. We will have many preview page tests, that all hit the same page and perform the same 
    /// exact steps, the only difference being warning message labels. So we dont need to run all preview page tests in all browsers. 
    /// We will only run the RegistrationCutOff test in all browsers
    /// </summary>
    [TestFixture]
    public class CAP_Registration_AllBrowsers : TestBase_CAP
    {
        #region Constructors

        public CAP_Registration_AllBrowsers(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CAP_Registration_AllBrowsers(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I navigate to the Activity Preview page without logging in for an activity whose registration cut-off date " +
            "has passed, Then all cut-off indicators should not appear, then when I click the Launch button, it should redirect me to " +
            "the login page, then when I Login, all cut-off indicators should now appear and the Not Available button should be disabled")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Reg_CutOffDateInPast(Constants.SiteCodes siteCode)
        {
            string activityTitle = Constants.ActTitle.Automation_Registration_Cut_off_Date_In_The_Past.GetDescription();
            UserModel user = profession1User1;

            /// 1. Log in and go to the Activity Preview page for an activity that the notifications appear this time. 
            /// Also assert that the Not Available button appears, and that it is disabled
            ActPreviewPage AFP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityTitle, false,
                user.Username);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorIcon, ElementCriteria.IsVisible),
                "The warning icon did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorMessageLbl, ElementCriteria.IsVisible),
                "The error message did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.ActPreviewPage.NotAvailableBtn, ElementCriteria.IsVisible),
                "The Not Available button did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.AreEqual(AFP.NotificationErrorMessageLbl.Text, "Online Registration for this activity is closed");
            AFP.NotAvailableBtn.Click(Browser);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorMessageLbl, ElementCriteria.IsVisible),
                "The Not Available button was not disabled");
        }



        #endregion tests
    }

    /// <summary>
    /// Only running these remaining PreviewPage tests in Chrome. We will have many preview page tests, that all hit the same page and perform 
    /// the same exact steps, the only difference being warning message labels. So we dont need to run all preview page tests in all browsers. 
    /// We will only run the RegistrationCutOff test in all browsers
    /// </summary>
    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]

    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class CAP_Registration_ChromeSet_1 : TestBase_CAP
    {
        #region Constructors

        public CAP_Registration_ChromeSet_1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CAP_Registration_ChromeSet_1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am logged in and on the Activity Preview page for an activity whose registration is disabled, Then all indicators " +
            "should appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Reg_OnlineDisabled(Constants.SiteCodes siteCode)
        {
            string activityTitle = Constants.ActTitle.Automation_Registration_Online_Registration_Disabled.GetDescription();
            UserModel user = profession1User1;

            /// 1. Log in and go to the Activity Preview page for an activity that has been disabled
            ActPreviewPage AFP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityTitle, false, user.Username);

            /// 2. Assert that the warning notification message appears. Also assert that the Not Available button appears, and that it is disabled
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnIcon, ElementCriteria.IsVisible),
                "The icon did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
                "The error message did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.ActPreviewPage.NotAvailableBtn, ElementCriteria.IsVisible),
                "The Not Available button did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.AreEqual(AFP.NotificationWarnMessageLbl.Text, "This activity can not be registered online");
            AFP.NotAvailableBtn.Click(Browser);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
                "The Not Available button was not disabled");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("When I navigate to the Activity Preview page without logging in for an activity whose registration has " +
            "expired, then all disabled indicators should not appear, then when I log in, they should appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Reg_ExpiredActivity(Constants.SiteCodes siteCode)
        {
            string activityTitle = Constants.ActTitle.Automation_Registration_Expired.GetDescription();
            UserModel user = profession1User1;

            /// 1. Log in and go to the Activity Preview page for an activity that has expired
            ActPreviewPage AFP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityTitle, false, user.Username);

            /// 2. Assert that the warning notification message appears. Also assert that the Not Available button appears, and that it is disabled
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnIcon, ElementCriteria.IsVisible),
                "The icon did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
                "The error message did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.ActPreviewPage.NotAvailableBtn, ElementCriteria.IsVisible),
                "The Not Available button did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.AreEqual(AFP.NotificationWarnMessageLbl.Text, "This activity is no longer available");
            AFP.NotAvailableBtn.Click(Browser);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnIcon, ElementCriteria.IsVisible),
                "The Not Available button was not disabled");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am logged in as a physician and on the Activity Preview page for an activity that only pharmacists can register to, " +
      "Then the proper warning message and the Not Available button should appear, and When I log in as a pharmacist, then the Register " +
      "button should appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Reg_ProfessionDependent(Constants.SiteCodes siteCode)
        {
            string activityTitle = Constants.ActTitle.Automation_Registration_Profession_Should_Match_Physician.GetDescription();
            UserModel user1 = profession1User1;
            UserModel user2 = profession2User1;
            APIHelp.DeleteActivityForUser(user1.Username, activityTitle, siteCode);

            /// 1. Log in as a pharmacist and go to the Activity Preview page for an activity that only physicians can register to
            ActPreviewPage AFP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityTitle, false, user2.Username);

            /// 2. Assert that the warning notification message and the Not Available button appears, and that it is disabled
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorIcon, ElementCriteria.IsVisible),
                "The icon did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");

            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorMessageLbl, ElementCriteria.IsVisible),
                "The error message did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");

            Assert.True(Browser.Exists(Bys.ActPreviewPage.NotAvailableBtn, ElementCriteria.IsVisible),
                "The Not Available button did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");

            Assert.AreEqual(AFP.NotificationErrorMessageLbl.Text, "You are not eligible to register for this activity");

            AFP.NotAvailableBtn.Click(Browser);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorIcon, ElementCriteria.IsVisible),
                "The Not Available button was not disabled");

            /// 3. Log in as a physician and go to the same page, then assert that the Register button is available
            AFP.LogOut(siteCode);
            Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityTitle, false, user1.Username);
            Assert.True(Browser.Exists(Bys.ActPreviewPage.RegisterBtn, ElementCriteria.IsVisible), "The Register button did not appear");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Asserting the user can successfully register to an activity through the Registration page with skip logic")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Reg_RegPage(Constants.SiteCodes siteCode)
        {
            string activityTitle = Constants.ActTitle.Automation_Registration_8_Registration_Form_with_Skip_Logic_and_Formatting.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, activityTitle, siteCode);

            /// 1. Log in, go to the activity, click Register, fill in the registration fields including the skip 
            /// logic fields, click Submit and assert that the Overview page appears
            ActPreviewPage APP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityTitle, false, user.Username);
            ActRegistrationPage ARP = APP.ClickAndWait(APP.LaunchOrRegisterOrResumeBtn);

            Browser.FindElement(By.XPath("//span[text()='True']")).Click();
            Browser.FindElement(By.XPath("(//span[text()='Yes'])[2]")).Click();
            Browser.WaitForElement(By.XPath("//input[contains(@aria-label,'Tell me why you think')]"),
                ElementCriteria.IsVisible).SendKeys("Just because");
            ARP.ClickAndWait(ARP.RegisterBtn);
        }

        #endregion tests
    }

    /// <summary>
    /// Only running these remaining PreviewPage tests in Chrome. We will have many preview page tests, that all hit the same page and perform 
    /// the same exact steps, the only difference being warning message labels. So we dont need to run all preview page tests in all browsers. 
    /// We will only run the RegistrationCutOff test in all browsers
    /// </summary>
    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]

    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class CAP_Registration_ChromeSet_2 : TestBase_CAP
    {
        #region Constructors

        public CAP_Registration_ChromeSet_2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CAP_Registration_ChromeSet_2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests     

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am logged in and on the Activity Preview page for an activity whose registration has prerequisites, " +
            "Then all disabled indicators should appear, Then when I click on the prerequisites link, Then it should redirect me " +
            "to those prerquisite pages, and When I complete a prerequisite, it should let me register to the activity")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Reg_Prerequisites(Constants.SiteCodes siteCode)
        {
            string activityWithPreReqs = Constants.ActTitle.Automation_Registration_Prerequisites_Need_Completion.GetDescription();
            string activityPreReq1 = Constants.ActTitle.Activity_Special_Chars.GetDescription();
            string activityPreReq2 = Constants.ActTitle.Activity_Special_Chars_2.GetDescription();
            UserModel user = profession2User2;
            APIHelp.DeleteActivityForUser(user.Username, activityWithPreReqs, siteCode);
            APIHelp.DeleteActivityForUser(user.Username, activityPreReq1, siteCode);
            APIHelp.DeleteActivityForUser(user.Username, activityPreReq2, siteCode);

            /// 1. Log in and go to the Activity Preview page for an activity has prerequisites
            ActPreviewPage AFP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityWithPreReqs, false, user.Username);

            /// 2. Assert that the warning notification message and the prerequisite links appear. Also assert that the Not 
            /// Available button appears, and that it is disabled
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorIcon, ElementCriteria.IsVisible),
                "The icon did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorMessageLbl, ElementCriteria.IsVisible),
                "The error message did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.ActPreviewPage.NotAvailableBtn, ElementCriteria.IsVisible),
                "The Not Available button did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            StringAssert.Contains(activityPreReq1, AFP.NotificationErrorMessageLbl.Text);
            StringAssert.Contains(activityPreReq2, AFP.NotificationErrorMessageLbl.Text);
            AFP.NotAvailableBtn.Click(Browser);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorIcon, ElementCriteria.IsVisible),
                "The Not Available button was not disabled");

            /// 3. Click on the special characters prerequisites and assert that it takes you to that prerequisite's page
            IWebElement FirstPreReqLnks = Browser.FindElement(By.XPath("//span[@data-evt='prereqClick']"));
            FirstPreReqLnks.Click();
            // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1497
            AFP.WaitForInitialize();
            Assert.AreEqual(activityPreReq1, AFP.ActivityTitleLbl.Text);

            /// 4. Complete this activity, then navigate back to the prerequisite activity and assert that it is now available to register
            Help.CompleteActivity(Browser, siteCode, activityPreReq1);
            Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityWithPreReqs);
            Assert.True(Browser.Exists(Bys.ActPreviewPage.RegisterBtn, ElementCriteria.IsVisible), "The Register button did not appear");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am logged in and on the Activity Preview page for an activity whose registration has reached the maximum amount " +
            "of users, Then all disabled indicators should appear")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Reg_MaxUsersLimit(Constants.SiteCodes siteCode)
        {
            string activityTitle = Constants.ActTitle.Automation_Registration_Maximum_Users_Allowed_1.GetDescription();
            UserModel user = profession1User1;

            /// 1. Log in and go to the Activity Preview page for an activity has reached its maximum amount of registrations
            ActPreviewPage AFP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, activityTitle, false, user.Username);

            /// 2. Assert that the warning notification message appears. Also assert that the Not Available button appears, and that it is disabled
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorIcon, ElementCriteria.IsVisible),
                "The icon did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorMessageLbl, ElementCriteria.IsVisible),
                "The error message did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.True(Browser.Exists(Bys.ActPreviewPage.NotAvailableBtn, ElementCriteria.IsVisible),
                "The Not Available button did not appear after navigating to the Activity Preview page by entering the AID in the URL while logged in");
            Assert.AreEqual(AFP.NotificationErrorMessageLbl.Text, "Maximum number of registrations reached, registration is closed");
            AFP.NotAvailableBtn.Click(Browser);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorIcon, ElementCriteria.IsVisible),
                "The Not Available button was not disabled");
        } 
    }




    #endregion tests
}








