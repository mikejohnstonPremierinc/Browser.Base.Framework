using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class SearchResultsPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // Labels                                              

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       
        public readonly By ActivitiesTbl = By.XPath("//div[@id='ActivityResultsPanel']/descendant::table");
        public readonly By ActivitiesTblBody = By.XPath("//div[@id='ActivityResultsPanel']/descendant::table/tbody");
        public readonly By ActivitiesTblBodyRow = By.XPath("//div[@id='ActivityResultsPanel']/descendant::table/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']"); // if one row exists in this table, this will be that row


        // Tabs

        // Text boxes




    }
}