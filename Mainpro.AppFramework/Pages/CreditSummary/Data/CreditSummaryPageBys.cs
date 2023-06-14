using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    public class CreditSummaryPageBys
    {
        // Buttons
        public readonly By ViewAllCyclesBtn = By.XPath("//span[text()='View All Cycles']");
        public readonly By ViewAllCyclesFormCloseBtn = By.XPath("//a[text()='CLOSE']");
        public readonly By CSViewFormViewActivitiesCloseBtn = By.XPath("//span[text()='Close']");
        


        // Charts

        // Check boxes

        // Labels                                              
        public readonly By YouAreCurrentlyViewingPreviousCycleDate = By.XPath("//span[contains(text(), 'You are currently viewing previous cycle data.')]");


        // Links


        // Menu Items    

        // Radio buttons

        // Select Element

        // Tables 
        public readonly By AnnualRequirementsTbl = By.XPath("//table[contains(@aria-labelledby, 'AnnualReqGrid')]");
        public readonly By AnnualRequirementsTblHdr = By.XPath("//table[contains(@aria-labelledby, 'AnnualReqGrid')]/thead");
        public readonly By AnnualRequirementsTblBody = By.XPath("//table[contains(@aria-labelledby, 'AnnualReqGrid')]/tbody");
        public readonly By AnnualRequirementsTblBodyRow = By.XPath("//table[contains(@aria-labelledby, 'AnnualReqGrid')]/tbody/tr");

        public readonly By GroupLearningTbl = By.XPath("//table[contains(@aria-labelledby, 'GroupLearningCreditsGrid')]");
        public readonly By GroupLearningTblHdr = By.XPath("//table[contains(@aria-labelledby, 'GroupLearningCreditsGrid')]/thead");
        public readonly By GroupLearningTblBody = By.XPath("//table[contains(@aria-labelledby, 'GroupLearningCreditsGrid')]/tbody");
        public readonly By GroupLearningTblBodyRow = By.XPath("//table[contains(@aria-labelledby, 'GroupLearningCreditsGrid')]/tbody/tr");

        public readonly By SelfLearningTbl = By.XPath("//table[contains(@aria-labelledby, 'SelfLearningCreditsGrid')]");
        public readonly By SelfLearningTblHdr = By.XPath("//table[contains(@aria-labelledby, 'SelfLearningCreditsGrid')]/thead");
        public readonly By SelfLearningTblBody = By.XPath("//table[contains(@aria-labelledby, 'SelfLearningCreditsGrid')]/tbody");
        public readonly By SelfLearningTblBodyRow = By.XPath("//table[contains(@aria-labelledby, 'SelfLearningCreditsGrid')]/tbody/tr");

        public readonly By AssessmentTbl = By.XPath("//table[contains(@aria-labelledby, 'AssessmentCreditsGrid')]");
        public readonly By AssessmentTblHdr = By.XPath("//table[contains(@aria-labelledby, 'AssessmentCreditsGrid')]/thead");
        public readonly By AssessmentTblBody = By.XPath("//table[contains(@aria-labelledby, 'AssessmentCreditsGrid')]/tbody");
        public readonly By AssessmentTblBodyRow = By.XPath("//table[contains(@aria-labelledby, 'AssessmentCreditsGrid')]/tbody/tr");

        public readonly By OtherTbl = By.XPath("//table[contains(@aria-labelledby, 'csOtherCreditsGrid')]");
        public readonly By OtherTblHdr = By.XPath("//table[contains(@aria-labelledby, 'csOtherCreditsGrid')]/thead");
        public readonly By OtherTblBody = By.XPath("//table[contains(@aria-labelledby, 'csOtherCreditsGrid')]/tbody");
        public readonly By OtherTblBodyRow = By.XPath("//table[contains(@aria-labelledby, 'csOtherCreditsGrid')]/tbody/tr");

        public readonly By ViewAllCyclesFormCyclesTbl = By.XPath("//table[contains(@aria-labelledby, 'allCyclesGrid')]");
        public readonly By ViewAllCyclesFormCyclesTblHdr = By.XPath("//table[contains(@aria-labelledby, 'allCyclesGrid')]/thead");
        public readonly By ViewAllCyclesFormCyclesTblBody = By.XPath("//table[contains(@aria-labelledby, 'allCyclesGrid')]/tbody");
        public readonly By ViewAllCyclesFormCyclesTblBodyRow = By.XPath("//table[contains(@aria-labelledby, 'allCyclesGrid')]/tbody/tr");

        public readonly By CSViewFormViewActivitiesTbl = By.XPath("//table[contains(@aria-labelledby, 'CSViewActivityModalGrid')]");
        public readonly By CSViewFormViewActivitiesTblHdr = By.XPath("//table[contains(@aria-labelledby, 'CSViewActivityModalGrid')]/thead");
        public readonly By CSViewFormViewActivitiesTblBody = By.XPath("//table[contains(@aria-labelledby, 'CSViewActivityModalGrid')]/tbody");
        public readonly By CSViewFormViewActivitiesTblBodyRow = By.XPath("//table[contains(@aria-labelledby, 'CSViewActivityModalGrid')]/tbody/tr");

        // Tabs

        // Text boxes






    }
}
