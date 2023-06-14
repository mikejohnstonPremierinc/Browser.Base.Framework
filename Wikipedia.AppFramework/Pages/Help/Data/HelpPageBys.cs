using OpenQA.Selenium;

namespace Wikipedia.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PQA/PageBy+Class+and+Naming+Conventions
    /// </summary>
    public class HelpPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // Labels                                              
        
        // Links

        // Menu Items    

        // Radio buttons

        // Select Elements  

        // Tables       
        public readonly By TableOfContentsTbl = By.XPath("//div[@role='navigation']/table");
        public readonly By TableOfContentsTblHdr = By.XPath("//div[@role='navigation']/table/tbody/tr");
        public readonly By TableOfContentsTblBdy = By.XPath("//div[@role='navigation']/table/tbody");
        public readonly By TableOfContentsTblFirstRow = By.XPath("//div[@role='navigation']/table/tbody/tr[3]");

        // Tabs

        // Text boxes
        public readonly By SearchTxt = By.XPath("//div[@role='navigation']/table");


    }
}