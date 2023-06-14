using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class CPDActivitiesListPageBys
    {

        // Buttons

        // Charts

        // Check boxes

        // Labels

        // Links

        // Menu Items    

        // Radio buttons

        // Select Elements
        public readonly By ActTblActivityDateSelElem = By.XPath("//span[text()='Activity Date:']/..//select");
        public readonly By ActTblActivityDateSelElemBtn = By.XPath("//span[text()='Activity Date:']/..//button");
        public readonly By ActTblCycleSelElem = By.XPath("//span[text()='Cycle:']/..//select");
        public readonly By ActTblCycleSelElemBtn = By.XPath("//span[text()='Cycle:']/..//button");
        public readonly By ActTblStatusSelElem = By.XPath("//span[text()='Activity Status:']/..//select");
        public readonly By ActTblStatusSelElemBtn = By.XPath("//span[text()='Activity Status:']/..//button");
        public readonly By ActTblPerformanceGoalSelElem = By.XPath("//span[text()='Performance Goal:']/..//select");
        public readonly By ActTblPerformanceGoalSelElemBtn = By.XPath("//span[text()='Performance Goal:']/..//button");

        // Tables       
        public readonly By ActTbl = By.XPath("//table[contains(@aria-labelledby, 'ActivitiesListGrid')]");
        public readonly By ActTblHdr = By.XPath("//table[contains(@aria-labelledby, 'ActivitiesListGrid')]/thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By ActTblBody = By.XPath("//table[contains(@aria-labelledby, 'ActivitiesListGrid')]/tbody");
        public readonly By ActTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'ActivitiesListGrid')]/tbody/tr");
        public readonly By ActTblNoCPDActivitiesLbl = By.XPath("//td[text()='No CPD activities have been created']");
        
        public readonly By ActTblActivityColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdActivitiesListGrid')]//thead[@class='main-header']//span[text()='Activity']");
        public readonly By ActTblCreditsReportedColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdActivitiesListGrid')]//thead[@class='main-header']//span[text()='Credits Reported']");
        public readonly By ActTblCreditsAppliedColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdActivitiesListGrid')]//thead[@class='main-header']//span[text()='Credits Applied']");
        public readonly By ActTblLastUpdatedColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdActivitiesListGrid')]//thead[@class='main-header']//span[text()='Last Updated']");
        public readonly By ActTblActivityDateColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdActivitiesListGrid')]//thead[@class='main-header']//span[text()='Activity Date']");

        // Tabs

        // Text boxes



    }
}
