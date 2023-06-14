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
using System.Globalization;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_CreditValidation1_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CreditValidation1_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_CreditValidation1_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given I submit an activity with 400 credits that requires credit validation, When I view the UI, " +
            "Then all labels should update accordingly, and When I validate this credit on Lifetime Support, Then " +
            "all labels should show that the credit was applied")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CreditValidationApproval()
        {
            /// 1. Submit an activity with 400 credits that requires credit validation
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_InternationalCPDActivitiesIndividualConsideration_VR_DR,
                creditsRequested: 400);

            /// 2. On the CPD Activities List page table, verify the following: Credits Reported = 400, Credits Applied = -,
            /// Actions = View button with Awaiting Approval label, Delete = disabled
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Reported", cellTextExpected: "400");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Applied", cellTextExpected: "-");
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title,
                "Awaiting Approval");
            var ViewBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByText(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow, Act.Title, "span", "View", "span");
            Assert.True(ViewBtn.Enabled);
            var DeleteBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow, Act.Title, "span", "div", "button-icon");
            Assert.True(DeleteBtn.GetAttribute("class").Contains("disabled"));

            /// 3. On the Credit Summary widget Cycle table, verify the following: Certified Applied = 0, Certified 
            /// Requirement Met = No, Total Applied = 0, Total Requirement Met = No
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "No");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "No");

            /// 4. On the Credit Summary page, verify the following on the Assessment table: Certified Activities 
            /// row Credits Applied = 0
            CreditSummaryPage CSP = ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "-");

            /// 5. Validate the credit through Lifetime Support
            string username = APIHelp.GetUserName(browser);
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.ValidateCredit(browser, "College of Family Physician", username, "Certification of Proficiency",
                Act.Title, Constants_LTS.CreditValidationOptions.Accept);
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", username);
            DashboardPage DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            DP.ClickAndWaitBasePage(DP.CPDActivitiesListTab);

            /// 6. On the CPD Activities List page table, verify the following: Credits Reported = 400, Credits Applied = 400,
            /// Actions = Edit button with no Awaiting Approval label, Delete = disabled
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Reported", cellTextExpected: "400");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Applied", cellTextExpected: "400");
            Help.VerifyGridDoesNotContainRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title,
                "Awaiting Approval");
            ViewBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByText(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow, Act.Title, "span", "View", "span");
            Assert.True(ViewBtn.Enabled);
            DeleteBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow, Act.Title, "span", "div", "button-icon");
            Assert.True(DeleteBtn.GetAttribute("class").Contains("disabled"));

            /// 7. On the Credit Summary widget Cycle table, verify the following: Certified Applied = 400, Certified 
            /// Requirement Met = Yes, Total Applied = 400, Total Requirement Met = Yes
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "400");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "Yes");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "400");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "Yes");

            /// 8. On the Credit Summary page, verify the following on the Assessment table: Certified Activities 
            /// row Credits Applied = 400
            ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "400");
        }

        #endregion Tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_CreditValidation2_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CreditValidation2_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_CreditValidation2_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Given I submit an activity with 400 credits that requires credit validation, When I reject this " +
            "credit on Lifetime Support, Then all labels should show that the credit was not applied and the " +
            "activity should be removed from the Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CreditValidationRejection()
        {
            /// 1. Submit an activity with 400 credits that requires credit validation
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_InternationalCPDActivitiesIndividualConsideration_VR_DR,
                creditsRequested: 400);

            /// 2. Reject the credit through Lifetime Support
            string username = APIHelp.GetUserName(browser);
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.ValidateCredit(browser, "College of Family Physician", username, "Certification of Proficiency",
                Act.Title, Constants_LTS.CreditValidationOptions.Reject);
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", username);
            DashboardPage DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            DP.ClickAndWaitBasePage(DP.CPDActivitiesListTab);

            /// 3. On the CPD Activities List page table, verify the following: The activity is removed
            Assert.True(browser.Exists(Bys.CPDActivitiesListPage.ActTblNoCPDActivitiesLbl, ElementCriteria.IsVisible));

            /// 4. On the Credit Summary widget Cycle table, verify the following: Certified Applied = 0, Certified 
            /// Requirement Met = No, Total Applied = 0, Total Requirement Met = Yes
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "No");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "No");

            /// 5. On the Credit Summary page, verify the following on the Assessment table: Certified Activities 
            /// row Credits Applied = -
            CreditSummaryPage CLP = ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CLP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "-");
        }

        #endregion Tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_CreditValidation3_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CreditValidation3_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_CreditValidation3_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Given I submit an activity with 400 credits that requires credit validation, When I choose " +
            "Needs More Information on Lifetime Support, Then all labels should show that the credit was not " +
            "applied and the activity should appear on the Holding Area and Activities List page with an Edit " +
            "button")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CreditValidationNeedsMoreInformation()
        {
            /// 1. Submit an activity with 400 credits that requires credit validation
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_InternationalCPDActivitiesIndividualConsideration_VR_DR,
                creditsRequested: 400);

            /// 2. Choose Needs More Information through Lifetime Support
            string username = APIHelp.GetUserName(browser);
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.ValidateCredit(browser, "College of Family Physician", username, "Certification of Proficiency",
                Act.Title, Constants_LTS.CreditValidationOptions.NeedMoreInformation);
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", username);
            DashboardPage DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            DP.ClickAndWaitBasePage(DP.CPDActivitiesListTab);

            /// 3. On the CPD Activities List page table, verify the Complete button is appearing for the activity
            var CompleteActBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByText(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow, Act.Title, "span", "Complete", "span");
            Assert.True(CompleteActBtn.Enabled);

            /// 4. On the Credit Summary widget Cycle table, verify the following: Certified Applied = 0, Certified 
            /// Requirement Met = No, Total Applied = 0, Total Requirement Met = Yes
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "No");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "No");

            /// 5. On the Credit Summary page, verify the following on the Assessment table: Certified Activities 
            /// row Credits Applied = -
            CreditSummaryPage CLP = ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CLP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "-");

            /// 6. Verify the activity appears in the Holding Area
            HoldingAreaPage HP = CLP.ClickAndWaitBasePage(CLP.HoldingAreaTab);
            CompleteActBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByText(browser, HP.SummTabIncompActTbl,
            Bys.HoldingAreaPage.SummTabIncompActTblBodyFirstRow, Act.Title, "span", "Complete Activity", "span");
            Assert.True(CompleteActBtn.Enabled);
        }

        #endregion Tests
    }


}