using Browser.Core.Framework;
using LMS.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using LMSAdmin.AppFramework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OpenQA.Selenium.Firefox;
using Browser.Core.Framework.Resources;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.IO;
using LMSAdmin.AppFramework.HelperMethods;
using System.Globalization;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;

namespace LMSAdmin.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    //[LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    //[RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]
    [TestFixture]
   // [Ignore("Ignore a fixture; this is old cme360;keeping for reference , soon it will be deleted")]
    public class GeneralTests : TestBase
    {
        #region Constructors
        public GeneralTests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public GeneralTests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version , platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties

        //TPHelperMethods TestPortalHelp = new TPHelperMethods();

        #endregion properties

        #region Tests

       // [Test]
        [Description("Tests the Available Catalogs table search function, tests that a user can add and remove catalogs to an activity, and tests that" +
            " the portal associated to a catalog appears/disappears in the Portal table depending on if you add or remove that catalog")]
        [Property("Prerequisites", "See manual steps and IDs 1, 2 and 3 here: https://code.premierinc.com/docs/x/BYnbAw")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void SearchAddRemoveCatalogAndPortalDependency()
        {
            string activityName = "";
            if (BrowserName == BrowserNames.Chrome)
            { activityName = "TestAuto Activity 1 Chrome"; }
            if (BrowserName == BrowserNames.InternetExplorer)
            { activityName = "TestAuto Activity 1 IE"; }
            if (BrowserName == BrowserNames.Firefox)
            { activityName = "TestAuto Activity 1 FF"; }

            string catalogName = "TestAuto Catalog 1";
            string portalName = "_Test Portal";

            /// 1. Login as TestAuto_TestPortal_User1
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage MDP = LP.Login(UserUtils.CMEUser_TestPortal_UserName1, "password");

            /// 2. Open the "TestAuto Activity 1" activity
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);

            /// 3. Go to the Publishing Details and do a search for "TestAuto Catalog 1" and verify that it shows in the available table
            // First, we should check to make sure that this catalog is not in the Selected table (if it is, then the test failed at some point, and didnt reach 
            // the part of the test where it moves the catalog back to the available list). So if its not in the available list, then we need to put it back
            AMP.ClickAndWait(AMP.PubDetailsTab);
            if (ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabSelCatTbl, Bys.ActMainPage.PubDetailsTabSelCatTblBodyRow, 1,
                catalogName, "td"))
            {
                AMP.RemoveCatalogFromActivity(catalogName);
            }
            AMP.SearchForAvailableCatalog(catalogName);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabAvailCatTbl, Bys.ActMainPage.PubDetailsTabAvailCatTblBodyRow, 1,
                "TestAuto Catalog 1", "td", Bys.ActMainPage.PubDetailsTabAvailCatTblFirstBtn, Bys.ActMainPage.PubDetailsTabAvailCatTblNextBtn),
                "Publishing Details tab, Available Catalogs table does not contain 'TestAuto Catalog 1'");

            /// 4. Click the + icon in the catalog row of the available table and verify that it gets added to the selected table and removed from
            /// the available table
            AMP.AddCatalogToActivity(catalogName);
            Assert.False(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabAvailCatTbl, Bys.ActMainPage.PubDetailsTabAvailCatTblBodyRow, 1,
                 catalogName, "td", Bys.ActMainPage.PubDetailsTabAvailCatTblFirstBtn, Bys.ActMainPage.PubDetailsTabAvailCatTblNextBtn),
                "'TestAuto Catalog 1' was not removed from the available table");
            Assert.True(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabSelCatTbl, Bys.ActMainPage.PubDetailsTabSelCatTblBodyRow, 1,
                catalogName, "td"),
                "'TestAuto Catalog 1' was not added to the selected list");

            /// 5. Verify that the Portals table populates with the cooresponding portal that the Catalog is associated to
            Assert.True(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabPortalsTbl, Bys.ActMainPage.PubDetailsTabPortalsTblBodyRow, 0,
                portalName, "td"));

            /// 6. Click the X icon in the catalog row of the selected table and verify that it gets removed from the selected table and added to
            /// the available table. Also verify that the portal gets removed from the portals table
            AMP.RemoveCatalogFromActivity(catalogName);
            Assert.True(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabAvailCatTbl, Bys.ActMainPage.PubDetailsTabAvailCatTblBodyRow, 1,
                catalogName, "td", Bys.ActMainPage.PubDetailsTabAvailCatTblFirstBtn, Bys.ActMainPage.PubDetailsTabAvailCatTblNextBtn),
                "'TestAuto Catalog 1' was not added to the catalogs available table");
            Assert.False(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabSelCatTbl, Bys.ActMainPage.PubDetailsTabSelCatTblBodyRow, 1,
                catalogName, "td"),
                "'TestAuto Catalog 1' was not removed from the catalogs selected table");
            Assert.False(ElemGet.Grid_ContainsRecord(browser, AMP.PubDetailsTabPortalsTbl, Bys.ActMainPage.PubDetailsTabPortalsTblBodyRow, 0,
                portalName, "td"),
                "'TestAuto Catalog 1' was not removed from the portals selected table");
        }

      //  [Test]
        [Description("When a LMSAdmin admin changes a custom fee on the Publishing Details tab, Then that new fee should be reflected on the portal " +
            "payment page")]
        [Property("Prerequisites", "See manual steps and IDs 4, 5 and 6 here: https://code.premierinc.com/docs/x/BYnbAw")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CustomFee()
        {
            string activityName = "";
            if (BrowserName == BrowserNames.Chrome)
            { activityName = "TestAuto Activity 2 Chrome"; }
            if (BrowserName == BrowserNames.InternetExplorer)
            { activityName = "TestAuto Activity 2 IE"; }
            if (BrowserName == BrowserNames.Firefox)
            { activityName = "TestAuto Activity 2 FF"; }

            string portalName = "_Test Portal";

            /// 1. Login as TestAuto_TestPortal_User1
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage MDP = LP.Login(UserUtils.CMEUser_TestPortal_UserName1, "password");

            /// 2. Open the "TestAuto Activity 1" activity
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);

            /// 3. Go to the Publishing Details tab, then modify the Custom fee for the portal that the activity is associated to         
            string newFee = string.Format("{0}.00", DataUtils.GetRandomInteger(200).ToString());
            AMP.ChangeCustomFee(portalName, newFee);

            /// 4. Get the AID of the activity, navigate to the test portal's Final environment, login, and assert that new fee is reflected on the
            /// Activity Details page
            AMP.ClickAndWait(AMP.DetailsTab);
            string AID = AMP.DetailsTabActivityNumberLbl.Text;
          //  TestPortalHelp.Login(browser, UserUtils_TP.TestPortalUser_UserName1, "test");
          //  Assert.AreEqual("$" + newFee, TestPortalHelp.ActivityFee(browser, AID));
        }

      //  [Test]
        [Description("Given an activity is not added to the Test Portal catalog on the Publishing Details tab, When a user tries to access this activity on the" +
            " Test Portal by entering the AID into the URL, Then the activity should not appear, and When a LMSAdmin user adds this activity to the Test Portal" +
            " catalog, Then that activity should then appear")]
        [Property("Prerequisites", "See manual steps and IDs 7, 8 and 9 here: https://code.premierinc.com/docs/x/BYnbAw")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ActivityPortalAccessDependingOnCatalogAssociation()
        {
            string activityName = "";
            if (BrowserName == BrowserNames.Chrome) { activityName = "TestAuto Activity 3 Chrome"; }
            if (BrowserName == BrowserNames.InternetExplorer) { activityName = "TestAuto Activity 3 IE"; }
            if (BrowserName == BrowserNames.Firefox) { activityName = "TestAuto Activity 3 FF"; }

            string catalogName = "TestAuto Catalog 1";


            /// 1. Login to LMSAdmin as TestAuto_TestPortal_User1
            LoginPage LP = Navigation.GoToLoginPage(browser);
            MyDashboardPage MDP = LP.Login(UserUtils.CMEUser_TestPortal_UserName1, "password");


            /// 2. Open the "TestAuto Activity 1" activity
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);

            /// 3. Go to the Publishing Details and remove "TestAuto Catalog 1" from the Selected Catalogs table.
            AMP.RemoveCatalogFromActivity(catalogName);

            /// 4. Get the AID of the activity, navigate to the test portal's Final environment, login, and assert that the activity does not show up since
            /// the activity is not associated to the catalog
            AMP.ClickAndWait(AMP.DetailsTab);
            string AID = AMP.DetailsTabActivityNumberLbl.Text;
           // TestPortalHelp.Login(browser, UserUtils_TP.TestPortalUser_UserName1, "test");
          //  Assert.False(TestPortalHelp.ActivityAppearingWithAID(browser, AID), "The activity appeared");

            /// 5. Go back to LMSAdmin, add the activity to the portal, then go to the test portal and assert that the activity is now appearing. Note that the
            /// system needs about 5 seconds for the activity to appear on the portal, so we will sleep for 5 seconds
            Navigation.GoToMyDashboardPage(browser);
            MDP.GoToRecentItem(Constants_LMSAdmin.RecentItemCategory.Activity, activityName);
            AMP.AddCatalogToActivity(catalogName);
          //  Assert.True(TestPortalHelp.ActivityAppearingWithAID(browser, AID), "The activity did not appear");
        }


       //[Test]
        [Description("Workflow test making sure nothing goes wrong when a user logs in, creates a project, creates an activity, adds 2 Accreditations each " +
            "with scenarios, 2 awards, 2 assessments, front matter, saves, publishes and then adds a catalog to the activity")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void CreateActivity()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();
                //LoginPage LP = Navigation.GoToLoginPage(Browser);

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.CAP, "password");

                //Legacy_SetupPage SUP = 


                ///// 2. Create activity
                //Activity activity = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneLiveMeeting).
                //    AddLocationToLiveActivity().
                //   AddAccreditation().
                //   AddAccreditation().
                //   AddAward().
                //   AddAward().                    
                //    AddAssessment().
                //    AddFrontMatter().
                //    Save().
                //    Publish().
                //    AddCatalog(Constants.SiteCodes.TP);

                //// stakeholder should be created

                Activity activity = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "TestCreation",
                    Constants_LMSAdmin.LoginUserNames.CAP, "password").AddAccreditation().AddAssessment().Save().
                    Publish();


            }
            }
        [Test]
        public void AssessmentImportTurnon_abam()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();               

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.ABAM, "password");                
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PreTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
            }
        }
        [Test]
        public void AssessmentImportTurnon_ama()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();
                //LoginPage LP = Navigation.GoToLoginPage(Browser);

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.AMA, "password");

                //Legacy_SetupPage SUP = 


                ///// 2. Create activity
                //Activity activity = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneLiveMeeting).
                //    AddLocationToLiveActivity().
                //   AddAccreditation().
                //   AddAccreditation().
                //   AddAward().
                //   AddAward().                    
                //    AddAssessment().
                //    AddFrontMatter().
                //    Save().
                //    Publish().
                //    AddCatalog(Constants.SiteCodes.TP);

                //// stakeholder should be created

                //Activity activity = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "TestCreation",
                //    Constants_LMSAdmin.LoginUserNames.CAP, "password").AddAccreditation().AddAssessment().Save().
                //    Publish();

                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PreTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
            }
        }
        [Test]
        public void AssessmentImportTurnon_asnc()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();
                //LoginPage LP = Navigation.GoToLoginPage(Browser);

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.ASNC, "password");

                //Legacy_SetupPage SUP = 


                ///// 2. Create activity
                //Activity activity = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneLiveMeeting).
                //    AddLocationToLiveActivity().
                //   AddAccreditation().
                //   AddAccreditation().
                //   AddAward().
                //   AddAward().                    
                //    AddAssessment().
                //    AddFrontMatter().
                //    Save().
                //    Publish().
                //    AddCatalog(Constants.SiteCodes.TP);

                //// stakeholder should be created

                //Activity activity = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "TestCreation",
                //    Constants_LMSAdmin.LoginUserNames.CAP, "password").AddAccreditation().AddAssessment().Save().
                //    Publish();

                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PreTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
            }
        }
        [Test]
        public void AssessmentImportTurnon_ons()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.ONS, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PreTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_cmeca()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.CMECAL, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PostTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_chi()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.CHI, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.Evaluation);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_SNMMI()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.SNMMI, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PreTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_pfizer()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.Pfizer, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PostTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_AWHONN()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.AWHONN, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PostTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_cfpc()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.CFPC, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PostTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]

        public void AssessmentImportTurnon_nof()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.NOF, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PostTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_NCPA()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.NCPA, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PostTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_wiley()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.Wiley, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PostTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        [Test]
        public void AssessmentImportTurnon_UCI()
        {
            // Only running this in chrome, as the team made a decision to stop LMSAdmin automation, and that decision was made before I was able to debug this
            // on FF and IE
            if (BrowserName == BrowserNames.Chrome)
            {
                /// 1. Login
                LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

                CMEHelp.Login(Browser, Constants_LMSAdmin.LoginUserNames.UCI, "password");
                Activity activity1 = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "Test");
                ActMainPage AMP = new ActMainPage(Browser);
                ActAssessmentsPage ASP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
                ActAssessmentDetailsPage ASDP = ASP.GoToActAssessmentDetailsPage();
                ASDP.FillAndSaveDetailsTab(Constants.AssessmentTypes.PostTestAssessment);
                ASDP.ClickAndWait(ASDP.QATab);
                Assert.True(Browser.Exists(By.Id("btnImport"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ImportInstruction"), ElementCriteria.TextContains("To import a Microsoft Excel file (.xlsx) of questions and answers, select the 'import button'.")));
                Browser.FindElement(By.Id("btnImport")).Click();
                Assert.True(Browser.Exists(By.XPath("//input[@value='Hide Import']"), ElementCriteria.IsVisible));
                Assert.True(Browser.Exists(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate"), ElementCriteria.TextContains("Click Here.")));
                FileUtils.UploadFileUsingSendKeys(Browser, Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_FileUpload")),
                    "C:\\Users\\bthangar\\Downloads\\ImportTemplate (1).xlsx");
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$UploadFile")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(60));
                Browser.FindElement(By.Name("ctl00$ctl00$ctl00$ctl00$Refresh")).Click();
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 4, "td", "2"));
                Assert.True(ElemGet.Grid_CellTextFound(Browser, Browser.FindElement(By.ClassName("ccTableAL")), 3, "td", "2"));
                Browser.FindElement(By.Id("ctl00_ctl00_ctl00_ctl00_lnkDownloadTemplate")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }


        #endregion Tests

    }
}

