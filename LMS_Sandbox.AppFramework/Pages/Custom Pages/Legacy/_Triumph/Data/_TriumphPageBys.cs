using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class _TriumphPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // General

        // Labels                                              

        // Links
        public readonly By ProfessionalEducationTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[2]/../descendant::span//a[@href]");
        public readonly By MyRecentActivitiesTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[3]/../descendant::span//a[@href]");
        public readonly By FeaturedActivitiesTblHdrLnks = By.XPath("(//span[contains(@id, 'UAMS')])[4]/../descendant::span//a[@href]");


        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By ProfessionalEducationTbl = By.XPath("//div[contains(@id, 'ActivityListingControl1')]");
        public readonly By ProfessionalEducationTblFirstLnk = By.XPath("//div[contains(@id, 'ActivityListingControl1')]//td/a | //div[contains(@id, 'ActivityListingControl2')]//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By MyRecentActivitiesTbl = By.XPath("//div[contains(@id, 'ActivityListingControl2')]");
        public readonly By MyRecentActivitiesTblFirstLnk = By.XPath("//div[contains(@id, 'ActivityListingControl2')]//td/a | //div[contains(@id, 'ActivityListingControl2')]//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");
        public readonly By FeaturedActivitiesTbl = By.XPath("//div[contains(@id, 'ActivityListingControl3')]");
        public readonly By FeaturedActivitiesTblFirstLnk = By.XPath("//div[contains(@id, 'ActivityListingControl3')]//td/a | //div[contains(@id, 'ActivityListingControl2')]//div[contains(text(), 'No custom list of activities') or contains(text(), 'No recent activities')]");

        
        // Tabs

        // Text boxes



    }
}