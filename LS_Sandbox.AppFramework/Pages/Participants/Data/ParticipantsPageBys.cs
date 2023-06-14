using OpenQA.Selenium;

namespace LS.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class ParticipantsPageBys
    {

        // Buttons
        public readonly By ProgramsTabRecognitionFormYesBtn = By.Id("btnYes");


        // Charts

        // Check boxes

        // forms
        
        public readonly By ProgramsTabRecognitionForm = By.Id("fancybox-content");

        // Labels                                              
        public readonly By DetailsTabGuidLbl = By.XPath("//label[text()='Guid:']");


        // Links


        // Menu Items    


        // Radio buttons


        // Tables       
        public readonly By ProgramsTabProgramTbl = By.Id("recognitionSelector");
        public readonly By ProgramsTabProgramTblBodyRow = By.XPath("//table[@id='recognitionSelector']/tbody/tr[1]");


        // Tabs
        public readonly By ProgramsTab = By.XPath("//a[contains(text(),'Programs')]");
        public readonly By RegeneratePasswordTab = By.XPath("//a[contains(text(),'Regenerate Password')]");

        
        // Text boxes
        public readonly By ProgramsTabRecognitionFormStartDtTxt = By.Id("txtRecognitionStartDate");
        public readonly By ProgramsTabRecognitionFormEndDtTxt = By.Id("txtRecognitionEndDate");

        


    }
}