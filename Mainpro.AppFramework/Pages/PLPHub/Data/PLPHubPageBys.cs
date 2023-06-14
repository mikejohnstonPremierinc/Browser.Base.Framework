using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    public class PLPHubPageBys
    {
        //buttons
        public readonly By PlpHubPlpEnterBtn = By.XPath("//a[text()='Enter']");
        public readonly By LearnMoreBtn = By.XPath("//button[text()='Learn More']");
        public readonly By printPlpCertificateLnk = By.XPath("//a[@data-attach-point='printPlpCertificate']");
        public readonly By printCompletedPlpLnk = By.XPath("//a[@data-attach-point='printPlp']");
        public readonly By ViewCompletedPlpLnk = By.XPath("//a[@data-attach-point='viewPlp']");
        public readonly By PrintPLPCertificateDownloadBtn = By.XPath("//div[contains(@class,'printPLPCertDownloadButton')]//span");
        public readonly By PrintmycompletedPLPDownloadBtn = By.XPath("//div[contains(@class,'printPLPCompleteDownloadButton')]//span");
        public readonly By printPLPCertCloseBtn = By.XPath("//div[contains(@class,'printPLPCertCloseButton')]//span");
        public readonly By printPLPCompleteCloseButton = By.XPath("//div[contains(@class,'printPLPCompleteCloseButton')]//span");

        // Charts

        // Check boxes

        // General

        
        // Labels                                              

        // Links


        // Menu Items    

        // Radio buttons

        // Select Element

        // Tables 
        public readonly By PLPHubCompletedPLPTbl = By.XPath("//div[contains(@class, 'plpHubGrid')]//table");
        public readonly By PLPHubCompletedPLPTblHdr = By.XPath("//div[contains(@class, 'plpHubGrid')]//table/thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By PLPHubCompletedPLPTblBody = By.XPath("//div[contains(@class, 'plpHubGrid')]//table/tbody");
        public readonly By PLPHubCompletedPLPTblBodyFirstRow = By.XPath("//div[contains(@class, 'plpHubGrid')]//table/tbody/tr");
        // Tabs

        // Text boxes


    }
}
