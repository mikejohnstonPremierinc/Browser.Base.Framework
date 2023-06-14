using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActBundlePageBys
    {

        // Buttons
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_bundle')]//div[@title='Continue']");



        // Charts

        // Check boxes

        // General


        // Labels                                              
        public readonly By ActivityTbl_NotStartedLbls = By.XPath("//div[text()='Not started']");
        public readonly By ActivityTbl_InProgressLbls = By.XPath("//div[text()='In Progress']");
        public readonly By ActivityTbl_CompleteLbls = By.XPath("//div[text()='Complete']");



        // Links


        // Menu Items    

        // Radio buttons

        // Tables       
        public readonly By ActivityTbl = By.XPath("//div[@class='fireball-widget childActivityTable']//table");
        public readonly By ActivityTblBody = By.XPath("//div[@class='fireball-widget childActivityTable']//table/tbody");
        public readonly By ActivityTblBodyRow = By.XPath("//div[@class='fireball-widget childActivityTable']//table/tbody/tr");
        public readonly By ActivityTblBodyActivityLnks = By.XPath("//div[@class='fireball-widget childActivityTable']//table/tbody//h4 | //div[@class='fireball-widget childActivityTable']//table/tbody//a");

        // Tabs

        // Text boxes

    }
}