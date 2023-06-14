using Browser.Core.Framework;
using LMS.Data;
using LS.AppFramework.Constants_LTS;
using LS.AppFramework.HelperMethods;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Mainpro.AppFramework
{
    public class MainproHelperMethods
    {
        #region properties
        public LSHelperMethods LSHelp = new LSHelperMethods();
        public static TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        public static DateTime currentDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);

        #endregion properties

        #region methods


        #region methods: random


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFirstAndLastName(IWebDriver Browser)
        {
            string firstAndLastName = Browser.FindElement(Bys.MainproPage.WelcomeLbl).Text.Substring(8);
            return firstAndLastName;
        }

        /// <summary>
        /// Some fields so far dont clear with the Clear() method. So I created a method to condition for this.
        /// </summary>
        public void ClearTextBox(IWebElement elem)
        {
            elem.Clear();
            int numberOfCharactersInElement = elem.GetAttribute("value").Length;
            if (numberOfCharactersInElement > 0)
            {
                elem.Click();
                for (int i = 0; i < numberOfCharactersInElement; i++)
                {
                    elem.SendKeys(Keys.Backspace);
                }
            }
        }

        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly, 
        /// clicks Enter on the Dashboard page, registers to PLP, then goes through every required step to reach 
        /// the desired step page. You can not use this method if your user has already registered to PLP because the 
        /// application launches the user to the last PLP page the user was on, and this method assumes the user
        /// will land on the register page, it is not conditioned to pick up where an existing PLP page was already accessed
        /// method assumes the user will 
        /// </summary>
        /// <summary>
        /// Creates a new user, logs into Mainpro, then goes through every required step to reach the desired step page 
        /// of the tester
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="currentTest"></param>
        /// <param name="stepNumber">The step number page you want to land on</param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. 
        /// Default = false</param>
        /// <param name="username">(Optional). If you are not logged in already and you want to specify an existing user to 
        /// login with, enter the username. If you dont specify this and you are not logged in yet, and then this method 
        /// will generate a username for you and login with that user</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation 
        /// users</param>
        public dynamic PLP_GoToStep(IWebDriver Browser, int stepNumber, TestContext.TestAdapter currentTest,
            bool isNewUser = false, string username = null, string password = null, bool isSelfGuided=false,
            bool iIconClickCheck=false, bool ToolsOptionsClickFromUserProfileCheck=false,
            bool isCPDSuggestionActivtyTblCheck = false,
            String primarydomain=null, List<string> subdomains=null,
            List<string> chooseGaps = null,int NumOfGaps=2, String TitleOfGap1="Testing gap 1",
            String TitleOfGap2="Testing gap 2", String TitleOfGap3 = "Testing gap 3")
        {
            LoginPage LP = new LoginPage(Browser);
            DashboardPage DP = new DashboardPage(Browser);
            EntryCarouselPathwayPage ECP = new EntryCarouselPathwayPage(Browser);
            Step1Page PS1 = new Step1Page(Browser);
            Step2Page PS2 = new Step2Page(Browser);
            Step3Page PS3 = new Step3Page(Browser);
            Step4Page PS4 = new Step4Page(Browser);
            Step5Page PS5 = new Step5Page(Browser);
            StepPRPage PSPR = new StepPRPage(Browser);
            string goalTitle = "Testing Goal 1 Title";
            PLP_Event chosenEvent = null;
            PLP_Event addedEvent = null;
            PLP_Event step5_addedEvent = null;

            #region login section
            // If not logged in already
            if (!Browser.Exists(Bys.MainproPage.LogoutLnk))
            {
                if (!Browser.Exists(Bys.MainproPage.PLP_Menu_DropDownBtn))
                {
                    Navigation.GoToLoginPage(Browser);

                    // If the tester wants a specific user to login with, login with that user
                    if (!string.IsNullOrEmpty(username))
                    {
                        LP.Login(username, password, isNewUser);
                    }
                    // else login with a new user from API
                    else
                    {
                        UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: currentTest);
                        LP.Login(newUser.Username, newUser.Password, true);
                        Console.WriteLine(String.Format("Tested User [{0}] and password [{1}]",
                            newUser.Username, newUser.Password));
                    }
                }
            }

            // If not logged in with a user that the tester specified
            if (!string.IsNullOrEmpty(username))
            {
                if (APIHelp.GetUserName(Browser).ToLower() != username.ToLower())
                {
                    // if on PLP, return to Mainpro, then logout
                    if (true)
                    {
                        DP.ClickAndWaitBasePage(DP.PLP_Menu_DropDownBtn);
                        DP.ClickAndWaitBasePage(DP.PLP_Menu_ExitToMainpro);
                    }
                    DP.ClickAndWaitBasePage(DP.LogoutLnk);
                    Navigation.GoToLoginPage(Browser);
                    LP.Login(username, password, isNewUser);
                    Console.WriteLine(String.Format("Tested User [{0}] and password [{1}]", username, password));
                }
            }

            #endregion login section

            #region Section - From Dashboard to PLP Pathway selection 

            /// Verify the 0 % shows in mainpro+ Dashboard PLP Section for New User 
            /// who enter into PLP Module for First time 
            if (string.IsNullOrEmpty(username))
            {
                //if PLP chart does not loaded for First time ; then refresh the page
                if (!Browser.Exists(Bys.MainproPage.PLPPercentChartTxt))
                {
                    Console.WriteLine("First time not loaded the chart");
                    DP.RefreshPage(true);
                }
                Assert.True(Browser.FindElement(Bys.MainproPage.PLPPercentChartTxt).Text.Contains("0%"),
                    "if new user enter for first time then PLP Percentage should be 0% ");
            }
            /// 1. Launch into PLP, verify the PLP Activity Overview slides load properly after clicking each 
            /// carousel button beneath them, then choose the Peer Supported option
            DP.ClickAndWait(DP.EnterBtn);
            
            ECP.ClickAndWait(ECP.EnterBtn);
            PLP_ReviewPLPActivityOverview(Browser);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);

            /// 2. Click the Begin button for the Peer Supported/Selfguided section as per user input, 
            /// click the Confirm button, then click Next until the 
            /// Domains of Care page is presented to the user
            if (isSelfGuided)
            {
                ECP.ClickAndWait(ECP.SelfGuidedBeginBtn);
                ECP.ClickAndWait(ECP.SelfGuidedModalPleaseConfirmFormConfirmBtn);
            }
            else
            {
                ECP.ClickAndWait(ECP.PeerSupportedBeginBtn);
                ECP.ClickAndWait(ECP.PeerSupportedPleaseConfirmFormConfirmBtn);
            }
            #endregion

            #region Section - Step1 all screens
            if (stepNumber == 1)
            {
                return PS1;
            }
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);
            PS1.ClickAndWait(PS1.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);
            PS1.ClickAndWait(PS1.NextBtn);

            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);
            /// 3. Expand all sections within the Domains of Care page, choose a check box within each section
            PS1.ClickAndWait(PS1.ExpandAllBtn);
            PLP_ClickCheckBoxOrRadioButton(Browser, "Committee chair/member");
            PLP_ClickCheckBoxOrRadioButton(Browser, "Clinical preceptor");
            PLP_ClickCheckBoxOrRadioButton(Browser, "Certificate examinations");
            PLP_ClickCheckBoxOrRadioButton(Browser, "Educational");
            PLP_ClickCheckBoxOrRadioButton(Browser, "Educational");
            PLP_ClickCheckBoxOrRadioButton(Browser, "Hospital care");

            /// 4. Assert that the Next button is disabled until the "To continue, please confirm..." check box is checked, then 
            /// click the Next button
            Assert.AreEqual("true", PS1.NextBtn.GetAttribute("aria-disabled"));
            PLP_ClickCheckBoxOrRadioButton(Browser, "To continue, please confirm you have reviewed all domains of care categories.");
            PS1.ClickAndWait(PS1.NextBtn);
            
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);

            /// 5. Verify percentageperdomain chart displays with 0% and full circle with grey color when 0%
            /// Enter "1" into the Other field then assert the Other Professional Practice field appears
            Assert.AreEqual("0%", PS1.TimeBreakdownGraphLabel.Text);
            Assert.True(PS1.TimeBreakdownGraphGreyColorLabel.Displayed,
                 "Chart should be filled with Full Grey color(When 0%)");
            PLP_EnterText(Browser, "Other", "1", "Other");
            PS1.BackBtn.SendKeys(Keys.Tab);
            Thread.Sleep(500);
            PLP_EnterText(Browser, "Other professional practice", "testing", "Other professional practice");

            /// 6. Assert the graph displays 1% then assert the Next button is disabled until the graph displays 100%
            /// And Verify percentageperdomain chart displays grey color when 1 % complete and 99% incomplete
            /// And Verify percentageperdomain chart should not display grey color when 100%
            Assert.AreEqual("1%", PS1.TimeBreakdownGraphLabel.Text);
            Assert.True(PS1.TimeBreakdownGraphGreyColorLabel.Displayed,
                "Chart should show with Grey color(because only 1% completed; Remaining 99 % should in Grey)");
            Assert.AreEqual("true", PS1.NextBtn.GetAttribute("aria-disabled"));
            PLP_EnterText(Browser, "Administration", "95", "Administration");
            PLP_EnterText(Browser, "Education & teaching", "1", "Education & teaching");
            PLP_EnterText(Browser, "Research & scholarship", "1", "Research & scholarship");
            PLP_EnterText(Browser, "QA/QI", "1", "QA/QI");
            PLP_EnterText(Browser, "Clinical care", "1", "Clinical care");
            PS1.BackBtn.SendKeys(Keys.Tab);
            Thread.Sleep(500);
            Assert.AreEqual("false", PS1.NextBtn.GetAttribute("aria-disabled"));
            Assert.False(Browser.Exists(Bys.Step1Page.TimeBreakdownGraphGreyColorLabel),
                "Chart should not show Grey color(because 100% completed)");

            /// 7. Click the next button, choose a characteristic, click Next again, choose a patient population, then click Next
            /// to navigate to Step 2

            PS1.ClickAndWait(PS1.NextBtn);
            PLP_ClickCheckBoxOrRadioButton(Browser, "Solo practice");
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);
            PS1.ClickAndWait(PS1.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);
            PLP_ClickCheckBoxOrRadioButton(Browser, "Indigenous");
            PS1.ClickAndWait(PS1.NextBtn);
            
            #endregion

            #region Section - Step2 all screens
            if (stepNumber == 2)
            {
                return PS2;
            }
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS2.ClickAndWait(PS2.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);
            /// 8. Choose a Domain of Care from the dropdown, choose 2 Subsets, then add a Gap and click Next
            PLP_DomainsSelection SelectedDomainsList=
                PS2.SelectPrimaryandSubDomains(primarydomain, subdomains);

            // Add Gaps 
            PS2.AddGaps(NumOfGaps, TitleOfGap1,TitleOfGap2,TitleOfGap3);
            PS2.ClickAndWait(PS2.GapNextBtn);
            
            //Thread.Sleep(20);
            /// 9. Click Next on each page choosing options from the check boxes until Step 3 appears
            //PS2.ClickAndWait(PS2.NextBtn);
            PS2.ClickAndWait(PS2.GapsContinueBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            //verify canmedFmrole screen is not mandatory to be filled and user is optional to choose values in the screen
            PLP_ClickCheckBoxOrRadioButton(Browser, "Family Medicine Expert");
            PS2.ClickAndWait(PS2.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS2.ClickAndWait(PS2.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PLP_ClickCheckBoxOrRadioButton(Browser, "Clinical practice audits and reports derived from your EMR");
            PS2.ClickAndWait(PS2.NextBtn);

            //verify "Working with a peer or colleague" screen applicable only for PeerVersion 
            if (!isSelfGuided)
            {
                //verify tools and resource section
                PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
                // if the pathway is PeerVersion then
                // "Working with a peer or colleague" screen will be displayed
                PS2.ClickAndWait(PS2.NextBtn);
                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
            #endregion

            #region Section - Step3 all screens 
            if (stepNumber == 3)
            {
                return PS3;
            }
            //Verify the PLP Activity Summary option under User Profile Menu, is Not enabled
            //  until user completes Step 3 of the activity.
            PS3.VerfiyEnabledOrDisabled_PLP_Menu_PLPActivitySummmaryOption(isEnabled:false);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck,ToolsOptionsClickFromUserProfileCheck);
            // 10. Click next, choose a gap, click next until Goal 1 page appears, then enter information in each one of the
            // formatted text controls");
            PS3.ClickAndWait(PS3.NextBtn);

            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS3.ChooseGaps(new List<string>(){ "Gap 1 (Testing gap 1)" , "Gap 2 (Testing gap 2)" });            
            PS3.ClickAndWait(PS3.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS3.ClickAndWait(PS3.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS3.FillSMARTGoalDetails( Browser,goalTitle,
                Const_Mainpro.PLP_TextboxlabelText.Step3Goal1TitleTxt);
            // 11. Click next then add text into the How Will This Goal Address Your Gap control" 
            PS3.ClickAndWait(PS3.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS3.FillAddressingGoalNeeds(Browser);
            PS3.ClickAndWait(PS3.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS3.FillGoalOutcomes(Browser);
            PS3.AddAnotherGoalNoRdo.Click();
            PS3.ClickAndWait(PS3.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS3.ClickAndWait(PS3.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);

            // Verify cpd suggested and custom activities feature in CPD suggestion screen 
            if (isCPDSuggestionActivtyTblCheck) 
            {
                chosenEvent = PS3.ChooseSystemCPDActivity();
                // 13.Edit the event, assert the edit in the table, Delete the event and assert it was removed from the table,
                // then add another event
                addedEvent = PS3.AddEditDeleteCustomAddActivity(Const_Mainpro.Table.PLPStep3Events,
                    PS3.CPDEventsTbl,Bys.Step3Page.CPDEventsTblBody);
                // Verify Moreinfo section should display details in Step3 Summary page 
                PS3.ClickAndWait(PS3.CPDEvents_MoreInfoDetailsBtn);
                Assert.False(PS3.CPDEvents_MoreinfoDetails_SessionIdLabel.Text.IsNullOrEmpty(),
                    "Table MoreInfo Section should display the Program/Activity Details ");
                // 12. Click next, choose an existing even with 2 gaps, then add a new CPD event and assert that the event was added
                // to the table

            }


            // 14. Click Next, choose a timeframe radio button, click Next, choose the Yes radio button, click Next then verify
            // that the gaps, goals, activities and timeframe show correctly, click Next to proceed to Step 4
            PS3.ClickAndWait(PS3.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            // Verify Step3 goalTimelineContent Text differs for Peer and Selfguided version
            if (isSelfGuided)
            {
                    Assert.True(Browser.Exists(By.XPath("//div[contains(@class,'goalTimelineContent')]//p[1]"),
                        ElementCriteria.TextContains(Const_Mainpro.PLP_Step3_goalTimelineContentText.
                        Forselfguided.GetDescription())));
                
            }
            else
            {
                Assert.True(Browser.Exists(By.XPath("//div[contains(@class,'goalTimelineContent')]//p[1]"),
                   ElementCriteria.TextContains(Const_Mainpro.PLP_Step3_goalTimelineContentText.
                   ForPeer.GetDescription())));
            }
            PLP_ClickCheckBoxOrRadioButton(Browser, "0-3 months");
            PS3.ClickAndWait(PS3.NextBtn);

            //verify "Goal Setting" screen will be displayed only for Peerversion 
            if (!isSelfGuided)
            {
                //verify tools and resource section
                PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
                // if the pathway is PeerVersion then
                // "Goal Setting" screen will be displayed
                PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
                PS3.ClickAndWait(PS3.NextBtn);
            }
            
            Assert.True(Browser.Exists(By.XPath("//div[text()='Testing gap 1']"), ElementCriteria.IsVisible));
            Assert.True(Browser.Exists(By.XPath("//div[text()='Testing gap 2']"), ElementCriteria.IsVisible));
            Assert.True(Browser.Exists(By.XPath(string.Format("//div[text()='{0}']", goalTitle)), ElementCriteria.IsVisible));
            Assert.True(Browser.Exists(By.XPath("//td[text()='Timeframe Selected: 0-3 months']"), ElementCriteria.IsVisible));

            // Verify cpd suggested and custom activities displayed in Step3 PLP Activity Summary page 
            if (isCPDSuggestionActivtyTblCheck)
            {
                VerifyGridContainsRecord(Browser, Const_Mainpro.Table.PLPStep3SetYourCPDGoalsSelectedActivities, chosenEvent.ActivityTitle);
                VerifyGridContainsRecord(Browser, Const_Mainpro.Table.PLPStep3SetYourCPDGoalsSelectedActivities, addedEvent.ActivityTitle);
                PS3.ClickAndWait(PS3.CPDEvents_MoreInfoDetailsBtn);
                Assert.False(PS3.CPDEvents_MoreinfoDetails_SessionIdLabel.Text.IsNullOrEmpty(),
                    "Table MoreInfo Section should display the Program/Activity Details ");
            }
            //Activity Summary page - Bottom container Text differs based on the version user choose
            if (isSelfGuided)
            {
                Assert.True(Browser.Exists(By.XPath("//div[contains(@class,'plpReviewContainer')]"),
                    ElementCriteria.Text(Const_Mainpro.PLP_Step3_plpReviewContainerText.Forselfguided.GetDescription())));
            }
            else
            {
                Assert.True(Browser.Exists(By.XPath("//div[contains(@class,'plpReviewContainer')]"),
                   ElementCriteria.Text(Const_Mainpro.PLP_Step3_plpReviewContainerText.ForPeer.GetDescription())));
            }
           
            PS3.ClickAndWait(PS3.NextBtn);
            PS3.VerfiyEnabledOrDisabled_PLP_Menu_PLPActivitySummmaryOption( isEnabled: true);
            #endregion

            #region Section - Step4 all screens 
            if (stepNumber == 4)
            {
                return PS4;
            }
            
            // 15. click Next ,Verify that the goal appears, add a commitment to change, click Next, write first and last name, click Next
            PS4.FillStep4Screens(isSelfGuided, new List<string>() { goalTitle },iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            #endregion

            #region Section - Step5 all screens 
            if (stepNumber == 5)
            {
                return PS5;
            }
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);

            // 16. verify the user selected and added activities in step3 are shown in Step5 - PreReflection screen 
            if (isCPDSuggestionActivtyTblCheck)
            {                
                VerifyGridContainsRecord(Browser, Const_Mainpro.Table.PLPStep5PreReflectionYourSelectedActivities, chosenEvent.ActivityTitle);
                VerifyGridContainsRecord(Browser, Const_Mainpro.Table.PLPStep5PreReflectionYourSelectedActivities, addedEvent.ActivityTitle);
                StringAssert.AreEqualIgnoringCase("2",
                    Browser.FindElements(Bys.Step5Page.PreReflectionCPDActivitiesTblBodyFirstRow).Count.ToString(),
                    "Table shows additional entries of records other than user selected/added during step3");
                PS3.ClickAndWait(PS3.CPDEvents_MoreInfoDetailsBtn);
                Assert.False(PS3.CPDEvents_MoreinfoDetails_SessionIdLabel.Text.IsNullOrEmpty(),
                    "Table MoreInfo Section should display the Program/Activity Details ");
            }

            //verify the Goal Setting Timelines shown with correct dates in PreReflection screen 
            Assert.True(Browser.Exists(By.XPath("//span[@class='date_pick']"),
                ElementCriteria.TextContains(PLP_GoalSetting_TimelineDate(3))),
                "Goal Setting Timelines shown with correct dates in PreReflection screen ");

            PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");
            PS5.ClickAndWait(PS5.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS5.ClickAndWait(PS5.NextBtn);            
            PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
                        PS5.ClickAndWait(PS5.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //verify tools and resource section
            PLP_Event.PLP_ToolsAndResourcesSection(Browser, iIconClickCheck, ToolsOptionsClickFromUserProfileCheck);
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            PLP_ClickCheckBoxOrRadioButton(Browser, "Yes");
            PS5.ClickAndWait(PS5.NextBtn);

            // verify the user selected and added activities in step3 are shown in Step5 - UsefulCPD screen 
            if (isCPDSuggestionActivtyTblCheck)
            {
                VerifyGridContainsRecord(Browser, Const_Mainpro.Table.PLPStep5UsefulCPDActivities, chosenEvent.ActivityTitle);
                VerifyGridContainsRecord(Browser, Const_Mainpro.Table.PLPStep5UsefulCPDActivities, addedEvent.ActivityTitle);
                StringAssert.AreEqualIgnoringCase("2",
                    Browser.FindElements(Bys.Step5Page.UsefulCPDActivitiesTblBodyFirstRow).Count.ToString(),
                    "Table shows additional entries of records other than user selected/added during step3");
                Grid_ClickCellInTable(Browser,
                   Const_Mainpro.Table.PLPStep5UsefulCPDActivities,
                   chosenEvent.ActivityTitle, Const_Mainpro.TableButtonLinkOrCheckBox.CheckBox);
                Grid_ClickCellInTable(Browser,
                   Const_Mainpro.Table.PLPStep5UsefulCPDActivities,
                   addedEvent.ActivityTitle, Const_Mainpro.TableButtonLinkOrCheckBox.CheckBox);
                step5_addedEvent = PS3.AddEditDeleteCustomAddActivity(Const_Mainpro.Table.PLPStep5UsefulCPDActivities,
                    PS5.UsefulCPDActivitiesTbl,Bys.Step5Page.UsefulCPDActivitiesTblBody);
                VerifyGridContainsRecord(Browser, Const_Mainpro.Table.PLPStep5UsefulCPDActivities, step5_addedEvent.ActivityTitle);
                Grid_ClickCellInTable(Browser,
                   Const_Mainpro.Table.PLPStep5UsefulCPDActivities,
                   step5_addedEvent.ActivityTitle, Const_Mainpro.TableButtonLinkOrCheckBox.CheckBox);
                PS3.ClickAndWait(PS3.CPDEvents_MoreInfoDetailsBtn);
                Assert.False(PS3.CPDEvents_MoreinfoDetails_SessionIdLabel.Text.IsNullOrEmpty(),
                    "Table MoreInfo Section should display the Program/Activity Details ");
            }
            PS5.ClickAndWait(PS5.NextBtn);           

            // Verify that Feedback screen applicable for Peer Version ONLY
            if (!isSelfGuided)
            {
                PLP_AddFormattedText(Browser,  "Testing Share Feedback",
                    Const_Mainpro.PLP_TextboxlabelText.Pleasewritemax1000Txt);
                PS5.ClickAndWait(PS5.NextBtn);
            }
            Assert.True(Browser.Exists(By.XPath("//div[@class='content-section']"),
              ElementCriteria.TextContains(PLP_PostReflection_UnlockDate())));
            Console.WriteLine(PLP_PostReflection_UnlockDate());
            PS5.ClickAndWait(PS5.NextBtn);
            #endregion

            if (stepNumber == 6)
            {
                return PSPR;
            }

            return null;
        }

        /// <summary>
        /// Return True if the current sytem date is less than august 15th
        /// </summary>
        /// <returns></returns>
        public bool IsCurrentDatePriortoAugust15()
        {
            if (currentDatetime.Month < 8)
            {
                return true;
            }
            else if (currentDatetime.Month == 8 && currentDatetime.Day < 15)
            {
                return true;
            }
            else return false;

        }

        /// <summary>
        /// Sets the CFPCMainPro+ GracePeriodEndDate to yesterday's date, so that if the user completes the current cycle, 
        /// it will be automatically rollover to next applicable cycle  
        /// IMPORTANT: We can NOT not use this method for UAT /Production Environemnts because it may alter the client's
        /// test data/users cycles. Also, do NOT call this method inside 2 different classes that would execute 
        /// in parallel with eachother, as the tests will interfere with eachother when changing this DB value. Also
        /// everytime we use this query, afterward we have to set the date back to the default date. See the method titled 
        /// SetGracePeriodEndDateToDefaultDate to set it back. That method should be executed at the test class level, which 
        /// will make it execute after every test in that class. See the Teardown fixture inside the 
        /// Mainpro_CycleCompletionAndRollover_Tests test class for an example
        /// </summary>
        /// <returns></returns>
        public bool ForceCycleAdvancementPriorToAug15()
        {
            if (EnvironmentEquals(Constants.Environments.CMEQA))
            {
                if (IsCurrentDatePriortoAugust15())
                {
                    DBUtils_Mainpro.SetGracePeriodEndDateToYesterdayDate();
                    return true;
                }
                else return false;
            }
            else return false;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public bool EnvironmentEquals(Constants.Environments environment)
        {
            //Constants.CurrentEnvironment has value from AppSettings.Config["environment"];
            if (Constants.CurrentEnvironment == environment.GetDescription())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Temporary. Currently logging in and launching from LTS goes to old UI. Need to switch to new rewrite UI
        /// </summary>
        /// <param name="Browser">erfre</param>
        public void SwitchToRewriteAfterLaunchingFromLTST(IWebDriver Browser)
        {
            DashboardPage page = new DashboardPage(Browser);

            // Need to switch to Rewrite URL because its going to old UI by default. For Production, the vanity URL is
            // used when launching from LTST, so we need to handle this differently in Prod
            if (Browser.Url.Contains("Default.aspx") && !EnvironmentEquals(Constants.Environments.Production))
            {
                string rewriteURL = AppSettings.Config["url"] + "cpd/dashboard";
                Browser.Navigate().GoToUrl(rewriteURL);
            }
            else if (Browser.Url.Contains("Default.aspx") && EnvironmentEquals(Constants.Environments.Production))
            {
                Browser.Navigate().GoToUrl("https://mainproplus.cfpc.ca/cpd/dashboard");
            }
            page.WaitForInitialize();
        }

        /// <summary>
        /// Temporary. Currently logging in and launching from LTS goes to old UI. Need to switch to new rewrite UI
        /// </summary>
        /// <param name="Browser">erfre</param>
        public void SwitchToRewriteAfterLoggingIn(IWebDriver Browser)
        {
            DashboardPage page = new DashboardPage(Browser);

            // Need to switch to Rewrite URL because its going to old UI by default
            if (Browser.Url.Contains("Default.aspx"))
            {
                string rewriteURL = AppSettings.Config["url"] + "cpd/dashboard";
                Browser.Navigate().GoToUrl(rewriteURL);
            }
            page.WaitForInitialize();
        }


        /// <summary>
        /// Get the users cycle years total based on the Current Cycle label that exists on every tab
        /// </summary>
        /// <returns></returns>
        public int GetCycleYearsTotal(IWebDriver Browser)
        {
            DashboardPage DP = new DashboardPage(Browser);
            int cycleStartYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(6, 4));
            int cycleEndYr = Int32.Parse(DP.CurrentCycleDateLbl.Text.Substring(19, 4));
            int cycleYearsTotal = cycleEndYr - cycleStartYr;
            return cycleYearsTotal;
        }

        #endregion methods: random

        #region methods: workflows

        /// <summary>
        /// Clicks on all of the circular buttons below the PLP Activity Overview slides, verifies the cooresponding slide 
        /// appears, clicks the Next button, then waits for the
        /// </summary>
        public void PLP_ReviewPLPActivityOverview(IWebDriver Browser)
        {
            EntryCarouselPathwayPage ECP = new EntryCarouselPathwayPage(Browser);
            ECP.PauseBtn.Click();

            // Verifying each carousel slide is showing whenever the cooresponding button is clicked
            Assert.True(ECP.CarouselCircleSlide1.Displayed);
            ECP.CarouselCircleBtn2.Click();
            Browser.WaitForElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide2, ElementCriteria.IsVisible);
            ECP.CarouselCircleBtn3.Click();
            Browser.WaitForElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide3, ElementCriteria.IsVisible);
            ECP.CarouselCircleBtn4.Click();
            Browser.WaitForElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide4, ElementCriteria.IsVisible);
            ECP.CarouselCircleBtn5.Click();
            Browser.WaitForElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide5, ElementCriteria.IsVisible);
            ECP.CarouselCircleBtn6.Click();
            Browser.WaitForElement(Bys.EntryCarouselPathwayPage.CarouselCircleSlide6, ElementCriteria.IsVisible);

            ECP.ClickAndWait(ECP.NextBtn);
        }

        /// <summary>
        /// Clicks on the pencil button, enters text in the formatted text popup, clicks Save and Close
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="labelText">The label of the closed control. i.e. "Gap #1" or "Please write..". If the control
        /// does not have a label, then send a blankl space " "</param>
        /// <param name="textToAdd">The text to add</param>
        public void PLP_AddFormattedText(IWebDriver Browser, string textToAdd, 
            Const_Mainpro.PLP_TextboxlabelText PLP_TextboxlabelText=Const_Mainpro.PLP_TextboxlabelText.None,
            string labelText = null,bool clearText=false)
        {
            if (!PLP_TextboxlabelText.GetDescription().ToString().Contains("None"))
            {
                labelText = PLP_TextboxlabelText.GetDescription().ToString();
            }

            Step3Page PS3 = new Step3Page(Browser);
            IWebElement pencilBtn = null;
            string xpathForPencilBtn = null;
            Thread.Sleep(30);
            // The xpath for the pencil button is different depending on what page we are on
            // If we are on Step 2 Identify Gaps 
            if (Browser.Exists(By.XPath("//h4[text()='Step 2: Identify Gaps']")))
            {
                int gapNumber = 1;
                if (labelText == "Gap #1")
                {
                    gapNumber = 1;
                }
                else if (labelText == "Gap #2")
                {
                    gapNumber = 2;
                }
                else
                {
                    gapNumber = 3;

                }
                xpathForPencilBtn = string.Format(
                    "(//div[contains(@class, '{0}EditButton')])/descendant::div[@title='Click to Edit']", gapNumber);
                pencilBtn = Browser.FindElement(By.XPath(xpathForPencilBtn));
            }
            // Else if we are on another page
            else
            {
                Browser.WaitJSAndJQuery();
                xpathForPencilBtn = string.Format(
                    "//div[text()='{0}']/ancestor::div[4]/descendant::div[contains(@class, 'button-icon')] | " +
                    "//p[text()='{0}']/ancestor::div[5]/descendant::div[contains(@class, 'button-icon')]", labelText.ToString());
                pencilBtn = Browser.FindElement(By.XPath(xpathForPencilBtn));
            }
            try { pencilBtn.Click(); }
            catch { pencilBtn.ClickJS(Browser); }
            

            Browser.WaitForElement(Bys.MainproPage.PLP_FormattedTextFormFrame, ElementCriteria.IsVisible);
            Browser.WaitJSAndJQuery();Thread.Sleep(TimeSpan.FromSeconds(10));
            Browser.SwitchTo().Frame(PS3.PLP_FormattedTextFormFrame);
            Browser.WaitForElement(Bys.MainproPage.PLP_FormattedTextFormFrameTxt, ElementCriteria.IsVisible);
            Thread.Sleep(500);

            //PS3.PLP_FormattedTextFormFrameTxt.Clear();
            Browser.SwitchTo().DefaultContent();
            Browser.SwitchTo().Frame(PS3.PLP_FormattedTextFormFrame);
            if (clearText)
            {
                PS3.PLP_FormattedTextFormFrameTxt.Clear();
            }
            PS3.PLP_FormattedTextFormFrameTxt.SendKeys(textToAdd);

            Browser.SwitchTo().DefaultContent();
            Thread.Sleep(500);
            Browser.WaitJSAndJQuery();
            PS3.ClickAndWaitBasePage(PS3.PLP_FormattedTextFormSaveAndCloseBtn);

        }
        /// <summary>
        /// Clicks on the comment box on the options page, and enters the value in it
        /// </summary>
        /// <param name="Browser">The driver instance</param>
       
        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly. Clicks 
        /// the CPD Planning tab, clicks on the Create a Personal Learning Plan Goal button, fills in all required fields, 
        /// clicks the Create button, then verifys that the goal appeared on this table with the correct title. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="currentTest"></param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. 
        /// Default = false</param>
        /// <param name="username">(Optional). If you want to specify a specific user to login with, enter the username.
        /// If not, leave it blank and this method will generate a username for you</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static 
        /// automation users</param>
        /// <param name="title">(Optional). Specify a goal title. If not specified, a random goal title will be used</param>
        /// <param name="completionDt">(Optional). Default = today</param>
        public Goal AddGoal(IWebDriver Browser, TestContext.TestAdapter currentTest, bool isNewUser = false,
            string username = null, string password = null, string title = null,
        DateTime completionDt = default(DateTime))
        {
            // Instantiate all the necessary page classes
            DashboardPage DP = new DashboardPage(Browser);
            LoginPage LP = new LoginPage(Browser);
            CPDPlanningPage PP = new CPDPlanningPage(Browser);

            // If not logged in already
            if (!Browser.Exists(Bys.MainproPage.LogoutLnk))
            {
                Navigation.GoToLoginPage(Browser);

                // If the tester wants a specific user to login with, login with that user
                if (!string.IsNullOrEmpty(username))
                {
                    LP.Login(username, password, isNewUser);
                }
                // else login with a new user from API
                else
                {
                    UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: currentTest);
                    LP.Login(newUser.Username, newUser.Password, true);
                }
            }

            DP.ClickAndWaitBasePage(DP.CPDPlanningTab);
            PP.ClickAndWait(PP.CreateAPersonalLearningGoalBtn);

            Goal goal = PP.FillGoalForm(title);

            PP.ClickAndWait(PP.CreateAGoalFormCreateBtn);
            PP.ClickAndWait(PP.CreateAGoalFormCloseButton);

            if (!ElemGet.Grid_ContainsRecord(Browser, PP.GoalsTbl, Bys.CPDPlanningPage.GoalsTblBody, 0,
                goal.Title, "div"))
            {
                string goalTitleInTable = Grid_GetRowCellTextByIndex(Browser, Const_Mainpro.Table.PlanningTabGoal, 0, 0, "//span");
                throw new Exception(string.Format("The method successfully filled the the form and clicked the Create, " +
                    "button, but when the method tried to verify that the goal title appeared in the CPD Planning " +
                    "table, it could not find it. The code is expecting the title of the goal to be {0}, however the " +
                    "goal table is displaying {1}. The cause is either that the " +
                    "application did not successfully populate this table, or the table has many goals and the " +
                    "application incorrectly sorted the last submitted one at the bottom (this table has an infinite " +
                    "scroll which makes activities not appear until scrolled to)", goal.Title, goalTitleInTable));
            }

            return goal;
        }

        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly. Clicks 
        /// the Enter CPD Activity button, chooses a user-specified Category, Certification Type, Activity Type, and 
        /// Activity Format (if applicable). It will also choose articles or volumes/questions depending on activity type. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="currentTest"></param>
        /// <param name="category"><see cref="Const_Mainpro.ActivityCategory"/></param>
        /// <param name="certType"><see cref="Const_Mainpro.ActivityCertType"/></param>
        /// <param name="actType"><see cref="Const_Mainpro.ActivityType"/></param>
        /// <param name="actFormat"><see cref="Const_Mainpro.ActivityFormat"</param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. 
        /// Default = false</param>
        /// <param name="username">(Optional). If you are not logged in already and you want to specify an existing user to 
        /// login with, enter the username. If you dont specify this and you are not logged in yet, and then this method 
        /// will generate a username for you and login with that  user</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static automation 
        /// users</param>
        /// <param name="articleTitle">(Optional). Selects a user-specified article in the Article dropdown after selecting 
        /// the activity type</param>
        /// <param name="volumeTitleRequested">(Optional). Selects a user-specified volume in the Volume dropdown. If you 
        /// leave this argument null and are calling the Help.AddActivity method choosing an activity that prompts the 
        /// Volume select element to appear, but there are no Volumes for your current user, then this method will create 
        /// a volume (with questions) on-the-fly, refresh the page, and select this newly created Volume and questions.
        /// The title of the volume can not already be in the system, it has to be a new name. 
        /// Also, it must have the word "Volume" in it with a capital V, else the application fails to assign questions
        /// to the volume.
        /// Default = The first indexed volume</param>
        /// <param name="questionsRequested">(Optional). The exact text of the questions to choose from the Questions 
        /// dropdown after selecting the volume. See the argument volumeTitleRequested for further explanation of what 
        /// will occur if you leave this argument null. Default = The first 2 indexed ones</param>
        public EnterACPDActivityPage ChooseActivity(IWebDriver Browser, TestContext.TestAdapter currentTest,
            Const_Mainpro.ActivityCategory category,
            Const_Mainpro.ActivityCertType certType,
            Const_Mainpro.ActivityType actType,
            Const_Mainpro.ActivityFormat? actFormat = null,
            string articleTitle = null,
            string volumeTitleRequested = null,
            List<string> questionsRequested = null,
            bool isNewUser = false,
            string username = null,
            string appendStringToUserName = null,
            string password = null)
        {
            // Instantiate all the necessary page classes
            EnterACPDActivityPage EP = new EnterACPDActivityPage(Browser);
            DashboardPage DP = new DashboardPage(Browser);
            LoginPage LP = new LoginPage(Browser);

            // If not logged in already
            if (!Browser.Exists(Bys.MainproPage.LogoutLnk))
            {
                Navigation.GoToLoginPage(Browser);

                // If the tester wants a specific user to login with, login with that user
                if (!string.IsNullOrEmpty(username))
                {
                    LP.Login(username, password, isNewUser);
                }
                // else login with a new user from API
                else
                {
                    UserModel newUser = UserUtils.CreateAndRegisterUser(currentTest: currentTest);
                    LP.Login(newUser.Username, newUser.Password, true);
                }
            }

            DP.ClickAndWaitBasePage(DP.DashboardTab);
            DP.ClickAndWaitBasePage(DP.EnterCPDActBtn);

            // If the user requested to add an AMA activity and the application wont allow it because the user is in
            // one of the cycles shown below while already having 25 or more credits applie for an AMA activity
            if (actType == Const_Mainpro.ActivityType.GRPLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO ||
                actType == Const_Mainpro.ActivityType.SELFLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO)
            {
                string[] twoYearCycleMaxCreditLimits = { "Remedial", "Remedial Reinstate", "Leave Reinstate" };
                if (twoYearCycleMaxCreditLimits.
                    Any(DBUtils.GetParticpantLatestCycleType(APIHelp.GetUserName(Browser)).Contains))
                {
                    if (DBUtils.GetParticpantsCurrentAppliedAMARCPCredits(APIHelp.GetUserName(Browser), "AMA") > 25)
                    {
                        {
                            throw new Exception("You requested to add an AMA activity but you already have 25 or more " +
                                "credits for an AMA activity and you are in a cycle that will not allow more than 25. " +
                                "This will result in the UI warning the user after clicking the Continue button that " +
                                "this is not allowed.");
                        }
                    }
                }
            }

            // If the user requested to add an RCP activity and the application wont allow it because the user is in
            // one of the cycles shown below while already having 25 or more credits applie for an RCP activity
            if (actType == Const_Mainpro.ActivityType.ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO ||
                actType == Const_Mainpro.ActivityType.GRPLRNING_CERT_RoyalCollegeMOCAccreditedSection1_LO)
            {
                string[] twoYearCycleMaxCreditLimits = { "Remedial", "Remedial Reinstate", "Leave Reinstate" };
                if (twoYearCycleMaxCreditLimits.
                    Any(DBUtils.GetParticpantLatestCycleType(APIHelp.GetUserName(Browser)).Contains))
                {
                    if (DBUtils.GetParticpantsCurrentAppliedAMARCPCredits(APIHelp.GetUserName(Browser), "RCP") > 25)
                    {
                        {
                            throw new Exception("You requested to add an AMA activity but you already have 25 or more " +
                                "credits for an AMA activity and you are in a cycle that will not allow more than 25. " +
                                "This will result in the UI warning the user after clicking the Continue button that " +
                                "this is not allowed.");
                        }
                    }
                }
            }

            EP.ChooseActivity(category, certType, actType, actFormat, articleTitle, volumeTitleRequested, questionsRequested);

            return EP;
        }

        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly. Clicks 
        /// the Enter CPD Activity button, chooses a user-specified Category, Certification Type, Activity Type, and 
        /// Activity Format (if applicable). It will also choose articles or volumes/questions depending on activity type. 
        /// It will then click Continue and go to the Activity Details page
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="currentTest"></param>
        /// <param name="category"><see cref="Const_Mainpro.ActivityCategory"/></param>
        /// <param name="certType"><see cref="Const_Mainpro.ActivityCertType"/></param>
        /// <param name="actType"><see cref="Const_Mainpro.ActivityType"/></param>
        /// <param name="actFormat"><see cref="Const_Mainpro.ActivityFormat"</param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. 
        /// Default = false</param>
        /// <param name="username">(Optional). If you are not logged in already and you want to specify an existing user to 
        /// login with, enter the username. If you dont specify this and you are not logged in yet, and then this method 
        /// will generate a username for you and login with that  user</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static 
        /// automation users</param>
        /// <param name="articleTitle">(Optional). Selects a user-specified article in the Article dropdown after selecting
        /// the activity type</param>
        /// <param name="volumeTitle">(Optional). Selects a user-specified volume in the Volume dropdown after selected the
        /// article</param>
        /// <param name="questions">(Optiona). The exact text of the questions to choose from the Questions dropdown after
        /// selecting the volume</param>
        public EnterACPDActivityDetailsPage ChooseActivityContinueToDetailsPage(IWebDriver Browser,
            TestContext.TestAdapter currentTest,
            Const_Mainpro.ActivityCategory category,
            Const_Mainpro.ActivityCertType certType,
            Const_Mainpro.ActivityType actType,
            Const_Mainpro.ActivityFormat? actFormat = null,
            string articleTitle = null,
            string volumeTitle = null,
            List<string> questions = null,
            bool isNewUser = false,
            string username = null,
            string password = null)
        {
            EnterACPDActivityPage EP = ChooseActivity(Browser, currentTest, category, certType, actType, actFormat, articleTitle,
                volumeTitle, questions, isNewUser, username, password);

            return EP.ClickAndWait(EP.ContinueBtn);
        }

        /// <summary>
        /// If not logged in already, logs in with either a user-specified username or creates a new user on the fly. Clicks 
        /// the Enter CPD Activity button, chooses a user-specified Category, Certification Type, Activity Type, and 
        /// Activity Format (if applicable). It will also choose articles or volumes/questions depending on activity type. 
        /// It will then click Continue, fill in all required fields for any activity form including uploading of a document 
        /// if the document is required, then clicks Submit or 
        /// Send To Holding Area depending on what you want, then clicks Go TO CPD Activities or Go To Holding Area 
        /// button, landing at the CPD Activities List page or Holding Area page, and 
        /// verifying that the activity appeared on the table with the correct activity title. 
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="currentTest"></param>
        /// <param name="category"><see cref="Const_Mainpro.ActivityCategory"></see></param>
        /// <param name="certType"><see cref="Const_Mainpro.ActivityCertType"></see></param>
        /// <param name="actType"><see cref="Const_Mainpro.ActivityType"></see></param>
        /// <param name="actFormat"><see cref="Const_Mainpro.ActivityFormat"></see></param>
        /// <param name="articleTitle">(Optional). Selects a user-specified article in the Article dropdown after 
        /// selecting the activity type</param>
        /// <param name="volumeTitle">(Optional). Selects a user-specified volume in the Volume dropdown after 
        /// selected the article</param>
        /// <param name="questions">(Optiona). The exact text of the questions to choose from the Questions dropdown 
        /// after selecting the volume</param>
        /// <param name="isNewUser">(Optional) true or false depending on if the user has ever logged in or not. 
        /// Default = false</param>
        /// <param name="username">(Optional). If you are not logged in already and you want to specify an existing user to 
        /// login with, enter the username. If you dont specify this and you are not logged in yet, and then this method 
        /// will generate a username for you and login with that  user</param>
        /// <param name="password">(Optional). If not passed, will use the default password for all of our static 
        /// automation users</param>
        /// <param name="actName">(Optional). You can specify an activity title if the type of activity allows for it. 
        /// Default = "TestAuto_CurrentDate"</param>
        /// <param name="creditsRequested">(Optional). If the activity allows the user to enter credits, you can enter that here. 
        /// Default = 1</param>
        /// <param name="actStartDt">(Optional). Default = today</param>
        /// <param name="actCompletionDt">(Optional). Default = today</param>
        /// <param name="actDateOfReflection">(Optional). Default = today</param>
        /// <param name="submitActivity">(Optional). Set to false if you want to send the activity to the Holding Area 
        /// instead of the CPD Activities List page. Default = true</param>
        public Activity AddActivity(IWebDriver Browser, TestContext.TestAdapter currentTest,
        Const_Mainpro.ActivityCategory category,
        Const_Mainpro.ActivityCertType certType,
        Const_Mainpro.ActivityType actType,
        Const_Mainpro.ActivityFormat? actFormat = null,
        string articleTitle = null,
        string volumeTitle = null,
        List<string> questions = null,
        bool isNewUser = false,
        string username = null,
        string password = null,
        string actName = null,
        double creditsRequested = 1,
        DateTime actStartDt = default(DateTime),
        DateTime actCompletionDt = default(DateTime),
        DateTime actDateOfReflection = default(DateTime),
        bool submitActivity = true)
        {
            // Instantiate all the necessary page classes
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
            EnterACPDActivityPage EP = new EnterACPDActivityPage(Browser);
            DashboardPage DP = new DashboardPage(Browser);
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(Browser);
            LoginPage LP = new LoginPage(Browser);
            HoldingAreaPage HP = new HoldingAreaPage(Browser);

            ChooseActivityContinueToDetailsPage(Browser, currentTest, category, certType, actType, actFormat, articleTitle,
                volumeTitle, questions, isNewUser, username, password);

            Activity activity = EADP.FillActivityForm(actName, creditsRequested, actStartDt, actCompletionDt, actDateOfReflection);

            // If the user requested more than 50 credits and the application wont allow it because the activity type is
            // RCP/AMA and the user is in a Default cycle. NOTE: Resident, Voluntary, Affiliate-Default and
            // Affiliate-Remedial cycles allow infinite credits for these activity types
            if (actType == Const_Mainpro.ActivityType.ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO ||
                actType == Const_Mainpro.ActivityType.GRPLRNING_CERT_RoyalCollegeMOCAccreditedSection1_LO ||
                actType == Const_Mainpro.ActivityType.GRPLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO ||
                actType == Const_Mainpro.ActivityType.SELFLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO)
            {
                if (creditsRequested > 50)
                {
                    if (DBUtils.GetParticpantLatestCycleType(APIHelp.GetUserName(Browser)) == "Default")
                    {
                        // Above query finds Default users, but not all Default users have the maximum credit limit.
                        // Affiliate Default users do not have a maximum limit, so if this Default user is NOT affiliate, 
                        // then throw error
                        if (!DBUtils.GetParticpantLatestAdjustmentCode(APIHelp.GetUserName(Browser)).Contains("AFFILIATE"))
                        {
                            {
                                throw new Exception("You requested more than 50 credits for a Royal College Certified / AMA " +
                                    "activity while you are in a cycle that will not allow this. This will result in the UI " +
                                    "warning the user after clicking the Submit button that this is not allowed. " +
                                    "You must bypass this helper method and instead use the FillActivityForm method " +
                                    "because that method does not click the Submit button expecting the activity to be submitted");
                            }
                        }
                    }
                }
            }

            // If the tester chose to submit, submit then verify the activity was added to the CPD Activities List
            // page table, else click Go to Holding Area and verify it was added to the tables on the holding area page
            if (submitActivity)
            {
                EADP.ClickAndWait(EADP.SubmitBtn);
                EADP.ClickAndWait(EADP.YourActivityHasBeenSubmittedFormGoToCPDActBtn);
                // Select "All" in the Cycle Select Element. We do this because if the tester passed in a date
                // that falls outside the cycle date in the cycleCompletionDate parameter, then the table would not 
                // show this activity because by default, the Cycle select element has the current cycle chosen
                ALP.SelectAndWait(ALP.ActTblCycleSelElem, "All");
                if (!ElemGet.Grid_ContainsRecord(Browser, ALP.ActTbl, Bys.CPDActivitiesListPage.ActTblBody, 0,
                    activity.Title, "span"))
                {
                    // We have to send "3" as the colIndex parameter because this table is unconventional in a sense that the 
                    // text of the activity title is the 3rd indexed Span element within the first column
                    string actTitleInTable = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, ALP.ActTblBody,
                        Bys.CPDActivitiesListPage.ActTblBodyFirstRow, 0, 3, "//span");
                    throw new Exception(string.Format("The method successfully filled the the form and clicked the Submit, " +
                        "button, but when the method tried to verify that the activity title appeared in the CPD Activities " +
                        "List Page, it could not find it. The code is expecting the title of the activity to be {0}, however the " +
                        "activity table is displaying {1}. The cause is either that the " +
                        "application did not successfully populate this table, or the table has many activities and the " +
                        "application incorrectly sorted the last submitted one at the bottom (this table has an infinite " +
                        "scroll which makes activities not appear until scrolled to", activity.Title, actTitleInTable));
                }
            }
            else
            {
                // Click Send To Holding Area then verify the activity appears on the Incomplete Activities table on both 
                // Summary and Incomplete tabs
                EADP.ClickAndWait(EADP.SendToHoldingAreaBtn);
                EADP.ClickAndWait(EADP.YourActivityHasBeenSavedGoToHoldingAreaBtn);
                if (!ElemGet.Grid_ContainsRecord(Browser, HP.SummTabIncompActTbl,
                    Bys.HoldingAreaPage.SummTabIncompActTblBody, 0, activity.Title, "span"))
                {
                    string actTitleInTable = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, HP.SummTabActPendApprTblBody,
                        Bys.HoldingAreaPage.SummTabIncompActTblBodyFirstRow, 0, 0, "//span");
                    throw new Exception(string.Format("The method successfully filled the the form and click the Send To " +
                        "Holding Area button, " +
                        "but when the method tried to verify that the activity title appeared in the Incomplete Activities " +
                        "table on the Summary tab of the Holding Area page, " +
                        "it could not find it. The code is expecting the title of the activity to be {0}, however the " +
                        "activity table is displaying {1}. The cause is either that the " +
                        "application did not successfully populate this table, or the table has many activities and the " +
                        "application incorrectly sorted the last submitted one at the bottom (this table has an infinite " +
                        "scroll which makes activities not appear until scrolled to" +
                        "", activity.Title, actTitleInTable));
                }

                HP.ClickAndWait(HP.IncompleteTab);
                if (!ElemGet.Grid_ContainsRecord(Browser, HP.IncompleteTabActTbl,
                        Bys.HoldingAreaPage.IncompleteTabActTblBody, 0, activity.Title, "span"))
                {
                    string actTitleInTable = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, HP.IncompleteTabActTblBody,
                        Bys.HoldingAreaPage.IncompleteTabActTblBodyFirstRow, 0, 0, "//span");
                    throw new Exception(string.Format("The method successfully filled the the form and click the Send To " +
                        "Holding Area button, " +
                        "but when the method tried to verify that the activity title appeared in the Incomplete Activities " +
                        "table on the Incomplete tab of the Holding Area page, " +
                        "it could not find it. The code is expecting the title of the activity to be {0}, however the " +
                        "activity table is displaying {1}. The cause is either that the " +
                        "application did not successfully populate this table, or the table has many activities and the " +
                        "application incorrectly sorted the last submitted one at the bottom (this table has an infinite " +
                        "scroll which makes activities not appear until scrolled to" +
                        "", activity.Title, actTitleInTable));
                }
                HP.ClickAndWait(HP.SummTab);
            }

            return activity;
        }



        /// <summary>
        /// Logs into Lifetime Support, searches for the user-specified user, validates (accepts) credit for the 
        /// user-specified activity, reevaluates the user, then launches back into Mainpro for this user
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="mainproUsername">The username</param>
        /// <param name="actTitle">The activity title to validate</param>
        /// <param name="ltsUsername">The lifetime support username. Password must be 'password'</param>
        public void ValidateCreditReevaluateUserThenRelaunchMainpro(IWebDriver browser, string mainproUsername,
            string actTitle, string ltsUsername)
        {
            LSHelp.Login(browser, ltsUsername, AppSettings.Config["ltspassword"]);
            LSHelp.ValidateCredit(browser, "College of Family Physician", mainproUsername, "Certification of Proficiency",
                actTitle, Constants_LTS.CreditValidationOptions.Accept);
            LSHelp.ReevaluateUser(browser, "College of Family Physician", mainproUsername, "Certification of Proficiency");
            LSHelp.LaunchSiteFromParticipantPage(browser, "College of Family Physician", mainproUsername);
            DashboardPage DP = new DashboardPage(browser);
            DP.WaitForInitialize();
            SwitchToRewriteAfterLaunchingFromLTST(browser);
        }

        #endregion methods: workflows

        #region methods: tables

        /// <summary>
        /// Throws an error if the activity or goal does not exist within the table
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tbl">The table you are getting the cell text from</param>
        /// <param name="activityOrGoalName">The name of the activity/goal. i.e. The exact text from cell inside the first column</param>     
        /// <param name="additionalCellText">(Optional). Send cell text from an additional column if the rows in your table are not 
        /// unique and multiple activities/goals exist with the same title</param>     
        public void VerifyGridContainsRecord(IWebDriver Browser, Const_Mainpro.Table tbl, string activityOrGoalName,
            string additionalCellText = null)
        {
            String tagNameThatTextExistsWithin = "span";
            // Get the table iWebElement and the Body by
            List<object> tblElemsAndRowBy = Grid_GetTableElemsAndBys(Browser, tbl);
            IWebElement Tbl = (IWebElement)tblElemsAndRowBy[0];
            By BodyBy = (By)tblElemsAndRowBy[3];
            By RowBy = (By)tblElemsAndRowBy[4];

            // The column number of the record title is 0 in Mainpro, but 1 in PLP
            int colNumberToLookForRecordName = 0;
            if (Browser.Exists(Bys.MainproPage.PLP_Menu_ExitToMainpro))
            {
                colNumberToLookForRecordName = 1;
                tagNameThatTextExistsWithin = "div";

            }
            if (Browser.Url.Contains("usefulcpdprograms"))
            {
                colNumberToLookForRecordName = 2;
                tagNameThatTextExistsWithin = "div";
            }

            if (string.IsNullOrEmpty(additionalCellText))
            {
                //   if (!ElemGet.Grid_ContainsRecord(Browser, Tbl, BodyBy, colNumberToLookForRecordName, activityOrGoalName, "td"))
                ElemSet.ScrollToElement(Browser, Tbl);
                try
                {
                    if (!ElemGet.Grid_ContainsRecord(Browser, Tbl, BodyBy, colNumberToLookForRecordName, 
                        activityOrGoalName, tagNameThatTextExistsWithin ))
                    {
                        throw new Exception(string.Format("The table '{0}' did not contain the activity/goal '{1}' for " +
                            "user: '{2}'",
                            tbl.GetDescription().ToString(), activityOrGoalName, APIHelp.GetUserName(Browser)));
                    }
                }
                catch { }
            }
            else
            {
                ElemSet.ScrollToElement(Browser, Tbl);
                if (!ElemGet.Grid_ContainsRecord_WithAdditionalCellText(Browser, Tbl, RowBy, activityOrGoalName,
                    tagNameThatTextExistsWithin ,additionalCellText, "span"))
                {
                    throw new Exception(string.Format("The table '{0}' did not contain the activity/goal '{1}' with the " +
                        "addition call text of {2} for user: '{3}'",
                        tbl.GetDescription().ToString(), activityOrGoalName, additionalCellText,
                        APIHelp.GetUserName(Browser)));
                }

            }
        }

        /// <summary>
        /// Throws an error if the activity or goal exists within the table
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tbl">The table you are getting the cell text from</param>
        /// <param name="activityOrGoalName">The name of the activity/goal. i.e. The exact text from cell inside the first column</param>     
        /// <param name="additionalCellText">(Optional). Send cell text from an additional column if the rows in your table are not 
        /// unique and multiple activities/goals can coexist with the same title</param>     
        public void VerifyGridDoesNotContainRecord(IWebDriver Browser, Const_Mainpro.Table tbl, string activityOrGoalName,
            string additionalCellText = null)
        {
            // Get the table iWebElement and the Body by
            List<object> tblElemsAndRowBy = Grid_GetTableElemsAndBys(Browser, tbl);
            IWebElement Tbl = (IWebElement)tblElemsAndRowBy[0];
            By BodyBy = (By)tblElemsAndRowBy[3];
            By RowBy = (By)tblElemsAndRowBy[4];

            if (string.IsNullOrEmpty(additionalCellText))
            {
                if (ElemGet.Grid_ContainsRecord(Browser, Tbl, BodyBy, 0, activityOrGoalName, "span"))
                {
                    throw new Exception(string.Format("The table '{0}' contained the activity/goal '{1}'",
                        tbl.GetDescription().ToString(), activityOrGoalName));
                }

            }
            else
            {
                if (ElemGet.Grid_ContainsRecord_WithAdditionalCellText(Browser, Tbl, RowBy, activityOrGoalName, "span",
                    additionalCellText, "span"))
                {
                    throw new Exception(string.Format("The table '{0}' contained the activity/goal '{1}' with the " +
                        "addition call text of {2}",
                        tbl.GetDescription().ToString(), activityOrGoalName, additionalCellText));
                }

            }
        }

        /// <summary>
        /// Gets the cell text of a cell by specifying the either: 1. Both the row name and column name 2. The row name and 
        /// the column index 3. The column name and the row index. If you instead want to get the cell text by indexes only 
        /// (i.e. By row index AND column index, see Grid_GetRowCellTextByIndex)
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tbl">The table you are getting the cell text from</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from cell inside the first column</param>     
        /// <param name="colName"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        public string Grid_GetRowCellTextByName(IWebDriver Browser, Const_Mainpro.Table tbl,
            string rowName = null, string colName = null, string rowIndex = null, string colIndex = null)
        {
            // Instantiate all the necessary page classes
            DashboardPage DP = new DashboardPage(Browser);
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
            CPDPlanningPage PP = new CPDPlanningPage(Browser);
             PLPHubPage PLPHP = new PLPHubPage(Browser);

            // Get the table iWebElement, the  and the row By
            List<object> tblElemsAndRowBy = Grid_GetTableElemsAndBys(Browser, tbl);
            IWebElement Tbl = (IWebElement)tblElemsAndRowBy[0];
            IWebElement TblHdr = (IWebElement)tblElemsAndRowBy[1];
            IWebElement TblBody = (IWebElement)tblElemsAndRowBy[2];
            By RowBy = (By)tblElemsAndRowBy[4];
            By TblBy = (By)tblElemsAndRowBy[5];

            // Make sure the table that the tester specified is on the current page and is visible
            if (!Browser.Exists(TblBy, ElementCriteria.IsVisible))
            {
                throw new Exception("The table you specified is not on the current page or is not visible on this page. " +
                    "See screenshot");
            }

            string cellText = "null";

            switch (tbl)
            {
                case Const_Mainpro.Table.CreditSummaryTabGroupLearn:
                case Const_Mainpro.Table.CreditSummaryTabSelfLearn:
                case Const_Mainpro.Table.CreditSummaryTabAssessment:
                case Const_Mainpro.Table.CreditSummaryTabOther:
                case Const_Mainpro.Table.CreditSummaryWidgetCycle:
                case Const_Mainpro.Table.CPDActitivitesListTabAct:
                case Const_Mainpro.Table.HoldingAreaSummTabInc:
                case Const_Mainpro.Table.HoldingAreaSummTabPendAppr:
                case Const_Mainpro.Table.HoldingAreaIncTabInc:
                case Const_Mainpro.Table.HoldingAreaCredValTabPendAppr:
                case Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities:
                case Const_Mainpro.Table.PLPStep3Events:
                case Const_Mainpro.Table.PLPStep5UsefulCPDActivities:
                case Const_Mainpro.Table.PLPHubCompletedPLPTbl:

                    if (string.IsNullOrEmpty(rowName) || string.IsNullOrEmpty(colName))
                    {
                        throw new Exception("This table should only use the rowName and colName parameter");
                    }
                    if (!string.IsNullOrEmpty(rowIndex) || !string.IsNullOrEmpty(rowIndex))
                    {
                        throw new Exception("This table should only use the rowName and colName parameter");
                    }
                    switch (tbl)
                    {
                        case Const_Mainpro.Table.CreditSummaryTabGroupLearn:
                        case Const_Mainpro.Table.CreditSummaryTabSelfLearn:
                        case Const_Mainpro.Table.CreditSummaryTabAssessment:
                        case Const_Mainpro.Table.CreditSummaryTabOther:
                        case Const_Mainpro.Table.CreditSummaryWidgetCycle:
                            cellText = ElemGet.Grid_GetCellTextByRowNameAndColName(Browser, Tbl, TblHdr, RowBy, rowName, "th", colName);
                            break;
                        case Const_Mainpro.Table.CPDActitivitesListTabAct:
                        case Const_Mainpro.Table.HoldingAreaSummTabInc:
                        case Const_Mainpro.Table.HoldingAreaSummTabPendAppr:
                        case Const_Mainpro.Table.HoldingAreaIncTabInc:
                        case Const_Mainpro.Table.HoldingAreaCredValTabPendAppr:
                        case Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities:
                        case Const_Mainpro.Table.PLPStep3Events:
                        case Const_Mainpro.Table.PLPStep5UsefulCPDActivities:
                            cellText = ElemGet.Grid_GetCellTextByRowNameAndColName(Browser, Tbl, TblHdr, RowBy, rowName, "span", colName,
                                "//span");
                            break;
                        case Const_Mainpro.Table.PLPHubCompletedPLPTbl:
                            cellText = ElemGet.Grid_GetCellTextByRowNameAndColName(Browser, Tbl, TblHdr, RowBy, rowName, "div", colName,
                                "//span");
                            break;
                    }
                    break;

                case Const_Mainpro.Table.CreditSummaryWidgetCurrentYear:
                case Const_Mainpro.Table.CreditSummaryTabViewAllCyclesFormCycle:
                    if (string.IsNullOrEmpty(rowIndex) || string.IsNullOrEmpty(colName))
                    {
                        throw new Exception("This table should only use the rowIndex and colName parameter");
                    }
                    if (!string.IsNullOrEmpty(rowName) || !string.IsNullOrEmpty(colIndex))
                    {
                        throw new Exception("This table should only use the rowIndex and colName parameter");
                    }
                    cellText = ElemGet.Grid_GetCellTextByRowIndexAndColName(Browser, TblBody, TblHdr, Int32.Parse(rowIndex), colName);
                    break;

                case Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs:
                case Const_Mainpro.Table.AMARCPMaxCreditForm:
                    if (string.IsNullOrEmpty(rowName) || string.IsNullOrEmpty(colIndex))
                    {
                        throw new Exception("This table should only use the rowName and colIndex parameter");
                    }
                    if (!string.IsNullOrEmpty(rowIndex) || !string.IsNullOrEmpty(colName))
                    {
                        throw new Exception("This table should only use the rowName and colIndex parameter");
                    }
                    cellText = ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, Tbl, RowBy, rowName, "th", Int32.Parse(colIndex));
                    break;

                case Const_Mainpro.Table.PlanningTabGoal:
                case Const_Mainpro.Table.DashboardTabInc:
                case Const_Mainpro.Table.DashbooardTabPendAppr:
                case Const_Mainpro.Table.DashbooardTabGoal:
                case Const_Mainpro.Table.ActivitySearchResults:
                    throw new Exception("I have not needed to verify this table yet, so this has not been coded yet");
            }

            return cellText;
        }

        /// <summary>
        /// Gets the cell text of a cell by specifying the row index AND column index. If you instead want to get the cell 
        /// text by either the column name or row name, or both, see Grid_GetRowCellTextByName
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tbl">The table you are getting the cell text from</param>
        /// <param name="rowIndex">The zero-based index of the row you want to extract the text from</param>      
        /// <param name="colIndex">The zero-based index of the column you want to extract the text from.</param>
        /// <param name="xpathForCellTextCells">(Optional). The xpath where you are extracting the cell 
        /// text from. Default = '//td'</param>
        public string Grid_GetRowCellTextByIndex(IWebDriver Browser, Const_Mainpro.Table tbl, int rowIndex, int colIndex,
            string xpathForCellTextCells = null)
        {
            // Get the table's body iWebelement and the table's row By
            List<object> tblElemsAndRowBy = Grid_GetTableElemsAndBys(Browser, tbl);
            IWebElement TblBody = (IWebElement)tblElemsAndRowBy[2];
            By RowBy = (By)tblElemsAndRowBy[4];

            string cellText = "null";

            cellText = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, TblBody, RowBy, rowIndex, colIndex,
                xpathForCellTextCells);

            return cellText;

        }

        /// <summary>
        /// Verifies cell text within a tester-specified table. Note that if you add an activity through LTST (not 
        /// through the Mainpro Activity Details page) which awards credits, this method will will retry the cell 
        /// text verification every 3 seconds, then refresh the page 40 times because once an activity is submitted, 
        /// a record gets put into a windows service queue, and then waits for that service to push the 
        /// activity through. Because of this, we need to wait in our code. If your application ever takes longer than 
        /// the refreshes, then see the note at the end of this Description regarding clearing folders on DevOps 
        /// servers. For when you submit the activity on the 
        /// Activity Details page directly, this method will not require a retry/refresh because we have auto code 
        /// in place after clicking the Submit button on the Activity Details page which checks for 
        /// a "Credit Service = Complete" label that DEV has provided us inside the HTML.
        /// NOTE: For credits to be applied in our application, a Windows Service needs executed. Sometimes this windows
        /// service takes an unacceptable amount of time because a certain folder on a DevOps server gets filled up 
        /// with 0KB cache files, and/or the windows service has a huge amount of credit requests already inside it. 
        /// Manik, Kiran and DevOps figured this out, and then set a daily job to clear this folder out. They also 
        /// stopped some recognition services so that the queue doesnt get filled up every morning. If this ever
        /// creeps up again, and the 40 refreshes is not long enough, we will have to notify DEV and DevOps again.
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="cellTextExpected">The cell text you expect to be in the cell</param>
        /// <param name="Page">The page to refresh</param>
        /// <param name="tbl">The table you are getting the cell text from</param>
        /// <param name="rowName">The name of the row. i.e. The exact text from cell inside the first column</param>     
        /// <param name="colName"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="wasActivityUploaded">If the activity was not Submitted within the Mainpro UI, and instead 
        /// uploaded, set this to True. The method will then continually refresh the page until the cell text 
        /// matches. We dont need to do this for Mainpro UI Submissions because we now have an indicator telling 
        /// us when the Credit Service has completed. See https://code.premierinc.com/issues/browse/MAINPROREW-893
        /// and see https://code.premierinc.com/issues/browse/CFPC-2903
        /// </param>
        public void VerifyCellTextMatches(IWebDriver Browser, Page Page, Const_Mainpro.Table tbl, string cellTextExpected,
            string rowName = null, string colName = null, string rowIndex = null, string colIndex = null,
            bool wasActivityUploaded = false)
        {
            // If the activity was submitted through the Mainpro UI, then we dont need to refresh in a loop and 
            // continuously check that cell text matches, because we have auto code in place after clicking the 
            // Submit button on the Activity Details page which checks for a "Credit Service = Complete" label.
            if (!wasActivityUploaded)
            {
                string cellTextActual = Grid_GetRowCellTextByName(Browser, tbl, rowName, colName, rowIndex, colIndex);

                if (cellTextActual.Trim() == cellTextExpected.Trim())
                {
                    return;
                }
                else
                {
                    string error = string.Format("The test expected cell text '{0}', But was '{1}'. Text was " +
                        "extracted from the {2} table, rowName = '{3}' colName = '{4}' rowIndex = '{5}' colIndex = '{6}'." +
                        "1 of 3 things caused this failure. 1. Either you hard-coded the wrong credit number to wait for, " +
                        "or put the user in the wrong cycle, which would result in this method never finding your " +
                        "hard-coded credit number. Verify the cellTextExpected parameter is correct. If it is correct " +
                        "then 2. The application has a defect and the wrong credit number is appearing. See the Description in the " +
                        "XML comments of this method for more info. ", cellTextExpected, cellTextActual,
                        tbl.GetDescription().ToString(), rowName, colName, rowIndex, colIndex);
                    throw new Exception(error);
                }
            }

            // if activity was uploaded, then we dont have a Credit Service Completion indicator, so we dont know when
            // the Credit Service completed, so we have to keep refreshing and verifying
            else
            {
                for (int i = 1; i < 40; i++)
                {
                    string cellTextActual = Grid_GetRowCellTextByName(Browser, tbl, rowName, colName, rowIndex, colIndex);

                    if (cellTextActual.Trim() == cellTextExpected.Trim())
                    {
                        break;
                    }

                    if (i == 39)
                    {
                        throw new Exception(string.Format("The test expected cell text '{0}', But was '{1}'. Text was " +
                            "extracted from the {2} table, rowName = '{3}' colName = '{4}' rowIndex = '{5}' colIndex = '{6}'." +
                            "Failing this test because we refreshed 40 times at this point waiting for credits " +
                            "for a match, and that is too long. " +
                            "1 of 3 things caused this failure. 1. Either you hard-coded the wrong credit number to wait for, " +
                            "or put the user in the wrong cycle, which would result in this method never finding your " +
                            "hard-coded credit number. Verify the cellTextExpected parameter is correct. If it is correct " +
                            "then 2. The Windows Service which queues a bunch of records is overloaded and is taking " +
                            "forever (longer time than this loop provides). If this is the case, DEV will need to fix the " +
                            "refresh process again if it starts taking too long. If this is not the case, then 3. The " +
                            "application has a defect and the wrong credit number is appearing. See the Description in the " +
                            "XML comments of this method for more info. ", cellTextExpected, cellTextActual,
                            tbl.GetDescription().ToString(), rowName, colName, rowIndex, colIndex));
                    }

                    Thread.Sleep(3000);
                    // If we are verifying the AMA/RCP popup, after we refresh, we have to open it
                    if (Browser.Exists(Bys.MainproPage.AMARCPMaxCreditFormTblFirstRow, ElementCriteria.IsVisible))
                    {
                        Page.RefreshPage(true);
                        CreditSummaryPage randomPage = new CreditSummaryPage(Browser);
                        randomPage.ClickAndWaitBasePage(randomPage.ClickHereToViewYourAmaRCPCreditsLnk);
                    }
                    // If we are verifying the View All Cycles popup, after we refresh, we have to open it
                    else if (Browser.Exists(Bys.MainproPage.AMARCPMaxCreditFormTblFirstRow, ElementCriteria.IsVisible))
                    {
                        Page.RefreshPage(true);
                        CreditSummaryPage CSP = new CreditSummaryPage(Browser);
                        CSP.ClickAndWait(CSP.ViewAllCyclesBtn);
                    }
                    else
                    {
                        Page.RefreshPage(true);
                    }
                }
            }
        }

        /// <summary>
        /// Clicks on a user-specified cell or button within a cell. You can choose to click on the cell/button by
        /// many methods... 
        /// 1. By specifying the row name only (firstColumnCellText), it will click on the first column within that row
        /// 2. By specifying the row name (firstColumnCellText) and button name (tblBtnOrLnk), it will click on the 
        /// user-specified button or link within the user-specified row
        /// 3. By specifying cell text only (cellText), it will click on the first indexed cell found with the 
        /// user-specified cell text
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="tbl">The table to click on</param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>     
        /// <param name="tblBtnOrLnk">(Optional). If you want to click on a button or link within the row, add this 
        /// parameter. NOTE: You can only choose one of the following parameters: tblBtnOrLnk, cellText, colIndex. 
        /// If you dont send any of these 3, the method will click on the first column cell/></param>
        /// <param name="cellText">(Optional). If you want to click on a ell by its cell text. NOTE: You can only choose 
        /// one of the following parameters: tblBtnOrLnk, cellText, colIndex. If you dont send any of these 3, the 
        /// method will click on the first column cell/></param>
        /// <param name="rowNum">(Optional). If you passed a value for cellText and you want to specify which row the 
        /// cell text exists within</param>
        public dynamic Grid_ClickCellInTable(IWebDriver Browser, Const_Mainpro.Table tbl, string firstColumnCellText = null,
            Const_Mainpro.TableButtonLinkOrCheckBox? tblBtnOrLnk = null, string cellText = null, int? rowNum = null)
        {
            // If the user chose both tblBtnOrLnk and cellText, throw error
            if (tblBtnOrLnk != null && cellText != null)
            {
                throw new Exception("You can only pass one of either tblBtnOrLnk or cellText");
            }

            // If the user chose to click on a buttonOrlink but did not provide the firstColumnCellText parameter
            if (tblBtnOrLnk != null && firstColumnCellText == null)
            {
                throw new Exception("You must specify the firstColumnCellText parameter if you want to click on a " +
                    "button or link within a row");
            }

            // If the user chose to click on a cell by cell text but also passed firstColumnCellText
            if (cellText != null && firstColumnCellText != null)
            {
                throw new Exception("You specified a cell text to click on, but also passed firstColumnCellText. " +
                    "firstColumnCellText can not be used when clicking on cell text");
            }

            // If the user chose the Credit Summary widget or the activity tables on the Credit Summary tab to click on,
            // throw error saying these are not clickable
            switch (tbl)
            {
                // case Const_Mainpro.Table.CreditSummaryTabGroupLearn:
                // case Const_Mainpro.Table.CreditSummaryTabSelfLearn:
                //case Const_Mainpro.Table.CreditSummaryTabAssessment:
                case Const_Mainpro.Table.CreditSummaryTabOther:
                case Const_Mainpro.Table.CreditSummaryWidgetCycle:
                case Const_Mainpro.Table.CreditSummaryWidgetCurrentYear:
                case Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs:
                case Const_Mainpro.Table.AMARCPMaxCreditForm:
                    throw new Exception("This table is not clickable");
            }

            // Instantiate all the necessary page classes
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
            CPDPlanningPage PP = new CPDPlanningPage(Browser);
            CreditSummaryPage CSP = new CreditSummaryPage(Browser);
            Step3Page PS3 = new Step3Page(Browser);

            // Get the table iWebElement and the row By
            List<object> tblElemsAndRowBy = Grid_GetTableElemsAndBys(Browser, tbl);
            IWebElement tblToClick = (IWebElement)tblElemsAndRowBy[0];
            By rowBy = (By)tblElemsAndRowBy[4];

            // If the user chose to click a button or link within the table
            switch (tblBtnOrLnk)
            {
                case Const_Mainpro.TableButtonLinkOrCheckBox.Edit:
                    // If we are on the CPD Activities List page
                    if (Browser.Exists(Bys.CPDActivitiesListPage.ActTbl, ElementCriteria.IsVisible))
                    {
                        ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, tblToClick, rowBy, firstColumnCellText,
                            "span", tblBtnOrLnk.GetDescription().ToString(), "span");
                        EADP.WaitForInitialize();
                        return EADP;
                    }
                    // If we are on the PLP Step 3 CPD Events page
                    else if (Browser.Exists(Bys.Step3Page.CPDEventsTbl, ElementCriteria.IsVisible))
                    {
                        ElemSet.Grid_ClickButtonOrLinkWithoutTextByPartialClassName(Browser, tblToClick, rowBy,
                            firstColumnCellText, "span", "div", "edit");
                        Browser.WaitForElement(Bys.Step3Page.ActivityDetailFormCityTxt, ElementCriteria.IsVisible);
                        Browser.WaitJSAndJQuery();
                        return PS3;
                    }
                    else if (Browser.Exists(Bys.Step5Page.UsefulCPDActivitiesTbl, ElementCriteria.IsVisible))
                    {
                        ElemSet.Grid_ClickButtonOrLinkWithoutTextByPartialClassName(Browser, tblToClick, rowBy,
                            firstColumnCellText, "span", "div", "edit");
                        Browser.WaitForElement(Bys.Step3Page.ActivityDetailFormCityTxt, ElementCriteria.IsVisible);
                        Browser.WaitJSAndJQuery();
                        return PS3;
                    }
                    // Else if we are on the Credit Summary View form
                    else
                    {
                        ElemSet.Grid_ClickButtonOrLinkWithoutTextByPartialClassName(Browser, tblToClick, rowBy,
                            firstColumnCellText, "span", "div", "edit");
                        EADP.WaitForInitialize();
                        return EADP;
                    }
                case Const_Mainpro.TableButtonLinkOrCheckBox.View:
                    ElemSet.Grid_ClickButtonOrLinkWithoutTextByPartialClassName(Browser, tblToClick, rowBy,
                        firstColumnCellText, "span", "div", "view");
                    Browser.WaitJSAndJQuery();
                    return null;
                case Const_Mainpro.TableButtonLinkOrCheckBox.ViewDetails:
                    ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, tblToClick, rowBy, firstColumnCellText,
                        "span", tblBtnOrLnk.GetDescription().ToString(), "span");
                    EADP.WaitForInitialize();
                    return EADP;
                case Const_Mainpro.TableButtonLinkOrCheckBox.CompleteActivity:
                    ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, tblToClick, rowBy, firstColumnCellText,
                        "span", tblBtnOrLnk.GetDescription().ToString(), "span");
                    EADP.WaitForInitialize();
                    return EADP;
                case Const_Mainpro.TableButtonLinkOrCheckBox.Select:
                    ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, tblToClick, rowBy, firstColumnCellText,
                        "span", tblBtnOrLnk.GetDescription().ToString(), "span");
                    EADP.WaitForInitialize();
                    return EADP;
                case Const_Mainpro.TableButtonLinkOrCheckBox.Delete:
                    ElemSet.Grid_ClickButtonOrLinkWithoutTextByPartialClassName(Browser, tblToClick, rowBy,
                        firstColumnCellText, "span", "div", "trash");
                    PS3.WaitUntilAny(Criteria.Step3Page.DeleteFormYesBtnVisible,
                        Criteria.Step3Page.PLP_AreYouSureYouWantToDeleteFormDeleteBtnVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                case Const_Mainpro.TableButtonLinkOrCheckBox.CheckBox:
                    // If we are on the PLP Step 3 CPD Events page
                    if (Browser.Exists(Bys.Step3Page.CPDEventsTbl, ElementCriteria.IsVisible))
                    {
                        //Need to add this for selecting future year
                        //IWebElement row = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, tblToClick, rowBy, firstColumnCellText, "div",
                        //    "02/01/23", "td");
                        //ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, row, "input");

                        ElemSet.Grid_ClickButtonOrLinkWithoutTextByPartialClassName(Browser, tblToClick, rowBy,
                            firstColumnCellText, "div", "div", "Select");
                        return null;
                    }
                    // If we are on the PLP Step 5 useful CPD programs page
                    if (Browser.Exists(Bys.Step5Page.UsefulCPDActivitiesTbl, ElementCriteria.IsVisible))
                    {
                        ElemSet.Grid_ClickButtonOrLinkWithoutTextByPartialClassName(Browser, tblToClick, rowBy,
                            firstColumnCellText, "div", "div", "Select");
                        return null;
                    }
                    return null;
                case Const_Mainpro.TableButtonLinkOrCheckBox.ActionsButton:


                    return null;
            }

            // If the user chose to click on a cell by cell text
            if (!string.IsNullOrEmpty(cellText))
            {
                try
                {
                    ElemSet.ScrollToElement(Browser, tblToClick);
                    ElemSet.Grid_ClickCellByCellText(Browser, tblToClick, cellText, rowNum);
                }
                catch  { }

                switch (tbl)
                {
                    case Const_Mainpro.Table.DashboardTabInc:
                    case Const_Mainpro.Table.DashbooardTabPendAppr:
                        EADP.WaitForInitialize();
                        return EADP;
                    case Const_Mainpro.Table.DashbooardTabGoal:
                        PP.WaitForInitialize();
                        return PP;
                    case Const_Mainpro.Table.CreditSummaryTabViewAllCyclesFormCycle:
                        CSP.WaitForInitialize();
                        return CSP;
                    case Const_Mainpro.Table.CreditSummaryTabAssessment:
                    case Const_Mainpro.Table.CreditSummaryTabGroupLearn:
                    case Const_Mainpro.Table.CreditSummaryTabSelfLearn:
                        Browser.WaitForElement(Bys.CreditSummaryPage.CSViewFormViewActivitiesTbl, ElementCriteria.IsVisible);
                        Browser.WaitJSAndJQuery();
                        return null;
                }
            }

            // Else click on the first cell of the user-specified row
            else
            {
                ElemSet.Grid_ClickCellByCellText(Browser, tblToClick, firstColumnCellText);

                // Add the wait criteria depending on the table
                switch (tbl)
                {
                    case Const_Mainpro.Table.CPDActitivitesListTabAct:
                    case Const_Mainpro.Table.HoldingAreaSummTabInc:
                    case Const_Mainpro.Table.HoldingAreaSummTabPendAppr:
                    case Const_Mainpro.Table.HoldingAreaIncTabInc:
                    case Const_Mainpro.Table.HoldingAreaCredValTabPendAppr:
                    case Const_Mainpro.Table.DashboardTabInc:
                    case Const_Mainpro.Table.DashbooardTabPendAppr:
                        EADP.WaitForInitialize();
                        return EADP;
                    case Const_Mainpro.Table.PlanningTabGoal:
                    case Const_Mainpro.Table.DashbooardTabGoal:
                        PP.WaitForInitialize();
                        return PP;

                }
            }

            throw new Exception("Code should not have reached here. If it did, then this method needs updated");
        }

        private List<object> Grid_GetTableElemsAndBys(IWebDriver Browser, Const_Mainpro.Table tbl)
        {
            // Instantiate all the necessary page classes
            EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
            EnterACPDActivityPage EP = new EnterACPDActivityPage(Browser);
            DashboardPage DP = new DashboardPage(Browser);
            CPDActivitiesListPage ALP = new CPDActivitiesListPage(Browser);
            LoginPage LP = new LoginPage(Browser);
            CPDPlanningPage PP = new CPDPlanningPage(Browser);
            PLPHubPage PLPHP = new PLPHubPage(Browser);
            HoldingAreaPage HP = new HoldingAreaPage(Browser);
            CreditSummaryPage CSP = new CreditSummaryPage(Browser);
            Step3Page PLPStep3P = new Step3Page(Browser);
             Step5Page PLPStep5P = new Step5Page(Browser);

            IWebElement tblElem = null;
            IWebElement tblHdrElem = null;
            IWebElement tblBodyElem = null;
            By bodyBy = null;
            By firstRowBy = null;
            By tblElemBy = null;

            switch (tbl)
            {
                case Const_Mainpro.Table.ActivitySearchResults:
                    tblElem = EP.SearchResultsTbl;
                    tblHdrElem = EP.SearchResultsTblHdr;
                    tblBodyElem = EP.SearchResultsTblBody;
                    bodyBy = Bys.EnterACPDActivityPage.SearchResultsTblBody;
                    firstRowBy = Bys.EnterACPDActivityPage.SearchResultsTblFirstRow;
                    tblElemBy = Bys.EnterACPDActivityPage.SearchResultsTbl;
                    break;
                case Const_Mainpro.Table.AMARCPMaxCreditForm:
                    tblElem = CSP.AMARCPMaxCreditFormTbl;
                    tblHdrElem = CSP.AMARCPMaxCreditFormTblHdr;
                    tblBodyElem = CSP.AMARCPMaxCreditFormTblBody;
                    bodyBy = Bys.MainproPage.AMARCPMaxCreditFormTblBody;
                    firstRowBy = Bys.MainproPage.AMARCPMaxCreditFormTblFirstRow;
                    tblElemBy = Bys.MainproPage.AMARCPMaxCreditFormTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryTabViewAllCyclesFormCycle:
                    tblElem = CSP.ViewAllCyclesFormCyclesTbl;
                    tblHdrElem = CSP.ViewAllCyclesFormCyclesTblHdr;
                    tblBodyElem = CSP.ViewAllCyclesFormCyclesTblBody;
                    bodyBy = Bys.CreditSummaryPage.ViewAllCyclesFormCyclesTblBody;
                    firstRowBy = Bys.CreditSummaryPage.ViewAllCyclesFormCyclesTblBodyRow;
                    tblElemBy = Bys.CreditSummaryPage.ViewAllCyclesFormCyclesTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryTabViewFormViewActivities:
                    tblElem = CSP.CSViewFormViewActivitiesTbl;
                    tblHdrElem = CSP.CSViewFormViewActivitiesTblHdr;
                    tblBodyElem = CSP.CSViewFormViewActivitiesTblBody;
                    bodyBy = Bys.CreditSummaryPage.CSViewFormViewActivitiesTblBody;
                    firstRowBy = Bys.CreditSummaryPage.CSViewFormViewActivitiesTblBodyRow;
                    tblElemBy = Bys.CreditSummaryPage.CSViewFormViewActivitiesTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryTabGroupLearn:
                    tblElem = CSP.GroupLearningTbl;
                    tblHdrElem = CSP.GroupLearningTblHdr;
                    tblBodyElem = CSP.GroupLearningTblBody;
                    bodyBy = Bys.CreditSummaryPage.GroupLearningTblBody;
                    firstRowBy = Bys.CreditSummaryPage.GroupLearningTblBodyRow;
                    tblElemBy = Bys.CreditSummaryPage.GroupLearningTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryTabSelfLearn:
                    tblElem = CSP.SelfLearningTbl;
                    tblHdrElem = CSP.SelfLearningTblHdr;
                    tblBodyElem = CSP.SelfLearningTblBody;
                    bodyBy = Bys.CreditSummaryPage.SelfLearningTblBody;
                    firstRowBy = Bys.CreditSummaryPage.SelfLearningTblBodyRow;
                    tblElemBy = Bys.CreditSummaryPage.SelfLearningTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryTabAssessment:
                    tblElem = CSP.AssessmentTbl;
                    tblHdrElem = CSP.AssessmentTblHdr;
                    tblBodyElem = CSP.AssessmentTblBody;
                    bodyBy = Bys.CreditSummaryPage.AssessmentTblBody;
                    firstRowBy = Bys.CreditSummaryPage.AssessmentTblBodyRow;
                    tblElemBy = Bys.CreditSummaryPage.AssessmentTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryTabOther:
                    tblElem = CSP.OtherTbl;
                    tblHdrElem = CSP.OtherTblHdr;
                    tblBodyElem = CSP.OtherTblBody;
                    bodyBy = Bys.CreditSummaryPage.OtherTblBody;
                    firstRowBy = Bys.CreditSummaryPage.OtherTblBodyRow;
                    tblElemBy = Bys.CreditSummaryPage.OtherTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryWidgetCycle:
                    tblElem = CSP.CredSummCycleTbl;
                    tblHdrElem = CSP.CredSummCycleTblHdr;
                    tblBodyElem = CSP.CredSummCycleTblBody;
                    bodyBy = Bys.MainproPage.CredSummCycleTblBody;
                    firstRowBy = Bys.MainproPage.CredSummCycleTblFirstRow;
                    tblElemBy = Bys.MainproPage.CredSummCycleTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryWidgetCurrentYear:
                    tblElem = CSP.CredSummCurrentYrTbl;
                    tblHdrElem = CSP.CredSummCurrentYrTblHdr;
                    tblBodyElem = CSP.CredSummCurrentYrTblBody;
                    bodyBy = Bys.MainproPage.CredSummCurrentYrTblBody;
                    firstRowBy = Bys.MainproPage.CredSummCurrentYrTblFirstRow;
                    tblElemBy = Bys.MainproPage.CredSummCurrentYrTbl;
                    break;
                case Const_Mainpro.Table.CreditSummaryWidgetAnnualReqs:
                    tblElem = CSP.AnnualRequirementsTbl;
                    tblHdrElem = CSP.AnnualRequirementsTblHdr;
                    tblBodyElem = CSP.AnnualRequirementsTblBody;
                    bodyBy = Bys.MainproPage.CredSummAnnualReqsTblBody;
                    firstRowBy = Bys.MainproPage.CredSummAnnualReqsTblFirstRow;
                    tblElemBy = Bys.MainproPage.CredSummAnnualReqsTbl;
                    break;
                case Const_Mainpro.Table.CPDActitivitesListTabAct:
                    tblElem = ALP.ActTbl;
                    tblHdrElem = ALP.ActTblHdr;
                    tblBodyElem = ALP.ActTblBody;
                    bodyBy = Bys.CPDActivitiesListPage.ActTblBody;
                    firstRowBy = Bys.CPDActivitiesListPage.ActTblBodyFirstRow;
                    tblElemBy = Bys.CPDActivitiesListPage.ActTbl;
                    break;
                case Const_Mainpro.Table.HoldingAreaSummTabInc:
                    tblElem = HP.SummTabIncompActTbl;
                    tblHdrElem = HP.SummTabIncompActTblHdr;
                    tblBodyElem = HP.SummTabIncompActTblBody;
                    bodyBy = Bys.HoldingAreaPage.SummTabIncompActTblBody;
                    firstRowBy = Bys.HoldingAreaPage.SummTabIncompActTblBodyFirstRow;
                    tblElemBy = Bys.HoldingAreaPage.SummTabIncompActTbl;
                    break;
                case Const_Mainpro.Table.HoldingAreaSummTabPendAppr:
                    tblElem = HP.SummTabActPendApprTbl;
                    tblHdrElem = HP.SummTabActPendApprTblHdr;
                    tblBodyElem = HP.SummTabActPendApprTblBody;
                    bodyBy = Bys.HoldingAreaPage.SummTabActPendApprTblBody;
                    firstRowBy = Bys.HoldingAreaPage.SummTabActPendApprTblBodyFirstRow;
                    tblElemBy = Bys.HoldingAreaPage.SummTabActPendApprTbl;
                    break;
                case Const_Mainpro.Table.HoldingAreaIncTabInc:
                    tblElem = HP.IncompleteTabActTbl;
                    tblHdrElem = HP.IncompleteTabActTblHdr;
                    tblBodyElem = HP.IncompleteTabActTblBody;
                    bodyBy = Bys.HoldingAreaPage.IncompleteTabActTblBody;
                    firstRowBy = Bys.HoldingAreaPage.IncompleteTabActTblBodyFirstRow;
                    tblElemBy = Bys.HoldingAreaPage.IncompleteTabActTbl;
                    break;
                case Const_Mainpro.Table.HoldingAreaCredValTabPendAppr:
                    tblElem = HP.CredValTabActTbl;
                    tblHdrElem = HP.CredValTabActTblHdr;
                    tblBodyElem = HP.CredValTabActTblBody;
                    bodyBy = Bys.HoldingAreaPage.CredValTabActTblBody;
                    firstRowBy = Bys.HoldingAreaPage.CredValTabActTblBodyFirstRow;
                    tblElemBy = Bys.HoldingAreaPage.CredValTabActTbl;
                    break;
                case Const_Mainpro.Table.PlanningTabGoal:
                    tblElem = PP.GoalsTbl;
                    tblHdrElem = PP.GoalsTblHdr;
                    tblBodyElem = PP.GoalsTblBody;
                    bodyBy = Bys.CPDPlanningPage.GoalsTblBody;
                    firstRowBy = Bys.CPDPlanningPage.GoalsTblBodyFirstRow;
                    tblElemBy = Bys.CPDPlanningPage.GoalsTbl;
                    break;
                case Const_Mainpro.Table.PLPHubCompletedPLPTbl:
                    tblElem = PLPHP.PLPHubCompletedPLPTbl;
                    tblHdrElem = PLPHP.PLPHubCompletedPLPTblHdr;
                    tblBodyElem = PLPHP.PLPHubCompletedPLPTblBody;
                    bodyBy = Bys.PLPHubPage.PLPHubCompletedPLPTblBody;
                    firstRowBy = Bys.PLPHubPage.PLPHubCompletedPLPTblBodyFirstRow;
                    tblElemBy = Bys.PLPHubPage.PLPHubCompletedPLPTbl;
                    break;
                case Const_Mainpro.Table.DashboardTabInc:
                    tblElem = DP.IncompleteActivitiesTbl;
                    tblHdrElem = DP.IncompleteActivitiesTblHdr;
                    tblBodyElem = DP.IncompleteActivitiesTblBody;
                    bodyBy = Bys.DashboardPage.IncompleteActivitiesTblBody;
                    firstRowBy = Bys.DashboardPage.IncompleteActivitiesTblBodyFirstRow;
                    tblElemBy = Bys.DashboardPage.IncompleteActivitiesTbl;
                    break;
                case Const_Mainpro.Table.DashbooardTabPendAppr:
                    tblElem = DP.ActivitiesNeedCreditApprovalTbl;
                    tblHdrElem = DP.ActivitiesNeedCreditApprovalTblHdr;
                    tblBodyElem = DP.ActivitiesNeedCreditApprovalTblBody;
                    bodyBy = Bys.DashboardPage.ActivitiesNeedCreditApprovalTblBody;
                    firstRowBy = Bys.DashboardPage.ActivitiesNeedCreditApprovalTblBodyFirstRow;
                    tblElemBy = Bys.DashboardPage.ActivitiesNeedCreditApprovalTbl;
                    break;
                case Const_Mainpro.Table.DashbooardTabGoal:
                    tblElem = DP.PersonalLearnPlanTbl;
                    tblHdrElem = DP.PersonalLearnPlanTblHdr;
                    tblBodyElem = DP.PersonalLearnPlanTblBody;
                    bodyBy = Bys.DashboardPage.PersonalLearnPlanTblBody;
                    firstRowBy = Bys.DashboardPage.PersonalLearnPlanTblBodyFirstRow;
                    tblElemBy = Bys.DashboardPage.PersonalLearnPlanTbl;
                    break;
                case Const_Mainpro.Table.PLPStep3Events:
                    tblElem = PLPStep3P.CPDEventsTbl;
                    tblHdrElem = PLPStep3P.CPDEventsTblHdr;
                    tblBodyElem = PLPStep3P.CPDEventsTblBody;
                    bodyBy = Bys.Step3Page.CPDEventsTblBody;
                    firstRowBy = Bys.Step3Page.CPDEventsTblBodyFirstRow;
                    tblElemBy = Bys.Step3Page.CPDEventsTbl;
                    break;
                case Const_Mainpro.Table.PLPStep3SetYourCPDGoalsSelectedActivities:
                    tblElem = PLPStep3P.SetYourGoalsSelectedActivitiesTbl;
                    tblHdrElem = PLPStep3P.SetYourGoalsSelectedActivitiesTblHdr;
                    tblBodyElem = PLPStep3P.SetYourGoalsSelectedActivitiesTblBody;
                    bodyBy = Bys.Step3Page.SetYourGoalsSelectedActivitiesTblBody;
                    firstRowBy = Bys.Step3Page.SetYourGoalsSelectedActivitiesTblBodyFirstRow;
                    tblElemBy = Bys.Step3Page.SetYourGoalsSelectedActivitiesTbl;
                    break;
                case Const_Mainpro.Table.PLPStep3IdentifiedGaps:
                    tblElem = PLPStep3P.PLPStep3IdentifiedGapsTbl;
                    tblHdrElem = PLPStep3P.PLPStep3IdentifiedGapsTblHdr;
                    tblBodyElem = PLPStep3P.PLPStep3IdentifiedGapsTblBody;
                    bodyBy = Bys.Step3Page.PLPStep3IdentifiedGapsTblBody;
                    firstRowBy = Bys.Step3Page.PLPStep3IdentifiedGapsTblBodyFirstRow;
                    tblElemBy = Bys.Step3Page.PLPStep3IdentifiedGapsTbl;
                    break;
                case Const_Mainpro.Table.PLPStep5PreReflectionYourSelectedActivities:
                    tblElem = PLPStep5P.PreReflectionCPDActivitiesTbl;
                    tblHdrElem = PLPStep5P.PreReflectionCPDActivitiesTblHdr;
                    tblBodyElem = PLPStep5P.PreReflectionCPDActivitiesTblBody;
                    bodyBy = Bys.Step5Page.PreReflectionCPDActivitiesTblBody;
                    firstRowBy = Bys.Step5Page.PreReflectionCPDActivitiesTblBodyFirstRow;
                    tblElemBy = Bys.Step5Page.PreReflectionCPDActivitiesTbl;
                    break;
                case Const_Mainpro.Table.PLPStep5UsefulCPDActivities:
                    tblElem = PLPStep5P.UsefulCPDActivitiesTbl;
                    tblHdrElem = PLPStep5P.UsefulCPDActivitiesTblHdr;
                    tblBodyElem = PLPStep5P.UsefulCPDActivitiesTblBody;
                    bodyBy = Bys.Step5Page.UsefulCPDActivitiesTblBody;
                    firstRowBy = Bys.Step5Page.UsefulCPDActivitiesTblBodyFirstRow;
                    tblElemBy = Bys.Step5Page.UsefulCPDActivitiesTbl;
                    break;
                default:
                    break;
            }

            return new List<object>() { tblElem, tblHdrElem, tblBodyElem, bodyBy, firstRowBy, tblElemBy };
        }


        #endregion methods: tables

        #region methods: Check boxes, text boxes, etc.

        /// <summary>
        /// Clicks a check box based on its label name in PLP. We created this to manipulate check boxes in PLP instead of 
        /// hardcoding all of the many checkboxes that exist on the page
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="checkBoxLabelTitle">The label of the check box</param>
        public void PLP_ClickCheckBoxOrRadioButton(IWebDriver Browser, string checkBoxLabelTitle)
        {
            string xpathForGaps = null;
            if (checkBoxLabelTitle.Contains("Gap 1"))
            {
                xpathForGaps = "| //span[contains(@id, 'gap')]";
            }
            else if (checkBoxLabelTitle.Contains("Gap 2"))
            { 
                xpathForGaps = "| (//span[contains(@id, 'gap')])[2]";
            }
            else if (checkBoxLabelTitle.Contains("Gap 3"))
            {
                xpathForGaps = "| (//span[contains(@id, 'gap')])[3]";
            }

            Thread.Sleep(30);
            Browser.WaitJSAndJQuery();
            string xpath = string.Format(
                "//label[text()='{0}'] | //input[@value='{0}'] | //input[@aria-label='{0}'] | //span[text()='{0}'] {1}",
                checkBoxLabelTitle, xpathForGaps);
            IWebElement Chk = Browser.FindElement(By.XPath(xpath));
            try { Chk.Click(); }
            catch  { Chk.ClickJS(Browser); }
            
        }

        /// <summary>
        /// Enters text into a text box based on its section name (yellow banners) and label name in PLP. This can be used on the 
        /// following pages: We created this to manipulate text boxes in PLP instead of hardcoding all of the many text boxes 
        /// that exist on the page
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="textboxLabelTitle">The label of the text box</param>
        /// <param name="textToEnter">The text you want to enter</param>
        /// <param name="sectionTitle">(Optional). The section title. This is useful if there are textboxes with the same title on the 
        /// same page. You can differentiate between them by sending the section title (text in the yellow banner)</param>
        public void PLP_EnterText(IWebDriver Browser, string textboxLabelTitle, string textToEnter, string sectionTitle = null)
        {
            string xpathForTextAreaElems =
                string.Format("//*[text()='{0}']/../../descendant::label[contains(text(), '{1}')]/following-sibling::textarea | //*[text()='']/../../descendant::textarea[@aria-label='{1}']",
                sectionTitle,
                textboxLabelTitle);

            string xpathForInputElems =
                string.Format("//*[text()='{0}']/../../descendant::input[@aria-label='{1}'] | //*[text()='{1}']/../descendant::input",
                sectionTitle,
                textboxLabelTitle);

            string fullXpath = xpathForTextAreaElems + " | " + xpathForInputElems;

            Browser.FindElement(By.XPath(fullXpath)).SendKeys(textToEnter);
        }

        /// <summary>
        /// This method is to caluclate 6 weeks period from step5 submission date and
        /// will return the PLP post-reflection(Step6) unlocked date.
        /// </summary>
        /// <returns></returns>
        public string PLP_PostReflection_UnlockDate()
        {
            string PLP_PostReflection_UnlockDate= 
                currentDatetime.AddDays(42).ToString("MM/dd/yyyy",CultureInfo.InvariantCulture);
            return PLP_PostReflection_UnlockDate;
        }

        /// <summary>
        /// This method is to caluclate the date based on goal setting timeline period 
        /// user chosen during step3 and 
        /// will return the Timeline period date.
        /// </summary>
        /// <returns></returns>
        public string PLP_GoalSetting_TimelineDate(int months)
        {
            string submittedDate = currentDatetime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string enddate =
                currentDatetime.AddMonths(months).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string PLP_GoalSetting_TimelineDate =
                String.Format("{0} - {1}", submittedDate, enddate);
            return PLP_GoalSetting_TimelineDate;
        }


        #endregion methods: Check boxes, text boxes, etc.

        #endregion methods
    }

    public class Activity
    {
        #region properties

        public IWebDriver Browser { get; set; }
        public string Category { get; set; }
        public string CertType { get; set; }
        public string ActType { get; set; }
        public string Title { get; set; }
        public string actDate { get; set; }

        #endregion properties

        #region constructors

        public Activity(IWebDriver browser, string category, string certType, string actType, string title, string actDate)
        {
            Browser = browser;
            Category = category;
            CertType = certType;
            ActType = actType;
            Title = title;
            this.actDate = actDate;
        }



        #endregion constructors

        #region methods


        #endregion methods

    }

    public class Goal
    {
        #region properties

        public IWebDriver Browser { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string DueDate { get; set; }

        #endregion properties

        #region constructors

        public Goal(IWebDriver browser, string title, string type, string dueDate)
        {
            Browser = browser;
            Title = title;
            Type = type;
            DueDate = dueDate;
        }



        #endregion constructors

        #region methods


        #endregion methods

    }

    public class PLP_DomainsSelection
    {
        #region properties

        public IWebDriver Browser { get; set; }
        public string PrimaryDomain { get; set; }
        public List<string> SubDomains { get; set; }
        
        #endregion properties

        #region constructors

        public PLP_DomainsSelection( string primarydomain, List<string> subdomains)
        {
             PrimaryDomain = primarydomain;
            SubDomains = subdomains;
        }



        #endregion constructors

        #region methods


        #endregion methods

    }

    public class PLP_Event
    {
        #region properties

        //public IWebDriver Browser { get; set; }
        public string ActivityTitle { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string EventType { get; set; }
        public List<string> Gaps { get; set; }

        #endregion properties

        #region constructors

        public PLP_Event(string activityTitle, string date, string category, string city, string province,
            string eventType, List<string> gaps)
        { 
            //Browser = browser;
            ActivityTitle = activityTitle;
            Date = date;
            Category = category;
            City = city;
            Province = province;
            EventType = eventType;
            Gaps = gaps;
        }



        #endregion constructors

        #region methods

        ///<summary>
        /// Checks the presence of info Button in the page and Clicks it from a specific step and so it navigates to new tab in browser 
        /// In the new tab, checks whether the info section is expanded for the current step only and all other Step info sections are collapsed
        ///</summary>
        /// <param name="Browser"></param>

        public static void PLP_infoBtn(IWebDriver Browser, char IdNumber)
        {
            if (Browser.Exists(Bys.MainproPage.InfoBtn))
            {
                Browser.FindElement(Bys.MainproPage.InfoBtn).ClickJS(Browser);
                Browser.SwitchTo().Window(Browser.WindowHandles[1]);
                //Getting the webelements of Expand button of all steps and store it in a list
                IList<IWebElement> panellist = Browser.FindElements(Bys.MainproPage.PanelBodyList);
                //Itrating webelements list for each and every step 
                foreach (IWebElement EachSection in panellist)
                {
                    //checks whether the "class" attribute contains "in" , as the keyword "in" will appear only after expanding a section.
                    if (EachSection.GetAttribute("class").Contains("in"))
                    {
                        //Verifies current Step number is expanded
                        Assert.True(EachSection.GetAttribute("id").Contains(IdNumber));
                        Console.WriteLine("The Step " + IdNumber + " section is expanded");
                    }
                }
                Browser.Close();
                Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            }
            else { Console.WriteLine("No info Icon Present in current screen- " +Browser.Url); }
        }
        public static void PLP_ToolsResourceOptionClickFromUserProfile(IWebDriver Browser, char IdNumber)
        {

            Browser.FindElement(Bys.MainproPage.PLP_Menu_DropDownBtn).Click();
            Browser.WaitForElement(Bys.MainproPage.PLP_Menu_PLPToolsAndResources, ElementCriteria.IsEnabled);
            Thread.Sleep(60);
            try
            {
                Browser.FindElement(Bys.MainproPage.PLP_Menu_PLPToolsAndResources).Click();
                Thread.Sleep(60);
            }
            catch { }
            Browser.SwitchTo().Window(Browser.WindowHandles[1]);
                //Getting the webelements of Expand button of all steps and store it in a list
                IList<IWebElement> panellist = Browser.FindElements(Bys.MainproPage.PanelBodyList);
                //Itrating webelements list for each and every step 
                foreach (IWebElement EachSection in panellist)
                {
                    //checks whether the "class" attribute contains "in" , as the keyword "in" will appear only after expanding a section.
                    if (EachSection.GetAttribute("class").Contains("in"))
                    {
                        Assert.True(EachSection.GetAttribute("id").Contains(IdNumber));
                        Console.WriteLine("The Step " + IdNumber + " section is expanded");
                    }
                }
                Browser.Close();
                Browser.SwitchTo().Window(Browser.WindowHandles[0]);
            Browser.FindElement(Bys.MainproPage.PLP_Menu_CloseBtn).Click();

        }
        /// <summary>
        /// Checks which page you are in, and verifies PLP_infoBtn method for the corresponding step
        /// <param name="Browser"></param>
        public static void PLP_ToolsAndResourcesSection(IWebDriver Browser, bool iIconClickCheck=false,
            bool ToolsOptionsClickFromUserProfileCheck=false)
        {
            if (iIconClickCheck)
            {
                IList<IWebElement> i_Btn_list = Browser.FindElements(Bys.MainproPage.InfoButtons_SelectYourPathway);
                //Condition for Select your pathway Page
                if (Browser.Exists(Bys.MainproPage.SelectYourPathway_PageTitle))
                {
                    for (int i = 0; i < i_Btn_list.Count; i++)
                    {
                        PLP_Event.PLP_infoBtn(Browser, '0');
                        if (i == 0) { Console.WriteLine("PeerSupported version Info button clicked and verified"); }
                        else { Console.WriteLine("SelfGuided version Info button clicked and verified"); }
                    }
                }
                //Condition to check from Step1 to Step5
                else if (Browser.Exists(Bys.MainproPage.StepTitle) && Browser.FindElement(Bys.MainproPage.StepTitle).Text.Contains("Step"))
                {
                    char CurrentStepNumber = Browser.FindElement(Bys.MainproPage.StepTitle).Text[5];
                    PLP_Event.PLP_infoBtn(Browser, CurrentStepNumber);
                  
                }
                //Condition for PR Page/Step6
                else if (Browser.Exists(Bys.MainproPage.StepTitle) && Browser.FindElement(Bys.MainproPage.StepTitle).Text.Contains("PLP Post Reflection"))
                {
                    PLP_Event.PLP_infoBtn(Browser, '6');
                   
                }
            }
            if (ToolsOptionsClickFromUserProfileCheck)
            {  
                //Condition for Select your pathway Page
                if (Browser.Exists(Bys.MainproPage.SelectYourPathway_PageTitle))
                {
                    PLP_Event.PLP_ToolsResourceOptionClickFromUserProfile(Browser, '0');
                }
                //Condition to check from Step1 to Step5
                else if (Browser.Exists(Bys.MainproPage.StepTitle) && Browser.FindElement(Bys.MainproPage.StepTitle).Text.Contains("Step"))
                {
                    char CurrentStepNumber = Browser.FindElement(Bys.MainproPage.StepTitle).Text[5];
                   PLP_Event.PLP_ToolsResourceOptionClickFromUserProfile(Browser, CurrentStepNumber);
                }
                //Condition for PR Page/Step6
                else if (Browser.Exists(Bys.MainproPage.StepTitle) && Browser.FindElement(Bys.MainproPage.StepTitle).Text.Contains("PLP Post Reflection"))
                {
                    PLP_Event.PLP_ToolsResourceOptionClickFromUserProfile(Browser, '6');
                }
            }
        }

        #endregion methods

    }
}
