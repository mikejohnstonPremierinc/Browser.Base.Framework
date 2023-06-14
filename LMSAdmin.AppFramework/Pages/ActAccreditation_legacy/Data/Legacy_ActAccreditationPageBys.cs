using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class Legacy_ActAccreditationPageBys
    {

        // Buttons
        public readonly By AvailableAccreditationTypesContinueBtn = By.Id("ctl00_ContinueButton");
        public readonly By AddScenarioSaveBtn = By.Id("ctl00_BottomSaveButton");
        public readonly By SelectProfAddBtn = By.Id("ctl00_ProfessionSelection_AddItems");



        // Charts

        // Check boxes


        // General


        // Labels                                                   


        // Links
        public readonly By AddAccreditationTypeLnk = By.XPath("//a[contains(text(), 'Add Accreditation Type')]");
        public readonly By AddScenarioLnk = By.XPath("//a[contains(text(), 'Add Scenario')]");
        public readonly By BackToAccreditationsLnk = By.XPath("//a[contains(text(), 'Back to Accreditations')]");

        

        // Menu Items    

        // Radio buttons

        // select elements
        public readonly By SelectProfAvailSelElem = By.Id("ctl00_ProfessionSelection_AvailableItems");


        // Tables   
        public readonly By AvailableAccreditationTypesTbl = By.XPath("//div[@id='ctl00_Panel_NonAccrRow']/../table[2]");
        public readonly By AvailableAccreditationTypesTblFirstRow = By.XPath("//div[@id='ctl00_Panel_NonAccrRow']/../table[2]/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");

        public readonly By ScenariosTbl = By.XPath("//span[text()='Accreditation Scenarios']/../table/descendant::table[3]");
        
        // Tabs

        // Text boxes
        public readonly By ScenarioNameTxt = By.Id("ctl00_TextBox_ScenarioName");
        public readonly By FixedCreditsTxt = By.Id("ctl00_TextBox_UnitTop");


        

            
            
    }
}