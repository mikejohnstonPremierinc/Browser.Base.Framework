using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActOrderReceiptPageBys
    {




        // Buttons
        //public readonly By ContinueBtn = By.XPath("//a[text()='Continue']");
        public readonly By ExitBtn = By.XPath("//a[text()='Exit']");

        // Charts


        // Check boxes


        // General

        // Labels                                              
        public readonly By ActivityFeeLbl = By.XPath("//td[text()='Activity Fee:']/following-sibling::td");
        public readonly By TotalAmountLbl = By.XPath("//td[text()='Total Amount:']/following-sibling::td");


        // Links


        // Menu Items    


        // Radio buttons

        // Select Elements

        // Tables       


        // Tabs


        // Text boxes






    }
}