using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActFrontMatterPageBys
    {

        // Buttons
        public readonly By FrontMatterScenarioSaveBtn = By.Id("ctl00_button_bot_save");
        public readonly By FrontMatterDetailSaveBtn = By.Id("ctl00_Button_Save");

        

        // Charts

        // Check boxes

        // Frames
        public readonly By FrontMatterFrame = By.Id("ctl00_RadEditor1_contentIframe");
        // General


        // Labels                                                   


        // Links
        public readonly By AddFrontMatterLnk = By.XPath("//a[contains(text(), 'Add Front Matter')]");

        

        // Menu Items    

        // Radio buttons

        // select elements


        // Tables   
        public readonly By FrontMatterTbl = By.XPath("//span[text()='Front Matter for this Activity ']/ancestor::table[@class='ccTableAL']");
        public readonly By FrontMatterTblBody = By.XPath("//span[text()='Front Matter for this Activity ']/ancestor::table[@class='ccTableAL']/tbody");
        public readonly By FrontMatterTblFirstRow = By.XPath("//span[text()='Front Matter for this Activity ']/ancestor::table[@class='ccTableAL']/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");
        public readonly By ScenarioTbl = By.XPath("//span[contains(., 'Who and under what conditions should a participant see this front matte')]/following-sibling::table[@class='ccTableAL']");
        public readonly By ScenarioTblBody = By.XPath("//span[contains(., 'Who and under what conditions should a participant see this front matte')]/following-sibling::table[@class='ccTableAL']/tbody");
        public readonly By ScenarioTblFirstRow = By.XPath("//span[contains(., 'Who and under what conditions should a participant see this front matte')]/following-sibling::table[@class='ccTableAL']/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");

        
        // Tabs

        // Text boxes
        public readonly By NameTxt = By.Id("ctl00_txtDisclaimerName");


        


    }
}