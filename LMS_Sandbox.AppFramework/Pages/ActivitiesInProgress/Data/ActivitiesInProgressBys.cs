using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActivitiesInProgressPageBys
    {

        // Buttons
        

        // Charts

        // Check boxes

        // Labels                                              
        public readonly By ActivitiesInProgressLbl = By.XPath("//h2[contains(text(), 'Activities in Progress')]");
        public readonly By YouAreCurrentlyNotParticipatingLbl = By.XPath("//td[text()='You are currently not participating in any activities']");


        // Links

        // Menu Items    

        // Radio buttons

        // Tables       
        public readonly By ActivitiesTbl = By.XPath("//table[@class='grid-table']");
        public readonly By ActivitiesTblBody = By.XPath("//table[@class='grid-table']/tbody");
        public readonly By ActivitiesTblFirstLnk = By.XPath("//table[@class='grid-table']/tbody//td[2]/ancestor::tr");
        public readonly By ActivitiesTblExpandBtns = By.XPath("//button[contains(@class, 'grid-expand-button')]/div");

        // Tabs

        // Text boxes

    }
}