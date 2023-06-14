using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActContentPageBys
    {
        // Buttons
        public readonly By AddContentBtn = By.XPath("//div[@title='+ ADD CONTENT']");
        public readonly By BackToActivityBtn = By.XPath("//button[contains(@class,'activity-return-button')]");
        public readonly By AddContentFormAddBtn = By.XPath("//button[@aria-label='ADD']");       
        public readonly By ContentSaveBtn = By.XPath("//div[@title='SAVE']");
        public readonly By confirmMsgPopupOkBtn = By.XPath("//button[contains(@class,'confirm-button')]");
        public readonly By ContentDeleteBtn = By.XPath("//span[@class='glyphicon glyphicon-trash']");
        public readonly By ViewContentFormCloseBtn = By.XPath("//div[contains(@class,'viewContent')]/div[@aria-label='Close']");
        public readonly By ContentPreviewFormCloseBtn = By.XPath("//div[contains(@class,'modal-window contentViewer')]//div[@aria-label='Close']");
        public readonly By EditContentFormSaveBtn = By.XPath("//div[contains(@class,'editContent')]//button[@aria-label='SAVE']");

        // Charts
        // Check boxes
        public readonly By ContentRequiredEnabledChkbox = By.XPath("//input[@type='checkbox'][@name='required']");
        // Frames
        // Text boxes
        public readonly By AddContentFormDisplayNameTxt = By.XPath("//div[contains(@class,'addContent')]//input[@aria-label='DISPLAY NAME']");
        public readonly By AddContentFormDescriptionTxt = By.XPath("//div[contains(@class,'addContent')]//textarea[@aria-label='DESCRIPTION']");
        public readonly By AddContentFormEnterURLTxt = By.XPath("//div[contains(@class,'addContent')]//input[@placeholder='Enter URL here']");
        public readonly By ContentRequiredInstructionsTxt = By.Name("instructions");
        public readonly By EditContentFormDisplayNameTxt = By.XPath("//div[contains(@class,'editContent')]//input[@aria-label='DISPLAY NAME']");
        public readonly By ViewContentFormEnterURLTxt = By.XPath("//input[@name='contentUrl']");

        // General
        public readonly By ContentLoadingImage = By.XPath("//*[@class='loading-indicator']");
        public readonly By ContentFirstrow = By.XPath("//*[@class='grid-table']/tbody/tr[1]");
        // Labels                                                   
        public readonly By ContentTitleLbl = By.XPath("//div[contains(@class,'contentTitle')]");
        public readonly By ContentLbl = By.XPath("//div[@class='admin-progress-steps']/div[@aria-label='Content']");
        public readonly By ViewContentTitleLbl = By.XPath("//div[@class='modal-title'][text()='View Content']");
        public readonly By EditContentTitleLbl = By.XPath("//div[@class='modal-title'][text()='Edit Content']");

        // Links
        //public readonly By BrowseToUploadLnk = By.XPath("//span[text()='Browse to upload.']");
        public readonly By BrowseToUploadLnk = By.Name("scormContentName");
        public readonly By BrowseToUploadFileLnk = By.Name("contentName");
        public readonly By ContentFileDownloadLnk = By.XPath("//div[contains(@id,'uploadedFile')]/a");
        // Menu Items    

        // Radio buttons

        // select elements
        public readonly By ContentTypeSelElemBtn = By.XPath("//div[text()='CONTENT TYPE']/following-sibling::div//button[contains(@aria-labelledby,'contentType')]");
        public readonly By ContentTypeSelElemOptionsDropdown = By.XPath("//button[contains(@aria-labelledby,'contentType')]/following-sibling::select");
        public readonly By ContentRequiredSelElemBtn= By.XPath("//button[contains(@aria-labelledby,'required')]");

        // Tables   
        public readonly By ContentsTbl = By.XPath("//div[contains(@class,'contentData')]//table");
        public readonly By ContentTblContentNameColumn = By.XPath("//td[contains(@class,'column-name')]");
        public readonly By ContentsTblBody = By.XPath("//div[contains(@class,'contentData')]//table/tbody");
        public readonly By ContentsTblFirstRow = By.XPath("//div[contains(@class,'contentData')]//table/tbody/tr");

        // Tabs
        // Text boxes        
    }
}