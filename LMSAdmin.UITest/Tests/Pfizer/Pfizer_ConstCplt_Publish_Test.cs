using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using LMSAdmin.AppFramework;
using System;
using System.Threading;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;

namespace LMSAdmin_Pfizer.UITest
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
    public class Pfizer_ConstCplt_Publish_Test : TestBase_Pfizer
    {
        #region Constructors
        public Pfizer_ConstCplt_Publish_Test(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Pfizer_ConstCplt_Publish_Test(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion



        #region Tests

        [Test]
        [Description("Verify on Award page that, User can add the template, preview template with image , html tab on customise awardtemplate " +
            "and Set construction complete the activity Then User can verify the Viewmode/disabled mode of all fields on view award page and" +
            "Preview the award, Set Publish the activity Then user verify the view award page again ")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]

        public void Test_AwardViewPreview()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Login as admin
            MyDashboardPage MDP = LP.Login(Constants_LMSAdmin.LoginUserNames.CMECAL, "password");


            string activityName = Autotest_ActivityNegativeCase; ; //Activity Name -> AutomationTest Activity1_DoNotUse_<<BrowserName>>
            string templateName = "AutomationTest_Award Template - Do Not Edit";
            string awardName = "PreviewTest";
            /// The accreditation body 
            string accreditationBody = "Non-Accredited";

            /// 3. Search for the given Autotest_Activity1 activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            ActAccreditationPage ACCPage = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);
            if (!Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                    ACCPage.DeleteAccreditation(accreditationBody);
            }

            /// 4. Click "Accredition" node , navigate to fireball page and add an accreditation
            ACCPage.AddAccreditation(accreditationBody, claimCreditEnabled: false);
            ACCPage.RefreshPage(true);

            /// 5. Click "Add Scenario" to add a new anscenario 
            string ScenarioName = ACCPage.AddScenario(accreditationBody, accrClaimCreditEnabled: false);

            /// 6. Click on "Awards" Tab , award page should be displayed
            ActAwardsPage AWDP = ACCPage.ClickAndWaitBasePage(ACCPage.Steps_AwardLbl);
            if (ElemGet.Grid_ContainsRecord(Browser, AWDP.AwardsTbl, Bys.ActAwardsPage.AwardsTblBody, 0, awardName, "td"))
            {
                AWDP.DeleteAward(awardName);
            }
            /// 7. Click "Add Award" button , Add AWARD page should be dispalyed
            AWDP.ClickAndWait(AWDP.AddAwardBtn);

            /// 8. Fill all the details , select "AutomationTest_Award Template - Do Not Edit" award template 
            ElemSet.TextBox_EnterText(Browser, AWDP.AwardNameTxt, true, awardName);
            IWebElement templateRow = null;

            templateRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(AWDP.TemplatesTbl, Bys.ActAwardsPage.TemplatesTblFirstRow, templateName, "td");

            templateRow.FindElement(By.XPath(".//td[3]"), ElementCriteria.HasText);

            ElemSet_LMSAdmin.Grid_TickCheckBox(templateRow, 1);
            Browser.WaitJSAndJQuery();

            /// 9. Click on template preview and verify Preview window opened and image displayed
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, templateRow, "button[@aria-label='click to template preview']");

            Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.ActAwardsPage.TemplatePreviewFormTitleLbl, ElementCriteria.IsVisible);
            Browser.WaitForElement(Bys.ActAwardsPage.TemplatePreviewFormImg, ElementCriteria.IsVisible);
            AWDP.TemplatePreviewFormCloseBtn.Click();

            ElemSet.ScrollToElement(Browser, AWDP.PortraitTypeRdoBtn);

            /// 10. Goto Customize Award Section, Click on HTML icon and verify the html window opened and html code for image is displayed
            AWDP.CustomiseAwardHtmlLnk.Click();
            Browser.WaitForElement(Bys.ActAwardsPage.ViewHTMLFormTitleLbl, ElementCriteria.IsVisible);
            Browser.WaitForElement(Bys.ActAwardsPage.ViewHtmlTextAreaTxt, ElementCriteria.AttributeValueContains("value", "Images"));
            AWDP.ViewHtmlFormCloseBtn.Click();

            // Browser.WaitForElement(Bys.ActAwardsPage.CustomiseAwardImg, ElementCriteria.IsVisible);

            /// 11. Goto Scenarios section, Choose newly created scenario and save the award
            IWebElement scenarioRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(AWDP.ScenariosTbl, Bys.ActAwardsPage.ScenariosTblFirstRow, ScenarioName, "td");
            ElemSet_LMSAdmin.Grid_TickCheckBox(scenarioRow, 1);
            Browser.WaitJSAndJQuery();

            AWDP.ClickAndWait(AWDP.SaveAwardBtn);
            Browser.WaitJSAndJQuery();

            /// 12. Goback to main activity page, set to construction complete
            AWDP.ClickAndWaitBasePage(AWDP.BackToActivityBtn);
            AMP.ChangeActivityStage(Constants.ActStage.ConstructionComplete);

            /// 13. Click on Award node, Go to Award fireball page
            AMP.ClickAndWaitBasePage(AMP.TreeLinks_Awards);

            /// 14. Search for the created Award row , click on award name and verify ViewAward page is loaded
            IWebElement awardRow = ElemGet.Grid_GetRowByRowName(Browser, AWDP.AwardsTbl, Bys.ActAwardsPage.AwardsTblFirstRow, awardName, "td");
            ElemSet.Grid_ClickCellByColIndex(Browser, awardRow, 0, "//td");
            Browser.WaitJSAndJQuery();

            /// 15. Veirify all fields are disabled , and values are displayed ,template and scenario checkboxes are shown with selected value
            /// and SaveAward Button should be disabled 
            Browser.WaitForElement(Bys.ActAwardsPage.ViewAwardTitleLbl, ElementCriteria.IsVisible);
            Assert.True(Browser.Exists(Bys.ActAwardsPage.AwardNameTxt, ElementCriteria.HasAttribute("disabled")));
            IWebElement templateviewRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(AWDP.TemplatesTbl, Bys.ActAwardsPage.TemplatesTblFirstRow, templateName, "td");
            IWebElement templatechkbox = templateviewRow.FindElement(By.XPath(".//input[@type='checkbox']"));
            Assert.False(templatechkbox.Enabled);
            Assert.True(templatechkbox.GetAttribute("checked").Equals("true"));

            IWebElement scenarioviewRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(AWDP.ScenariosTbl, Bys.ActAwardsPage.ScenariosTblFirstRow, ScenarioName, "td");
            IWebElement scenariochkbox = scenarioviewRow.FindElement(By.XPath(
                ".//input[@type='checkbox']"));
            Assert.False(scenariochkbox.Enabled);
            Assert.True(scenariochkbox.GetAttribute("checked").Equals("true"));
            Assert.True(Browser.Exists(Bys.ActAwardsPage.SaveAwardBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
               "The SaveAward button should not be enabled when Activity is in 'Construction Complete' Stage");

            /// 16. Click on closeAward button to go back to main award page
            AWDP.ClickAndWait(AWDP.CloseAwardBtn);
            Browser.WaitJSAndJQuery();

            /// 17. Click on Preview Award icon in main award table, verify the Preview window is opened and image is displayed
            Browser.WaitForElement(Bys.ActAwardsPage.AwardsTbl, ElementCriteria.IsVisible);
            IWebElement awardviewRow = ElemGet.Grid_GetRowByRowName(Browser, AWDP.AwardsTbl, Bys.ActAwardsPage.AwardsTblFirstRow, awardName, "td");
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, awardviewRow, "button[@aria-label='click to template preview']");
            Browser.WaitForElement(Bys.ActAwardsPage.TemplatePreviewFormTitleLbl, ElementCriteria.IsVisible);
            Browser.WaitForElement(Bys.ActAwardsPage.TemplatePreviewFormImg, ElementCriteria.IsVisible);
            AWDP.TemplatePreviewFormCloseBtn.ClickJS(Browser);

            /// 18 . Delete Award icon should not be visible in view mode
            Assert.False(Browser.Exists(Bys.ActAwardsPage.DeleteAwardBtn));

            /// 19 . GobacktoMainpage, Publish the activity, Goto Award page and verify that all view mode conditions again
            AWDP.ClickAndWaitBasePage(AWDP.BackToActivityBtn);
            AMP.PublishActivity();

            AMP.ClickAndWaitBasePage(AMP.TreeLinks_Awards);

            IWebElement pub_awardRow = ElemGet.Grid_GetRowByRowName(Browser, AWDP.AwardsTbl, Bys.ActAwardsPage.AwardsTblFirstRow, awardName, "td");
            ElemSet.Grid_ClickCellByColIndex(Browser, pub_awardRow, 0, "//td");
            Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.ActAwardsPage.ViewAwardTitleLbl, ElementCriteria.IsVisible);
            Assert.True(Browser.Exists(Bys.ActAwardsPage.AwardNameTxt, ElementCriteria.HasAttribute("disabled")));
            IWebElement pub_templateviewRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(AWDP.TemplatesTbl, Bys.ActAwardsPage.TemplatesTblFirstRow, templateName, "td");
            IWebElement pub_templatechkbox = pub_templateviewRow.FindElement(By.XPath(".//input[@type='checkbox']"));
            Assert.False(pub_templatechkbox.Enabled);
            Assert.True(pub_templatechkbox.GetAttribute("checked").Equals("true"));

            IWebElement pub_scenarioviewRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(AWDP.ScenariosTbl, Bys.ActAwardsPage.ScenariosTblFirstRow, ScenarioName, "td");
            IWebElement pub_scenariochkbox = pub_scenarioviewRow.FindElement(By.XPath(".//input[@type='checkbox']"));
            Assert.False(pub_scenariochkbox.Enabled);
            Assert.True(pub_scenariochkbox.GetAttribute("checked").Equals("true"));
            Assert.True(Browser.Exists(Bys.ActAwardsPage.SaveAwardBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
               "The SaveAward button should not be enabled when Activity is in 'Construction Complete' Stage");
            AWDP.ClickAndWait(AWDP.CloseAwardBtn);
            Browser.WaitJSAndJQuery();

            Browser.WaitForElement(Bys.ActAwardsPage.AwardsTbl, ElementCriteria.IsVisible);
            IWebElement pub_awardviewRow = ElemGet.Grid_GetRowByRowName(Browser, AWDP.AwardsTbl, Bys.ActAwardsPage.AwardsTblFirstRow, awardName, "td");
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, pub_awardviewRow, "button[@aria-label='click to template preview']");
            Browser.WaitForElement(Bys.ActAwardsPage.TemplatePreviewFormTitleLbl, ElementCriteria.IsVisible);
            Browser.WaitForElement(Bys.ActAwardsPage.TemplatePreviewFormImg, ElementCriteria.IsVisible);
            AWDP.TemplatePreviewFormCloseBtn.ClickJS(Browser);
            Assert.False(Browser.Exists(Bys.ActAwardsPage.DeleteAwardBtn));

            /// 14. Click "back to activity" button and page will be redirected to Old CME360 and logoff.
            AWDP.ClickAndWaitBasePage(AWDP.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }

        [Test]
        [Description(" This test will do the validation of negative cases as 'Field Required error', 'notification messages'," +
          " 'date/digit validation messages', 'ActivityStages label color check', 'View mode/Read-only mode of all fields validations on " +
          " published/constructioncomplete stage', in both Accreditation and CompletionPathway Modules")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void AccreditationandCompletionPathway_NegativeCases()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            /// 2. Username as Autotest_user1 to login
            MyDashboardPage MDP = LP.Login(Constants_LMSAdmin.LoginUserNames.Pfizer, "password");

            string activityName = Autotest_ActivityNegativeCase;

            /// The accreditation body 
            string accreditationBody = "Non-Accredited";
            string primaryProvider = siteName;
            string Colourcode = "";

            /// 3. Search for the given test activity and ensure that it is in under construction stage
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            /// 4. Click on Accreditation node and The page will be redirected to New LMSadmin UI 
            ActAccreditationPage ACCPage = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 5. Verify that the activity Stage Text 'Under Construction' is displayed in blue color
            if (BrowserName.Equals("Chrome")) { Colourcode = "rgba(89, 143, 178, 1)"; }
            else if (BrowserName.Equals("Firefox")) { Colourcode = "rgb(89, 143, 178)"; }
            StringAssert.Contains(Colourcode, ACCPage.ActivityStageLbl.GetCssValue("color"),
            "UNDER CONSTRUCTION Text should be displayed in BLUE color in Activity Stage Details");


            /// 6. Click on AddAccreditation button and add an accreditation with empty fields and verify that the 'field required' message is displayed
            ///  for required fields
            ACCPage.ClickAndWait(ACCPage.AddAccreditationBtn);
            Assert.False(Browser.Exists(Bys.ActAccreditationPage.ClaimCreditEnabledChkbox, ElementCriteria.IsVisible),
                "ClaimCreditEnabled Checkbox should not be visible in UI for Pfizer ");
            ACCPage.AddAccreditationFormAddAccreditationBtn.ClickJS(Browser);
            Assert.LessOrEqual(Browser.FindElements(Bys.ActAccreditationPage.ThisFieldIsRequiredLbl).Count, 2,
                "Required Field count is more than two which is wrong in Add Accreditation Dialog ");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AccreditationBodyandTypeFieldRequiredLbl,
                ElementCriteria.Text("This field is required")), "If AccreditationBodyandTypeField is Empty , then should be shown as 'Required Field'");
            ACCPage.AnyDialogCommonCloseBtn.ClickJS(Browser);
            ACCPage.RefreshPage(true);

            /// 7. Search the accreditation body, if it's already there then delete and add newly , if not already there then add it
            if (!Browser.Exists(Bys.ActAccreditationPage.AccreditationEmptyMessage, ElementCriteria.IsVisible))
            {
                if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, ACCPage.AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                    ACCPage.DeleteAccreditation(accreditationBody);
            }
            ACCPage.AddAccreditation(accreditationBody, primaryProvider, claimCreditEnabled: false);
            ACCPage.RefreshPage(true);

            /// 8. Click on AddScenario Button and Save a scenario with empty fields and verify that the 'field required' message is displayed
            ///  for required fields
            IWebElement AddScenarioBtn = Browser.FindElement(By.XPath(string.Format("(//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'chart')][2]//following-sibling::div//span[text()='Add scenario'])[1]",
               accreditationBody)));
            AddScenarioBtn.Click();
            Browser.WaitForElement(Bys.ActAccreditationPage.ScenarionameTxt, ElementCriteria.IsEnabled);

            ACCPage.FixedCreditTxt.SendKeys("1234.123");
            ACCPage.EquivalentCreditTxt.SendKeys("123.12345");
            ACCPage.AddScenarioFormSaveScenarioBtn.Click();
            Assert.False(Browser.Exists(Bys.ActAccreditationPage.MaximumCreditsTxt, ElementCriteria.IsVisible),
               "MaximumCreditsTxt field should not be visible in UI for Pfizer ");
            Assert.False(Browser.Exists(Bys.ActAccreditationPage.MinimumCreditsTxt, ElementCriteria.IsVisible),
               "MinimumCreditsTxt field should not be visible in UI for Pfizer ");
            Assert.False(Browser.Exists(Bys.ActAccreditationPage.CreditIncrementsTxt, ElementCriteria.IsVisible),
              "CreditIncrementsTxt field should not be visible in UI for Pfizer ");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddScenarioFormScenarioNameFieldRequiredLbl,
                 ElementCriteria.Text("This field is required")), "If ScenarioNameField is Empty , then should be shown as 'Required Field'");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddScenarioFormEligibleProfessionsFieldRequiredLbl,
                 ElementCriteria.Text("This field is required")), "If EligibleProfessionsField is Empty , then should be shown as 'Required Field'");

            /// 9. Enter invalid input format of Credit fields digits in Scenario dialog and verify that the format error is  displayed 
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddScenarioFormFixedCreditFormatError,
                ElementCriteria.Text("This numeric field, will accept 3 digit numbers up to two decimals.")),
                "FixedCredit field should only accept 3 digit numbers up to two decimals ");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddScenarioFormEquivalentCreditFormatError,
               ElementCriteria.Text("This numeric field, will accept 3 digit numbers up to two decimals.")),
               "FixedCredit field should only accept 3 digit numbers up to two decimals ");
            ACCPage.AnyDialogCommonCloseBtn.ClickJS(Browser);
            ACCPage.RefreshPage(true);

            AddScenarioBtn = Browser.FindElement(By.XPath(string.Format("(//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'chart')][2]//following-sibling::div//span[text()='Add scenario'])[1]",
               accreditationBody)));
            AddScenarioBtn.Click();
            Browser.WaitForElement(Bys.ActAccreditationPage.ScenarionameTxt, ElementCriteria.IsEnabled);
            ACCPage.ScenarionameTxt.SendKeys("testnegative");
            ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ACCPage.EligibleProfessionElemBtn, "Administrator");
            ACCPage.ReleaseDateTxt.SendKeys("1/1/2001");
            ACCPage.ExpirationDateTxt.SendKeys("1/1/2000");
            ACCPage.FixedCreditTxt.Clear();
            ACCPage.EquivalentCreditTxt.Clear();
            ElemSet.ScrollToElement(Browser, ACCPage.AddScenarioFormSaveScenarioBtn, false);
            ACCPage.AddScenarioFormSaveScenarioBtn.Click();

            /// 10. Enter invalid range of Release and Expiration Dates in Scenario Dialog and verify that an error notification is displayed 
            Browser.WaitForElement(Bys.Page.AlertNotificationIconMsg, TimeSpan.FromSeconds(10), ElementCriteria.IsVisible);
            StringAssert.Contains("The expiration date cannot fall before the release date.", ACCPage.AlertNotificationIconMsg.Text,
                string.Format("Error should thrown when Expiration date [ {0} ] is lesser than Release date [ {1} ]", "1/1/2000", "1/1/2001"));
            ACCPage.AnyDialogCommonCloseBtn.ClickJS(Browser);
            ACCPage.RefreshPage(true);

            string scenarioName = ACCPage.AddScenario(accreditationBody, fixedCredits: 10, EquivalentCredits: 10);

            /// 11. Click on CompletionPathway step and CompletionPathway page should be loaded
            ActCompletionPathwayPage ACPLP = ACCPage.ClickAndWaitBasePage(ACCPage.Steps_CompletionPathwayLbl);

            /// 12. Click on ScenarioSettingsTab to load Scenario Settings Page and verify that NumberOfAttemptsAllowed fields has default value
            /// as 0(unlimited)
            ACPLP.ClickAndWait(ACPLP.ScenarioSettingsTab);
            IWebElement assessmentRow = ACPLP.GetAssessmentRowPresentInScenario(ACPLP.ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment");
            StringAssert.Contains("0 (unlimited)", assessmentRow.FindElement(Bys.ActCompletionPathwayPage.NumOfAttemptsAllowedTxt).GetAttribute("placeholder"),
                string.Format("By default , # ATTEMPTS ALLOWED field should have 0 (unlimited) value"));

            /// 13. Click on DeliverySettingsTab to load Delivery Settings Page 
            ACPLP.ClickAndWait(ACPLP.DeliverySettingsTab);
            ACPLP.WaitForInitialize();

            /// 14. Save the assessment by selecting Timing as "After" and without filling Number and Unit fields , 
            /// Verify that an error message is displayed for required fields
            ACPLP.FillAndSaveAsessmentDeliverySettings(scenarioName, accreditationBody, "AutoTest_DonotDelete_PreTest1", "Pre-Test Assessment", actionType: scenarioName,
               conditionType: "Passed", timingType: "After");
            StringAssert.Contains("Number and Units fields are required when Timing selected as 'After'", ACPLP.AlertNotificationIconMsg.Text,
                string.Format("[ {0} ] - Notification should throw upon saving Delivery Settings for Scenario [ {1} ] without filling the number and unit fields when Timing selected as 'After' ",
                ACPLP.AlertNotificationIconMsg.Text, scenarioName));

            /// 15. Back to Activity MainPage and Set the Activity to ConStruction Complete Stage then Navigate to Accreditation Page
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);
            AMP.ChangeActivityStage(Constants.ActStage.ConstructionComplete);
            AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            /// 16. Verify that notification displayed as accreditation section can not be modified and 
            /// the activity Stage Text 'Construction Complete' is displayed in Green color
            Assert.True(Browser.Exists(Bys.Page.InformationNotificationMsg,
               ElementCriteria.Text("This section cannot be modified because the activity stage is ''Construction Complete'' or ''Published''")),
                "Section Can not be modified notification should shown if the activity status is 'Published' ");
            Assert.True(Browser.Exists(Bys.Page.ActivityStageLbl, ElementCriteria.Text("CONSTRUCTION COMPLETE")),
                "Activity Stage should display as 'CONSTRUCTION COMPLETE' in the Leftpane");

            if (BrowserName.Equals("Chrome")) { Colourcode = "rgba(145, 197, 63, 1)"; }
            else if (BrowserName.Equals("Firefox")) { Colourcode = "rgb(145, 197, 63)"; }
            StringAssert.Contains(Colourcode, ACCPage.ActivityStageLbl.GetCssValue("color"),
                "CONSTRUCTION COMPLETE Text should be displayed in GREEN color in Activity Stage Details");

            /// 17. Verify that On Construction Complete Stage , AddAccreditation and AddScenario button should be disabled
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddAccreditationBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The AddAccreditation button should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddScenarioBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The AddScenario button should not be enabled when Activity is in 'Construction Complete' Stage");

            /// 18. Verify that On Construction Complete Stage , Edit and Delete Buttons for Accreditation and Scenario should be disabled
            Assert.False(Browser.Exists(Bys.ActAccreditationPage.AccreditationOrScenarioDeleteBtn),
                "The AccreditationOrScenarioDelete button should not be available when Activity is in 'Construction Complete' Stage");
            Assert.False(Browser.Exists(Bys.ActAccreditationPage.AccreditationOrScenarioEditBtn),
                "The AccreditationOrScenarioEdit button should not be available when Activity is in 'Construction Complete' Stage");

            /// 19. Verify that On Construction Complete Stage , View Button for Accreditation and Scenario should be Enabled
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AccreditationOrScenarioViewBtn),
               "The AccreditationOrScenarioView button should be available when Activity is in 'Construction Complete' Stage");

            /// 20. Click on View button of Accreditation and verify that AccreditationBodyandType,PrimaryProvider,AdditionalProvider fields 
            /// are displayed as read-only mode in View Accreditation Dialog
            IWebElement accrTable = Browser.FindElement(By.XPath(string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::table",
             accreditationBody)));

            IWebElement parentRow = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, accrTable, Bys.ActAccreditationPage.AccreditationDetailsTblRow,
                accreditationBody, "td", primaryProvider, "td");
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, parentRow, "button", 0);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Browser.WaitForElement(Bys.ActAccreditationPage.ViewAccreditationFormViewAccreditationLbl, TimeSpan.FromSeconds(10), ElementCriteria.IsVisible);
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AccreditationBodyandTypeSelElemBtn, ElementCriteria.TextContains(accreditationBody),
               ElementCriteria.AttributeValueContains("class", "disabled")), "AccreditationBodyandType Field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(By.XPath("//button[contains(@aria-labelledby,'accProviderName')]"), ElementCriteria.TextContains(primaryProvider),
              ElementCriteria.AttributeValueContains("class", "disabled")), "PrimaryProvider Field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(By.XPath("//button[contains(@aria-labelledby,'accAddProviderName')]"), ElementCriteria.AttributeValueContains("class", "disabled")),
                "AdditionalProvider Field should not be enabled when Activity is in 'Construction Complete' Stage");
            ACCPage.AnyDialogCommonCloseBtn.ClickJS(Browser);
            ACCPage.RefreshPage(true);

            /// 21. Click on View button of Scenario and verify that Scenarioname, ReleaseDate, ExpirationDate, Date's CalendarIcon,
            /// Fixed&EquivalentCredit, CreditUnits, EligibleProfession fields are displayed as read-only mode in View Scenario Dialog
            string scenarioTblXpath = string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'accreditationGridChart')]/following-sibling::div[contains(@class,'scenarioGridChart')]//table", accreditationBody);
            string scenarioTblRowXpath = string.Format("{0}/tbody/tr", scenarioTblXpath);

            IWebElement specifiedAccreditationScenarioTbl = Browser.FindElement(By.XPath(scenarioTblXpath));

            IWebElement scenarioRow = ElemGet.Grid_GetRowByRowName(Browser, specifiedAccreditationScenarioTbl, By.XPath(scenarioTblRowXpath), scenarioName, "td");

            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, scenarioRow, "button", 0);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Browser.WaitForElement(Bys.ActAccreditationPage.ViewScenarioFormViewScenarioLbl, TimeSpan.FromSeconds(10), ElementCriteria.IsVisible);
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.ScenarionameTxt, ElementCriteria.AttributeValue("value", scenarioName),
               ElementCriteria.HasAttribute("disabled")), "Scenarioname Field should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.ReleaseDateTxt, ElementCriteria.AttributeValueNot("value", ""),
               ElementCriteria.HasAttribute("disabled")), "ReleaseDate Field should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(ACCPage.ReleaseDateTxt.FindElement(By.XPath("./../span")).GetAttribute("aria-hidden").Contains("true"),
                 "ReleaseDate Field's Calendar Icon should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.ExpirationDateTxt, ElementCriteria.AttributeValueNot("value", ""),
               ElementCriteria.HasAttribute("disabled")), "ExpirationDate Field should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(ACCPage.ExpirationDateTxt.FindElement(By.XPath("./../span")).GetAttribute("aria-hidden").Contains("true"),
                 "ExpirationDate Field's Calendar Icon should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.FixedCreditTxt, ElementCriteria.AttributeValue("value", "10.00"),
               ElementCriteria.HasAttribute("disabled")), "FixedCredit Field should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.FixedCreditUnitSelElemBtn, ElementCriteria.AttributeValue("title", "CECH"),
               ElementCriteria.AttributeValueContains("class", "disabled")), "FixedCreditUnit Field should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.EquivalentCreditTxt, ElementCriteria.AttributeValue("value", "10.00"),
               ElementCriteria.HasAttribute("disabled")), "EquivalentCredit Field should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.EquivalentCreditUnitSelElemBtn, ElementCriteria.AttributeValue("title", "Cases"),
               ElementCriteria.AttributeValueContains("class", "disabled")), "EquivalentCreditUnit Field should not be enabled in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.EligibleProfessionElemBtn, ElementCriteria.AttributeValueNot("text", ""),
               ElementCriteria.AttributeValueContains("class", "disabled")), "EligibleProfession Field should not be enabled and has value in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            Assert.True(ACCPage.EligibleProfessionElemBtn.FindElement(By.XPath("./ancestor::div[2]/following::div[1]/button")).GetAttribute("class").Contains("disabled"),
                 "Selected Professions should not be alowed to remove in View Scenario Dialog when Activity is in 'Construction Complete' Stage");
            ACCPage.AnyDialogCommonCloseBtn.ClickJS(Browser);
            ACCPage.RefreshPage(true);

            /// 22. Click on Completion Pathway Tab to navigate to the module
            ACCPage.ClickAndWaitBasePage(ACCPage.Steps_CompletionPathwayLbl);

            /// 23. Verify that On Construction Complete Stage , Navigate to Scenario Settings Page to Verify the section can not be modified notification is displayed
            /// and Verify that Display,Order,CompletionRequired,GradedQues,AttemptsAllowed Fields are displayed as read-only mode 
            ACPLP.ClickAndWait(ACPLP.ScenarioSettingsTab);

            Assert.True(Browser.Exists(Bys.Page.InformationNotificationMsg,
            ElementCriteria.Text("This section cannot be modified because the activity stage is ''Construction Complete'' or ''Published''")),
            "Section Can not be modified notification should shown if the activity status is 'Construction Complete' ");

            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.AssessmentDisplayChkbox, ElementCriteria.HasAttribute("disabled")),
              "The Display field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.OrderOfAssessmentSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The OrderOfAssessment field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.CompletionRequiredSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The CompletionRequired field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.NumOfGradedQuesToPassSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The NumOfGradedQuesToPass field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.NumOfAttemptsAllowedTxt, ElementCriteria.HasAttribute("disabled")),
               "The NumOfAttemptsAllowed field should not be enabled when Activity is in 'Construction Complete' Stage");

            /// 24. Verify that On Construction Complete Stage , Navigate to Delivery Settings Page to Verify the section can not be modified notification is displayed
            /// and Verify the Action, Condition, Timing, Units, Number, Assessment Notification fields are displayed as read-only mode
            ACPLP.ClickAndWait(ACPLP.DeliverySettingsTab);

            Assert.True(Browser.Exists(Bys.Page.InformationNotificationMsg,
              ElementCriteria.Text("This section cannot be modified because the activity stage is ''Construction Complete'' or ''Published''")),
              "Section Can not be modified notification should shown if the activity status is 'Construction Complete' ");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.ActionTriggerSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Action field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.ConditionTypeSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Condition field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.TimingTypeSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Timing field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.NumberofUnitsTxt, ElementCriteria.HasAttribute("disabled")),
               "The NumberofUnits field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.UnitsTypeSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Units field should not be enabled when Activity is in 'Construction Complete' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.AssessmentNotificationSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
               "The AssessmentNotification field should not be enabled when Activity is in 'Construction Complete' Stage");

            /// 25. Click BackToActivity button, Navigate to Activity Main page and Change the activity stage to "Published" 
            ACCPage.ClickAndWaitBasePage(ACCPage.BackToActivityBtn);

            AMP.PublishActivity();

            /// 26. Navigate to Accreditation Page and Verify that notification displayed as accreditation section can not be modified and 
            /// the activity Stage Text 'PUBLISHED' is displayed in Green color
            AMP.ClickAndWaitBasePage(AMP.TreeLinks_Accreditation);

            Assert.True(Browser.Exists(Bys.Page.InformationNotificationMsg,
               ElementCriteria.Text("This section cannot be modified because the activity stage is ''Construction Complete'' or ''Published''")),
               "Section Can not be modified notification should shown if the activity status is 'Published' ");

            Assert.True(Browser.Exists(Bys.Page.ActivityStageLbl, ElementCriteria.Text("PUBLISHED")),
                "Activity Stage should display as 'PUBLISHED' in the Leftpane");

            if (BrowserName.Equals("Chrome")) { Colourcode = "rgba(145, 197, 63, 1)"; }
            else if (BrowserName.Equals("Firefox")) { Colourcode = "rgb(145, 197, 63)"; }
            StringAssert.Contains(Colourcode, ACCPage.ActivityStageLbl.GetCssValue("color"),
                "PUBLISHED Text should be displayed in GREEN color in Activity Stage Details");

            /// 27. Verify that On Published Stage , AddAccreditation and AddScenario button should be disabled
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddAccreditationBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The AddAccreditation button should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AddScenarioBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The AddScenario button should not be enabled when Activity is in 'Published' Stage");

            /// 28. Verify that On Published Stage , Edit and Delete Buttons for Accreditation and Scenario should be disabled
            Assert.False(Browser.Exists(Bys.ActAccreditationPage.AccreditationOrScenarioDeleteBtn),
                "The AccreditationOrScenarioDelete button should not be available when Activity is in 'Published' Stage");
            Assert.False(Browser.Exists(Bys.ActAccreditationPage.AccreditationOrScenarioEditBtn),
                "The AccreditationOrScenarioEdit button should not be available when Activity is in 'Published' Stage");

            /// 29. Verify that On Published Stage , View Button for Accreditation and Scenario should be Enabled
            Assert.True(Browser.Exists(Bys.ActAccreditationPage.AccreditationOrScenarioViewBtn),
               "The AccreditationOrScenarioView button should be available when Activity is in 'Published' Stage");

            /// 30. Click on Completion Pathway Tab to navigate to the module
            ACCPage.ClickAndWaitBasePage(ACCPage.Steps_CompletionPathwayLbl);

            /// 31. Verify that On Published tage , Navigate to Scenario Settings Page to Verify the section can not be modified notification is displayed
            /// and Verify that Display,Order,CompletionRequired,GradedQues,AttemptsAllowed Fields are displayed as read-only mode
            ACPLP.ClickAndWait(ACPLP.ScenarioSettingsTab);

            Assert.True(Browser.Exists(Bys.Page.InformationNotificationMsg,
            ElementCriteria.Text("This section cannot be modified because the activity stage is ''Construction Complete'' or ''Published''")),
            "Section Can not be modified notification should shown if the activity status is 'Published' ");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.AssessmentDisplayChkbox, ElementCriteria.HasAttribute("disabled")),
              "The Display field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.OrderOfAssessmentSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The OrderOfAssessment field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.CompletionRequiredSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The CompletionRequired field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.NumOfGradedQuesToPassSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The NumOfGradedQuesToPass field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.NumOfAttemptsAllowedTxt, ElementCriteria.HasAttribute("disabled")),
               "The NumOfAttemptsAllowed field should not be enabled when Activity is in 'Published' Stage");

            /// 32. Verify that On Published Stage , Navigate to Delivery Settings Page to Verify the section can not be modified notification is displayed
            /// and Verify the Action, Condition, Timing, Units, Number, Assessment Notification fields are displayed as read-only mode
            ACPLP.ClickAndWait(ACPLP.DeliverySettingsTab);

            Assert.True(Browser.Exists(Bys.Page.InformationNotificationMsg,
              ElementCriteria.Text("This section cannot be modified because the activity stage is ''Construction Complete'' or ''Published''")),
              "Section Can not be modified notification should shown if the activity status is 'Published' ");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.ActionTriggerSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Action field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.ConditionTypeSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Condition field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.TimingTypeSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Timing field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.NumberofUnitsTxt, ElementCriteria.HasAttribute("disabled")),
               "The NumberofUnits field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.UnitsTypeSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
                "The Units field should not be enabled when Activity is in 'Published' Stage");
            Assert.True(Browser.Exists(Bys.ActCompletionPathwayPage.AssessmentNotificationSelElemDropdownBtn, ElementCriteria.AttributeValueContains("class", "disabled")),
               "The AssessmentNotification field should not be enabled when Activity is in 'Published' Stage");

        }


    }

    #endregion Tests



}


