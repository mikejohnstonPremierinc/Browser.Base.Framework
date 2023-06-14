using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    public class DashboardPageBys
    {
        // Buttons
        public readonly By EnterCPDActBtn_OldUI = By.Id("ctl00_ContentPlaceHolder1_lnkAddActivity");
        public readonly By EnterCPDActBtn = By.XPath("//span[text()='ENTER A CPD ACTIVITY']");
        public readonly By NoCycleCloseBtn = By.XPath("//a[text()='Close']");
        public readonly By LearnMoreBtn = By.XPath("//button[text()='Learn More']");
        public readonly By EnterBtn = By.XPath("//a[text()='Enter']");

        // Charts
        // See the following for SVG elements: https://stackoverflow.com/questions/31520642/how-to-use-xpath-in-selenium-webdriver-to-grab-svg-elements
        public readonly By MainproDashboardPLPCompletePercentageLbl = By.XPath("//*[name()='svg']//*[name()='text' and @class='middle value']");
        public readonly By MainproDashboardPLPChartWhiteareaLbl = By.XPath("//*[name()='svg']//*[@class='donut-layer']//*[@style='fill-opacity: 0;']");
        public readonly By MainproDashboardPLPChartGreenareaLbl = By.XPath("//*[name()='svg']//*[@class='donut-layer']//*[@style='fill-opacity: 1;']");

        // Check boxes

        // Labels                                              
        public readonly By AMARCPFormCycleTblRCPAppliedCell = By.XPath("//table[contains(@aria-labelledby, 'CPDCycleMaxCreditActivityGrid')]/descendant::td[contains(text(), 'Royal College')]/../descendant::td[contains(@class, 'Applied')]");
        public readonly By AMARCPFormCycleTblRCPReportedCell = By.XPath("//table[contains(@aria-labelledby, 'CPDCycleMaxCreditActivityGrid')]/descendant::td[contains(text(), 'Royal College')]/../descendant::td[contains(@class, 'Reported')]");
        

        // Links
        public readonly By ClickHereToViewAMARCPCreditsLnk = By.XPath("//span[contains(text(), 'CLICK HERE')]");



        // Menu Items    

        // Radio buttons

        // Select Element

        // Tables 
        public readonly By IncompleteActivitiesTbl = By.XPath("//div[contains(text(), 'INCOMPLETE CPD ACTIVITIES')]/..//table");
        public readonly By IncompleteActivitiesTblHdr = By.XPath("//div[contains(text(), 'INCOMPLETE CPD ACTIVITIES')]/..//table/thead");
        public readonly By IncompleteActivitiesTblBody = By.XPath("//div[contains(text(), 'INCOMPLETE CPD ACTIVITIES')]/..//table/tbody");
        public readonly By IncompleteActivitiesTblBodyFirstRow = By.XPath("//div[contains(text(), 'INCOMPLETE CPD ACTIVITIES')]/..//table/tbody/tr");
        public readonly By IncompleteActivitiesTblFirstRowActivityCell = By.XPath("//div[contains(text(), 'INCOMPLETE CPD ACTIVITIES')]/..//td//span");
        
        public readonly By ActivitiesNeedCreditApprovalTbl = By.XPath("//div[contains(text(), 'ACTIVITIES NEEDING CREDIT APPROVAL')]/..//table");
        public readonly By ActivitiesNeedCreditApprovalTblHdr = By.XPath("//div[contains(text(), 'ACTIVITIES NEEDING CREDIT APPROVAL')]/..//table/thead");
        public readonly By ActivitiesNeedCreditApprovalTblBody = By.XPath("//div[contains(text(), 'ACTIVITIES NEEDING CREDIT APPROVAL')]/..//table/tbody");
        public readonly By ActivitiesNeedCreditApprovalTblBodyFirstRow = By.XPath("//div[contains(text(), 'ACTIVITIES NEEDING CREDIT APPROVAL')]/..//table/tbody/tr");
        public readonly By AtivitiesNeedCreditApprTblFirstRowActivityCell = By.XPath("//div[contains(text(), ''ACTIVITIES NEEDING CREDIT APPROVAL')]/..//td//span");

        public readonly By PersonalLearnPlanTbl = By.XPath("//div[contains(text(), 'PERSONAL LEARNING PLAN')]/..//table");
        public readonly By PersonalLearnPlanTblHdr = By.XPath("//div[contains(text(), 'PERSONAL LEARNING PLAN')]/..//table/thead");
        public readonly By PersonalLearnPlanTblBody = By.XPath("//div[contains(text(), 'PERSONAL LEARNING PLAN')]/..//table/tbody");
        public readonly By PersonalLearnPlanTblBodyFirstRow = By.XPath("//div[contains(text(), 'PERSONAL LEARNING PLAN')]/..//table/tbody/tr");
        public readonly By PersonalLearnPlanTblFirstRowActivityCell = By.XPath("//div[contains(text(), 'PERSONAL LEARNING PLAN')]/..//td//span");


        public readonly By CredSummaryCycleTblCertAppliedLbl = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary')]/descendant::td[contains(@class, 'RequiredAppliedCredits')][1]");
        public readonly By CredSummaryCycleTblTotalAppliedLbl = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary')]/descendant::td[contains(@class, 'RequiredAppliedCredits')][3]");       
        public readonly By CredSummaryCycleTblTotalReqLbl = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary')]/descendant::td[contains(@class, 'RequiredCertifiedCredits')][3]");
        public readonly By CredSummaryCycleTblCertReqMetLbl = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary')]/descendant::td[contains(@class, 'TotalRequirementsMet')][1]");
        public readonly By CredSummaryCycleTblTotalReqMetLbl = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary')]/descendant::td[contains(@class, 'TotalRequirementsMet')][3]");
        public readonly By CredSummaryCycleTblCertReqLbl = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary')]/descendant::td[contains(@class, 'RequiredCertifiedCredits')][1]");
       
        public readonly By CredSummaryCurrYrTblTotalReqMetLbl = By.XPath("//table[contains(@aria-labelledby, 'currentYearSummaryGrid')]/descendant::td[contains(@class, 'TotalRequirementsMet')][3]");
        public readonly By CredSummaryCurrYrTblTotalAppliedLbl = By.XPath("//table[contains(@aria-labelledby, 'currentYearSummaryGrid')]/descendant::td[contains(@class, 'RequiredAppliedCredits')][3]");
        public readonly By CredSummaryCurrYrTblCertReqLbl = By.XPath("//table[contains(@aria-labelledby, 'currentYearSummaryGrid')]/descendant::td[contains(@class, 'RequiredCertifiedCredits')][3]");

        // Tabs

        // Text boxes



    }
}
