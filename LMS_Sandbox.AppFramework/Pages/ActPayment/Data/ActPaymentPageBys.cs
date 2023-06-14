using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActPaymentPageBys
    {




        // Buttons
        public readonly By SubmitRegistrationBtn = By.XPath("(//button[@type='submit'])[2]");
        public readonly By SubmitBtn = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_btnSubmit");
        public readonly By ApplyBtn = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_btnApplyDiscount");
        public readonly By DeleteDiscountCodeXBtn = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_iconDeleteDiscountCode");
        public readonly By NextBtn = By.XPath("//img[@class='RedirectToAppRedirector']");
        public readonly By OkBtn = By.XPath("//input[@type='submit' and contains(@value, 'OK')]");
        public readonly By SubmitAccessCodeBtn = By.Id("ctl00_ContentPlaceHolder1_ctl00_MyPanel_ctl00_btnSubmit");

        // Charts


        // Check boxes


        // General
        public readonly By LoadIcon_Registration = By.Id("ctl00_ContentPlaceHolder1_ctl00_MyPanel_ctl00_fb1_UpdateProgress1"); // This is the loading screen that appears for a brief moment when a user selects the Are you CHES certified radio button
        public readonly By LoadIcon_Payment = By.Id("ctl00_ContentPlaceHolder1_ctl00_uprogressCreditCard");


        // Labels                                              
        public readonly By FeeAmountValueLbl = By.Id("ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_lblOriginalAmount");
        public readonly By PaymentInvoiceLbl = By.XPath("//div[@id='ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_pnlCCInstructions']/span");
        public readonly By PaymentConfirmationLbl = By.Id("ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_Receipt1_pnlMessage_pnlMessageTitleLabel");



        // Links
        public readonly By UseADiscountCodeLnk = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_lnkUseDiscountCode");


        // Menu Items    


        // Radio buttons
        public readonly By AreYouCHESNoRdo = By.XPath("(//div[contains(text(), 'Are you CHES/MCHES certified?')]/following-sibling::div//input)[2]");

        // Select Elements
        public readonly By StateSelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlState");
        public readonly By CountrySelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlCountry");
        public readonly By CreditCardTypeSelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlCreditCardType");
        public readonly By ExpDateMonthSelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlMonth");
        public readonly By ExpDateYearSelElem = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_ddlYear");
        public readonly By ProfessionSelElem = By.XPath("");
        public readonly By StateRegViewSelElem = By.XPath("");
        // Tables       


        // Tabs


        // Text boxes
        public readonly By FirstNameRegViewTxt = By.XPath("//div[text()='First Name']/following-sibling::div/input");
        public readonly By LastNameRegViewTxt = By.XPath("//div[text()='Last Name']/following-sibling::div/input");
        public readonly By FirstNameTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtFirstName");
        public readonly By LastNameTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtLastName");
        public readonly By AddLine1Txt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtAddressLine1");
        public readonly By TownCityTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtCity");
        public readonly By ZipCodeTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtZipCode");
        public readonly By DaytimePhoneTxt = By.Id("ctl00_ContentPlaceHolderBase1_ctrlASTROPaymentControl_txtDayTimePhone");
        public readonly By CreditCardNumTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtCardNumber");
        public readonly By SecurityCodeTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtSecurityCode");
        public readonly By DiscountCodeTxt = By.Id("ctl00_ContentPlaceHolder1_ctl00_pnlMainContent_ctl00_txtDiscountCode");
        public readonly By ParticipantIdTxt = By.XPath("//div[text()='Participant ID']/following-sibling::div/input");
        public readonly By Address01Txt = By.XPath("//span[contains(text(),'Address 01')]/../..//input");
        public readonly By CityTxt = By.XPath("//div[text()='City']/following-sibling::div/input");
        public readonly By PostalCodeTxt = By.XPath("//div[text()='Postal Code']/following-sibling::div/input");
        public readonly By EmailTxt = By.XPath("//div[text()='Email']/following-sibling::div/input");
        public readonly By OrganiztionorCompanyTxt = By.XPath("//div[text()='Organization']/following-sibling::div/input");
        public readonly By PasswordTxt = By.XPath("//span[text()='Password is required.']/../..//input");
        public readonly By ConfirmPasswordTxt = By.XPath("//span[text()='The Password and Confirmation Password must match.']/../..//input");
        public readonly By SecurityAnswerTxt = By.XPath("//span[text()='Security answer is required.']/../..//input");
        public readonly By AccessCodeTxt = By.XPath("//span[contains(text(), 'Access Code')]/../..//input");
        public readonly By EnterDiscountCodeTxt = By.XPath("//input[@name='discountCode']");



    }
}