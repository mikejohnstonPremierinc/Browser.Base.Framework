using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using Wikipedia.AppFramework;
using System.Threading;
using AventStack.ExtentReports;
using System;
using System.Linq;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Reactive.Concurrency;
using System.Data;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

/// <summary>
/// The below test class will show examples of how to use this framework. It will also give comment explanations as to what each line
/// of code does. Be sure to go inside of each method (Right click on the method name, then click "Go To Definition") to view the code 
/// and to view more comment explanations. Do this for Page class methods and other "Wikipedia.AppFramework" code, and even code 
/// inside of the Browser.Core.Framework project (Our lowest layer of framework that handles the setup/configuration/teardown of 
/// the Browser itself, and also many shared utilities that can be used across different applications. i.e. "ElemGet", which is 
/// used inside this test class)
/// </summary>
namespace Wikipedia.UITest
{
    /// <summary>
    /// The below lines of code represent remote or local test fixtures that show up in your Test Explorer window depending
    /// on if they are commented out or not. By including these test fixtures, you can run on any browser, and can also run
    /// on a mobile device via emulation inside Chrome. Note that inside the Test Explorer window of this IDE, the remote 
    /// instances will show the "platform" name, i.e. Windows, while local instances do not
    /// </summary>
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [LocalSeleniumTestFixture(BrowserNames.Edge)]
    [LocalSeleniumTestFixture(BrowserNames.Firefox)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, "", "", Platforms.Windows)]
    [RemoteSeleniumTestFixture(BrowserNames.Edge, "", "", Platforms.Windows)]
    [RemoteSeleniumTestFixture(BrowserNames.Firefox, "", "", Platforms.Windows)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome, EmulationDevices.iPhoneX, "", Platforms.Windows, "", "")]

    [TestFixture]
    public class SampleTests : TestBase
    {
        #region Constructors

        /// <summary>
        /// The below 2 constructors represent local versus remote. The remote constructor is the second one. Notice it has more
        /// parameters defined. You will see this represented on the Test Explorer window. The local instances in that window
        /// will only have 1 parameter (Browser name), the remote will have multiple empty double quote parameters
        /// </summary>
        public SampleTests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SampleTests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors


        #region Tests

        /// <summary>
        /// When creating any test method, be sure to include the below NUnit attributes (Test, Description, Status and Author)
        /// </summary>
        [Test]
        [Description("First sample test showing how to use some of the basics of this framework")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("Mikes Category")]
        public void SampleTest_BasicsOfFramework()
        {
            // Include test step comments in your tests, utilizing our Extent Report's TESTSTEP property:
            TESTSTEP.Log(Status.Info, "Navigate to the homepage, do a search on the word \"help\", then wait for the Search " +
                "Results page to return");

            // The below line of code will most likely be used first in every test method. It navigates to any page you want and 
            // returns that initialized page object. See inside the Navigation class for explanation of how this works              
            HomePage HP = Navigation.GoToHomePage(Browser, true);

            // There are many ways to navigate to different pages. The above Navigation class is one way, and below is a 
            // different way. Below, we are calling a custom method inside the Base page class that we have created, which
            // searches, waits for the Search Results page to load, then returns us an instance of the SearchResultsPage 
            // class. Look inside this Search method for more explanation
            SearchResultsPage SP = HP.Search("help");

            // Another example of how to properly navigate to another page. If you are clicking on an element which results in a
            // hidden or disabled element to appear or enable, or results in going to another page, then you can use the
            // ClickAndwait or ClickAndWaitBasePage method, which handles any waiting and any page instantiation 
            TESTSTEP.Log(Status.Info, "Go to the Help page and then Assert that the Table Of Contents table has a row count of 14");
            HP.ClickAndWaitBasePage(HP.VectorMainMenuBtn);

            // Go To Definition for this specific ClickAndWait method, I added a comment about scrolling
            HelpPage HelpPage = HP.ClickAndWait(HP.HelpLnk);
            // Use Asserts to make verifications of data. In this AreEqual Assert, we enter the expected value first "14", then we 
            // grab the actual row count from the Table Of Contents table. This is a good example of using the Browser.Core.Framework
            // utilities classes. In this example, we are using the 'ElemGet' class. This ElemGet class has many different methods
            // that are already created and ready for you to use. Each method inside Browser.Core.Framework includes a detailed
            // description of what the method does, and describes what to pass for each parameter.
            Assert.AreEqual(14, ElemGet.Grid_GetRowCount(Browser, HelpPage.TableOfContentsTbl));

            TESTSTEP.Log(Status.Info, "Assert that the Table Of Contents table contains the tester-specified text");
            // Here is another example of using Browser.Core.Framework's shared code. Go to this method and notice that there are
            // optional parameters defined. We do not need to use these optional parameters because this table does not have 
            // traversal pagination buttons
            Assert.True(ElemGet.Grid_ContainsRecord(Browser, HelpPage.TableOfContentsTbl, Bys.HelpPage.TableOfContentsTblBdy, 0,
                "Who writes Wikipedia?", "a"));
        }

        [Test]
        [TestCase("Username1")]
        [TestCase("Username2")]
        [Description("Showing how to use the TestCase attribute, which allows you to have multiple instances of the same test/code per " +
            " test case data type (user type, etc.). For example, if you type the below search string into the Search bar of " +
            " the Test Explorer window of Visual Studio, you will see 2 instances of this test in the Test Explorer window, " +
            " one for each user...In the Test Explorer window, type the below search string (replace single quotes with double quotes): " +
            "FullName:'chrome' Trait:'local' 'Project:'wikipedia.UITest' " +
            "then expand the tree until you see both instances")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void SameTest_MultipleUsers(string username)
        {
            string myUserName = username;

            if (username == "Username1")
            {
                // do this
            }
        }

        [TestCase(MyUser)]
        [Description("This test has no code, but I put this here to redirect you to a test that does have code to show examples of how " +
            "we work with APIs, Database queries, and Helper Methods. That test lives in the LMS folder of this solution. It is within " +
            "the... LMS folder>LMS.UITest>Tests folder>AHA folder>AHA_MISC_Tests.cs>API_DB_Overview test method " +
            " is the test name.")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void SampleTest_API_DB_HelperMethods(string user)
        {
            string newUser = user;
        }


        [Test]
        [Description("Providing examples for some of the excel utility methods")]
        [Property("Status", "InProgress")]
        [Author("Bama")]
        [DatapointSource]
        public void SampleTest_ExcelFunctions_ViaEPPlus()
        {
            string fileName = "MyExcelWorkbook.xlsx";
            string sheetName = "MySheetName";
            string columnName_activityTitle = "Activity Title";
            string columnName_activityId = "Activity ID";

            // Identifies how many records / rows are available in the excel workbook
            int totalRowCount = FileUtils.Excel_GetTotalRowCount(fileName, sheetName);
            Console.WriteLine("Total Number of Rows in the sheet : " + totalRowCount);

            // Identifies the column index of the specified columnName 
            int activityTitle_columnIndex = FileUtils.Excel_GetColumnNumber(fileName, sheetName, columnName_activityTitle);
            Console.WriteLine(columnName_activityTitle + " Field is in Column Index of : " + activityTitle_columnIndex);

            // Identifies the column index of the specified columnName 
            int activityId_columnIndex = FileUtils.Excel_GetColumnNumber(fileName, sheetName, columnName_activityId);
            Console.WriteLine(columnName_activityId + " Field is in Column Index of : " + activityId_columnIndex);

            Console.WriteLine();

            // Loop through all the records/rows in the excel sheet and print the "Activity Title" and the corresponding "Activity ID"
            // then check the output in console
            for (int rowindex = 1; rowindex <= totalRowCount; rowindex++)
            {
                String activityTitleValue =
                    FileUtils.Excel_GetData_ByRowAndColumn(fileName, sheetName, rowindex, activityTitle_columnIndex);

                String activityIdValue =
                    FileUtils.Excel_GetData_ByRowAndColumn(fileName, sheetName, rowindex, activityId_columnIndex);

                Console.
                    WriteLine("ROW [" + rowindex + "]=> ACTIVITY [ " + activityTitleValue + " ] HAS ACTIVITY ID [ " + activityIdValue + " ]");

            }

        }

        [Test]
        [Description("Showing an alternative way to manipulate excel files")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [DatapointSource]
        public void SampleTest_ExcelFunctions_ViaSystemDataOLEDB()
        {
            DataTable dt = Help.GetAllDataFromExcelSheet();
            //Users userMike = Help.GetUserData("ID");
        }

        [Test]
        [Description("Showing how to download files, edit those files, then upload a file, locally and remotely")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("Mikes Category")]
        public void SampleTest_DownloadAndEditFiles()
        {
            // Navigate to a page which has a download link. Currently the below URL has a Google result that downloads a file 
            // after you click the link of this Google result. If this Google result gets removed in the future, will have to 
            // modify the URL to another URL which contains a download link. Or if the file name changes of the Microsoft 
            // file to be downloaded, will have to modify the fileName parameter
            Browser.Navigate().GoToUrl("https://www.google.com/search?q=microsoft+go+download+excel+file+example");
            IWebElement DownloadBtn = Browser.WaitForElement(By.XPath("//h3[text()='Sample Excel Spreadsheet']"),
                ElementCriteria.IsVisible);

            // There are 3 ways to download a file. You can simultaneously click and wait all within one line of code, as shown in this
            // block of code. Note that the download goes to C:\SeleniumDownloads folder on your local file system...
            string fileName = "Financial Sample.xlsx";
            string pathToDownloadedFile = DownloadBtn.ClickAndWaitForDownload(Browser, fileName, true);
            string folderPath = FileUtils.GetFolderPath_FromFullPath(pathToDownloadedFile);

            // Or you can click the download link then separately wait for the download...
            //DownloadBtn.ClickJS(Browser);
            //pathToDownloadedFile = Browser.WaitForDownload(fileName);

            // Or you can navigate to a URL and wait for the download...
            // I do not know of a public site that has a download available upon navigation, so the below line of code is commented out,
            // and is only for informational purposes to show you the correct method to call for downloading a file upon navigation
            //pathToDownloadedFile = Browser.EnterURLAndWaitForDownload(fileName, "MyUrl.com", true);

            // The above methods will return a string of the path of the download. If you executed your test locally, the
            // path will be c\seleniumdownloads. If you executed your test on the grid, it will be \\YourGridsHubServersName\seleniumdownloads
            // The YourGridsHubServersName value is based on whatever is populated into the HubName parameter of your appsettings.json file
            FileUtils.Excel_SetData_ByRowNumAndColName(fileName, "Sheet1", 1, "Country", "Contry Edited", folderPath);

            // The below shows how to upload a file
            Browser.Navigate().GoToUrl("https://www.w3schools.com/howto/howto_html_file_upload_button.asp");
            IWebElement UploadBtn = Browser.WaitForElement(By.XPath("//input[@type='file']"), ElementCriteria.IsVisible);
            FileUtils.UploadFileUsingSendKeys(Browser, UploadBtn, pathToDownloadedFile);
        }

        #endregion tests
    }
}


