using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActAssessmentDetailsPageBys
    {

        // Buttons
        public readonly By DetailsTabAddAudBtn = By.Id("ctl00_AddAudience");
        public readonly By DetailsTabSaveBtn = By.Id("ctl00_SaveButtonBottom");
        public readonly By QATabContinueBtn = By.Id("ctl00_ctl00_ctl00_btnContinue");
        public readonly By QATabSaveandContinuebtn = By.Id("ctl00_ctl00_FB_QUEST_btnContinue");
        public readonly By AnswerKeyTabSaveBtn = By.Id("ctl00_AnswerKey_frmBTNSubmit");


        // Charts

        // Check boxes


        // General


        // Labels                                                   
        public readonly By DetailsTabChngsSavedLbl = By.Id("ctl00_lblChangesSavedMsg");
        public readonly By AnswerKeyTabChangesSavedLbl = By.Id("ctl00_AnswerKey_lblChangesSavedMsg");

        
        // Links
        public readonly By QATabAddNewQuesBtn = By.XPath("//a[contains(text(), 'Add New Question')]");
        public readonly By QATabInsertNewQuesLnk = By.XPath("//span[contains(text(), 'Insert New Question')]");
        public readonly By BackToAssessmentsLnk = By.XPath("//a[text()='Back to Assessments']");


        // Menu Items    

        // Radio buttons
        public readonly By QATabGradedQuestionRdoBtn = By.Id("ctl00_ctl00_FB_QUEST_Graded");

        // select elements
        public readonly By DetailsTabAssTypeSelElem = By.Id("ctl00_AssessmentType");
        public readonly By DetailsTabAvailAudSelElem = By.Id("ctl00_AvailableAudiences");
        public readonly By QATabQuestTypeSelElem = By.Id("ctl00_ctl00_FB_QUEST_selQuestionType");


        // Tables   


        // Tabs
        public readonly By QATab = By.XPath("//a[contains(text(), 'Q&A')]");
        public readonly By AnswerKeyTab = By.XPath("//a[contains(text(), 'Answer Key')]");

        // Text boxes
        public readonly By DetailsTabAssTitleTxt = By.Id("ctl00_AssessmentName");
        public readonly By DetailsTabAssDescTxt = By.Id("ctl00_AssessmentTitle");
        public readonly By QATabQuesNameTxt = By.Id("ctl00_ctl00_FB_QUEST_QuestionName");
        public readonly By QATabQuesText = By.Id("ctl00_ctl00_FB_QUEST_QuestionText");
        public readonly By QATabAnswerOptionsTxt = By.Id("ctl00_ctl00_FB_QUEST_AnswerOptions");


        





    }
}