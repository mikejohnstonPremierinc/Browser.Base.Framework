using Browser.Core.Framework;
using NUnit.Framework;
using Mainpro.AppFramework;
using System;
using LS.AppFramework.Constants_LTS;
using OpenQA.Selenium;
using System.Globalization;
using LMS.Data;


namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_CycleCompletionAndRollover_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CycleCompletionAndRollover_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_CycleCompletionAndRollover_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        // Code to be performed after every test in this class file / test fixture
        [TearDown]
        public override void AfterTest()
        {
            // In each test of this class file, we call a method (Help.ForceCycleAdvancementPriorToAug15). This 
            // method updates a date within a DB table. We need to ensure this date gets set back to it's default after 
            // the below tests have executed. The below method sets it back. We only need to do this on QA because
            // we only update the flag in QA. IMPORTANT: Do NOT call this method inside of another class that is executing 
            // in parallel with these tests, as the tests will interfere with eachother when changing this DB value
            if (Help.EnvironmentEquals(Constants.Environments.CMEQA)) 
            { 
                DBUtils_Mainpro.SetGracePeriodEndDateToDefaultDate();
            }            
            base.AfterTest();
        }

        #region Tests
        [Test]
        [Description("Given I adjust a user into a voluntary cycle and submit an activity with the required amount " +
            "of certified credits, When I complete the user's Current Voluntary cycle " +
            "Then the user should be rolled over to next new Voluntary Cycle with no Carryover Credits, " +
            "no My Mainpro Cycle Completion Certificate report and the Voluntary cycle basic conditions should be met")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void VoluntaryCycleCompletion()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT)&& Help.IsCurrentDatePriortoAugust15()||
                    Help.EnvironmentEquals(Constants.Environments.Production) && Help.IsCurrentDatePriortoAugust15())
            {
                Assert.Ignore("This cycle completion test will be ignored if the environment is UAT/Production " +
                    " and the current system date is less than August 15th of the year. We dont want to adjust " +
                    "the GracePeriodEndDate on UAT and Prod since this may alter clients data/users");
            }

            /// 1. Create a Default cycle user, then put the user into a Voluntary cycle using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.P, currentDatetime.AddYears(-4));

            /// 2. Submit an activity with 150 certified credits
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
               Const_Mainpro.ActivityCategory.Assessment,
               Const_Mainpro.ActivityCertType.Certified,
               Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
               Const_Mainpro.ActivityFormat.Live,
               username: user.Username, isNewUser: false,
               creditsRequested: 150,
               actStartDt: currentDatetime.AddYears(-2),
               actCompletionDt: currentDatetime.AddYears(-2));

            /// 3. Update the GracePeriodEndDate field in the DB to current date - 1. This field controls whether the system 
            /// triggers the cycle rollover or not. By default, it is set to August 15, so users will be rolled over on or 
            /// after August 15. But for testing purposes, if we execute this test before Aug 15th, we set the date to 
            /// current date - 1 so that the test user will rollover properly. NOTE: We are only updating this on QA, which
            /// means this test goes untested in UAT and Prod prior to August 15th
            Help.ForceCycleAdvancementPriorToAug15();

            /// 4. Through Lifetime Support, add a C-Custom adjustment and enter a past date for the Cycle End Date
            /// so that the cycle is ended/complete. 
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.GoToParticipantProgramPage(browser, "College of Family Physician", user.Username,
                "Certification of Proficiency");
            DateTime completiondate = currentDatetime.AddYears(-1);
            LSHelp.AddProgramAdjustment(browser, 
                user.FullName,                
                Constants_LTS.AdjustmentCodes.CUSTOM,
                adjustCycleSelection:
                Constants_LTS.AddAdjustFormCFPCCustomAdjustFirstSelElemItem.AdjustCycleEndDate,
                adjustCycleDate: completiondate);
            LSHelp.ReevaluateUser(Browser, "College of Family Physician", user.Username, "Certification of Proficiency");

            /// 5. On the Details tab of the Program page of LTS, assert that the new cycle is showing as Voluntary and 
            /// is 5 years 
            By ProgramLblBy = By.XPath("//td[contains(text(),'Program:')]/following-sibling::td[1]");
            LSHelp.VerifyLabelTextEquals(Browser, ProgramLblBy, "Voluntary");
            Assert.AreEqual(completiondate.AddDays(1).ToString("M/d/yyyy"), LSHelp.GetProgramDetail(browser, "Starts"),
                "Start Date is not correclty dispalyed in LTST-ProgramDetail");
            String newcycleenddate = String.Format("6/30/{0}", completiondate.AddYears(5).Year);
            Assert.AreEqual(newcycleenddate, LSHelp.GetProgramDetail(browser, "Ends"),
                "End Date is not correclty dispalyed in LTST-ProgramDetail");

            /// 6. Launch the user and verify the Current Cycle label shows a 5-year range starting with last year, and 
            /// the Certified and Total rows on the Credit Summary widget show blank under the Required and Requirement 
            /// Met columns
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            int cycleStartYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(6, 4));
            int cycleEndYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(19, 4));
            Assert.AreEqual(cycleStartYr, completiondate.Year,"DashboardPage - Cycle year Start Date is incorrect");
            Assert.AreEqual(cycleEndYr, completiondate.AddYears(5).Year, "DashboardPage - Cycle year End Date is incorrect");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                  rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
                  colName: Const_Mainpro.TableColumnNames.RequirementMet.GetDescription(), cellTextExpected: "");

            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.RequirementMet.GetDescription(), cellTextExpected: "");

            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "");

            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "");

            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                   rowIndex: "2",
                  colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "");

            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                   rowIndex: "2",
                  colName: Const_Mainpro.TableColumnNames.RequirementMet.GetDescription(), cellTextExpected: "");
           

            /// 7. Verify the Credit Summary page should not display any Carryover credits and the Annual Requirements 
            /// table should not appear
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabOther,
                    rowName: "Carryover Certified Credit", colName: "Credits Applied", cellTextExpected: "-");
            Assert.False(Browser.Exists(Bys.CreditSummaryPage.AnnualRequirementsTbl, ElementCriteria.IsVisible),
                "Voluntary Cycle should not display Annual Requirement Table in Credit Summary Page ");

            /// 8. Verify the Cycle select element on the My Mainpro Cycle Completion Certificate report does not 
            /// contain any cycles
            ReportsPage RP = DP.ClickAndWaitBasePage(DP.ReportsTab);
            RP.MyMainproCycleCompleteionCertRunReportBtn.Click();
            Browser.WaitForElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCycleSelElem,TimeSpan.FromSeconds(20));
            Assert.False( Browser.Exists(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCycleSelElem,
                           ElementCriteria.SelectElementHasAtLeast1Item),
                           "My Mainpro Completion Cert Form Cycle select element contains items");          
        }

        [Test]
        [Description("Given I adjust a user into a Resident cycle and and submit an activity with the required amount " +
            "of certified credits, When I complete the user's Current Resident cycle " +
            "Then the user should be moved to No cycle and When I adjust the user into Default Cycle" +
            "Then the user should be credited with 40 Carryover Credits, no My Mainpro Cycle Completion Certificate " +
            "report and the Default cycle basic conditions should be met")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void ResidentCycleCompletion()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) && Help.IsCurrentDatePriortoAugust15() ||
                    Help.EnvironmentEquals(Constants.Environments.Production) && Help.IsCurrentDatePriortoAugust15())
            {
                Assert.Ignore("This cycle completion test will be ignored if the environment is UAT/Production " +
                    " and the current system date is less than August 15th of the year. We dont want to adjust " +
                    "the GracePeriodEndDate on UAT and Prod since this may alter clients data/users");
            }

            /// 1. Create a Default cycle user, then put the user into a Resident cycle using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.AS, currentDatetime.AddYears(-4));

            /// 2. Submit an activity with 150 certified credits 
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
               Const_Mainpro.ActivityCategory.Assessment,
               Const_Mainpro.ActivityCertType.Certified,
               Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
               Const_Mainpro.ActivityFormat.Live,
               username: user.Username, isNewUser: false,
               creditsRequested: 30,
               actStartDt: currentDatetime.AddYears(-3),
               actCompletionDt: currentDatetime.AddYears(-3));

            /// 3. Update the GracePeriodEndDate field in the DB to current date - 1. This field controls whether the system 
            /// triggers the cycle rollover or not. By default, it is set to August 15, so users will be rolled over on or 
            /// after August 15. But for testing purposes, if we execute this test before Aug 15th, we set the date to 
            /// current date - 1 so that the test user will rollover properly. NOTE: We are only updating this on QA, which
            /// means this test goes untested in UAT and Prod prior to August 15th
            Help.ForceCycleAdvancementPriorToAug15();
           
            /// 4. Through Lifetime Support, add a C-Custom adjustment and enter a past date for the Cycle End Date
            /// so that the cycle is ended/complete.
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.GoToParticipantProgramPage(browser, "College of Family Physician", user.Username,
                "Certification of Proficiency");
            DateTime completiondate = currentDatetime.AddYears(-2);
            LSHelp.AddProgramAdjustment(browser,
                user.FullName,
                Constants_LTS.AdjustmentCodes.CUSTOM,
                adjustCycleSelection:
                Constants_LTS.AddAdjustFormCFPCCustomAdjustFirstSelElemItem.AdjustCycleEndDate,
                adjustCycleDate: completiondate);
            LSHelp.ReevaluateUser(Browser, "College of Family Physician", user.Username, "Certification of Proficiency");

            /// 5. Through Lifetime Support, add an A-Active(Default) adjustment 
            DateTime newcyclestartdate = currentDatetime.AddYears(-1);
            LSHelp.AddProgramAdjustment(Browser, user.FullName, Constants_LTS.AdjustmentCodes.AActiveDefault, 
                effectiveDate:newcyclestartdate.ToString("yyyy-MM-dd"));

            /// 6. On the Details tab of the Program page of LTS, assert that the new cycle is showing as Default and 
            /// is 5 years 
            
            By ProgramLblBy = By.XPath("//td[contains(text(),'Program:')]/following-sibling::td[1]");
            LSHelp.VerifyLabelTextEquals(Browser, ProgramLblBy, "Default");
            Assert.AreEqual(newcyclestartdate.ToString("M/d/yyyy"), LSHelp.GetProgramDetail(browser, "Starts"),
                "Start Date is not correclty dispalyed in LTST-ProgramDetail");
            String newcycleenddate = String.Format("6/30/{0}", newcyclestartdate.AddYears(5).Year);
            Assert.AreEqual(newcycleenddate, LSHelp.GetProgramDetail(browser, "Ends"), 
                "End Date is not correclty dispalyed in LTST-ProgramDetail");

            /// 7. Launch the user and verify the Current Cycle label shows a 5-year range starting with last year, the 
            /// AMARCP link appears, the Certified Required label is 125, and the Total Applied label is 40 because this user 
            /// should get Carryover credits, 
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            int cycleStartYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(6, 4));
            int cycleEndYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(19, 4));
            Assert.AreEqual(cycleStartYr, newcyclestartdate.Year, "DashboardPage- Cycleyear range is incorrect" );
            Assert.AreEqual(cycleEndYr, newcyclestartdate.AddYears(5).Year, 
                "DashboardPage - Cycleyear range is incorrect");
            Assert.True(DP.ClickHereToViewYourAmaRCPCreditsLnk.Displayed,
                "ClickHereToViewYourAmaRCPCreditsLink should display for " +
                "Default cycle user");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "125");

            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Applied.GetDescription(), cellTextExpected: "30");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                   rowIndex: "2",
                   colName: Const_Mainpro.TableColumnNames.Applied.GetDescription(), cellTextExpected: "0");

            /// 8. Verify the Credit Summary page should display 40 Carryover credits
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabOther,
                    rowName: Const_Mainpro.TableRowNames.CarryoverCertifiedCredit.GetDescription(),
                    colName: "Credits Applied", cellTextExpected: "30");

            /// 9. Verify the Cycle select element on the My Mainpro Cycle Completion Certificate report does not 
            /// contain any cycles
            ReportsPage RP = DP.ClickAndWaitBasePage(DP.ReportsTab);
            RP.MyMainproCycleCompleteionCertRunReportBtn.Click();
            Browser.WaitForElement(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCycleSelElem, TimeSpan.FromSeconds(20));
            Assert.False(Browser.Exists(Bys.ReportsPage.MyMainproCycleCompleteionCertFormCycleSelElem,
                           ElementCriteria.SelectElementHasAtLeast1Item), 
                           "My Mainpro Completion Cert Form Cycle select element contains items");
        }

        [Test]
        [Description("Given I have user in Default cycle and Submitted an activity with 140 certified credits and" +
           "160 Non-certified credits, When I complete the user's Current Default cycle, Then The user should be" +
            " rolled over to next new Default cycle with 15 Carryover Credits, the My Mainpro Cycle Completion" +
            "Cert Report shoukd generate for prior cycle and New Default Cycle basic conditions should be met")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void DefaultCycleCompletion()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) && Help.IsCurrentDatePriortoAugust15() ||
                    Help.EnvironmentEquals(Constants.Environments.Production) && Help.IsCurrentDatePriortoAugust15())
            {
                Assert.Ignore("This cycle completion test will be ignored if the environment is UAT/Production " +
                    " and the current system date is less than August 15th of the year. We dont want to adjust " +
                    "the GracePeriodEndDate on UAT and Prod since this may alter clients data/users");
            }
            
            /// 1.  Create a Default cycle user
            UserModel user = UserUtils.CreateAndRegisterUser(effectiveDt: currentDatetime.AddYears(-3),
                currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LP.Login(user.Username, isNewUser: true);
         
            /// 2. Submit an activity with certified = 140 credits, Non-Certified = 160 credits
            DashboardPage DP = new DashboardPage(browser);
            DP.RefreshPage(true);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
               Const_Mainpro.ActivityCategory.Assessment,
               Const_Mainpro.ActivityCertType.Certified,
               Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
               Const_Mainpro.ActivityFormat.Live,
               username: user.Username, isNewUser: false,
               creditsRequested: 140,
               actStartDt: currentDatetime.AddYears(-3),
               actCompletionDt: currentDatetime.AddYears(-3));
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
               Const_Mainpro.ActivityCategory.GroupLearning,
               Const_Mainpro.ActivityCertType.NonCertified,
               Const_Mainpro.ActivityType.GRPLRNING_NONCERT_AmericanCollegeofEmergencyPhysiciansACEP_LO,
               Const_Mainpro.ActivityFormat.Live,
               username: user.Username, isNewUser: false,
               creditsRequested: 160,
               actStartDt: currentDatetime.AddYears(-3),
               actCompletionDt: currentDatetime.AddYears(-3));

            /// 3. Update the GracePeriodEndDate field in the DB to current date - 1. This field controls whether the system 
            /// triggers the cycle rollover or not. By default, it is set to August 15, so users will be rolled over on or 
            /// after August 15. But for testing purposes, if we execute this test before Aug 15th, we set the date to 
            /// current date - 1 so that the test user will rollover properly. NOTE: We are only updating this on QA, which
            /// means this test goes untested in UAT and Prod prior to August 15th
            Help.ForceCycleAdvancementPriorToAug15();

            /// 4. Through Lifetime Support, add a C-Custom adjustment and enter a past date for the Cycle End Date
            /// so that the cycle is ended/complete.
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.GoToParticipantProgramPage(browser, "College of Family Physician", user.Username,
                "Certification of Proficiency");
            DateTime completiondate = currentDatetime.AddYears(-2);
            LSHelp.AddProgramAdjustment(browser,
                user.FullName,
                Constants_LTS.AdjustmentCodes.CUSTOM,
                adjustCycleSelection:
                Constants_LTS.AddAdjustFormCFPCCustomAdjustFirstSelElemItem.AdjustCycleEndDate,
                adjustCycleDate: completiondate);
            LSHelp.ReevaluateUser(Browser, "College of Family Physician", user.Username, "Certification of Proficiency");

            /// 5. On the Details tab of the Program page of LTS, assert that the new cycle is showing as 
            /// Default and is 5 years
            By ProgramLblBy = By.XPath("//td[contains(text(),'Program:')]/following-sibling::td[1]");
            LSHelp.VerifyLabelTextEquals(Browser, ProgramLblBy, "Default");            
            String newcyclestartdate = completiondate.AddDays(1).ToString("M/d/yyyy");
            Assert.AreEqual(newcyclestartdate, LSHelp.GetProgramDetail(browser, "Starts"),
                "Start Date is not correct in LTST-ProgramDetail");
            String newcycleenddate = String.Format("6/30/{0}", completiondate.AddYears(5).Year);
            Assert.AreEqual(newcycleenddate, LSHelp.GetProgramDetail(browser, "Ends"),
                "End Date is not correct in LTST-ProgramDetail");

            /// 6. Launch the user and verify the Current Cycle label shows a 5-year range, 
            /// the AMARCP link appears, the Certified Required label is 125, the Cycle table Total 
            /// Applied label is 15 (from the carry over credits) but Current Year table Total Applied label is 0            
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            int cycleStartYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(6, 4));
            int cycleEndYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(19, 4));
            Assert.AreEqual(cycleStartYr, completiondate.Year, 
                "DashboardPage - Cycle year Start Date is incorrect");
            Assert.AreEqual(cycleEndYr, completiondate.AddYears(5).Year,
                "DashboardPage - Cycle year End Date is incorrect");
           
            Assert.True(DP.ClickHereToViewYourAmaRCPCreditsLnk.Displayed,
                "ClickHereToViewYourAmaRCPCreditsLink should display for Default cycle user");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "125");

            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Applied.GetDescription(), cellTextExpected: "15");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                   rowIndex: "2",
                   colName: Const_Mainpro.TableColumnNames.Applied.GetDescription(), cellTextExpected: "0");

            /// 7. Verify the Credit Summary page Other table displays 15 Carryover Credits
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabOther,
                    rowName: Const_Mainpro.TableRowNames.CarryoverCertifiedCredit.GetDescription(),
                    colName: "Credits Applied", cellTextExpected: "15");

            /// 8. Click on the Reports Page, click on the Run Report button for the 
            /// My Mainpro+ Cycle Completion Certificate report, select a cycle, click Create Report, 
            /// then click Download Report and verify the report generates
            /// 
            // open defect https://code.premierinc.com/issues/browse/CFPC-3499. Uncomment and Execute when fixed
            //ReportsPage RP = DP.ClickAndWaitBasePage(DP.ReportsTab);
            //RP.ClickAndWait(RP.MyMainproCycleCompleteionCertRunReportBtn);
            //RP.SelectAndWait(RP.MyMainproCycleCompleteionCertFormCycleSelElem,
            //    RP.MyMainproCycleCompleteionCertFormCycleSelElem.Options[0].Text);
            //RP.ClickAndWait(RP.MyMainproCycleCompleteionCertFormCreateReportBtn);
            //RP.ClickAndWait(RP.MyMainproCycleCompleteionCertFormDownloadReportBtn);
            //browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            //WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 2, 1); 
            //RP.ClickAndWait(RP.MyMainproCycleCompleteionCertFormXBtn);
        }

        #endregion Tests
    }

   




}