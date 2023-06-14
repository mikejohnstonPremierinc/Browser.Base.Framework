using Browser.Core.Framework;
using LMS.Data;
using LMSAdmin.AppFramework;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Criteria = LMSAdmin.AppFramework.Criteria;

namespace LMSAdmin_CMECA.UITest
{
    //[NonParallelizable]
    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    // [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    //[LocalSeleniumTestFixture(BrowserNames.InternetExplorer)]

    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox)]
    //[RemoteSeleniumTestFixture(BrowserNames.InternetExplorer)]

   [TestFixture]
    public class CMECA_ContentTest : TestBase_CMECA
    {
        #region Constructors
        public CMECA_ContentTest(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public CMECA_ContentTest(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion



        #region Tests
        
        [Test]
        [Description("The test will login Old CME360 and searches for the given test activity and upon selecting the Content node ," +
            " the page will be navigating to Fireball LMSADMIN, When user creates new content for three content types 'Scorm', File Upload, Url"+
            "Then verifies that those are added and displayed successfully , When User Edits verify the saved changes ,When user deletes the created Content, " +
            "Then verifies that those are deleted and not displayed on UI")]
        [Property("Status", "Completed")]
        [Author("Lakshmi Kaveti,Bama Thangaraj")]        
        public void Test_ContentAddEditDelete()
        {
            /// 1. Launch as old cme 360 (ex: lmsadmin.cmeqaf.premierinc.com )            
            LoginPage LP = Navigation.GoToLoginPage(Browser);
           
            /// 2. Username as cmeca_admin to login
            MyDashboardPage MDP = LP.Login(Constants_LMSAdmin.LoginUserNames.CMECAL, "password");
            string activityName = Autotest_Activity1; //Activity Name -> AutomationTest Activity1_DoNotUse_<<BrowserName>>
            string contentURLlink = "http://www.google.com/";

            /// 3. Search for the given Autotest_Activity1 activity 
            SearchResultsPage SP = MDP.Search(activityName);
            ActMainPage AMP = SP.GoToActivity(activityName);
            AMP.ChangeActivityStage(Constants.ActStage.UnderConstruction);

            /// 4. Select the Content node 
            /// 5. The page will be redirected to LMSadmin new UI
                        
            ActContentPage ConP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Content);

            /// Delete old records if any appear
            ConP.DeleteMultipleContentRecords("AutoTestData");
            ConP.RefreshPage();

            #region URLtype - content -  Add, Edit, Delete
            /// 6.Click "Add Content", Add URL Type,Fill out all the mandatory fields and save 
            string URLContentName = ConP.AddContentURL(contentURLlink);
            StringAssert.Contains(URLContentName,
               ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, URLContentName, "td", 1),
            string.Format("Added URL Content {0} not displayed in content table", URLContentName));

            /// 7. Edit the newly added URL Content , Save and Verify the changes shown
            IWebElement URLContentRow = ElemGet.Grid_GetRowByRowName(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, URLContentName, "td");
            ElemSet.Grid_ClickCellByColIndex(Browser, URLContentRow, 1, "//td");
            ConP.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ActContentPage.LoadIconNotExists);
            Browser.WaitForElement(Bys.ActContentPage.EditContentTitleLbl, ElementCriteria.IsVisible);
            string newURLContentName = URLContentName + "_Edited";
            ElemSet.TextBox_EnterText(Browser, ConP.EditContentFormDisplayNameTxt, true, newURLContentName);
            ConP.EditContentFormSaveBtn.Click(); Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.ActContentPage.ContentsTbl, TimeSpan.FromSeconds(30), ElementCriteria.IsVisible);
            StringAssert.Contains(newURLContentName,
               ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, newURLContentName, "td", 1),
            string.Format("Edited URL Content {0} not displayed in content table", newURLContentName));

            /// 8. Delete the newly added URL Content and Verify the content row is deleted from content table
            ConP.DeleteMultipleContentRecords(URLContentName);
            ConP.RefreshPage();
            Assert.IsFalse(ElemGet.Grid_CellTextFound(Browser, ConP.ContentsTbl, 1, "td", URLContentName),
            string.Format("Content [ {0} ] not deleted from content table ", URLContentName));
            #endregion

            ConP.RefreshPage();

            #region  FileUpload Type - Content - Add, Edit, Delete

            /// 9.Click "Add Content", Add File Type,Fill out all the mandatory fields and save 
            string FileContentName = ConP.AddContentFile();
            StringAssert.Contains(FileContentName,
               ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, FileContentName, "td", 1),
            string.Format("Added File Content {0} not displayed in content table", FileContentName));

            /// 10. Edit the newly added File Content , Save and Verify the change shown
            IWebElement FileContentRow = ElemGet.Grid_GetRowByRowName(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, FileContentName, "td");
            ElemSet.Grid_ClickCellByColIndex(Browser, FileContentRow, 1, "//td");
            ConP.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ActContentPage.LoadIconNotExists);
            Browser.WaitForElement(Bys.ActContentPage.EditContentTitleLbl, ElementCriteria.IsVisible);
            string newFileContentName = FileContentName + "_Edited";
            ElemSet.TextBox_EnterText(Browser, ConP.EditContentFormDisplayNameTxt, true, newFileContentName);
            ConP.EditContentFormSaveBtn.Click(); Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.ActContentPage.ContentsTbl, TimeSpan.FromSeconds(30), ElementCriteria.IsVisible);
            StringAssert.Contains(newFileContentName,
               ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, newFileContentName, "td", 1),
            string.Format("Edited File Content {0} not displayed in content table", newFileContentName));

            /// 11. Delete the newly added File Content and Verify the content row is deleted from content table
            ConP.DeleteMultipleContentRecords(FileContentName);
            ConP.RefreshPage();
            Assert.IsFalse(ElemGet.Grid_CellTextFound(Browser, ConP.ContentsTbl, 1, "td", FileContentName),
            string.Format("Content [ {0} ] not deleted from content table ", FileContentName));
            #endregion

            ConP.RefreshPage();
            #region Scorm type - Content - Add, Edit, Delete
            /// 12.Click "Add Content", Add Scorm Type,Fill out all the mandatory fields and save 
            string ScormContentName = ConP.AddContentScorm();
            StringAssert.Contains(ScormContentName,
               ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, ScormContentName, "td", 1),
            string.Format("Added Scorm Content {0} not displayed in content table", ScormContentName));

            /// 13. Edit the newly added Scorm Content , Save and Verify the change shown
            IWebElement ScormContentRow = ElemGet.Grid_GetRowByRowName(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, ScormContentName, "td");
            ElemSet.Grid_ClickCellByColIndex(Browser, ScormContentRow, 1, "//td");
            ConP.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ActContentPage.LoadIconNotExists);
            Browser.WaitForElement(Bys.ActContentPage.EditContentTitleLbl, ElementCriteria.IsVisible);
            string newScormContentName = ScormContentName + "_Edited";
            ElemSet.TextBox_EnterText(Browser, ConP.EditContentFormDisplayNameTxt, true, newScormContentName);
            ConP.EditContentFormSaveBtn.Click(); Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.ActContentPage.ContentsTbl, TimeSpan.FromSeconds(30), ElementCriteria.IsVisible);
            StringAssert.Contains(newScormContentName,
               ElemGet.Grid_GetCellTextByRowNameAndColIndex(Browser, ConP.ContentsTbl, Bys.ActContentPage.ContentsTblFirstRow, newScormContentName, "td", 1),
            string.Format("Edited Scorm Content {0} not displayed in content table", newScormContentName));

            /// 14. Delete the newly added Scorm Content and Verify the content row is deleted from content table
            ConP.DeleteMultipleContentRecords(ScormContentName);
            ConP.RefreshPage();
            Assert.IsFalse(ElemGet.Grid_CellTextFound(Browser, ConP.ContentsTbl, 1, "td", ScormContentName),
            string.Format("Content [ {0} ] not deleted from content table ", ScormContentName));
            #endregion

            /// 15. Click the "back to activity" button and the page will be redirected to Old CME360 and logoff.
            Browser.WaitJSAndJQuery();
            ConP.ClickAndWaitBasePage(ConP.BackToActivityBtn);
            AMP.ClickAndWaitBasePage(AMP.LogoutLnk);
        }

    }

    #endregion Tests

    

}


