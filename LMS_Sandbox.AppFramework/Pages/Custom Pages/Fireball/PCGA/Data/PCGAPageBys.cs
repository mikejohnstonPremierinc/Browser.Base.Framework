using OpenQA.Selenium;
using LMS.AppFramework.Constants_UAMS_;
namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class PCGAPageBys
    {

        // Buttons
        public readonly By ProfessionalEduTblViewAllBtn = Constants_UAMS.Tbl1ViewAllBtn;
        public readonly By ProfessionalEdu2TblViewAllBtn = Constants_UAMS.Tbl2ViewAllBtn;
        public readonly By ProfessionalEdu3TblViewAllBtn = Constants_UAMS.Tbl3ViewAllBtn;
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
        public readonly By ProfessionalEdu2Tbl = Constants_UAMS.Tbl2;
        public readonly By ProfessionalEdu2TblFirstLnk = Constants_UAMS.Tbl2FirstLnk;
        public readonly By ProfessionalEdu3Tbl = Constants_UAMS.Tbl3;
        public readonly By ProfessionalEdu3TblFirstLnk = Constants_UAMS.Tbl3FirstLnk;

        // Tabs

        // Text boxes



    }
}