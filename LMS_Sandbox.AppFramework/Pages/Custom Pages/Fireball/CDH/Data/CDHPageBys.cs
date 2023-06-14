using OpenQA.Selenium;
using LMS.AppFramework.Constants_UAMS_;
namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class CDHPageBys
    {
        // Buttons
        public readonly By ProfessionalEduTblViewAllBtn = Constants_UAMS.Tbl1ViewAllBtn;
        public readonly By ComingSoonTblViewAllBtn = Constants_UAMS.Tbl2ViewAllBtn;
        public readonly By AdditionalResourcesTblViewAllBtn = Constants_UAMS.Tbl3ViewAllBtn;

        // Charts

        // Check boxes

        // frames       



        // General

        // Labels                                              

        // Links

        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By ProfessionalEduTbl = Constants_UAMS.Tbl1;
        public readonly By ProfessionalEduTblFirstLnk = Constants_UAMS.Tbl1FirstLnk;
        public readonly By ComingSoonTbl = Constants_UAMS.Tbl2;
        public readonly By ComingSoonTblFirstLnk = Constants_UAMS.Tbl2FirstLnk;
        public readonly By AdditionalResourcesTbl = Constants_UAMS.Tbl3;
        public readonly By AdditionalResourcesTblFirstLnk = Constants_UAMS.Tbl3FirstLnk;

        // Tabs

        // Text boxes
    }
}