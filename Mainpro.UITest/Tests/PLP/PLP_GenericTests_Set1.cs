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
    public class PLP_GenericTests_Set1 : TestBase
    {
        #region Constructors
        public PLP_GenericTests_Set1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_GenericTests_Set1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        // [Test]
        [Description("")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PLP_Demo()
        {
            //Help.PLP_GoToStep(Browser, 4, TestContext.CurrentContext.Test, username: "TestAuto787_Nov-16-21_15-5_PLP_Demo",
            //    password: password);

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
            Step4Page PS4 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test, isSelfGuided: false, iIconClickCheck: false);


        }

        //   [Test]
        [Description("")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PLP_RegisterPLPLaunchFromLTSThenBackToPLP()
        {
            //            1.Take a user and register to PLP
            //2.Complete until Step 3
            //3.Click the top level navigation of Step 1
            //4.Exit to Mainpro+
            //5.Launch the same user again from LTST
            //6.Verify that the register to PLP screen doesn’t show up again
            //7.The test can be done with the same user or different user everytime.

            for (int i = 0; i < 20; i++)
            {
                UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
                Step3Page PS3 = Help.PLP_GoToStep(Browser, 3, TestContext.CurrentContext.Test,
                    isNewUser: true, username: newUser.Username, password: password);
                PS3.ClickAndWaitBasePage(PS3.PLP_Menu_DropDownBtn);
                DashboardPage DP = PS3.ClickAndWaitBasePage(PS3.PLP_Menu_ExitToMainpro);
                LSHelp.Login(browser, TestContext.CurrentContext.Test.Name, AppSettings.Config["ltspassword"]);
                LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", newUser.Username);
                DP.WaitForInitialize();
                Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
                DP.ClickAndWait(DP.EnterBtn);
                Assert.True(Browser.Exists(By.XPath("//h4[contains(text(), 'Step 3')]")));
            }

        }

        

        [Test]
        [Description("Verify user add 2 goals in Step 3 and those 2 goals should be reflected in Step 4 ")]
        [Property("Status", "Complete")]
        [Author("Bama Thangaraj")]
        public void Add2goalsVerifyItReflectsinStep4()
        {
            // Create a new user
            UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            String username = newUser.Username;
            String password = newUser.Password;

            String Goal1Title = "Testing Goal 1";
            String Goal2Title = "Testing Goal 2";
            String TitleOfGap1 = "Testing gap 1";
            String TitleOfGap2 = "Testing gap 2";
            String TitleOfGap3 = "Testing gap 3";


            // 1. Enter into PLP Module, Complete Step1 and Step 2 stage by Adding 3 Gaps and fill all the required fields
            Step3Page PS3 = Help.PLP_GoToStep(Browser, 3, TestContext.CurrentContext.Test,
               isNewUser: true, username: username, password: password,
               isSelfGuided: true, NumOfGaps: 3, TitleOfGap1: TitleOfGap1,
               TitleOfGap2: TitleOfGap2, TitleOfGap3: TitleOfGap3
               );
            PS3.ClickAndWait(PS3.NextBtn);

            //2. Now in Step 3, Add Goal 1 , Choose Gap 3 only and fill the Goal1 screens(SMARTGoalDetails, AddressingGoalNeeds, GoalOutcomes) by filling required fields
            //3. Choose Yes for AddAnotherGoal Question

            #region Adding Goal 1

            PS3.ChooseGaps(new List<string>() { "Gap 3 (Testing gap 3)" });
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillSMARTGoalDetails(Browser, Goal1Title, Const_Mainpro.PLP_TextboxlabelText.Step3Goal1TitleTxt);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillAddressingGoalNeeds(Browser);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillGoalOutcomes(Browser);
            PS3.AddAnotherGoalYesRdo.Click();
            PS3.ClickAndWait(PS3.NextBtn);
            #endregion
            //4. Now, Add Goal 2, Choose all Gaps 1, 2, 3 checkboxes and fill the Goal2 screens(SMARTGoalDetails, AddressingGoalNeeds, GoalOutcomes) by filling required fields
            //5. Choose No for AddAnotherGoal Question and Complete the Step 3 screens by filling subsequent screens

            #region Adding Goal 2

            PS3.ChooseGaps(new List<string>() { "Gap 1 (Testing gap 1)",
                "Gap 2 (Testing gap 2)","Gap 3 (Testing gap 3)" });
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillSMARTGoalDetails(Browser, Goal2Title, Const_Mainpro.PLP_TextboxlabelText.Step3Goal2TitleTxt);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillAddressingGoalNeeds(Browser);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillGoalOutcomes(Browser);
            PS3.AddAnotherGoalNoRdo.Click();
            PS3.ClickAndWait(PS3.NextBtn);
            #endregion  

            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "0-3 months");
            PS3.ClickAndWait(PS3.NextBtn);
            Step4Page PS4 = PS3.ClickAndWait(PS3.NextBtn);
            //6. Now in step4, verify that CTC screen is displayed for Goal1 and Goal 2 only and Goal3 CTC screen is not displayed
            PS4.FillStep4Screens(isSelfGuided: true, goalTitles: new List<string>() { Goal1Title, Goal2Title });
        }

        [Test]
        [Description("Verify user add 3 goals in Step 3 and AddAnotherGoal" +
            " Question should not be displayed after 3 goals and in Step 4 ," +
            " those 3 goals should be reflected ")]
        [Property("Status", "Complete")]
        [Author("Bama Thangaraj")]
        public void Add3goalsVerifyItReflectsinStep4()
        {
            // Create a new user
            UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            String username = newUser.Username;
            String password = newUser.Password;

            String Goal1Title = "Testing Goal 1";
            String Goal2Title = "Testing Goal 2";
            String Goal3Title = "Testing Goal 3";
            String TitleOfGap1 = "Testing gap 1";


            // 1. Enter into PLP Module, Complete Step1 and Step 2 stage by Adding 1 Gap and fill all the required fields
            Step3Page PS3 = Help.PLP_GoToStep(Browser, 3, TestContext.CurrentContext.Test,
               isNewUser: true, username: username, password: password,
               isSelfGuided: true, NumOfGaps: 1, TitleOfGap1: TitleOfGap1
               );
            PS3.ClickAndWait(PS3.NextBtn);
            //2. Now in Step 3, Add Goal 1 , Choose Gap 1 only and fill the Goal1 screens(SMARTGoalDetails, AddressingGoalNeeds, GoalOutcomes) by filling required fields
            //3. Choose Yes for AddAnotherGoal Question

            #region Adding Goal 1

            PS3.ChooseGaps(new List<string>() { "Gap 1 (Testing gap 1)" });
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillSMARTGoalDetails(Browser, Goal1Title, Const_Mainpro.PLP_TextboxlabelText.Step3Goal1TitleTxt);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillAddressingGoalNeeds(Browser);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillGoalOutcomes(Browser);
            PS3.AddAnotherGoalYesRdo.Click();
            PS3.ClickAndWait(PS3.NextBtn);
            #endregion
            //4.  Now, Add Goal 2, Choose Gap 1 checkbox and fill the Goal2 screens(SMARTGoalDetails, AddressingGoalNeeds, GoalOutcomes) by filling required fields
            //5. Choose Yes for AddAnotherGoal Question

            #region Adding Goal 2

            PS3.ChooseGaps(new List<string>() { "Gap 1 (Testing gap 1)"});
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillSMARTGoalDetails(Browser, Goal2Title, Const_Mainpro.PLP_TextboxlabelText.Step3Goal2TitleTxt);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillAddressingGoalNeeds(Browser);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillGoalOutcomes(Browser);
            PS3.AddAnotherGoalYesRdo.Click();
            PS3.NextBtn.ClickJS(Browser);
            Browser.WaitJSAndJQuery();
            #endregion
            //6. Now, Add Goal 3, Choose Gap 1 checkbox and fill the Goal3 screens(SMARTGoalDetails, AddressingGoalNeeds, GoalOutcomes) by filling required fields

            //7. Verify that AddAnotherGoal Question should not be displayed and Complete the Step 3 screens by filling subsequent screens

            #region Adding Goal 3

            PS3.ChooseGaps(new List<string>() { "Gap 1 (Testing gap 1)"});
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillSMARTGoalDetails(Browser, Goal3Title, Const_Mainpro.PLP_TextboxlabelText.Step3Goal3TitleTxt);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillAddressingGoalNeeds(Browser);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillGoalOutcomes(Browser);
            Assert.False(Browser.Exists(Bys.Step3Page.AddAnotherGoalYesRdo), "" +
                "AddAnotherGoal Question should not be displayed after adding 3 goals");
            PS3.ClickAndWait(PS3.NextBtn);
            #endregion  

            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "0-3 months");
            PS3.ClickAndWait(PS3.NextBtn);
            Step4Page PS4 = PS3.ClickAndWait(PS3.NextBtn);
            //8. Now in step4, verify that CTC screen is displayed for Goal1 and Goal 2 CTC and Goal3 CTC screen is displayed
            PS4.FillStep4Screens(isSelfGuided: true, goalTitles: new List<string>() { Goal1Title, Goal2Title, Goal3Title });
        }

       

    }

    #endregion Tests
}
