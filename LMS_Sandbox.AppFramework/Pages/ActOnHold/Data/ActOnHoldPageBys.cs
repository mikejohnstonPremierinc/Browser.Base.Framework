using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActOnHoldPageBys
    {

        // Buttons
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_hold')]//div[@title='Continue']");

        // Charts

        // Check boxes


        // General       
        public readonly By NotificationWarnIcon = By.XPath("//body[contains(@class, 'activity_hold')]/descendant::div[contains(@class, 'notification-warn')]/descendant::i[@class='notification-icon']");

        // Labels                                                      
        public readonly By NotificationWarnMessageLbl = By.XPath("//div[contains(@class, 'notification-warn')]//span[@class='notification-message']");


        // Links

        // Menu Items    

        // Radio buttons

        // Tables               

        // Tabs

        // Text boxes



    }
}