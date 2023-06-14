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

namespace CMECA.UITest
{
    // Local
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // Remote
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

    /// <summary>
    /// Running only 1 test in all browsers. We will have many bundle page tests, that all hit the same page/tab. So we dont need to run 
    /// all bundle page tests in all browsers. 
    /// </summary>
    [TestFixture]
    public class CMECA_Bundle_Tests_Chrome : TestBase_CMECA
    {
        #region Constructors

        public CMECA_Bundle_Tests_Chrome(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public CMECA_Bundle_Tests_Chrome(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region  Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I register to a bundled parent activity which has the Hide From Curriculum option set, When I go to the Curriculum " +
            "page, Then I should not see the parent activity in the list, but I should see the child activities")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Bundle_HideFromCurriculum(Constants.SiteCodes siteCode)
        {
            string parentActTitle = Constants.ActTitle.Automation_Bundle_Hide_From_Activities_In_Progress.GetDescription();
            string child1ActTitle = Constants.ActTitle.Automation_Bundle_Child_Hide_From_Activities_In_Progress.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, parentActTitle, siteCode);
            APIHelp.DeleteActivityForUser(user.Username, child1ActTitle, siteCode);

            /// 1. Login, register to a bundled parent activity which has the Hide From Curriculum option checked in CME360
            ActBundlePage ABP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle, false, user.Username);

            /// 2. Go to the Activities In Progress page and assert that the child activities do show, but the parent does not
            ActivitiesInProgressPage AIPP = ABP.ClickAndWaitBasePage(ABP.ActivitiesInProgressTab);
            Assert.False(
                ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.ActivitiesInProgressPage.ActivitiesTblBody, 1, parentActTitle, "*"));
            // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1257
            Assert.True(ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.ActivitiesInProgressPage.ActivitiesTblBody, 1, child1ActTitle, "*"));
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I register to a bundled parent activity which has the Hide From Transcript option set, When I go to the Curriculum " +
            "page, Then I should see parent and child activities in the list, and When I complete all activities, Then I should not see the " +
            "parent activities on the Transcript page, but I should see the child activities")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Bundle_HideFromTrancscript(Constants.SiteCodes siteCode)
        {
            string parentActTitle = Constants.ActTitle.Automation_Bundle_Hide_From_Transcript.GetDescription();
            string child1ActTitle = Constants.ActTitle.Automation_Bundle_Child_Hide_From_Trascript.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, parentActTitle, siteCode);
            APIHelp.DeleteActivityForUser(user.Username, child1ActTitle, siteCode);

            /// 1. Login, register to a bundled parent activity which has the Hide From Transcript option checked in CME360
            ActBundlePage ABP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle, false, user.Username);

            /// 2. Go to the Activities In Progress page and assert that the child and parent activities show in the list
            ActivitiesInProgressPage AIPP = ABP.ClickAndWaitBasePage(ABP.ActivitiesInProgressTab);
            Assert.True(
                ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.ActivitiesInProgressPage.ActivitiesTblBody, 1, parentActTitle, "*"));
            // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1257
            Assert.True(ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.ActivitiesInProgressPage.ActivitiesTblBody, 1, child1ActTitle, "*"));

            /// 3. Complete the parent activity, go to the Transcript page, assert that the parent does not show in the list
            TranscriptPage TP = Help.CompleteActivity(Browser, siteCode, parentActTitle);
            Assert.False(ElemGet.Grid_ContainsRecord(Browser, TP.ActivitiesTbl, Bys.TranscriptPage.ActivitiesTblBody, 1, parentActTitle, "*"));
        }

        #endregion tests
    }
}






