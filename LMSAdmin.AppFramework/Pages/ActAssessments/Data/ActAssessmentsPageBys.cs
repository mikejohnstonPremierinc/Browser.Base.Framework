using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActAssessmentsPageBys
    {

        // Buttons
        public readonly By ContinueBtn = By.Id("ctl00_Submit");
        public readonly By ComplPathTabSaveBtn = By.Id("ctl00_button_bot_save");
        public readonly By AssSearchBtn = By.Id("ctl00_SearchBtn");
        

        // Charts

        // Check boxes


        // General


        // Labels                                                   


        // Links
        public readonly By AddNewAssessmentLnk = By.XPath("//strong[contains(text(), 'Add New Assessment')]");
        public readonly By ComplPathTabBackToComplPathwaySumLnk = By.XPath("//a[contains(., 'Back to Completion Pathway Summary')]");
        


        // Menu Items    

        // Radio buttons

        // select elements

        // Tables   
        public readonly By ComplPathTabScenarioTbl = By.XPath("//font[text()='Completion Pathway']/ancestor::table[@class='ccTableAL']");
        public readonly By ComplPathTabScenarioTblBody = By.XPath("//font[text()='Completion Pathway']/ancestor::table[@class='ccTableAL']/tbody");
        public readonly By ComplPathTabScenarioTblBodyRow = By.XPath("//font[text()='Completion Pathway']/ancestor::table[@class='ccTableAL']/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");
        public readonly By ComplPathTabAssessmentsTbl = By.XPath("//span[contains(., 'Who and under')]/following-sibling::table/descendant::table");
        public readonly By ComplPathTabAssessmentsTblBody = By.XPath("//span[contains(., 'Who and under')]/following-sibling::table/descendant::table/tbody");
        public readonly By ComplPathTabAssessmentsTblBodyRow = By.XPath("//span[contains(., 'Who and under')]/following-sibling::table/descendant::table/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");
        public readonly By SearchAssTbl = By.ClassName("ccTableAL");
        public readonly By SearchAssTblBodyRow = By.XPath("//table[@class='ccTableAL']/tbody/tr[@class='ccTableRow']");


        // Tabs
        public readonly By ComplPathTab = By.XPath("//a[contains(text(), 'Completion Pathway')]");
        public readonly By AssessmentsTab = By.XPath("//td/a[contains(text(), 'Assessments')]");


        // Text boxes
        public readonly By AssTemplateSearchTxt = By.Id("ctl00_KeywordStr");






    }
}