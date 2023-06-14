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
using System.Diagnostics;
using Newtonsoft.Json;

namespace CMECA.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class CMECA_Misc : TestBase_CMECA
    {
        #region Constructors

        public CMECA_Misc(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CMECA_Misc(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
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

            /// 1. Assert that a user can login and logout with any issue
            LoginPage LP = Navigation.GoToLoginPage(Browser, siteCode);
            LP.Login(user.Username);
            LP.LogOut(siteCode);
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Asserting that header and footer display accurate information")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void HeaderFooter(Constants.SiteCodes siteCode)
        {
            UserModel user = profession1User1;

            /// 1. Login and verify all header and footer information is present and accurate
            LoginPage LP = Navigation.GoToLoginPage(Browser, siteCode);
            HomePage HP = LP.Login(user.Username);

            Assert.True(Browser.Exists(Bys.LMSPage.Footer_CopyrightLbl, ElementCriteria.IsVisible));
            Assert.True(Browser.Exists(Bys.LMSPage.Footer_SupportLbl, ElementCriteria.IsVisible));
            //Assert.True(Browser.Exists(Bys.LMSPage.Footer_LegalNoticesLbl, ElementCriteria.IsVisible));
            if (browser.MobileEnabled())
            {
                Assert.False(Browser.Exists(Bys.LMSPage.ClientLogo, ElementCriteria.IsVisible));
            }
            else
            {
                Assert.True(Browser.Exists(Bys.LMSPage.ClientLogo, ElementCriteria.IsVisible));
            }

            Assert.True(Browser.Exists(Bys.LMSPage.Footer_TermsOfUseLbl));
            Assert.True(Browser.Exists(Bys.LMSPage.Footer_PrivacyPolicyLbl));
            Assert.False(Browser.Exists(Bys.LMSPage.Footer_AboutThisSiteLbl));
        }


        #endregion tests
    }
}






