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
    public class Mainpro_CycleGeneral_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CycleGeneral_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_CycleGeneral_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        ///// <summary>
        ///// Example of how to override the teardown at the test class level
        ///// </summary>
        //public override void TearDown()
        //{
        //    // Add your script here
        //    // ........
        //    base.AfterTest();
        //}

        [Test]
        [Description("Given I adjust a user into a 2-year cycle, When I add an activity, Then credit will need validation and " +
            "documentation will be required")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void TwoYearCycleCreditValDocumentationReq()
        {
            /// 1. Create a Default cycle user, then put the user into a Remedial cycle using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.R);

            /// 2. Go to the Activity Details page and verify the Validation and Documentation Required label appear and that the 
            /// Submit button is disabled
            Help.ChooseActivityContinueToDetailsPage(browser, 
                TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Live, username: user.Username);
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(browser);
            Assert.AreEqual(EADP.CreditApprovalRequiredAndDocUploadRequiredLbl.Text, "Credit Approval and Documentation Required");
            Assert.AreEqual("true", EADP.SubmitBtn.GetAttribute("aria-disabled"));

            /// 3. Fill required fields, add 1 credit, upload a document, submit the activity and verify Credits Report = 1,
            /// Credits Applied = -, Actions = Awaiting Approval
            Activity Act = EADP.FillActivityForm();
            EADP.ClickAndWait(EADP.SubmitBtn);
            CPDActivitiesListPage ALP = EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Reported", cellTextExpected: "1");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Act.Title, colName: "Credits Applied", cellTextExpected: "-");
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title,
                "Awaiting Approval");
        }

        [Test]
        [Description("Given I adjust a user into a 2-year cycle, When I add an activity with 100 certified credits within the " +
            "first year of this 2 year cycle and then re-evaluate this user on Lifetime Support, Then the user will " +
            "automatically be put back into a Default cycle and all cooresponding indicators will appear accordingly")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void TwoYearCycleBackToDefaultCycle()
        {
            /// 1. Create a Default cycle user, then put the user into a Remedial cycle using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.R);

            /// 2. Submit an activity and add 100 certified credits, then get the cycle start and end dates so we 
            /// can verify dates changed accordingly in test step 4
            Activity Act = Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Live, username: user.Username, creditsRequested: 100);
            int cycleStartYr_RemedialUser = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(6, 4));
            int cycleEndYr_RemedialUser = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(19, 4));

            /// 3. Validate the credit through Lifetime Support, Re-evaluate the user through LTS then verify 
            /// the user is put back into a default cycle and 'EC = early completion' was added on the Adjustment 
            /// tab in LTS. NOTE: We are re-evaluating the user here because the cycle change relies on an overnight 
            /// process to complete, but we can bypass this by re-evaluating through LTS
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.ValidateCredit(browser, "College of Family Physician", user.Username, "Certification of Proficiency",
                Act.Title, Constants_LTS.CreditValidationOptions.Accept);
            LSHelp.ReevaluateUser(browser, "College of Family Physician", user.Username, "Certification of Proficiency");
            By ProgramLblBy = By.XPath("//td[contains(text(),'Program:')]/following-sibling::td[1]");
            LSHelp.VerifyLabelTextEquals(Browser, ProgramLblBy, "Default");
            LSHelp.GoToProgramAdjustmentTab(browser);
            Assert.True(Browser.Exists(By.XPath("//td[text()='EC-Early Completion']")));
            
            /// 4. Verify the cycle start date is the same as it was when it was in Remedial, and the end date is 5 
            /// years from there. 
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            int cycleStartYr_DefaultUser = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(6, 4));
            int cycleEndYr_DefaultUser = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(19, 4));
            Assert.AreEqual(cycleStartYr_RemedialUser, cycleStartYr_DefaultUser);
            Assert.AreEqual(cycleStartYr_DefaultUser + 5, cycleEndYr_DefaultUser);

            /// 5. Verify credits are applied to this cycle 
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle, "100", rowName: "Certified",
                colName: "Applied");
        }

        [Test]
        [Description("Create a user with API, register to default cycle, then add a No Cycle adjustment, then" +
            "launch from LTS and verify it works normally. Log in backdoor and verify it says user does " +
            "not have a valid cycle and when you click button it should take you to CFPC page")]
        [Property("Status", "Complete")]
        [Author("Neela Anand")]
        public void NoCycleAdjustment()
        {
            /// 1. Create a Default cycle user, then put the user into a No cycle using the Adjustment API
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            UserUtils.AdjustUserCycle(Browser, user.Username, Const_Mainpro.AdjustmentCodes.NC, 
                effectiveDate: currentDatetime.AddMonths(-1), isUserLoggedIn: false);

            /// 2. Log in into the application directly, checking user's does not have a valid cycle message is displayed
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);
            Browser.WaitForElement(Bys.DashboardPage.NoCycleCloseBtn, ElementCriteria.IsVisible);
            DP.NoCycleCloseBtn.Click();
            Assert.True(DP.EnterCPDActBtn.Displayed);

            /// 3. Launch same user from LTS and verify it works normally
            LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", user.Username);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            Assert.True(DP.EnterCPDActBtn.Displayed);
        }

        #endregion Tests
    }


}