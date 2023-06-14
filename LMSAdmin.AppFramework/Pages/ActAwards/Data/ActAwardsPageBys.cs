using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActAwardsPageBys
    {

        // Buttons
        public readonly By AddAwardBtn = By.XPath("//div[@title='+ ADD AWARD']");
        public readonly By SaveAwardBtn = By.XPath("//div[@title='SAVE AWARD']");
        public readonly By TemplatePreviewFormCloseBtn = By.XPath("//div[contains(@class,'modal-window previewAwardTemplate')]//div[@aria-label='Close']");
        public readonly By ViewHtmlFormCloseBtn = By.XPath("//button[contains(@class,'close')]");
        public readonly By CloseAwardBtn = By.XPath("//div[contains(@class,'button')][@title='CLOSE']");
        public readonly By DeleteAwardBtn = By.XPath("//button[@aria-label='click to delete']");


        // Charts

        // Check boxes

        // Frames
        public readonly By TemplatePreviewFrame = By.XPath("//div[contains(@class,'modal-window previewAwardTemplate')]");

        // General        
        public readonly By TemplatePreviewFormImg = By.XPath("//div[contains(@class,'previewAwardTemplateModelForm')]//img");
        public readonly By CustomiseAwardImg = By.XPath("//title[contains(text(),'Editor')]/ancestor::html/body//img");        
        public readonly By OvrlayPg = By.XPath("//div[@class='overlay']");

        // Labels                                                   
        public readonly By AwardsTitleLbl = By.XPath("//div/p[@class='title-bar' and text()='Awards']");
        public readonly By AddAwardTitleLbl = By.XPath("//div[@class='awardTitle' and text()='ADD AWARD']");
        public readonly By EditAwardTitleLbl = By.XPath("//div[@class='awardTitle' and text()='EDIT AWARD']");
        public readonly By ViewAwardTitleLbl = By.XPath("//div[@class='awardTitle' and text()='VIEW AWARD']");
        public readonly By ViewHTMLFormTitleLbl = By.XPath("//div[contains(@class,'viewhtml')]");
        public readonly By TemplatePreviewFormTitleLbl = By.XPath("//div[@class='modal-title'][text()='Preview']");

        // Links

        public readonly By BackToAwardsLnk = By.XPath("//a[text()='Back to Awards']");
        public readonly By CustomiseAwardHtmlLnk = By.XPath("//a[@title='View HTML']");


        // Menu Items    

        // Radio buttons
        public readonly By PortraitTypeRdoBtn = By.XPath("//input[@name='awardOrientation'][@value=0]");
        public readonly By LandscapeTypeRdoBtn = By.XPath("//input[@name='awardOrientation'][@value=1]");

        // select elements
        public readonly By SelectTypeSelElem = By.Id("ctl00_dropdown_AwardType");
        public readonly By SelectEmailTempSelElem = By.Id("ctl00_MessageTemplateList");
        public readonly By SelectTempLibrarySelElem = By.Id("ctl00_dropdown_TemplateLibraries");


        // Tables   
        public readonly By AwardsTbl = By.XPath("//div[contains(@class,'awardsData')]//table");
        public readonly By AwardsTblBody = By.XPath("//div[contains(@class,'awardsData')]//table/tbody");
        public readonly By AwardsTblFirstRow = By.XPath("//div[contains(@class,'awardsData')]//table/tbody/tr");
       
        public readonly By TemplatesTbl = By.XPath("//div[contains(@class,'awardDetailsGridTable')]//table");
        public readonly By TemplatesTblBody = By.XPath("//div[contains(@class,'awardDetailsGridTable')]//table/tbody");
        public readonly By TemplatesTblFirstRow = By.XPath("//div[contains(@class,'awardDetailsGridTable')]//table/tbody/tr");

        public readonly By ScenariosTbl = By.XPath("//div[contains(@class,'awardScenarios')]//table");
        public readonly By ScenariosTblBody = By.XPath("//div[contains(@class,'awardScenarios')]//table/tbody");
        public readonly By ScenariosTblFirstRow = By.XPath("//div[contains(@class,'awardScenarios')]//table/tbody/tr");

        

        // Tabs

        // Text boxes        
        public readonly By AwardNameTxt = By.Name("awardName");
        public readonly By TemplateNameTxt = By.XPath("//input[@placeholder='Template Name']");
        public readonly By ViewHtmlTextAreaTxt = By.XPath("//div[contains(@class,'viewhtml')]/textarea");


    }
}