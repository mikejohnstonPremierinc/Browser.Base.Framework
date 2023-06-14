using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Mainpro.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Collections.ObjectModel;
using LMS.Data;
using LS.AppFramework.Constants_LTS;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_CycleGeneral2_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CycleGeneral2_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_CycleGeneral2_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given a user has been in 2 cycles and received credits for each, When the user clicks on the View All " +
            "Cycles button, Then a form should appear showing the cooresponding credits of each cycle, and When the user " +
            "clicks View Details for each cycle, Then the appropriate functionality should occur")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ViewAllCycles()
        {
            /// 1. Create a user and submit an activity with 1 certified credit with an activity completion date of 
            /// last month
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Live,
                username: user.Username, 
                isNewUser: true,
                actStartDt: currentDatetime.AddMonths(-1),
                actCompletionDt: currentDatetime.AddMonths(-1),
                creditsRequested: 1);

            /// 2. Adjust the user into another cycle then submit an activity with 2 credits for this cycle
            /// with an activity completion date of today
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.AF, currentDatetime);
            Activity remedialAct = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Live,
                creditsRequested: 2);

            /// 3. On the Credit Summary page, click on View All Cycles button and then verify the form open and shows 
            /// the coorresponding credits for each cycle. 1 Credit for prior Default cycle. 2 credits for current Affiliate  
            /// cycle
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
            CreditSummaryPage CSP = EADP.ClickAndWaitBasePage(EADP.CreditSummaryTab);
            CSP.ClickAndWait(CSP.ViewAllCyclesBtn);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryTabViewAllCyclesFormCycle,
                rowIndex: "0", colName: Const_Mainpro.TableColumnNames.CertifiedApplied.GetDescription(), 
                cellTextExpected: "2");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryTabViewAllCyclesFormCycle,
                rowIndex: "0", colName: Const_Mainpro.TableColumnNames.TotalAppliedCredits.GetDescription(),
                cellTextExpected: "2");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryTabViewAllCyclesFormCycle,
                rowIndex: "1", colName: Const_Mainpro.TableColumnNames.CertifiedApplied.GetDescription(),
                cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryTabViewAllCyclesFormCycle,
                rowIndex: "1", colName: Const_Mainpro.TableColumnNames.TotalAppliedCredits.GetDescription(),
                cellTextExpected: "1");

            /// 4. Click the View Details button for the prior cycle then verify: A label appears warning the user that 
            /// the current view is the previous cycle. 1 credit is appearing in the appropriate tables. 
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.CreditSummaryTabViewAllCyclesFormCycle, 
                cellText: "VIEW DETAILS", rowNum: 2);
            Assert.True(Browser.Exists(Bys.CreditSummaryPage.YouAreCurrentlyViewingPreviousCycleDate, ElementCriteria.IsVisible));
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "1");

        }

        #endregion Tests
    }
}