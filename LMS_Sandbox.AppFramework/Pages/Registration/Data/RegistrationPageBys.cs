using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class RegistrationPageBys
    {
        // Buttons
        public readonly By ContinueBtn = By.XPath("//input[@value='Continue']");



        // Charts

        // Check boxes
        public readonly By IAgreeChk = By.XPath("//input[@type='checkbox']");

        // General 
        public readonly By LoadIcon_Registration = By.Id("ctl00_ContentPlaceHolder1_PortalRegistrationWizardControl1_MyPanel_ctl00_CreateUserWizard1_PortalRegistrationFormControl1_fb1_UpdateProgress1"); // This is the loading screen that appears for a brief moment when a user selects the Profession dropdown, or clicks a radio button, etc.


        // Labels                                              



        // Links      

        // Menu Items    

        // Radio buttons
        public readonly By AreYouCHESYesRdo = By.XPath("//span[contains(text(), 'Are you CHES/MCHES certified?')]/../following-sibling::td//label[contains(text(),'Yes')]");
        public readonly By AreYouCHESNoRdo = By.XPath("//span[contains(text(), 'Are you CHES/MCHES certified?')]/../following-sibling::td//label[contains(text(),'No')]");
        public readonly By HispanicLatinoNoRdo = By.XPath("//span[contains(text(), 'Hispanic')]/../following-sibling::td//label[contains(text(),'No')]");
        public readonly By AreYouOncologyNurseYesRdo = By.XPath("//span[contains(text(), 'Are you an Oncology Nurse')]/../following-sibling::td//label[contains(text(),'Yes')]");


        

        // Select Elements
        public readonly By CountrySelElem = By.XPath("//span[contains(text(),'Country')]/../..//Select");
        public readonly By StateSelElem = By.XPath("//span[contains(text(),'State')]/../..//Select");
        public readonly By ProfessionSelElem = By.XPath("//span[contains(text(),'Profession')]/../..//Select");
        public readonly By YourPrimFuncRoleSelElem = By.XPath("//span[contains(text(), 'Your Primary Functional Role')]/../following-sibling::td/select");
        public readonly By PrimaryPositionSelElem = By.XPath("//span[contains(text(),'Primary Position')]/../..//Select");
        public readonly By PrimarySpecialtySelElem = By.XPath("//span[contains(text(),'Primary Specialty')]/../..//Select");
        public readonly By PrimaryWorkSettingSelElem = By.XPath("//span[contains(text(),'Primary Work Setting')]/../..//Select");

        


        // Tables       

        // Tabs

        // Text boxes
        public readonly By FirstNameTxt = By.XPath("//span[contains(text(),'First Name')]/../..//input");
        public readonly By LastNameTxt = By.XPath("//span[contains(text(),'Last Name')]/../..//input");
        public readonly By Address01Txt = By.XPath("//span[contains(text(),'Address 01')]/../..//input");
        public readonly By CityTxt = By.XPath("//span[contains(text(),'City')]/../..//input");
        public readonly By PostalCodeTxt = By.XPath("//span[contains(text(),'Postal Code')]/../..//input");
        public readonly By EmailTxt = By.XPath("//span[contains(text(),'Email')]/../..//input");
        public readonly By OrganiztionorCompanyTxt = By.XPath("//span[contains(text(),'Organization')]/../..//input");
        public readonly By PasswordTxt = By.XPath("//span[text()='Password is required.']/../..//input");
        public readonly By ConfirmPasswordTxt = By.XPath("//span[text()='The Password and Confirmation Password must match.']/../..//input");
        public readonly By SecurityAnswerTxt = By.XPath("//span[text()='Security answer is required.']/../..//input");
        public readonly By ParticipantId = By.XPath("//span[contains(text(),'Participant ID')]/../..//input");
        public readonly By UsernameTxt = By.XPath("//span[text()='UserName must be a valid user name.']/../..//input");







    }
}