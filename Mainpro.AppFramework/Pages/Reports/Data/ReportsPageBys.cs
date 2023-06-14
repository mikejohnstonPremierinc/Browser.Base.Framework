using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class ReportsPageBys
    {

        // Button
        public readonly By MyCreditSummaryRunReportBtn = By.XPath("//div[contains(@class, 'reportCreditSummaryButton')]//span[text()='Run Report']");
        public readonly By MyCreditSummaryFormCreateReportBtn = By.XPath("//div[contains(text(), 'My Credit Summary')]/..//span[text()='Create Report']");
        public readonly By MyCreditSummaryFormDownloadReportBtn = By.XPath("//div[contains(@class, 'cpdCreditDownloadButton')]//span[text()='Download Report']");
        public readonly By MyCreditSummaryFormXBtn = By.XPath("//div[contains(@class, 'reportCreditSummaryModalView')]//div[@aria-label='Close']");
        
        public readonly By MyTranscriptOfCPDActsRunReportBtn = By.XPath("//div[contains(@class, 'reportTranscriptButton')]//span[text()='Run Report']");
        public readonly By MyTranscriptOfCPDActsFormCreateReportBtn = By.XPath("//div[contains(@class, 'cpdTranscriptInputForm')]//span[text()='Create Report']");
        public readonly By MyTranscriptOfCPDActsFormDownloadReportBtn = By.XPath("//div[contains(@class, 'cpdTranscriptDownloadButton')]//span[text()='Download Report']");
        public readonly By MyTranscriptOfCPDActsFormXBtn = By.XPath("//div[contains(@class, 'cpdActivitiesTranscriptModalView')]//div[@aria-label='Close']");

        public readonly By MyMainproCycleCompleteionCertRunReportBtn = By.XPath("//div[contains(@class, 'reportCycleCompletionButton')]//span[text()='Run Report']");
        public readonly By MyMainproCycleCompleteionCertFormCreateReportBtn = By.XPath("//div[contains(@class, 'reportMainProSummaryForm')]//span[text()='Create Report']");
        public readonly By MyMainproCycleCompleteionCertFormDownloadReportBtn = By.XPath("//div[contains(@class, 'cpdMainProDownloadButton')]//span[text()='Download Report']");
        public readonly By MyMainproCycleCompleteionCertFormXBtn = By.XPath("//div[contains(@class, 'ReportMainProCycleModalView')]//div[@aria-label='Close']");


        // Check box

        // General
        // This is the element that we can verify exists and is visible within the PDF browser window. Chrome and Edge can be 
        // verified by the embed element. Firefox does not have this embed element
        public readonly By ReportPDFEmbedElem = By.XPath("//body/embed[@type='application/pdf']"); 
        public readonly By ReportPDFEmbedElemFirefox = By.XPath("//body//div[@id='viewerContainer']"); 

        // Label


        // Link

        // Menu Item    

        // Radio button

        // Select Element
        public readonly By MyCreditSummaryFormFormCycleSelElem = By.XPath("//div[contains(@class, 'reportCreditSummaryForm')]//div[text()='Cycle:']/..//select");
        public readonly By MyCreditSummaryFormCycleSelElemBtn = By.XPath("//div[contains(@class, 'reportCreditSummaryForm')]//div[text()='Cycle:']/..//button");

        public readonly By MyTranscriptOfCPDActsFormCycleSelElem = By.XPath("//div[contains(@class, 'cpdTranscriptInputForm')]//div[text()='Cycle:']/..//select");
        public readonly By MyTranscriptOfCPDActsFormCycleSelElemBtn = By.XPath("//div[contains(@class, 'cpdTranscriptInputForm')]//div[text()='Cycle:']/..//button");

        public readonly By MyMainproCycleCompleteionCertFormCycleSelElem = By.XPath("//div[contains(@class, 'reportMainProSummaryForm')]//div[text()='Cycle:']/..//select");
        public readonly By MyMainproCycleCompleteionCertFormCycleSelElemBtn = By.XPath("//div[contains(@class, 'reportMainProSummaryForm')]//div[text()='Cycle:']/..//button");


        // Tables      

        // Text box



    }
}
