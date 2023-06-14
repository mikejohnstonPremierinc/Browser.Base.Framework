using Browser.Core.Framework;
using Mainpro.AppFramework;
using NUnit.Framework;
using System;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_AffiliateCycle_Tests : TestBase
    {
        #region Constructors
        public Mainpro_AffiliateCycle_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_AffiliateCycle_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Given I adjust a user into a Affiliate Default cycle, When i add Royal College" +
            " MOC Accredited, and American Medical Association Activity with above 50 credits," +
            " Then i should not get Max Credit limit reached error popup and I should see " +
            "the given credits are applied in Credit Tables ")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AffiliateDefaultCycle_AMARCPactivity_NoMaxLimit()
        {
            /// 1. Create a Default cycle user, then put the user into a Affiliate cycle type using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.AF, currentDatetime.AddYears(-4));

            /// 2. Verify the user met all these conditions -  5 year cycle Timeframe,
            /// No AMA/RCP Link , No Max Limit for AMA RCP Certified activity
            Assert.False(DP.ClickHereToViewYourAmaRCPCreditsLnk.Displayed, "ClickHereToViewYourAmaRCPCreditsLink " +
                "should not displayed for affiliate default cycle user");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "125");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "250");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                   rowIndex: "2",
                  colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "25");

            Assert.True(Help.GetCycleYearsTotal(Browser) == 5, "Affiliate user should be in 5 year cycle range");

            var actCategory1 = Const_Mainpro.ActivityCategory.SelfLearning;
            var actType1 = Const_Mainpro.ActivityType.SELFLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO;
            var actCertType = Const_Mainpro.ActivityCertType.Certified;
            var actCategory2 = Const_Mainpro.ActivityCategory.Assessment;
            var actType2 = Const_Mainpro.ActivityType.ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO;
            var actFormat = Const_Mainpro.ActivityFormat.Online;

            /// 3. Add an AMA activity with credits = 70.5 and RCP activity with credits = 70 and 
            /// Verify the submitted credits displayed in CreditSummary page Category Table sections 
            Help.AddActivity(browser, TestContext.CurrentContext.Test, actCategory1, actCertType, actType1,
           actFormat, username: user.Username, creditsRequested: 70.5);
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabSelfLearn,
                   rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "70.5");

            Help.AddActivity(browser, TestContext.CurrentContext.Test, actCategory2, actCertType, actType2,
          actFormat, username: user.Username, creditsRequested: 70);
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                   rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "70");

            /// 4. Verify the Total submitted credits = 140.5 applied in CYCLE year Table 
            DP.ClickAndWaitBasePage(DP.DashboardTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: "Certified", colName: "Applied", cellTextExpected: "140.5");
        }

        [Test]
        [Description("Given I adjust a user into a Affiliate Cycle and Adjust to Remedial cycle, " +
             "And i add Royal College MOC Accredited and American Medical Association" +
             " Activity with above 25 credits and clicks the Submit button, " +
            "When the credit is validated, Then both submitted credits should get reflected in " +
            "Credit Tables because Affiliate Remedial does not have Max Credit Rule")]
        [Property("Status", "Completed")] 
        [Author("Bama Thangaraj")]
        public void AffiliateRemedialCycle_AMARCPactivity_NoMaxLimit()
        {
            /// 1. Create a Default cycle user, then put the user into a Affiliate cycle type using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.AF, currentDatetime.AddYears(-4));

            ///2. Now, adjust the Affiliate user into a Remedial Cycle type using the Adjustment API
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.R);

            ///3. Verify the user met all these condtions -  2 year cycle Timeframe,
            /// No AMA/RCP Link , No Max Limit for AMA RCP Certified activity
            Assert.False(DP.ClickHereToViewYourAmaRCPCreditsLnk.Displayed, "ClickHereToViewYourAmaRCPCreditsLink " +
                "should not displayed for Affiliate Remedial cycle user");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "50");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Required.GetDescription(), cellTextExpected: "100");

            Assert.True(Help.GetCycleYearsTotal(Browser) == 2, "Affiliate Remedial user " +
                "should be in 2 year cycle range");

            var actCategory1 = Const_Mainpro.ActivityCategory.SelfLearning;
            var actType1 = Const_Mainpro.ActivityType.SELFLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO;
            var actCertType = Const_Mainpro.ActivityCertType.Certified;
            var actCategory2 = Const_Mainpro.ActivityCategory.Assessment;
            var actType2 = Const_Mainpro.ActivityType.ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO;
            var actFormat = Const_Mainpro.ActivityFormat.Online;

            ///4. Add an AMA activity with credits = 30.5 and RCP activity with credits = 30, 
            ///the user should not any Max error warning and Validate the credits through LTS 
            ///and verify the submitted Credits are reflected in 
            ///Credit Summary Page -Category Table Sections and Cycle Credit Tables applied column
            Activity Act1 = Help.AddActivity(browser, TestContext.CurrentContext.Test, actCategory1, actCertType, actType1,
            actFormat, username: user.Username, creditsRequested: 30.5);
            Help.ValidateCreditReevaluateUserThenRelaunchMainpro(Browser, user.Username, Act1.Title, 
                TestContext.CurrentContext.Test.Name);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
                   colName: Const_Mainpro.TableColumnNames.Applied.GetDescription(), cellTextExpected: "30.5");
            DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryTabSelfLearn,
                   rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "30.5");

            Activity Act2 = Help.AddActivity(browser, TestContext.CurrentContext.Test, actCategory2, actCertType, actType2,
               actFormat, username: user.Username, creditsRequested: 30);
            Help.ValidateCreditReevaluateUserThenRelaunchMainpro(Browser, user.Username, Act2.Title, 
                TestContext.CurrentContext.Test.Name);

            ///5. Verify Total submitted credits = 60.5 applied in CYCLE year Table 
            DP.ClickAndWaitBasePage(DP.DashboardTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: "Certified", colName: "Applied", cellTextExpected: "60.5");
        }


        #endregion Tests
    }


}