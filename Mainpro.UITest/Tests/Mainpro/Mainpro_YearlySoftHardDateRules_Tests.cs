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

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_YearlySoftHardDateRules_Tests : TestBase
    {
        #region Constructors
        public Mainpro_YearlySoftHardDateRules_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        public Mainpro_YearlySoftHardDateRules_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests



        [Test]
        [Description("Given a user is registered to Mainpro for the prior year on March 2nd, When an activity is " +
            "added with 25 credits and a completion date of March 1st (outside of the 'soft date'), " +
            "Then credits should not be applied, and When the activity is modified with a completion " +
            "date of March 3rd (inside the 'soft date'), Then credits should be applied and requirements " +
            "will be met for the year, and When the activity is modified to only have 24 credits, Then requirements " +
            "will not be met for the year")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UserRegisteredBeforeJuly1()
        {
            /// 1. Register a user with an effective date of March 2nd of the prior year. For this user, add an activity 
            /// with a completion date of March 1st with 25 credits. Since March 1st would fall outside the cycle "soft" 
            /// date (March 2nd of prior year), then credit should not be applied. On the UI, Credits Reported = 25, 
            /// Credits Applied = 0 on all labels except for the activity table, Requirements Met = No on the yearly 
            /// requirements table. NOTE: Credits Applied on the activity table will show the credits. Per
            /// BA, this is misleading because these credits are actually not applied, but it has been functioning 
            /// this way and has not been a problem so we will keep it this way.
            DateTime userEffectiveDt = new DateTime(currentDatetime.Year - 1, 3, 2);
            DateTime completionDtWithinSoftDate = new DateTime(currentDatetime.Year - 1, 3, 2);
            DateTime completionDtOutsideSoftDate = new DateTime(currentDatetime.Year - 1, 3, 1);
            UserModel user1 = UserUtils.CreateAndRegisterUser(effectiveDt: userEffectiveDt,
                currentTest: TestContext.CurrentContext.Test);
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live, 
                username: user1.Username, isNewUser: true, creditsRequested: 25, actStartDt: completionDtOutsideSoftDate, 
                actCompletionDt: completionDtOutsideSoftDate);

            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Reported", cellTextExpected: "25");

            // Read step 1. above and 
            // <25/Jan Comments by Bama>>
            // cellTextExpected: "25" Changed to cellTextExpected: "0"
            // because Credits Applied = 0 started showing in activity table
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Applied", cellTextExpected: "0");
            
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "0");

            CreditSummaryPage CSP = ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Certified Credits Applied", colIndex: "1", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Total Credits Applied", colIndex: "1", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Requirement Met", colIndex: "1", cellTextExpected: "No");

            /// 2. Change the completion date of the activity to March 2nd of prior year. Since March 2nd falls inside 
            /// the cycle "soft" date (March 2nd of prior year), then credit should be applied. On the UI, Credits 
            /// Reported = 25, Credits Applied = 25 on all labels, Requirements Met = Yes on the annual requirement table
            ALP.ClickAndWaitBasePage(ALP.CPDActivitiesListTab);
            ALP.SelectAndWait(ALP.ActTblCycleSelElem, "All");
            EnterACPDActivityDetailsPage EADP = Help.Grid_ClickCellInTable(browser,
                Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title,
                Const_Mainpro.TableButtonLinkOrCheckBox.Edit);
            EADP.FillActivityForm(actTitle: Act.Title, credits: 25, actStartDt: completionDtWithinSoftDate, 
                actCompletionDt: completionDtWithinSoftDate);
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);

            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Reported", cellTextExpected: "25");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Applied", cellTextExpected: "25");

            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "25");

            ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Certified Credits Applied", colIndex: "1", cellTextExpected: "25");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Total Credits Applied", colIndex: "1", cellTextExpected: "25");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Requirement Met", colIndex: "1", cellTextExpected: "Yes");

            /// 3. Change the credits of the activity to 24. On the UI, Requirements Met = No on the annual requirement table
            ALP.ClickAndWaitBasePage(ALP.CPDActivitiesListTab);
            Help.Grid_ClickCellInTable(browser,
                Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title,
                Const_Mainpro.TableButtonLinkOrCheckBox.Edit);
            EADP.FillActivityForm(actTitle: Act.Title, credits: 24, actStartDt: completionDtWithinSoftDate,
                actCompletionDt: completionDtWithinSoftDate);
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);

            ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Requirement Met", colIndex: "1", cellTextExpected: "No");
        }

        [Test]
        [Description("Given a user is registered to Mainpro for the prior year on August 2nd, When an activity is " +
            "added with 25 credits and a completion date of August 1st (before effective date of registration), " +
            "Then credits should not be applied, and When the activity is modified with a completion " +
            "date of August 3rd (after effective date of registration), Then credits should be applied and requirements " +
            "will be met for the year, and When the activity is modified to only have 24 credits, Then requirements " +
            "will not be met for the year")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void UserRegisteredAfterJuly1()
        {
            /// 1. Register a user with an effective date of August 2nd of the prior year. For this user, add an activity 
            /// with a completion date of August 1st with 25 credits. Since August 1st would fall before the effective 
            /// date of the registration (August 2nd of prior year), then credit should not be applied. On the UI, 
            /// Credits Reported = 25, Credits Applied = 0 on all labels except for the activity table, 
            /// Requirements Met = No on the yearly requirements table. NOTE: Credits Applied on the activity table 
            /// will show the credits. Per BA, this is misleading because these credits are actually not applied, 
            /// but it has been functioning this way and has not been a problem so we will keep it this way. 
            DateTime userEffectiveDt = new DateTime(currentDatetime.Year - 1, 8, 2);
            DateTime completionDtOnEffectiveDt = new DateTime(currentDatetime.Year - 1, 8, 2);
            DateTime completionDtBeforeEffectiveDt = new DateTime(currentDatetime.Year - 1, 8, 1);
            UserModel user1 = UserUtils.CreateAndRegisterUser(effectiveDt: userEffectiveDt, 
                currentTest: TestContext.CurrentContext.Test);
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live,
                username: user1.Username, isNewUser: true, creditsRequested: 25, actStartDt: completionDtBeforeEffectiveDt,
                actCompletionDt: completionDtBeforeEffectiveDt);

            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Reported", cellTextExpected: "25");
            
            // Read step 1. above and 
            // <25/Jan Comments by Bama>>
            // cellTextExpected: "25" Changed to cellTextExpected: "0"
            // because Credits Applied = 0 started showing in activity table
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Applied", cellTextExpected: "0");

            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "0");

            CreditSummaryPage CSP = ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Certified Credits Applied", colIndex: "1", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Total Credits Applied", colIndex: "1", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Requirement Met", colIndex: "1", cellTextExpected: "No");

            /// 2. Change the completion date of the activity to August 2nd of prior year. Since August 2nd falls on or  
            /// after the effective date of the registration (August 2nd of prior year), then credit should be applied.
            /// On the UI, Credits Reported = 25, Credits Applied = 25 on all labels, Requirements Met = Yes on the 
            /// annual requirement table
            ALP.ClickAndWaitBasePage(ALP.CPDActivitiesListTab);
            ALP.SelectAndWait(ALP.ActTblCycleSelElem, "All");
            EnterACPDActivityDetailsPage EADP = Help.Grid_ClickCellInTable(browser,
                Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title,
                Const_Mainpro.TableButtonLinkOrCheckBox.Edit);
            EADP.FillActivityForm(actTitle: Act.Title, credits: 25, actStartDt: completionDtOnEffectiveDt,
                actCompletionDt: completionDtOnEffectiveDt);
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);

            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Reported", cellTextExpected: "25");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Applied", cellTextExpected: "25");

            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "25");

            ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Certified Credits Applied", colIndex: "1", cellTextExpected: "25");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Total Credits Applied", colIndex: "1", cellTextExpected: "25");
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Requirement Met", colIndex: "1", cellTextExpected: "Yes");

            /// 3. Change the credits of the activity to 24. On the UI, Requirements Met = No on the annual requirement table
            ALP.ClickAndWaitBasePage(ALP.CPDActivitiesListTab);
            Help.Grid_ClickCellInTable(browser,
                Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title,
                Const_Mainpro.TableButtonLinkOrCheckBox.Edit);
            EADP.FillActivityForm(actTitle: Act.Title, credits: 24, actStartDt: completionDtOnEffectiveDt,
                actCompletionDt: completionDtOnEffectiveDt);
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);

            ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CSP, Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs,
                rowName: "Requirement Met", colIndex: "1", cellTextExpected: "No");
        }

        #endregion Tests
    }


}