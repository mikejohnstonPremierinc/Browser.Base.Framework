using System;
using System.ComponentModel;
using System.Configuration;
using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;

namespace Mainpro.AppFramework
{
    public static class Const_Mainpro
    {
        /// <summary>
        /// The connection string to the database of the Web Application
        /// </summary>
        public static readonly string SQLconnString = AppSettings.Config["SQLConnectionString"];

        public static List<By> TextBoxCharacterAndIntegerElemBys = new List<By>()
            {
                Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheExamActInWhichYouParticipatedTxt,
                Bys.EnterACPDActivityDetailsPage.CityTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeWhatYouHaveLearnedTxt,
                Bys.EnterACPDActivityDetailsPage.HowWillYouIncorporateTheKnowledgeYouGainedTxt,
                Bys.EnterACPDActivityDetailsPage.IfYesWhatMightTheseBenefitsBeTxt,
                Bys.EnterACPDActivityDetailsPage.IndicateYourRoleForThisActTxt,
                Bys.EnterACPDActivityDetailsPage.OversightOrganizationTxt,
                Bys.EnterACPDActivityDetailsPage.PlanningOrganizationTxt,
                Bys.EnterACPDActivityDetailsPage.WhatResourcesWouldSupportYouTxt,
                Bys.EnterACPDActivityDetailsPage.WhatResourcesWouldSupportYouInAdvancingYourUnderstandingOfThisAssessmentTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWouldYouDoDifferentlyTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeTheActivityTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeWhatWasLearnedAsAResultTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeHowParticipatingInThisCourseTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWasYourRoleTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeTheNatureOfYourPracticeTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWereYourSpecificQuestionsTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeTheProcessTxt,
                Bys.EnterACPDActivityDetailsPage.WhatIsYourassessmentTxt,
                Bys.EnterACPDActivityDetailsPage.WhatApproachOrToolsTxt,
                Bys.EnterACPDActivityDetailsPage.BasedOnWhatYouLearnedTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWillYouHaveToDoTxt,
                Bys.EnterACPDActivityDetailsPage.PleaseDescribeYourReflectionsTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeTheOutcomeTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWereYourReasonsForWritingTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeYourActionPlanTxt,
                Bys.EnterACPDActivityDetailsPage.WhatImpactTxt,
                Bys.EnterACPDActivityDetailsPage.WhatStepTxt,
                Bys.EnterACPDActivityDetailsPage.ListTheJournalArticlesTxt,
                Bys.EnterACPDActivityDetailsPage.WhatMeshTxt,
                Bys.EnterACPDActivityDetailsPage.WhatTriggeredThisQuestionTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeAnyKeySuccessesTxt,
                Bys.EnterACPDActivityDetailsPage.WhatKindOfInformationTxt,
                Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheFindingsTxt,
                Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheAuditTxt,
                Bys.EnterACPDActivityDetailsPage.ForThePurposeOfThisExerciseTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWasTheOriginTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWasYourRoleTxt,
                Bys.EnterACPDActivityDetailsPage.IfYouAreSubmittingTxt,
                Bys.EnterACPDActivityDetailsPage.WhoWasInvolvedTxt,
                Bys.EnterACPDActivityDetailsPage.WhatMustYouDoTxt,
                Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheAssessmentTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWasYourAssessmentTxt,
                Bys.EnterACPDActivityDetailsPage.WhatChangesTxt,
                Bys.EnterACPDActivityDetailsPage.PleaseExplainTxt,
                Bys.EnterACPDActivityDetailsPage.FurtherDescribeTheActivityTxt,
                Bys.EnterACPDActivityDetailsPage.ExplainToWhatTxt,
                Bys.EnterACPDActivityDetailsPage.SupervisorTxt,
                Bys.EnterACPDActivityDetailsPage.LocationsTxt,
               // Bys.EnterACPDActivityDetailsPage.InGeneralTermsTxt,
                Bys.EnterACPDActivityDetailsPage.WhatIstheSpecificAreaTxt,
                Bys.EnterACPDActivityDetailsPage.WhyDidYouAddressTxt,
                Bys.EnterACPDActivityDetailsPage.HowSuccessfullTxt,
                Bys.EnterACPDActivityDetailsPage.HowWillYouIntegrateTxt,
                Bys.EnterACPDActivityDetailsPage.BasedUponTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeTheCourseTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWereYourLearningTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeHowThisCourseTxt,
                Bys.EnterACPDActivityDetailsPage.NameOfCourseTxt,
                Bys.EnterACPDActivityDetailsPage.NameOfUniversityTxt,
                Bys.EnterACPDActivityDetailsPage.WhatWillYouTakeTxt,
                Bys.EnterACPDActivityDetailsPage.WhatOpportunitiesTxt,
                Bys.EnterACPDActivityDetailsPage.HowWouldThisExperienceTxt,
                Bys.EnterACPDActivityDetailsPage.WhatFeedbackTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeHowYourAdminstrativeTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeYourRoleTxt,
                Bys.EnterACPDActivityDetailsPage.WhatDidYouLearnTxt,
                Bys.EnterACPDActivityDetailsPage.WhatFeedbackHaveYouReceivedFromReviewersTxt,
                Bys.EnterACPDActivityDetailsPage.FormatOfFeedback2Txt,
                Bys.EnterACPDActivityDetailsPage.FormatOfFeedback1Txt,
                Bys.EnterACPDActivityDetailsPage.FromTheFeedback2Txt,
                Bys.EnterACPDActivityDetailsPage.FromTheFeedback1Txt,
                Bys.EnterACPDActivityDetailsPage.BasedOnThisFeedback1Txt,
                Bys.EnterACPDActivityDetailsPage.DescribeTheTeachingTxt,
                Bys.EnterACPDActivityDetailsPage.AudienceTxt,
                Bys.EnterACPDActivityDetailsPage.SettingTxt,
                Bys.EnterACPDActivityDetailsPage.BasedOnThisFeedback2Txt,
                Bys.EnterACPDActivityDetailsPage.DescribeYourCriticalAppraisalTxt,
                Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheOutcomeTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeTheFeedbackTxt,
                Bys.EnterACPDActivityDetailsPage.BasedOnTheFeedbackTxt,
                Bys.EnterACPDActivityDetailsPage.InTheTextBoxBelowTxt,
                Bys.EnterACPDActivityDetailsPage.AssessTheInformationTxt,
                Bys.EnterACPDActivityDetailsPage.DescribeTheCriticalTxt,
                Bys.EnterACPDActivityDetailsPage.ProgramActivityIDTxt,
                Bys.EnterACPDActivityDetailsPage.ObtainFeedbackTxt,
                Bys.EnterACPDActivityDetailsPage.GatherInformationTxt,
                Bys.EnterACPDActivityDetailsPage.PleaseDescribeTheTeachingTxt,
                Bys.EnterACPDActivityDetailsPage.BasedOnYourAppraisalTxt,

                
    };



        public static List<By> TextBoxIntegerOnlyElemBys = new List<By>()
            {
                Bys.EnterACPDActivityDetailsPage.HoursTxt,
                Bys.EnterACPDActivityDetailsPage.ArticlesTxt,
    };


        public static List<By> RadioButtonElemBys = new List<By>()
            {
                Bys.EnterACPDActivityDetailsPage.DoYouAnticipateThisExperienceBenefitingYesRdo,
                Bys.EnterACPDActivityDetailsPage.IAmMotivatedToLearnMoreYesRdo,
                Bys.EnterACPDActivityDetailsPage.ILearnedSomethingNewYesRdo,
                Bys.EnterACPDActivityDetailsPage.IndicateYourRoleForThisActivityAssessmentOfSelfRdo,
                Bys.EnterACPDActivityDetailsPage.IPerceivedBiasYesRdo,
                Bys.EnterACPDActivityDetailsPage.IWasDissatisfiedYesRdo,
                Bys.EnterACPDActivityDetailsPage.MyPracticeWillBeChangedAndImprovedYesRdo,
                Bys.EnterACPDActivityDetailsPage.PleaseIndicateYourRoleInThisAssessmentAssessedRdo,
                Bys.EnterACPDActivityDetailsPage.TheExperienceConfirmedIAmDoingTheRightThingYesRdo,
                Bys.EnterACPDActivityDetailsPage.IsThisActivityAccreditedByAnotherOrgNoRdo,
                Bys.EnterACPDActivityDetailsPage.WereTheResultsYesRdo,
                Bys.EnterACPDActivityDetailsPage.HowSuccessfullVerySuccessfullRdo,
                Bys.EnterACPDActivityDetailsPage.WhoDidtheLiteratureSearchMyselfRdo,
                Bys.EnterACPDActivityDetailsPage.IfYouAreSubmitting1Rdo,
                Bys.EnterACPDActivityDetailsPage.HowWillThisQuestionChangeYourPractice1TheChangeWillBeLargeRdo,
                Bys.EnterACPDActivityDetailsPage.HowWillThisQuestionChangeYourPractice2TheChangeWillBeLargeRdo,
                Bys.EnterACPDActivityDetailsPage.HowWillThisQuestionChangeYourPractice3TheChangeWillBeLargeRdo,
                Bys.EnterACPDActivityDetailsPage.HowWillThisQuestionChangeYourPractice4TheChangeWillBeLargeRdo,
                Bys.EnterACPDActivityDetailsPage.DidYouPerceiveYesRdo,
                Bys.EnterACPDActivityDetailsPage.WhatTypeOfActivityPatientEncounterRdo,
                Bys.EnterACPDActivityDetailsPage.DoYouForeseeYesRdo,
                Bys.EnterACPDActivityDetailsPage.DidthisActivityYesRdo,
                Bys.EnterACPDActivityDetailsPage.ProfessionalActivitiesFirstRdo,
                Bys.EnterACPDActivityDetailsPage.UponWhatKindFirstRdo,
                Bys.EnterACPDActivityDetailsPage.TypeOfFeedback2FirstRdo,
                Bys.EnterACPDActivityDetailsPage.TypeOfFeedback1FirstRdo,







            };

        public static List<By> ChkElemBys = new List<By>()
            {
                Bys.EnterACPDActivityDetailsPage.PeerReviewedArticlesChk,
                Bys.EnterACPDActivityDetailsPage.WhatSourcesFirstChk,
                Bys.EnterACPDActivityDetailsPage.AssignedReadingChk,
                Bys.EnterACPDActivityDetailsPage.CollaboratorChk,
                Bys.EnterACPDActivityDetailsPage.ClinicalPreceptorChk,
                Bys.EnterACPDActivityDetailsPage.ConferenceOrWorkshopChk,


                
            };


        public static List<By> SelectElementBys = new List<By>()
            {
       //         Bys.EnterACPDActivityDetailsPage.IndicateYourRoleInThisActSelElem,
                Bys.EnterACPDActivityDetailsPage.ProvinceSeleElem,
                Bys.EnterACPDActivityDetailsPage.IdentifyTheTypeOfAssessmentActivitySelElem,
                Bys.EnterACPDActivityDetailsPage.DidYouParticipateInSelElem,
            };

        public static List<String> ScreenNamesWithBtnEnabledByDefault = new List<String>()
            {
               "canmedsfmroles",
            };

        public enum AdjustmentCodes
        {
            [Description("r")] // R-Remedial
            R,

            [Description("rr")] //RR-Remedial Reinstatement (Reinstatement)
            RR,

            [Description("P")] // P-PENDING (Voluntary)
            P,

            [Description("NC")] //NO CYCLE
            NC,

            [Description("L")]  // L-Leave
            L,

            [Description("AS")] // AS-RESIDENT (Resident)
            AS,

            [Description("AF")] // AF-AFFILIATE (Default)
            AF


        }

        public enum DesignationCode
        {
            [Description("PC")]
            PC
        }

        public enum ActivityCategory
        {
            [Description("Assessment (e.g. Being an examiner, Practice audits)")]
            Assessment,

            [Description("Group Learning (e.g. Courses, conferences, workshops)")]
            GroupLearning,

            [Description("Self-Learning (e.g. Journal reading, CFP articles, Online learning)")]
            SelfLearning
        }

        public enum ActivityCertType
        {
            [Description("Certified")]
            Certified,

            [Description("Non-Certified")]
            NonCertified
        }

        public enum ActivityFormat
        {
            [Description("Live In-person or Live Webcast")]
            Live,

            [Description("Online Self-Study")]
            Online
        }


        public enum ActivitySearchField
        {
            [Description("Program/Activity Title:")]
            ProgramActivityTitle,

            [Description("Session ID:")]
            SessionID,

            [Description("City:")]
            City,

            [Description("Activity Date:")]
            ActivityDate
        }

        public enum Table
        {
            [Description("Search Results")]
            ActivitySearchResults,

            [Description("AMA RCP Max Credit Form table")]
            AMARCPMaxCreditForm,

            [Description("Credit Summary tab, View, View Activities table")]
            CreditSummaryTabViewFormViewActivities,

            [Description("Credit Summary tab, View All Cycles, Cycle table")]
            CreditSummaryTabViewAllCyclesFormCycle,

            [Description("Credit Summary tab, Group Learning table")]
            CreditSummaryTabGroupLearn,

            [Description("Credit Summary tab, Self Learning table")]
            CreditSummaryTabSelfLearn,

            [Description("Credit Summary tab, Assessment table")]
            CreditSummaryTabAssessment,

            [Description("Credit Summary tab, Other table")]
            CreditSummaryTabOther,

            [Description("Credit Summary Widget, Cycle table")]
            CreditSummaryWidgetCycle,

            [Description("Credit Summary Widget, Current Year table")]
            CreditSummaryWidgetCurrentYear,

            [Description("Credit Summary Widget, Annual Requirements table")]
            CreditSummaryWidgetAnnualReqs,

            [Description("CPD Actitivites List page, Activity table")]
            CPDActitivitesListTabAct,

            [Description("Holding Area page, Summary tab Incomplete Activities table")]
            HoldingAreaSummTabInc,

            [Description("Holding Area page, Summary tab Pending Approcal table")]
            HoldingAreaSummTabPendAppr,

            [Description("Holding Area page, Incomplete tab, Incomplete Activities table")]
            HoldingAreaIncTabInc,

            [Description("Holding Area page, Credit Validation tab, Pending Approval table")]
            HoldingAreaCredValTabPendAppr,

            [Description("Planning page, Goal table")]
            PlanningTabGoal,

            [Description("PLP Hub Page, Completed PLP table")]
            PLPHubCompletedPLPTbl,

            [Description("Dashboard tab, Incomplete table")]
            DashboardTabInc,

            [Description("Dashboard tab, Pending Approval table")]
            DashbooardTabPendAppr,

            [Description("Dashboard tab, Goal table")]
            DashbooardTabGoal,

            [Description("PLP Step 3 page, Events table")]
            PLPStep3Events,

            [Description("PLP Step 3 page, Set Your CPD Goals, Selected Activities")]
            PLPStep3SetYourCPDGoalsSelectedActivities,
             [Description("PLP Step 3 page, PLP Summary Page, Identified Gaps Section")]
            PLPStep3IdentifiedGaps,

            [Description("PLP Step 5, PreReflection page, Selected and Added Activities in Step 3")]
            PLPStep5PreReflectionYourSelectedActivities,

            [Description("PLP Step 5, Useful CPD Programs page, Selected and Added Activities in Step 3")]
            PLPStep5UsefulCPDActivities,
        }

        public enum TableButtonLinkOrCheckBox
        {
            [Description("Complete Activity")]
            CompleteActivity,

            [Description("View")]
            View,

            [Description("View Details")]
            ViewDetails,

            [Description("Delete")]
            Delete,

            [Description("Edit")]
            Edit,

            [Description("SELECT")]
            Select,

            [Description("Checkbox")]
            CheckBox,

            [Description("Actions")]
            ActionsButton,
            [Description("Print Completed PLP")]
            PrintCompletedPLP,
                [Description("Print PLP Certificate")]
            PrintPLPCertificate,
                [Description("View Completed PLP")]
            ViewCompletedPLP
        }

        public enum TableRowNames
        {
            [Description("Certified")]
            Certified,

            [Description("Non-Certified")]
            NonCertified,

            [Description("Total")]
            Total,

            [Description("Certified Activities")]
            CertifiedActivities,

            [Description("Non-certified Activities")]
            NoncertifiedActivities,

            [Description("Carryover Certified Credit")]
            CarryoverCertifiedCredit,

        }

        public enum PLP_ActivityNames
        {
            [Description("Professional Learning Plan - Self-guided pathway")]
                SelfGuided,
            [Description ("Professional Learning Plan - Peer-supported pathway")]
            PeerSupported
        }

        public enum TableColumnNames
        {
            [Description("Required")]
            Required,

            [Description("Applied")]
            Applied,

            [Description("Requirement Met")]
            RequirementMet,

            [Description("Credits Applied")]
            CreditsApplied,

            [Description("Last Updated")]
            LastUpdate,

            [Description("Certified Applied")]
            CertifiedApplied,

            [Description("Total Applied Credits")]
            TotalAppliedCredits,

        }





        /// <summary>
        /// Represents the activities in the Activity Type Select Element. Format is as follows: 
        /// 1. The first word before the underscore represents the Category of the activity. 
        /// 2. The second word represents the Certification Type. 
        /// 3. The third word represents the actual activity type title
        /// 4. "L" or "LO" is appended if the activity includes both Live and/or Online version. 
        /// 5. "FC#" "DC#" "MCC#" "V" is appended if the activity provides a Fixed, Default, Multiple Choice or 
        /// Varied amount of credits, as opposed to entering the amount of credits yourself. 
        /// 6. "Max#" is appended if the activity only allows for a certain amount of maximum credits
        /// 7. VR is appended if the activity requires validation. 
        /// 8. DR is appended if it requires documentation. 
        /// For more info about these activities,  see "CFPC CPD Activities Business Rules v22_withFr_RecCodes.xlsx" at the 
        /// following link: 
        /// https://code.premierinc.com/docs/display/CFPC/CFPC+Documentation
        /// </summary>
        public enum ActivityType
        {
            [Description("AAFP and ABFM Activities")]
            ASMT_CERT_AAFPandABFMActivities_L_FC30,

            [Description("Accreditation surveyor")]
            ASMT_CERT_Accreditationsurveyor_Max1,

            [Description("CFPC Certified Mainpro+ Activities")]
            ASMT_CERT_CFPCCertifiedMainproActivities_LO,

            [Description("CFPC peer tutor (e.g. ARC or Pearls.ce tutor)")]
            ASMT_CERT_CFPCpeertutorARCorPearlscetutor_Max15,

            [Description("Clinical Supervisor")]
            ASMT_CERT_ClinicalSupervisor_Max30_VR_DR,

            [Description("CPCSSN Quality Assurance Program")]
            ASMT_CERT_CPCSSNQualityAssuranceProgram_L_D6_Max6_VR,

            [Description("Examiner for medical exams")]
            ASMT_CERT_Examinerformedicalexams_Max50,

            [Description("Family or Emergency Medicine Examinations")]
            ASMT_CERT_FamilyorEmergencyMedicineExaminations_FC50_VR,

            [Description("International CPD Activities (Individual Consideration)")]
            ASMT_CERT_InternationalCPDActivitiesIndividualConsideration_VR_DR,

            [Description("Linking Learning to Administration")]
            ASMT_CERT_LinkingLearningtoAdministration_FC5_Max5_VR,

            [Description("Linking Learning to Assessment")]
            ASMT_CERT_LinkingLearningtoAssessment_FC5_Max5_VR,

            [Description("MCC360")]
            ASMT_CERT_MCC360_L_MC12or15_VR_DR,

            [Description("Other CFPC Certified Mainpro+ Assessment Activities")]
            ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,

            [Description("Pearls™")]
            ASMT_CERT_Pearls_FC6_VR,

            [Description("Practice Audits/Quality Assurance Programs")]
            ASMT_CERT_PracticeAuditsQualityAssurancePrograms_FC6_VR,

            [Description("Provincial Practice Review and Enhancement Programs")]
            ASMT_CERT_ProvincialPracticeReviewandEnhancementPrograms_FC6_VR,

            [Description("Quebec Category 1 Credit")]
            ASMT_CERT_QuebecCategory1Credit_LO,

            [Description("Royal College MOC Accredited Section 3")]
            ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO,

            [Description("Family Medicine Curriculum Review")]
            ASMT_NONCERT_FamilyMedicineCurriculumReview,

            [Description("Manuscript Review for Medical Journals")]
            ASMT_NONCERT_ManuscriptReviewforMedicalJournals,

            [Description("Other Non-Certified Assessment Activities")]
            ASMT_NONCERT_OtherNonCertifiedAssessmentActivities_LO_VR,

            [Description("Practice Audits")]
            ASMT_NONCERT_PracticeAudits_L,

            [Description("Review of Clinical Practice Guidelines")]
            ASMT_NONCERT_ReviewofClinicalPracticeGuidelines_L,

            [Description("Royal College MOC Section 3")]
            ASMT_NONCERT_RoyalCollegeMOCSection3_LO,

            [Description("AAFP and ABFM Activities")]
            GRPLRNING_CERT_AAFPandABFMActivities_L,

            [Description("Advanced Life Support Programs (Participant)")]
            GRPLRNING_CERT_AdvancedLifeSupportProgramsParticipant_L,

            [Description("American Medical Association (AMA) PRA Category 1")]
            GRPLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO,

            [Description("CFPC Certified Mainpro+ Activities")]
            GRPLRNING_CERT_CFPCCertifiedMainproActivities,

            [Description("Foundation for Medical Practice Education (FMPE)")]
            GRPLRNING_CERT_FoundationforMedicalPracticeEducationFMPE_LO,

            [Description("International CPD Activities (Individual Consideration)")]
            GRPLRNING_CERT_InternationalCPDActivitiesIndividualConsideration_O_VR_DR,

            [Description("MOREᴼᴮ Plus Program")]
            GRPLRNING_CERT_MOREPlusProgram_LO,

            [Description("Other CFPC Certified Mainpro+ Group Learning Activities")]
            GRPLRNING_CERT_OtherCFPCCertifiedMainproGroupLearningActivities_LO,

            [Description("Quebec Category 1 Credit")]
            GRPLRNING_CERT_QuebecCategory1Credit_LO,

            [Description("Royal College MOC Accredited Section 1")]
            GRPLRNING_CERT_RoyalCollegeMOCAccreditedSection1_LO,

            [Description("AAFP and ABFM Elective Activities")]
            GRPLRNING_NONCERT_AAFPandABFMElectiveActivities_LO,

            [Description("American College of Emergency Physicians (ACEP)")]
            GRPLRNING_NONCERT_AmericanCollegeofEmergencyPhysiciansACEP_LO,

            [Description("American Medical Association (AMA) PRA Category 1")]
            GRPLRNING_NONCERT_AmericanMedicalAssociationAMAPRACategory1_LO,

            [Description("Other Non-Certified Group Learning Activities")]
            GRPLRNING_NONCERT_OtherNonCertifiedGroupLearningActivities_LO_VR,

            [Description("Royal College MOC Section 1")]
            GRPLRNING_NONCERT_RoyalCollegeMOCSection1_LO,

            [Description("AAFP and ABFM Activities")]
            SELFLRNING_CERT_AAFPandABFMActivities_L,

            [Description("American Medical Association (AMA) PRA Category 1")]
            SELFLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO,

            [Description("CFP Mainpro+ Articles")]
            SELFLRNING_CERT_CFPMainproArticles_V,

            [Description("CFPC Certified Mainpro+ Activities")]
            SELFLRNING_CERT_CFPCCertifiedMainproActivities_V,

            [Description("COVID-19 Learning On The Go")]
            SELFLRNING_CERT_COVID19LearningOnTheGo_D2_Max2_VR,

            [Description("Formal Clinical Traineeship/Fellowship")]
            SELFLRNING_CERT_FormalClinicalTraineeshipFellowship_D50_Max50_VR_DR,

            [Description("Formal Studies University Degree/Diploma Programs")]
            SELFLRNING_CERT_FormalStudiesUniversityDegreeDiplomaPrograms_FC20_VR_DR,

            [Description("Foundation for Medical Practice Education (FMPE)")]
            SELFLRNING_CERT_FoundationforMedicalPracticeEducationFMPE_LO,

            [Description("International CPD Activities (Individual Consideration)")]
            SELFLRNING_CERT_InternationalCPDActivitiesIndividualConsideration_VR_DR,

            [Description("Linking Learning to Practice")]
            ASMT_CERT_LinkingLearningtoPractice_FC5_Max5_VR,

            [Description("Linking Learning to Research")]
            ASMT_CERT_LinkingLearningtoResearch_FC5_Max5_VR,

            [Description("Linking Learning to Teaching")]
            ASMT_CERT_LinkingLearningtoTeaching_FC5_Max5_VR,

            [Description("Office use only")]
            SELFLRNING_CERT_Officeuseonly,

            [Description("Other CFPC Certified Mainpro+ Self-Learning Activities")]
            SELFLRNING_CERT_OtherCFPCCertifiedMainproSelfLearningActivities_LO,

            [Description("Quebec Category 1 Credit")]
            SELFLRNING_CERT_QuebecCategory1Credit_LO,

            [Description("Self Learning Program Impact Assessment")]
            SELFLRNING_CERT_SelfLearningProgramImpactAssessment_FC5,

            [Description("UpToDate®")]
            SELFLRNING_CERT_UpToDate,

            [Description("AAFP Self Learning Activities")]
            SELFLRNING_NONCERT_AAFPSelfLearningActivities,

            [Description("American Medical Association (AMA) PRA Category 1")]
            SELFLRNING_NONCERT_AmericanMedicalAssociationAMAPRACategory1_LO,

            [Description("Committee Participation (educational planning)")]
            SELFLRNING_NONCERT_CommitteeParticipationeducationalplanning,

            [Description("Curriculum Development (medical education/professional development)")]
            SELFLRNING_NONCERT_CurriculumDevelopmentmedicaleducationprofessionaldevelopment,

            [Description("Journal Reading")]
            SELFLRNING_NONCERT_JournalReading_O,

            [Description("Manuscript Submission")]
            SELFLRNING_NONCERT_ManuscriptSubmission,

            [Description("Online (eLearning, podcasts, etc.)")]
            SELFLRNING_NONCERT_OnlineeLearningpodcastsetc,

            [Description("Other Non-Certified Self Learning Activities")]
            SELFLRNING_NONCERT_OtherNonCertifiedSelfLearningActivities_LO_VR,

            [Description("Preparing Papers for Publication")]
            SELFLRNING_NONCERT_PreparingPapersforPublication,

            [Description("Research")]
            SELFLRNING_NONCERT_Research,

            [Description("Teaching/presenting (clinical and/or academic)")]
            SELFLRNING_NONCERT_Teachingpresentingclinicalandoracademic,

            [Description("Tutoring")]
            SELFLRNING_NONCERT_Tutoring
        }

        public enum PLP_Step3_plpReviewContainerText
        {
            [Description("Review the likelihood of achieving your" +
                " goal(s) based on the timeframe you selected.")]
            Forselfguided,

            [Description("Review the likelihood of achieving your" +
                " goal(s) with your peer/colleague based on the timeframe you selected.")]
            ForPeer
        }
        public enum PLP_Step3_goalTimelineContentText
        {
            [Description("You can set the timeframe for these goals " +
                "based on your CPD plan to address your stated gaps and will be able" +
                " to adjust these once you view your PLP Activity Summary." +
                " However, once you leave Step 4, you cannot edit steps 1 to 4," +
                " including your goal-setting timeline. You will be prompted when" +
                " it’s time to lock in your answers.")]
            Forselfguided,

            [Description("You can set the timeframe for these goals" +
                " based on your CPD plan to address your stated gaps and will be" +
                " able to adjust these once you speak with your peer. " +
                "However, once you leave Step 4, you cannot edit steps 1 to 4," +
                " including your goal-setting timeline. You will be prompted" +
                " when it’s time to lock in your answers.")]
            ForPeer
        }

        public enum PLP_TextboxlabelText
        {
            [Description("None")]
            None=0,
            [Description("Gap #1")]
            Step2Gap1TitleTxt, 
            [Description("Gap #2")]
            Step2Gap2TitleTxt, 
            [Description("Gap #3")]
            Step2Gap3TitleTxt, 
            [Description("Goal 1 Title")]
            Step3Goal1TitleTxt, 
            [Description("Goal 2 Title")]
            Step3Goal2TitleTxt,
            [Description("Goal 3 Title")]
            Step3Goal3TitleTxt,
            [Description("Specific")]
            Step3_SMARTGoal_SpecificTxt,
            [Description("Measurable")]
            Step3_SMARTGoal_MeasurableTxt,
            [Description("Achievable")]
            Step3_SMARTGoal_AchievableTxt,
            [Description("Realistic")]
            Step3_SMARTGoal_RealisticTxt,
             [Description("Timely")]
            Step3_SMARTGoal_TimelyTxt,
            [Description("Please write..")]
            PleasewriteTxt,
            [Description("Please write [max 1000 characters]")]
            Pleasewritemax1000Txt, 
            [Description("Please write: [max 1000 characters]")]
            Pleasewritecolonmax1000Txt,
            [Description("Commitment to change statement that addresses your goal(s): Please write [max 1000 characters]")]
            Step4CTCTxt,

            
            [Description("Example: I am going to review all my patients over age 55 according to diabetic " +
                "guidelines or I am going to make improvements in compliance/efficiency.")]
            Step3_GoalOutcomeReviewTxt,
            [Description("Example: a patient survey completed identified a shift in compliance.")]
            Step3_GoalOutcomeSurveyTxt,
            [Description("Example: logistical, organizational, system changes, etc.")]
            Step3_GoalOutcomeLogisticalTxt,


        }



    }


}
