using Browser.Core.Framework;
using LMS.Data;
using LMS.Data;
using NUnit.Framework;
using LMSAdmin.AppFramework;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
//
using LMSAdmin.AppFramework.HelperMethods;
using System.Threading;

namespace LMSAdmin.UITest
{
    //[BrowserMode(BrowserMode.New)]
    [LocalSeleniumTestFixture(BrowserNames.Chrome)]
    [TestFixture]
    [Ignore("Ignore a fixture; this is old cme360;keeping for reference , soon it will be deleted")]
    public class Utilities : TestBase
    {
        #region Constructors
        public Utilities(string browserName, string emulationDevice) : base(browserName, emulationDevice) { }

        // Remote Selenium Grid Test
        public Utilities(string browserName, string emulationDevice, string version, string platform, string hubUri, string extrasUri)
                                    : base(browserName, emulationDevice, version, platform, hubUri, extrasUri)
        { }
        #endregion

        #region properties

    //    TPHelperMethods TestPortalHelp = new TPHelperMethods();

        #endregion properties

        #region Tests

    //    [Test]
        public void MikesActivities()
        {
            LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            LP.Login(Constants_LMSAdmin.LoginUserNames.DHMC, "password");

            // Stand alone live activity with a custom location, including 2 accreditations (accredited with 10 credits, and non-accredited), 
            // multiple assessments (pre-test, post-test, evaluation), a randomized award, custom front matter
            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneLiveMeeting, "MikeActTitle 1").
                AddLocationToLiveActivity(null, "567 blah street", "Portland", "Oregon", null, "97035").
                AddAccreditation(true, 10).
                AddAccreditation(false).
                AddAssessment(Constants.AssessmentTypes.PreTestAssessment,
                questionTypes: Constants.QuestionTypes.MultipleChoiceSingleAnswer).
                AddAssessment(Constants.AssessmentTypes.PostTestAssessment,
                questionTypes: Constants.QuestionTypes.MultipleChoiceSingleAnswer).
                AddAssessment(Constants.AssessmentTypes.Evaluation,
                questionTypes: Constants.QuestionTypes.MultipleChoiceSingleAnswer).
                AddAward().
                AddFrontMatter("This is the front matter").
                Save().
                Publish().
                AddCatalog(Constants.SiteCodes.DHMC);

            // Stand alone activity with an assessment that includes 20 multiple choice questions having multiple answers
            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "MikeActTitle 2").
                AddAccreditation().
                AddAssessment(Constants.AssessmentTypes.Evaluation,
                20, "AssessTitle 3", questionTypes: Constants.QuestionTypes.MultipleChoicewithMultipleAnswers).
                Save().
                Publish().
                AddCatalog(Constants.SiteCodes.DHMC);

            // Stand alone activity with multiple awards
            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneLiveMeeting, "MikeActTitle 3").
                AddLocationToLiveActivity().
                AddAccreditation().
                AddAssessment().
                AddAward().
                AddAward().
                Save().
                Publish().
                AddCatalog(Constants.SiteCodes.DHMC);
        }

     //   [Test]
        public void RandomActivities()
        {
            LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            LP.Login(Constants_LMSAdmin.LoginUserNames.DHMC, "password");

            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "AutoAct FrontEvalCert 1").
                AddAccreditation(true, 10).
                AddAssessment(Constants.AssessmentTypes.Evaluation,
                    questionTypes: Constants.QuestionTypes.MultipleChoiceSingleAnswer).
                AddAward().
                AddFrontMatter("This is the front matter text").
                Save().
                Publish().
                AddCatalog(Constants.SiteCodes.UAMS);
        }

       // [Test]
        public void SearchTestActivities()
        {
            LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();
            LoginPage LP = Navigation.GoToLoginPage(Browser);
            LP.Login(Constants_LMSAdmin.LoginUserNames.CMECAL.GetDescription(), "password");

            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "AutomationSearchTest Online 1").
                AddAccreditation().
                Save().
                Publish().
                AddCatalog(Constants.SiteCodes.CMECA);

            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity, "AutomationSearchTest Online 2").
                AddAccreditation().
                Save().
                Publish().
                AddCatalog(Constants.SiteCodes.CMECA);

            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneLiveMeeting, "AutomationSearchTest Live 1").
                AddLocationToLiveActivity().
                AddAccreditation().
                Save().
                Publish().
                AddCatalog(Constants.SiteCodes.CMECA);

            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneLiveMeeting, "AutomationSearchTest Live 2").
                AddLocationToLiveActivity().
                AddAccreditation().
                Save().
                Publish().
                AddCatalog(Constants.SiteCodes.CMECA);


            CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneLiveMeeting, "MikeActTitle 1").
    AddLocationToLiveActivity(null, "567 test street", "Portland", "Oregon", null, "97035").
    AddAccreditation(true, 10).
    AddAccreditation(false).
    AddAssessment(Constants.AssessmentTypes.PreTestAssessment,
    questionTypes: Constants.QuestionTypes.MultipleChoiceSingleAnswer).
    AddAssessment(Constants.AssessmentTypes.PostTestAssessment,
    questionTypes: Constants.QuestionTypes.MultipleChoiceSingleAnswer).
    AddAssessment(Constants.AssessmentTypes.Evaluation,
    questionTypes: Constants.QuestionTypes.MultipleChoiceSingleAnswer).
    Save().
    Publish().
    AddCatalog(Constants.SiteCodes.DHMC);


        }

      //  [Test]
        public void PrePostEval_And_Eval()
        {
            LMSAdminHelperGeneral CMEHelp = new LMSAdminHelperGeneral();

            //        CMEHelp.CreateActivity(Browser, Constants_LMS.ProjectType.StandaloneActivity, "OverviewPage Pre Post Eval and Award", 
            //            Constants_LMSAdmin.LoginUserNames.CHI, "password")
            //.AddAccreditation(true, 10)
            //.AddFrontMatter("This is the front matter")
            //.AddAward()
            //.AddAssessment(Constants_LMS.AssessmentTypes.PreTestAssessment)
            //.AddAssessment(Constants_LMS.AssessmentTypes.PostTestAssessment)
            //.AddAssessment(Constants_LMS.AssessmentTypes.Evaluation)
            //.Save()
            //.Publish()
            //.AddCatalog(Constants_LMS.SiteCodes.PREM);

            Activity blah = CMEHelp.CreateActivity(Browser, Constants.ProjectType.StandaloneActivity,
                "Front page Eval and Award", Constants_LMSAdmin.LoginUserNames.CHI, "password")
                .AddAccreditation(true, 10)
                .AddFrontMatter("This is the front matter")
                .AddAward()
                .AddAssessment()
                .Save()
                .Publish()
                .AddCatalog(Constants.SiteCodes.PREM);
        }

        #endregion Tests

    }
}

