using Browser.Core.Framework;
using NUnit.Framework;
using Mainpro.AppFramework;
using System;
using LS.AppFramework.Constants_LTS;
using OpenQA.Selenium;
using System.Threading;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_ResidentCycle_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ResidentCycle_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ResidentCycle_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Verifying the following for a Resident user: 5 year cycle Timeframe, " +
            "No Credit Requirements, No AMA/RCP Link, No Annual CreditSummaryTable " +
            "and No Leave Adjustment ")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void ResidentCycle_PrimaryValidations()
        {
            /// 1. Create a Default cycle user, then put the user into a Resident cycle using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.AS, currentDatetime.AddYears(-4));

            /// 2. Verify the user met all these condtions -  5 year cycle Timeframe , No Credit Requirements ,
            /// No AMA/RCP Link , No Annual CreditSummaryTable 
            Assert.False(DP.ClickHereToViewYourAmaRCPCreditsLnk.Displayed, "ClickHereToViewYourAmaRCPCreditsLink " +
                "should not displayed for resident cycle user");
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

            Assert.True(Help.GetCycleYearsTotal(Browser) == 5, "Resident user should be in 5 year cycle range");

            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Assert.False(Browser.Exists(Bys.CreditSummaryPage.AnnualRequirementsTbl, ElementCriteria.IsVisible),
                "Resident Cycle should not display  Annual Requirement Table in Credit Summary Page ");

            /// 3.  Verify the user can not apply for leave adjustment 
            LSHelp.Login(Browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);

            LS.AppFramework.ProgramPage PP = LSHelp.GoToParticipantProgramPage(Browser, "College of Family Physician",
             user.Username, "Certification of Proficiency");

            String leaveStartDate = currentDatetime.AddYears(-3).ToShortDateString();
            String leaveEndDate = currentDatetime.AddYears(-2).ToShortDateString();

            PP.ClickAndWait(PP.ProgramAdjustmentsTab);

            PP.ClickAndWait(PP.ProgAdjustTabAddAdjustLnk);
            PP.SelectAndWait(PP.ProgAdjustTabAddAdjustFormAdjustCodeSelElem, Constants_LTS.AdjustmentCodes.LLeave.GetDescription());

            Browser.WaitForElement(LS.AppFramework.Bys.ProgramPage.ProgAdjustTabAddAdjustFormLeaveStartDtTxt, ElementCriteria.IsVisible);
            PP.ProgAdjustTabAddAdjustFormLeaveStartDtTxt.SendKeys(leaveStartDate);
            PP.ProgAdjustTabAddAdjustFormLeaveEndDtTxt.SendKeys(leaveEndDate);
            Thread.Sleep(1000);
            PP.ProgAdjustTabAddAdjustFormAddAdjustBtn.ClickJS(browser);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Browser.WaitForElement(By.XPath("//div[@class='error warning']"),TimeSpan.FromSeconds(120),
                ElementCriteria.IsVisible);
           
            Assert.True(Browser.FindElement(By.XPath("//div[@class='error warning']")).Text.
                Contains("The adjustment (Adjustment Code: CFPC_LEAVE) cannot be applied to this recognition"),
                "Leave Adjustments should not be applied to Resident cycle user");

        }

        [Test]
        [Description("Given I adjust a user into a Resident cycle, When i add Royal College MOC Accredited, " +
            "American Medical Association Activity with above 50 credits,Then i should not get any Max limit" +
            " reached error popup and I should see the given credits are applied in Credit Tables ")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void ResidentCycle_AMARCPactivity_NoMaxLimit()
        {
            var actCategory1 = Const_Mainpro.ActivityCategory.SelfLearning;
            var actType1 = Const_Mainpro.ActivityType.SELFLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO;
            var actCertType = Const_Mainpro.ActivityCertType.Certified;
            var actCategory2 = Const_Mainpro.ActivityCategory.Assessment;
            var actType2 = Const_Mainpro.ActivityType.ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO;
            var actFormat = Const_Mainpro.ActivityFormat.Online;

            /// 1. Create a Default cycle user, then put the user into a Resident cycle using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.AS, currentDatetime.AddYears(-4));

            /// 2. Add an AMA activity with credits = 70.5 and RCP activity with credits = 70 activity  
            Help.AddActivity(browser, TestContext.CurrentContext.Test, actCategory1, actCertType, actType1,
              actFormat, username: user.Username, creditsRequested: 70.5);

            /// 3. Verify the submitted credits in CreditSummary page Table sections 
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabSelfLearn,
                   rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "70.5");

            Help.AddActivity(browser, TestContext.CurrentContext.Test, actCategory2, actCertType, actType2,
                actFormat, username: user.Username, creditsRequested: 70);
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                   rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "70");

            /// 4. Verify the submitted credits in CYCLE year Table 
            DP.ClickAndWaitBasePage(DP.DashboardTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: "Certified", colName: "Applied", cellTextExpected: "140.5");
        }


        #endregion Tests
    }


}