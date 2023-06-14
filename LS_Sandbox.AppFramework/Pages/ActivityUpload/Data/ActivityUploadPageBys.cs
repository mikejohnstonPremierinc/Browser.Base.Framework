using OpenQA.Selenium;

namespace LS.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class ActivityUploadPageBys
    {

        // Buttons
        public readonly By UploadBtn = By.ClassName("upload");

        
        // Charts

        // Check boxes

        // General
        public readonly By ChooseFileUploadElem = By.Id("ExternalActivities");


        // Labels                                              



        // Links


        // Menu Items    


        // Radio buttons


        // Tables       
        public readonly By UploadTbl = By.Id("eauploadhistory");
        public readonly By UploadTblHdr = By.XPath("//table[@id='eauploadhistory']/thead");
        public readonly By UploadTblBody = By.XPath("//table[@id='eauploadhistory']/tbody");
        public readonly By UploadTblBodyFirstRow = By.XPath("//table[@id='eauploadhistory']/tbody/tr");
        
        // Tabs


        // Text boxes



    }
}