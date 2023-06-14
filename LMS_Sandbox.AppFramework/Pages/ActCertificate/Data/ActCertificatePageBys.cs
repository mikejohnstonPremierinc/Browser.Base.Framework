using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActCertificatePageBys
    {

        // Buttons
        public readonly By FinishBtn = By.XPath("//body[contains(@class, 'activity_certificate')]//span[text()='Finish']");
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_certificate')]//span[text()='Continue']");
        public readonly By BackBtn = By.XPath("//body[contains(@class, 'activity_certificate')]//span[text()='Back']");

        // Charts

        // Check boxes

        // General
        public readonly By CertificateObject = By.XPath("//body[contains(@class, 'activity_certificate')]//object");


        

        // Labels                                              
        public readonly By CertificateNameLbl = By.XPath("//body[contains(@class, 'activity_certificate')]//div[@class='certificate-name']");
        public readonly By NoCertificatesGeneratedLbl = By.XPath("//td[contains(text(), 'No certificates generated')]");



        

        // Links


        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes

    }
}