using OpenQA.Selenium;
using LMS.AppFramework.Constants_UAMS_;
namespace LMS.AppFramework
{
    /// <summary>
    /// This is where we locate our elements. Please use standard naming conventions and group your elements as shown below. Standard naming 
    /// conventions are defined here: https://code.premierinc.com/docs/display/PGHLMSDOCS/Best+Practices
    /// </summary>
    public class AngelsLivePageBys
    {

        // Buttons
        public readonly By OBEmergenciesTblViewAllBtn = Constants_UAMS.Tbl1ViewAllBtn;
        public readonly By FetalHeartMonitoringTblViewAllBtn = Constants_UAMS.Tbl2ViewAllBtn;
        public readonly By SpecialEventsTblViewAllBtn = Constants_UAMS.Tbl3ViewAllBtn;
        // Charts

        // Check boxes

        // frames



        // General

        // Labels                                              

        // Links

        // Menu Items    

        // Radio buttons

        // Tables  
        public readonly By OBEmergenciesTbl = Constants_UAMS.Tbl1;
        public readonly By OBEmergenciesTblFirstLnk = Constants_UAMS.Tbl1FirstLnk;
        public readonly By FetalHeartMonitoringTbl = Constants_UAMS.Tbl2;
        public readonly By FetalHeartMonitoringTblFirstLnk = Constants_UAMS.Tbl2FirstLnk;
        public readonly By SpecialEventsTbl = Constants_UAMS.Tbl3;
        public readonly By SpecialEventsTblFirstLnk = Constants_UAMS.Tbl3FirstLnk;

        // Tabs

        // Text boxes



    }
}