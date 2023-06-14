using System.Collections.Generic;
using System.Configuration;
//using System.ComponentModel.DataAnnotations; // need to add a nuget package (System.ComponentModel.DataAnnotations) for this for it to work. https://stackoverflow.com/questions/10174420/why-cant-i-reference-system-componentmodel-dataannotations
using System.ComponentModel;
using System;
using System.Data;
using Browser.Core.Framework;
using Browser.Core.Framework;

namespace LMS.Data
{
    /// <summary>
    /// Constants for all core-platform LMS applications
    /// </summary>
    public static class Constants
    {
        public static readonly string CurrentEnvironment = AppSettings.Config["environment"];

        #region activity objects

        /// <summary>
        /// 
        /// </summary>
        public class AssQAndAs
        {
            public int QuestionId { get; set; }
            public string QuestionText { get; set; }
            public string QuestionTypeName { get; set; }
            //public string Value { get; set; }
            public bool IsSelected { get; set; }
            public bool IsGraded { get; set; }
            public bool IsRequired { get; set; }
            public int CorrectOrderSeq { get; set; }
            //public string ChoiceTag { get; set; }
            //public Nullable<Int32> ChoiceOrderSeq { get; set; }
            public bool IsOther { get; set; }
            public string OtherValue { get; set; }
            public string Feedback { get; set; }
            public int QuestionOrderId { get; set; }
            public string AnswerID { get; set; }
            public string AnswerText { get; set; }
            public bool IsCorrect { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Assessments
        {
            public int ScenarioAssessmentId { get; set; }
            public int ScenarioId { get; set; }
            public string ScenarioName { get; set; }
            public int AssessmentId { get; set; }
            public string AssessmentName { get; set; }
            public string AssessmentTitle { get; set; }
            public string AssessmentDescription { get; set; }
            public int FormId { get; set; }
            public Nullable<Int32> FormInstanceId { get; set; }
            public int AccreditationBodyTypeId { get; set; }
            public string AccreditationBodyTypeName { get; set; }
            public bool? IsFormProcessed { get; set; }
            public string Score { get; set; }
            public string Result { get; set; }
            public string Status { get; set; }
            public string IsRequired { get; set; }
            public string IsEnabled { get; set; }
            public Nullable<Int32> AttemptCount { get; set; }
            public Nullable<Int32> AttemptsAllowed { get; set; }
            public Nullable<Int32> AttemptedAttempts { get; set; }
            public int AssessmentType { get; set; }
            public string AssessmentTypeName { get; set; }
            public decimal MinimumPercentageCorrect { get; set; }
        }

        public enum QuestionTypeName
        {
            [Description("DropDown")]
            DropDown,

            [Description("TextLabel")]
            TextLabel,

            [Description("ChoiceOneAnswer")]
            ChoiceOneAnswer,

            [Description("RatingScaleMatrix")]
            RatingScaleMatrix,

            [Description("ChoiceMultipleAnswers")]
            ChoiceMultipleAnswers,

            [Description("DatePicker")]
            DatePicker,

            [Description("RadioButton")]
            RadioButton,

            [Description("TextOneAnswer")]
            TextOneAnswer,

            [Description("TextOneAnswerMultiLine")]
            TextOneAnswerMultiLine,
        }

        public enum Environments
        {
            [Description("UAT")]
            UAT,

            [Description("CMEQA")]
            CMEQA,

            [Description("Production")]
            Production,
        }

        /// <summary>
        /// 
        /// </summary>
        public class Transcript
        {
            public string ActivityTitle { get; set; }
            public string ActivityType { get; set; }
            public string CityStateCountry { get; set; }
            public string CreditBody { get; set; }
            public string CreditAmountAndUnit { get; set; }
            public string CompletionDate { get; set; }
            public bool CertificateGenerated { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class EarnedCredit
        {
            public string ActivityTitle { get; set; }
            public string ActivityType { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string Provider { get; set; }
            public string Credit { get; set; }
            public string Amount { get; set; }
            public string CompletionDate { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class ActivityInProgress
        {
            public string ActivityTitle { get; set; }
            public string ActivityType { get; set; }
            public string Address { get; set; }
            public string ExpirationDate { get; set; }
            public List<string> Credit { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Activity
        {
            public string ActivityTitle { get; set; }
            public string ActivityType { get; set; }
            public string FrontMatter { get; set; }
            public AddressAndLocation AddressAndLocation { get; set; }
            public List<Accreditation> Accreditations { get; set; }
            public string Release_Date { get; set; }
            public string Expiration_Date { get; set; }
            public string CompletionDate { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class MyActivity
        {
            public string ActivityName { get; set; }
            public string CompletedDate { get; set; }
            public string AccreditationProvider { get; set; }
            public string ReferenceNumber { get; set; }
            public int CreditAmount { get; set; }
            public string Units { get; set; }
            public string CreditType { get; set; }
            public string Location { get; set; }

        }

        public class AddressAndLocation
        {
            public string Addr_Line_01 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string Country { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        public enum AssessmentElementLabels
        {
            StatusPassFailLbl,

            YourStatusValueLbl,

            NumOfAttemptsLeftValueLbl,

            LaunchBtn
        }

        public class Accreditation
        {
            public string BodyName { get; set; }
            public double CreditAmount { get; set; }
            public string CreditUnit { get; set; }
            //public List<Credit> Credits { get; set; }
        }

        //public class Credit
        //{
        //    public int Amount { get; set; }
        //    public string Unit { get; set; }

        //    public Credit(int amount, string unit)
        //    {
        //        Amount = amount;
        //        Unit = unit;
        //    }   
        //}

        #endregion activity objects

        #region site differences

        /// <summary>
        /// Site differences: searching
        /// </summary>
        public static List<string> SitesWith_SearchByType = new List<string>() { "aapa", "aha", "cmeca", "nof" };
        public static List<string> SitesWith_SearchByCategory = new List<string>() { "ncpalc" };
        public static List<string> SitesWith_SearchTypeDropDown = new List<string>() { "aha" };
        public static List<string> SitesWith_SearchTypeRadioButtons = new List<string>() { "aapa", "cmeca", "nof" };

        public static List<string> SitesWith_Bins = new List<string>() { "aafprs", "aapa", "acr", "aha", "asnc", "cap",
            "cmcd", "cmeca", "cvsce", "isuog", "ncpalc", "nof", "riteaid", "snmmi", "wiley"};

        public static List<string> SitesWith_SearchSort = new List<string>() { "aapa", "asnc", "cap",
            "cmcd", "cmeca", "cvsce", "dhmc", "isuog", "nof", "riteaid", "snmmi", "wiley"};

        /// <summary>
        /// Site differences: URL
        /// </summary>
        public static List<string> SitesWith_SearchURLIsEducation = new List<string>() { "aafprs", "aapa", "asnc", "cmcd", "cmeca",
            "cvsce", "dhmc", "isuog", "ncpalc", "nof", "riteaid", "wiley" };
        public static List<string> SitesWith_SearchURLIsCatalog = new List<string>() { "aha", "snmmi" };
        public static List<string> SitesWith_SearchURLIsLearningSearch = new List<string>() { "acr", "cap" };

        /// <summary>
        /// Site differences: User Registration
        /// </summary>
        public static List<string> SitesWith_EmailAndUsernameMustBeTheSame = new List<string>() { "uams" };
        public static List<string> SitesWith_UsernameCanNotContainEmail = new List<string>() { "onslt" };


        /// <summary>
        /// Site differences: Page navigation post-login
        /// </summary>
        public static List<string> SitesWith_PostLoginPageIsHome = new List<string>() 
        { "cap", "uams", "onslt", "cmeca", "dhmc" };
        public static List<string> SitesWith_PostLoginPageIsCatalog = new List<string>() { "aha" };
        public static List<string> SitesWith_PostLoginPageIsCustom = new List<string>() { "asnc", "acr", "snmmi" };

        #endregion site differences

        #region activity general

        public enum Pages_ActivityPage
        {
            Assessment,

            Bundle,

            ClaimCredit,

            Material,

            OnHold,

            Sessions
        }

        public enum Page
        {
            Login, 

            ActivitiesInProgress,

            Transcript
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ActStage
        {
            UnderConstruction,
            UnderReview,
            ConstructionComplete
        }

        public enum ProjectType
        {
            [Description("Blank Project")]
            BlankProject,

            [Description("Exam Activity")]
            ExamActivity,

            [Description("Live Meeting")]
            LiveMeeting,

            [Description("Maintenance of Certification")]
            MaintenanceofCertification,

            [Description("Measure Set")]
            MeasureSet,

            [Description("Measure Set Container")]
            MeasureSetContainer,

            [Description("Patient Survey Activity")]
            PatientSurveyActivity,

            [Description("Performance Improvement Module")]
            PerformanceImprovementModule,

            [Description("Quality Improvement Project")]
            QualityImprovementProject,

            [Description("Recognition Program Activity")]
            RecognitionProgramActivity,

            [Description("Stand-alone Activity")]
            StandaloneActivity,

            [Description("Stand-alone Live Meeting")]
            StandaloneLiveMeeting,
        }

        public enum ActType
        {
            [Description("Blank Project")]
            BlankProject,

            [Description("Exam Activity")]
            ExamActivity,

            [Description("Live Meeting")]
            LiveMeeting,

            [Description("Maintenance of Certification")]
            MaintenanceofCertification,

            [Description("Measure Set")]
            MeasureSet,

            [Description("Measure Set Container")]
            MeasureSetContainer,

            [Description("Patient Survey Activity")]
            PatientSurveyActivity,

            [Description("Performance Improvement Module")]
            PerformanceImprovementModule,

            [Description("Quality Improvement Project")]
            QualityImprovementProject,

            [Description("Recognition Program Activity")]
            RecognitionProgramActivity,

            [Description("Stand-alone Activity")]
            StandaloneActivity,

            [Description("Stand-alone Live Meeting")]
            StandaloneLiveMeeting,
        }

        public enum QuestionTypes
        {
            [Description("Multiple Choice Single Answer")]
            MultipleChoiceSingleAnswer,

            [Description("Multiple Choice with Drop Down")]
            MultipleChoicewithDropDown,

            [Description("Multiple Choice with Multiple Answers")]
            MultipleChoicewithMultipleAnswers,
        }

        public enum AssessmentTypes
        {
            [Description("Pre-Test Assessment")]
            PreTestAssessment,

            [Description("Post-Test Assessment")]
            PostTestAssessment,

            [Description("Evaluation")]
            Evaluation,
        }


        public enum ActTitle
        {
            [Description("Automation - Stand Alone Activity")]
            Automation_Stand_Alone_Activity,

            [Description("Activity - Assessments - All Question with Feedback settings")]
            Activity_Assessments_All_Question_with_Feedback_settings,

            [Description("March Access Srilu007")]
            March_Access_Srilu007,

            [Description("Automation - Registration - 11. On Hold")]
            Automation_Registration_11_OnHold,

            [Description("Automation - Registration - 11.1 On Hold Expired")]
            Automation_Registration_11_1_OnHold,

            [Description("Automation - 16.0 Live Meeting in San Diego")]
            Automation_16_0_Live_Meeting_in_San_Diego,

            [Description("Automation - 12 Activity Overview with Consent Form (One Page)")]
            Automation_12_ActivityOverviewWithConsentForm_OnePage,

            [Description("Automation - Assessment - 12 Pre-Assessment (Page Break)")]
            Automation_Assessment_12_PreAssessment_PageBreak,

            [Description("Automation - Assessment - 12 Post-Assessment (One Page)")]
            Automation_Assessment_12_PostAssessment_OnePage,

            [Description("Automation - Assessment - 12 Evaluation (Multiple)")]
            Automation_Assessment_12_Evaluation__Multiple,

            [Description("Automation - Assessment - 12 Follow-Up Assessment (One Page)")]
            Automation_Assessment_12_FollowUpAssessment_OnePage,

            [Description("Automation - Assessment - 1.1. Initial Stage: 1 Hide, 2 Show, 3 Disable, 4 Enable, 5. Default Answer, 6. Disable Answer, 7. Enable Answer No Gender or Profession")]
            AutomationAssessment_1_1_InitialStage1Hide2Show3Disable4Enable5DefaultAnswer6DisableAnswer7EnableAnswer,           

            [Description("Activity - $pecial Ch@rs")]
            Activity_Special_Chars,

            [Description("Activity - $pecial Ch@rs (2)")]
            Activity_Special_Chars_2,

            [Description("Automation - Assessments - All Questions with Feedback")]
            Automation_Assessments_All_Questions_with_Feedback,

            [Description("Automation - Assessment - 3. Multiple Assessments")]
            Automation_Assessments_3_Multiple_Assessments,

            [Description("Automation - Assessment - 3. Multiple Assessments - No Gender or Profession")]
            Automation_Assessments_3_Multiple_Assessments_No_Geder_Or_Profession,

            [Description("Automation - Assessment - 1. One Page - No Gender or Profession")]
            Automation_Assessment_1_One_Page_No_Gender_Or_Profession,

            [Description("Automation - 12 Activity Overview with Consent Form (One Page)")]
            Automation_12_ActivityOverview_with_ConsentForm_OnePage,

            [Description("Automation - 12 Activity Overview No Consent Form - One Page")]
            Automation_12_ActivityOverview_NoConsentForm_OnePage,

            [Description("NIH Stroke Scale - Test Group A")]
            NIH_Stroke_Scale_Test_Group_A,

            [Description("NIH Stroke Scale - Test Group B")]
            NIH_Stroke_Scale_Test_Group_B,

            [Description("NIH Stroke Scale - Test Group C")]
            NIH_Stroke_Scale_Test_Group_C,

            [Description("NIH Stroke Scale - Test Group D")]
            NIH_Stroke_Scale_Test_Group_D,

            [Description("NIH Stroke Scale - Test Group E")]
            NIH_Stroke_Scale_Test_Group_E,

            [Description("NIH Stroke Scale - Test Group F")]
            NIH_Stroke_Scale_Test_Group_F,

            [Description("Automation - Assessment - 3. Multiple Assessments - No Gender or Profession")]
            Automation_Assessments_3_Multiple_Assessments_NoGenderOrProfession,

            [Description("Automation - Assessments - All Questions with Feedback - No GenderOrProfession")]
            Automation_Assessments_All_Questions_with_Feedback_NoGenderOrProfession,

            [Description("Automation - Bundle - Access Code and Payment Required, One Child Required")]
            Automation_Bundle_Access_Code_And_Payment_Required_One_Child_Required,

            [Description("Automation - Bundle Child - Access Code and Payment Required, One Child Required - Access")]
            Automation_Bundle_Child_Access_Code_And_Payment_Required_One_Child_Required_Access,

            [Description("Automation - Bundle Child - Access Code and Payment Required, One Child Required - Pay")]
            Automation_Bundle_Child_Access_Code_And_Payment_Required_One_Child_Required_Payment,

            [Description("Automation - Bundle - Access Code and Payment Not Required, One Child required")]
            Automation_Bundle_Access_Code_And_Payment_Not_Required_One_Child_Required,

            [Description("Automation - Bundle - Hide from Activities in Progress")]
            Automation_Bundle_Hide_From_Activities_In_Progress,

            [Description("Automation - 17 Credit Claim: 0 Set up at Activity Level")]
            Automation_CreditClaim_170_SetUpAtActivityLevel,

            [Description("Automation - Claim Credit - 17.1 Maximum, Minimum with and without Increment (AHA)")]
            Automation_ClaimCredit_171_MaximumMinimumwithandwithoutIncrement_AHA,

            [Description("Automation - Claim Credit - 17.1 Maximum, Minimum with and without Increment (CAP)")]
            Automation_ClaimCredit_171_MaximumMinimumwithandwithoutIncrement_CAP,

            [Description("Automation - Claim Credit - 17.2 Expired Credits (AHA)")]
            Automation_ClaimCredit_172_Expired_Credits_AHA,

            [Description("Automation - Claim Credit - 17.3 Profession Specific")]
            Automation_ClaimCredit_173_ProfessionSpecific,


            [Description("Automation - Bundle Child - Hide from Activities in Progress")]
            Automation_Bundle_Child_Hide_From_Activities_In_Progress,

            [Description("Automation - Bundle - Price Includes All")]
            Automation_Bundle_Price_Includes_All,

            [Description("Automation - Bundle Child - Price Includes All")]
            Automation_Bundle_Child_Price_Includes_All,

            [Description("Automation - Bundle - Hide from Transcript")]
            Automation_Bundle_Hide_From_Transcript,

            [Description("Automation - Bundle Child - Hide from Trascript")]
            Automation_Bundle_Child_Hide_From_Trascript,

            [Description("Automation - Content - ALL (URL Embedded, Mobile, Non Mobile), (Embedded - PDF, Audio, Video, JPEG), (Not Embedded - Everything else)")]
            Automation_Content_All,

            [Description("Automation - 14. Activity Content - 4 Body Interact & Impelysis (AHA)")]
            Automation_14_Activity_Content_4BodyInteractImpelysis,

            [Description("Automation - Content - SCORM")]
            Automation_Content_SCORM,

            //[Description("Automation - Registration - Access Code Customized (Srilu007, sramayan)")]
            //Automation_Registration_Access_Code_Customized_Srilu007_sramayan,
            [Description("Automation - Registration - 7. Access Code, Registration Page, CPE Monitor, Payment, On Hold")]
            Automation_Registration_Access_Code_RegistrationPage_CPEMonitor_Payment_OnHold,

            [Description("Automation - Registration - 6. Profession Should Match (Physician)")]
            Automation_Registration_Profession_Should_Match_Physician,

            [Description("Automation - Registration - 3.1 Cut Off Date in Future")]
            Automation_Registration_Cut_off_Date_In_The_Future,

            [Description("Automation - Registration - 3. Cut Off Date in the Past")]
            Automation_Registration_Cut_off_Date_In_The_Past,

            [Description("Automation - Registration - 2. Online Registration Disabled")]
            Automation_Registration_Online_Registration_Disabled,

            [Description("Automation - Registration - 1. Expired")]
            Automation_Registration_Expired,

            [Description("Auromation - Registration - 8. Registration Page, 10. Payment Discounted (AHA)")]
            Auromation_Registration_RegistrationPage_Payment_Discounted_AHA,

            [Description("Automation - Registration - Expired Activity with Disabled Online Registration")]
            Automation_Registration_Expired_Activity_with_Disabled_Online_Registration,

            [Description("Automation - Registration - 4. Maximum Users allowed = 1")]
            Automation_Registration_Maximum_Users_Allowed_1,

            [Description("Automation - Registration - 5. Prerequisites need Completion")]
            Automation_Registration_Prerequisites_Need_Completion,

            [Description("Automation - Session - Unlimited Credits - Disabled Session Selection")]
            Automation_Session_Unlimited_Credits_Disabled_Session_Selection,

            [Description("Automation - Registration - 8. Registration Form with Skip Logic with On Hold expired.")]
            Automation_Registration_Form_With_Skip_Logic_With_On_Hold_Expired,

            [Description("Automation - Registration - 8. Registration Form with Skip Logic No Hold")]
            Automation_Registration_Form_With_Skip_Logic_No_Hold,

            [Description("Automation - Claim Credit - 17.1 Maximum, Minimum with and without Increment (ASNC)")]
            Automation_ClaimCredit_171_MaximumMinimumwithandwithoutIncrement_ASNC,

            [Description("Automation - Registration - 7 Access Code")]
            Reg_7_1_Access_Code_Only,

            [Description("Automation - 11 Registration - 7.Access Code, 8.Registration Page, 9.CPE Monitor, 10.Payment, 11.On Hold (UAMS)")]
            Automation_Registration_7AccessCode_8RegistrationPage_9CPEMonitor_10Payment_11OnHold,

            [Description("Automation - Registration - 10 Payment Needed 2 (CMECA)")]
            Automation_Registration_10PaymentNeeded2_CMECA,
            

            [Description("Automation - Registration - 9. CPE Monitor (Pharmacist only)")]
            Automation_Registration_9CPEMonitor_PharmacistOnly,

            [Description("Automation - 11 Registration - 7. Access Code 8. Registration Form 10. Payment 11.1 On Hold expired (UAMS)")]
            Automation_Registration_7AccessCode_8RegistrationPage_10Payment_111OnHoldExpired,

            [Description("Automation - Registration - 8.1 Registration Form with Skip Logic and Formatting")]
            Automation_Registration_8_Registration_Form_with_Skip_Logic_and_Formatting,

            [Description("Automation - Registration - 10.0 Payment with Discounts")]
            Automation_Registration_10_Payment_with_Discounts,

            // Sessions
            [Description("Automation - Session - 16.1 Disabled Session Selection with Activity Content - Unlimited Credits")]
            Automation_Session_16_1_DisabledSessionSelection_With_Activity_Content_UnlimitedCredits,

            [Description("Automation - Session - 16.2 Single Session Selection with Activity Content - Unlimited Credits")]
            Automation_Session_16_2_SingleSessionSelection_With_Activity_Content_UnlimitedCredits,

            [Description("Automation - Session - 16.3 Access Code - Unlimited Credits")]
            Automation_Session_16_3__AccessCode_UnlimitedCredits,

            [Description("Automation - Session - 16.4 Overlap NOT Allowed - Unlimited Credits")]
            Automation_Session_16_4_Overlap_Not_Allowed_UnlimitedCredits,

            [Description("Automation - Session - 16.5 Overlap Allowed - Unlimited Credits")]
            Automation_Session_16_5_Overlap_Allowed_UnlimitedCredits,

            // OLD. REMOVE THESE LATER
            //[Description("Registration - Fees Discounted")]
            //Registration_FeesDiscounted,

            //[Description("Automation - Registration - Reg Page Only")]
            //Automation_Registration_Reg_Page_Only,

            //[Description("Automation - Session - Unlimited Credits - Access Code, Overlap Allowed")]
            //Automation_Session_Unlimited_Credits_Access_Code_Overlap_Allowed,

            //[Description("Automation - Session - Overlap Not Allowed")]
            //Automation_Session_Overlap_Not_Allowed,

            //[Description("Automation - Session - Unlimited Credits - Single Session Selection")]
            //Automation_Session_Unlimited_Credits_Single_Session_Selection,

            //[Description("Automation - Session - Unlimited Credits - Disabled Session Selection with Activity Content")]
            //Automation_Session_Unlimited_Credits_Disabled_Session_Selection_with_Activity_Content,

            //[Description("Front page Eval and Award")]
            //Front_Page_Eval_and_Award,

            //[Description("OverviewPage Pre Post Eval and Award")]
            //OverviewPage_Pre_Post_Eval_and_Award,
        }

        public enum Sessions
        {
            [Description("January Session 1, Access Code - Srilu007, AUTO123")]
            January_Session_1_AccessCode_Srilu007_AUTO123,

            [Description("January Session 1")]
            January_Session_1,

            [Description("January Session 2")]
            January_Session_2,

            [Description("January Session 2 URLs should open in new tabs")]
            January_Session_2_URLs_should_open_in_new_tabs,

            [Description("January Session 1 Downloadable content")]
            January_Session_1_Downloadable_content
        }

        public enum ActContentType
        {
            [Description("PDF")]
            PDF,

            [Description("JPEG")]
            JPEG,

            [Description("Video")]
            Video,

            [Description("Audio")]
            Audio,

            [Description("HTML")]
            HTML,

            [Description("URL")]
            URL,

            [Description("Word")]
            Word,

            [Description("Excel")]
            Excel,

            [Description("PPT")]
            PPT,

            [Description("TEXT")]
            TEXT,

            [Description("PNG")]
            PNG,

            [Description("Google")]
            Google,
        }


        #endregion  activity general

        #region organization/site general

        public enum OrgCodes
        {
            [Description("ABAM")]
            ABAM,

            [Description("AHA")]
            AHA,

            [Description("CAP")]
            CAP,

            [Description("UCD")]
            UCD,

            [Description("UAMS")]
            UAMS,

            [Description("PREM")]
            PREM,

            [Description("CECITY2")]
            Cecity2,
        }

        public enum SiteCodes
        {
            [Description("ama")]
            AMA,

            [Description("aapa")]
            AAPA,

            [Description("uams")]
            UAMS,

            [Description("awhonn")]
            AWHONN,

            [Description("ilms")]
            BREAKTHROUGH,
            
            [Description("tp")]
            TP,

            [Description("prem")]
            PREM,

            [Description("dhmc")]
            DHMC,

            [Description("aafprs")]
            AAFPRS,

            [Description("aafppn")]
            AAFPPN,

            [Description("asnc")]
            ASNC,

            [Description("abam")]
            ABAM,

            [Description("aboto")]
            ABOTO,

            [Description("acr")]
            ACR,

            [Description("aha")]
            AHA,

            [Description("cap")]
            CAP,

            [Description("cfpc")]
            CFPC,

            [Description("cmeca")]
            CMECA,

            [Description("cmcd")]
            CMCD,

            [Description("cvsce")]
            CVSCE,

            [Description("isuog")]
            ISUOG,

            [Description("medconcert")]
            MEDCONCERT,

            [Description("nbme")]
            NBME,

            [Description("ncpalc")]
            NCPALC,

            [Description("nof")]
            NOF,

            [Description("onslt")]
            ONSLT,

            [Description("kp2p")]
            KP2P,

            [Description("riteaid")]
            RITEAID,

            [Description("snmmi")]
            SNMMI,

            [Description("wiley")]
            WILEY,
        }

        #endregion organization/site general

        #region user

        /// <summary>
        /// ToDo: Add more professions from the select list on the registration page
        /// </summary>
        public enum Profession
        {
            [Description("Physician")]
            Physician,

            [Description("Pharmacist")]
            Pharmacist,

            [Description("Physicist")]
            Physicist,

            [Description("Nurse Practitioner")]
            Nurse_Practitioner,

            [Description("Nurse")]
            Nurse,

            [Description("Nurse Scientist")]
            Nurse_Scientist,

            [Description("Other")]
            Other,
        }

        /// <summary>
        /// select * from dbo.profession  order by description
        /// ToDo: Add more professions from the select list on the registration page
        /// </summary>
        public enum ProfessionCode
        {
            [Description("NPR")]
            NursePracticioner_NPR,

            [Description("NS")]
            NurseScientist_NS,

            [Description("RN")]
            Nurse_RN,

            [Description("PHY")]
            Physician_PHY,

            [Description("RPH")]
            Pharmacist_RPH,

            [Description("PCST")]
            Physicist_PCST,

            [Description("OTH")]
            Other_OTH,
        }

        #endregion user

        #region search

        public enum ActivitySearchType
        {
            [Description("All")]
            All,

            [Description("All Activities")]
            AllActivities,

            [Description("Online")]
            Online,

            [Description("Live")]
            Live,
        }

        /// <summary>
        /// NCPALC has this search option
        /// </summary>
        public enum ActivitySearchCategory
        {
            [Description("Credit Type")]
            CreditType,

            [Description("Disease")]
            Disease,

            [Description("MeSH")]
            MeSH,

            [Description("-- All Categories --")]
            AllCategories,
        }

        public enum ActivitySearchSortOptions
        {
            [Description("Release Date")]
            ReleaseDate,

            [Description("Relevance")]
            Relevance,

            [Description("Title A-Z")]
            TitleAZ,

            [Description("Least Distance")]
            LeastDistance,

            [Description("Last Updated")]
            LastUpdated,
        }

        #endregion search

        #region general

        public enum PageURLs
        {
            // Temporary for ONSLT
            [Description("account.aspx")]
            Account,

            [Description("profile")]
            Profile,

            [Description("login")]
            Login,

            //[Description("Registration.aspx")]
            //Registration,

            [Description("Registration.aspx")]
            Registration,

            [Description("Registration.aspx")]
            Activity_Payment_Legacy,

            [Description("activity_order_details")]
            Activity_Order_Details,

            [Description("activity_order_payment_receipt")]
            Activity_Order_Payment_Receipt,

            [Description("/activity_registration")]
            Activity_Registration,


            [Description("activity_cpeconsent")]
            Activity_CPEConsent,

            [Description("activity_sessions")]
            Activity_Sessions,

            [Description("activity_content")]
            Activity_Content,

            [Description("cybersource")]
            eCommerce_cybersource,



            // Activity workflow view URLs, in order of workflow
            [Description("activity_hold")]
            Activity_OnHold,

            [Description("activity_overview")]
            Activity_Overview,

            [Description("activity_bundle")] // Include These Activities tab
            Activity_Bundle,

            [Description("activity_pretest")]
            Activity_Pretest,

            [Description("activity_pim")]
            Activity_PIM,

            [Description("activity_test")] // Post test
            Activity_Test,

            [Description("activity_evaluation")]
            Activity_Evaluation,

            [Description("activity_followup")]
            Activity_Followup,

            [Description("activity_assessment")]
            Activity_Assessment,

            [Description("activity_claim_credit")]
            Activity_Claim_Credit,

            [Description("certificate")]
            Certificate,

            [Description("/curriculum")]
            Curriculum,

            // Preview page URL
            [Description("activity?")]
            Activity_Preview,
        }

        #endregion general

    }
}