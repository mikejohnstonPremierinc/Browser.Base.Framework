using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class SearchPageBys
    {

        // Buttons
        public readonly By CreditTypeBtn = By.XPath("//span[text()='Credit Type:']/..//button");
        public readonly By Mobile_ShowHideFiltersBtn = By.XPath("//div[@class='show-hide-filters-button']/div/div/div");

        
        // Charts


        // Check boxes


        // Labels                                              
        public readonly By NoDataAvailableLbl = By.XPath("//td[text()='No data available']");



        // Links
        public readonly By BinLnks = By.XPath("//td[text()='No data available']");


        // Menu Items    


        // Radio buttons

        // select elements       
        public readonly By CreditTypeSelElem = By.XPath("//span[text()='Credit Type:']/..//select");


        // Tables              
        public readonly By SearchResultsTbl = By.XPath("//div[@class='fireball-widget activityCatalogTable']//table");
        public readonly By SearchResultsTblBody = By.XPath("//div[@class='fireball-widget activityCatalogTable']//table/tbody");
        public readonly By SearchResultsTblBodyActivityLnks = By.XPath("//div[@class='fireball-widget activityCatalogTable']//table/tbody//h4");


        // Tabs


        // Text boxes



    }
}