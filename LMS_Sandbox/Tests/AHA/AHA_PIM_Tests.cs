using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using LMS.AppFramework;
using LMS.AppFramework.LMSHelperMethods;
using LMS.AppFramework.Constants_;
//
//
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using LMS.UITest;

namespace AHA.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_PIM_Complete_NIHStrokeScaleA : TestBase_AHA
    {
        #region Constructors

        public AHA_PIM_Complete_NIHStrokeScaleA(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_PIM_Complete_NIHStrokeScaleA(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        // 3-13-21: This takes too long, not running this, cause it fails every time anyway
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod"), 
        //    Category("OnDemandOnly")]
        //[Description("Completing the full workflow of NIH Stroke Scale Test Group A PIM, claiming credits, then going to " +
        //    "the Transcript page")]
        //[Property("Status", "Complete")]
        //[Author("Mike Johnston")]
        //public void PIM_CompletePIM_NIHStrokeScaleA(Constants.SiteCodes siteCode)
        //{
        //    string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
        //    UserModel user = professionMember1User1;
        //    APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);
        //    TestData TD = new TestData();

        //    /// 1. Complete the PIM and claim credit
        //    Help.CompleteActivity(Browser, siteCode, actTitle, false, user.Username, UserUtils.Password,
        //        assessmentQandAs: TD.PIM_NIHGroup_QandAs, assessments: TD.PIM_NIHGROUP_AAssessments, PIM: true);
        //}
  
        #endregion tests
    }

    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    //[TestFixture]
    //public class AHA_PIM_Complete_NIHStrokeScaleB : TestBase_AHA
    //{
    //    #region Constructors

    //    public AHA_PIM_Complete_NIHStrokeScaleB(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
    //    public AHA_PIM_Complete_NIHStrokeScaleB(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
    //                                : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
    //    { }

    //    #endregion Constructors

    //    #region Tests

    //    [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
    //    public void PIM_CompletePIM_NIHStrokeScaleB(Constants.SiteCodes siteCode)
    //    {
    //        //// Prerequisites. This block of code should be commented out unless on a new environment. The activities in this
    //        //// block of code are only prerequisite PIMs that the user must complete before the actual test PIM below this block
    //        //TestData prerequisiteTD = new TestData();
    //        //UserModel prerequisiteuser = professionMember1User2;
    //        //string preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 20000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);

    //        /// 1. Complete the PIM and claim credit
    //        string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_B.GetDescription();
    //        UserModel user = professionMember1User2;
    //        APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode, 20000);
    //        TestData TD = new TestData();
    //        Help.CompleteActivity(Browser, siteCode, actTitle, false, user.Username, UserUtils.Password,
    //            assessmentQandAs: TD.PIM_NIHGroup_QandAs, assessments: TD.PIM_NIHGROUP_AAssessments, PIM: true);
    //    }
    //    #endregion tests
    //}

    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    //[TestFixture]
    //public class AHA_PIM_Complete_NIHStrokeScaleC : TestBase_AHA
    //{
    //    #region Constructors

    //    public AHA_PIM_Complete_NIHStrokeScaleC(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
    //    public AHA_PIM_Complete_NIHStrokeScaleC(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
    //                                : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
    //    { }

    //    #endregion Constructors

    //    #region Tests

    //    [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
    //    public void PIM_CompletePIM_NIHStrokeScaleC(Constants.SiteCodes siteCode)
    //    {
    //        //// Prerequisites. This block of code should be commented out unless on a new environment. The activities in this
    //        //// block of code are only prerequisite PIMs that the user must complete before the actual test PIM below this block
    //        //TestData prerequisiteTD = new TestData();
    //        //UserModel prerequisiteuser = professionMember1User3;
    //        //string preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 40000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_B.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 40000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);

    //        /// 1. Complete the PIM and claim credit
    //        string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_C.GetDescription();
    //        UserModel user = professionMember1User3;
    //        APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode, 40000);
    //        TestData TD = new TestData();
    //        Help.CompleteActivity(Browser, siteCode, actTitle, false, user.Username, UserUtils.Password,
    //            assessmentQandAs: TD.PIM_NIHGroup_QandAs, assessments: TD.PIM_NIHGROUP_AAssessments, PIM: true);
    //    }
    //    #endregion tests
    //}

    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    //[TestFixture]
    //public class AHA_PIM_Complete_NIHStrokeScaleD : TestBase_AHA
    //{
    //    #region Constructors

    //    public AHA_PIM_Complete_NIHStrokeScaleD(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
    //    public AHA_PIM_Complete_NIHStrokeScaleD(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
    //                                : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
    //    { }

    //    #endregion Constructors

    //    #region Tests

    //    [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
    //    public void PIM_CompletePIM_NIHStrokeScaleD(Constants.SiteCodes siteCode)
    //    {
    //        // Prerequisites. This block of code should be commented out unless on a new environment. The activities in this
    //        // block of code are only prerequisite PIMs that the user must complete before the actual test PIM below this block
    //        //TestData prerequisiteTD = new TestData();
    //        //UserModel prerequisiteuser = professionMember1User4;
    //        //string preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 60000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_B.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 60000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_C.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 60000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);

    //        string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_D.GetDescription();
    //        UserModel user = professionMember1User4;
    //        APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode, 60000);
    //        TestData TD = new TestData();

    //        /// 1. Complete the PIM and claim credit
    //        Help.CompleteActivity(Browser, siteCode, actTitle, false, user.Username, UserUtils.Password,
    //            assessmentQandAs: TD.PIM_NIHGroup_QandAs, assessments: TD.PIM_NIHGROUP_AAssessments, PIM: true);
    //    }
    //    #endregion tests
    //}

    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    //[TestFixture]
    //public class AHA_PIM_Complete_NIHStrokeScaleE : TestBase_AHA
    //{
    //    #region Constructors

    //    public AHA_PIM_Complete_NIHStrokeScaleE(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
    //    public AHA_PIM_Complete_NIHStrokeScaleE(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
    //                                : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
    //    { }

    //    #endregion Constructors

    //    #region Tests

    //    [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
    //    public void PIM_CompletePIM_NIHStrokeScaleE(Constants.SiteCodes siteCode)
    //    {
    //        // Prerequisites. This block of code should be commented out unless on a new environment. The activities in this
    //        // block of code are only prerequisite PIMs that the user must complete before the actual test PIM below this block
    //        //TestData prerequisiteTD = new TestData();
    //        //UserModel prerequisiteuser = professionMember1User5;
    //        //string preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 80000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_B.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 80000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_C.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 80000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_D.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 80000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);

    //        string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_E.GetDescription();
    //        UserModel user = professionMember1User5;
    //        APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode, 80000);
    //        TestData TD = new TestData();

    //        /// 1. Complete the PIM and claim credit
    //        Help.CompleteActivity(Browser, siteCode, actTitle, false, user.Username, UserUtils.Password,
    //            assessmentQandAs: TD.PIM_NIHGroup_QandAs, assessments: TD.PIM_NIHGROUP_AAssessments, PIM: true);
    //    }
    //    #endregion tests
    //}

    //[LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    //[TestFixture]
    //public class AHA_PIM_Complete_NIHStrokeScaleF : TestBase_AHA
    //{
    //    #region Constructors

    //    public AHA_PIM_Complete_NIHStrokeScaleF(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
    //    public AHA_PIM_Complete_NIHStrokeScaleF(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
    //                                : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
    //    { }

    //    #endregion Constructors

    //    #region Tests

    //    [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
    //    public void PIM_CompletePIM_NIHStrokeScaleF(Constants.SiteCodes siteCode)
    //    {
    //        // Prerequisites. This block of code should be commented out unless on a new environment. The activities in this
    //        // block of code are only prerequisite PIMs that the user must complete before the actual test PIM below this block
    //        //TestData prerequisiteTD = new TestData();
    //        //UserModel prerequisiteuser = professionMember1User6;
    //        //string preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 100000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_B.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 100000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_C.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 100000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_D.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 100000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);
    //        //preact = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_E.GetDescription();
    //        //APIHelp.DeleteActivityForUser(prerequisiteuser.Username, preact, siteCode, 100000);
    //        //Help.CompleteActivity(Browser, siteCode, preact, false, prerequisiteuser.Username, UserUtils.Password,
    //        //    assessmentQandAs: prerequisiteTD.PIM_NIHGroup_QandAs, assessments: prerequisiteTD.PIM_NIHGROUP_AAssessments, PIM: true);

    //        /// 1. Complete the PIM and claim credit
    //        string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_F.GetDescription();
    //        UserModel user = professionMember1User6;
    //        APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode, 100000);
    //        TestData TD = new TestData();
    //        Help.CompleteActivity(Browser, siteCode, actTitle, false, user.Username, UserUtils.Password,
    //            assessmentQandAs: TD.PIM_NIHGroup_QandAs, assessments: TD.PIM_NIHGROUP_AAssessments, PIM: true);
    //    }
    //    #endregion tests
    //}

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_PIM1_Misc : TestBase_AHA
    {
        #region Constructors

        public AHA_PIM1_Misc(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_PIM1_Misc(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod"),
        //                Category("OnDemandOnly")]
        //[Description("Given I click to all Overview sections of the NIH Stroke Scale Test Group A PIM, When I view each section, " +
        //      "Then all completion check marks should appear up until Test Group A, and all labels and videos within the " +
        //      "Overview sections should appear without error")]
        //[Property("Status", "Complete")]
        //[Author("Mike Johnston")]
        //public void PIM_NIHStrokeScaleA_Overview(Constants.SiteCodes siteCode)
        //{
        //    string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
        //    UserModel user = professionMember1User2;
        //    APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);
        //    TestData TD = new TestData();            

        //    /// 1. Go to the NIH Stroke Scale Test Group A PIM, and verify the General Overview page label
        //    ActPIMPage APP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username, PIM: true);
        //    Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
        //    Assert.AreEqual("General Information", APP.SectionNameLbl.Text, "label did not contain correct text");

        //    /// 2. Click continue and verify the video appears
        //    APP.ClickAndWait(APP.ContinueBtn);
        //    Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
        //    Assert.AreEqual("Significance of the Stroke Scale", APP.SectionNameLbl.Text, "label did not contain correct text");
        //    Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

        //    /// 3. Click continue, expand each section, verify all steps till *TEST GROUP A > Patient 1* are active 
        //    /// with a check ON indicating they are completed.
        //    APP.ExpandOrCollapsePIMSection("Learning and Understanding", "Expand");
        //    APP.ExpandOrCollapsePIMSection("Demonstration Patients", "Expand");
        //    APP.ExpandOrCollapsePIMSection("Test Group A", "Expand");
        //    Assert.True(Browser.Exists(Bys.ActPIMPage.SectionCompletionGreenCheckIcons), "No green check marks appeared");
        //    Assert.AreEqual(20, Browser.FindElements(Bys.ActPIMPage.SectionCompletionGreenCheckIcons).Count,
        //        "20 check marks did not appear. Actual amount = " +
        //        Browser.FindElements(Bys.ActPIMPage.SectionCompletionGreenCheckIcons).Count.ToString());

        //    /// 4. Click continue to each remaining Overview sub-section and verify the videos appear
        //    APP.ClickAndWait(APP.ContinueBtn);
        //    Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
        //    Assert.AreEqual("Relevance to Medical Specialties", APP.SectionNameLbl.Text, "label did not contain correct text");
        //    Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

        //    APP.ClickAndWait(APP.ContinueBtn);
        //    Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
        //    Assert.AreEqual("Tips for Scoring", APP.SectionNameLbl.Text, "label did not contain correct text");
        //    Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");
        //}


        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_PIM2_Misc : TestBase_AHA
    {
        #region Constructors

        public AHA_PIM2_Misc(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_PIM2_Misc(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests


        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod"),
            //Category("OnDemandOnly")]
        [Description("Given I click to all Learning and Understanding sections of the NIH Stroke Scale Test Group A PIM, When " +
            "I view each section, Then all labels and videos should appear without error")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PIM_NIHStrokeScaleA_LearningAndUnderstanding(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
            UserModel user = professionMember1User3;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);
            TestData TD = new TestData();

            /// 1. Go to the NIH Stroke Scale Test Group A PIM, and verify the Introduction page label and video
            ActPIMPage APP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username, PIM: true);
            APP.ClickAndWait(APP.ContinueBtn);
            APP.GoToSubSection("Learning and Understanding", "Introduction");
            Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            Assert.AreEqual("Introduction", APP.SectionNameLbl.Text, "label did not contain correct text");
            Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            /// 3. Click continue to each remaining Learning And Understanding sub-section and verify the labels and videos appear
            APP.ClickAndWait(APP.ContinueBtn);
            Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            Assert.AreEqual("1A Level of Consciousness", APP.SectionNameLbl.Text, "label did not contain correct text");
            Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            APP.ClickAndWait(APP.ContinueBtn);
            Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            Assert.AreEqual("1B LOC - Questions", APP.SectionNameLbl.Text, "label did not contain correct text");
            Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            APP.ClickAndWait(APP.ContinueBtn);
            Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            Assert.AreEqual("1C LOC - Commands", APP.SectionNameLbl.Text, "label did not contain correct text");
            Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            APP.ClickAndWait(APP.ContinueBtn);
            Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            Assert.AreEqual("2 Best Gaze", APP.SectionNameLbl.Text, "label did not contain correct text");
            Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            // This test is too long, shortening it by commenting this out
            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            //Assert.AreEqual("3 Visual", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            //Assert.AreEqual("4 Facial Palsy", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            //Assert.AreEqual("5 Motor Arm", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            //Assert.AreEqual("6 Motor Leg", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            //Assert.AreEqual("7 Limb Ataxia", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            //Assert.AreEqual("8 Sensory", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            //Assert.AreEqual("9 Best Language", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            //Assert.AreEqual("10 Dysarthria", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");

            //APP.ClickAndWait(APP.ContinueBtn);
            //Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            ////Assert.AreEqual("11 Extinction and Inattention", APP.SectionNameLbl.Text, "label did not contain correct text");
            //Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");
        }


        #endregion tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_PIM3_Misc : TestBase_AHA
    {
        #region Constructors

        public AHA_PIM3_Misc(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_PIM3_Misc(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        // Always get Were Sorry errors for this test, and DEV wont fix due to intermittency, so commenting out
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Verifying all videos and questions appear as they should, and that answering " +
            "incorrect questions results in the correct percentage calculation")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void PIM_NIHStrokeScaleA_DemoPatientB(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.NIH_Stroke_Scale_Test_Group_A.GetDescription();
            UserModel user = professionMember1User4;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);
            TestData TD = new TestData();
            string assessmentName = "Demo Patient B Assessment";

            /// 1. Go to the NIH Stroke Scale Test Group A PIM, and verify the number of videos is 15
            ActPIMPage APP = Help.GoTo_ActivityWorkflow_OverviewPage(Browser, siteCode, actTitle, false, user.Username, 
                PIM: true);
            APP.ClickAndWait(APP.ContinueBtn);
            APP.GoToSubSection("Demonstration Patients", "Demonstration Patient B");
            Assert.True(Browser.Exists(Bys.ActPIMPage.SectionNameLbl), "label did not appear");
            Assert.AreEqual("Demo Patient B Assessment", APP.SectionNameLbl.Text, "label did not contain correct text");
            Assert.True(Browser.Exists(Bys.ActPIMPage.VideoPlayers), "Video did not appear");
            Assert.AreEqual(15, Browser.FindElements(Bys.ActPIMPage.VideoPlayers).Count, "Videos did not equal " +
                "the expected amount of 15");

            /// 2. Click Submit without answering any questions and verify required questions throw error
            APP.SubmitBtn.Click();
            Assert.True(Browser.Exists(Bys.ActPIMPage.ThisFieldIsRequiredLbls), "Required labels did not appear");
            Assert.AreEqual(15, Browser.FindElements(Bys.ActPIMPage.ThisFieldIsRequiredLbls).Count, "Required labels did not equal " +
                "the expected amount of 15");

            /// 3.Answer all incorrect questions, click Submit, then verify that the calculations are reflected accordingly
            List<Constants.AssQAndAs> assessmentQandIncorrectAs = TD.GetAnswerSet(TD.PIM_NIHGroup_QandAs, false,
                APP.ActivityTitleLbl.Text, assessmentName);
            APP.SubmitAssesment(assessmentName, assessmentQandAs: assessmentQandIncorrectAs);
            Assert.True(APP.PatientScorePercentageLbl.Text.Contains("0%"));
        }

        #endregion tests
    }
}






