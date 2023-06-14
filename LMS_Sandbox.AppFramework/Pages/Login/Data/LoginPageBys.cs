using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginPageBys
    {     

        // Buttons
        public readonly By LoginBtn = By.XPath("//td[@class='LoginButtonAlign']/descendant::input[@type='submit' and contains(@id, 'Login')] | //*[@id='ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl2_LoginControl1_LTLogin_tbNewSecurityAnswer'] | //*[@id='ctl00_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl1_LTLogin_Login']");
        public readonly By SelectASecurityQuesFormSubmitBtn = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl2_LoginControl1_LTLogin_btnChangePasswordSubmit");

        
        // Charts

        // Check boxes
        public readonly By RememberMeChk = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl2_LoginControl1_LTLogin_RememberMe");

        // Labels                                              
        public readonly By UserNameWarningLbl = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl2_LoginControl1_LTLogin_UserNameRequired");
        public readonly By PasswordWarningLbl = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl2_LoginControl1_LTLogin_PasswordRequired");
        public readonly By LoginUnsuccessfullWarningLbl = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl2_LoginControl1_LTLogin_FailureText");

        // Links
        public readonly By ClickHereToRegLnk = By.XPath("//a[text()='here']");

        // Menu Items    

        // Radio buttons

        // Select Element
        public readonly By SelectASecurityQuesFormNewSecQuesSelElem = By.Id("ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl2_LoginControl1_LTLogin_ddlNewSecurityQuestion");

        

        // Tables       

        // Tabs

        // Text boxes
        // Second value is for CMECA
        public readonly By UserNameTxt = By.XPath("//span[text()='Username']/ancestor::tr/following-sibling::tr//input|//td[text()='Password']/../following-sibling::tr//input | //div[contains(@id, 'LoginControl')]/descendant::input[contains(@name, 'UserName')]");
        public readonly By PasswordTxt = By.XPath("//span[text()='Password']/ancestor::tr/following-sibling::tr//input|//td[text()='Password']/../following-sibling::tr//input | //div[contains(@id, 'LoginControl')]/descendant::input[contains(@name, 'Password')]");
        public readonly By SelectASecurityQuesFormNewSecAnsTxt = By.Id("//*[@id='ctl00_ctl00_ContentPlaceHolderBase1_ContentPlaceHolder1_cpLoginAspx_ctl00_LoginControl2_LoginControl1_LTLogin_tbNewSecurityAnswer']");



    }
}