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
using LS.AppFramework.Constants_LTS;
using System.Globalization;

namespace Mainpro.UITest
{
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_RoyalCollege_tests : TestBase
    {
        #region Constructors
        public Mainpro_RoyalCollege_tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Mainpro_RoyalCollege_tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version , platform, hubUri, extrasUri)
        { }
        #endregion


        #region Tests

        [Test]
        [Description("Given a user with a Default cycle submits a Certified Royal College activity with 200 credits, " +
            "When the user clicks the Submit button, Then the user should be prompted of the Max Credits rules outlining " +
            "how many credits will be applied and not applied, and When the user chooses both options on the " +
            "prompt (Add Non-Certified/Update Current Form), Then the application should function accordingly " +
            "in terms of reporting and applying credit appropriately")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("Complete")]
        public void RoyalCollegeAdd200CreditsCertified400NonCertified()
        {
            // Store the test data into variables
            var actCategory = Const_Mainpro.ActivityCategory.GroupLearning;
            var actCertType = Const_Mainpro.ActivityCertType.Certified;
            var actType = Const_Mainpro.ActivityType.GRPLRNING_CERT_RoyalCollegeMOCAccreditedSection1_LO;
            var actFormat = Const_Mainpro.ActivityFormat.Live;

            /// 1. For a user in a Default cycle, enter 200 credits for a Royal College Section 1 activity and 
            /// click the Submit button
            EnterACPDActivityDetailsPage EADP = Help.ChooseActivityContinueToDetailsPage(browser, TestContext.CurrentContext.Test,
                actCategory, actCertType, actType, actFormat);
            Activity CertifiedAct1 = EADP.FillActivityForm(credits: 200);
            ElemSet.ClickAfterScroll(Browser, EADP.SubmitBtn);

            /// 2. Verify the Max Credits form appears. The labels should appear on this form showing
            /// credits claimed = 200, applied = 50, not applied = 150
            browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormClaimedLbl, TimeSpan.FromSeconds(120),
                ElementCriteria.IsVisible);
            Assert.AreEqual("200", EADP.MaxCreditReachedFormClaimedLbl.Text);
            Assert.AreEqual("50", EADP.MaxCreditReachedFormUpdateAppliedLbl.Text);
            Assert.AreEqual("150", EADP.MaxCreditReachedFormUpdateNotAppliedLbl.Text);
            
            /// 3. Click the Add Non Certified Activity button, verify the form switches to Non-Certified.
            EADP.ClickAndWait(EADP.MaxCreditReachedFormAddNonCertActBtn);
            Assert.True(EADP.NonCertifiedRdo.Selected);

            /// 4. Enter 200 in the Credits field, submit the activity, then verify that the CPD Activity List 
            /// page table contains both certified and non-certified with the following credits. Certified 
            /// activity Credits Reported = 200, Credits Applied = 50. Non-Certified activity Credits Reported 
            /// and Applied = 400. 
            Activity NonCertifiedAct1 = EADP.FillActivityForm(credits: 400);
            EADP.ClickAndWait(EADP.SubmitBtn);
            CPDActivitiesListPage ALP = EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);

            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, CertifiedAct1.Title);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: CertifiedAct1.Title, colName: "Credits Reported", cellTextExpected: "200");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: CertifiedAct1.Title, colName: "Credits Applied", cellTextExpected: "50");

            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, NonCertifiedAct1.Title);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: NonCertifiedAct1.Title, colName: "Credits Reported", cellTextExpected: "400");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: NonCertifiedAct1.Title, colName: "Credits Applied", cellTextExpected: "400");

            /// 5. Verify the Credit Summary widget/table is also reflecting the correct numbers as shown in step #4
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "50");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Non-Certified", colName: "Applied", cellTextExpected: "400");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "450");

            /// 6. Click on the Click Here to view your certified AMA/RCP" link and verify RCP Reported = 200,
            /// Applied = 50
            // Closed defect https://code.premierinc.com/issues/browse/MAINPROREW-793. 
            ALP.ClickAndWaitBasePage(ALP.ClickHereToViewYourAmaRCPCreditsLnk);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.AMARCPMaxCreditForm,
                rowName: "Royal College MOC Accredited Section 1 & 3", colIndex: "2", cellTextExpected: "200");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.AMARCPMaxCreditForm,
                rowName: "Royal College MOC Accredited Section 1 & 3", colIndex: "3", cellTextExpected: "50");
        }

        // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-847. See comments inside the test for 
        // more info. Uncomment and execute when fixed
        [Test]
        [Description("Given a user with a Default cycle submits a Certified Royal College activity with 30 credits " +
            "and another one with 30 credits, When the second one is submitted, Then the user should be prompted " +
            "of the Max Credits rules outlining how many credits will be applied and not applied, and the Activities " +
            "List page should function accordingly in terms of reporting and applying credit appropriately, and When " +
            "the user tries to add another RCP activity and selects the Activity Type in the Select Element " +
            "on the Enter an Activity page, Then the user should be prompted of the Max Credits rules and the " +
            "buttons on the prompt should function accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void RoyalCollegeAdd2CertifiedActiviesOverMax()
        {
            // Store the test data into variables
            var actCategory1 = Const_Mainpro.ActivityCategory.GroupLearning;
            var actType1 = Const_Mainpro.ActivityType.GRPLRNING_CERT_RoyalCollegeMOCAccreditedSection1_LO;
            var actCategory2 = Const_Mainpro.ActivityCategory.Assessment;
            var actType2 = Const_Mainpro.ActivityType.ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO;
            var actCertType = Const_Mainpro.ActivityCertType.Certified;
            var actFormat = Const_Mainpro.ActivityFormat.Live;

            /// 1. For a user in a Default cycle, enter 30 credits for an AMA PRA Group Learning activity, click 
            /// the Submit button, go to the Activity List page, verify Credits Applied = 30
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(Browser);
            Activity Act1 = Help.AddActivity(browser, TestContext.CurrentContext.Test, actCategory1, actCertType, actType1, actFormat, creditsRequested: 30);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act1.Title, colName: "Credits Applied", cellTextExpected: "30");
            
            //<<<<< The below 2 lines of code is Not necessary instead of Sleep added these 2 lines >>>>
            // Update: I added a new line of code to wait for the credit to appear in the widget because this test still
            // fails to show the Max Credit popup even after the credit gets returned on the activities list table. If this 
            // still fails, going to have to add a Thread.Sleep or figure out what else would work
            // Update: Script still failles so I added Goto Dashboard page line of code instead of sleep.
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "30");
            Thread.Sleep(TimeSpan.FromSeconds(30));
            DashboardPage DP = ALP.ClickAndWaitBasePage(ALP.DashboardTab);

            
            /// 2. Enter 30 credits for a Royal College Section 3 activity and click the Submit button then 
            /// Verify the Max Credits form appears. The labels should appear on this form showing
            /// credits claimed = 30, applied = 20, not applied = 10
            EnterACPDActivityDetailsPage EADP = Help.ChooseActivityContinueToDetailsPage(browser, TestContext.CurrentContext.Test,
                actCategory2, actCertType, actType2, actFormat);
            Activity Act2 = EADP.FillActivityForm(credits: 30);
            ElemSet.ClickAfterScroll(Browser, EADP.SubmitBtn);
            // Outstanding defect https://code.premierinc.com/issues/browse/MAINPROREW-847. See comments a couple lines 
            // above this one for explanation. This is the line of code it fails on
            browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormClaimedLbl);
            Assert.AreEqual("30", EADP.MaxCreditReachedFormClaimedLbl.Text);
            Assert.AreEqual("20", EADP.MaxCreditReachedFormUpdateAppliedLbl.Text);
            Assert.AreEqual("10", EADP.MaxCreditReachedFormUpdateNotAppliedLbl.Text);
            EADP.ClickAndWait(EADP.MaxCreditReachedFormAddNonCertActBtn);
            EADP.ClickAndWaitBasePage(EADP.CPDActivitiesListTab);

            /// 3. Verify that the CPD Activity List page table contains both activities with the following. 
            /// Section 1 activity Credits Reported = 30, and Applied = 30. Section 2 activity Credits Reported 
            /// = 30 Applied = 20. 
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act1.Title);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act1.Title, colName: "Credits Reported", cellTextExpected: "30");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act1.Title, colName: "Credits Applied", cellTextExpected: "30");

            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act2.Title);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act2.Title, colName: "Credits Reported", cellTextExpected: "30");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act2.Title, colName: "Credits Applied", cellTextExpected: "20");

            /// 4. Verify the Credit Summary widget/table is also reflecting the correct numbers as shown in step #4
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "50");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Non-Certified", colName: "Applied", cellTextExpected: "0");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Total", colName: "Applied", cellTextExpected: "50");

            /// 5. Click Enter a CPD Activity, choose the same activity, click Continue and verify that a form appears
            /// indicating the user has already added the maximum of 50 credits
            EnterACPDActivityPage EAP = EADP.ClickAndWaitBasePage(EADP.EnterCPDActBtn);
            EAP.SelectAndWait(EAP.CategorySelElem, actCategory2.GetDescription());
            EAP.ClickAndWait(EAP.CertifiedRdo);
            EAP.SelectAndWait(EAP.ActivityTypeSelElem, actType2.GetDescription());
            Assert.AreEqual("50", EAP.MaxCreditReachedFormClaimedLbl.Text);

            /// 6. Click the Start Over button and verify the page refreshes
            EAP.ClickAndWait(EAP.MaxCreditReachedFormStartOverBtn);

            /// 7. Repeat step 5 but this time click the Add Non-Certified Activity button and verify the Cert Type 
            /// radio button changes to Non-Certified. 
            EAP.SelectAndWait(EAP.CategorySelElem, actCategory2.GetDescription());
            EAP.ClickAndWait(EAP.CertifiedRdo);
            EAP.SelectAndWait(EAP.ActivityTypeSelElem, actType2.GetDescription());
            EAP.ClickAndWait(EAP.MaxCreditReachedFormAddNonCertActBtn);
            Assert.True(EAP.NonCertifiedRdo.Selected);

        }

        #endregion Tests
    }
}
