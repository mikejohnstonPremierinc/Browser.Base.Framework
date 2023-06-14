using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActCPEMonitorPageBys
    {


        // Buttons
        public readonly By SubmitBtn = By.XPath("//div[contains(@class, 'cpeMonitorForm')]/descendant::button[@aria-label='Submit']");

        // Charts


        // Check boxes
        public readonly By IAttestChk = By.XPath("//input[@name='notLicensed']");

        
        // General

        // Labels                                              



        // Links


        // Menu Items    


        // Radio buttons

        // Select Elements
        public readonly By MonthOfBirthSelElem = By.XPath("//div[contains(text(), 'Month of Birth')]/following-sibling::div//select");
        public readonly By MonthOfBirthSelElemBtn = By.XPath("//div[contains(text(), 'Month of Birth')]/following-sibling::div//button");
        public readonly By DayOfBirthSelElem = By.XPath("//div[contains(text(), 'Day of Birth')]/following-sibling::div//select");
        public readonly By DayOfBirthSelElemBtn = By.XPath("//div[contains(text(), 'Day of Birth')]/following-sibling::div//button");

        // Tables       


        // Tabs


        // Text boxes
        public readonly By NABPEProfileIDTxt = By.XPath("//input[contains(@aria-label, 'NABP e-Profile ID')]");







        

    }
}