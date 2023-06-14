using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class Distribution_PortalsPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // Labels                                              


        // Links
        public readonly By AddNewPortalLnk = By.XPath("//strong[text()='Add New Portal']");

        // Menu Items    

        // Radio buttons

        // Tables   
        public readonly By CatAndActTabSelCatalogTbl = By.XPath("(//table[@class='ccTableAL'])[1]");
        public readonly By PortalLibraryTbl = By.Id("ctl00_distributionSourceGrid");
        public readonly By PortalLibraryTblBody = By.Id("//table[@id='ctl00_distributionSourceGrid']/tbody");
        public readonly By PortalLibraryTblBodyRow = By.XPath("//table[@id='ctl00_distributionSourceGrid']/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");  // Represents the first row in the table, if there are any rows appearing

        public readonly By CatAndActTabSelCatalogsTbl = By.XPath("//td[@id='ContentPane']/table[1]/tbody[1]/tr[2]/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td/table[1]");
        public readonly By CatAndActTabSelCatalogsTblBody = By.XPath("//td[@id='ContentPane']/table[1]/tbody[1]/tr[2]/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td/table[1]/tbody");
        public readonly By CatAndActTabSelCatalogsTblBodyRow = By.XPath("//td[@id='ContentPane']/table[1]/tbody[1]/tr[2]/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td/table[1]/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");



        // Tabs
        public readonly By CatAndActTab = By.XPath("//a[text()='Catalogs And Activities']");


        // Text boxes

        // Labels                                              
        public readonly By FilterByLbl = By.XPath("//strong[text()='Filter By: ']");
        public readonly By CatalogLibraryLbl = By.XPath("//div[@id='ctl00_pnlHeading']/descendant::span[1]");








    }
}