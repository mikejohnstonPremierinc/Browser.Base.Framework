using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on every single page of the RCP Application
    /// </summary>
    public class MainproPageBys
    {
        // Buttons
        public readonly By DeleteFormYesBtn = By.XPath("//div[contains(@class, 'DeleteConfirmButton')]//span[text()='YES']");
        public readonly By DeleteFormNoBtn = By.XPath("//div[contains(@class, 'DeleteConfirmButton')]//span[text()='NO']");
        public readonly By NotificationFormXBtn = By.XPath("//div[@data-attach-point='dismissContainer' and @aria-label='Close']");
        public readonly By EnterCPDActBtn = By.XPath("//span[text()='ENTER A CPD ACTIVITY']");
        public readonly By ViewDetailsBtn = By.XPath("//div[@title='View Details'][@role='button']");

        public readonly By PLP_Header_1Btn = By.XPath("//a[text()='1']");
        public readonly By PLP_Header_2Btn = By.XPath("//a[text()='2']");
        public readonly By PLP_Header_3Btn = By.XPath("//a[text()='3']");
        public readonly By PLP_Header_4Btn = By.XPath("//a[text()='4']");
        public readonly By PLP_Header_5Btn = By.XPath("//a[text()='5']");
        public readonly By PLP_Header_PRBtn = By.XPath("//a[text()='PR']");
        public readonly By PLP_FormattedTextFormSaveAndCloseBtn = By.XPath("//span[text()='Save and Close']");
        public readonly By PLP_AreYouSureYouWantToDeleteFormDeleteBtn = By.XPath("//div[contains(@class, 'deleteConfirmButton')]//span[text()='Delete']/..");
        public readonly By PLP_ActivityDetailFormAddBtn = By.XPath("//span[text()='Add']");
        public readonly By PLP_ActivityDetailFormCancelBtn = By.XPath("//span[text()='Cancel']");
        public readonly By SupportInfoFormCloseBtn = By.XPath("//div[contains(@class, 'supportInfoClose')]//div[@title='Close']|//span[text()='CLOSE']");

        //info button
        public readonly By InfoBtn = By.ClassName("info-icon");
        public readonly By StepTitle = By.XPath("//div[@id='smartWizard']//h4");
        public readonly By PanelBodyList = By.XPath("//div[@id='accordion']//div[contains(@id,'collapse')]");
        public readonly By SelectYourPathway_PageTitle = By.XPath("//div[text()='Select Your Pathway']");
        public readonly By InfoButtons_SelectYourPathway = By.ClassName("info-icon");

        // Charts
        public readonly By PLPTopPercentChartTxt = By.XPath("//div[contains(@class, 'plpGaugeChart')]//*[name()='svg']//*[@class='gaugh']/div");
        public readonly By PLPPercentChartTxt = By.XPath("//div[contains(@class, 'plpGaugeChart')]//*[name()='svg']//*[@class='middle value']");

        // Check boxes

        // Frames
        public readonly By PLP_FormattedTextFormFrame = By.XPath("//iframe[@title='Editable area. Press F10 for toolbar.']");


        // General
        public readonly By LoadIcon = By.XPath("//div[@class='loading-indicator']");
        public readonly By LoadIconOverlay = By.XPath("//div[@class='loading overlay']");
        public readonly By ZendeskChatFrame = By.XPath("//iframe[@id='launcher']");
        public readonly By ZendeskChatMinimizeBtn = By.XPath("//button[contains(@class,'minimizeButton')]");


        // See the following for SVG elements: https://stackoverflow.com/questions/31520642/how-to-use-xpath-in-selenium-webdriver-to-grab-svg-elements
        public readonly By PLP_Header_PLPCompleteGraphLabel = By.XPath("//div[contains(@class, 'plpGaugeChart')]//*[name()='svg']//*[@class='custom value']");

        // Labels
        public readonly By WelcomeLbl = By.XPath("//h2[@class='eng-cycle']");
        public readonly By WereSorryErrorLbl = By.XPath("//span[contains(text(), 'retrieve your information')]");
        public readonly By NotificationFormLbl = By.XPath("//span[@class='notification-message']");
        public readonly By CurrentCycleDateLbl = By.XPath("//span[contains(@class, 'cycle-date')]");
        public readonly By PLP_Header_StepNumberLabel = By.XPath("//div[@id='smartWizard']/h2");
        public readonly By SupportInfoFormSupportInfoLbl = By.XPath("//h2[text()='Support Information']");
        public readonly By SupportInfoFormPLPSiteLbl = By.XPath("//h3[text()='plp@cfpc.ca']");
        public readonly By SupportInfoFormPLPExtnLbl = By.XPath("//span[text()='1-800-387-6197 ext 512']");
        public readonly By PLPActivitySummaryLbl = By.XPath("//div[text()='PLP Activity Summary']");


        // Links
        public readonly By LogoutLnk = By.XPath("//a[text()='Logout >> ']");
        public readonly By ClickHereToViewYourAmaRCPCreditsLnk = By.XPath("//span[contains(text(), 'to view your certified')]");

        // Menu Items    
        public readonly By Menu_MyDashboard = By.XPath("");
        public readonly By Menu_MyCPDActivitiesList = By.XPath("");

        public readonly By PLP_Menu_DropDownBtn = By.XPath("(//button[@class='profileButton']//span[@class='caret'])[1]");
        public readonly By PLP_Menu_PLPToolsAndResources = By.XPath("//div[contains(@class,'profileSelect')]//span[text()='>  PLP Tools and Resources']");
        public readonly By PLP_Menu_PLPActivitySumm = By.XPath("//span[text()='> PLP Activity Summary']");
        public readonly By PLP_Menu_PrintCompletedPLP = By.XPath("//span[text()='> Print my completed PLP']");
        public readonly By PLP_Menu_PrintPLPCertificate = By.XPath("//span[text()='> Print PLP Certificate']");
        public readonly By PLP_Menu_ContactUs = By.XPath("//span[text()='> Contact Us']");
        public readonly By PLP_Menu_ExitToMainpro = By.XPath("//span[text()='> Exit to Mainpro+']");
        public readonly By PLP_Menu_CloseBtn = By.XPath("//span[@class='close']");


        // Radio buttons

        // Tabs
        public readonly By DashboardTab = By.XPath("//div[@data-option-key='dashboard']//span[text()='Dashboard']");
        public readonly By CreditSummaryTab = By.XPath("//div[@data-option-key='creditsummary']//span[text()='Credit Summary']");
        public readonly By HoldingAreaTab = By.XPath("//div[@data-option-key='holdingarea']//span[text()='Holding Area']");
        public readonly By CPDActivitiesListTab = By.XPath("//div[@data-option-key='activities']//span[text()='CPD Activities List']");
        public readonly By CPDPlanningTab = By.XPath("//div[@data-option-key='planning']//span[text()='CPD Planning']");
        public readonly By PlpHubTab = By.XPath("//div[@data-option-key='plphub']//span[text()='PLP HUB']");
        public readonly By ReportsTab = By.XPath("//div[@data-option-key='reports']//span[text()='Reports']");
        public readonly By CACTab = By.XPath("");

        public readonly By CACCreditSummaryTab = By.XPath("");
        public readonly By CACHoldingAreaTab = By.XPath("");
        public readonly By CACCPDActivitiesListTab = By.XPath("");
        public readonly By CACReportsTab = By.XPath("");

        // Tables       
        public readonly By CredSummCycleTbl = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary') or contains(@aria-labelledby, 'csCycleSummary') or contains(@aria-labelledby, 'HoldAreaCycleSummary') or contains(@aria-labelledby, 'cpdCycleSummary')]");
        public readonly By CredSummCycleTblHdr = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary') or contains(@aria-labelledby, 'csCycleSummary') or contains(@aria-labelledby, 'HoldAreaCycleSummary') or contains(@aria-labelledby, 'cpdCycleSummary')]/thead");
        public readonly By CredSummCycleTblBody = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary') or contains(@aria-labelledby, 'csCycleSummary') or contains(@aria-labelledby, 'HoldAreaCycleSummary') or contains(@aria-labelledby, 'cpdCycleSummary')]/tbody");
        public readonly By CredSummCycleTblFirstRow = By.XPath("//table[contains(@aria-labelledby, 'cycleSummary') or contains(@aria-labelledby, 'csCycleSummary') or contains(@aria-labelledby, 'HoldAreaCycleSummary') or contains(@aria-labelledby, 'cpdCycleSummary')]/tbody/tr");

        public readonly By CredSummCurrentYrTbl = By.XPath("//table[contains(@aria-labelledby, 'currentYearSummary') or contains(@aria-labelledby, 'csCurrentYearSummary') or contains(@aria-labelledby, 'haCurrentYearSummary') or contains(@aria-labelledby, 'cpdCurrentYearSummary')]");
        public readonly By CredSummCurrentYrTblHdr = By.XPath("//table[contains(@aria-labelledby, 'currentYearSummary') or contains(@aria-labelledby, 'csCurrentYearSummary') or contains(@aria-labelledby, 'haCurrentYearSummary') or contains(@aria-labelledby, 'cpdCurrentYearSummary')]/thead");
        public readonly By CredSummCurrentYrTblBody = By.XPath("//table[contains(@aria-labelledby, 'currentYearSummary') or contains(@aria-labelledby, 'csCurrentYearSummary') or contains(@aria-labelledby, 'haCurrentYearSummary') or contains(@aria-labelledby, 'cpdCurrentYearSummary')]/tbody");
        public readonly By CredSummCurrentYrTblFirstRow = By.XPath("//table[contains(@aria-labelledby, 'currentYearSummary') or contains(@aria-labelledby, 'csCurrentYearSummary') or contains(@aria-labelledby, 'haCurrentYearSummary') or contains(@aria-labelledby, 'cpdCurrentYearSummary')]/tbody/tr");

        public readonly By CredSummAnnualReqsTbl = By.XPath("//table[contains(@aria-labelledby, 'csAnnualReqGrid')]");
        public readonly By CredSummAnnualReqsTblHdr = By.XPath("//table[contains(@aria-labelledby, 'csAnnualReqGrid')]/thead");
        public readonly By CredSummAnnualReqsTblBody = By.XPath("//table[contains(@aria-labelledby, 'csAnnualReqGrid')]/tbody");
        public readonly By CredSummAnnualReqsTblFirstRow = By.XPath("//table[contains(@aria-labelledby, 'csAnnualReqGrid')]/tbody/tr");

        public readonly By AMARCPMaxCreditFormTbl = By.XPath("//table[contains(@aria-labelledby, 'CycleMaxCreditActivityGrid')]");
        public readonly By AMARCPMaxCreditFormTblHdr = By.XPath("//table[contains(@aria-labelledby, 'CycleMaxCreditActivityGrid')]/thead");
        public readonly By AMARCPMaxCreditFormTblBody = By.XPath("//table[contains(@aria-labelledby, 'CycleMaxCreditActivityGrid')]/tbody");
        public readonly By AMARCPMaxCreditFormTblFirstRow = By.XPath("//table[contains(@aria-labelledby, 'CycleMaxCreditActivityGrid')]/tbody/tr");

        // Text boxes
        public readonly By PLP_FormattedTextFormFrameTxt = By.XPath("//body");
        public readonly By PLP_commentBoxTxt = By.XPath("//div[contains(@id,'CPDActivityRecommendationExplanation')]/following-sibling::div/input");
    }
}
