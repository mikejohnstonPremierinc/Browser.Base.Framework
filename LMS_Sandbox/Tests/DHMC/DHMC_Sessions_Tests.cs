//using Browser.Core.Framework;
//using LMS.Data;
//using NUnit.Framework;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using LMS.AppFramework;
//using LMS.AppFramework.Constants_;
//using LMS.AppFramework.LMSHelperMethods;
////
//using System.Configuration;
//using System.Globalization;

//namespace DHMC.UITest
//{
//    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
//    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
//    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

//    /// <summary>
//    /// Running only 1 test in all browsers. We will have many preview page tests, that all hit the same page and perform the same 
//    /// exact steps, the only difference being warning message labels. So we dont need to run all preview page tests in all browsers. 
//    /// We will only run the RegistrationCutOff test in all browsers
//    /// </summary>
//    [TestFixture]
//    public class DHMC_Sessions_Tests_Chrome : TestBase_DHMC
//    {
//        #region Constructors

//        public DHMC_Sessions_Tests_Chrome(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
//        public DHMC_Sessions_Tests_Chrome(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
//                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
//        { }

//        #endregion Constructors

//        #region Tests

//        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
//        [Description("Given I add a session, When I click Select on the second session which overlaps the first, Then it should " +
//            "be added and the corresponding warnings should appear")]
//        [Property("Status", "Complete")]
//        [Author("Mike Johnston")]
//        public void Sessions_OverlapAllowed(Constants.SiteCodes siteCode)
//        {
//            string actTitle = Constants.ActTitle.Automation_Session_16_5_Overlap_Allowed_UnlimitedCredits.GetDescription();
//            string session1 = Constants.Sessions.January_Session_1.GetDescription();
//            string session2 = Constants.Sessions.January_Session_2.GetDescription();
//            UserModel user = profession1User1;
//            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

//            /// 1. Go to the Sessions page, click on the Select button for any session, assert message - Successfully added! 
//            /// Add more or press "Register Sessions" to continue. Also assert that the session got added to the selected list
//            ActSessionsPage ASP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Sessions, false,
//                user.Username);
//            ASP.SelectSession(session1);

//            /// 2. Add a second session which overlaps the first, assert message - Successfully added! Add more or press 
//            /// "Register Sessions" to continue. Also assert that the session got added to the selected list
//            ASP.SelectSession(session2);
//        }

//        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
//        [Description("Given I enter a blank, invalid, and valid Access Code into the Access Code form for a Session, When I click " +
//      "Continue, Then all cooresponding warnings should appear and the session should get added after a valid code")]
//        [Property("Status", "Complete")]
//        [Author("Mike Johnston")]
//        public void Sessions_AccessCode(Constants.SiteCodes siteCode)
//        {
//            string actTitle = Constants.ActTitle.Automation_Session_16_3__AccessCode_UnlimitedCredits.GetDescription();
//            string session1 = Constants.Sessions.January_Session_1_AccessCode_Srilu007_AUTO123.GetDescription();
//            string session2 = Constants.Sessions.January_Session_2.GetDescription();
//            UserModel user = profession1User1;
//            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

//            /// 1. Go to the Sessions page, click on the Select button for any session with an Access Code, leave the Access Code
//            /// field blank, click Continue then assert the error message - This field is required.
//            ActSessionsPage ASP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Sessions, false,
//                user.Username);
//            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, ASP.AvailableSessionsTbl, Bys.ActSessionsPage.AvailableSessionsTblBody,
//                session1, "a", "Select", "span");
//            Browser.WaitForElement(Bys.ActSessionsPage.AccessCodeFormAccessCodeTxt, ElementCriteria.IsVisible);
//            ASP.AccessCodeFormContinueBtn.Click(Browser);
//            Assert.True(Browser.Exists(Bys.ActSessionsPage.AccessCodeFormRequiredLbl, ElementCriteria.IsVisible),
//                "The error message did not appear");
//            // On Production, the message is wrong, but it is fixed in QA, so skipping this line in production until next SP
//            if (Help.EnvironmentReadyAfterDate(DateTime.ParseExact("06/24/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture),
//                Constants.Environments.Production))
//            {
//                Assert.AreEqual("This field is required", ASP.AccessCodeFormRequiredLbl.Text);
//            }
//            else
//            {
//            }

//            /// 2. Enter an invalid code, click Continue, assert the error message - Invalid access code
//            ASP.AccessCodeFormAccessCodeTxt.SendKeys("wrongcode");
//            ASP.AccessCodeFormContinueBtn.Click(Browser);
//            // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1496 
//            // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1416
//            Browser.WaitForElement(Bys.LMSPage.NotificationErrorMessageLbl);
//            browser.WaitJSAndJQuery();
//            Browser.WaitForElement(Bys.LMSPage.NotificationErrorMessageLbl);
//            Assert.True(Browser.Exists(Bys.LMSPage.NotificationErrorMessageLbl, ElementCriteria.IsVisible),
//                 "The error message did not appear");
//            Assert.AreEqual("Invalid access code", ASP.NotificationErrorMessageLbl.Text);

//            /// 3. Enter a valid code, click Continue, assert message - Successfully added! Add more or press 
//            /// "Register Sessions" to continue. Also assert that the session got added to the selected list
//            ASP.ClickAndWaitBasePage(ASP.NotificationErrorMessageLblXBtn);
//            ASP.SelectSession(session1);
//        }

//        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
//        [Description("Given I am on the Sessions page and I added a session to the selected list, When I click Select to add another " +
//            "session with overlapping dates and the activity does not allow for it, Then the session should not be added and the " +
//            "corresponding warnings should appear")]
//        [Property("Status", "Complete")]
//        [Author("Mike Johnston")]
//        public void Sessions_OverlapNotAllowed(Constants.SiteCodes siteCode)
//        {
//            string actTitle = Constants.ActTitle.Automation_Session_16_4_Overlap_Not_Allowed_UnlimitedCredits.GetDescription();
//            string session1 = Constants.Sessions.January_Session_1.GetDescription();
//            string session2 = Constants.Sessions.January_Session_2.GetDescription();
//            UserModel user = profession1User1;
//            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

//            /// 1. Go to the Sessions page and add the first session
//            ActSessionsPage ASP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Sessions, false,
//                user.Username);
//            ASP.SelectSession(session1);

//            /// 2. Click Select on the second session, assert message - Unable to add session! The timing of
//            /// this session overlaps with one already selected
//            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, ASP.AvailableSessionsTbl, Bys.ActSessionsPage.AvailableSessionsTblBody,
//                session2, "a", "Select", "span");
//            Browser.WaitForElement(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.HasText);
//            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
//                "The error message did not appear");
//            Assert.AreEqual("Unable to add session! The timing of this session overlaps with one already selected",
//                ASP.NotificationWarnMessageLbl.Text);
//        }

//        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
//        [Description("Given I am on the Sessions page and I added a session to the selected list, When I click Select to add another " +
//            "session with and the activity does not allow for multiple sessions, Then the session should not be added and the " +
//            "corresponding warnings should appear")]
//        [Property("Status", "Complete")]
//        [Author("Mike Johnston")]
//        public void Sessions_SingleSession(Constants.SiteCodes siteCode)
//        {
//            string actTitle = Constants.ActTitle.Automation_Session_16_2_SingleSessionSelection_With_Activity_Content_UnlimitedCredits.GetDescription();
//            string session1 = Constants.Sessions.January_Session_1_Downloadable_content.GetDescription();
//            string session2 = Constants.Sessions.January_Session_2_URLs_should_open_in_new_tabs.GetDescription();
//            UserModel user = profession1User1;
//            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

//            /// 1. Go to the Sessions page and add the first session
//            ActSessionsPage ASP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Sessions, false,
//                user.Username);
//            ASP.SelectSession(session1);

//            /// 2. Click Select on the second session, assert message - Only one session may be selected.
//            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, ASP.AvailableSessionsTbl, Bys.ActSessionsPage.AvailableSessionsTblBody,
//                session2, "a", "Select", "span");
//            Browser.WaitForElement(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.HasText);
//            Assert.True(Browser.Exists(Bys.LMSPage.NotificationWarnMessageLbl, ElementCriteria.IsVisible),
//                "The error message did not appear");
//            // On Production, the message is wrong, but it is fixed in QA, so skipping this line in production until next SP
//            if (Help.EnvironmentReadyAfterDate(DateTime.ParseExact("06/24/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture),
//                Constants.Environments.Production))
//            {
//                Assert.AreEqual("Only one session may be selected.", ASP.NotificationWarnMessageLbl.Text);
//            }
//            else
//            {
//            }
//        }

//        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
//        [Description("Given I am on the Sessions page for an activity with disabled sessions and Content, When I click Select to " +
//            "add a session, Then the cooresponding warnings should appear, and when I view the session rows in the available " +
//            "table, Then content should appear and also open accordingly")]
//        [Property("Status", "Complete")]
//        [Author("Mike Johnston")]
//        public void Sessions_DisabledAndContent(Constants.SiteCodes siteCode)
//        {
//            string actTitle =
//            Constants.ActTitle.Automation_Session_16_1_DisabledSessionSelection_With_Activity_Content_UnlimitedCredits.GetDescription();
//            string session1 = Constants.Sessions.January_Session_1_Downloadable_content.GetDescription();
//            string session2 = Constants.Sessions.January_Session_2_URLs_should_open_in_new_tabs.GetDescription();
//            UserModel user = profession1User1;
//            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

//            /// 1. Go to the Sessions page, assert message - You can only review this meeting's sessions below. The 
//            /// sessions you attend will be selected for you after the meeting. Also assert the Continue button is disabled
//            /// and the Select buttons do not appear in the sessions rows
//            ActSessionsPage ASP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Sessions, false,
//                user.Username);
//            Assert.True(Browser.Exists(By.XPath("//*[contains(text(), 'You can only review this meeting')]")), "The warning did not appear");
//            Assert.True(browser.Exists(Bys.ActSessionsPage.ContinueBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
//                "The Continue button was not disabled");
//            Assert.False(ElemGet.Grid_ContainsRecord(Browser, ASP.AvailableSessionsTbl, Bys.ActSessionsPage.AvailableSessionsTblBody, 3,
//                "Select", "span"), "The session rows contained Select buttons");

//            /// 2. Assert that the content shows and that when I click on the URL content, it opens in a new tab 
//            Help.OpenAndSwitchToContentTypeInNewWindowOrTab(Browser, "Google", "//*[@name='q']");
//            Assert.True(Browser.Exists(By.XPath("//*[@name='q']")), "The new tab did not load the page");
//        }

//        #endregion tests
//    }

//}






