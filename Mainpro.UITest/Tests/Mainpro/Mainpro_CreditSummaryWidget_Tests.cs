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

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]

    class Mainpro_CreditSummaryWidget_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CreditSummaryWidget_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Mainpro_CreditSummaryWidget_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version , platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given I add a Certified activity with 300 credits that does not require validation, and a " +
            "Non-Certified activity with 1 credit, When I view the Credit Summary widgets for each page, Then the " +
            "corresponding credit numbers and Requirement Met label should be updated accordingly, and the " +
            "user should have met his/her requirement")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CreditSummaryWidgetsVerificationReqMet()
        {
            /// 1. Submit a Certified activity with 300 credits that does not require validation. Also submit a 
            /// Non-Certified activity with 1 credit
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Live, creditsRequested: 300);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_FamilyMedicineCurriculumReview, creditsRequested: 1);

            /// 2. Go to the Credit Summary page and verify the labels are correct on the Cycle table. Certified Applied 
            /// = 300, Certified Req Met = Yes, Non-Certified Applied = 1, Total Applied = 301, Total Req Met = Yes
            CreditSummaryPage CSP = ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "Yes");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Non-Certified", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "Yes");

            /// 3. Verify the labels are correct on the Current Year table. Certified Applied 
            /// = 300, Certified Req Met = Yes, Non-Certified Applied = 1, Total Applied = 301, Total Req Met = Yes
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "0", colName: "Applied", cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "1", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Applied", cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Requirement Met", cellTextExpected: "Yes");

            /// 4. Verify the labels are correct on the Annual Requirements table. Certified Credits 
            /// Applied = 300, Non-Certified Credits Applied = 1, Total Credits Applied = 301, Requirement Met = Yes
            /// NOTE: If current date is July 1 or after, then the credits from the activity will show in the 
            /// current year - next year column. 
            string colIndex = currentDatetime.Month < 7 ? "1" : "2";
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Certified Credits Applied", colIndex: colIndex, cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Non-Certified Credits Applied", colIndex: colIndex, cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Total Credits Applied", colIndex: colIndex, cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Requirement Met", colIndex: colIndex, cellTextExpected: "Yes");

            /// 5. Repeat the same verifications on the other pages
            DashboardPage DP = CSP.ClickAndWaitBasePage(CSP.DashboardTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "Yes");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Non-Certified", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "Yes");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "0", colName: "Applied", cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "1", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Applied", cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Requirement Met", cellTextExpected: "Yes");
            HoldingAreaPage HP = CSP.ClickAndWaitBasePage(CSP.HoldingAreaTab);
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "Yes");
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Non-Certified", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "Yes");
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "0", colName: "Applied", cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "1", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Applied", cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, HP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Requirement Met", cellTextExpected: "Yes");
            CSP.ClickAndWaitBasePage(CSP.CPDActivitiesListTab);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "Yes");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Non-Certified", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "Yes");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "0", colName: "Applied", cellTextExpected: "300");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "1", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Applied", cellTextExpected: "301");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Requirement Met", cellTextExpected: "Yes");
        }


        [Test]
        [Description("Given I add a Non-Certified activity with 300 credits, When I view the Credit Summary widgets, " +
            "Then the corresponding credit numbers and Requirement Met label should be updated accordingly, and the " +
            "user should not have met his/her requirement")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CreditSummaryWidgetsVerificationReqNotMet()
        {
            /// 1. Submit a Non-Certified activity with 300 credits
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_FamilyMedicineCurriculumReview, creditsRequested: 1);

            /// 2. Go to the Credit Summary page and verify the labels are correct on the Cycle table. Certified Applied 
            /// = 0, Certified Req Met = No, Non-Certified Applied = 1, Total Applied = 1, Total Req Met = No
            CreditSummaryPage CSP = ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Requirement Met", cellTextExpected: "No");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Non-Certified", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Requirement Met", cellTextExpected: "No");

            /// 3. Verify the labels are correct on the Current Year table. Certified Applied 
            /// = 0, Certified Req Met = No, Non-Certified Applied = 1, Total Applied = 1, Total Req Met = No
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "0", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "1", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                rowIndex: "2", colName: "Requirement Met", cellTextExpected: "No");

            /// 4. Verify the labels are correct on the Annual Requirements table. Certified Credits 
            /// Applied = 0, Non-Certified Credits Applied = 1, Total Credits Applied = 1, Requirement Met = No.
            /// NOTE: If current date is July 1 or after, then the credits from the activity will show in the 
            /// current year - next year column. 
            string colIndex = currentDatetime.Month < 7 ? "1" : "2";
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Certified Credits Applied", colIndex: colIndex, cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Non-Certified Credits Applied", colIndex: colIndex, cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Total Credits Applied", colIndex: colIndex, cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Requirement Met", colIndex: colIndex, cellTextExpected: "No");
        }


        #endregion

    }
}
