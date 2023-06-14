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
using AventStack.ExtentReports;

namespace AHA.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_Misc : TestBase_AHA
    {
        #region Constructors

        public AHA_Misc(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_Misc(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
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

            TESTSTEP.Log(Status.Info, "Go to the Preview page for an Activity that is On Hold, and assert that the cooresponding On Hold message appears");
            ActPreviewPage APP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, actTitle, false, user.Username);
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnIcon, ElementCriteria.IsVisible),
                "The warning icon did not appear after navigating to the Activity Preview page for an activity that is On Hold");
            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
                "The warning message did not appear after navigating to the Activity Preview page for an activity that is On Hold");
            StringAssert.Contains("There is a hold on this activity till", APP.NotificationWarnMessageLbl.Text);

            TESTSTEP.Log(Status.Info, "Click Register, then assert that the user is taken to the On Hold page in the Activity Workflow page and the On " +
                "Hold message appears");
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

            TESTSTEP.Log(Status.Info, "Go to the Preview page for an Activity that is On Hold, and assert that the On Hold message does not appear");
            ActPreviewPage APP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, actTitle, false, user.Username);
            Assert.False(Browser.Exists(Bys.ActOnHoldPage.NotificationWarnIcon, ElementCriteria.IsVisible),
                "The warning icon appeared after navigating to the Activity Preview page for an activity that is On Hold");
            Assert.False(Browser.Exists(Bys.ActOnHoldPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
                "The warning message appeared after navigating to the Activity Preview page for an activity that is On Hold");

            TESTSTEP.Log(Status.Info, "Click Register, then assert that the user is taken to the Overview tab, and that the On Hold tab does not exists");
            ActOverviewPage AOP = APP.ClickAndWait(APP.RegisterBtn);
            Assert.False(Browser.Exists(Bys.ActOnHoldPage.NotificationWarnIcon), "The On Hold page appeared");
        }

        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Asserting that there are no issues upon login and logout")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void LoginLogout(Constants.SiteCodes siteCode)
        {
            UserModel user = profession1User1;
            Assert.Fail();
            // The API to create users on AHA CMEQA in this code doesnt work for some reason. Dont have time to look at it now
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

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"),Category("UAT"), Category("Prod")]
        [Description("Asserting that header and footer display accurate information")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void HeaderFooter(Constants.SiteCodes siteCode)
        {
            UserModel user = profession1User1;

            /// 1. Login and verify all header and footer information is present and accurate
            LoginPage LP = Navigation.GoToLoginPage(Browser, siteCode);
            SearchPage SP = LP.Login(user.Username);

            Assert.True(Browser.Exists(Bys.LMSPage.Footer_CopyrightLbl, ElementCriteria.IsVisible));
            Assert.True(Browser.Exists(Bys.LMSPage.Footer_SupportLbl, ElementCriteria.IsVisible));
            if (browser.MobileEnabled())
            {
                Assert.False(Browser.Exists(Bys.LMSPage.ClientLogo, ElementCriteria.IsVisible));
            }
            else
            {
                Assert.True(Browser.Exists(Bys.LMSPage.ClientLogo, ElementCriteria.IsVisible));
            }

            Assert.True(Browser.Exists(Bys.LMSPage.Footer_TermsOfUseLbl));
        }

        // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1488
        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Verifies that the All Receipts page loads the appropriate data")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ReceiptsPage(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Auromation_Registration_RegistrationPage_Payment_Discounted_AHA.GetDescription();
            UserModel user = professionMember1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);
            ActOverviewPage AOP = new ActOverviewPage(browser);

            /// 1. Log in, complete a transaction, go to the All Receipts page and verify the users transaction is displayed
            // On production, a different login page will need to be used because if we use the backdoor non-vanity URL,
            // after we click Complete Order, it throws an error
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Browser.Navigate().GoToUrl("https://learn.heart.org/login.aspx?action=enablelogin");              
                LoginPage page = new LoginPage(Browser);
                page.Login(user.Username, UserUtils.Password);
                Browser.Navigate().GoToUrl("https://learn.heart.org/lms/activity?@activity.id=6863295");
                // We need to wait for the page here, else the method below will navigate AGAIN to the page and
                // mess things up. I used a Sleep at first which worked, but this is better I think
                ActPreviewPage APP = new ActPreviewPage(Browser);
                APP.WaitForInitialize();
                Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, discountCode: "Full");
            }
            else
            {
                Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, discountCode: "Full");
            }

            AOP.ClickAndWaitBasePage(AOP.FullNameLbl);
            AllReceiptsPage ARP = AOP.ClickAndWaitBasePage(AOP.Menu_UserProfile_AllReceiptsLnk);

            Assert.True(Browser.Exists(By.XPath(string.Format("//div[text()='{0}']", actTitle))), 
                "The activity title was not displayed on the Transaction table");
            Assert.True(Browser.Exists(By.XPath("//div[text()='Transaction amount: $0.00']")), 
                "The transaction amount was not displayed on the Transaction table");

            IWebElement transactionAmtLbl = Browser.FindElement(By.XPath("//div[text()='Transaction amount: $0.00']"));
            IWebElement activityTitleLbl = 
                Browser.FindElement(By.XPath(string.Format("//div[text()='{0}']", actTitle)));

            Assert.AreEqual(actTitle, activityTitleLbl.Text);
            Assert.AreEqual("Transaction amount: $0.00", transactionAmtLbl.Text);
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am on the Transcript page, Then the date format should be MM/DD/YYYY")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void DateFormatVerification(Constants.SiteCodes siteCode)
        {
            UserModel user = profession1User1;

            /// 1. Go to the transcript page and verify the date format is correct
            TranscriptPage TP = Help.GoTo_Page(browser, siteCode, Constants.Page.Transcript, username: user.Username);
            // Fixed Defect. https://code.premierinc.com/issues/browse/LMSREW-1572
            Assert.AreEqual("MM/DD/YYYY", TP.FromDateLbl.Text);
            Assert.AreEqual("MM/DD/YYYY", TP.ToDateLbl.Text);           
        }


        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am on the Home page or Search page, When I click an activity link, Then the Preview page " +
            "should load properly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PreviewPageViaLinkNotLoggedIn(Constants.SiteCodes siteCode)
        {
            /// 1. Click on an activity link from the Search page and verify that the Preview page loads properly
            SearchPage SP = Navigation.GoToSearchPage(Browser, siteCode);
            SP.Search(Constants.ActivitySearchType.AllActivities);
            SP.SearchResultsTblBodyActivityLnks[0].Click();
            ActPreviewPage APP = new ActPreviewPage(Browser);
            APP.WaitForInitialize();
        }

        // Do not delete the sample test below. You can instead comment it out
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Demoing different components")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void API_DB_Overview(Constants.SiteCodes siteCode)
        {
            string username = string.Format("MyUser{0}@mailinator.com", DataUtils.GetRandomInteger(1000));

            UserModel user = UserUtils.CreateUser(siteCode, username);

            LoginPage LP = Navigation.GoToLoginPage(Browser, siteCode);
            LP.Login(user.Username);
            LP.LogOut(siteCode);

            string fullName = DBUtils.GetFullNameByUsername(username);
            int siteSeq = DBUtils.GetSiteSequence(siteCode);
            DBUtils.WhitelistIPAddressForSite("107.0.0.0", siteCode);

            Navigation.GoToLoginPage(Browser, siteCode);
            LP.Login(user.Username);
            string actTitle = Constants.ActTitle.Automation_Assessment_1_One_Page_No_Gender_Or_Profession.GetDescription();
            Help.CompleteActivity(Browser, siteCode, actTitle, isNewUser: true, username: user.Username);
        }

        #endregion tests
    }
}






