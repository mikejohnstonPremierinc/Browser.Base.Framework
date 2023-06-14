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

// Uncomment this code if you find any defects in any of the individual flows and execute that 
// test case alone seperately
/*namespace PLP.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step5YesFlows_Tests_1 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step5YesFlows_Tests_1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step5YesFlows_Tests_1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Yes_Yes_ flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Yes_Yes_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Yes in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "identify the most useful CPD program(s)" Screen and Clicks next
            Assert.True(Browser.Url.Contains("usefulcpdprograms"), "Step 5> Identify useful CPD programs> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Yes_Partial_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Yes_Partial_Yes_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Yes in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Yes");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomYesCommentTxt, true, "Yes comment");
            PS5.ClickAndWait(PS5.NextBtn);

            //Verifies the user is on "identify the most useful CPD program(s)" Screen and Clicks next
            Assert.True(Browser.Url.Contains("usefulcpdprograms"), "Step 5> Identify useful CPD programs> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);

           PS5.ClickAndWait(PS5.NextBtn);
        }
        #endregion 
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step5YesFlows_Tests_2 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step5YesFlows_Tests_2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step5YesFlows_Tests_2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Yes_Partial_Partial flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Yes_Partial_Partial_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Partially");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomPartialCommentTxt, true, "Partial comment");
            //Help.PLP_AddFormattedText(Browser, "[max 100 characters]", "Testing Comments");
            PS5.ClickAndWait(PS5.NextBtn);

            //Verifies the user is on "identify the most useful CPD program(s)" Screen and Clicks next
            Assert.True(Browser.Url.Contains("usefulcpdprograms"), "Step 5> Identify useful CPD programs> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
                        
            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Yes_Partial_No flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Yes_Partial_No_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("No");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomNoCommentTxt, true, "No comment");
            PS5.ClickAndWait(PS5.NextBtn);

            
            PS5.ClickAndWait(PS5.NextBtn);
        }
        #endregion 
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]

    public class PLP_SelfGuided_Step5YesFlows_Tests_3 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step5YesFlows_Tests_3(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step5YesFlows_Tests_3(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Yes_No_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Yes_No_Yes_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("No");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Yes in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Yes");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomYesCommentTxt, true, "Yes comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Yes_No_Partial flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Yes_No_Partial_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("No");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Partially");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomPartialCommentTxt, true, "Partial comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Yes_No_No flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Yes_No_No_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("No");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "No");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomNoCommentTxt, true, "No comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }
        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step5PartialFlows_Tests_1 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step5PartialFlows_Tests_1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step5PartialFlows_Tests_1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Partial_Yes_ flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Partial_Yes_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Yes in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("Yes");
            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "identify the most useful CPD program(s)" Screen and Clicks next
            Assert.True(Browser.Url.Contains("usefulcpdprograms"), "Step 5> Identify useful CPD programs> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Partial_Partial_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Partial_Partial_Yes_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Yes in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Yes");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomYesCommentTxt, true, "Yes comment");
            PS5.ClickAndWait(PS5.NextBtn);

            //Verifies the user is on "identify the most useful CPD program(s)" Screen and Clicks next
            Assert.True(Browser.Url.Contains("usefulcpdprograms"), "Step 5> Identify useful CPD programs> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step5PartialFlows_Tests_2 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step5PartialFlows_Tests_2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step5PartialFlows_Tests_2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Partial_Partial_Partial flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Partial_Partial_Partial_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Partially");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomPartialCommentTxt, true, "Partial comment");
            //Help.PLP_AddFormattedText(Browser, "[max 100 characters]", "Testing Comments");
            PS5.ClickAndWait(PS5.NextBtn);

            //Verifies the user is on "identify the most useful CPD program(s)" Screen and Clicks next
            Assert.True(Browser.Url.Contains("usefulcpdprograms"), "Step 5> Identify useful CPD programs> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Partial_Partial_No flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Partial_Partial_No_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("No");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomNoCommentTxt, true, "No comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step5PartialFlows_Tests_3 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step5PartialFlows_Tests_3(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step5PartialFlows_Tests_3(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Partial_No_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Partial_No_Yes_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("No");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Yes in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Yes");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomYesCommentTxt, true, "Yes comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Partial_No_Partial flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Partial_No_Partial_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("No");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks Partially in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Partially");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomPartialCommentTxt, true, "Partial comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with Partial_No_No flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_Partial_No_No_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks partially in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("Partially");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "share any barrier" screen and moves to next screen
            PS5.FillShareAnyBarrier();
            PS5.ClickAndWait(PS5.NextBtn);
            //Fills out "How did you determine success in achieving your goal(s)?" and moves to next screen
            PS5.FillDetermineSuccess();
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did the CPD activities suggested in the PLP activity help you meet your CPD goals?" and moves to next screen
            PS5.SelectifCPD_SuggestedActivitiesHelpedGoals("No");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "No");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomNoCommentTxt, true, "No comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }
        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step5NoFlows_Tests_1 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step5NoFlows_Tests_1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step5NoFlows_Tests_1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step5 end with No_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_No_Yes_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("No");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "Please describe any barriers you encountered that prevented you from achieving your goal(s)." screen and moves to next screen
            PS5.FillBarriersToAchievingGoals();
            PS5.ClickAndWait(PS5.NextBtn);

            //Clicks Yes in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Yes");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomYesCommentTxt, true, "Yes comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step5 end with No_Partial flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_No_Partial_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("No");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "Please describe any barriers you encountered that prevented you from achieving your goal(s)." screen and moves to next screen
            Help.PLP_AddFormattedText(Browser,"Testing Barriers I encountered",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritemax1000Txt);
            PS5.ClickAndWait(PS5.NextBtn);

            //Clicks Partially in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("Partially");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomPartialCommentTxt, true, "Partial comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }
        [Test]
        [Description("SelfGuidedTest - completes till step5 end with No_No flow")]
        [Property("Status", "InProgress")]
        [Author("Bama Thangaraj")]
        public void step5_No_No_Flow()
        {
            //Launch to Step5 first screen
            Step5Page PS5 = Help.PLP_GoToStep(Browser, 5, TestContext.CurrentContext.Test);
            //Verifies the User is in first screen prereflection screen
            Assert.True(Browser.Url.Contains("prereflection"), "Step 5> Pre Reflection screen> screen not loaded");
            Help.PLP_ClickCheckBoxOrRadioButton(Browser, "Move to Step 5");

            PS5.ClickAndWait(PS5.NextBtn);
            //Verifies the user is on "PLP Goal reflection page" and clicks next
            Assert.True(Browser.Url.Contains("goalreflection"), "Step 5> PLP Goal Reflection> screen not loaded");
            PS5.ClickAndWait(PS5.NextBtn);
            //Clicks No in "Did you achieve your learning goal(s)?" page and moves to next page
            PS5.SelectIfGoalAchieved("No");
            PS5.ClickAndWait(PS5.NextBtn);
            // Fills out "Please describe any barriers you encountered that prevented you from achieving your goal(s)." screen and moves to next screen
            Help.PLP_AddFormattedText(Browser,  "Testing Barriers I encountered",
                Const_Mainpro.PLP_TextboxlabelText.Pleasewritemax1000Txt);
            PS5.ClickAndWait(PS5.NextBtn);

            //Clicks No in "Did you find the CPD activity recommendations feature helpful in creating your plan?" , enters comments and moves to next screen
            PS5.SelectifCPD_ActivityRecommendationsHelped("No");
            ElemSet.TextBox_EnterText(browser, PS5.CPDActRecomNoCommentTxt, true, "No comment");
            PS5.ClickAndWait(PS5.NextBtn);

            PS5.ClickAndWait(PS5.NextBtn);
        }

        #endregion
    }
}*/



