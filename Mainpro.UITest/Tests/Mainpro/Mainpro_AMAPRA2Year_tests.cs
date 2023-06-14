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
    public class Mainpro_AMAPRA2Year_tests : TestBase
    {
        #region Constructors
        public Mainpro_AMAPRA2Year_tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Mainpro_AMAPRA2Year_tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion


        #region Tests

        [Test]
        [Description("Given a user with a 2-year cycle submits an AMA activity with 201 credits, When the " +
            "user clicks the Submit button, Then the user should NOT be prompted of the Max Credits rules  " +
            "because 2 year cycles require validation, and When the credit is validated, Then the credits " +
            "should appear on the Activities List page at only 25 applied, Then the page should show the correct credit amounts")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("Complete")]
        public void AMAPRA2YearCycleAdd200CreditsCertified()
        {
            // Store the test data into variables
            var actCategory = Const_Mainpro.ActivityCategory.GroupLearning;
            var actCertType = Const_Mainpro.ActivityCertType.Certified;
            var actType = Const_Mainpro.ActivityType.GRPLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO;
            var actFormat = Const_Mainpro.ActivityFormat.Live;

            /// 1. For a user in a Remedial cycle, add an AMA PRA activity, entering 200 credits and verify the user 
            /// does not get warned of the Max Credit rules
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.R);
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test, actCategory, actCertType, actType, actFormat, creditsRequested: 201);

            /// 3. Validate the credit through LTS, go to the Activities List page, then verify that the CPD Activity 
            /// List page table and Credit Summary widget table contains the activity with the following credit amounts. 
            /// Credits Reported = 200, Credits Applied = 25
            Help.ValidateCreditReevaluateUserThenRelaunchMainpro(Browser, user.Username, Act.Title,
                TestContext.CurrentContext.Test.Name);
            CPDActivitiesListPage ALP = LP.ClickAndWaitBasePage(LP.CPDActivitiesListTab);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Reported", cellTextExpected: "201");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Applied", cellTextExpected: "25");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                rowName: "Certified", colName: "Applied", cellTextExpected: "25");

            /// 4. Click Enter a CPD Activity, choose the same activity, click Continue and verify that a form appears
            /// indicating the user has already added the maximum of 25 credits
            EnterACPDActivityPage EAP = ALP.ClickAndWaitBasePage(ALP.EnterCPDActBtn);
            EAP.SelectAndWait(EAP.CategorySelElem, actCategory.GetDescription());
            EAP.ClickAndWait(EAP.CertifiedRdo);
            EAP.SelectAndWait(EAP.ActivityTypeSelElem, actType.GetDescription());
            Assert.AreEqual("25", EAP.MaxCreditReachedFormClaimedLbl.Text);
        }


        #endregion Tests
    }
}
