using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    public class CPDPlanningPageBys
    {
        //buttons
        public readonly By CreateAPersonalLearningGoalBtn = By.XPath("//span[text()='CREATE A PERSONAL LEARNING PLAN GOAL']");
        public readonly By CreateAGoalFormCreateBtn = By.XPath("//span[text()='Create']");
        public readonly By CreateAGoalFormCloseButton = By.XPath("//span[text()='CLOSE']");
        public readonly By UpdateProgressFormSaveBtn = By.XPath("//div[contains(@class, 'ProgressSaveButton')]//span[text()='Save']");
        public readonly By UpdateProgressFormCancelBtn = By.Name("//div[contains(@class, 'ProgressCancelButton')]//span[text()='Cancel']");
        public readonly By DeleteGoalFormYesBtn = By.XPath("//span[text()='YES']");

        // Charts

        // Check boxes

        // General
        public readonly By UpdateProgressFormSlider = By.XPath("//div[@class='slider-filter-container']");


        // Labels                                              

        // Links


        // Menu Items    

        // Radio buttons

        // Select Element
        public readonly By CreateAGoalFormWhatIsTheGoalTypeSelElem = By.XPath("//div[text()='What is the goal type?']/..//select");
        public readonly By CreateAGoalFormWhatIsTheGoalTypeSelElemBtn = By.XPath("//div[text()='What is the goal type?']/..//button");


        public readonly By GoalTblTypeSelElem = By.XPath("//span[text()='Type:']/..//select");
        public readonly By GoalTblTypeSelElemBtn = By.XPath("//span[text()='Type:']/..//button");

        public readonly By GoalTblDueDateSelElem = By.XPath("//span[text()='Due Date:']/..//select");
        public readonly By GoalTblDueDateSelElemBtn = By.XPath("//span[text()='Due Date:']/..//button");

        public readonly By GoalTblProgressSelElem = By.XPath("//span[text()='Progress:']/..//select");
        public readonly By GoalTblProgressSelElemBtn = By.XPath("//span[text()='Progress:']/..//button");

        // Tables 
        public readonly By GoalsTbl = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]");
        public readonly By GoalsTblHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]//thead[not(contains(@aria-hidden, 'true'))]");
        public readonly By GoalsTblBody = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]//tbody");
        public readonly By GoalsTblBodyFirstRow = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]//tbody/tr");
        public readonly By GoalTblCPDGoalOrObjColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]//thead[@class='main-header']//span[text()='CPD Goal or Objective']");
        public readonly By GoalTblTypeColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]//thead[@class='main-header']//span[text()='Type']");
        public readonly By GoalTblDueDateColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]//thead[@class='main-header']//span[text()='Due Date']");
        public readonly By GoalTblProgressColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]//thead[@class='main-header']//span[text()='Progress']");
        public readonly By GoalTblActionColHdr = By.XPath("//table[contains(@aria-labelledby, 'cpdPlanningGoalsObjectsListGrid')]//thead[@class='main-header']//span[text()='Actions']");

        // Tabs

        // Text boxes
        public readonly By CreateAGoalFormWhatIsTheGoalTxt = By.XPath("//textarea[@aria-label='What is the goal?']");
        public readonly By CreateAGoalFormHowWillAccomplishTxt = By.XPath("//textarea[@aria-label='How will you accomplish the goal?']");
        public readonly By CreateAGoalFormWhatIsPlannedDueDateTxt = By.XPath("//input[@aria-label='select day']");
        public readonly By UpdateProgressFormCompletionDateTxt = By.XPath("//input[@aria-label='select day']");



        // MJ 2/23/21: See my note in the OpenUpdateProgressPopup method
        public readonly By UpdateProgress1Lnk = By.XPath("");
        public readonly By UpdateProgress2Lnk = By.XPath("");
        public readonly By UpdateProgress3Lnk = By.XPath("");
        public readonly By UpdateProgress4Lnk = By.XPath("");
        public readonly By UpdateProgress5Lnk = By.XPath("");
        // MJ 2/23/21: See my note in the OpenUpdateProgressPopup method
        public readonly By Close1Btn = By.XPath("");
        public readonly By Close2Btn = By.XPath("");
        public readonly By Close3Btn = By.XPath("");
        public readonly By Close4Btn = By.XPath("");
        public readonly By Close5Btn = By.XPath("");

    }
}
