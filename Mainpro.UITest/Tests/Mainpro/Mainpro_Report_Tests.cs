using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Mainpro.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using LMS.Data;
using System.Collections.ObjectModel;
using LS.AppFramework.Constants_LTS;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_Report_Tests : TestBase
    {
        #region Constructors
        public Mainpro_Report_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_Report_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given a user submits an activity totalling 400 credit, When a Custom adjustment is entered on Lifetime " +
            "Support with a past date for the Cycle End Date and the user goes to the Reports page, Then all reports should " +
            "be available and downloaded in PDF without error")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ReportGeneration()
        {

            /// 1. Create a user and submit and activity with 400 credits so the user meets the current year's
            /// requirements
            /// 
           //  currentDatetime effectiveDate = currentcurrentDatetime.AddYears(-3);
            UserModel user = UserUtils.CreateAndRegisterUser(effectiveDt: currentDatetime.AddYears(-3),
                currentTest: TestContext.CurrentContext.Test);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Live,
                username: user.Username, isNewUser: true,
                creditsRequested: 400,
                actStartDt: currentDatetime.AddYears(-2),
                actCompletionDt: currentDatetime.AddYears(-2));

            /// 2. Through Lifetime Support, add a C-Custom adjustment and enter a past date for the Cycle End Date
            /// so that the cycle is ended/complete. This is so that we can access the My Mainpro Cycle Completion 
            /// Certificate report
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.GoToParticipantProgramPage(browser, "College of Family Physician", user.Username,
                "Certification of Proficiency");
            LSHelp.AddProgramAdjustment(browser, user.FullName, Constants_LTS.AdjustmentCodes.CUSTOM,
                adjustCycleSelection:
                Constants_LTS.AddAdjustFormCFPCCustomAdjustFirstSelElemItem.AdjustCycleEndDate,
                adjustCycleDate: currentDatetime.AddYears(-1));
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DashboardPage DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);

            /// 3. Click on the Reports Page, Click on the Run Report button for the My Credit Summary section, select
            /// a cycle, click Create Report, then Download Report, then verify the report generates
            ReportsPage RP = DP.ClickAndWaitBasePage(DP.ReportsTab);
            RP.ClickAndWait(RP.MyCreditSummaryRunReportBtn);
            RP.SelectAndWait(RP.MyCreditSummaryFormCycleSelElem, 
                RP.MyCreditSummaryFormCycleSelElem.Options[0].Text);
            RP.ClickAndWait(RP.MyCreditSummaryFormCreateReportBtn);
            RP.ClickAndWait(RP.MyCreditSummaryFormDownloadReportBtn);
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-786. Execute when fixed
            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 2, 1);
            RP.ClickAndWait(RP.MyCreditSummaryFormXBtn);

            /// 4. Repeat for My Transcript of CPD Activityies report
            RP.ClickAndWait(RP.MyTranscriptOfCPDActsRunReportBtn);
            RP.SelectAndWait(RP.MyTranscriptOfCPDActsFormCycleSelElem, 
                RP.MyTranscriptOfCPDActsFormCycleSelElem.Options[0].Text);
            RP.ClickAndWait(RP.MyTranscriptOfCPDActsFormCreateReportBtn);
            RP.ClickAndWait(RP.MyTranscriptOfCPDActsFormDownloadReportBtn);
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-786. Execute when fixed
            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 2, 1);
            RP.ClickAndWait(RP.MyTranscriptOfCPDActsFormXBtn);

            /// 5. Repeat for the My Mainpro Cycle Completion Certificate report
            DP =RP.ClickAndWaitBasePage(RP.DashboardTab);
            RP= DP.ClickAndWaitBasePage(DP.ReportsTab);
            RP.ClickAndWait(RP.MyMainproCycleCompleteionCertRunReportBtn);
            RP.SelectAndWait(RP.MyMainproCycleCompleteionCertFormCycleSelElem,
                RP.MyMainproCycleCompleteionCertFormCycleSelElem.Options[0].Text);
            RP.ClickAndWait(RP.MyMainproCycleCompleteionCertFormCreateReportBtn);
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-747. Uncomment and execute when fixed.
            // open defect https://code.premierinc.com/issues/browse/CFPC-3499. Uncomment and Execute when fixed
            RP.ClickAndWait(RP.MyMainproCycleCompleteionCertFormDownloadReportBtn);
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-786. Execute when fixed
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-917. Uncomment and Execute when fixed

            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 2, 1);
            RP.ClickAndWait(RP.MyMainproCycleCompleteionCertFormXBtn);
        }

        #endregion Tests
    }

}