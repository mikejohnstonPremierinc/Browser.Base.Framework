using OpenQA.Selenium;
using LMS.AppFramework.Constants_UAMS_;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class HRSAPageBys
    {
        // Buttons
        public readonly By GeriatricKnowledgeTblViewAllBtn = Constants_UAMS.Tbl1ViewAllBtn;
        public readonly By InterprofessionalEducationTblViewAllBtn = Constants_UAMS.Tbl2ViewAllBtn;
        public readonly By EducationCompetenciesTblViewAllBtn = Constants_UAMS.Tbl3ViewAllBtn;



        // Charts

        // Check boxes

        // frames       


        // General

        // Labels                                              

        // Links

        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By GeriatricKnowledgeTbl = Constants_UAMS.Tbl1;
        public readonly By GeriatricKnowledgeTblFirstLnk = Constants_UAMS.Tbl1FirstLnk;
        public readonly By InterprofessionalEducationTbl = Constants_UAMS.Tbl2;
        public readonly By InterprofessionalEducationTblFirstLnk = Constants_UAMS.Tbl2FirstLnk;
        public readonly By EducationCompetenciesTbl = Constants_UAMS.Tbl3;
        public readonly By EducationCompetenciesTblFirstLnk = Constants_UAMS.Tbl3FirstLnk;

        // Tabs

        // Text boxes
    }
}