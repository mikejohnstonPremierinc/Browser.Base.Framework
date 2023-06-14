using OpenQA.Selenium;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PQA/PageBy+Class+and+Naming+Conventions
    /// </summary>
    public class SearchResultsPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // Labels                                              

        // Links
        public readonly By HelpContentsLnk = By.LinkText("Help:Contents");

        // Menu Items    

        // Radio buttons

        // Select Elements  

        // Tables       
        // Whenever adding table elements to your PageBy class, be sure to add the Tbl, the TblHdr, the TblBody and the TblBodyRow
        // If you add all 4, you can then use ALL of the the ElemGet.Grid... and ElemSet.Grid... shared methods 
        public readonly By MySpreadsheetTbl = By.XPath("//caption[contains(.,'My Spreadsheet')]/..");
        public readonly By MySpreadsheetTblHdr = By.XPath("//caption[contains(.,'My Spreadsheet')]/../tbody/tr[1]");
        public readonly By MySpreadsheetTblBody = By.XPath("//caption[contains(.,'My Spreadsheet')]/../tbody");
        public readonly By MySpreadsheetTblBodyRow = By.XPath("//caption[contains(.,'My Spreadsheet')]/../tbody/tr[2]");


        // Tabs

        // Text boxes

    }
}