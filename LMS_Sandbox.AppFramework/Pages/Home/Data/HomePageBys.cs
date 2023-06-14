using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class HomePageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // General

        // Labels                                              

        // Links

        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By ActivityTable1 = By.XPath("//div[@class='activity-card-list card']");
        public readonly By ActivityTable1_FirstLnk = By.XPath("//div[@class='activity-card-list card']/div[1]/descendant::h4");

        // Tabs

        // Text boxes
    }
}