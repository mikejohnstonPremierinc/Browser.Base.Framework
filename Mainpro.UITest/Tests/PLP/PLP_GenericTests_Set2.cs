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
    public class PLP_GenericTests_Set2 : TestBase
    {
        #region Constructors
        public PLP_GenericTests_Set2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_GenericTests_Set2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

       
        [Test]
        [Description("This Test Verifies the below: " +
            "1)Verify the % shows in PLP progress Chart in Dashboard matched with" +
            " the chart inside the PLP Module" +
            "2)Verify the user left of at 10% and on 2nd time login ( from Login URL )," +
            " user should lands on the same step where user left off" +
            "3)Verify the user left of at 10% and on 2nd time login (from LTST URL )," +
            " user should lands on the same step where user left off" +
            "4)Verify Learnmore Link opens up a PLP info new Tab" +
            "5)Verify Contact us popup from Userprofile section" +
            "6)Verify Exit to Mainpro+ from Userprofile section, Returns to Dashboard page")]
        [Property("Status", "Complete")]
        [Author("Bama Thangaraj")]
        public void LeftUserResumesVerifyProgress()
        {
            // Create a new user
            UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            String username = newUser.Username;
            String password = newUser.Password;

            // 1. Enter into PLP Module, Complete Step1 stage only and
            // get the PLP completion percentage (ex: 10%) from plp module 
            Step2Page PS2 = Help.PLP_GoToStep(Browser, 2, TestContext.CurrentContext.Test,
               isNewUser: true, username: username, password: password);
            String PLPPercentageValue = PS2.PLPTopPercentChartTxt.Text;

            PS2.ClickAndWaitBasePage(PS2.PLP_Menu_DropDownBtn);
            DashboardPage DP = PS2.ClickAndWaitBasePage(PS2.PLP_Menu_ExitToMainpro);

            // 2. Click on LearnMore button and Verify that it is opening the new PLP info Tab 
            // then switch to Dashboard Tab 
            DP.LearnMoreBtn.Click();
            Browser.SwitchTo().Window(Browser.WindowHandles[1]);
            Assert.True(Browser.Url.Contains("professional-learning-plan"),
                "it should open the new PLP info Tab ");
            Browser.Close();
            Browser.SwitchTo().Window(Browser.WindowHandles[0]);

            // Verify that in Dashboard, PLP completion percentage(ex: 10%) is displaying same value
            // as inside the PLP module and the Chart not filled with Full Green color(because only 10%)
            Assert.True(DP.MainproDashboardPLPCompletePercentageLbl.Text.Equals(PLPPercentageValue),
                "Dashboard should show PLP completion percentage(ex: 10%) as same as value" +
                " inside the PLP module");
            Assert.True(DP.MainproDashboardPLPChartWhiteareaLbl.Displayed,
                "Chart should not be filled with Full Green color(because only 10% completed)");
            DP.ClickAndWaitBasePage(DP.LogoutLnk);

            // Relogin the user again from Login URL 
            // and Enter into PLP from Dahboard page Then
            // the user should be launched to the same step where they left from PLP module
            // and the completion percentage(ex: 10%) is showing correctly              
            LoginPage LP = Navigation.GoToLoginPage(browser);
            DashboardPage DP1 = LP.Login(username, password);
            DP1.ClickAndWait(DP1.EnterBtn);
            Assert.True(PS2.PLP_Header_StepNumberLabel.Text.Contains("Step 2"));
            Assert.True(PS2.PLPTopPercentChartTxt.Text.Equals(PLPPercentageValue));

            // Relaunch the user again from LTST and Enter into PLP from Dahboard page Then
            // the user should launched to the same step where they left from PLP module
            // and the completion percentage(ex: 10%) is showing correctly  
            LSHelp.Login(browser, "PLP_RegisterPLPLaunchFromLTSThenBackToPLP", AppSettings.Config["ltspassword"]);
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", username);
            DP.WaitForInitialize();
            Help.SwitchToRewriteAfterLaunchingFromLTST(browser);
            DP.ClickAndWait(DP.EnterBtn);
            Assert.True(PS2.PLP_Header_StepNumberLabel.Text.Contains("Step 2"));
            Assert.True(PS2.PLPTopPercentChartTxt.Text.Equals(PLPPercentageValue));

        }
               

        [Test]
        [Description("This Test Verifies the below:" +
            "1)Verify that Delete button should not show if only one gap" +
            "2)Verifies AdditionalGap Btn is disabled  after adding all three Gaps" +
            "3)Delete the Gap2 and Gap 1, verify those are removed and only Gap 3 Retained" +
            "4)Move to next page from IdentifyGaps and come back again to verify the additionalgap button and Delete button are disabled" +
            "5)Verfies the next button is enabled without selecting any options in CanMEDS-FM roles page" +
            "6)Verify in resource screen, Other textbox appears only if user selects the option 'I require more data ..'" +
                        "and the textbox should be filled mandatorily to enable the Next button" +
            "7)Verifies only Gap3 reflected in Step3-gap1goalselection screen, CPDSuggesstion screen-Title, Gaps dropdowns" +
            "8)Verifies that user can go back to step2, edit the gap again, and it reflects in all step 3 screens and summary page")]
        [Property("Status", "Complete")]
        [Author("Bama Thangaraj")]
        public void VerifyAddEditDeleteGapsinStep2ReflectsinStep3()
        {
            /* // To Debug this testcase - use this commented lines 
             LoginPage LP = Navigation.GoToLoginPage(browser);
             DashboardPage DP = LP.Login("TestAuto32F_Mar-29-22_4-28_VerifyAddEditDeleteGapsinStep2andReflectsinStep3",
                isNewUser: false);
             DP.ClickAndWait(DP.EnterBtn);
             Step2Page PS2 = new Step2Page(browser);*/

            String Goal1Title = "Testing Goal 1";

            String TitleOfGap1 = "Testing gap 1";
            String TitleOfGap2 = "Testing gap 2";
            String TitleOfGap3 = "Testing gap 3"; String TitleOfGap3_Edited = "Testing gap 3 Edited";

            // 1. Create a new user
            UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: TestContext.CurrentContext.Test);
            String username = newUser.Username;
            String password = newUser.Password;

            // 2. Enter into PLP module and complete Step1 all screens
            Step2Page PS2 = Help.PLP_GoToStep(Browser, 2, TestContext.CurrentContext.Test,
               isNewUser: true, username: username, password: password,
               isSelfGuided: true, NumOfGaps: 3, TitleOfGap1: TitleOfGap1,
               TitleOfGap2: TitleOfGap2, TitleOfGap3: TitleOfGap3
               );

            // Fill Step 2 - Fill Primary and Subdomains 
            PS2.ClickAndWait(PS2.NextBtn);
            PS2.SelectPrimaryandSubDomains();

            //Verify that Delete button should not show if only one gap 
            Assert.False(PS2.Gap1DeleteBtn.Displayed, "Delete button should not show if only one gap");
            //Add 3 gaps
            Help.PLP_AddFormattedText(Browser, TitleOfGap1, Const_Mainpro.PLP_TextboxlabelText.Step2Gap1TitleTxt);
            PS2.ClickAndWait(PS2.AdditionalGapBtn);
            Help.PLP_AddFormattedText(Browser, TitleOfGap2, Const_Mainpro.PLP_TextboxlabelText.Step2Gap2TitleTxt);
            PS2.ClickAndWait(PS2.AdditionalGapBtn);
            Help.PLP_AddFormattedText(Browser, TitleOfGap3, Const_Mainpro.PLP_TextboxlabelText.Step2Gap3TitleTxt);

            //Verifies AdditionalGap Btn is disabled  after adding all three Gaps
            Assert.AreEqual("true", PS2.AdditionalGapBtn.GetAttribute("aria-disabled"),
                "Once Added three Gaps then Additional Gap Button should be disabled");

            //Delete the Gap2 and verify Gap2 is removed 
            PS2.ClickAndWait(PS2.Gap2DeleteBtn);
            Assert.False(Browser.PageSource.Contains(TitleOfGap2), "Gap2 entry should not show after gap2 deleted");
            //Delete the Gap1 and verify Gap1 is removed 
            PS2.ClickAndWait(PS2.Gap1DeleteBtn);
            Assert.False(Browser.PageSource.Contains(TitleOfGap1), "Gap1 entry should not show after gap1 deleted");
            //Now only Gap3 remains in the page and it becomes the Gap#1 , Delete button should not show if only one gap 
            Assert.True(Browser.PageSource.Contains(TitleOfGap3), "Gap1 and Gap 2 are deleted, So only Gap 3 entry should show");
            Assert.False(PS2.Gap1DeleteBtn.Displayed, "Delete button should not show if only one gap");

            //Now Gap3 goes top to gap#1, click next page and come back again to verify the additionalgap buttons,
            //Domain of Care, Sub Domain of care is disabled,
            PS2.ClickAndWait(PS2.GapNextBtn); PS2.ClickAndWait(PS2.GapsContinueBtn);
            PS2.ClickAndWait(PS2.BackBtn);
            Assert.AreEqual("true", PS2.AdditionalGapBtn.GetAttribute("aria-disabled"),
                "Additional Gap Btn should be disabled while coming back from next screen");
            try
            {
                Assert.AreEqual("true", PS2.SelectDomainOfCareSelElem.SelectedOption.GetAttribute("disabled"),
                    "Domain of care dropdown options should be disabled while coming back from next screen");
                Assert.AreEqual("true", PS2.SelectSubsetsSelElem.SelectedOption.GetAttribute("disabled"),
                    "SubDomain dropdown options should be disabled while coming back from next screen");
            }
            catch { Console.WriteLine("domain dropdown"); }

            PS2.ClickAndWait(PS2.GapScreenViewModeNextBtn);

            //Verfies the next is enabled without selecting any options in CanMEDS-FM roles page
            Assert.True(Browser.Exists(Bys.Step2Page.NextBtn, ElementCriteria.IsEnabled),
                "CanmedFmrole screen is not mandatory to be filled and " +
                "user is optional to choose values in the screen");
            PS2.ClickAndWait(PS2.NextBtn);
            PS2.ClickAndWait(PS2.NextBtn);

            #region Verify in resource screen, Other textbox appears only if user selects the option "I require more data .." and the textbox should be filled mandatorily to enable the Next button

            Assert.True(Browser.Exists(Bys.Step2Page.OtherDataTxt,
                ElementCriteria.HasAttribute("disabled")),
                "User doesnot check 'I require more data' option, SO OtherDataToAccessYourPracticeNeeds TextBox " +
                "should not been shown");
            ElemSet.ScrollToElement(Browser, PS2.BackBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "I require more data to assess my practice");
            ElemSet.SendKeysAfterScroll(Browser, PS2.OtherDataTxt, "Other Data");
            PS2.ClickAndWait(PS2.NextBtn);
            PS2.ClickAndWait(PS2.NextBtn);
            #endregion 

            //In Step3 - Verify that only Gap3 Title Reflected in Gap selection of Goal screen
            Step3Page PS3 = new Step3Page(Browser);
            #region Adding Goal 1
            Assert.False(Browser.Exists(Bys.Step3Page.Gap2Chk), "Should not display Gap2 because Gap2 is deleted in Step2");
            Assert.False(Browser.Exists(Bys.Step3Page.Gap3Chk), "Should not display Gap1 because Gap1 is deleted in Step2");
            Assert.True(Browser.Exists(Bys.Step3Page.Gap1Chk, ElementCriteria.TextContains(TitleOfGap3))
                , "Should display Gap3 Text because Gap3 Text is not deleted in Step2");
            PS3.Gap1Chk.Click();
            // Fill Remaining screens in step 3
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillSMARTGoalDetails(Browser, Goal1Title, Const_Mainpro.PLP_TextboxlabelText.Step3Goal1TitleTxt);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillAddressingGoalNeeds(Browser);
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.FillGoalOutcomes(Browser);
            PS3.AddAnotherGoalNoRdo.Click();
            PS3.ClickAndWait(PS3.NextBtn);
            #endregion  

            PS3.ClickAndWait(PS3.NextBtn);
            Assert.True(Browser.Exists(By.ClassName("gap1"), ElementCriteria.TextContains(TitleOfGap3))
                , "Should display Gap3 Text because Gap3 Text is not deleted in Step2");
            Assert.False(Browser.Exists(By.ClassName("gap2")), "Should Not display 2nd Gap because only one gap in Step2");
            Assert.False(Browser.Exists(By.ClassName("gap3")), "Should Not display 3rd Gap because only one gap in Step2");

            Assert.True(PS3.ActivityTblGapsSelElem.Options.Count.Equals(1) &&
               PS3.ActivityTblGapsSelElem.Options[0].Text.Equals("Gap 1"), "Should show only one Gap in Gaps dropdown");
            PS3.ClickAndWait(PS3.PlusActivitiesBtn);
            Assert.True(PS3.ActivityDetailFormGapsSelElem.Options.Count.Equals(1) &&
                PS3.ActivityDetailFormGapsSelElem.Options[0].Text.Equals("Gap 1"), "Should show only one Gap in Gaps dropdown");
            //  PS3.ClickAndWait(PS3.PLP_ActivityDetailFormCancelBtn); cancel click code doesnt work; needs fix 
            PS3.RefreshPage(true);// this line is not needed If the above line Cancel click worked.
            PS3.ClickAndWait(PS3.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "0-3 months");
            PS3.ClickAndWait(PS3.NextBtn);

            #region Verify Added Gaps Title can be edited again in step2 and edited value reflected in Step3
            //Go Back to Step 2 by clicking header label and Edit the Gap Text 
            Step2Page PS2E = PS3.ClickAndWaitBasePage(PS3.PLP_Header_2Btn);
            PS2E.ClickAndWait(PS2E.NextBtn);
            Help.PLP_AddFormattedText(Browser, TitleOfGap3_Edited, Const_Mainpro.PLP_TextboxlabelText.Step2Gap1TitleTxt,
                clearText: true);
            Assert.False(PS2.Gap2EditBtn.Displayed && PS2.Gap3EditBtn.Displayed, "Gap2 and 3 button should not show");
            PS2E.ClickAndWait(PS2E.GapScreenViewModeNextBtn);

            //Go Back to Step 3 Intro Page by clicking header label 
            Step3Page PS3E = PS2.ClickAndWaitBasePage(PS2.PLP_Header_3Btn);
            PS3E.ClickAndWait(PS3E.NextBtn);
            //verify edited gap text displayed in step3 gap1goalselection screen and click Next button
            Assert.True(Browser.Exists(Bys.Step3Page.Gap1Chk, ElementCriteria.TextContains(TitleOfGap3_Edited))
                , "Should display [" + TitleOfGap3_Edited + "] because Gap Text is edited in Step2");
            PS3E.ClickAndWait(PS3E.NextBtn);
            //click Next from smartoverview screen
            PS3E.ClickAndWait(PS3E.NextBtn);
            //click Next from goal1detail screen
            PS3E.ClickAndWait(PS3E.NextBtn);
            //click Next from addressinggoal1needs screen
            PS3E.ClickAndWait(PS3E.NextBtn);
            //click Next from goal1outcomes screen
            PS3E.ClickAndWait(PS3E.NextBtn);
            //click Next from goalinstructions screen
            PS3E.ClickAndWait(PS3E.NextBtn);

            // Verifies Edited Gap texts are reflected in PLP CPDSuggesstion activity Page
            Assert.True(Browser.Exists(By.ClassName("gap1"), ElementCriteria.TextContains(TitleOfGap3_Edited))
                , "Should display Gap3 Text because Gap3 Text is not deleted in Step2");
            PS3E.ClickAndWait(PS3E.NextBtn);
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "0-3 months");
            PS3E.ClickAndWait(PS3E.NextBtn);
            //Verifies Edited Gap texts are reflected in PLP Activity Summary Page
            Assert.True(ElemGet.Grid_GetRowCount(Browser, PS3E.PLPStep3IdentifiedGapsTblBody).Equals(1),
                "Should show only one Gap row");
            Assert.True(
            ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, PS3E.PLPStep3IdentifiedGapsTbl,
                Bys.Step3Page.PLPStep3IdentifiedGapsTblBodyFirstRow, 1, 0).Contains(TitleOfGap3_Edited),
            "Should show only one Gap row and Should display Gap3 Edited Text");
            #endregion

        }

       
    }

    #endregion Tests
}
