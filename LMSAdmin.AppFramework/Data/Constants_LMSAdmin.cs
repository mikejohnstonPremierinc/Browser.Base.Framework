using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel.DataAnnotations; // need to add a nuget package (System.ComponentModel.DataAnnotations) for this for it to work. https://stackoverflow.com/questions/10174420/why-cant-i-reference-system-componentmodel-dataannotations
using System.ComponentModel;
using Browser.Core.Framework;

namespace LMSAdmin.AppFramework.ConstantsLMSAdmin
{
    public static class Constants_LMSAdmin
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Constants_LMSAdmin));

        /// <summary>
        /// The connection string to the database of the Web Application
        /// </summary>
        public static readonly string SQLconnString = AppSettings.Config["SQLConnectionString"];

        public enum ActivityStage
        {
            UnderConstruction,
            UnderReview,
            ConstructionComplete
        }

        public enum RecentItemCategory
        {
            Activity,
            Project
        }

        public enum LoginUserNames
        {
            [Description("uams_admin")]
            UAMS,

            [Description("ama_admin")]
            AMA,

            [Description("prem_admin")]
            PREM,

            [Description("chi_admin")]
            CHI,

            [Description("tp_admin")]
            TP,

            [Description("dhmc_admin")]
            DHMC,

            [Description("aafp_admin")]
            AAFPRS,

            [Description("aafp_admin")]
            AAFPPN,

            [Description("asnc_admin")]
            ASNC,

            [Description("aapa_admin")]
            AAPA,

            [Description("abam_admin")]
            ABAM,

            [Description("aboto_admin")]
            ABOTO,

            [Description("acr_admin")]
            ACR,

            [Description("aha_admin")]
            AHA,


            [Description("cap_admin")]
            CAP,

            [Description("cfpc_admin")]
            CFPC,

            [Description("cmcd_admin")]
            CMCD,

            [Description("isuog_admin")]
            ISUOG,

            [Description("medconcert_admin")]
            Medconcert,

            [Description("nbme_admin")]
            NBME,

            [Description("nof_admin")]
            NOF,

            [Description("kp2p_admin")]
            KP2P,

            [Description("snmmi_admin")]
            SNMMI,

            [Description("wiley_admin")]
            Wiley,

            [Description("cmeca_admin")]
            CMECAL,
           
            [Description("ucd_admin")]
            UCD,
            
            [Description("pfe_admin")]
            Pfizer,

            [Description("oncology_admin")]
            ONS,

            [Description("AWHONN_admin")]
            AWHONN,

            [Description("NCPA_admin")]
            NCPA,

            [Description("UCI_admin")]
            UCI,

            [Description("lmsdemo_admin")]
            CurDemoPortal,
        }

        public enum ActivityTitle
        {
          [Description("Automation - Claim Credit - 17.3 Profession Specific")]
          AHA_standAloneActivity,

          [Description("Automation - Session - 16, Parent set to ZERO 17 Credit Claim should not show (AHA)")]
          AHA_LiveMeetingActivity,

        [Description("Automation - 11 Registration - 09.CPE Monitor (UAMS)")]
        UAMS_standAloneActivity,

        [Description("Automation - Session - 16.06 Fixed Credits (UAMS)")]
        UAMS_LiveMeetingActivity,

        [Description("Automation - Registration - 10 Payment Needed (CAP)")]
        CAP_standAloneActivity,

        [Description("Live Session Max Credit Test")]
        CAP_LiveMeetingActivity,

        }

        public enum SessionTitle
        {
            [Description("01 Jan Empty")]
            AHA_SessionActivity,

            [Description("03 March - 1")]
            UAMS_SessionActivity,

            [Description("Session 1")]
            CAP_SessionActivity

        }

    }
}