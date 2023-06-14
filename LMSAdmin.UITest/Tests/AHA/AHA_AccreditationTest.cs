using Browser.Core.Framework;
using LMS.Data;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using LMSAdmin.AppFramework;
using System;
using System.Threading;

namespace LMSAdmin_AHA.UITest
{
    //[NonParallelizable]
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]

    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]

    [TestFixture]
    public class AHA_AccreditationTest : TestBase_AHA
    {
        #region Constructors
        public AHA_AccreditationTest(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public AHA_AccreditationTest(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion


        #region Tests

        [Test]
         [Description("The test will login Old CME360 and searches for the given test activity and upon selecting the Accreditation node ," +
            " the page will be navigating to New CME360, When user creates accreditation and scenario under the accreditation on Accrediation page," +
            "Then verifies that those are added and displayed successfully, Goto Award page and Add award by choosing awardtemplate,scenario " +
            "and save sucess, And User can Edit the award and save sucess, And User can Delete the award and verify the record deleted successfully" +
            "When user deletes the created scenario and accreditation, " +
            "Then verifies that those are deleted and not displayed on UI")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void Tests_AccreditationAndAddEditDeleteAward()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user1 to login
            MyDashboardPage MDP = LP.Login(Autotest_user1, password);             
            
            string activityName = Autotest_Activity1; //Activity Name -> AutomationTest Activity1_DoNotUse_<<BrowserName>>

            /// The accreditation body which has to be created /added to the activity
            string accreditationBody = string.Format("DonotDelete_Autotest1 > {0}", CommonAccreditationbody);

            /// 3. Search for the given Autotest_Activity1 activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            /// 4. Select Accreditation node 
            /// 5. The page will be redirected to New LMSadmin UI and check whether the accreditation body already there if yes, then delete it
            ActAccreditationPage ACCPage = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);
            if (!Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                    ACCPage.DeleteAccreditation(accreditationBody);
            }
            
            /// 6.Click "Add accreditation", Fill all fields and save 
            /// 7.Created Accreditation should be diplayed as a new table                        
            ACCPage.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            ACCPage.RefreshPage(true);

            /// 8. Click "Add Scenario" to the newly added accreditation, Enter random scenario name, current date, static credits and save the scenario
            /// 9. Saved Scenario should be displayed under the newly added accreditation 
            string ScenarioName = ACCPage.AddScenario(accreditationBody, accrClaimCreditEnabled: false);
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);

            /// 10. Click on "Award" node , Award fireball page should be loaded
            ActAwardsPage AWDP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Awards);

            /// 11. cilck "Add award" , AddAward Page should be displayed and fill all the fields,select "AutomationTest_Award Template - Do Not Edit",
 //Select "Landscape" type , choose the newly created scenario and save the award
            string AwardName = AWDP.AddAward(templateName: "AutomationTest_Award Template - Do Not Edit", OrientationType: "Landscape");

            /// 12. verify the created Award and selected scenario is displayed in awards table list 
            Browser.WaitForElement(Bys.ActAwardsPage.AwardsTbl, ElementCriteria.IsVisible);
            Assert.IsTrue(ElemGet.Grid_CellTextFound(Browser, AWDP.AwardsTbl, 0, "td", AwardName),
                string.Format("Added award [ {0} ] not shown in awardlist ", AwardName));
            StringAssert.Contains(ScenarioName,
                ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, AWDP.AwardsTbl, Bys.ActAwardsPage.AwardsTblFirstRow, AwardName, "td", 2),
                string.Format("Selected scenario {0} not added to award", ScenarioName));

            /// 13. Click on created award for editing,  update the name field and save it
            AWDP = AWDP.ClickAndWaitBasePage(AWDP.Steps_AwardLbl);
            string New_AwardName = AWDP.EditAward(AwardName);

            /// 14. verify the updated value shows in award and listed in table
            Browser.WaitForElement(Bys.ActAwardsPage.AwardsTbl, ElementCriteria.IsVisible);
            Assert.IsTrue(ElemGet.Grid_CellTextFound(Browser, AWDP.AwardsTbl, 0, "td", New_AwardName),
                string.Format("Edited award [ {0} ] not saved in awardlist ", New_AwardName));

            /// 15. Click Delete icon and verify the award is deleted from awards table list 
            AWDP = AWDP.ClickAndWaitBasePage(AWDP.Steps_AwardLbl);
            AWDP.DeleteAward(New_AwardName);
            Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.ActAwardsPage.AwardsTbl, ElementCriteria.IsVisible);
            Assert.IsFalse(ElemGet.Grid_CellTextFound(Browser, AWDP.AwardsTbl, 0, "td", New_AwardName),
               string.Format("Award [ {0} ] not deleted from awardlist ", New_AwardName));

            /// 16. Go back to Accreditation tab , and delete the created accreditation and scenario
            ACCPage = AWDP.ClickAndWaitBasePage(AWDP.Steps_AccreditationLbl);

            ///  17. Find the unique added Scenario associated with newly added accreditation, Clicking Delete option, the test will verify the OK button &text message on the confirmation pop up. Confirm.
            ///  18. Ensure the newly added Scenario is deleted
            ACCPage.DeleteScenario(accreditationBody,ScenarioName);

            /// 19.Find the unique added accreditation, Clicking Delete option, the test will verify the OK button & text message on the confirmation pop up. Confirm.
            /// 20. Ensure the newly added accreditation is deleted            
            ACCPage.DeleteAccreditation(accreditationBody);
                                   
            /// 21. Click "back to activity" button and page will be redirected to Old CME360 and logoff.
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }

        //    [Test]
        [Description("Verifies that on accreditation page , user can create a scenario with adding EligibleProfession, EligibleSpeciality and EligibleCountry successfully")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AccreditationManagement_CountrySpeciality()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user1 to login
            MyDashboardPage MDP = LP.Login("cmeca_test3", password);

            string activityName = "AutoTest_Activity1_CountrySpecialty";

            /// The accreditation body 
            string accreditationBody = "Non-Accredited";

            /// 3. Search for the given test activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            /// 4. Click on Accreditation node 
            /// 5. The page will be redirected to New LMSadmin UI and check whether the accreditation body already there if no, then add it
            ActAccreditationPage ACCPage = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);
            if (Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                ACCPage.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            }
            else if (!ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
            {
                ACCPage.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            }
            ACCPage.RefreshPage(true);
            /// 6. Click "Add Scenario" to the newly added accreditation, Enter random scenario name, current date, static credits ,
            ///  eligibleprofession, eligiblespeciality , eligiblecountry and save the scenario            
            string ScenarioName = ACCPage.AddScenario(accreditationBody, eligibleProfession: "Physician",
                eligibleSpecialities: "Dental Surgery", eligibleCountries: "Belgium");

            ///  7. Find the unique added Scenario associated with accreditation, Clicking Delete option, the test will verify the OK button &text message on the confirmation pop up. Confirm.
            ///  8. Ensure the Scenario is deleted
            ACCPage.DeleteScenario(accreditationBody, ScenarioName);

            /// 9. Click "back to activity" button and page will be redirected to Old CME360 and logoff.
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }

        [Test]
        [Description("Verifies that on accreditation page , user can edit an accreditation, change the value , " +
            "save the same and check that the updated values are displayed on accreditation table")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AccreditationManagement_EditAccreditation()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user1 to login
            MyDashboardPage MDP = LP.Login(Autotest_user1, password);

            string activityName = Autotest_Activity1;

            /// The accreditation body 
            string accreditationBody = string.Format("DonotDelete_Autotest2 > {0}", CommonAccreditationbody);
            /// The primaryProvider value  -- Before Edit 
            string primaryProvider_BeforeEdit = "Empty";
            /// The primaryProvider value  -- After Edit
            string primaryProvider_AfterEdit = siteName;

            /// 3. Search for the given test activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);

            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            /// 4. Click on Accreditation node 
            /// 5. The page will be redirected to New LMSadmin UI 
            ActAccreditationPage ACCPage = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 6. Search the accreditation body, if it's already there then delete and add newly , if not already there then add it
            if (!Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                    ACCPage.DeleteAccreditation(accreditationBody);
            }
            ACCPage.AddAccreditation(accreditationBody, primaryProvider_BeforeEdit, claimCreditEnabled: false);
            ACCPage.RefreshPage(true);
            IWebElement accrTable = Browser.FindElement(By.XPath(string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::table",
              accreditationBody)));

            IWebElement parentRow = ElemGet.Grid_GetRowByRowName(Browser, accrTable, Bys.ActAccreditationPage.AccreditationDetailsTblRow,
                accreditationBody, "td");

            /// 7. Verify the accreditation details are displayed once added 
            StringAssert.IsMatch("none selected",
                ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, accrTable, Bys.ActAccreditationPage.AccreditationDetailsTblRow, accreditationBody, "td", 1),
             string.Format("[ {0} ] - is not displayed as Primary Provider for accreditationBody [ {1} ]", "none selected", accreditationBody));

            /// 8. Click pencil edit icon , Edit Accreditation dialog should be displayed
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, parentRow, "button", 0);
            Thread.Sleep(TimeSpan.FromSeconds(15));
            Browser.WaitForElement(Bys.ActAccreditationPage.EditAccreditationFormEditAccreditationLbl, ElementCriteria.IsVisible);

            /// 9. AccreditationBodyType field should be displayed as disabled 
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AccreditationBodyandTypeSelElemBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The AccreditationBodyType field should not be enabled On Editing the Accreditation ");

            /// 10. Change the value of primary provider field             
            Browser.WaitForElement(Bys.ActAccreditationPage.EditAccreditationFormPrimaryProviderSelElemBtn, ElementCriteria.IsEnabled);
            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, ACCPage.EditAccreditationFormPrimaryProviderSelElemBtn, primaryProvider_AfterEdit);
            Thread.Sleep(TimeSpan.FromSeconds(15));

            /// 11. Click on Save Accreditation button and get back to the Accreditation Page 
            ACCPage.ClickAndWait(ACCPage.EditAccreditationFormSaveAccreditationBtn);

            IWebElement accrTableAfterEdit = Browser.FindElement(By.XPath(string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::table",
              accreditationBody)));

            /// 12. Verify the updated primary provider value is displayed 
            StringAssert.IsMatch(primaryProvider_AfterEdit,
                ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, accrTableAfterEdit, Bys.ActAccreditationPage.AccreditationDetailsTblRow, accreditationBody, "td", 1),
             string.Format("After Editing Accreditation , [ {0} ] - is not displayed as Primary Provider for accreditationBody [ {1} ]", primaryProvider_AfterEdit, accreditationBody));

            /// 13. Verify the "none selected" is displayed on additional providers field 
            StringAssert.IsMatch("none selected",
                ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, accrTableAfterEdit, Bys.ActAccreditationPage.AccreditationDetailsTblRow, accreditationBody, "td", 2),
             string.Format("After Editing Accreditation , [ {0} ] - is not displayed as Additional Provider for accreditationBody [ {1} ]", "none selected", accreditationBody));

            /// 14. Click "back to activity" button and page will be redirected to Old CME360 and logoff.
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }

        [Test]
        [Description("Verifies that on accreditation page , user can edit the scenario, change the value , " +
            "save the same and check that the updated values are displayed under scenario section on accreditation table ")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AccreditationManagement_EditScenario()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user1 to login
            MyDashboardPage MDP = LP.Login(Autotest_user1, password);

            string activityName = Autotest_Activity1;

            /// The accreditation body 
            string accreditationBody = string.Format("DonotDelete_Autotest3 > {0}", CommonAccreditationbody);

            /// 3. Search for the given test activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            /// 4. Click on Accreditation node 
            /// 5. The page will be redirected to New LMSadmin UI 
            ActAccreditationPage ACCPage = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 6. Search the accreditation body, if it's already there then delete and add newly , if not already there then add it
            if (!Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                    ACCPage.DeleteAccreditation(accreditationBody);
            }
            ACCPage.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            ACCPage.RefreshPage(true);

            /// 7. Add Scenario to the given accreditation body            
            string scenarioNameBeforeEdit = ACCPage.AddScenario(accreditationBody);
            ACCPage.RefreshPage(true);

            string scenarioTblXpath = string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'accreditationGridChart')]/following-sibling::div[contains(@class,'scenarioGridChart')]//table", accreditationBody);
            string scenarioTblRowXpath = string.Format("{0}/tbody/tr", scenarioTblXpath);

            IWebElement specifiedAccreditationScenarioTbl = Browser.FindElement(By.XPath(scenarioTblXpath));

            IWebElement scenarioRow = ElemGet.Grid_GetRowByRowName(Browser, specifiedAccreditationScenarioTbl, By.XPath(scenarioTblRowXpath), scenarioNameBeforeEdit, "td");

            /// 8. Click pencil edit icon on the added scenario , Edit Scenario dialog should be displayed
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, scenarioRow, "button", 0);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Browser.WaitForElement(Bys.ActAccreditationPage.EditScenarioFormEditScenarioLbl, ElementCriteria.IsVisible);

            string scenarioNameAfterEdit = "TestScenario_Edit";

            /// 9. Update the Scenario Name , Release Date, Expiration Date, FixedCredit, FixedCreditUnit, EquivalentCredit, EquivalentCreditUnit, EligibleProfession fields 
            ElemSet.TextBox_EnterText(Browser, ACCPage.ScenarionameTxt, true, scenarioNameAfterEdit);
            ElemSet.TextBox_EnterText(Browser, ACCPage.ReleaseDateTxt, true, "1/1/2019");
            ElemSet.TextBox_EnterText(Browser, ACCPage.ExpirationDateTxt, true, "1/1/2020");
            ElemSet.TextBox_EnterText(Browser, ACCPage.FixedCreditTxt, true, "10");
            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, ACCPage.FixedCreditUnitSelElemBtn, "Hours");
            ElemSet.TextBox_EnterText(Browser, ACCPage.EquivalentCreditTxt, true, "15");
            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, ACCPage.EquivalentCreditUnitSelElemBtn, "Units");
            ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ACCPage.EligibleProfessionElemBtn, "Administrator");

            /// 10. Click on Save Scenario button
            ACCPage.ClickAndWait(ACCPage.EditScenarioFormSaveScenarioBtn);

            specifiedAccreditationScenarioTbl = Browser.FindElement(By.XPath(scenarioTblXpath));

            /// 11. Verify the updated fields are displayed on the edited scenario     
            StringAssert.IsMatch("01/01/2019 - 01/01/2020",
                ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, specifiedAccreditationScenarioTbl, By.XPath(scenarioTblRowXpath), scenarioNameAfterEdit, "td", 1),
             string.Format("After Editing Scenario , [ {0} ] - is not updated to [ {1} ] Scenario - Date field ", "01/01/2019 - 01/01/2020", scenarioNameAfterEdit));

            StringAssert.StartsWith(" (2) ", ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, specifiedAccreditationScenarioTbl, By.XPath(scenarioTblRowXpath), scenarioNameAfterEdit, "td", 2),
             string.Format("After Editing Scenario , [ {0} ] - is not updated to [ {1} ] Scenario - Eligibile Professions field ", "Administrator", scenarioNameAfterEdit));

            StringAssert.IsMatch("10.00 Hours, 15.00 Units",
                ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, specifiedAccreditationScenarioTbl, By.XPath(scenarioTblRowXpath), scenarioNameAfterEdit, "td", 3),
             string.Format("After Editing Scenario , [ {0} ] - is not updated to [ {1} ] Scenario - Credits field ", "10.00 Hours, 15.00 Units", scenarioNameAfterEdit));

            /// 12. Click "Back to activity" button and page will be redirected to Old CME360 and logoff.
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }

        [Test]
        [Description(" Verifies that for Live Activity with Session type, User can add or delete a scenario or accreditation and" +
            "validates the cascading flow  between Parent and Session Activity working correctly")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AccreditationManagement_LiveActivityWithSessionCases()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user1 to login
            MyDashboardPage MDP = LP.Login(Autotest_user1, password);

            string activityName = Autotest_LiveActivity1;

            /// The accreditation body 
            string accreditationBody = string.Format("DonotDelete_Autotest4 > {0}", CommonAccreditationbody);

            string scenarioNameRegistered = "test test";

            /// 3. Search for the given test activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);

            By SessionActivityBy = By.XPath("//span[contains(@class, 'TreeNode') and text()='Accreditations']/ancestor::div[2]/div/span[@class= 'TreeNode' and text()='Session 1']");
            By MainActivityBy = By.XPath("(//span[contains(@class, 'TreeNode') and text()='Accreditations']/ancestor::div[contains(@style,'block')]/preceding-sibling::div)[last()-1]/span");
            By SessionActivityAccreditationBy = By.XPath("//span[text()='Session 1']/parent::div/following-sibling::div/div/span[text()='Accreditations']");
            By MainActivityAccreditationBy = By.XPath("//span[text()='Session 1']/parent::div/following-sibling::div/span[text()='Accreditations']");

            /// 4. Click on Parent Activity's Accreditation node and page will be navigated to LMSAdmin UI of Session Activity
            Browser.FindElement(MainActivityAccreditationBy).Click();
            ActAccreditationPage ACCPage = new ActAccreditationPage(Browser);
            ACCPage.WaitForInitialize();

            /// 5. Click to Delete a scenario in Parent which was registered to participant already , then Confirmation prompt for deletion is displayed 
            string specifiedRegisteredScenarioRowXpath = string.Format("//td[contains(@class,'column-scenarios') and text()='{0}']/following-sibling::td//button[@aria-label='click to delete']", scenarioNameRegistered);
            IWebElement DeleteScenarioBtn = Browser.FindElement(By.XPath(specifiedRegisteredScenarioRowXpath));
            DeleteScenarioBtn.ClickJS(Browser);

            /// 6. Click ok to confirm the deletion , verify that error shown as "This Scenario cannot be deleted because Participant data has been collected"
            Browser.WaitForElement(Bys.ActAccreditationPage.ScenarioDeleteFormOkBtn, ElementCriteria.IsVisible);
            ACCPage.ScenarioDeleteFormOkBtn.Click();

            Browser.WaitForElement(Bys.Page.AlertNotificationIconMsg, ElementCriteria.IsVisible);
            StringAssert.Contains("This Scenario cannot be deleted because Participant data has been collected", ACCPage.AlertNotificationIconMsg.Text,
                string.Format("[ {0} ] - Notification thrown upon deleting the Participant registered Scenario [ {1} ] In Parent Activity", ACCPage.AlertNotificationIconMsg.Text, scenarioNameRegistered));

            /// 7. Add New accreditation body and add two scenarios under to it 
            if (!Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                    ACCPage.DeleteAccreditation(accreditationBody);
            }
            ACCPage.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            ACCPage.RefreshPage(true);

            string scenarioNameFromParent = ACCPage.AddScenario(accreditationBody);
            ACCPage.RefreshPage(true);

            string scenario2NameFromParent = ACCPage.AddScenario(accreditationBody);
            ACCPage.RefreshPage(true);

            /// 8. Delete one scenario from Parent and go back to main page
            ACCPage.DeleteScenario(accreditationBody, scenario2NameFromParent);
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            Thread.Sleep(TimeSpan.FromSeconds(10));


            /// 9. Click Accreditation on Session Activity with "Under Construction" status, Navigate to LMSAdmin UI of Session Activity
            Browser.FindElement(SessionActivityBy).ClickJS(Browser);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            AMP.WaitForInitialize();
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);
            Browser.FindElement(SessionActivityAccreditationBy).Click();
            ACCPage.WaitForInitialize();

            /// 10. Verify that scenario deleted from Parent, not displayed in Session activity
            string specifiedAccreditationScenarioTblXpath = string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'accreditationGridChart')]/following-sibling::div[contains(@class,'scenarioGridChart')]", accreditationBody);
            IWebElement specifiedAccreditationScenarioTbl = Browser.FindElement(By.XPath(specifiedAccreditationScenarioTblXpath));
            Assert.False(ElemGet_LMSAdmin.Grid_CellTextFound(Browser, specifiedAccreditationScenarioTbl, Bys.ActAccreditationPage.ScenarioDetailsTblScenNameColumn, scenario2NameFromParent),
                string.Format(" Deleted Scenario [{0}] From Parent should not be available to Session Activity ", scenario2NameFromParent));

            /// 11. Verify that AddAccreditation button is not enabled in Session activity with "Under Construction"
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddAccreditationBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                 "The AddAccreditation button should not be enabled in Session Activity");

            /// 12. Verify that Accreditation created in Parent, displayed in Session activity
            Assert.True(ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody),
            string.Format("Accreditation [ {0} ] Created in Parent should cascaded/available to Session Activity ", accreditationBody));

            /// 13. In Session activity, Click to Delete a scenario  which was registered to participant already , then Confirmation prompt for deletion is displayed 
            DeleteScenarioBtn = Browser.FindElement(By.XPath(specifiedRegisteredScenarioRowXpath));
            DeleteScenarioBtn.ClickJS(Browser);

            /// 14. Click ok to confirm the deletion , verify that error shown as "This Scenario cannot be deleted because Participant data has been collected"
            Browser.WaitForElement(Bys.ActAccreditationPage.ScenarioDeleteFormOkBtn, ElementCriteria.IsVisible);
            ACCPage.ScenarioDeleteFormOkBtn.Click();
            Browser.WaitForElement(Bys.Page.AlertNotificationIconMsg, ElementCriteria.IsVisible);
            StringAssert.Contains("This Scenario cannot be deleted because Participant data has been collected", ACCPage.AlertNotificationIconMsg.Text,
                string.Format("[ {0} ] - Notification thrown upon deleting the Participant registered Scenario [ {1} ] in Session Activity", ACCPage.AlertNotificationIconMsg.Text, scenarioNameRegistered));

            /// 15. Delete a scenario from Session activity to verify in Parent activity
            ACCPage.DeleteScenario(accreditationBody, scenarioNameFromParent);

            /// 16. Add a scenario to Session activity to verify in Parent activity
            string scenarioNameFromSession = ACCPage.AddScenario(accreditationBody);

            /// 17. Go back to Parent's Activity and navigate new LMSadminUI 
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Browser.FindElement(MainActivityBy).ClickJS(Browser);
            AMP.WaitForInitialize();
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Browser.FindElement(MainActivityAccreditationBy).Click();
            ACCPage.WaitForInitialize();


            /// 18. Verify that scenario created in session activity , is not displayed in Parent activity
            specifiedAccreditationScenarioTbl = Browser.FindElement(By.XPath(specifiedAccreditationScenarioTblXpath));
            Assert.False(ElemGet_LMSAdmin.Grid_CellTextFound(Browser, specifiedAccreditationScenarioTbl, Bys.ActAccreditationPage.ScenarioDetailsTblScenNameColumn, scenarioNameFromSession),
                string.Format(" Scenario [{0}] Created in Session should not be available to Parent Activity ", scenarioNameFromSession));

            /// 19. Verify that scenario deleted in session activity , is not deleted from Parent activity
            Assert.True(ElemGet_LMSAdmin.Grid_CellTextFound(Browser, specifiedAccreditationScenarioTbl, Bys.ActAccreditationPage.ScenarioDetailsTblScenNameColumn, scenarioNameFromParent),
                string.Format(" Deleted Scenario [{0}] From Session should be available to Parent Activity ", scenarioNameFromParent));

            /// 20. Verify that 'Add accreditation' button is disabled once all accrediting body's are added up
            if (Browser.Exists(Bys.ActAccreditationPage.AddAccreditationBtn, ElementCriteria.AttributeValueContains("class", "disabled"))) { }
            else
            {
                ACCPage.AddAllAccreditation();
                Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddAccreditationBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                    "The AddAccreditation button should not be enabled in Parent Activity once all accrediting body's are added up ");
            }

            /// 21. Delete an accreditation from Parent's Activity to verify in Session activity
            ACCPage.DeleteAccreditation(accreditationBody);
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            Thread.Sleep(TimeSpan.FromSeconds(10));

            /// 22. Go back to Session activity and change the status to "Construction complete"
            Browser.FindElement(SessionActivityBy).ClickJS(Browser);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            AMP.ChangeActivityStage(Constants.ActStage.ConstructionComplete);

            /// 23. Click Accreditation Node , Navigate to LMSAdmin UI of Session Activity
            Browser.FindElement(SessionActivityAccreditationBy).Click();
            ACCPage.WaitForInitialize();

            /// 24. Verify that Add Accreditation button is disabled when the status is "Construction complete"
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddAccreditationBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The AddAccreditation button should not be enabled when Session Activity with status of 'ConstructionComplete'");

            /// 25. Verify that deleted accreditation from parent activity is not displayed to session activity
            Assert.False(ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody),
            string.Format("Deleted Accreditation [ {0} ] in Parent should be removed from Session Activity as well", accreditationBody));

            /// 26. Click "Back to activity" button and page will be redirected to Old CME360 and logoff
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }



    }

    #endregion Tests

}


