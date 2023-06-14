using Browser.Core.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Mainpro.AppFramework;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using LMS.Data;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Mainpro.UITest
{
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission1_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission1_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission1_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission1()
        {
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_Accreditationsurveyor_Max1);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_CFPCpeertutorARCorPearlscetutor_Max15);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_ClinicalSupervisor_Max30_VR_DR);

           /* this activity is been removed from application
            * Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_CPCSSNQualityAssuranceProgram_L_D6_Max6_VR);*/

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_Examinerformedicalexams_Max50);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_FamilyorEmergencyMedicineExaminations_FC50_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_FormalClinicalTraineeshipFellowship_D50_Max50_VR_DR);
        }

        #endregion Tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission2_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission2_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission2_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission2()
        {
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_MCC360_L_MC12or15_VR_DR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_FoundationforMedicalPracticeEducationFMPE_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_InternationalCPDActivitiesIndividualConsideration_VR_DR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_Pearls_FC6_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_PracticeAuditsQualityAssurancePrograms_FC6_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                 Const_Mainpro.ActivityCategory.Assessment,
                 Const_Mainpro.ActivityCertType.Certified,
                 Const_Mainpro.ActivityType.ASMT_CERT_ProvincialPracticeReviewandEnhancementPrograms_FC6_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live);
        }
        #endregion Tests
    }


    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission3_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission3_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission3_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission3()
        {
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_RoyalCollegeMOCAccreditedSection3_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.ASMT_CERT_OtherCFPCCertifiedMainproAssessmentActivities_LO,
                Const_Mainpro.ActivityFormat.Live,
                actName: "my activity1",
                creditsRequested: 1);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_FamilyMedicineCurriculumReview);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_ManuscriptReviewforMedicalJournals);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_PracticeAudits_L);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_ReviewofClinicalPracticeGuidelines_L);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_RoyalCollegeMOCSection3_LO,
                Const_Mainpro.ActivityFormat.Live);
        }

        #endregion Tests
    }


    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission4_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission4_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission4_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission4()
        {
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_RoyalCollegeMOCSection3_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.Assessment,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.ASMT_NONCERT_OtherNonCertifiedAssessmentActivities_LO_VR,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_AAFPandABFMActivities_L);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_AdvancedLifeSupportProgramsParticipant_L);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_FoundationforMedicalPracticeEducationFMPE_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_InternationalCPDActivitiesIndividualConsideration_O_VR_DR);


        }

        #endregion Tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission5_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission5_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission5_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission5()
        {
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_MOREPlusProgram_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_QuebecCategory1Credit_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_RoyalCollegeMOCAccreditedSection1_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.GRPLRNING_CERT_OtherCFPCCertifiedMainproGroupLearningActivities_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.GRPLRNING_NONCERT_AAFPandABFMElectiveActivities_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.GRPLRNING_NONCERT_AmericanCollegeofEmergencyPhysiciansACEP_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.GRPLRNING_NONCERT_AmericanMedicalAssociationAMAPRACategory1_LO,
                Const_Mainpro.ActivityFormat.Live);

        }

        #endregion Tests
    }


    [BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission6_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission6_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission6_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests



        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission6()
        {
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.GRPLRNING_NONCERT_RoyalCollegeMOCSection1_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.GroupLearning,
                Const_Mainpro.ActivityCertType.NonCertified,
                Const_Mainpro.ActivityType.GRPLRNING_NONCERT_OtherNonCertifiedGroupLearningActivities_LO_VR,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_AAFPandABFMActivities_L);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_AmericanMedicalAssociationAMAPRACategory1_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_CFPMainproArticles_V);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_COVID19LearningOnTheGo_D2_Max2_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_FormalStudiesUniversityDegreeDiplomaPrograms_FC20_VR_DR);

        }

        #endregion Tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission7_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission7_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission7_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission7()
        {
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.SELFLRNING_CERT_InternationalCPDActivitiesIndividualConsideration_VR_DR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.SELFLRNING_CERT_QuebecCategory1Credit_LO,
            Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.Assessment,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.ASMT_CERT_LinkingLearningtoPractice_FC5_Max5_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.Assessment,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.ASMT_CERT_LinkingLearningtoTeaching_FC5_Max5_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.Assessment,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.ASMT_CERT_LinkingLearningtoAssessment_FC5_Max5_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.Assessment,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.ASMT_CERT_LinkingLearningtoAdministration_FC5_Max5_VR);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.Assessment,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.ASMT_CERT_LinkingLearningtoResearch_FC5_Max5_VR);
        }

        #endregion Tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission8_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission8_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission8_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests


        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission8()
        {
            // Fixed defect https://code.premierinc.com/issues/browse/MAINPROREW-864
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
                Const_Mainpro.ActivityCategory.SelfLearning,
                Const_Mainpro.ActivityCertType.Certified,
                Const_Mainpro.ActivityType.SELFLRNING_CERT_SelfLearningProgramImpactAssessment_FC5);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.SELFLRNING_CERT_UpToDate);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.Certified,
            Const_Mainpro.ActivityType.SELFLRNING_CERT_OtherCFPCCertifiedMainproSelfLearningActivities_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_AAFPSelfLearningActivities);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_AmericanMedicalAssociationAMAPRACategory1_LO,
                Const_Mainpro.ActivityFormat.Live);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_CommitteeParticipationeducationalplanning);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_CurriculumDevelopmentmedicaleducationprofessionaldevelopment);
        }

        #endregion Tests
    }

    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [RemoteSeleniumTestFixture(BrowserNames.Chrome)]

    [TestFixture]
    public class Mainpro_ActivitySubmission9_Tests : TestBase
    {
        #region Constructors
        public Mainpro_ActivitySubmission9_Tests(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }
        public Mainpro_ActivitySubmission9_Tests(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region Tests

        [Test]
        [Description("Verifies that a  of activities can be submitted without error and then verifies that the " +
            "activity is populated into the CPD Activities List page")]
        [Property("Status", "Complete")]
        [Author("Mike Johnston")]
        [Category("ActivitySubmission")]
        public void ActivitySubmission9()
        {
            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_JournalReading_O);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_ManuscriptSubmission);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_OnlineeLearningpodcastsetc);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_PreparingPapersforPublication);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_Research);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_Teachingpresentingclinicalandoracademic);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_Tutoring);

            Help.AddActivity(browser, TestContext.CurrentContext.Test,
            Const_Mainpro.ActivityCategory.SelfLearning,
            Const_Mainpro.ActivityCertType.NonCertified,
            Const_Mainpro.ActivityType.SELFLRNING_NONCERT_OtherNonCertifiedSelfLearningActivities_LO_VR,
            Const_Mainpro.ActivityFormat.Online);
        }

        #endregion Tests
    }

}