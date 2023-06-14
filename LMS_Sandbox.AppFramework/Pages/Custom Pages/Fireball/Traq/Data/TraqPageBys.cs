using OpenQA.Selenium;
using LMS.AppFramework.Constants_UAMS_;
namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class TraqPageBys
    {

        // Buttons
        public readonly By RecordedPresentationsTblViewAllBtn = Constants_UAMS.Tbl1ViewAllBtn;
        public readonly By RegSchedTeleconfTblViewAllBtn = Constants_UAMS.Tbl2ViewAllBtn;
        public readonly By ComingSoonTblViewAllBtn = Constants_UAMS.Tbl3ViewAllBtn;
        // Charts

        // Check boxes

        // frames




        // General

        // Labels                                              

        // Links

        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By RegSchedTeleconfTbl = Constants_UAMS.Tbl1;
        public readonly By RegSchedTeleconfTblFirstLnk = Constants_UAMS.Tbl1FirstLnk;
        public readonly By RecordedPresentationsTbl = Constants_UAMS.Tbl2;
        public readonly By RecordedPresentationsTblFirstLnk = Constants_UAMS.Tbl2FirstLnk;
        public readonly By ComingSoonTbl = Constants_UAMS.Tbl3;
        public readonly By ComingSoonTblFirstLnk = Constants_UAMS.Tbl3FirstLnk;

        // Tabs

        // Text boxes



    }
}