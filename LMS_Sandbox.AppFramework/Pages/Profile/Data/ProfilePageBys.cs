using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ProfilePageBys
    {
        // Buttons
        public readonly By SaveBtn = By.XPath("(//input[@value='Save'])[2]");
        public readonly By Backbtn = By.XPath("//a[text()='Back']");



        // Charts

        // Check boxes

        // General 
        public readonly By LoadIcon = By.Id(""); 


        // Labels                                              



        // Links      

        // Menu Items    

        // Radio buttons
        public readonly By AreYouCHESYesRdo = By.XPath("//label[contains(text(),'Yes')]");
        public readonly By AreYouCHESNoRdo = By.XPath("//label[contains(text(),'No')]");

        // Select Elements
        public readonly By CountrySelElem = By.XPath("//span[contains(text(),'Country')]/../..//Select");
        public readonly By StateSelElem = By.XPath("//span[contains(text(),'State')]/../..//Select");
        public readonly By ProfessionSelElem = By.XPath("//span[contains(text(),'Profession')]/../..//Select");


        // Tables       

        // Tabs

        // Text boxes
        public readonly By FirstNameTxt = By.XPath("//span[contains(text(),'First Name')]/../..//input");
        public readonly By LastNameTxt = By.XPath("//span[contains(text(),'Last Name')]/../..//input");
        public readonly By Address01Txt = By.XPath("//span[contains(text(),'Address 01')]/../..//input");
        public readonly By CityTxt = By.XPath("//span[contains(text(),'City')]/../..//input");
        public readonly By PostalCodeTxt = By.XPath("//span[contains(text(),'Postal Code')]/../..//input");
        public readonly By EmailTxt = By.XPath("//span[contains(text(),'Email')]/../..//input");
        public readonly By OrganiztionorCompanyTxt = By.XPath("//span[contains(text(),'Organization/Company')]/../..//input");
        public readonly By ParticipantId = By.XPath("");



        




    }
}