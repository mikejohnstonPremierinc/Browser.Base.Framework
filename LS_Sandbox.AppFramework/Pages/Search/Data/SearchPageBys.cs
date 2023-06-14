using OpenQA.Selenium;

namespace LS.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class SearchPageBys
    {

        // Buttons
        
        public readonly By GoBtn = By.XPath("//input[@value='Go']");

        // Charts

        // Check boxes

        // Labels                                              
        public readonly By NoResultsFoundLbl = By.XPath("//td[text()='No results were found']");


        // Links

        // Menu Items    


        // Radio buttons


        // Tables       
        public readonly By GenericTblBody = By.XPath("//table/tbody");
        public readonly By GenericTblBodyRow = By.XPath("//table/tbody/tr"); // If one row exists in ANY table for any search page, then this will be that row
        public readonly By SitesTbl = By.XPath("//table[@id='usersites']");
        public readonly By SitesTblBody = By.XPath("//table[@id='usersites']/tbody"); 
        public readonly By SitesTblBodyRow = By.XPath("//table[@id='usersites']/tbody/tr"); // If one row exists in this table, then this will be that row
        public readonly By AllParticpantsTbl = By.XPath("//table[@id='part']");
        public readonly By AllParticpantsTblBody = By.XPath("//table[@id='part']/tbody");
        public readonly By AllParticpantsTblBodyRow = By.XPath("//table[@id='part']/tbody"); // If one row exists in this table, then this will be that row


        

        // Tabs


        // Text boxes
        public readonly By SearchTxt = By.XPath("//input[@type='text']");

    }
}