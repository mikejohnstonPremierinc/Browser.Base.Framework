using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActAccreditationPageBys
    {

        // Buttons
        public readonly By BackToActivityBtn = By.XPath("//button[contains(@class,'activity-return-button')]");
        public readonly By AddAccreditationBtn = By.XPath("//div[@title='ADD ACCREDITATION']");            
        public readonly By AddAccreditationFormAddAccreditationBtn = By.XPath("//button[@aria-label='ADD ACCREDITATION']");
        public readonly By AddScenarioBtn = By.XPath("//button/span[text()='Add scenario']|//span[text()='Add scenario']/parent::button");
        
        public readonly By AddScenarioFormSaveScenarioBtn = By.XPath("//div[contains(@class,'addScenario')]//button[@aria-label='SAVE SCENARIO']");
        public readonly By AccreditationDeleteFormOkBtn = By.XPath("//button[@aria-label='Ok']");
        public readonly By ScenarioDeleteFormOkBtn = By.XPath("//button[@aria-label='Ok']");
        public readonly By EditAccreditationFormSaveAccreditationBtn = By.XPath("//button[@aria-label='SAVE ACCREDITATION']");
        public readonly By EditScenarioFormSaveScenarioBtn = By.XPath("//div[contains(@class,'editScenario')]//button[@aria-label='SAVE SCENARIO']");
        public readonly By AnyDialogCommonCloseBtn = By.XPath("//div[@data-attach-point='closeButton']");
        public readonly By AccreditationOrScenarioEditBtn = By.XPath("//button/div[contains(@class,'icon-pencil')]");
        public readonly By AccreditationOrScenarioDeleteBtn = By.XPath("//button/div[contains(@class,'icon-trash')]");
        public readonly By AccreditationOrScenarioViewBtn = By.XPath("//button/div[contains(@class,'icon-eye-open')]");



        // Charts

        // Check boxes
        public readonly By ClaimCreditEnabledChkbox = By.XPath("//input[@type='checkbox'][@name='claimCreditEnabledName']");

        // General
        public readonly By DeleteScenarioFormDeleteScenarioConfirmMessage = By.XPath("//div[@data-bind-value='confirmationMessage']");
        public readonly By DeleteAccreditationFormDeleteAccreditationConfirmMessage = By.XPath("//div[@data-bind-value='confirmationMessage']");
        public readonly By AccreditationEmptyMessage = By.XPath("//div[contains(text(),'do not have any accreditations')]");

        // Labels                                                   
        public readonly By AccreditationPageTitleLbl = By.XPath("//div/p[@class='title-bar' and text()='Accreditation']");
        public readonly By AddAccreditationFormAddAccreditationLbl = By.XPath("//div[@class='modal-title'][text()='Add Accreditation']");
        public readonly By EditAccreditationFormEditAccreditationLbl = By.XPath("//div[@class='modal-title'][text()='Edit Accreditation']");
        public readonly By ViewAccreditationFormViewAccreditationLbl = By.XPath("//div[@class='modal-title'][text()='View Accreditation']");
        public readonly By AddScenarioFormAddScenarioLbl = By.XPath("//div[@class='modal-title'][text()='Add a Scenario']");
        public readonly By EditScenarioFormEditScenarioLbl = By.XPath("//div[@class='modal-title'][text()='Edit Scenario']");
        public readonly By ViewScenarioFormViewScenarioLbl = By.XPath("//div[@class='modal-title'][text()='View Scenario']");
        public readonly By ThisFieldIsRequiredLbl = By.XPath("//div[contains(@class,'validation-message')][text()='This field is required']");
        public readonly By AccreditationBodyandTypeFieldRequiredLbl = By.XPath("//div[@id='addAccreditationModalForm-accBodyName-validation']");
        public readonly By AddScenarioFormScenarioNameFieldRequiredLbl = By.XPath("//div[@id='scenarioModelForm-scenarioName-validation']");
        public readonly By AddScenarioFormEligibleProfessionsFieldRequiredLbl = By.Id("scenarioModelForm-eligibleProfessions-validation");
        public readonly By AddScenarioFormFixedCreditFormatError = By.Id("scenarioModelForm-fxdCredit-validation");
        public readonly By AddScenarioFormEquivalentCreditFormatError = By.Id("scenarioModelForm-eqCredit-validation");
        public readonly By AddScenarioFormMaxCreditFormatError = By.Id("scenarioModelForm-maxCredit-validation");
        public readonly By AddScenarioFormMinCreditFormatError = By.Id("scenarioModelForm-minCredit-validation");
        public readonly By AddScenarioFormCreditIncrementFormatError = By.Id("scenarioModelForm-creditIncrement-validation");

        // Links

        // Menu Items    

        // Radio buttons

        // select elements

        public readonly By AccreditationBodyandTypeSelElemBtn = By.XPath("//div[text()='ACCREDITING BODY AND TYPE']/following-sibling::div//button[contains(@aria-labelledby,'accBodyName')]");
        public readonly By AccreditationBodyandTypeSelElemOptionsDropdown = By.XPath("//button[contains(@aria-labelledby,'accBodyName')]/following-sibling::select");
        public readonly By PrimaryProviderSelElemBtn = By.XPath("//button[contains(@aria-labelledby,'accProviderName') and contains(@aria-labelledby,'addAccr') and not(contains(@class,'disabled'))]");
        //public readonly By PrimaryProviderSelElemOptionsDropdown = By.XPath("//button[contains(@aria-labelledby,'accProviderName') and contains(@aria-labelledby,'addAccr') and not(contains(@class,'disabled'))]/following-sibling::select");

        public readonly By PrimaryProviderSelElemOptionsDropdown = By.XPath("/html/body/div[3]/div/div[2]/ul/li[2]/a/span[2]");
        
        public readonly By AdditionalProviderSelElemBtn = By.XPath("//button[contains(@aria-labelledby,'accAddProviderName') and contains(@aria-labelledby,'addAccr') and not(contains(@class,'disabled'))]");
        public readonly By AdditionalProviderSelElemOptionsDropdown = By.XPath("//button[contains(@aria-labelledby,'accAddProviderName') and contains(@aria-labelledby,'addAccr') and not(contains(@class,'disabled'))]/following-sibling::select/option");
        public readonly By EligibleProfessionElemBtn = By.XPath("//button[contains(@aria-labelledby,'eligibleProfessions')]");
        public readonly By EligibleProfessionElemOptionsDropdown = By.XPath("//button[contains(@aria-labelledby,'eligibleProfessions')]/following-sibling::select");

        //public readonly By EligibleProfessionElemBtn = By.XPath("/html/body/div[4]/div/div[3]/div[2]/div/div/div/div/div/form/div[5]/div/div/div[1]/div[2]/div/div/div[1]/div/button");
        
        public readonly By EligibleSpecialitiesSelElemBtn = By.XPath("//button[contains(@aria-labelledby,'eligibleSpecialties')]");
        public readonly By EligibleCountriesSelElemBtn = By.XPath("//button[contains(@aria-labelledby,'eligibleCountries')]");
        public readonly By EditAccreditationFormPrimaryProviderSelElemBtn = By.XPath("//button[contains(@aria-labelledby,'accProviderName') and contains(@aria-labelledby,'editAccr') and not(contains(@class,'disabled'))]");
        public readonly By FixedCreditUnitSelElemBtn= By.XPath("//button[contains(@aria-labelledby,'fxdCreditUnit')]");
        public readonly By EquivalentCreditUnitSelElemBtn = By.XPath("//button[contains(@aria-labelledby,'eqCreditUnit')]");

        // Tables   
        public readonly By AccreditationDetailsTbl = By.XPath("//div[@class='column-accreditationGridChartColumn chart-grid-row-cell']//table");
        public readonly By AccreditationDetailsTblRow = By.XPath("//div[@class='column-accreditationGridChartColumn chart-grid-row-cell']//table/tbody/tr");
        public readonly By ScenarioDetailsTbl = By.XPath("//div[@class='column-scenarioGridChartColumn chart-grid-row-cell']//table");       
        public readonly By AccreditationTblAccBodyTypeColumn = By.XPath("//td[contains(@class,'accBodyType')]");
        public readonly By AccreditationTblPrimaryProviderColumn = By.XPath("//td[contains(@class,'accPrimaryProvider')]");
        
        public readonly By ScenarioDetailsTblScenNameColumn = By.XPath("//td[contains(@class,'column-scenarios')]");        

        // Tabs

        // Text boxes
        public readonly By ScenarionameTxt = By.XPath("//input[@aria-label='SCENARIO NAME']");
        public readonly By CodeNumberTxt = By.XPath("//input[@name='codeNbr']");        
        public readonly By ReleaseDateTxt = By.XPath("//input[@name='releaseDate']");
        public readonly By ExpirationDateTxt = By.XPath("//input[@name='expDate']");
        public readonly By FixedCreditTxt = By.XPath("//input[@aria-label='FIXED CREDIT']");
        public readonly By EquivalentCreditTxt = By.XPath("//input[@aria-label='EQUIVALENT CREDIT']");
        public readonly By MaximumCreditsTxt = By.XPath("//input[@aria-label='MAXIMUM CREDITS']");
        public readonly By MinimumCreditsTxt = By.XPath("//input[@aria-label='MINIMUM CREDITS']");
        public readonly By CreditIncrementsTxt = By.XPath("//input[@aria-label='CREDIT INCREMENTS']");
        public readonly By CustomTextTxt = By.XPath("//input[@aria-label='CUSTOM TEXT']");
        
    }
}