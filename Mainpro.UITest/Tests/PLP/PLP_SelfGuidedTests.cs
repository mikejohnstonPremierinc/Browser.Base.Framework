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
using AventStack.ExtentReports;
using Mainpro.UITest;

namespace PLP.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuidedTests : TestBase
    {
        #region Constructors
        public PLP_SelfGuidedTests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuidedTests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("For Selfguided version - verifies the flow step1 to step5 end ")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void Self_CheckStep1toStep5()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT) || Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("This test is only to execute in production env and will be ignored in lower environments " +
                    "because 'Self_CheckStep1to6withMProTblHub' script is available fo covering this test also");
            }
            //Step4Page PS4 = Help.PLP_GoToStep(Browser, 4, TestContext.CurrentContext.Test,
            //    username: "TestAutoI1H_Dec-24-21_10-45_Self_CompleteFlowStep1toStep6",
            //    password: password,
            //    isSelfGuided: true);

            #region logs

            TESTSTEP.Log(Status.Info, "1. Create a user, launch into PLP, verify the PLP Activity Overview slides load properly " +
                "after clicking each carousel button beneath them, then choose the Peer Supported option");
            TESTSTEP.Log(Status.Info, "2. Click the Begin button for the Peer Supported section, click the Confirm button, then " +
                "click Next until the Domains of Care page is presented to the user");
            TESTSTEP.Log(Status.Info, "3. Expand all sections within the Domains of Care page, choose a check box within each section");
            TESTSTEP.Log(Status.Info, "4. Assert that the Next button is disabled until the 'To continue, please confirm...' " +
                "check box is checked, then click the Next button");
            TESTSTEP.Log(Status.Info, "5. Enter '1' into the Other field then assert the Other Professional Practice field appears");
            TESTSTEP.Log(Status.Info, "6. Assert the graph displays 1% then assert the Next button is disabled until the graph " +
                "displays 100%");
            TESTSTEP.Log(Status.Info, "7. Click the next button, choose a characteristic, click Next again, choose a patient " +
                "population, then click Next to navigate to Step 2");
            TESTSTEP.Log(Status.Info, "8. Choose a Domain of Care from the dropdown, choose 2 Subsets, then add a Gap and click Next");
            TESTSTEP.Log(Status.Info, "9. Click Next on each page choosing options from the check boxes until Step 3 appears");
            TESTSTEP.Log(Status.Info, "10. Click next, choose a gap, click next until Goal 1 page appears, then enter information " +
                "in each one of the formatted text controls");
            TESTSTEP.Log(Status.Info, "11. Click next then add text into the How Will This Goal Address Your Gap control");
            TESTSTEP.Log(Status.Info, "12. Click next and then add a CPD event and assert that the event was added to the table");
            TESTSTEP.Log(Status.Info, "13. Edit the event, assert the edit in the table, then Delete the even and assert it was " +
                "removed from the table");
            TESTSTEP.Log(Status.Info, "14. Click Next, choose a timeframe radio button, click Next, choose the Yes radio " +
                "button, click Next to proceed to Step 4");
            #endregion

            Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,
                isSelfGuided: true);
        }

        [Test]
        [Description("For Selfguided version, Given that if No suggestion of CPD programs results then" +
            "verify that NO Suggestions Result message displayed on Step3-cpdsuggestion screen" +
            " and Step5-prereflection screen, Step5-usefulcpdprograms screen")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void NoCPDResultMessageDisplay()
        {
            /*//This commented Block for Debug Process       
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP = LP.Login("TestAuto342_Feb-18-22_8-34_Self_NoCPDProgramsResultsCheck",
                isNewUser: false);
            DP.ClickAndWait(DP.EnterBtn);
            SuppStep2Page PS2 = new SuppStep2Page(browser);*/

            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    " since we dont have empty search combination in production ");
            }
            TESTSTEP.Log(Status.Info, "1.Enter into PLP Module");
            TESTSTEP.Log(Status.Info, "2.complete step1,2,3,4");
            TESTSTEP.Log(Status.Info, "3.In step2 ,choose");
            TESTSTEP.Log(Status.Info, "primarydomain: Administration");
            TESTSTEP.Log(Status.Info, "subdomains: Committee chair/ member"); 
            TESTSTEP.Log(Status.Info, "4.verify that NO Suggestions Result message displayed on Step3-cpdsuggestion screen");
            TESTSTEP.Log(Status.Info, "5.dont add any activities in step3");
            TESTSTEP.Log(Status.Info, "6.and go to step5 and verify the same message is shown in Step5 - prereflection screen, Step5 - usefulcpdprograms screen");
            TESTSTEP.Log(Status.Info, "7.Message is");
            TESTSTEP.Log(Status.Info, "If there are no activities yielded by search results, you can add CPD activities to your plan manually by clicking on the + Activities button.Options may include Mainpro+® Linking Learning Exercises conferences, online resources, webinars, peer - to - peer education, journal reading, instructional videos, etc.");


            Step3Page PS3 = Help.PLP_GoToStep(Browser, 3, TestContext.CurrentContext.Test,
            isSelfGuided: true,
            primarydomain: "Administration",
            subdomains: new List<string>() { "Committee chair/member" });

            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ChooseGaps(new List<string>() { "Gap 1 (Testing gap 1)", "Gap 2 (Testing gap 2)" });
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillSMARTGoalDetails(Browser, "Goal 1 Title", Const_Mainpro.PLP_TextboxlabelText.Step3Goal1TitleTxt);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillAddressingGoalNeeds(Browser);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillGoalOutcomes(Browser);
            PS3.AddAnotherGoalNoRdo.Click();
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            Assert.True(Browser.FindElement(By.ClassName("gap1")).Text.
                Equals("Gap 1: Testing gap 1"));
            Assert.True(Browser.FindElement(By.ClassName("gap2")).Text.
                Equals("Gap 2: Testing gap 2"));
            Assert.True(PS3.CPDEventsTblBody.Text.Trim().Contains("If there are no activities yielded by search results, " +
              "you can add CPD activities to your plan manually by clicking on the + Activities button."));
            Assert.True(PS3.CPDEventsTblBody.Text.Trim().Contains("Options may include Mainpro+® Linking Learning Exercises," +
                " conferences, online resources, " +
               "webinars, peer-to-peer education, journal reading, instructional videos, etc."));
            Assert.True(PS3.PlusActivitiesBtn.Enabled);
            PS3.ClickAndWait(PS3.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "0-3 months");
            PS3.ClickAndWait(PS3.NextBtn);
            Step4Page PS4 = PS3.ClickAndWait(PS3.NextBtn);
            PS4.FillStep4Screens(isSelfGuided: true, new List<string>() { "Goal 1 Title" });
            Step5Page PS5 = new Step5Page(Browser);
            Assert.True(PS5.PreReflectionCPDActivitiesTblEmptyBody.Text.Trim().
                Contains("If there are no activities yielded by search results, " +
             "you can add CPD activities to your plan manually by clicking on the + Activities button."),
             "'there are no activities yielded by search results' - this Message should display");
            Assert.True(PS5.PreReflectionCPDActivitiesTblEmptyBody.Text.Trim().Contains("Options may include Mainpro+® Linking Learning Exercises," +
                " conferences, online resources, " +
               "webinars, peer-to-peer education, journal reading, instructional videos, etc."),
               "there are no activities yielded by search results - Message should display");
            Assert.True(PS5.PlusActivitiesBtn.Enabled);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");
            PS5.ClickAndWait(PS5.NextBtn);
            PS5.ClickAndWait(PS5.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            Assert.True(PS5.UsefulCPDActivitiesTblBody.Text.Trim().
                Contains("If there are no activities yielded by search results, " +
             "you can add CPD activities to your plan manually by clicking on the + Activities button."),
             "there are no activities yielded by search results - Message should display");
            Assert.True(PS5.UsefulCPDActivitiesTblBody.Text.Trim().Contains("Options may include Mainpro+® Linking Learning Exercises," +
                " conferences, online resources, " +
               "webinars, peer-to-peer education, journal reading, instructional videos, etc."),
               "there are no activities yielded by search results - Message should display");
            Assert.True(PS5.PlusActivitiesBtn.Enabled);
        }

        [Test]
        [Description(" This Test navigates from 0 % PLP to 100 % PLP by filling all the screens" +
            " and By choosing Yes Flows in Step5 and step6 along with the Verification cases below:" +
            "1.Verify the 0 % shows in mainpro + Dashboard PLP Section for New User who enter into PLP Module for First time" +
            "2.verify the PLP Activity Overview slides load properly after clicking each carousel button beneath them" +
            "3.Domains of Care page, Assert that the Next button is disabled until the 'To continue, please confirm...'" +
            "    check box is checked" +
            "4.Verify percentageperdomain chart displays with 0 % and full circle with grey color when 0 %" +
            "5.Assert the graph displays 1 % then assert the Next button is disabled until the graph displays 100 %" +
            ".Verify percentageperdomain chart displays grey color when 1 % complete and 99 % incomplete" +
            "7.Verify percentageperdomain chart should not display grey color when 100 %" +
            "8.verify canmedFmrole screen is not mandatory to be filled and user is optional to choose values in the screen" +
            "9.verify the CPDSuggestion screen is optional, so not filling this screen and proceeding" +
            "10.verify Working with a peer or colleague screen applicable only for PeerVersion" +
            "11.Verify the PLP Activity Summary option under User Profile Menu, is Not enabled until user " +
            "completes Step 3 of the activity." +
            "12.Verify Step3 goalTimelineContent Text differs for Peer and Selfguided version" +
            "13.verify Goal Setting screen will be displayed only for Peerversion" +
            "14.Goal Setting Timelines shown with correct dates in PreReflection screen" +
            "15.Verify that Feedback screen applicable for Peer Version ONLY" +
            "16.Mainpro + Dashboardpage - verify the Credits in Applied column in CURRENT YEAR - Credits Applied to Date Table" +
            "17.Mainpro + Dashboardpage - verify the Credits in Applied column in CYCLE - Credits Applied to Date Table" +
            "18.Navigate to CreditSummaryPage, under Assessment Table section, " +
            "Click view Link to open the View the activities List Popup and " +
            "Verify that PLP Activity and Credits displayed correctly in the popup List" +
            "19.Verify that for PLP Activity can not be editable or deleteable in CreditSummaryPage" +
            " - View the activities List Popup" +
            "20.On the CPD Activities List page table, verify the following: Credits Applied, " +
            "ActivityTitleLink = disabled, Delete = disabled" +
            "21.on PLP Hub page, Verify it shows completion Date" +
            "22.PLPHUB Table - From Actions menu, click printPlpCertificate option, Verify the PLPCertificateButton" +
            " opens the popup and download the file correctly" +
            "23.PLPHUB Table - From Actions menu, click Print Completed PLP option, Verify the Print Completed PLP" +
            " opens the popup and download the file correctly" +
            "24.PLPHUB Table - From Actions menu, click View Completed PLP option, Verify the View Completed PLP opens" +
            "the PLP module and lands on overallplpcompletionscreen" +
            "25. on ExitPLP button and verify its navigated back to Dashboard page")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void Self_CheckStep1to6withMProTblHub()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    " since it requires Step6 unlocked to complete the full flow and " +
                    "we dont want to do in Prod since this may alter clients data/users");
            }
            #region  log info 
            TESTSTEP.Log(Status.Info, "This Test navigates from 0 % PLP to 100 % PLP by filling all the screens" +
            " and By choosing Yes Flows in Step5 and step6 along with the Verification cases below:" +
            "1.Verify the 0 % shows in mainpro + Dashboard PLP Section for New User who enter into PLP Module for First time" +
            "2.verify the PLP Activity Overview slides load properly after clicking each carousel button beneath them" +
            "3.Domains of Care page, Assert that the Next button is disabled until the 'To continue, please confirm...'" +
            "    check box is checked" +
            "4.Verify percentageperdomain chart displays with 0 % and full circle with grey color when 0 %" +
            "5.Assert the graph displays 1 % then assert the Next button is disabled until the graph displays 100 %" +
            ".Verify percentageperdomain chart displays grey color when 1 % complete and 99 % incomplete" +
            "7.Verify percentageperdomain chart should not display grey color when 100 %" +
            "8.verify canmedFmrole screen is not mandatory to be filled and user is optional to choose values in the screen" +
            "9.verify the CPDSuggestion screen is optional, so not filling this screen and proceeding" +
            "10.verify Working with a peer or colleague screen applicable only for PeerVersion" +
            "11.Verify the PLP Activity Summary option under User Profile Menu, is Not enabled until user " +
            "completes Step 3 of the activity." +
            "12.Verify Step3 goalTimelineContent Text differs for Peer and Selfguided version" +
            "13.verify Goal Setting screen will be displayed only for Peerversion" +
            "14.Goal Setting Timelines shown with correct dates in PreReflection screen" +
            "15.Verify that Feedback screen applicable for Peer Version ONLY" +
            "16.Mainpro + Dashboardpage - verify the Credits in Applied column in CURRENT YEAR - Credits Applied to Date Table" +
            "17.Mainpro + Dashboardpage - verify the Credits in Applied column in CYCLE - Credits Applied to Date Table" +
            "18.Navigate to CreditSummaryPage, under Assessment Table section, " +
            "Click view Link to open the View the activities List Popup and " +
            "Verify that PLP Activity and Credits displayed correctly in the popup List" +
            "19.Verify that for PLP Activity can not be editable or deleteable in CreditSummaryPage" +
            " - View the activities List Popup" +
            "20.On the CPD Activities List page table, verify the following: Credits Applied, " +
            "ActivityTitleLink = disabled, Delete = disabled");
            #endregion 

            //This is for Debug Process
            //LoginPage LP = Navigation.GoToLoginPage(browser);
            //DashboardPage DP = LP.Login("TestAutob64_May-20-22_7-28_Self_CheckStep1to6withMProTblHub",
                //isNewUser: false);
            //DP.ClickAndWait(DP.EnterBtn);
            //StepPRPage PS6 = new StepPRPage(browser);

            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,
                isSelfGuided: true, isCPDSuggestionActivtyTblCheck: true);
            PS6.FillStep6_YesFlow(Browser);

             DashboardPage DP = new DashboardPage(Browser);
            Assert.True(Browser.FindElement(Bys.MainproPage.PLPPercentChartTxt).Text.Contains("0%"));
            DP.RefreshPage(true);
            ///  Navigate to CreditSummaryPage,under Assessment Table section
            /// Click view Link to open the View the activities List Popup
            CreditSummaryPage CP = DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.CreditSummaryTabAssessment,
                cellText: "View", rowNum: 1);

            /// Verify that PLP Activity and Credits displayed correctly in the popup List
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities,
                    rowName: "Professional Learning Plan - Self-guided pathway",
                    colName: "Credits", cellTextExpected: "12");
            // Verify that for PLP Activity can not be editable or deleteable
            var EditBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, CP.CSViewFormViewActivitiesTbl,
                Bys.CreditSummaryPage.CSViewFormViewActivitiesTblBodyRow,
                Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(), "span", "div", "button-icon");
            Assert.True(EditBtn.GetAttribute("class").Contains("hide"));

            var DeleteBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, CP.CSViewFormViewActivitiesTbl,
               Bys.CreditSummaryPage.CSViewFormViewActivitiesTblBodyRow,
               Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(), "span", "div", "glyphicon-trash");
            Assert.True(DeleteBtn.GetAttribute("class").Contains("disabled"));
            CP.ClickAndWait(CP.CSViewFormViewActivitiesCloseBtn);

            /// Dashboardpage - verify the Credits in Applied column in CURRENT YEAR - Credits Applied to Date Table
            Help.VerifyCellTextMatches(Browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                "12", rowIndex: "2",
               colName: Const_Mainpro.TableColumnNames.Applied.GetDescription());
            Help.VerifyCellTextMatches(Browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
               "12", rowIndex: "0",
              colName: Const_Mainpro.TableColumnNames.Applied.GetDescription());

            /// Dashboardpage - verify the Credits in Applied column in  CYCLE - Credits Applied to Date Table
            Help.VerifyCellTextMatches(Browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                "12", rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
               colName: Const_Mainpro.TableColumnNames.Applied.GetDescription());
            Help.VerifyCellTextMatches(Browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
               "12", rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
              colName: Const_Mainpro.TableColumnNames.Applied.GetDescription());

            /// On the CPD Activities List page table, verify the following: Credits Applied,
            /// ActivityTitleLink = disabled , Delete = disabled
            CPDActivitiesListPage ALP = CP.ClickAndWaitBasePage(CP.CPDActivitiesListTab);
            var ALPDeleteBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow,
                Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
                "span", "div", "button-icon");
            Assert.True(ALPDeleteBtn.GetAttribute("class").Contains("disabled"));

            var ActivityTitleLink = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow,
                Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
                "span", "span", "act-met");
            Assert.False(ActivityTitleLink.GetAttribute("class").Contains("activityLink-text"));

            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
                colName: "Credits Reported", cellTextExpected: "12");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
                colName: "Credits Applied", cellTextExpected: "12");
            TESTSTEP.Log(Status.Info, "Click PLP Hub Tab, Navigate to PLP Hub page");
            PLPHubPage PLPHubP = DP.ClickAndWaitBasePage(DP.PlpHubTab);

            TESTSTEP.Log(Status.Info, "on PLP Hub page, Verify it shows 12 Credits for Self-Guided");
            Help.VerifyCellTextMatches(browser, PLPHubP, Const_Mainpro.Table.PLPHubCompletedPLPTbl,
              rowName: Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
              colName: "Credits Applied", cellTextExpected: "12.00");

            TESTSTEP.Log(Status.Info, "on PLP Hub page, Verify it shows completion Date");
            Help.VerifyCellTextMatches(browser, PLPHubP, Const_Mainpro.Table.PLPHubCompletedPLPTbl,
              rowName: Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
              colName: "Completion Date", cellTextExpected: currentDatetime.ToString("MM/dd/yyyy"));

            TESTSTEP.Log(Status.Info, "PLPHUB Table- From Actions menu, click printPlpCertificate option, Verify the PLPCertificateButton opens the popup and download the file correctly");
            IWebElement actbuttonPrCertPLP = ElemGet.Grid_GetButtonOrLinkInsideRowByText(Browser, PLPHubP.PLPHubCompletedPLPTbl,
               Bys.PLPHubPage.PLPHubCompletedPLPTblBodyFirstRow, Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
               "div", "Actions", "button");
            actbuttonPrCertPLP.Click(); Thread.Sleep(5);
            PLPHubP.ClickAndWait(PLPHubP.printPlpCertificateLnk);
            PLPHubP.ClickAndWait(PLPHubP.PrintPLPCertificateDownloadBtn);
            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 1, 0);
            PLPHubP.ClickAndWait(PLPHubP.printPLPCertCloseBtn);

            TESTSTEP.Log(Status.Info, "PLPHUB Table- From Actions menu, click Print Completed PLP option, Verify the Print Completed PLP opens the popup and download the file correctly");
            IWebElement actbuttonPrComplPLP = ElemGet.Grid_GetButtonOrLinkInsideRowByText(Browser, PLPHubP.PLPHubCompletedPLPTbl,
               Bys.PLPHubPage.PLPHubCompletedPLPTblBodyFirstRow, Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
               "div", "Actions", "button");
            actbuttonPrComplPLP.Click(); Thread.Sleep(5);
            PLPHubP.ClickAndWait(PLPHubP.printCompletedPlpLnk);
            PLPHubP.ClickAndWait(PLPHubP.PrintmycompletedPLPDownloadBtn);
            browser.WaitForElement(Bys.ReportsPage.ReportPDFEmbedElem, ElementCriteria.IsVisible);
            WindowAndFrameUtils.CloseWindowthenSwitchToWindow(browser, 1, 0);
            PLPHubP.ClickAndWait(PLPHubP.printPLPCompleteCloseButton);

            TESTSTEP.Log(Status.Info, "PLPHUB Table- From Actions menu, click View Completed PLP option, Verify the View Completed PLP opens the PLP module and lands on overallplpcompletionscreen ");
            IWebElement actbuttonViCompPLP = ElemGet.Grid_GetButtonOrLinkInsideRowByText(Browser, PLPHubP.PLPHubCompletedPLPTbl,
               Bys.PLPHubPage.PLPHubCompletedPLPTblBodyFirstRow, Const_Mainpro.PLP_ActivityNames.SelfGuided.GetDescription(),
               "div", "Actions", "button");
            actbuttonViCompPLP.Click(); Thread.Sleep(5);
            StepPRPage PR = PLPHubP.ClickAndWait(PLPHubP.ViewCompletedPlpLnk);
            TESTSTEP.Log(Status.Info, "Click on ExitPLP button and verify its navigated back to Dashboard page");
            PR.ClickAndWait(PR.ExitPLPBtn);

        }








        #endregion Tests
    }
}