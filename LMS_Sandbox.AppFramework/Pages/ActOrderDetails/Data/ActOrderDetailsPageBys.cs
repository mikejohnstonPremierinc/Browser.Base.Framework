using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActOrderDetailsPageBys
    {




        // Buttons
        public readonly By ContinueToPaymentBtn = By.XPath("//span[text()='Continue to Payment']|//span[text()='Continue To Payment']");
        public readonly By CompleteOrderBtn = By.XPath("//span[text()='Complete Order']");
        public readonly By ConfirmFormOkBtn = By.XPath("//button[text()='Ok']");
        public readonly By ConfirmFormCancelBtn = By.XPath("//button[text()='Cancel']");
        public readonly By RemoveCodeBtn = By.XPath("//button[@aria-label='Remove Code']");
        public readonly By ApplyCodeBtn = By.XPath("//button[@aria-label='Apply Code']");

        // Charts


        // Check boxes


        // General

        // Labels                                              
        public readonly By ActivityFeeValueLbl = By.XPath("//p[@name='activityFee']");
        public readonly By DiscountFeeValueLbl = By.XPath("//p[@name='discountFee']");
        public readonly By TotalFeeValueLbl = By.XPath("//p[@name='totalFee']");


        // Links


        // Menu Items    


        // Radio buttons

        // Select Elements

        // Tables       


        // Tabs


        // Text boxes
        public readonly By DiscountCodeTxt = By.XPath("//input[@name='discountCode']");





    }
}