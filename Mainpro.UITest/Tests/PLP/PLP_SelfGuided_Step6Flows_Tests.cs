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
    public class PLP_SelfGuided_Step6YesFlows_Tests_1 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step6YesFlows_Tests_1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step6YesFlows_Tests_1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Yes_Yes_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Yes_Yes_Yes_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please elaborate and describe in detail.
            PS6.FillElborateDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("Yes");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Yes_Yes_No flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Yes_Yes_No_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please elaborate and describe in detail.
            PS6.FillElborateDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("No");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step6YesFlows_Tests_2 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step6YesFlows_Tests_2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step6YesFlows_Tests_2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Yes_Partial_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Yes_Partial_Yes_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you improve on your outcomes.
            PS6.FillImproveOutcomeDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("Yes");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Yes_Partial_No flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Yes_Partial_No_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you improve on your outcomes.
            PS6.FillImproveOutcomeDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("No");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }
        #endregion 
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]

    public class PLP_SelfGuided_Step6YesFlows_Tests_3 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step6YesFlows_Tests_3(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step6YesFlows_Tests_3(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Yes_No_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Yes_No_Yes_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you improve on your outcomes.
            PS6.FillImproveOutcomeDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("Yes");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Yes_No_No flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Yes_No_No_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you improve on your outcomes.
            PS6.FillImproveOutcomeDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("No");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }
        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step6PartialFlows_Tests_1 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step6PartialFlows_Tests_1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step6PartialFlows_Tests_1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Partial_Yes_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Partial_Yes_Yes_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you follow through with your learning goal(s).
            PS6.FillDescribeWhatHelpedFollowGoals(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please elaborate and describe in detail.
            PS6.FillElborateDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("Yes");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Partial_Yes_No flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Partial_Yes_No_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you follow through with your learning goal(s).
            PS6.FillDescribeWhatHelpedFollowGoals(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please elaborate and describe in detail.
            PS6.FillElborateDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("No");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }
        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step6PartialFlows_Tests_2 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step6PartialFlows_Tests_2(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step6PartialFlows_Tests_2(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Partial_Partial_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Partial_Partial_Yes_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you follow through with your learning goal(s).
            PS6.FillDescribeWhatHelpedFollowGoals(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you improve on your outcomes.
            PS6.FillImproveOutcomeDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("Yes");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Partial_Partial_No flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Partial_Partial_No_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you follow through with your learning goal(s).
            PS6.FillDescribeWhatHelpedFollowGoals(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you improve on your outcomes.
            PS6.FillImproveOutcomeDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("No");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step6PartialFlows_Tests_3 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step6PartialFlows_Tests_3(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step6PartialFlows_Tests_3(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Partial_No_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Partial_No_Yes_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you follow through with your learning goal(s).
            PS6.FillDescribeWhatHelpedFollowGoals(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you improve on your outcomes.
            PS6.FillImproveOutcomeDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("Yes");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with Partial_No_No flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_Partial_No_No_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("Partially");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you follow through with your learning goal(s).
            PS6.FillDescribeWhatHelpedFollowGoals(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Did you see any of the outcomes you'd hoped to see? (e.g., improvements in your data).
            PS6.Selectif_ImprovementsInData("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you improve on your outcomes.
            PS6.FillImproveOutcomeDetail(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("No");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }
        #endregion
    }
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    public class PLP_SelfGuided_Step6NoFlows_Tests_1 : TestBase
    {
        #region Constructors
        public PLP_SelfGuided_Step6NoFlows_Tests_1(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public PLP_SelfGuided_Step6NoFlows_Tests_1(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion
        #region Tests
        [Test]
        [Description("SelfGuidedTest - completes till step6 end with No_Yes flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_No_Yes_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you follow through with your learning goal(s).
            PS6.FillDescribeWhatHelpedFollowGoals(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("Yes");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("Yes");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        [Test]
        [Description("SelfGuidedTest - completes till step6 end with No_No flow")]
        [Property("Status", "InProgress")]
        [Author("Ganapathy Raja")]
        public void step6_No_No_Flow()
        {
            StepPRPage PS6 = Help.PLP_GoToStep(Browser, 6, TestContext.CurrentContext.Test,isSelfGuided:true);

            //Verifies the user is on Post reflection page and clicks next
            Assert.True(Browser.Url.Contains("postreflection"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.NextBtn);

            //Verifies the user is on Impact on practice screen and clicks next
            PS6.FillImpactOnPractice(Browser);
            PS6.ClickAndWait(PS6.NextBtn);
            //Did your PLP help you achieve your learning goal(s)?
            PS6.Selectif_PLPHelpedAchievedGoal("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Please describe what would have helped you follow through with your learning goal(s).
            PS6.FillDescribeWhatHelpedFollowGoals(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Based on your outcomes, list up to three changes/modifications you might plan to make in a future PLP.
            PS6.FillOutcomeModifications(Browser);
            PS6.ClickAndWait(PS6.NextBtn);

            //Would you recommend this learning activity to a colleague?
            PS6.Selectif_RecommendToCollegue("No");
            PS6.ClickAndWait(PS6.NextBtn);


            //Do you have any additional comments about the PLP that may help us to improve it for future users?
            PS6.Selectif_AnyAdditionalComments("No");
            PS6.ClickAndWait(PS6.NextBtn);

            //Congratulations! You have completed your PLP!
            Assert.True(Browser.Url.Contains("step6submission"), "Step 5> Pre Reflection > screen not loaded");
            PS6.ClickAndWait(PS6.SubmitBtn);

            //overallplpcompletion
            PS6.ClickAndWait(PS6.ExitPLPBtn);
        }

        #endregion
    }
}*/



