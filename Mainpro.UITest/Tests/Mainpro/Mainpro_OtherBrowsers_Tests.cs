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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.Edge)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox, "", "", Platforms.Windows, "", "")]
    [RemoteSeleniumTestFixture(BrowserNames.Edge, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class Mainpro_OtherBrowsers_Tests : TestBase
    {
        #region Constructors
        public Mainpro_OtherBrowsers_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_OtherBrowsers_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Test that each page loads properly in other browsers, as well as activity submission and report generation")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void EdgeAndFirefoxSmokeTest()
        {
            /// 1. On Firefox and Edge, submit an activity from each category and verify it was submitted
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_Accreditationsurveyor_Max1, isNewUser: true, username: user.Username);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_AAFPandABFMActivities_L, isNewUser: true, username: user.Username);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_FormalClinicalTraineeshipFellowship_D50_Max50_VR_DR,
                isNewUser: true, username: user.Username);

            /// 2. Click on each page and verify the page loads
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
            DashboardPage DP = EADP.ClickAndWaitBasePage(EADP.DashboardTab);
            CreditSummaryPage CSP = DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            HoldingAreaPage HP = CSP.ClickAndWaitBasePage(CSP.HoldingAreaTab);
            ReportsPage RP = HP.ClickAndWaitBasePage(HP.ReportsTab);

            /// 3. Login to LTST, launch CFPC, verify it launches, then run a report and verify it loads in another tab 
            // Outstanding defect https://code.premierinc.com/issues/browse/MAINPROREW-939. In Edge and Firefox, reports
            // will throw an error if we are logged in through the backdoor, so we are launching from LTST here
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name + "_" + BrowserName, AppSettings.Config["ltspassword"]);
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            DP.ClickAndWaitBasePage(DP.ReportsTab);
            RP.ClickAndWait(RP.MyCreditSummaryRunReportBtn);
            RP.SelectAndWait(RP.MyCreditSummaryFormCycleSelElem, RP.MyCreditSummaryFormCycleSelElem.Options[0].Text);
            RP.ClickAndWait(RP.MyCreditSummaryFormCreateReportBtn);
            // Outstanding defect https://code.premierinc.com/issues/browse/MAINPROREW-939. Uncomment when fixed
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-786.
            RP.ClickAndWait(RP.MyCreditSummaryFormDownloadReportBtn);
            if (BrowserName == BrowserNames.Firefox)
            {
                browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElemFirefox, ElementCriteria.IsVisible);
            }
            else
            {
                browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            }
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 2, 1);
            RP.ClickAndWait(RP.MyCreditSummaryFormXBtn);
        }

        #endregion Tests
    }


}