using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActRegistrationPageBys
    {


        // Buttons
        public readonly By RegisterBtn = By.XPath("//button[@aria-label='Register']");
        public readonly By BackBtn = By.Id("//button[@aria-label='Back']");

        // Charts


        // Check boxes
        public readonly By EthnicityCaucasianChk = By.XPath("//span[contains(text(), text()='Caucasian/White')]");
        public readonly By SelectCreditTypesChks = By.XPath("//div[contains(text(), 'Credit Types')]/..//input");
        public readonly By EthnicityChks = By.XPath("//div[contains(text(), 'Ethnicity')]/..//input");

        
        // General

        // Labels                                              



        // Links


        // Menu Items    


        // Radio buttons
        public readonly By AreYouCHESNoRdo = By.XPath("(//div[contains(text(), 'Are you CHES/MCHES certified?')]/following-sibling::div//input)[2]");
        public readonly By PIM_MemberStatus_MemberRdo = By.XPath("(//div[contains(text(), 'Member Status')]/following-sibling::div//input)[1]");
        public readonly By DidThisProgramMeetRdo = By.XPath("(//div[contains(text(), 'Did this program meet your professional needs')]/following-sibling::div//input)[1]");
        public readonly By IsThereARegForm = By.XPath("(//div[contains(text(), 'Is there a Registration form')]/following-sibling::div//input)[1]");

        // Select Elements
        public readonly By ProfessionSelElem = By.XPath("//div[contains(text(), 'Profession')]/following-sibling::div//select");
        public readonly By ProfessionSelElemBtn = By.XPath("//div[contains(text(), 'Profession')]/following-sibling::div//button");
        public readonly By SecondaryProfessionSelElem = By.XPath("//div[contains(text(), 'SECONDARY_PROFESSION')]/following-sibling::div//select");
        public readonly By SecondaryProfessionSelElemBtn = By.XPath("//div[contains(text(), 'SECONDARY_PROFESSION')]/following-sibling::div//button");
        public readonly By GenderSelElem = By.XPath("//div[contains(text(), 'Gender')]/following-sibling::div//select");
        public readonly By GenderSelElemBtn = By.XPath("//div[contains(text(), 'Gender')]/following-sibling::div//button");
        public readonly By StateProvinceSelElem = By.XPath("//div[contains(text(), 'State/Province')]/following-sibling::div//select");
        public readonly By StateProvinceSelElemBtn = By.XPath("//div[contains(text(), 'State/Province')]/following-sibling::div//button");
        public readonly By CountrySelElem = By.XPath("//div[contains(text(), 'Country')]/following-sibling::div//select");
        public readonly By CountrySelElemBtn = By.XPath("//div[contains(text(), 'Country')]/following-sibling::div//button");

        // Tables       


        // Tabs


        // Text boxes
        public readonly By ParticipantIDTxt = By.XPath("//input[contains(@aria-label, 'Participant ID')]");
        public readonly By FirstNameTxt = By.XPath("//input[contains(@aria-label, 'First Name')]");
        public readonly By MiddleInitialTxt = By.XPath("//input[contains(@aria-label, 'Middle Initial')]");
        public readonly By LastNameTxt = By.XPath("//input[contains(@aria-label, 'Last Name')]");
        public readonly By AddLine1Txt = By.XPath("//input[contains(@aria-label, 'Address 01')]");
        public readonly By CityTxt = By.XPath("//input[contains(@aria-label, 'City')]");
        public readonly By PostalCodeTxt = By.XPath("//input[contains(@aria-label, 'Postal Code')]");
        public readonly By EmailTxt = By.XPath("//input[contains(@aria-label, 'Email')]");
        public readonly By PhoneNumberTxt = By.XPath("//input[contains(@aria-label, 'Phone Number')]");
        public readonly By OrganiztionorCompanyTxt = By.XPath("//input[contains(@aria-label, 'Organization/Company')]");
        public readonly By Degreext = By.XPath("//input[contains(@aria-label, 'Degree')]");

















        //// Buttons
        //public readonly By SubmitRegistrationBtn = By.XPath("(//button[@type='submit'])[2]");
        //    public readonly By SubmitPaymentBtn = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_btnSubmit");
        //    public readonly By ApplyBtn = By.XPath("//input[@value='Apply']");
        //    public readonly By DeleteDiscountCodeXBtn = By.XPath("//input[@id='ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_iconDeleteDiscountCode']");
        //    public readonly By NextBtn = By.XPath("//img[@class='RedirectToAppRedirector']");
        //    public readonly By OkBtn = By.XPath("//input[@type='submit' and contains(@value, 'OK')]");
        //    public readonly By SubmitAccessCodeBtn = By.Id("ctl00_ContentPlaceHolder1_ctl00_MyPanel_ctl00_btnSubmit");

        //    // Charts


        //    // Check boxes


        //    // General
        //    public readonly By LoadIcon_Registration = By.Id("ctl00_ContentPlaceHolder1_ctl00_MyPanel_ctl00_fb1_UpdateProgress1"); // This is the loading screen that appears for a brief moment when a user selects the Are you CHES certified radio button
        //    public readonly By LoadIcon_Payment = By.Id("ctl00_ContentPlaceHolder1_ctl00_uprogressCreditCard");


        //    // Labels                                              
        //    public readonly By FeeAmountValueLbl = By.Id("ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_lblOriginalAmount");
        //    public readonly By PaymentInvoiceLbl = By.XPath("//div[@id='ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_pnlCCInstructions']/span");
        //    public readonly By PaymentConfirmationLbl = By.Id("ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_Receipt1_pnlMessage_pnlMessageTitleLabel");



        //    // Links
        //    public readonly By UseADiscountCodeLnk = By.Id("ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_lnkUseDiscountCode");


        //    // Menu Items    


        //    // Radio buttons
        //    public readonly By AreYouCHESNoRdo = By.XPath("(//div[contains(text(), 'Are you CHES/MCHES certified?')]/following-sibling::div//input)[2]");

        //    // Select Elements
        //    public readonly By StateSelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlState");
        //    public readonly By CountrySelElem = By.XPath("//div[text()='Country']/following-sibling::div//select");
        //    public readonly By CountrySelElemBtn = By.XPath("//div[text()='Country']/following-sibling::div//button");
        //    public readonly By CreditCardTypeSelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlCreditCardType");
        //    public readonly By ExpDateMonthSelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlMonth");
        //    public readonly By ExpDateYearSelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlYear");
        //    public readonly By ProfessionSelElem = By.XPath("//div[text()='Profession']/following-sibling::div//select");
        //    public readonly By ProfessionSelElemBtn = By.XPath("//div[text()='Profession']/following-sibling::div//button");
        //    public readonly By StateRegViewSelElem = By.XPath("//div[text()='State/Province']/following-sibling::div//select");
        //    public readonly By StateRegViewSelElemBtn = By.XPath("//div[text()='State/Province']/following-sibling::div//button");
        //    // Tables       


        //    // Tabs


        //    // Text boxes
        //    public readonly By FirstNameRegViewTxt = By.XPath("//div[text()='First Name']/following-sibling::div/input");
        //    public readonly By LastNameRegViewTxt = By.XPath("//div[text()='Last Name']/following-sibling::div/input");        
        //    public readonly By FirstNameTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtFirstName");
        //    public readonly By LastNameTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtLastName");
        //    public readonly By AddLine1Txt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtAddressLine1");
        //    public readonly By TownCityTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtCity");
        //    public readonly By ZipCodeTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtZipCode");
        //    public readonly By DaytimePhoneTxt = By.Id("ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_txtDayTimePhone");
        //    public readonly By CreditCardNumTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtCardNumber");
        //    public readonly By SecurityCodeTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtSecurityCode");
        //    public readonly By DiscountCodeTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_lnkUseDiscountCode");
        //    public readonly By ParticipantIdTxt = By.XPath("//div[text()='Participant ID']/following-sibling::div/input");
        //    public readonly By Address01Txt = By.XPath("//span[contains(text(),'Address 01')]/../..//input");
        //    public readonly By CityTxt = By.XPath("//div[text()='City']/following-sibling::div/input");
        //    public readonly By PostalCodeTxt = By.XPath("//div[text()='Postal Code']/following-sibling::div/input");
        //    public readonly By EmailTxt = By.XPath("//div[text()='Email']/following-sibling::div/input");
        //    public readonly By OrganiztionorCompanyTxt = By.XPath("//div[text()='Organization']/following-sibling::div/input");
        //    public readonly By PasswordTxt = By.XPath("//span[text()='Password is required.']/../..//input");
        //    public readonly By ConfirmPasswordTxt = By.XPath("//span[text()='The Password and Confirmation Password must match.']/../..//input");
        //    public readonly By SecurityAnswerTxt = By.XPath("//span[text()='Security answer is required.']/../..//input");
        //    public readonly By AccessCodeTxt = By.XPath("//span[contains(text(), 'Access Code')]/../..//input");



    }
}