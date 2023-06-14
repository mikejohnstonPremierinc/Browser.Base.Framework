using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    public class HoldingAreaPageBys
    {
        // Buttons

        // Charts

        // Check boxes

        // Labels                                              


        // Links


        // Menu Items    

        // Radio buttons


        // Select Element

        // Tables 
        public readonly By SummTabIncompActTbl = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]");
        public readonly By SummTabIncompActTblHdr = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]/thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By SummTabIncompActTblBody = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]/tbody");
        public readonly By SummTabIncompActTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]/tbody/tr");
        public readonly By SummTabIncompActTblActivityColHdr = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]//thead[@class='main-header']//span[@role='button' and text()='Activity']");
        public readonly By SummTabIncompActTblCreditsColHdr = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]//thead[@class='main-header']//span[@role='button' and text()='Credits']");
        public readonly By SummTabIncompActTblLastUpdatedColHdr = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]//thead[@class='main-header']//span[@role='button' and text()='Last Updated']");
        public readonly By SummTabIncompActTblFirstRowDeleteBtn = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]//div[contains(@class, 'glyphicon-trash')]");
        public readonly By SummTabIncompActTblFirstRowActivityCellLnk = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabIncompleteActivitiesGrid')]//tbody//div/span");

        public readonly By SummTabActPendApprTbl = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabPendingApprovalsGrid')]");
        public readonly By SummTabActPendApprTblHdr = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabPendingApprovalsGrid')]/thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By SummTabActPendApprTblBody = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabPendingApprovalsGrid')]/tbody");
        public readonly By SummTabActPendApprTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabPendingApprovalsGrid')]/tbody/tr");
        public readonly By SummTabActPendApprTblActivityColHdr = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabPendingApprovalsGrid')]//thead[@class='main-header']//span[@role='button' and text()='Activity']");
        public readonly By SummTabActPendApprTblCreditsColHdr = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabPendingApprovalsGrid')]//thead[@class='main-header']//span[@role='button' and text()='Credits']");
        public readonly By SummTabActPendApprTblLastUpdatedColHdr = By.XPath("//table[contains(@aria-labelledby, 'SumaryTabPendingApprovalsGrid')]//thead[@class='main-header']//span[@role='button' and text()='Last Updated']");

        public readonly By IncompleteTabActTbl = By.XPath("//table[contains(@aria-labelledby, 'IncompleteTabGrid')]");
        public readonly By IncompleteTabActTblHdr = By.XPath("//table[contains(@aria-labelledby, 'IncompleteTabGrid')]/thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By IncompleteTabActTblBody = By.XPath("//table[contains(@aria-labelledby, 'IncompleteTabGrid')]/tbody");
        public readonly By IncompleteTabActTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'IncompleteTabGrid')]/tbody/tr");
        public readonly By IncompleteTabActTblActivityColHdr = By.XPath("//table[contains(@aria-labelledby, 'IncompleteTabGrid')]//thead[@class='main-header']//span[@role='button' and text()='Activity']");
        public readonly By IncompleteTabActTblCreditsColHdr = By.XPath("//table[contains(@aria-labelledby, 'IncompleteTabGrid')]//thead[@class='main-header']//span[@role='button' and text()='Credits']");
        public readonly By IncompleteTabActTblLastUpdatedColHdr = By.XPath("//table[contains(@aria-labelledby, 'IncompleteTabGrid')]//thead[@class='main-header']//span[@role='button' and text()='Last Updated']");

        public readonly By CredValTabActTbl = By.XPath("//table[contains(@aria-labelledby, 'cpdCreditValidationTabGrid')]");
        public readonly By CredValTabActTblHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdCreditValidationTabGrid')]/thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By CredValTabActTblBody = By.XPath("//table[contains(@aria-labelledby, 'cpdCreditValidationTabGrid')]/tbody");
        public readonly By CredValTabActTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'cpdCreditValidationTabGrid')]/tbody/tr");
        public readonly By CredValTabActTblActivityColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdCreditValidationTabGrid')]//thead[@class='main-header']//span[@role='button' and text()='Activity']");
        public readonly By CredValTabActTblCreditsColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdCreditValidationTabGrid')]//thead[@class='main-header']//span[@role='button' and text()='Credits']");
        public readonly By CredValTabActTblLastUpdatedColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdCreditValidationTabGrid')]//thead[@class='main-header']//span[@role='button' and text()='Last Updated']");


        // Tabs
        public readonly By IncompleteTab = By.XPath("//span[text()='Incomplete']");
        public readonly By CredValTab = By.XPath("//span[text()='Credit Validation']");
        public readonly By SummTab = By.XPath("//span[text()='Summary']");

        // Text boxes

        








    }
}
