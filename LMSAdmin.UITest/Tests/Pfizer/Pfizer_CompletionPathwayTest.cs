using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using LMSAdmin.AppFramework;
using System;
using System.Threading;

namespace LMSAdmin_Pfizer.UITest
{
    //[NonParallelizable]
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]

    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]

    [TestFixture]
    public class Pfizer_CompletionPathwayTest : TestBase_Pfizer
    {
        #region Constructors
        public Pfizer_CompletionPathwayTest(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Pfizer_CompletionPathwayTest(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests
        [Test]
        [Description(" Tests that the user can create a new scenario from Accreditation page and navigate to Scenario settings Tab on Completion Pathway page " +
            " Then the scenario should be appeared with configured assessments, and When user filled the specific Assessment's Scenario settings fields and Save ," +            
            " Then the fields values should be saved and displayed with correct values, When user performes CopytoAll and Display All,Then the fields values should be copied to " +
            " other Scenario and all assessments should be checked as Displayed" )]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void CompletionPathwayManagement_ScenarioSettings()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user2 to login
            MyDashboardPage MDP = LP.Login(Autotest_user2, password);

            string activityName = Autotest_Activity1;

            /// 3. Search for the given "Autotest_Activity1" activity
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            /// 4. Click on Accreditation node
            /// 5. The page will be redirected to New LMSadmin UI
            ActAccreditationPage ACCP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 6. Search for the given Accreditation ; if not found in UI , then Add the accreditation
            string accreditationBody = string.Format("DonotDelete_Autotest5 > {0}", CommonAccreditationbody);
            if (Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                ACCP.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            }
            else if (!ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCP.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
            {
                ACCP.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            }
            ACCP.RefreshPage(true);
            /// 7. Add two Scenarios to the given accreditation body
            string scenarioName = ACCP.AddScenario(accreditationBody);
            ACCP.RefreshPage(true);
            string scenario2Name = ACCP.AddScenario(accreditationBody);

            /// 8. Click on CompletionPathway step and CompletionPathway page should be loaded
            ActCompletionPathwayPage ACPLP = ACCP.ClickAndWaitBasePage(ACCP.Steps_CompletionPathwayLbl);

            /// 9. Click on ScenarioSettingsTab to load ScenarioSettings page
            ACPLP.ClickAndWait(ACPLP.ScenarioSettingsTab);

            /// 10. Search for the Created Scenario section 
            IWebElement scenarioRow = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, ACPLP.ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow,
            scenarioName, "td", accreditationBody, "td");

            /// 11. Verify the given assessments are displayed for the newly created scenario section
            Assert.True(ACPLP.GetAssessmentRowPresentInScenario(ACPLP.ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment").Displayed, "AutoTest_DonotDelete_PreTest1 assessment is displayed");
            Assert.True(ACPLP.GetAssessmentRowPresentInScenario(ACPLP.ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow,scenarioName, accreditationBody, "AutoTest_DonotDelete_PostTest1", "Post-Test Assessment").Displayed, "AutoTest_DonotDelete_PostTest1 assessment is displayed");
            Assert.True(ACPLP.GetAssessmentRowPresentInScenario(ACPLP.ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow,scenarioName, accreditationBody, "AutoTest_DonotDelete_EvalTest1", "Evaluation").Displayed, "AutoTest_DonotDelete_EvalTest1 assessment is displayed");

            /// 12. Click icon for Collapse present in Created Scenario Title Row and verify that assessments not shown
            ElemSet_LMSAdmin.Grid_ExpandOrCollapse(scenarioRow, "collapse", "button");
            IWebElement Collapsed_assessmentRow = ACPLP.GetAssessmentRowPresentInScenario(ACPLP.ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment");
            Assert.False(Collapsed_assessmentRow.Displayed,"Assessment not displayed/visible after collapsing the scenario section");

            /// 13. Click icon for Expand present in Created Scenario Title Row and verify that assessments are shown
            ElemSet_LMSAdmin.Grid_ExpandOrCollapse(scenarioRow, "expand", "button");
            IWebElement Expanded_assessmentRow = ACPLP.GetAssessmentRowPresentInScenario(ACPLP.ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment");
            Assert.True(Expanded_assessmentRow.Displayed, "Assessment is displayed/visible after expanding the scenario section");
            
            /// 14. Fill the fields for the given assesment and Save the scenario settings
            ACPLP.FillAndSaveAsessmentScenarioSettings( scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment",
                numOfAttempts: "5", numOfGradedQuesToPass: 1, assessmentTobeDisplayed: true, orderOftheAssessment: "2", completionRequired: "Yes");
            StringAssert.Contains("success", ACPLP.AlertNotificationIconMsg.Text,
                string.Format("[ {0} ] - Notification thrown upon saving Sceanrio Settings for Scenario [ {1} ]", ACPLP.AlertNotificationIconMsg.Text, scenarioName));
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(2));

            /// 15. Click CopyToAll to copy the given assessment settings to other scenarios
            scenarioRow = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, ACPLP.ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow,
            scenarioName, "td", accreditationBody, "td");
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, scenarioRow, "div[@title = 'Copy To All']", 0);
            Browser.Exists(Bys.Page.ConfirmtationPopUpMsg, ElementCriteria.TextContains("Are you sure you want to copy these settings to all other scenarios?"));                       
            ACPLP.confirmMsgPopupOkBtn.Click(Browser);
            Browser.Exists(Bys.Page.AlertNotificationIconMsg, ElementCriteria.TextContains("success"));          
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(2));

            /// 16. Verify that the saved values are shown correctly in the assessment row and copied settings are displayed correctly in another sceanrio assessment
            ACPLP.VerifyTheSavedAsessmentScenarioSettings(scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment",
               numOfAttempts: "5", numOfGradedQuesToPass: 1, assessmentTobeDisplayed: true, orderOftheAssessment: "2", completionRequired: "Yes");            
            ACPLP.VerifyTheSavedAsessmentScenarioSettings( scenario2Name, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment",
               numOfAttempts: "5", numOfGradedQuesToPass: 1, assessmentTobeDisplayed: true, orderOftheAssessment: "2", completionRequired: "Yes");

            /// 17. Click Display All button and verify that the assessments are checked as displayed in across scenarios
            ACPLP.AsessmentDisplayAllBtn.Click();
            Browser.Exists(Bys.Page.ConfirmtationPopUpMsg, ElementCriteria.TextContains("Are you sure, you want to display all assessments for all scenarios?"));
            ACPLP.confirmMsgPopupOkBtn.Click(Browser);
            Browser.Exists(Bys.Page.AlertNotificationIconMsg, ElementCriteria.TextContains("success"));
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(2));
            ACPLP.VerifyTheSavedAsessmentScenarioSettings(scenario2Name, accreditationBody, "AutoTest_DonotDelete_PostTest1", "Post-Test Assessment",
                assessmentTobeDisplayed: true);
            ACPLP.VerifyTheSavedAsessmentScenarioSettings(scenarioName, accreditationBody, "AutoTest_DonotDelete_PostTest1", "Post-Test Assessment",
                assessmentTobeDisplayed: true);
           
            /// 18.  Navigate Back To Accreditation Step and Delete the newly created Accreditation 
            ACCP = ACPLP.ClickAndWaitBasePage(ACPLP.Steps_AccreditationLbl);           
            if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCP.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                ACCP.DeleteAccreditation(accreditationBody);
            
            /// 19. Click "back to activity" button and page will be redirected to Old CME360 and logoff.
            ACCP.ClickAndWaitBasePage(ACCP.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);

        }


        [Test]
        [Description("Tests that the user can create a new scenario from Accreditation page and navigate to Delivery settings Tab on Completion Pathway page " +
            " Then the scenario should display the configured assessments with initial values, and When user filled the specific Assessment's Delivery settings fields and Save, and performed CopyToAll" +
            " Then the fields values should be saved and displayed with correct values for the specified assessment and should be copied to other scenario")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void CompletionPathwayManagement_DeliverySettings()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user2 to login
            MyDashboardPage MDP = LP.Login(Autotest_user2, password);

            string activityName = Autotest_Activity1;

            /// 3. Search for the given test activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            /// 4. Select Accreditation node 
            /// 5. The page will be redirected to New LMSadmin UI
            ActAccreditationPage ACCP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 6. Search for the given Accreditation ; if not found in UI , then Add the accreditation
            string accreditationBody = string.Format("DonotDelete_Autotest1 > {0}", CommonAccreditationbody);
            if (Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                ACCP.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            }
            else if (!ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCP.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
            {
                    ACCP.AddAccreditation(accreditationBody, claimCreditEnabled: false);            
            }
            ACCP.RefreshPage(true);
            /// 7. Add Scenario to the given accreditation body            
            string scenarioName = ACCP.AddScenario(accreditationBody);
            ACCP.RefreshPage(true);
            string scenario2Name = ACCP.AddScenario(accreditationBody);

            /// 8. Click on CompletionPathway step and CompletionPathway page should be loaded
            ActCompletionPathwayPage ACPLP = ACCP.ClickAndWaitBasePage(ACCP.Steps_CompletionPathwayLbl);

            /// 9. Click on DeliverySettingsTab to load DeliverySettings page
            ACPLP.ClickAndWait(ACPLP.DeliverySettingsTab);

            /// 10. Search for the Created Scenario section 
            IWebElement scenarioRow = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, ACPLP.DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow,
            scenarioName, "td", accreditationBody, "td");

            /// 11. Verify the given assessments are displayed for the newly created scenario section
            Assert.True(ACPLP.GetAssessmentRowPresentInScenario(ACPLP.DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment").Displayed);
            Assert.True(ACPLP.GetAssessmentRowPresentInScenario(ACPLP.DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow,scenarioName, accreditationBody, "AutoTest_DonotDelete_PostTest1", "Post-Test Assessment").Displayed);
            Assert.True(ACPLP.GetAssessmentRowPresentInScenario(ACPLP.DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow,scenarioName, accreditationBody, "EvalTest1", "Evaluation").Displayed);

            /// 12. Click icon for Collapse present in Created Scenario Title Row and verify that assessments not shown
            ElemSet_LMSAdmin.Grid_ExpandOrCollapse(scenarioRow, "collapse", "button");
            IWebElement Collapsed_assessmentRow = ACPLP.GetAssessmentRowPresentInScenario(ACPLP.DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment");
            Assert.False(Collapsed_assessmentRow.Displayed);

            /// 13. Click icon for Expand present in Created Scenario Title Row and verify that assessments are shown
            ElemSet_LMSAdmin.Grid_ExpandOrCollapse(scenarioRow, "expand", "button");
            IWebElement Expanded_assessmentRow = ACPLP.GetAssessmentRowPresentInScenario(ACPLP.DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment");
            Assert.True(Expanded_assessmentRow.Displayed);


            /// 14. Verify that initial configuration values for newly created scenario are shown correctly in the given assessment row 
            ACPLP.VerifyTheSavedAsessmentDeliverySettings( scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment",
                 actionType: "Participant Registration", timingType: "Immediate");


            /// 15. Fill the fields for the given assessment and Save the delivery settings and Verify the same is displayed             
            ACPLP.FillAndSaveAsessmentDeliverySettings( scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment",actionType: scenarioName,
                conditionType: "Passed", timingType: "After", numberoftimelimit: "2", units: "Weeks",notificationTemplate: "No Notice");
            Browser.Exists(Bys.Page.AlertNotificationIconMsg, ElementCriteria.TextContains("success"));           
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(3));
           
            ACPLP.VerifyTheSavedAsessmentDeliverySettings(scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment", actionType: scenarioName,
              conditionType: "Passed", timingType: "After", numberoftimelimit: "2", units: "Weeks", notificationTemplate: "No Notice");
            ACPLP.RefreshPage(true);
            ACPLP.ClickAndWait(ACPLP.DeliverySettingsTab);

            /// 16. Again change the assessment settings and Click CopyToAll to copy the given assessment settings to other scenarios           
            ACPLP.FillAndSaveAsessmentDeliverySettings(scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment", actionType: "Participant Registration",
            timingType: "After", numberoftimelimit: "2", units: "Weeks", notificationTemplate: "No Notice");            
            
            scenarioRow = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, ACPLP.DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow,
            scenarioName, "td", accreditationBody, "td");
            ElemSet_LMSAdmin.highLighterMethod(Browser, scenarioRow);
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, scenarioRow, "div[@title = 'Copy To All']", 0);
            Browser.Exists(Bys.Page.ConfirmtationPopUpMsg, ElementCriteria.TextContains("Are you sure you want to copy these settings to all other scenarios?"));
            ACPLP.confirmMsgPopupOkBtn.Click(Browser);
            Browser.Exists(Bys.Page.AlertNotificationIconMsg, ElementCriteria.TextContains("success"));
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(3));

            /// 17. Verify the copied values are shown correctly in the assessment row for other scenario           
            ACPLP.VerifyTheSavedAsessmentDeliverySettings(scenario2Name, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment", actionType: "Participant Registration",
                timingType: "After", numberoftimelimit: "2", units: "Weeks", notificationTemplate: "No Notice");

            /// 18. Navigate Back To Accreditation Step
            ACCP = ACPLP.ClickAndWaitBasePage(ACPLP.Steps_AccreditationLbl);

            /// 19. Delete the Accreditation 
            if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCP.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                ACCP.DeleteAccreditation(accreditationBody);

            /// 20. Click "back to activity" button and page will be redirected to Old CME360 and logoff.
            ACCP.ClickAndWaitBasePage(ACCP.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }


        #endregion Tests
    }

}