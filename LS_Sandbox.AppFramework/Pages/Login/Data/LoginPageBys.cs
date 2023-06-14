using OpenQA.Selenium;

namespace LS.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class LoginPageBys
    {     

        // Buttons
        public readonly By LoginBtn = By.XPath("//input[@value='Log In']");

        // Charts

        // Check boxes

        // Labels                                              

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes
        public readonly By UserNameTxt = By.XPath("//input[@id='test']");
        public readonly By PasswordTxt = By.XPath("//input[@id='Password']");



    }
}