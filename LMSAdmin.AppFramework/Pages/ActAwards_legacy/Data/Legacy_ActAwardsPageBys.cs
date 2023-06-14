using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class Legacy_ActAwardsPageBys
    {

        // Buttons
        public readonly By AddAwardSaveAndContBtn = By.Id("ctl00_button_bot_customize");
        public readonly By ScenarioSaveBtn = By.Id("ctl00_button_bot_save");
        public readonly By TemplateSaveBtn = By.Id("ctl00_btnSave");

        

        // Charts

        // Check boxes

        // Frames


        // General


        // Labels                                                   
        public readonly By ChangesSavedLbl = By.XPath("//span[text()='Changes Saved']");

        
        // Links
        public readonly By AddAwardLnk = By.XPath("//a[contains(text(), 'Add Award')]");
        public readonly By BackToAwardsLnk = By.XPath("//a[text()='Back to Awards']");

        

        // Menu Items    

        // Radio buttons

        // select elements
        public readonly By SelectTypeSelElem = By.Id("ctl00_dropdown_AwardType");
        public readonly By SelectEmailTempSelElem = By.Id("ctl00_MessageTemplateList");
        public readonly By SelectTempLibrarySelElem = By.Id("ctl00_dropdown_TemplateLibraries");


        // Tables   
        public readonly By AwardsTbl = By.XPath("//span[text()='Award(s) for this Activity ']/ancestor::table[@class='ccTableAL']");
        public readonly By AwardsTblBody = By.XPath("//span[text()='Award(s) for this Activity ']/ancestor::table[@class='ccTableAL']/tbody");
        public readonly By AwardsTblFirstRow = By.XPath("//span[text()='Award(s) for this Activity ']/ancestor::table[@class='ccTableAL']/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");
        public readonly By TemplatesTbl = By.XPath("//div[@id='ctl00_panel_TemplateList']/table");
        public readonly By TemplatesTblBody = By.XPath("//div[@id='ctl00_panel_TemplateList']/table/tbody");
        public readonly By TemplatesTblFirstRow = By.XPath("//div[@id='ctl00_panel_TemplateList']/table/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");
        public readonly By ScenariosTbl = By.XPath("//span[contains(., 'Who')]/following-sibling::table");
        public readonly By ScenariosTblBody = By.XPath("//span[contains(., 'Who')]/following-sibling::table/tbody");
        public readonly By ScenariosTblFirstRow = By.XPath("//span[contains(., 'Who')]/following-sibling::table/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");

        

        // Tabs

        // Text boxes        
        public readonly By AwardNameTxt = By.Id("ctl00_textbox_AwardName");



    }
}