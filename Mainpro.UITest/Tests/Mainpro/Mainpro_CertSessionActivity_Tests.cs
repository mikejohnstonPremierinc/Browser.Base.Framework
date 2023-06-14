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

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class Mainpro_CertSessionActivity_Tests : TestBase
    {
        #region Constructors
        public Mainpro_CertSessionActivity_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Mainpro_CertSessionActivity_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Given I enter the CPD activity of Assessment type Cert+ Session Activity" +
            " which has configured with Assessor and Assessed Role, When I select the Role individually" +
            " I should see the corresponding Credits displayed in form, When I enter more than " +
            "max credit limit Then I should see error notification")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AssessmentTypeCertSessionAcctivityWithBothRole()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    "because we dont have Cert+ Test Program in Prod and we could not create because " +
                    "Cert+ Program's Require Various Roles Approval Process which Trigger" +
                    "Mails to Client");
            }

            string AssessorMaxCredits = AppSettings.Config["CertAssessmentAssessorOnlyRoleSessionMaxCredit"].ToString();
            string AssessedMaxCredits = AppSettings.Config["CertAssessmentAssessedOnlyRoleSessionMaxCredit"].ToString();
            string MaximumCreditOfBothRoles = AppSettings.Config["MaximumCreditOfBothRoles"].ToString();


            /// 1. Create a Default cycle user
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);

            /// 2. Search an Cert+ Assessment type Session Activity configured with 
            /// Assessor and Assessed Role 
            EnterACPDActivityPage EAP =  DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(AppSettings.Config["CertAssessmentBothRoleSessionIDString"]);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(Keys.Tab);            
            // Auto comment: For some reason, we have to wait here or else after clicking the Continue button, the 
            // application returns the wrong session warning label text
            Thread.Sleep(500);
            EnterACPDActivityDetailsPage EADP = EAP.ClickAndWait(EAP.DoYouKnowYourSessionIDContinueBtn);

                        /// 3. Both Roles Max Credits should be displayed on the Form
            Assert.IsTrue(EADP.AssessorMaxCreditsTxt.GetAttribute("value").Equals(AssessorMaxCredits));
            Assert.IsTrue(EADP.AssessedMaxCreditsTxt.GetAttribute("value").Equals(AssessedMaxCredits));

            /// 4. Fill the fields that are required and not filled in yet. 
            /// Keep the activity title as it's default prepopulated value (with session ID). 
            Activity Act = EADP.FillActivityForm(actStartDt: currentDatetime, actCompletionDt: currentDatetime,
                keepExistingActTitle: true);

            /// 5. Select Assessor Role and Submit Credits more than Max value and Verify that 
            /// error notification displayed
            EADP.PleaseIndicateYourRoleInThisAssessmentAssessorRdo.Click();
            Help.ClearTextBox(EADP.CreditsRequestedOrClaimedTxt);
            EADP.CreditsRequestedOrClaimedTxt.SendKeys("500");
            ElemSet.ClickAfterScroll(Browser, EADP.SubmitBtn);
           
            browser.WaitForElement(Bys.MainproPage.NotificationFormLbl, TimeSpan.FromSeconds(60),
                ElementCriteria.IsVisible);
            Assert.AreEqual("Credit amount should be less than or equal to " + MaximumCreditOfBothRoles +
                " credits for this activity.", EAP.NotificationLbl.Text);
            EAP.ClickAndWaitBasePage(EAP.NotificationFormXBtn);

            /// 6. Select Assessed Role and Submit Credits more than Max value and Verify that 
            /// error notification displayed
            EADP.PleaseIndicateYourRoleInThisAssessmentAssessedRdo.Click();
            Help.ClearTextBox(EADP.CreditsRequestedOrClaimedTxt);
            EADP.CreditsRequestedOrClaimedTxt.SendKeys("600");
            ElemSet.ClickAfterScroll(Browser, EADP.SubmitBtn);
            browser.WaitForElement(Bys.MainproPage.NotificationFormLbl,TimeSpan.FromSeconds(60),
                ElementCriteria.IsVisible);
            Assert.AreEqual("Credit amount should be less than or equal to "+ MaximumCreditOfBothRoles + " credits " +
                "for this activity.", EAP.NotificationLbl.Text);
            EAP.ClickAndWaitBasePage(EAP.NotificationFormXBtn);

            /// 7. Select Assessed Role and Submit the activity with Credits less than Max value
            /// and verify it shows in the Activity Lists page and Credits appeared on 
            /// Cycle Credit Table
            Help.ClearTextBox(EADP.CreditsRequestedOrClaimedTxt);
            EADP.CreditsRequestedOrClaimedTxt.SendKeys("0.5");
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
            DP.ClickAndWaitBasePage(DP.DashboardTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: "Certified", colName: "Applied", cellTextExpected: "0.5");

        }

        [Test]
        [Description("Given I enter the CPD activity of Assessment type Cert+ Session Activity" +
             " which has configured with Assessed Only Role, I should see activity form displayed with" +
            "default selection of Assessed Role and should see only Assessed Max Credit displayed," +
            " When I submit the form, credits should reflected in Credit Table")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]

        public void AssessmentTypeCertSessionActAssessedOnlyRole()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    "because we dont have Cert+ Test Program in Prod and we could not create because " +
                    "Cert+ Program's Require Various Roles Approval Process which Trigger" +
                    "Mails to Client");
            }
            string AssessedMaxCredits = AppSettings.Config["CertAssessmentAssessedOnlyRoleSessionMaxCredit"].ToString();
            string AssessedMaxCreditsTblValue = AppSettings.Config["CertAssessmentAssessedOnlyRoleSessionMaxCreditTblValue"].ToString();
           
            /// 1. Create a Default cycle user
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);

            ///2. Search an Cert+ Assessment type Session Activity configured with Only Assessed Role
            EnterACPDActivityPage EAP = DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(AppSettings.Config["CertAssessmentAssessedOnlyRoleSessionIDString"]);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(Keys.Tab);
            // Auto comment: For some reason, we have to wait here or else after clicking the Continue button, the 
            // application returns the wrong session warning label text
            Thread.Sleep(500);
            EnterACPDActivityDetailsPage EADP = EAP.ClickAndWait(EAP.DoYouKnowYourSessionIDContinueBtn);
            
            ///3. Verify that Assessed Role is selected by default
            Assert.IsTrue(Browser.Exists(Bys.EnterACPDActivityDetailsPage.PleaseIndicateYourRoleInThisAssessmentAssessedRdo,
                ElementCriteria.HasAttribute("disabled"), ElementCriteria.IsSelected));

            /// 4. Verify that only Assessed Max Credit is displayed
            Assert.IsFalse(Browser.Exists(Bys.EnterACPDActivityDetailsPage.AssessorMaxCreditsTxt, ElementCriteria.IsVisible));
            Assert.IsTrue(Browser.Exists(Bys.EnterACPDActivityDetailsPage.AssessedMaxCreditsTxt,
                ElementCriteria.HasAttribute("disabled"), ElementCriteria.AttributeValue("value", AssessedMaxCredits)));

            /// 5. Fill the fields that are required and not filled in yet. Keep the activity title 
            /// as it's default prepopulated value (with session ID) and Submit the activity . verify 
            /// it shows in the Activity Lists page and Credits appeared on  Cycle Credit Table
            Activity Act = EADP.FillActivityForm(actStartDt: currentDatetime, actCompletionDt: currentDatetime,
                keepExistingActTitle: true);
            Help.ClearTextBox(EADP.CreditsRequestedOrClaimedTxt);
            EADP.CreditsRequestedOrClaimedTxt.SendKeys(AssessedMaxCredits);
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);
            DP.ClickAndWaitBasePage(DP.DashboardTab);
           
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: "Certified", colName: "Applied", cellTextExpected: AssessedMaxCreditsTblValue);
        }

        [Test]
        [Description("Given I enter the CPD activity of Assessment type Cert+ Session Activity" +
              " which has configured with Assessor Only Role, I should see activity form displayed with" +
             "default selection of Assessor Role and should see only Assessor Max Credit displayed," +
             " When I submit the form, credits should reflected in Credit Table")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]

        public void AssessmentTypeCertSessionActAssessorOnlyRole()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    "because we dont have Cert+ Test Program in Prod and we could not create because " +
                    "Cert+ Program's Require Various Roles Approval Process which Trigger" +
                    "Mails to Client");
            }
            string AssessorMaxCredits = AppSettings.Config["CertAssessmentAssessorOnlyRoleSessionMaxCredit"].ToString();
           string AssessorMaxCreditsTblVal = AppSettings.Config["CertAssessmentAssessorOnlyRoleSessionMaxCreditTblValue"].ToString();
           
            ///1. Create a Default cycle user
            UserModel user = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login(user.Username, isNewUser: true);

            ///2. Search an Cert + Assessment type Session Activity configured with Only Assessor Role
            EnterACPDActivityPage EAP = DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(AppSettings.Config["CertAssessmentAssessorOnlyRoleSessionIDString"]);
            EAP.DoYouKnowYourSessionIDSearchTxt.SendKeys(Keys.Tab);
            // Auto comment: For some reason, we have to wait here or else after clicking the Continue button, the 
            // application returns the wrong session warning label text
            Thread.Sleep(500);
            EnterACPDActivityDetailsPage EADP = EAP.ClickAndWait(EAP.DoYouKnowYourSessionIDContinueBtn);

            ///3.Verify that Assessor Role is selected by default
            Assert.IsTrue(Browser.Exists(Bys.EnterACPDActivityDetailsPage.PleaseIndicateYourRoleInThisAssessmentAssessorRdo,
                ElementCriteria.HasAttribute("disabled"), ElementCriteria.IsSelected));
            
            ///4. Verify that only Assessor Max Credit is displayed
            Assert.IsFalse(Browser.Exists(Bys.EnterACPDActivityDetailsPage.AssessedMaxCreditsTxt, 
                ElementCriteria.IsVisible));
            Assert.IsTrue(Browser.Exists(Bys.EnterACPDActivityDetailsPage.AssessorMaxCreditsTxt,
                ElementCriteria.HasAttribute("disabled"), ElementCriteria.AttributeValue("value", AssessorMaxCredits)));

            ///5. Fill the fields that are required and not filled in yet.Keep the activity title as 
            ///it's default prepopulated value (with session ID) and Submit the activity . 
            ///verify it shows in the Activity Lists page and Credits appeared on  Cycle Credit Table
            Activity Act = EADP.FillActivityForm(actStartDt: currentDatetime, actCompletionDt: currentDatetime,
                keepExistingActTitle: true);
            Help.ClearTextBox(EADP.CreditsRequestedOrClaimedTxt);
            EADP.CreditsRequestedOrClaimedTxt.SendKeys(AssessorMaxCredits);
            EADP.ClickAndWait(EADP.SubmitBtn);
            EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
            browser.WaitForElement(Bys.CPDActivitiesListPage.ActTbl, TimeSpan.FromSeconds(30), ElementCriteria.IsVisible);
            Help.VerifyGridContainsRecord(browser, Const_Mainpro.Table.CPDActitivitesListTabAct, Act.Title);
            DP.ClickAndWaitBasePage(DP.DashboardTab);
            Help.VerifyCellTextMatches(browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                   rowName: "Certified", colName: "Applied", cellTextExpected: AssessorMaxCreditsTblVal);

        }




        #endregion Tests
    }


}