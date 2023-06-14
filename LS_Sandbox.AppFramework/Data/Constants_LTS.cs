using System.ComponentModel;
using System.Configuration;

namespace LS.AppFramework.Constants_LTS
{
    public static class Constants_LTS
    {

        /// <summary>
        /// Represents what is searched for on any search page
        /// </summary>
        public enum SearchResults
        {
            Sites,
            Participants,
            Programs,
            Activities
        }

        /// <summary>
        /// Represents the Credit Validation page radio buttons
        /// </summary>
        public enum CreditValidationOptions
        {
            Accept,
            Reject,
            NeedMoreInformation
        }

        public enum AddAdjustFormCFPCCustomAdjustFirstSelElemItem
        {
            [Description("Adjust Cycle End Date")]
            AdjustCycleEndDate,

            [Description("Adjust end of cycle by Days")]
            AdjustEndOfCycleByDays,

            [Description("Adjust end of cycle by Months")]
            AdjustEndOfCycleByMonths,

            [Description("Adjust end of cycle by Years")]
            AdjustEndOfCycleByYears,

            [Description("Adjust Cycle Start Date")]
            AdjustCycleStartDate,
        }

        /// <summary>
        /// The adjustment codes inside the Adjustment Code select element on the Add Adjustment form within the 
        /// Program Adjustments tab on the RCP Maintenance of Certification Program page
        /// </summary>
        public enum AdjustmentCodes
        {
            [Description("A-ACTIVE (Default)")]
            AActiveDefault,

            [Description("AF-AFFILIATE (Default)")]
            AFAffilidateDefault,

            [Description("AM-ASSOCIATE MEMBER (Voluntary)")]
            AMAssociateMemberVoluntary,

            [Description("AS-RESIDENT (Resident)")]
            ASResidentResident,

            [Description("C-CUSTOM")]
            CCustom,

            [Description("EO-EXAMS ONLY (Voluntary)")]
            EOExamsOnlyVoluntary,

            [Description("EX-NON MEMBER-FM EXAM (Voluntary)")]
            ExNonMemberFMExamVoluntary,

            [Description("HO-HONORARY (Voluntary)")]
            HOHonoraryVoluntary,

            [Description("IM-IMG (Voluntary)")]
            IMIMGVoluntary,

            [Description("L-Leave")]
            LLeave,

            [Description("LI-P - Life Practicing (Default)")]
            LIPLifePracticingDefault,

            [Description("MP-*MAINPRO+ NM (Default)")]
            MPMainproNMDefault,

            [Description("NC-NO CYCLE")]
            NCNoCycle,

            [Description("N-NON-MEMBER (Voluntary)")]
            NNonMemberVoluntary,

            [Description("P-PENDING (Voluntary)")]
            PPendingVoluntary,

            [Description("PB-PUBLIC MEMBER (Voluntary)")]
            PBPublicMemberVoluntary,

            [Description("RE-RETIRED (Voluntary)")]
            RERetiredVoluntary,

            [Description("RR-Remedial Reinstatement (Reinstatement)")]
            RRRemedialReinstatementReinstatement,

            [Description("SE-SENIOR (Default)")]
            SESeniorDefault,

            [Description("SM-SUSTAINING MAINPRO+ (Default)")]
            SMSustainingMainproDefault,

            [Description("ST-STUDENT (Voluntary)")]
            STStudentVoluntary,

            [Description("SU-SUSTAINING (Voluntary)")]
            SUSustainingVoluntary,

            [Description("Z-NON-MEMBER (Voluntary)")]
            ZNonMemberVoluntary,

            [Description("R-Remedial")]
            RRemedial,

            [Description("EC-Early Completion")]
            ECEarlyCompletion,

            [Description("DC-Discontinuation")]
            DCDiscontinuation,

            [Description("MR-Member Reinstate")]
            MRMemberReinstate,

            [Description("LI-NP - Life Nonpracticing (Voluntary)")]
            LINPLifeNonPracticingVoluntary,

            [Description("PA-PROSPECT (Voluntary)")]
            PAProspectVoluntary,

            [Description("EH-EXHIBITOR (Voluntary)")]
            EHExhibitorVoluntary,

            [Description("RCC-Remedial Class Change")]
            RCCRemedialClassChange,

            [Description("EXT1")]
            EXT1,

            [Description("EXT2")]
            EXT2,

            [Description("EXT2F")]
            EXT2F,

            [Description("PRA")]
            PRA,

            [Description("PER")]
            PER,

            [Description("INTNL")]
            INTNL,

            [Description("LEAVE")]
            LEAVE,

            [Description("TEMP")]
            TEMP,

            [Description("VOLUNTARY")]
            VOLUNTARY,

            [Description("NOCYCLE")]
            NOCYCLE,

            [Description("CUSTOM")]
            CUSTOM,

            [Description("REINSTATED – NON COMPLIANCE")]
            REINSTATEDNonCompliance,

            [Description("REINSTATED – OTHER")]
            REINSTATEDOther,

            [Description("PER Program")]
            PERProgram,

            [Description("PRA Program")]
            PRAProgram,

            [Description("Voluntary Program")]
            VoluntaryProgram,

            [Description("International Program")]
            InternationalProgram,

            [Description("Main Program")]
            MainProgram,

            [Description("Resident Program")]
            ResidentProgram,
        }

    }
}