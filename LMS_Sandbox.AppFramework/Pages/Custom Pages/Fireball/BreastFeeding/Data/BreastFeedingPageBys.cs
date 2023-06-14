using OpenQA.Selenium;
using LMS.AppFramework.Constants_UAMS_;
namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class BreastFeedingPageBys
    {

        // Buttons
        public readonly By BreastFeeingCurricTblViewAllBtn = Constants_UAMS.Tbl1ViewAllBtn;
        public readonly By BundledProgramsTblViewAllBtn = Constants_UAMS.Tbl2ViewAllBtn;
        public readonly By BundledActivitiesTblViewAllBtn = Constants_UAMS.Tbl3ViewAllBtn;
        public readonly By FeaturedActivitiesTblViewAllBtn = Constants_UAMS.Tbl4ViewAllBtn;

        // Charts

        // Check boxes

        // Frames


        // General
        public readonly By MainHdr = By.XPath("//span[contains(@id, 'UAMSDashboard')]");

        // Labels                                              

        // Links



        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By BreastFeeingCurricTbl = Constants_UAMS.Tbl1;
        public readonly By BreastFeeingCurricTblFirstLnk = Constants_UAMS.Tbl1FirstLnk;
        public readonly By BundledProgramsTbl = Constants_UAMS.Tbl2;
        public readonly By BundledProgramsTblFirstLnk = Constants_UAMS.Tbl2FirstLnk;
        public readonly By BundledActivitiesTbl = Constants_UAMS.Tbl3;
        public readonly By BundledActivitiesTblFirstLnk = Constants_UAMS.Tbl3FirstLnk;
        public readonly By FeaturedActivitiesTbl = Constants_UAMS.Tbl4;
        public readonly By FeaturedActivitiesTblFirstLnk = Constants_UAMS.Tbl4FirstLnk;

        // Tabs

        // Text boxes
    }
}