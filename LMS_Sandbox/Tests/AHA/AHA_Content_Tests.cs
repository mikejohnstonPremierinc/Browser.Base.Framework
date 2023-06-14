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

namespace AHA.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows, "", "")]
    //[RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class AHA_Content : TestBase_AHA
    {
        #region Constructors

        public AHA_Content(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public AHA_Content(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors

        #region Tests

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am on the Activity Material page, When I view the content types, Then all " +
            "downloadable content types should appear and their cooresponding title text and content type " +
            "format should show as expected")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ContentLinkTextAndPNGFile(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Content_All.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Navigate to the Activity Material page for an activity that has content for all file types and 
            /// assert that the following content types exist: DOCX, XLSX, PowerPoint, Text, PNG
            // The Content tab for this activity always throws a We're Sorry error when many tests are executed
            // in parallel (except for the first ContentLinkTextAndPNGFile test from AHA, that test gets kicked
            // off first due to alphabetical order). Inserting a try catch here as a workaround, then if DEV
            // fixes, can remove this
            ActMaterialPage AP = new ActMaterialPage(Browser);
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Material,
                        false, user.Username);
                }
                catch 
                {
                    // If still fails after 5 tries, test will fail here
                    if (i == 4)
                    {
                        AP.RefreshPage();
                    }
                }
            }

            /// 2. Assert that the icon and text match the content type format
            Assert.True(Browser.Exists(Bys.ActMaterialPage.WordContentTypeITagElem),
                "The i tag element did not contain the text 'doc.png' in the Style attribute for the Word content type");
            Assert.True(Browser.Exists((Bys.ActMaterialPage.WordContentTypeATagElem)),
                "The a tag element did not contain the text 'Word' in the Text attribute for the Word content type");

            Assert.True(Browser.Exists(Bys.ActMaterialPage.XLSXContentTypeITagElem),
                "The i tag element did not contain the text 'xls.png' in the Style attribute for the Excel content type");
            Assert.True(Browser.Exists((Bys.ActMaterialPage.XLSXContentTypeATagElem)),
                "The a tag element did not contain the text 'Excel' in the Text attribute for the Excel content type");

            Assert.True(Browser.Exists(Bys.ActMaterialPage.PPTContentTypeITagElem),
                "The i tag element did not contain the text 'ppt.png' in the Style attribute for the PPT content type");
            Assert.True(Browser.Exists((Bys.ActMaterialPage.PPTContentTypeATagElem)),
                "The a tag element did not contain the text 'PPT' in the Text attribute for the PPT content type");

            Assert.True(Browser.Exists(Bys.ActMaterialPage.TXTContentTypeITagElem),
                "The i tag element did not contain the text 'none.png' in the Style attribute for the TEXT content type");
            Assert.True(Browser.Exists((Bys.ActMaterialPage.TXTContentTypeATagElem)),
                "The a tag element did not contain the text 'TEXT' in the Text attribute for the TEXT content type");

            Assert.True(Browser.Exists(Bys.ActMaterialPage.PNGContentTypeITagElem),
                "The i tag element did not contain the text 'none.png' in the Style attribute for the PNG content type");
            Assert.True(Browser.Exists(Bys.ActMaterialPage.PNGContentTypeATagElem),
                "The a tag element did not contain the text 'PNG' in the Text attribute for the PNG content type");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Given I am on the Activity Material page, When I click the link for a URL content type, Then a new tab should load with " +
            "the URL from the database for this content type")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ContentOpensInNewTab(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_Content_All.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Navigate to the Activity Material page for an activity that has URL content types
            ActMaterialPage AMP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Material, false, user.Username);

            /// 2. Assert that the expansion icon does not exist for this content type
            Assert.False(Browser.Exists(By.XPath("//a[contains(text(),'Google')]//ancestor::tr//button[@class='grid-expand-button']")),
                "An expansion icon existed for the URL content type");

            /// 3. Get the expected URL from the database for URL content types, click the link, then assert that it opens
            /// in a new tabs with the expected URL, and the page loads
            string expectedURLWindowUrl = DBUtils.GetActivityContentTypeInfo(siteCode, actTitle, Constants.ActContentType.Google, "URL");        
            Help.OpenAndSwitchToContentTypeInNewWindowOrTab(Browser, "Google", "//*[@name='q']");
            Assert.True(Browser.Exists(By.XPath("//*[@name='q']")), "The new tab did not load the page");
            string currentURLWindowUrl = Browser.Url;
            StringAssert.Contains(expectedURLWindowUrl, currentURLWindowUrl);
        }


        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("UAT"), Category("Prod")]
        [Description("Validating that the Body Interact and Impelsys content types act as links and open in a new tab")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void BodyInteractImpelsys(Constants.SiteCodes siteCode)
        {
            string actTitle = Constants.ActTitle.Automation_14_Activity_Content_4BodyInteractImpelysis.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Go to the Content page 
            ActMaterialPage AMP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle,
                Constants.Pages_ActivityPage.Material, false, user.Username);

            /// 2. Verify that the body interact link goes to the correct URL
            Help.OpenAndSwitchToContentTypeInNewWindowOrTab(Browser, "BI 55", URLToWaitFor: "web.bodyinteract.com");
            StringAssert.Contains("55", browser.Url);
            Browser.SwitchTo().Window(Browser.WindowHandles.First());

            /// 3. Verify that the Impelsys link opens a new tab. If it throws an error, that is fine, as we are 
            /// not responsible for these 3rd party pages
            Help.OpenAndSwitchToContentTypeInNewWindowOrTab(Browser, "Impelsys 6", URLToWaitFor: "ebooks.heart.org");
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("Prod"), Category("OnDemandOnly")]
        [Description("Given I am on the Activity Material page, When I click the downloadable content type links, Then the appropriate file " +
            "should download with the appropriate file name from the database")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ContentDownloadFiles(Constants.SiteCodes siteCode)
        {
            if (TestContext.CurrentContext.Test.FullName.ToString().Contains("WINDOWS"))
            {
                Assert.Ignore("Only running this locally because we can not control remote machines file system");
            }

            string actTitle = Constants.ActTitle.Automation_Content_All.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Navigate to the Activity Material page for an activity that has content for all file types, download each file and 
            /// assert that the file names match what is in the DB
            ActMaterialPage AMP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, Constants.Pages_ActivityPage.Material, false, user.Username);

            string wordFileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.Word);
            AMP.DownloadFile("Word", wordFileNameDB, true);

            string excelFileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.Excel);
            AMP.DownloadFile("Excel", excelFileNameDB, true);

            string pptFileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.PPT);
            AMP.DownloadFile("PPT", pptFileNameDB, true);

            string textFileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.TEXT);
            AMP.DownloadFile("TEXT", textFileNameDB, true);

            string pngFileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.PNG);
            AMP.DownloadFile("PNG", pngFileNameDB, true);

            string htmlFileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.PNG);
            AMP.DownloadFile("PNG", htmlFileNameDB, true);
        }

        [TestCase(siteCodeAttribute), Category(siteCodeCategory), Category("QA"), Category("Prod"), Category("OnDemandOnly")]
        [Description("Given I am on the Activity Material page, When I click the downloadable embedded content type links, Then the " +
            "appropriate file should download with the appropriate file name from the database")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void ContentEmbeddedDownloads(Constants.SiteCodes siteCode)
        {
            if (TestContext.CurrentContext.Test.FullName.ToString().Contains("WINDOWS"))
            {
                Assert.Ignore("Only running this locally because we can not control remote machines file system");
            }

            string actTitle = Constants.ActTitle.Automation_Content_All.GetDescription();
            UserModel user = profession1User1;
            APIHelp.DeleteActivityForUser(user.Username, actTitle, siteCode);

            /// 1. Navigate to the Activity Material page for an activity that has content for all file types, download each file and 
            /// assert that the file names match what is in the DB
            ActMaterialPage AMP = Help.GoTo_ActivityWorkflow_SpecificPage(Browser, siteCode, actTitle, 
                Constants.Pages_ActivityPage.Material, false, user.Username);

            string jpgFileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.JPEG);
            AMP.DownloadEmbeddedFile(siteCode, "JPEG", Constants.ActContentType.JPEG, jpgFileNameDB, true);

            string pdfFileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.PDF);
            AMP.DownloadEmbeddedFile(siteCode, "PDF", Constants.ActContentType.PDF, pdfFileNameDB, true);

            string mp3FileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.Audio);
            AMP.DownloadEmbeddedFile(siteCode, "Audio", Constants.ActContentType.Audio, mp3FileNameDB, true);

            string mp4FileNameDB = DBUtils.GetActivityContentTypeFileName(siteCode, actTitle, Constants.ActContentType.Video);
            AMP.DownloadEmbeddedFile(siteCode, "Video", Constants.ActContentType.Video, mp4FileNameDB, true, 60000);
        }

        #endregion tests
    }
}






