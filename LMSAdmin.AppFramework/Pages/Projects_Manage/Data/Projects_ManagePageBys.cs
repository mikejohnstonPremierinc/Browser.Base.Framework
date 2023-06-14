using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class Projects_ManagePageBys
    {

        // Buttons
        public readonly By ManageSearchBtn = By.Id("ctl00_btnSearchAct");

        // Charts

        // Check boxes

        // Labels                                              

        // Links
        public readonly By AddNewProjectLnk = By.XPath("//strong[contains(text(), 'Add')]");

        // Menu Items    

        // Radio buttons

        // Tables       
        public readonly By ManageTbl = By.ClassName("ccTableAL");
        public readonly By ManageTblBodyRow = By.XPath("//table[@class='ccTableAL']/descendant::tr/following-sibling::tr[@class='ccTableRow' or @class='ccTableRowAlt']");  // If one row exists, this will represent that row

        


        // Tabs

        // Text boxes
        public readonly By ManageSearchTxt = By.Id("ctl00_txtSearchAct");

        

    }
}