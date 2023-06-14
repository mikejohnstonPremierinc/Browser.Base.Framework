using AventStack.ExtentReports;
using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using Wikipedia.AppFramework;


/// <summary>
/// Use this class to create any test that you want and to play around in. Note that if you check code in, first make sure this
/// project builds without exceptions
/// </summary>
namespace Wikipedia.UITest
{
    [TestFixture]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class SandboxTests : TestBase
    {
        #region Constructors

        public SandboxTests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public SandboxTests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }

        #endregion Constructors



        #region Tests
        [Test]
        [Description("Use this test method as a reference when creating your shared utitily test method that was requested " +
            "within our Curriculum from our Documentation.")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        public void SharedUtilityStarter()
        {
            HomePage HP = Navigation.GoToHomePage(Browser, true);
            SearchResultsPage SP = HP.Search("Spreadsheet");

            // Below, I am retreiving the cell text by specifying the column index and row index. For all of these shared methods
            // I add comments defining each parameter and telling you what you need to pass to each parameter. Notice that I am
            // passing the MySpreadsheetTblBody and MySpreadsheetTblBodyRow elements. Please open the SearchResultsPageBys.cs
            // class file and read the comments above the table elements.
            string cellText = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(browser, SP.MySpreadsheetTblBody,
                Bys.SearchResultsPage.MySpreadsheetTblBodyRow, 2, 2);
        }

        #endregion tests
    }
}





