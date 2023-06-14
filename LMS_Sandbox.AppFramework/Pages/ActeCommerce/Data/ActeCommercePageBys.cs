using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActeCommercePageBys
    {




        // Buttons
        public readonly By PayBtn = By.XPath("//input[@name='commit']");

        // Charts


        // Check boxes


        // General

        // Labels                                              

        public readonly By TotalAmountValueLbl = By.XPath("(//ul[@class='totalamount']/p)[3]");


        // Links


        // Menu Items    


        // Radio buttons
        public readonly By VisaRdo = By.XPath("//label[text()='Visa']/preceding-sibling::input");
        
        // Select Elements
        public readonly By CountrySelElem = By.XPath("//select[@id='bill_to_address_country']");
        public readonly By ExirationDateMonthSelElem = By.XPath("//select[@id='card_expiry_month']");
        public readonly By ExirationDateYearSelElem = By.XPath("//select[@id='card_expiry_year']");

        // Tables       


        // Tabs


        // Text boxes
        public readonly By FirstNameTxt = By.XPath("//input[@name='bill_to_forename']");
        public readonly By LastNameTxt = By.XPath("//input[@name='bill_to_surname']");
        public readonly By AddressTxt = By.XPath("//input[@name='bill_to_address_line1']");
        public readonly By CityTxt = By.XPath("//input[@name='bill_to_address_city']");
        public readonly By StateTxt = By.XPath("//input[@name='bill_to_address_state']");
        public readonly By ZipTxt = By.XPath("//input[@name='bill_to_address_postal_code']");
        public readonly By PhoneNumberTxt = By.XPath("//input[@name='bill_to_phone']");
        public readonly By EmailTxt = By.XPath("//input[@name='bill_to_email']");
        public readonly By CardNumberTxt = By.XPath("//input[@name='card_number']");
        public readonly By CVNTxt = By.XPath("//input[@name='card_cvn']");





    }
}