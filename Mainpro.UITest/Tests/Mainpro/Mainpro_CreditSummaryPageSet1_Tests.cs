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
using Criteria = Mainpro.AppFramework.Criteria;

namespace Mainpro.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_CreditSummaryPageSet1_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CreditSummaryPageSet1_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        public Mainpro_CreditSummaryPageSet1_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given a user adds both a certified and non-certified activity for each category with a " +
            "specified amount of credits, When the Credit Summary page is viewed, Then the credits appear in " +
            "the tables")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CreditSummaryTableCreditVerification()
        {
            /// 1. Add an activity with 1 certified credit and an activity with 1 non-certified credit for each 
            /// Category
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(browser);
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Online,
                creditsRequested: 1);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_FamilyMedicineCurriculumReview,
                creditsRequested: 1);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_OtherCFPCCertifiedMainproGroupLearningActivities_LO,
                Const_Mainpro.ActivityFormat.Live,
                creditsRequested: 1);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.GRPLRNING_NONCERT_RoyalCollegeMOCSection1_LO,
                Const_Mainpro.ActivityFormat.Live,
                creditsRequested: 1);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_OtherCFPCCertifiedMainproSelfLearningActivities_LO,
                Const_Mainpro.ActivityFormat.Live,
                creditsRequested: 1);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.SELFLRNING_NONCERT_Research,
                creditsRequested: 1);

            /// 2 Assert that the table's Certified/NonCertified Activities row Credits Applied cell shows
            /// 1 credit each
            CreditSummaryPage CP = ALP.ClickAndWaitBasePage(ALP.CreditSummaryTab);
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabGroupLearn,
                    rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabGroupLearn,
                    rowName: "Non-certified Activities", colName: "Credits Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabSelfLearn,
                    rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabSelfLearn,
                    rowName: "Non-certified Activities", colName: "Credits Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                    rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                    rowName: "Non-certified Activities", colName: "Credits Applied", cellTextExpected: "1");
        }

        [Test]
        [Description("Verify that user able to Edit, Delete and View the activities from" +
            " View Link Popup in Credit Summary Page")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void CreditSummaryPageEditDeleteViewActions()
        {
            
            /// 1. Create an user and register to Default Cycle
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);

            /// 2. Submit 2 activities of type GroupLearning and Asessment Category type 
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live);

            Activity Act1 = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.GRPLRNING_NONCERT_OtherNonCertifiedGroupLearningActivities_LO_VR,
                Const_Mainpro.ActivityFormat.Live);
            Help.ValidateCreditReevaluateUserThenRelaunchMainpro(Browser, user.Username, Act1.Title,
               TestContext.CurrentContext.Test.Name);

            /// 3. Navigate to CreditSummaryPage,under GroupLearning Table section
            /// Click view Link to open the View the activities List Popup
            CreditSummaryPage CP1 = DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.CreditSummaryTabGroupLearn,
                cellText: "View", rowNum: 2);

            /// 4. Verify that Submitted Activity and Credits displayed correctly in the popup List
            Help.VerifyCellTextMatches(browser, CP1, Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities,
                    rowName: Act1.Title, colName: "Credits", cellTextExpected: "1");

            /// 5. Click on View icon of the activity and Verify that View CPD Activity Form 
            /// displayed with correct ativity and credits
            EnterACPDActivityDetailsPage EADP1 = Help.Grid_ClickCellInTable(Browser,
                Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities, Act1.Title,
                Const_Mainpro.TableButtonLinkOrCheckBox.View);
            Thread.Sleep(TimeSpan.FromSeconds(120));
            Assert.IsTrue(Browser.Exists(Bys.EnterACPDActivityDetailsPage.CreditsRequestedOrClaimedTxt,
                ElementCriteria.AttributeValueContains("value","1")));
            Assert.IsTrue(Browser.Exists(Bys.EnterACPDActivityDetailsPage.ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt,
                ElementCriteria.AttributeValueContains("value", Act1.Title)));

            /// 6. Navigate to CreditSummaryPage,under Assessment Table section
            /// Click view Link to open the View the activities List Popup
            CreditSummaryPage CP = DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            ElemSet.ScrollToElement(Browser, CP.AssessmentTbl);
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.CreditSummaryTabAssessment,
                cellText: "View", rowNum: 1);
            
            /// 7. Verify that Submitted Activity and Credits displayed correctly in the View popup List
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities,
                    rowName: Act.Title, colName: "Credits", cellTextExpected: "1");

            /// 8. Click on Edit icon of the activity and Verify that CPD Activity Form 
            /// displayed in editable mode, Change the Credit value and submit it .            
            EnterACPDActivityDetailsPage EADP = Help.Grid_ClickCellInTable(Browser, 
                Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities, Act.Title,
                Const_Mainpro.TableButtonLinkOrCheckBox.Edit);

            ElemSet.TextBox_EnterText(Browser, EADP.CreditsRequestedOrClaimedTxt, true, "10.5");
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.YourActivityHasBeenSubmittedCloseBtn.Click();

            CP.RefreshPage(true);

            /// 9. Now, verify that updated credits reflected in Assessment Table section and 
            /// View Link Popup and Cycle Table in CreditSummary Page
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                    rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "10.5");
            ElemSet.ScrollToElement(Browser, CP.CredSummAnnualReqsTbl);

            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.CreditSummaryTabAssessment,
                cellText: "View", rowNum: 1);
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities,
                    rowName: Act.Title, colName: "Credits", cellTextExpected: "10.5");

            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryWidgetCycle, "10.5",
                rowName: "Certified", colName: "Applied");

            /// 10. Click on Delete icon of the activity in the ViewLink Popup and 
            /// Click confirm to delete the activity. 
            Help.Grid_ClickCellInTable(Browser, Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities, Act.Title,
                Const_Mainpro.TableButtonLinkOrCheckBox.Delete);
            CP.DeleteFormYesBtn.Click();
            Browser.WaitJSAndJQuery();
            Help.VerifyGridDoesNotContainRecord(Browser, Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities, Act.Title);
            
            CP.ClickAndWait(CP.CSViewFormViewActivitiesCloseBtn);

            /// 11. Now, verify that deleted activity credits removed from Assessment Table section and 
            /// View Link Popup and Cycle Table in CreditSummary Page
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryWidgetCycle, "0",
                rowName: "Certified", colName: "Applied");
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabAssessment,
                    rowName: "Certified Activities", colName: "Credits Applied", cellTextExpected: "-");

        }

        #endregion Tests
    }

}