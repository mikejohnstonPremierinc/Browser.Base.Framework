using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class LMSPageBys
    {
        // Banners
        public readonly By Banner = By.XPath("//div[@class='main-menu-nav-bar toggle-button-bar']");

        // Buttons    
        public readonly By SearchBtn = By.XPath("//button[@type='submit']");
        public readonly By TermsOfServiceFormIAcceptBtn = By.XPath("//input[@value='I accept']");
        public readonly By VerifyYourProfessionFormSubmitBtn = By.XPath("//input[contains(@id, 'SiteHeader_UAMSProfessionVerify_btnProfessionSave')]");
        public readonly By SearchTypeBtn = By.XPath("//div[@class='fireball-widget searchForm inline-block']//button[@class='btn dropdown-toggle btn-default']");
        public readonly By Mobile_SearchMagnifyingGlassBtn = By.ClassName("glyphicon-search");
        public readonly By Menu_UserProfile_SignOutBtn = By.XPath("//a[@href='services/logout']");
        public readonly By Menu_UserProfile_DropDownBtn = By.XPath("//div[@class='main-menu-button']");
        public readonly By Menu_UserProfile_AllReceiptsLnk = By.XPath("//a[text()='All Receipts']");




        // Charts

        // Check boxes

        // Frames

        // General      
        public readonly By UserInfoScript = By.XPath("//script[contains(.,'userName')]"); // Script element containing information like username, etc.
        public readonly By ClientLogo = By.XPath("//img[contains(@src, 'logo.png')]");
        public readonly By ClientTitle = By.XPath("//h2[@class='app-title-text']"); 
        public readonly By NotificationErrorIcon = By.XPath("//div[contains(@class, 'notification-error')]//i[@class='notification-icon']");
        public readonly By NotificationWarnIcon = By.XPath("//div[contains(@class, 'notification-warn')]//i[@class='notification-icon']");
        public readonly By CustomPageHTMLComponentGroups = By.XPath("//div[contains(@class, 'group') and contains(@class, 'widget') and not(contains(@class,'group0'))]");
        public readonly By CustomPageHTMLComponentNonGroups = By.XPath("//div[contains(@class, 'group') and contains(@class, 'widget') and contains(@class,'group0')]");

        // Labels   
        public readonly By NotificationErrorMessageLbl = By.XPath("//div[contains(@class, 'notification-error')]//span[@class='notification-message']");
        public readonly By NotificationErrorMessageLblXBtn = By.XPath("//div[contains(@class, 'notification-close')]");
        public readonly By NotificationWarnMessageLbl = By.XPath("//div[contains(@class, 'notification-warn')]//span[@class='notification-message']");
        public readonly By NotificationInfoMessageLbl = By.XPath("//div[contains(@class, 'notification-info')]//span[@class='notification-message']");
        public readonly By NotificationInfoMessageLblXBtn = By.XPath("//span[contains(text(), 'Successfully added')]/following-sibling::div");
        public readonly By FullNameLbl = By.XPath("//span[@class='user-name']");
        public readonly By ActivityTitleLbl = By.XPath("//div[@class='view-title']//h2");
        public readonly By Footer_CopyrightLbl = By.XPath("//*[contains(text(), 'Premier, Inc.')]");
        public readonly By Footer_TermsOfUseLbl = By.XPath("//a[text()='Terms of Use']");
        public readonly By Footer_PrivacyPolicyLbl = By.XPath("//a[text()='Privacy Policy']");
        public readonly By Footer_SupportLbl = By.XPath("//a[text()='Support']");
        public readonly By Footer_AboutThisSiteLbl = By.XPath("//a[text()='About this Site']");
        public readonly By Footer_LegalNoticesLbl = By.XPath("//a[text()='Legal Notices']");
        public readonly By NotificationPageNoDataErrorLbl = By.XPath("//span[contains(text(), 'retrieve your information')]");
        public readonly By WereSorryErrorLbl = By.XPath("//span[contains(text(), 'retrieve your information')]");


        // Loading
        public readonly By LoadIcon = By.XPath("//div[@class='loading-indicator']");

        // Links   
        public readonly By ContactInformationLnk = By.XPath("//a[.='Contact Information']");
        public readonly By ChangePasswordLnk = By.XPath("//a[.='Change Password']");
        public readonly By ChangeSecurityQuestionLnk = By.XPath("//a[.='Change Security Question']");
        // Second xpath is for AHA
        public readonly By LoginLnk = By.XPath("//a[@target='_self']//span[text()='Login'] | //td[contains(@class, 'LoginButton')]/input[@value='Login']");
        public readonly By RegisterLnk = By.XPath("//a[@target='_self']//span[text()='Register']");


        // Menu Items    

        // Radio buttons


        // Select Elements
        public readonly By VerifyYourProfessionFormProfessionSelElem = By.Id("ctl00_ctl00_ctl00_ContentPlaceHolderBase1_ResponsiveHeader_UAMSProfessionVerify_ddlProfessions");
        public readonly By SearchTypeSelElem = By.XPath("//div[@class='title-bar-widgets']//select");


        // Text boxes    
        public readonly By SearchTxt = By.XPath("//div[@class='form-input']/input[@name='search']");
        public readonly By LocationTxt = By.XPath("//input[@placeholder='Location']");

        
        // Tabs
        public readonly By HomeTab = By.XPath("//span[text()='Home']");
        public readonly By CatalogTab = By.XPath("//span[text()='Catalog']");
        public readonly By ActivitiesInProgressTab = By.XPath("//span[contains(text(), 'Activities in Progress')]");
        public readonly By TranscriptTab = By.XPath("//span[text()='Transcript']");
        public readonly By CERequestTab = By.XPath("//span[text()='CE Request']");
        public readonly By BreastfeedingTab = By.XPath("//span[text()='Breastfeeding']");
        public readonly By EducationalEventsTab = By.XPath("//span[text()='Educational Events']");
        public readonly By AngelsLiveTab = By.XPath("//span[text()='ANGELS LIVE']");
        public readonly By TeleconferenceTab = By.XPath("//span[text()='Teleconference']");
        public readonly By DepartmentsTab = By.XPath("//span[text()='Departments']");
        public readonly By ActivitiesView_ActivityOverviewTab = By.XPath("//div[@class='progress-step']/div[contains(text(), 'Overview')]|//div[@class='progress-step']/div/span[contains(text(), 'Overview')]");
        public readonly By ActivitiesView_PreAssessmentTab = By.XPath("//div[@class='progress-step']/div[contains(text(), 'Overview')]|//div[@class='progress-step']/div/span[contains(text(), 'Pre-Assessment')]");
        public readonly By ActivitiesView_OnHoldTab = By.XPath("//div[@class='progress-step']/div[contains(text(), 'On Hold')]");

        //public readonly By ActivitiesView_ActivityMaterialTab = By.XPath("//div[@class='fireball-widget activityProgressBar widget-inline']//div[contains(text(), 'Activity Material')]");
        //public readonly By ActivitiesView_PreAssessmentTab = By.XPath("//div[@class='fireball-widget activityProgressBar widget-inline']//div[contains(text(), 'Pre-Assessment')]");
        //public readonly By ActivitiesView_PostAssessmentTab = By.XPath("//div[@class='fireball-widget activityProgressBar widget-inline']//div[contains(text(), 'Post-Assessment')]");
        //public readonly By ActivitiesView_EvaluationTab = By.XPath("//div[@class='fireball-widget activityProgressBar widget-inline']//div[contains(text(), 'Evaluation')]");
        //public readonly By ActivitiesView_CertificateTab = By.XPath("//div[@class='fireball-widget activityProgressBar widget-inline']//div[contains(text(), 'Certificate')]");


        // Text boxes














    }
}