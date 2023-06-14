using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class Step4PageBys
    {

        // Buttons
        public readonly By BackBtn = By.XPath("//span[text()='< Back']");
        public readonly By NextBtn = By.XPath("//div[contains(@class,'plpContinueButton')]//span[text()='Next >']/..");      
        public readonly By SubmitBtn = By.XPath("//span[text()='Submit >']");      
        public readonly By GoToBottomBtn = By.XPath("//span[contains(@class, 'goto-bottom')]");    
        public readonly By CollapseAllBtn = By.XPath("//button[text()='Collapse All']");
        public readonly By ExpandAllBtn = By.XPath("//button[text()='Expand All']");




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
