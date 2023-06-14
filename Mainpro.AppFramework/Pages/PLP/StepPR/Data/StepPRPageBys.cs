using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class StepPRPageBys
    {

        // Buttons
        public readonly By BackBtn = By.XPath("//span[text()='< Back']");
        public readonly By NextBtn = By.XPath("//div[contains(@class,'plpContinueButton')]//span[text()='Next >']/..");
        public readonly By GoToBottomBtn = By.XPath("//span[contains(@class, 'goto-bottom')]");
        public readonly By CollapseAllBtn = By.XPath("//button[text()='Collapse All']");
        public readonly By ExpandAllBtn = By.XPath("//button[text()='Expand All']");
        public readonly By RecommendMailIconBtn = By.XPath("//div[contains(@class,'glyphicon-envelope')]");
        public readonly By EmailToColleagueModalPopupCloseBtn = By.XPath("//span[text()='Close']");
        public readonly By SubmitBtn = By.XPath("//span[text()='Submit']");
        public readonly By ExitPLPBtn = By.XPath("//span[text()='Exit PLP']");
        public readonly By PLPCertificateBtn = By.XPath("//div[@class='plp-certificate']//button");
        public readonly By PrintmycompletedPLPBtn = By.XPath("//div[@class='plp-summary']//button");
        public readonly By RecommendPLPtoacolleagueBtn = By.XPath("//div[@class='plp-recommend']//button");
        public readonly By StartanewPLPBtn = By.XPath("//div[@class='plp-start']//button");
       public readonly By PrintmycompletedPLPDownloadBtn = By.XPath("//div[contains(@class,'printPLPCompleteDownloadButton')]//span");
       public readonly By PrintPLPCertificateDownloadBtn = By.XPath("//div[contains(@class,'printPLPCertDownloadButton')]//span");
        public readonly By printPLPCertCloseBtn = By.XPath("//div[contains(@class,'printPLPCertCloseButton')]//span");
        public readonly By printPLPCompleteCloseButton = By.XPath("//div[contains(@class,'printPLPCompleteCloseButton')]//span");
        //Image
        public readonly By ContactUsImg = By.XPath("//div[@class='plp-contact-us']//img");


        // Charts

        // Check boxes


        // Labels
        public readonly By EmailToColleagueModalPopupTxt = By.XPath("//div[contains(@class,'emailToColleagueModal')]");
        public readonly By SubmissionContentTxt = By.XPath("//div[contains(@class,'submissionContent')]");

        // Links

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes
        public readonly By AdditionalFeedbackDetailTxt = By.Name("AdditionalFeedbackDetail");


    }
}
