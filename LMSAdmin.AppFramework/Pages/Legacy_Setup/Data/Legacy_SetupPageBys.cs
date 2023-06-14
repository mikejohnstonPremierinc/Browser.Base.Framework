using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class Legacy_SetupPageBys
    {

        // Buttons
        public readonly By AccreditationTypeSaveChangesBtn = By.Id("ctl00_MainContent_SubmitBtn");

        // Charts

        // Check boxes

        // Labels                                              
        public readonly By OrganizationSettingsLbl = By.XPath("//div[text()='Organization Settings']");


        // Links

        public readonly By AccreditationTypesLnk = By.LinkText("Accreditation Types");
        public readonly By AddAccreditationTypeLnk = By.XPath("//a/span[text()='Add Accreditation Type']");


        // Menu Items    

        // Radio buttons

        // Tables       
        public readonly By AccreditationTypesTbl = By.ClassName("ccTableAL");

        // Tabs

        // Text boxes
        public readonly By AccreditationTypeNameTxt = By.XPath("//iframe[@id='ctl00_MainContent_reName_contentIframe']");



    }
}