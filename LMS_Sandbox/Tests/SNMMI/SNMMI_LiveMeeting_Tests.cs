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
    public class SNMMI_LiveMeeting_Tests : TestBase_SNMMI
    {
        #region Constructors

        public SNMMI_LiveMeeting_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SNMMI_LiveMeeting_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        // Defect: https://code.premierinc.com/issues/browse/LMSREW-1885 Uncomment and execute when fixed
        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I start and complete a Stand Alone Live Meeting activity, When I am on the Transcript page, " +
            "Then all relevant labels should populate with the correct data")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void StandAloneLiveInfoOnTranscript(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_16_0_Live_Meeting_in_San_Diego.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Complete a Stand Alone Live Meeting activity and assert that the awards show each credit type on the Transcript page
            TranscriptPage TP = Help.CompleteActivity(Browser, siteCode, actTitle, false, user.Username);
            List<Constants.Transcript> TranscriptForMyActivity = 
                TP.GetTranscript(Constants.SiteCodes.SNMMI).Where(t => t.ActivityTitle == actTitle).ToList();
            Assert.AreEqual(TranscriptForMyActivity.OrderBy(t => t.CreditBody).ToList()[0].CreditBody, TranscriptForMyActivity[0].CreditBody);

            /// 2. Assert the remaining labels populate with the correct data: Activity type. Credit amount. Date Completed.
            Assert.AreEqual("Live activity", TranscriptForMyActivity[0].ActivityType);
            Assert.AreEqual(currentDatetimeinEst.ToString("MM/dd/yyyy"), TranscriptForMyActivity[0].CompletionDate);
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I start a Stand Alone Live Meeting activity, When I am on the In Progress page, then all relevant labels should " +
            "populate with the correct data")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void StandAloneLiveInfoOnInProgPage(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_16_0_Live_Meeting_in_San_Diego.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Start a Stand Alone Live Meeting activity and assert that the awards show each credit type on the AIP page
            ActOverviewPage AOP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username);
            ActivitiesInProgressPage AIP = AOP.ClickAndWaitBasePage(AOP.ActivitiesInProgressTab);
            Constants.ActivityInProgress activitiesInProgress = AIP.GetActivityInProgressInfo(actTitle);
            List<Constants.Assessments> assessments = DBUtils.GetAssessmentsByActivityId(user.Username, siteCode, actTitle).
                OrderBy(a => a.AccreditationBodyTypeName).ToList();
            if (assessments[0].AccreditationBodyTypeName != "")
            {
                StringAssert.Contains(assessments[0].AccreditationBodyTypeName, activitiesInProgress.Credit[0].ToString());
            }

            /// 2. Assert the remaining labels populate with the correct data: Activity type. Address. Expiration Date
            // Open defect: https://code.premierinc.com/issues/browse/LMSREW-2370. Uncomment and execute when fixed
            //Assert.AreEqual("Live", activitiesInProgress.ActivityType);
            Assert.AreEqual("1234 Testing Ln, San Diego, CA 92829 US", activitiesInProgress.Address);
            Assert.AreEqual("12/31/9999", activitiesInProgress.ExpirationDate);
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I start a Stand Alone Live Meeting activity, When I am on the Overview page, then all relevant labels should " +
    "populate with the correct data")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void StandAloneLiveInfoOnOverviewPage(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_16_0_Live_Meeting_in_San_Diego.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Start a Stand Alone Live Meeting activity and assert that the relevant info displays correctly on the Overview page
            Constants.Activity ActivityDB = DBUtils.GetActivity_GeneralInfo(siteCode, actTitle, Constants.ActType.LiveMeeting);
            ActOverviewPage AOP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username);
            Constants.Activity ActivityUI = AOP.GetActivityDetails();
            Assert.AreEqual(ActivityDB.ActivityTitle, ActivityUI.ActivityTitle);
            // Fixed defect https://code.premierinc.com/issues/browse/LMSREW-1931
            Assert.AreEqual(ActivityDB.AddressAndLocation.Addr_Line_01, ActivityUI.AddressAndLocation.Addr_Line_01);
            StringAssert.Contains(ActivityUI.FrontMatter, ActivityDB.FrontMatter);
            Assert.AreEqual(ActivityDB.Accreditations.Count, ActivityUI.Accreditations.Count);
            for (int i = 0; i < ActivityDB.Accreditations.Count; i++)
            {
                Assert.AreEqual(ActivityDB.Accreditations[i].BodyName, ActivityUI.Accreditations[i].BodyName);
                Assert.AreEqual(ActivityDB.Accreditations[i].CreditAmount, ActivityUI.Accreditations[i].CreditAmount);
                Assert.AreEqual(ActivityDB.Accreditations[i].CreditUnit, ActivityUI.Accreditations[i].CreditUnit);
            }
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I start a Stand Alone Live Meeting activity, When I am on the Preview page, then all relevant labels should " +
"populate with the correct data")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void StandAloneLiveInfoOnPreviewPage(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_16_0_Live_Meeting_in_San_Diego.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Start a Stand Alone Live Meeting activity and assert that the relevant info displays correctly on the Preview page
            Constants.Activity ActivityDB = DBUtils.GetActivity_GeneralInfo(siteCode, actTitle, Constants.ActType.LiveMeeting);
            ActPreviewPage APP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, actTitle, false, user.Username);
            Constants.Activity ActivityUI = APP.GetActivityDetails();

            Assert.AreEqual(ActivityDB.ActivityTitle, ActivityUI.ActivityTitle);
            // Fixed defect https://code.premierinc.com/issues/browse/LMSREW-1931
            Assert.AreEqual(ActivityDB.AddressAndLocation.Addr_Line_01, ActivityUI.AddressAndLocation.Addr_Line_01);
            Assert.AreEqual(ActivityDB.Accreditations.Count, ActivityUI.Accreditations.Count);
            for (int i = 0; i < ActivityDB.Accreditations.Count; i++)
            {
                Assert.AreEqual(ActivityUI.Accreditations[i].CreditAmount, ActivityDB.Accreditations[i].CreditAmount);
                Assert.AreEqual(ActivityDB.Accreditations[i].CreditAmount, ActivityUI.Accreditations[i].CreditAmount);
                Assert.AreEqual(ActivityDB.Accreditations[i].CreditUnit, ActivityUI.Accreditations[i].CreditUnit);
            }
        }

        #endregion tests
    }
}






