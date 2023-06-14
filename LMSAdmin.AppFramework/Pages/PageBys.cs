using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    public class PageBys
    {
        // Banners
        public readonly By LoadingOverlayElement = By.XPath("//div[@class='loading overlay']");

        // Buttons
        public readonly By SearchBtn = By.Id("Search1_SearchSubmit");
        public readonly By AcceptBtn = By.Name("accept");
        public readonly By BackToActivityBtn = By.XPath("//button[contains(@class,'activity-return-button')]");
        public readonly By confirmMsgPopupOkBtn = By.XPath("//button[contains(@class,'confirm-button')]");

        // Charts

        // Check boxes

        // Frames

        // General
        public readonly By AlertNotificationIcon = By.XPath("//div[@role='alertdialog']/i");
        public readonly By AlertNotificationIconMsg = By.XPath("//div[@role='alertdialog']/span");
        public readonly By InformationNotificationMsg = By.XPath("//div[contains(@class,'notification')]/span");
        public readonly By ConfirmtationPopUpMsg = By.XPath("//div[@data-bind-value='confirmationMessage']");

        // Labels
        public readonly By Steps_CompletionPathwayLbl = By.XPath("//div[@class='admin-progress-steps']/div[@aria-label='Completion Pathway']");
        public readonly By Steps_AccreditationLbl = By.XPath("//div[@class='admin-progress-steps']/div[@aria-label='Accreditation']");
        public readonly By Steps_AwardLbl = By.XPath("//div[@class='admin-progress-steps']/div[@aria-label='Awards']");
        public readonly By ActivityStageLbl = By.XPath("//div[@class='admin-activity-stage']/span");



        // Links
        public readonly By TermsAndConditionsLnk = By.XPath("//a[@class='Normal']");        
        public readonly By TreeLinks_Activities = By.XPath("//span[contains(@class, 'TreeNode') and text()='Activities']");
        public readonly By TreeLinks_MainActivity = By.XPath("//div[@id='MyTree_t0_t0_t0']/img[4]");
        public readonly By TreeLinks_Accreditation = By.XPath("//span[contains(@class, 'TreeNode') and text()='Accreditations']");
        public readonly By TreeLinks_Legacy_Accreditation = By.XPath("//span[contains(@class, 'TreeNode') and text()='Accreditations']");
        public readonly By TreeLinks_Assessments = By.XPath("//span[contains(@class, 'TreeNode') and text()='Assessments']");
        public readonly By TreeLinks_FrontMatter = By.XPath("//span[contains(@class, 'TreeNode') and text()='Front Matter']");
        public readonly By TreeLinks_Awards = By.XPath("//span[contains(@class, 'TreeNode') and text()='Awards']");

        public readonly By SetupLnk = By.XPath("//a[@title='Setup']");
        public readonly By TreeLinks_Content = By.XPath("//span[contains(@class, 'TreeNode') and text()='Content']");
        // Loading
        public readonly By LoadIcon = By.XPath("//div[@class='loading-indicator']");

        // public readonly By LogoutLnk = By.LinkText("Log Out");
        public readonly By LogoutLnk = By.XPath("//a[@title='Logoff']");

        //Menu Items    


        // Radio buttons

        // Tables       
        public readonly By RecentItemsTbl = By.Id("RecentItems1_RecentItemPanel");

        

        // Tabs
        public readonly By HomeTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl00_ParentImageTab");
        public readonly By StakeholdersTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl01_ParentImageTab");
        public readonly By PlanningTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl02_ParentImageTab");
        public readonly By ProjectsTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl03_ParentImageTab");
        public readonly By DistributionTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl04_ParentImageTab");
        public readonly By ParticpantsTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl05_ParentImageTab");
        public readonly By ProcessingTab = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl06_ParentImageTab");
        public readonly By Outcomes = By.Id("PortalTabb_PortalTabs_ParentImageTabRptr_ctl07_ParentImageTab");
        public readonly By SetUpTab = By.Id("//td/a[text()='Setup']");
        



        // Text boxes
        public readonly By SearchTxt = By.Id("Search1_TextBox_SearchField");


        












    }
}