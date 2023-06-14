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
    public class PLP_PeerSupportedTests : TestBase
    {
        #region Constructors
        public PLP_PeerSupportedTests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_PeerSupportedTests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

      
        [Test]
        [Description("For Peer Supported version - verifies the flow step1 to step5 end ")]
        [Property("Status", "Completed")]
       // [Category("Prod")]
        [Author("Bama Thangaraj")]
        public void Peer_CheckStep1toStep5()
        {
            if (Help.EnvironmentEquals(Constants.Environments.UAT)|| Help.EnvironmentEquals(Constants.Environments.CMEQA))
            {
                Assert.Ignore("This test is only to execute in production env and will be ignored in lower environments " +
                    "because 'Peer_CheckStep1to6withMProTblHub' script is available fo covering this test also");
            }
            #region logs
            TESTSTEP.Log(Status.Info, "This Test will do verifications of Step1 to Step5 , because Step6 needs an unlock" +
                "and that can not be done in Production because it may effect on client data");
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
            TESTSTEP.Log(Status.Info, "14. Click Next, Choose Yes flow and complete the Step 5");
            #endregion


            //If you need to Debug for specific user, you can Pass the username and password as below. 
            //Step4Page PS4 = Help.PLP_GoToStep(Browser, 4, TestContext.CurrentContext.Test,
            //    username: "TestAuto6H5_Dec-29-21_11-6_Peer_CompleteFlowStep1toStep5",
            //    password: password,
            //    isSelfGuided: false);

            Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,
                isSelfGuided: false);           

        }

        [Test]
        [Description("PeerSupportedTest - completes  step6 ")]
        [Property("Status", "Completed")]
        [Author("Bama Thangaraj")]
        public void Peer_CheckStep1to6withMProTblHub()
        {
            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                Assert.Ignore("This test will be ignored if the environment is Production " +
                    " since it requires Step6 unlocked to complete the full flow and " +
                    "we dont want to do in Prod since this may alter clients data/users");
            }
            ////If you need to Debug for specific user, you can Pass the username and password as below.
            //LoginPage LP = Navigation.GoToLoginPage(browser);
            //DashboardPage DP = LP.Login("TestAutoH1F_Mar-28-22_6-9_Peer_CompleteFlowStep1toStep6withMainproValidation",
            //    isNewUser: false);
            //DP.ClickAndWait(DP.EnterBtn);
            ///And If you need to start debug at any specific step, use like below 
            //StepPRPage PS6 = new StepPRPage(browser);

            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,
                isSelfGuided: false, isCPDSuggestionActivtyTblCheck: true);
            PS6.FillStep6_YesFlow(Browser);

            DashboardPage DP = new DashboardPage(Browser);
            Assert.True(Browser.FindElement(Bys.MainproPage.PLPPercentChartTxt).Text.Contains("0%"));

            /// Dashboardpage - verify the Applied column in  CYCLE - Credits Applied to Date Table
            Help.VerifyCellTextMatches(Browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
                "20", rowName: Const_Mainpro.TableRowNames.Certified.GetDescription(),
               colName: Const_Mainpro.TableColumnNames.Applied.GetDescription());
            Help.VerifyCellTextMatches(Browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCycle,
               "20", rowName: Const_Mainpro.TableRowNames.Total.GetDescription(),
              colName: Const_Mainpro.TableColumnNames.Applied.GetDescription());

            DP.RefreshPage(true);
            /// Dashboardpage - verify the Applied column in CURRENT YEAR - Credits Applied to Date Table
            Help.VerifyCellTextMatches(Browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
                "20", rowIndex: "2",
               colName: Const_Mainpro.TableColumnNames.Applied.GetDescription());
            Help.VerifyCellTextMatches(Browser, DP, Const_Mainpro.Table.CreditSummaryWidgetCurrentYear,
               "20", rowIndex: "0",
              colName: Const_Mainpro.TableColumnNames.Applied.GetDescription());

            ///  Navigate to CreditSummaryPage,under Ass Table section
            /// Click view Link to open the View the activities List Popup
            CreditSummaryPage CP = DP.ClickAndWaitBasePage(DP.CreditSummaryTab);
            Help.Grid_ClickCellInTable(browser, Const_Mainpro.Table.CreditSummaryTabAssessment,
                cellText: "View", rowNum: 1);

            /// 4. Verify that Submitted Activity and Credits displayed correctly in the popup List
            Help.VerifyCellTextMatches(browser, CP, Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities,
                    rowName: "Professional Learning Plan - Peer-supported pathway",
                    colName: "Credits", cellTextExpected: "20");

            var EditBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, CP.CSViewFormViewActivitiesTbl,
                Bys.CreditSummaryPage.CSViewFormViewActivitiesTblBodyRow,
                Const_Mainpro.PLP_ActivityNames.PeerSupported.GetDescription(), "span", "div", "button-icon");
            Assert.True(EditBtn.GetAttribute("class").Contains("hide"));

            var DeleteBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, CP.CSViewFormViewActivitiesTbl,
               Bys.CreditSummaryPage.CSViewFormViewActivitiesTblBodyRow,
               Const_Mainpro.PLP_ActivityNames.PeerSupported.GetDescription(), "span", "div", "glyphicon-trash");
            Assert.True(DeleteBtn.GetAttribute("class").Contains("disabled"));
            CP.ClickAndWait(CP.CSViewFormViewActivitiesCloseBtn);

            CPDActivitiesListPage ALP = CP.ClickAndWaitBasePage(CP.CPDActivitiesListTab);
            var ALPDeleteBtn = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow,
                Const_Mainpro.PLP_ActivityNames.PeerSupported.GetDescription(),
                "span", "div", "button-icon");
            Assert.True(ALPDeleteBtn.GetAttribute("class").Contains("disabled"));

            var ActivityTitleLink = ElemGet.Grid_GetButtonOrLinkInsideRowByPartialClassName(browser, ALP.ActTbl,
                Bys.CPDActivitiesListPage.ActTblBodyFirstRow,
                Const_Mainpro.PLP_ActivityNames.PeerSupported.GetDescription(),
                "span", "span", "act-met");
            Assert.False(ActivityTitleLink.GetAttribute("class").Contains("activityLink-text"));

            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Const_Mainpro.PLP_ActivityNames.PeerSupported.GetDescription(),
                colName: "Credits Reported", cellTextExpected: "20");
            Help.VerifyCellTextMatches(browser, ALP, Const_Mainpro.Table.CPDActitivitesListTabAct,
                rowName: Const_Mainpro.PLP_ActivityNames.PeerSupported.GetDescription(),
                colName: "Credits Applied", cellTextExpected: "20");
        }
        
              
        [Test]
        [Description("Given Iam currently launched Step1 first screen" +
            " when I jump to Steps other than Step1 current Screen" +
            " then the Next button should be disabled")]
        [Property("Status", "Complete")]
        [Author("Sindhu P")]
        public void VerifyNextBtnNotEnabledonNotStartedSteps()
        {
            /// 1.user after select peer or selfgudied navigate to step1 and check whether the Next button is enabled in the current screen
            Step1Page PS1 = Help.PLP_GoToStep(Browser, 1, TestContext.CurrentContext.Test);
            Assert.True(Browser.Exists(Bys.Step1Page.NextBtn, ElementCriteria.IsEnabled));

            /// 1.click tab number2 to get into Step2Page, verify whether the Next Button is disabled
            Step2Page PS2 = PS1.ClickAndWaitBasePage(PS1.PLP_Header_2Btn);
            Assert.True(Browser.Exists(Bys.Step2Page.NextBtn, ElementCriteria.AttributeValue("aria-disabled", "true")));
            Console.WriteLine("THE STEP2 NEXTBTN IS DISABLED");

            /// 2.click tab number3 to get into Step3Page, verify whether the NextBtn is disabled
            Step3Page PS3 = PS2.ClickAndWaitBasePage(PS2.PLP_Header_3Btn);
           // Thread.Sleep(10);
            Assert.True(Browser.Exists(Bys.Step3Page.NextBtn, ElementCriteria.AttributeValue("aria-disabled", "true")));
            Console.WriteLine("THE STEP3 NEXTBTN IS DISABLED");

            /// 3.click tab number4 to get into Step4Page, verify whether the NextBtn is disabled
            Step4Page PS4 = PS3.ClickAndWaitBasePage(PS3.PLP_Header_4Btn);
           // Thread.Sleep(10);
            Assert.True(Browser.Exists(Bys.Step4Page.NextBtn, ElementCriteria.AttributeValue("aria-disabled", "true")));
            Console.WriteLine("THE STEP4 NEXTBTN IS DISABLED");

            /// 4.click tab number5 to get into Step5Page, verify whether the NextBtn is disabled
            Step5Page PS5 = PS4.ClickAndWaitBasePage(PS4.PLP_Header_5Btn);
           // Thread.Sleep(10);
            Assert.True(Browser.Exists(Bys.Step5Page.NextBtn, ElementCriteria.AttributeValue("aria-disabled", "true")));
            Console.WriteLine("THE STEP5 NEXTBTN IS DISABLED");

            /// 5.click tab number_PR to get into StepPRPage, verify whether the NextBtn is disabled
            StepPRPage PS_PR = PS5.ClickAndWaitBasePage(PS5.PLP_Header_PRBtn);
            //Thread.Sleep(10);
            Assert.True(Browser.Exists(Bys.StepPRPage.NextBtn, ElementCriteria.AttributeValue("aria-disabled", "true")));
            Console.WriteLine("THE POST REFLECTION NEXTBTN IS DISABLED");


        }

        //[Test]
        //public void PPL_PeerSuppStep4()
        //{
        //    //string goalTitle = "Testing Goal 1 Title";
        //    /// 1. Verify that the goal appears, add a commitment to change, click Next, write first and last name, click Next
        //    ///Assert.True(Browser.Exists(By.XPath(string.Format("//span[text()='{0}']", goalTitle)), ElementCriteria.IsVisible));
        //    Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
        //    //PS4.ClickAndWait(PS4.NextBtn);
        //    //Help.PLP_AddFormattedText(Browser, "Commitment to change statement that addresses your goal(s): Please write [max 1000 characters]",
        //    //    "Testing Commitment to change");
        //    //PS4.ClickAndWait(PS4.NextBtn);
        //    //Help.PLP_EnterText(Browser, "First Name", "testing first name");
        //    //Help.PLP_EnterText(Browser, "Last Name", "testing last name");
        //    //PS4.ClickAndWait(PS4.NextBtn);
        //    //PS4.ClickAndWait(PS4.SubmitBtn);
        //    //PS4.ClickAndWait(PS4.NextBtn);
        //    Step3Page PS3 = PS5.ClickAndWaitBasePage(PS5.PPL_Header_StepNumber3);
        //    Assert.True(Browser.Exists(Bys.Step3Page.PLP_Checkbox, ElementCriteria.AttributeValue("disabled", "disabled")));
        //    Console.WriteLine("THE STEP3 CHECKBOX IS DISABLED");
        //}


        #endregion Tests

    }




}