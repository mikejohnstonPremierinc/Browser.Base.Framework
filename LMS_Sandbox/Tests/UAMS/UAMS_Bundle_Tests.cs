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

namespace UAMS.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    /// <summary>
    /// Running only 1 test in all browsers. We will have many bundle page tests, that all hit the same page/tab. So we dont need to run 
    /// all bundle page tests in all browsers. 
    /// </summary>
    [TestFixture]
    public class UAMS_Bundle_Tests_AllBrowsers : TestBase_UAMS
    {
        #region Constructors

        public UAMS_Bundle_Tests_AllBrowsers(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public UAMS_Bundle_Tests_AllBrowsers(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1416
        //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Asserting various different requirements for an bundled activity with 2 children (1 required, 1 not required), having " +
            "Access Code and Payment requirements")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void Bundle_ChildReqAndNotReq_AcsCodeAndFee(Constants.SiteCodes siteCode)
        {
            // Not executing in Prod until after next SP because the application code in Prod does not work for payments. It throws
            // an error
            Help.IgnoreTestOnEnvironmentUntilDate(DateTime.ParseExact("06/24/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture),
                Constants.Environments.Production);

            string parentActTitle = Constants.ActTitle.Automation_Bundle_Access_Code_And_Payment_Required_One_Child_Required.GetDescription();
            string accessCodeActTitle = Constants.ActTitle.Automation_Bundle_Child_Access_Code_And_Payment_Required_One_Child_Required_Access.GetDescription();
            string paymentActTitle = Constants.ActTitle.Automation_Bundle_Child_Access_Code_And_Payment_Required_One_Child_Required_Payment.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, parentActTitle, siteCode);
            APIHelp.DeleteActivityForUser(user.Username, accessCodeActTitle, siteCode);
            APIHelp.DeleteActivityForUser(user.Username, paymentActTitle, siteCode);

            /// 1. Go to the Activity Preview page for a parent activity that has at least 1 child activity required, and where 1 child 
            /// activity requires an access code, and the other requires payment
            ActPreviewPage APP = Help.GoTo_ActivityNonWorkflow_PreviewPageViaURL(Browser, siteCode, parentActTitle, false, user.Username);

            /// 2. Click on the Preview page, Include These Activities tab and assert that both activities show, and that 1 of them is set to 
            /// required
            // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-957
            APP.ClickAndWait(APP.InclTheseActsTab);
            Assert.AreEqual(2, Browser.FindElements(Bys.ActPreviewPage.IncTheseActsTab_BundlesTblBodyActivityLnks).Count, "The table did " +
                "not return the expected count of 2 child activities for this bundle activity");
            Assert.AreEqual(1, Browser.FindElements(Bys.ActPreviewPage.IncTheseActsTab_RequiredLbls).Count, "The UI did not display the " +
                "expected Required label for 1 of the child activities");

            /// 3. Register to the parent activity, go to the Include These Activities tab on the activity view, then assert that both of 
            /// the child activities display the Not Started label
            ActBundlePage ABP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle);
            Assert.AreEqual(2,
                Browser.FindElements(Bys.ActBundlePage.ActivityTbl_NotStartedLbls).Count, "The Include These Activities page did " +
                "not display the expected count of 2 Not Started labels");

            /// 4. Go to the Activities In Progress page and Assert that the parent activity and its associated child activities show in the table
            ActivitiesInProgressPage AIPP = ABP.ClickAndWaitBasePage(ABP.ActivitiesInProgressTab);
            Assert.True(ElemGet.Grid_ContainsRecord(
                Browser, AIPP.ActivitiesTbl, Bys.ActivitiesInProgressPage.ActivitiesTblBody, 1, parentActTitle, "*"));
            // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1257
            Assert.True(ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.ActivitiesInProgressPage.ActivitiesTblBody, 1, accessCodeActTitle, "*"));
            Assert.True(ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.ActivitiesInProgressPage.ActivitiesTblBody, 1, paymentActTitle, "*"));

            /// 5. Go back to the Include These Activities tab on the activity view for the parent activity and start the activity that requires 
            /// an access code. Assert that the access code form appears
            Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle);
            ABP.ClickActivity(accessCodeActTitle);
            APP.SubmitAccessCode(DBUtils.GetAccessCode(accessCodeActTitle));

            /// 6. Go to the PreTest for the Access Code activity, then Go back to the Include These Activities tab on the activity view for the 
            /// parent activity. Assert that the status reads In Progress for the access code activity
            ActAssessmentPage AP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, accessCodeActTitle, Constants.Pages_ActivityPage.Assessment);
            Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle);
            Assert.Greater(Browser.FindElements(Bys.ActBundlePage.ActivityTbl_InProgressLbls).Count, 0, "The In Progress label did not appear");
            Assert.AreEqual(1,
                Browser.FindElements(Bys.ActBundlePage.ActivityTbl_InProgressLbls).Count, "The count of In Progress labels was more " +
                "than 1");

            /// 7. Click on the activity that requires payment, assert that the Payment page appeared. 
            ABP.ClickActivity(paymentActTitle);
            ActOrderDetailsPage AODP = APP.ClickAndWait(APP.LaunchOrRegisterOrResumeBtn);

            if (Help.EnvironmentEquals(Constants.Environments.Production))
            {
                if (Help.EnvironmentEquals(Constants.Environments.Production))
                {
                    Assert.Pass("Payment is expected to fail on Production. Per Arun: 'for the payment to work " +
                        "on production, the client has to cofigure theirpayment gateway account details'. Until then, we will " +
                        "continue to fail this test because it requires that payments are configured and working. Every SP, check " +
                        "with DEV to see if client has conigured details yet. If they have, then remove this If statement and run the test");

                    //throw new Exception("Payment is expected to fail here on Prod. Per Arun, 'Client has to cofigure their " +
                    //"payment gateway account details' for anything related to payment to work. Until then, we will " +
                    //"continue to fail this test. Every SP, check with DEV to see if client has conigured details. If they " +
                    //"have, then remove this If statement");
                }

                ActOrderReceiptPage ARP = AODP.SubmitDiscountCode("full");
                ARP.ClickAndWait(ARP.ExitBtn);

                /// 8. Complete the activity that requires payment, go back to the Include These Activities tab on the activity view for the 
                /// parent activity. Assert that it shows: Access Code activity = In Progress, Fee Discounted = Complete, 
                /// Continue button = Inactive
                TranscriptPage TP = Help.CompleteActivity(Browser, siteCode, paymentActTitle);
                Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle);
                Assert.Greater(Browser.FindElements(Bys.ActBundlePage.ActivityTbl_CompleteLbls).Count, 0, "The Complete label did not appear");
                Assert.AreEqual(1,
                    Browser.FindElements(Bys.ActBundlePage.ActivityTbl_CompleteLbls).Count, "The count of Complete labels was more " +
                    "than 1");
                Assert.Greater(Browser.FindElements(Bys.ActBundlePage.ActivityTbl_InProgressLbls).Count, 0, "The In Progress label did not appear");
                Assert.AreEqual(1,
                    Browser.FindElements(Bys.ActBundlePage.ActivityTbl_InProgressLbls).Count, "The count of In Progress labels was more " +
                    "than 1");
                Assert.True(Browser.Exists(Bys.ActBundlePage.ContinueBtn, ElementCriteria.AttributeValue("class", "simple-button disabled")));
                Assert.True(Browser.Exists(Bys.ActBundlePage.ContinueBtn, ElementCriteria.AttributeValueContains("class", "disabled")));

                /// 9. Complete the Access Code activity. Go back to the Include These Activities tab on the activity view for the parent activity.
                /// Assert that it shows: Access Code activity = Complete, Fee Discounted = Complete, Continue button = Active
                Help.CompleteActivity(Browser, siteCode, accessCodeActTitle);
                Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle);
                Assert.Greater(Browser.FindElements(Bys.ActBundlePage.ActivityTbl_CompleteLbls).Count, 0, "The Complete labels did not appear");
                Assert.AreEqual(2,
                    Browser.FindElements(Bys.ActBundlePage.ActivityTbl_CompleteLbls).Count, "The count of Complete labels was not 2 ");
                Assert.False(Browser.Exists(Bys.ActBundlePage.ContinueBtn, ElementCriteria.AttributeValueContains("class", "disabled")));

                /// 10. Complete the parent activity, then go to the Transcript page and assert that all 3 activities show on this page
                Help.CompleteActivity(Browser, siteCode, parentActTitle);
                Assert.True(ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.TranscriptPage.ActivitiesTbl, 1, parentActTitle, "*"));
                Assert.True(
                    ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.TranscriptPage.ActivitiesTblBody, 1, accessCodeActTitle, "*"));
                Assert.True(
                    ElemGet.Grid_ContainsRecord(Browser, AIPP.ActivitiesTbl, Bys.TranscriptPage.ActivitiesTblBody, 1, paymentActTitle, "*"));
            }

            #endregion Tests
        }

        // Local
        [LocalSeleniumTestFixture(BrowserNames.Chrome)]
        // Remote
        [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]

        /// <summary>
        /// Running only 1 test in all browsers. We will have many bundle page tests, that all hit the same page/tab. So we dont need to run 
        /// all bundle page tests in all browsers. 
        /// </summary>
        [TestFixture]
        public class UAMS_Bundle_Tests_Chrome : TestBase_UAMS
        {
            #region Constructors

            public UAMS_Bundle_Tests_Chrome(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
            public UAMS_Bundle_Tests_Chrome(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
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
                ActBundlePage ABP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle, false,
                    user.Username);

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

            // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1416
            // Fixed defect https://code.premierinc.com/issues/browse/LMSPLT-5457. Uncomment and execute when fixed
            //[TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
            [Description("Given I register to a bundle parent activity whose 'Bundle Price Includes All Activities' option is checked, When I register " +
                "to a child activity that requires a payment, Then the user should not have to pay")]
            [Property("Status", "Complete")]
            [Author("Mike Johnston")]
            public void Bundle_BundlePriceIncludesAll(Constants.SiteCodes siteCode)
            {
                string parentActTitle = Constants.ActTitle.Automation_Bundle_Price_Includes_All.GetDescription();
                string childActTitle = Constants.ActTitle.Automation_Bundle_Child_Price_Includes_All.GetDescription();
                UserModel user = profession1User1;
                APIHelp.DeleteActivityForUser(user.Username, parentActTitle, siteCode);
                APIHelp.DeleteActivityForUser(user.Username, childActTitle, siteCode);

                /// 1. Go to the parent bundle activity's Include these Activities page
                ActBundlePage ABP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, parentActTitle, Constants.Pages_ActivityPage.Bundle,
                    false, user.Username);

                /// 2. Start the activity that requires a fee, assert that the user is taken to the Activity Overview page without prompting payment
                ActPreviewPage APP = ABP.ClickActivity(childActTitle);
                APP.ClickAndWait(APP.LaunchOrRegisterOrResumeBtn);
                // Fixed Defect https://code.premierinc.com/issues/browse/LMSREW-1257. 
                Assert.True(Browser.Exists(Bys.ActOverviewPage.ActivityOverviewChk, ElementCriteria.IsVisible),
                    "The Activity Overview page did not appear");
            }

            #endregion tests
        }
    }
}







