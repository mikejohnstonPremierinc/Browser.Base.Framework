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
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace SNMMI.UITest
{
    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]


    [TestFixture]
    public class SNMMI_MemTranscript1_Tests : TestBase_SNMMI
    {
        #region Constructors

        public SNMMI_MemTranscript1_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SNMMI_MemTranscript1_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I navigate to the Transcript page as a member/non-member, When I land on the page " +
            "at the same time with other member/non-member users, Then buttons should appear/not appear accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ConcurrencyTest_MemberPhysician1(Constants.SiteCodes siteCode)
        {
            UserModel user = professionMember1User1;

            /// 1. Go to the Transcript page and assert that the Member-related elements appear since we are
            /// logged in as a Member
            TranscriptPage TP = Help.GoTo_Page(Browser, siteCode, Constants.Page.Transcript, user.Username);
            Assert.True(Browser.Exists(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible), 
                "The My Activity Button did not appear for a Member user");
            Assert.True(Browser.Exists(Bys.TranscriptPage.HoldingAreaTab, ElementCriteria.IsVisible),
                "The Holding Area tab did not appear for a Member user");
            Assert.True(Browser.Exists(Bys.TranscriptPage.PrintReportBtn, ElementCriteria.IsVisible),
                "The Print button did not appear for a Member user");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void MemberNonMember_Transcript(Constants.SiteCodes siteCode)
        {
            UserModel user = professionMember1User1;

            /// 1. Login as a member, go to the Transcript page, click the + My Activity button, then assert that the
            /// Activity Detaills form loads
        }

        #endregion tests
    }

    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]


    [TestFixture]
    public class SNMMI_NonMemTranscript1_Tests : TestBase_SNMMI
    {
        #region Constructors

        public SNMMI_NonMemTranscript1_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SNMMI_NonMemTranscript1_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I navigate to the Transcript page as a member/non-member, When I land on the page " +
            "at the same time with other member/non-member users, Then buttons should appear/not appear accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ConcurrencyTest_NonMemberPhysician1(Constants.SiteCodes siteCode)
        {
            UserModel user = profession1User1;

            /// 1. Go to the Transcript page and assert that the Member-related elements do not appear since we 
            /// are logged in as a non-Member
            TranscriptPage TP = Help.GoTo_Page(Browser, siteCode, Constants.Page.Transcript, user.Username);
            Assert.False(Browser.Exists(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible),
                "The My Activity Button appeared for a Non-Member user");
            Assert.False(Browser.Exists(Bys.TranscriptPage.HoldingAreaTab, ElementCriteria.IsVisible),
                "The Holding Area tab did not appeared for a Non-Member user");
            Assert.False(Browser.Exists(Bys.TranscriptPage.PrintReportBtn, ElementCriteria.IsVisible),
                "The Print button did not appeared for a Non-Member user");

        }

        #endregion tests
    }

    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]


    [TestFixture]
    public class SNMMI_MemTranscript2_Tests : TestBase_SNMMI
    {
        #region Constructors

        public SNMMI_MemTranscript2_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SNMMI_MemTranscript2_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I navigate to the Transcript page as a member/non-member, When I land on the page " +
            "at the same time with other member/non-member users, Then buttons should appear/not appear accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ConcurrencyTest_MemberPhysician2(Constants.SiteCodes siteCode)
        {
            UserModel user = professionMember1User2;

            /// 1. Go to the Transcript page and assert that the Member-related elements appear since we are
            /// logged in as a Member
            TranscriptPage TP = Help.GoTo_Page(Browser, siteCode, Constants.Page.Transcript, user.Username);
            Assert.True(Browser.Exists(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible),
                "The My Activity Button did not appear for a Member user");
            Assert.True(Browser.Exists(Bys.TranscriptPage.HoldingAreaTab, ElementCriteria.IsVisible),
                "The Holding Area tab did not appear for a Member user");
            Assert.True(Browser.Exists(Bys.TranscriptPage.PrintReportBtn, ElementCriteria.IsVisible),
                "The Print button did not appear for a Member user");
        }

        #endregion tests
    }

    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]


    [TestFixture]
    public class SNMMI_NonMemTranscript2_Tests : TestBase_SNMMI
    {
        #region Constructors

        public SNMMI_NonMemTranscript2_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SNMMI_NonMemTranscript2_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I navigate to the Transcript page as a member/non-member, When I land on the page " +
            "at the same time with other member/non-member users, Then buttons should appear/not appear accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ConcurrencyTest_NonMemberPhysician2(Constants.SiteCodes siteCode)
        {
            UserModel user = profession1User2;

            /// 1. Go to the Transcript page and assert that the Member-related elements do not appear since we 
            /// are logged in as a non-Member
            TranscriptPage TP = Help.GoTo_Page(Browser, siteCode, Constants.Page.Transcript, user.Username);
            Assert.False(Browser.Exists(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible),
                "The My Activity Button appeared for a Non-Member user");
            Assert.False(Browser.Exists(Bys.TranscriptPage.HoldingAreaTab, ElementCriteria.IsVisible),
                "The Holding Area tab did not appeared for a Non-Member user");
            Assert.False(Browser.Exists(Bys.TranscriptPage.PrintReportBtn, ElementCriteria.IsVisible),
                "The Print button did not appeared for a Non-Member user");

        }

        #endregion tests
    }

    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]


    [TestFixture]
    public class SNMMI_MemTranscript3_Tests : TestBase_SNMMI
    {
        #region Constructors

        public SNMMI_MemTranscript3_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SNMMI_MemTranscript3_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I navigate to the Transcript page as a member/non-member, When I land on the page " +
            "at the same time with other member/non-member users, Then buttons should appear/not appear accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ConcurrencyTest_MemberPhysician3(Constants.SiteCodes siteCode)
        {
            UserModel user = professionMember1User3;

            /// 1. Go to the Transcript page and assert that the Member-related elements appear since we are
            /// logged in as a Member
            TranscriptPage TP = Help.GoTo_Page(Browser, siteCode, Constants.Page.Transcript, user.Username);
            Assert.True(Browser.Exists(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible),
                "The My Activity Button did not appear for a Member user");
            Assert.True(Browser.Exists(Bys.TranscriptPage.HoldingAreaTab, ElementCriteria.IsVisible),
                "The Holding Area tab did not appear for a Member user");
            Assert.True(Browser.Exists(Bys.TranscriptPage.PrintReportBtn, ElementCriteria.IsVisible),
                "The Print button did not appear for a Member user");
        }

        #endregion tests
    }

    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]


    [TestFixture]
    public class SNMMI_NonMemTranscript3_Tests : TestBase_SNMMI
    {
        #region Constructors

        public SNMMI_NonMemTranscript3_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SNMMI_NonMemTranscript3_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I navigate to the Transcript page as a member/non-member, When I land on the page " +
            "at the same time with other member/non-member users, Then buttons should appear/not appear accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ConcurrencyTest_NonMemberPhysician3(Constants.SiteCodes siteCode)
        {
            UserModel user = profession2User1;

            /// 1. Go to the Transcript page and assert that the Member-related elements do not appear since we 
            /// are logged in as a non-Member
            TranscriptPage TP = Help.GoTo_Page(Browser, siteCode, Constants.Page.Transcript, user.Username);
            Assert.False(Browser.Exists(Bys.TranscriptPage.MyActivityBtn, ElementCriteria.IsVisible),
                "The My Activity Button appeared for a Non-Member user");
            Assert.False(Browser.Exists(Bys.TranscriptPage.HoldingAreaTab, ElementCriteria.IsVisible),
                "The Holding Area tab did not appeared for a Non-Member user");
            Assert.False(Browser.Exists(Bys.TranscriptPage.PrintReportBtn, ElementCriteria.IsVisible),
                "The Print button did not appeared for a Non-Member user");

        }

        #endregion tests
    }
}






