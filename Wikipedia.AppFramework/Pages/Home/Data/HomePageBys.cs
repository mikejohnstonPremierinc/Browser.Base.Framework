using OpenQA.Selenium;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PQA/PageBy+Class+and+Naming+Conventions
    /// </summary>
    public class HomePageBys
    {

        // Buttons



        // Charts

        // Check boxes

        // General
        public readonly By WikipediaImg = By.XPath("//img[@alt='Wikipedia']");

        // Labels                                              

        // Links
        public readonly By WikipediaLnk = By.LinkText("Wikipedia");

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes

    }
}