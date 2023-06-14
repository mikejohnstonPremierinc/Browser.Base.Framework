using OpenQA.Selenium;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class ActMaterialPageBys
    {

        // Buttons
        public readonly By ContinueBtn = By.XPath("//body[contains(@class, 'activity_content')]//span[text()='Continue']/..");
        public readonly By FinishBtn = By.XPath("//body[contains(@class, 'activity_content')]//span[text()='Finish']");

        // Charts

        // Check boxes
        public readonly By ConfirmWithCheckBoxChk = By.XPath("//input[@type='checkbox' and @name='confirmation']");
        public readonly By ActivityMaterialChk = By.XPath("//body[@class='fireball activity_content']//input[@type='checkbox']");

        // General
        public readonly By WordContentTypeITagElem = By.XPath("//i[contains(@style,'doc.png')] | //i[contains(@style,'docx.png')]");
        public readonly By XLSXContentTypeITagElem = By.XPath("//i[contains(@style,'xls.png')]");
        public readonly By PPTContentTypeITagElem = By.XPath("//i[contains(@style,'ppt.png')]");
        public readonly By TXTContentTypeITagElem = By.XPath("//a[text()='TEXT']/preceding-sibling::i[contains(@style,'png')]");
        public readonly By PNGContentTypeITagElem = By.XPath("//a[text()='PNG']/preceding-sibling::i[contains(@style,'png')]");
        public readonly By PDFContentTypeITagElem = By.XPath("//i[contains(@style,'pdf.png')]");
        public readonly By JPGContentTypeITagElem = By.XPath("//i[contains(@style,'image.png')]");
        public readonly By MP3ContentTypeITagElem = By.XPath("//i[contains(@style,'audio.png')]");
        public readonly By MP4ContentTypeITagElem = By.XPath("//i[contains(@style,'video.png')]");

        public readonly By WordContentTypeATagElem = By.XPath("//a[text()='Word']");
        public readonly By XLSXContentTypeATagElem = By.XPath("//a[text()='Excel']");
        public readonly By PPTContentTypeATagElem = By.XPath("//a[text()='PPT']");
        public readonly By TXTContentTypeATagElem = By.XPath("//a[text()='TEXT']");
        public readonly By PNGContentTypeATagElem = By.XPath("//a[text()='PNG']");



        // Labels                                              
        public readonly By PleaseClickTheNameLbl = By.XPath("//span[text()='Please click the name of the item you wish to view.']");       
        public readonly By ConfirmWithCheckBoxLbl = By.XPath("//div[@class='fireball-widget activityContentConfirmForm']//div[contains(@class, 'label')]");

        // Links
        public readonly By ActivityMaterialFileExtensionLnks = By.XPath("//td/div/*[contains(@class, 'hyperlink')]|//i[contains(@class, 'content')]/following-sibling::span");


        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes

    }
}