using OpenQA.Selenium;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PQA/PageBy+Class+and+Naming+Conventions
    /// </summary>
    public class WikipediaBasePageBys
    {
        // Banners


        // Buttons
        public readonly By SearchBtn = By.XPath("//input[@type='submit' and not(contains(@class, 'mw-fallbackSearchButton'))] | //button[contains(@class, 'search-input')]");
        public readonly By VectorMainMenuBtn = By.Id("vector-main-menu-dropdown-checkbox");
        
        // Charts

        // Check boxes

        // Frames

        // images
        public readonly By LogoImg = By.ClassName("mw-logo-icon");

       

        // Labels    

        // Links
        public readonly By HelpLnk = By.XPath("//a//span[text()='Help']");
        public readonly By ContributionsLnk = By.XPath("//span[text()='Contributions']");

        
        //Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes
        public readonly By SearchTxt = By.XPath("//input[contains(@id, 'searchInput')] | //div[contains(@class, 'search-input')]");

       












    }
}