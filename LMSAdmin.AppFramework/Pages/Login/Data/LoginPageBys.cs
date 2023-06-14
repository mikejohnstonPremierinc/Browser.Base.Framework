using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class LoginPageBys
    {     

        // Buttons
        public readonly By LoginBtn = By.Id("PortalLogin_Submit1");
        public readonly By IAcceptBtn = By.Id("EULAAcceptance1_btnOK");
        public readonly By MultiSessionContinueBtn = By.Id("btnContinue");

        // Charts

        // Check boxes

        // Labels             
        public readonly By MultiSessionAlertMsg = By.Id("MultiSesssionMsg");

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes
        public readonly By UserNameTxt = By.Id("PortalLogin_username");
        public readonly By PasswordTxt = By.Id("PortalLogin_password");



    }
}