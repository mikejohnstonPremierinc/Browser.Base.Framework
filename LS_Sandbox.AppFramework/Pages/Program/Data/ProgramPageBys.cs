using OpenQA.Selenium;

namespace LS.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class ProgramPageBys
    {
        // Buttons
        public readonly By CreditValidationSubmitBtn = By.XPath("//input[@value='Submit Validation']");
        public readonly By ProgAdjustTabAddAdjustFormAddAdjustBtn = By.XPath("//input[@value='Add Adjustment']");
        public readonly By ReevaluateBtn = By.XPath("//input[@value='Reevaluate']");
        public readonly By ProgAdjustTabAddAdjustFormIsIntnlYesRdo = By.Id("True_IsInternational");
        public readonly By ProgAdjustTabAddAdjustFormIsIntnlNoRdo = By.Id("False_IsInternational");
        public readonly By ProgAdjustTabAddAdjustFormLeaveStartDtTxt = By.Id("Attribute_LeaveStartDate");
        public readonly By ProgAdjustTabAddAdjustFormLeaveEndDtTxt = By.Id("Attribute_LeaveEndDate");
        public readonly By ProgAdjustTabAddAdjustFormLeaveCodeSelElem = By.Id("Attribute_LeaveCode");
        public readonly By ProgAdjustTabAddAdjustFormIsVoluntYesRdo = By.Id("True_IsVoluntary");
        public readonly By ProgAdjustTabAddAdjustFormIsVoluntNoRdo = By.Id("False_IsVoluntary");
        public readonly By ProgAdjustTabAddAdjustFormEffectiveDtTxt = By.Id("Attribute_EffectiveDate");
        public readonly By ApplyCarryOverCreditsBtn = By.XPath("//input[@id='ApplyRecognitionCOC']");


        // Charts

        // Check boxes


        // forms
        public readonly By ProgAdjustTabAddAdjustForm = By.Id("adjustmenteditor");


        // Labels         
        public readonly By DetailsTabNameValueLbl = By.XPath("//td[contains(text(),'Name:')]/following-sibling::td[1]");
        public readonly By DetailsTabStatusValueLbl = By.XPath("//td[contains(text(),'Status:')]/following-sibling::td[1]");
        public readonly By DetailsTabStartsValueLbl = By.XPath("//td[contains(text(),'Starts:')]/following-sibling::td[1]");
        public readonly By DetailsTabEndsValueLbl = By.XPath("//td[contains(text(),'Ends:')]/following-sibling::td[1]");
        public readonly By DetailsTabProgramValueLbl = By.XPath("//td[contains(text(),'Program:')]/following-sibling::td[1]");
        public readonly By DetailsTabCreditsValueLbl = By.XPath("//td[contains(text(),'Credits Applied:')]/following-sibling::td[1]");
        public readonly By RecognitionCarryOverGreenTextLbl = By.XPath("//div[@id='messages']/descendant::li[contains(.,'Recognition carry over')]");



        // Links
        public readonly By ReevaluateLnk = By.XPath("//a[text()='Reevaluate']");
        public readonly By ProgAdjustTabAddAdjustLnk = By.Id("addAdjustment");
        public readonly By ApplyRecognitionCOCLnk = By.XPath("//a[text()='Apply Recognition COC']");


        // Menu Items    


        // Radio buttons
        public readonly By CreditValidationAcceptRdo = By.Id("Accept");
        public readonly By CreditValidationRejectRdo = By.Id("Reject");
        public readonly By NeedsMoreInformationRdo = By.Id("HoldingArea");

        // select elements       
        public readonly By SelfReportActTabValidStatusSelElem = By.Id("validationFilter");
        public readonly By ProgAdjustTabAddAdjustFormAdjustCodeSelElem = By.XPath("//select[@id='LeaveCode']");
        public readonly By ProgAdjustTabAddAdjustFormAdjustCycleSelElem = By.XPath("//select[@id='group_customdate']");

        
        // Tables   
        public readonly By SelfReportActTabActivityTbl = By.XPath("//table[@id='externalActivitiesRepeater']");
        public readonly By SelfReportActTabActivityTblBody = By.XPath("//table[@id='externalActivitiesRepeater']/tbody");
        public readonly By SelfReportActTabActivityTblBodyRow = By.XPath("//table[@id='externalActivitiesRepeater']/tbody/tr"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load 
        public readonly By ProgramAdjustmentsActivityTbl = By.XPath("//table[@id='adjustmentRepeater']");
        public readonly By ProgramAdjustmentsActivityTblBody = By.XPath("//table[@id='adjustmentRepeater']/tbody");
        public readonly By ProgramAdjustmentsActivityTblBodyRow = By.XPath("//table[@id='adjustmentRepeater']/tbody/tr[1]"); // If one row exists in this table, then this will be that row. This is used to wait for the table to load



        // Tabs
        public readonly By SelfReportActTab = By.XPath("//a[text()='Self-Reported Activities']");
        public readonly By ProgramAdjustmentsTab = By.XPath("//a[text()='Program Adjustments']");
        public readonly By DetailsTab = By.XPath("//a[text()='Details']");


        // Text boxes
        public readonly By ProgAdjustTabAddAdjustFormAdjustCycleDateTxt = By.Id("Attribute_CycleSetEndDate");
        public readonly By CommentTxt = By.XPath("//textarea");

        
    }
}