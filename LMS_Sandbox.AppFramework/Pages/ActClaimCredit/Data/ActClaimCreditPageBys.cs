using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActClaimCreditPageBys
    {

        // Buttons
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_claim')]//span[text()='Continue']/..");
        public readonly By FinishBtn = By.XPath("//body[contains(@class, 'activity_claim')]//span[text()='Finish']");

        public readonly By ClaimCreditBtns = By.XPath("//span[text()='CLAIM']");

        

        // Charts

        // Check boxes



        // Labels                                              


        // Links

        // Menu Items    

        // Radio buttons

        // Tables               

        // Tabs

        // Text boxes



    }
}