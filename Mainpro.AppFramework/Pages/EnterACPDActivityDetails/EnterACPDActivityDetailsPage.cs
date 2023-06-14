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
    public class EnterACPDActivityDetailsPage : MainproPage, IDisposable
    {
        #region constructors
        public EnterACPDActivityDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        MainproHelperMethods Help = new MainproHelperMethods();

        public override string PageUrl { get { return "Default.aspx"; } }

        #endregion properties

        #region elements




        


        public IWebElement BasedOnYourAppraisalTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BasedOnYourAppraisalTxt); } }
        public IWebElement ObtainFeedbackTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ObtainFeedbackTxt); } }
        public IWebElement GatherInformationTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.GatherInformationTxt); } }
        public IWebElement PleaseDescribeTheTeachingTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.PleaseDescribeTheTeachingTxt); } }
        public IWebElement ProgramActivityIDTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ProgramActivityIDTxt); } }
        public IWebElement ConferenceOrWorkshopChk { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ConferenceOrWorkshopChk); } }
        public IWebElement AssessTheInformationTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.AssessTheInformationTxt); } }
        public IWebElement DescribeTheCriticalTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeTheCriticalTxt); } }
        public IWebElement CreditRefreshIsCompleteLbl { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CreditRefreshIsCompleteLbl); } }
        public IWebElement DescribeTheFeedbackTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeTheFeedbackTxt); } }
        public IWebElement BasedOnTheFeedbackTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BasedOnTheFeedbackTxt); } }
        public IWebElement InTheTextBoxBelowTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.InTheTextBoxBelowTxt); } }
        public IWebElement CertifiedRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CertifiedRdo); } }
        public IWebElement NonCertifiedRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.NonCertifiedRdo); } }
        public IWebElement SubmitBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.SubmitBtn); } }
        public IWebElement SendToHoldingAreaBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.SendToHoldingAreaBtn); } }
        public IWebElement YourActivityHasBeenSavedGoToHoldingAreaBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedGoToHoldingAreaBtn); } }
        public IWebElement SaveProgressBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.SaveProgressBtn); } }
        public IWebElement CancelBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CancelBtn); } }
        public SelectElement ProvinceSeleElem { get { return new SelectElement(this.WaitForElement(Bys.EnterACPDActivityDetailsPage.ProvinceSeleElem)); } }
        public SelectElement CategorySelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterACPDActivityDetailsPage.CategorySelElem)); } }
        public SelectElement ActivityTypeSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterACPDActivityDetailsPage.ActivityTypeSelElem)); } }
        public IWebElement ActivityTypeSelElemBtn { get { return this.WaitForElement(Bys.EnterACPDActivityDetailsPage.ActivityTypeSelElemBtn); } }
        public IWebElement ProvinceSeleElemBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ProvinceSeleElemBtn); } }
        public IWebElement IPerceivedBiasYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IPerceivedBiasYesRdo); } }
        public IWebElement IPerceiveAnyDegreeofBiasYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IPerceiveAnyDegreeofBiasYesRdo); } }               
        public IWebElement MyPracticeWillBeChangedAndImprovedYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.MyPracticeWillBeChangedAndImprovedYesRdo); } }
        public IWebElement IWasDissatisfiedYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IWasDissatisfiedYesRdo); } }
        public IWebElement IWasDissatisfiedNoRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IWasDissatisfiedNoRdo); } }
        public IWebElement ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt); } }
        public IWebElement CityTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CityTxt); } }
        public IWebElement ArticleTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ArticleTxt); } }

        public IWebElement PlanningOrganizationTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.PlanningOrganizationTxt); } }
        public IWebElement ActivityStartDateTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ActivityStartDateTxt); } }
        public IWebElement ActivityCompletionDateTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ActivityCompletionDateTxt); } }
        public IWebElement YourActivityHasBeenSubmittedFormGoToCPDActBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedFormGoToCPDActBtn); } }
        public IWebElement YourActivityHasBeenSubmittedCloseBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedCloseBtn); } }

        public IWebElement YourActivityHasBeenSubmittedFormEnterAnotherCPDActBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedFormEnterAnotherCPDActBtn); } }
        public IWebElement CreditApprovalRequiredAndDocUploadOptionalLbl { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CreditApprovalRequiredAndDocUploadOptionalLbl); } }
        public IWebElement CreditApprovalRequiredAndDocUploadRequiredLbl { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CreditApprovalRequiredAndDocUploadRequiredLbl); } }
        public IWebElement UploadFilesBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.UploadFilesBtn); } }
        public IWebElement UploadedFilesLbl { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.UploadedFilesLbl); } }
        public IWebElement CreditsRequestedOrClaimedTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CreditsRequestedOrClaimedTxt); } }
        public IWebElement CancelFormYesBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CancelFormYesBtn); } }
        public IWebElement CancelFormNoBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CancelFormNoBtn); } }
        public IWebElement ChooseOneQuesToAnswerArticleFirstRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ChooseOneQuesToAnswerArticleFirstRdo); } }
        public IWebElement PleaseIndicateYourRoleInThisAssessmentAssessorRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.PleaseIndicateYourRoleInThisAssessmentAssessorRdo); } }
        public IWebElement PleaseIndicateYourRoleInThisAssessmentAssessedRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.PleaseIndicateYourRoleInThisAssessmentAssessedRdo); } }
        public SelectElement ProgramTitleSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterACPDActivityDetailsPage.ProgramTitleSelElem)); } }
        public IWebElement ProgramTitleSelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ProgramTitleSelElemBtn); } }
        public IWebElement WhatResourcesWouldSupportYouTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatResourcesWouldSupportYouTxt); } }
        public IWebElement WhatWouldYouDoDifferentlyTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatWouldYouDoDifferentlyTxt); } }
        public IWebElement DescribeWhatYouHaveLearnedTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeWhatYouHaveLearnedTxt); } }
        public IWebElement IndicateYourRoleForThisActTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IndicateYourRoleForThisActTxt); } }
        public IWebElement BrieflyDescribeTheassessmentActInWhichYouParticipatedTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheExamActInWhichYouParticipatedTxt); } }
        public IWebElement WhatresourcesWouldSupportYouInAdvancingYourUnderstandingOfThisAssessmentTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatResourcesWouldSupportYouInAdvancingYourUnderstandingOfThisAssessmentTxt); } }
        public IWebElement HowWillYouIncorporateTheKnowledgeYouGainedTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.HowWillYouIncorporateTheKnowledgeYouGainedTxt); } }
        public IWebElement IfYesWhatMightTheseBenefitsBeTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IfYesWhatMightTheseBenefitsBeTxt); } }
        public IWebElement DoYouAnticipateThisExperienceBenefitingYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DoYouAnticipateThisExperienceBenefitingYesRdo); } }
        //  public SelectElement IndicateYourRoleInThisActSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterACPDActivityDetailsPage.IndicateYourRoleInThisActSelElem)); } }
        //  public IWebElement IndicateYourRoleInThisActSelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IndicateYourRoleInThisActSelElemBtn); } }
        public IWebElement OversightOrganizationTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.OversightOrganizationTxt); } }
        public IWebElement IndicateYourRoleForThisActivityAssessmentOfSelfRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IndicateYourRoleForThisActivityAssessmentOfSelfRdo); } }
        public IWebElement ILearnedSomethingNewYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ILearnedSomethingNewYesRdo); } }
        public IWebElement IAmMotivatedToLearnMoreYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IAmMotivatedToLearnMoreYesRdo); } }
        public IWebElement TheExperienceConfirmedIAmDoingTheRightThingYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.TheExperienceConfirmedIAmDoingTheRightThingYesRdo); } }
        public IWebElement DescribeTheActivityTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeTheActivityTxt); } }
        public IWebElement HoursTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.HoursTxt); } }
        public IWebElement DescribeWhatWasLearnedAsAResultTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeWhatWasLearnedAsAResultTxt); } }
        public IWebElement DescribeHowParticipatingInThisCourseTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeHowParticipatingInThisCourseTxt); } }
        public IWebElement IsThisActivityAccreditedByAnotherOrgNoRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IsThisActivityAccreditedByAnotherOrgNoRdo); } }
        public IWebElement PeerReviewedArticlesChk { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.PeerReviewedArticlesChk); } }
        public IWebElement WhatWillYouHaveToDoTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatWillYouHaveToDoTxt); } }
        public IWebElement BasedOnWhatYouLearnedTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BasedOnWhatYouLearnedTxt); } }
        public IWebElement WhatApproachOrToolsTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatApproachOrToolsTxt); } }
        public IWebElement WhatIsYourassessmentTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatIsYourassessmentTxt); } }
        public IWebElement DescribeTheProcessTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeTheProcessTxt); } }
        public IWebElement WhatWereYourSpecificQuestionsTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatWereYourSpecificQuestionsTxt); } }
        public IWebElement DescribeTheNatureOfYourPracticeTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeTheNatureOfYourPracticeTxt); } }
        public IWebElement BrieflyDescribeTheExamActInWhichYouParticipatedTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheExamActInWhichYouParticipatedTxt); } }
        public IWebElement PleaseDescribeYourReflectionsTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.PleaseDescribeYourReflectionsTxt); } }
        public IWebElement WhatWasYourRoleTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatWasYourRoleTxt); } }
        public IWebElement DateOfReflectionTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DateOfReflectionTxt); } }
        public IWebElement ActivityTypeRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ActivityTypeRdo); } }
        public IWebElement DescribeTheOutcomeTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeTheOutcomeTxt); } }
        public SelectElement IdentifyTheTypeOfAssessmentActivitySelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterACPDActivityDetailsPage.IdentifyTheTypeOfAssessmentActivitySelElem)); } }
        public IWebElement IdentifyTheTypeOfAssessmentActivitySelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IdentifyTheTypeOfAssessmentActivitySelElemBtn); } }
        public IWebElement DescribeYourActionPlanTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeYourActionPlanTxt); } }
        public SelectElement DidYouParticipateInSelElem { get { return new SelectElement(this.WaitForElement(Bys.EnterACPDActivityDetailsPage.DidYouParticipateInSelElem)); } }
        public IWebElement DidYouParticipateInSelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DidYouParticipateInSelElemBtn); } }
        public IWebElement WhatTriggeredThisQuestionTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatTriggeredThisQuestionTxt); } }
        public IWebElement WhoDidtheLiteratureSearchMyselfRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhoDidtheLiteratureSearchMyselfRdo); } }
        public IWebElement WhatMeshTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatMeshTxt); } }
        public IWebElement ArticlesTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ArticlesTxt); } }
        public IWebElement WereTheResultsYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WereTheResultsYesRdo); } }
        public IWebElement ListTheJournalArticlesTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ListTheJournalArticlesTxt); } }
        public IWebElement WhatStepTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatStepTxt); } }
        public IWebElement WhatImpactTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatImpactTxt); } }
        public IWebElement DescribeAnyKeySuccessesTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeAnyKeySuccessesTxt); } }
        public IWebElement HowSuccessfullVerySuccessfullRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.HowSuccessfullVerySuccessfullRdo); } }
        public IWebElement WhatMustYouDoTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatMustYouDoTxt); } }
        public IWebElement WhatKindOfInformationTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatKindOfInformationTxt); } }
        public IWebElement BrieflyDescribeTheFindingsTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheFindingsTxt); } }
        public IWebElement BrieflyDescribeTheAuditTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheAuditTxt); } }
        public IWebElement ForThePurposeOfThisExerciseTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ForThePurposeOfThisExerciseTxt); } }
        public IWebElement WhatWasTheOriginTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatWasTheOriginTxt); } }
        public IWebElement IfYouAreSubmittingTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IfYouAreSubmittingTxt); } }
        public IWebElement WhoWasInvolvedTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhoWasInvolvedTxt); } }
        public IWebElement BrieflyDescribeTheAssessmentTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheAssessmentTxt); } }
        public IWebElement WhatWasYourAssessmentTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatWasYourAssessmentTxt); } }
        public IWebElement IfYouAreSubmitting1Rdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.IfYouAreSubmitting1Rdo); } }
        public IWebElement HowWillThisQuestionChangeYourPractice1TheChangeWillBeLargeRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.HowWillThisQuestionChangeYourPractice1TheChangeWillBeLargeRdo); } }
        public IWebElement HowWillThisQuestion2ChangeYourPractice1TheChangeWillBeLargeRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.HowWillThisQuestion2ChangeYourPractice1TheChangeWillBeLargeRdo); } }
        public IWebElement DidYouPerceiveYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DidYouPerceiveYesRdo); } }
        public IWebElement DoYouForeseeYesRdof { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DoYouForeseeYesRdo); } }
        public IWebElement WhatChangesTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatChangesTxt); } }
        public IWebElement PleaseExplainTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.PleaseExplainTxt); } }
        public IWebElement WhatSourcesFirstChk { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatSourcesFirstChk); } }
        public IWebElement DidthisActivityYesRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DidthisActivityYesRdo); } }
        public IWebElement FurtherDescribeTheActivityTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.FurtherDescribeTheActivityTxt); } }
        public IWebElement WhatTypeOfActivityPatientEncounterRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatTypeOfActivityPatientEncounterRdo); } }
        public IWebElement ExplainToWhatTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ExplainToWhatTxt); } }
        public IWebElement AssignedReadingChk { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.AssignedReadingChk); } }
        public IWebElement SupervisorTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.SupervisorTxt); } }
        public IWebElement LocationsTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.LocationsTxt); } }
        //   public IWebElement InGeneralTermsTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.InGeneralTermsTxt); } }
        public IWebElement WhatIstheSpecificAreaTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatIstheSpecificAreaTxt); } }
        public IWebElement WhyDidYouAddressTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhyDidYouAddressTxt); } }
        public IWebElement HowSuccessfullTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.HowSuccessfullTxt); } }
        public IWebElement HowWillYouIntegrateTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.HowWillYouIntegrateTxt); } }
        public IWebElement BasedUponTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BasedUponTxt); } }
        public IWebElement DescribeTheCourseTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeTheCourseTxt); } }
        public IWebElement WhatWereYourLearningTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatWereYourLearningTxt); } }
        public IWebElement DescribeHowThisCourseTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeHowThisCourseTxt); } }
        public IWebElement NameOfCourseTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.NameOfCourseTxt); } }

        public IWebElement NameOfUniversityTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.NameOfUniversityTxt); } }
        public IWebElement WhatWillYouTakeTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatWillYouTakeTxt); } }
        public IWebElement WhatOpportunitiesTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatOpportunitiesTxt); } }
        public IWebElement HowWouldThisExperienceTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.HowWouldThisExperienceTxt); } }
        public IWebElement WhatFeedbackTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatFeedbackTxt); } }
        public IWebElement DescribeHowYourAdminstrativeTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeHowYourAdminstrativeTxt); } }
        public IWebElement CollaboratorChk { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CollaboratorChk); } }
        public IWebElement DescribeYourRoleTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeYourRoleTxt); } }
        public IWebElement UponWhatKindFirstRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.UponWhatKindFirstRdo); } }
        public IWebElement ProfessionalActivitiesFirstRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ProfessionalActivitiesFirstRdo); } }
        public IWebElement WhatFeedbackHaveYouReceivedFromReviewersTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatFeedbackHaveYouReceivedFromReviewersTxt); } }
        public IWebElement WhatDidYouLearnTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.WhatDidYouLearnTxt); } }
        public IWebElement TypeOfFeedback2FirstRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.TypeOfFeedback2FirstRdo); } }
        public IWebElement TypeOfFeedback1FirstRdo { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.TypeOfFeedback1FirstRdo); } }
        public IWebElement FromTheFeedback2Txt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.FromTheFeedback2Txt); } }
        public IWebElement FromTheFeedback1Txt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.FromTheFeedback1Txt); } }
        public IWebElement BasedOnThisFeedback2Txt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BasedOnThisFeedback2Txt); } }
        public IWebElement BasedOnThisFeedback1Txt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BasedOnThisFeedback1Txt); } }
        public IWebElement FormatOfFeedback1Txt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.FormatOfFeedback1Txt); } }
        public IWebElement DescribeTheTeachingTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeTheTeachingTxt); } }
        public IWebElement SettingTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.SettingTxt); } }
        public IWebElement ClinicalPreceptorChk { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.ClinicalPreceptorChk); } }
        public IWebElement AudienceTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.AudienceTxt); } }
        public IWebElement FormatOfFeedback2Txt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.FormatOfFeedback2Txt); } }
        public IWebElement MaxCreditReachedFormUpdateNotAppliedLbl { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormUpdateNotAppliedLbl); } }
        public IWebElement MaxCreditReachedFormUpdateAppliedLbl { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormUpdateAppliedLbl); } }
        public IWebElement MaxCreditReachedFormClaimedLbl { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormClaimedLbl); } }
        public IWebElement MaxCreditReachedFormUpdateCurrentFormBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormUpdateCurrentFormBtn); } }
        public IWebElement MaxCreditReachedFormAddNonCertActBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormAddNonCertActBtn); } }
        public IWebElement YourActivityHasBeenSavedBannerXBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedBannerXBtn); } }
        public IWebElement DescribeYourCriticalAppraisalTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.DescribeYourCriticalAppraisalTxt); } }
        public IWebElement BrieflyDescribeTheOutcomeTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.BrieflyDescribeTheOutcomeTxt); } }
        public IWebElement CompleteActivitySummaryInCompleteActivitiesBtn { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.CompleteActivitySummaryInCompleteActivitiesBtn); } }
        public IWebElement AssessedMaxCreditsTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.AssessedMaxCreditsTxt); } }
        public IWebElement AssessorMaxCreditsTxt { get { return this.FindElement(Bys.EnterACPDActivityDetailsPage.AssessorMaxCreditsTxt); } }








        #endregion elements        

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
            this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.EnterACPDActivityDetailsPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.EnterACPDActivityDetailsPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
        }
        public void Wait(int time)
        {
            System.Threading.Thread.Sleep(time);
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose DashboardPge", activeRequests.Count, ex); }
        }

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.SubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == SubmitBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser, elem);
                    // elem.Click();
                   
                    // Outstanding defect https://code.premierinc.com/issues/browse/MAINPROREW-831. I think I can remove
                    // the try block as well as the Thread.Sleep when this is fixed
                    try
                    {
                        Browser.WaitJSAndJQuery();
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(80));
                        if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
                        {
                            throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
                        }
                        Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedFormGoToCPDActBtn,
                            TimeSpan.FromSeconds(60), ElementCriteria.IsVisible);
                        Browser.WaitJSAndJQuery();
                        Thread.Sleep(600);
                        this.WaitUntil(Criteria.EnterACPDActivityDetailsPage.LoadIconNotExists);
                    }
                    catch
                    {
                        if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.SubmitBtn, ElementCriteria.IsVisible))
                        {
                            ElemSet.ScrollToElement(Browser, elem);
                            elem.ClickJS(Browser);
                            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(80));
                            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
                            {
                                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
                            }
                            Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedFormGoToCPDActBtn,
                                TimeSpan.FromSeconds(60), ElementCriteria.IsVisible);
                            Browser.WaitJSAndJQuery();
                            Thread.Sleep(600);
                            this.WaitUntil(Criteria.EnterACPDActivityDetailsPage.LoadIconNotExists);
                        }
                    }

                    WaitForCreditRefresh(TimeSpan.FromSeconds(180));

                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.SendToHoldingAreaBtn))
            {
                if (elem.GetAttribute("outerHTML") == SendToHoldingAreaBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    // Outstanding defect https://code.premierinc.com/issues/browse/MAINPROREW-831. I think I can remove
                    // the try block as well as the Thread.Sleep when this is fixed
                    try
                    {
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(80));
                        if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
                        {
                            throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
                        }
                        Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedGoToHoldingAreaBtn,
                            TimeSpan.FromSeconds(60), ElementCriteria.IsVisible);
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(80));
                        Thread.Sleep(600);
                        this.WaitUntil(Criteria.EnterACPDActivityDetailsPage.LoadIconNotExists);
                    }
                    catch
                    {
                        if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.SubmitBtn, ElementCriteria.IsVisible))
                        {
                            elem.ClickJS(Browser);
                            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(80));
                            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
                            {
                                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
                            }
                            Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedGoToHoldingAreaBtn,
                                TimeSpan.FromSeconds(60), ElementCriteria.IsVisible);
                            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(80));
                            Thread.Sleep(600);
                        }

                    }

                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.SaveProgressBtn))
            {
                if (elem.GetAttribute("outerHTML") == SaveProgressBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    try
                    {
                        Browser.WaitJSAndJQuery();
                        if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
                        {
                            throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
                        }
                        this.WaitUntil(Criteria.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedBannerLblExists);
                        Thread.Sleep(1000);
                        Browser.WaitJSAndJQuery();
                        Thread.Sleep(1000);
                        Browser.WaitJSAndJQuery();
                    }
                    catch
                    {
                        Browser.WaitJSAndJQuery();
                        if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
                        {
                            throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
                        }
                        elem.ClickJS(Browser);
                        this.WaitUntil(Criteria.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedBannerLblExists);
                        Thread.Sleep(1000);
                        Browser.WaitJSAndJQuery();
                        Thread.Sleep(1000);
                        Browser.WaitJSAndJQuery();
                    }

                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedBannerXBtn))
            {
                if (elem.GetAttribute("outerHTML") == YourActivityHasBeenSavedBannerXBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitUntil(Criteria.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedBannerLblNotExists);
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedFormGoToCPDActBtn))
            {
                if (elem.GetAttribute("outerHTML") == YourActivityHasBeenSubmittedFormGoToCPDActBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    CPDActivitiesListPage page = new CPDActivitiesListPage(Browser);
                    try
                    {
                        page.WaitForInitialize();
                    }
                    catch
                    {
                        elem.ClickJS(Browser);
                        page.WaitForInitialize();
                    }
                    return page;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedGoToHoldingAreaBtn))
            {
                if (elem.GetAttribute("outerHTML") == YourActivityHasBeenSavedGoToHoldingAreaBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    HoldingAreaPage page = new HoldingAreaPage(Browser);
                    try
                    {
                        page.WaitForInitialize();
                    }
                    catch
                    {
                        elem.ClickJS(Browser);
                        page.WaitForInitialize();
                    }
                    return page;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSavedGoToHoldingAreaBtn))
            {
                if (elem.GetAttribute("outerHTML") == YourActivityHasBeenSavedGoToHoldingAreaBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    HoldingAreaPage page = new HoldingAreaPage(Browser);
                    try
                    {
                        page.WaitForInitialize();
                    }
                    catch
                    {
                        elem.ClickJS(Browser);
                        page.WaitForInitialize();
                    }
                    return page;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormAddNonCertActBtn))
            {
                if (elem.GetAttribute("outerHTML") == MaxCreditReachedFormAddNonCertActBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    try
                    {
                        this.WaitForInitialize();
                        Thread.Sleep(1000);
                        this.WaitForInitialize();
                    }
                    catch
                    {
                        elem.ClickJS(Browser);
                        this.WaitForInitialize();
                        Thread.Sleep(1000);
                        this.WaitForInitialize();
                    }
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.MaxCreditReachedFormUpdateCurrentFormBtn))
            {
                if (elem.GetAttribute("outerHTML") == MaxCreditReachedFormUpdateCurrentFormBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    try
                    {
                        this.WaitForInitialize();
                    }
                    catch
                    {
                        elem.ClickJS(Browser);
                        this.WaitForInitialize();
                    }
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.CancelFormNoBtn))
            {
                if (elem.GetAttribute("outerHTML") == CancelFormNoBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.CancelFormYesBtn))
            {
                if (elem.GetAttribute("outerHTML") == CancelFormYesBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    DashboardPage DP = new DashboardPage(Browser);
                    DP.WaitForInitialize();
                    return DP;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.CancelBtn))
            {
                if (elem.GetAttribute("outerHTML") == CancelBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.CancelFormNoBtn);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.YourActivityHasBeenSubmittedCloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == YourActivityHasBeenSubmittedCloseBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    DashboardPage DP = new DashboardPage(Browser);
                    DP.WaitForInitialize();
                    return DP;
                }
            }


            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. " +
                "You either need to add this element to a new If statement, or if the element is already added, then the page " +
                "you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }


        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string selection)
        {
            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.ProvinceSeleElem))
            {
                if (selectElement.Options[1].Text == ProvinceSeleElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ProvinceSeleElemBtn, selection);
                    }
                    else
                    {
                        ProvinceSeleElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            //if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.IndicateYourRoleInThisActSelElem))
            //{
            //    if (selectElement.Options[1].Text == IndicateYourRoleInThisActSelElem.Options[1].Text)
            //    {
            //        if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
            //            BrowserNames.Firefox)
            //        {
            //            ElemSet.DropdownMulti_Fireball_SelectByText(Browser, IndicateYourRoleInThisActSelElemBtn, selection);
            //        }
            //        else
            //        {
            //            IndicateYourRoleInThisActSelElem.SelectByText(selection);
            //        }
            //        this.WaitForInitialize();
            //        return null;
            //    }
            //}

            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.IdentifyTheTypeOfAssessmentActivitySelElem))
            {
                if (selectElement.Options[1].Text == IdentifyTheTypeOfAssessmentActivitySelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, IdentifyTheTypeOfAssessmentActivitySelElemBtn,
                            selection);
                    }
                    else
                    {
                        IdentifyTheTypeOfAssessmentActivitySelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }


            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.DidYouParticipateInSelElem))
            {
                if (selectElement.Options[1].Text == DidYouParticipateInSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, DidYouParticipateInSelElemBtn,
                            selection);
                    }
                    else
                    {
                        DidYouParticipateInSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element."));

        }


        #endregion methods: repeated per page

        #region methods: page specific

        #region methods: filling activity forms

        /// <summary>
        /// Fills the required fields on the Activity form. This method is conditioned so that if the activity you send it 
        /// does not have the element, or if the element is disabled for the activity, the code will skip it. As we build  
        /// this method, it should eventually be able to handle all activites you throw at it. If a new activity is added 
        /// to CFPC which has a new field, please update this method
        /// </summary>
        /// <param name="actTitle">(Optional). You can specify an activity title if the type of activity allows for it. 
        /// Default = "TestAuto_CurrentDate"</param>
        /// <param name="credits">(Optional). If the activity allows the user to enter credits, you can enter that here. 
        /// <param name="actStartDt">(Optional). Default = today</param>
        /// <param name="actStartDt">(Optional). Default = today</param>
        /// <param name="actStartDt">(Optional). Default = today</param>
        /// <param name="keepExistingActTitle">(Optional). If you want to keep the default activity title. This is mainly 
        /// for Sessions, as they prepopulate the activity title text field</param>
        public Activity FillActivityForm(string actTitle = null, double credits = 1, DateTime actStartDt = default(DateTime),
            DateTime actCompletionDt = default(DateTime), DateTime actDateOfReflection = default(DateTime),
            bool keepExistingActTitle = false)
        {
            // Define the variables for the Activity object. We will populate some of these variables now, and some of them
            // throughout this method, we will return them at the end of this method
            string category = CategorySelElem.SelectedOption.Text;
            string certType = CertifiedRdo.Selected ? "Certified" : "Non-Certified";
            string actType = Browser.Exists(Bys.EnterACPDActivityDetailsPage.ActivityTypeSelElem) ?
                ActivityTypeSelElem.SelectedOption.Text : ActivityTypeRdo.Text;

            // Enter the title of the activity (if applicable) and get the true title returned to us
            actTitle = EnterThenReturnActivityTitle(actTitle, keepExistingActTitle);

            // Enter the dates of the activity (if applicable) and get the "activity date" returned to us
            string actDate = EnterThenReturnActivityDate(actStartDt, actCompletionDt, actDateOfReflection);

            // Enter credit. Some forms have either Credits Claimed or Credits Requested text box. 
            // Sometimes it is disabled and predetermined
            if (!Browser.Exists(Bys.EnterACPDActivityDetailsPage.CreditsRequestedOrClaimedTxt,
                ElementCriteria.HasAttribute("disabled"))
                // Outstanding defect https://code.premierinc.com/issues/browse/MAINPROREW-835. Right now, the credit field
                // for this activity type is NOT disabled and it should be. When this defect is fixed, remove the below line
                // because the above line (hasattribute) will work on this activity type once the bug is fixed. When bug is 
                // fixed and you remove the below line, be sure to execute a test that adds an article activity and make sure 
                // it passes
                && actType != Const_Mainpro.ActivityType.SELFLRNING_CERT_CFPMainproArticles_V.GetDescription())
            {
                Help.ClearTextBox(CreditsRequestedOrClaimedTxt);
                CreditsRequestedOrClaimedTxt.SendKeys(credits.ToString());
            }

            // The order of these next 3 methods are important. Dont switch the order. This is because some radio buttons
            // enable text boxes which are required. If we tried to enter text into all text boxes before clicking radio 
            // buttons, then some of them would be disabled
            ClickAllAppropriateRadioButtons();

            ClickAllAppropriateCheckBoxes();

            SelectFirstItemInRandomGenericSelectElement();

            EnterRandomTextIntoGenericTextFields();

            EnterIntegerTextIntoIntegerOnlyTextField();

            // If documentation is required, then upload it
            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.CreditApprovalRequiredAndDocUploadRequiredLbl,
                ElementCriteria.IsVisible)|| (Browser.Exists(Bys.EnterACPDActivityDetailsPage.DocUploadRequiredLbl,
                ElementCriteria.IsVisible)))
            {
                string filePath = "C:\\SeleniumAutoIt\\book1.xlsx";
                ElemSet.ScrollToElement(Browser, UploadFilesBtn);
                
                try
                {
                    ElemSet.ScrollToElement(Browser, UploadedFilesLbl);
                    FileUtils.UploadFileUsingSendKeys(Browser, UploadFilesBtn, filePath,
                        elemToWaitFor: By.XPath("//span[text()='book1.xlsx']"));

                }
                catch
                {

                }
            }

            //// For the "Office Use Only" activity, it has two fields that have xpaths in the
            //// ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt field, so it only enters 
            //// text into the first indexed one. Because of this, I am conditioning this to 
            //// enter text into the second indexed one because it is required on the form
            //if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.ActivityTypeSelElem))
            //{
            //    if (ActivityTypeSelElem.Options[1].Text ==
            //        Const_Mainpro.ActivityType.SELFLRNING_CERT_Officeuseonly.GetDescription())
            //    {
            //        Browser.FindElement(By.XPath("//input[@aria-label='Program or Activity Description']")).
            //            SendKeys(DataUtils.GetRandomString(5));
            //    }
            //}

            return new Activity(Browser, category, certType, actType, actTitle, actDate);
        }

        /// <summary>
        /// Enter the dates of the activity if the form has these Date text boxes. If the activity has both a 
        /// Completion Date field and a Date of Reflection field, then the returned date variable will get the 
        /// Date of Reflection text box value. If it does not have a Date Of Reflection, but has a Completion Date
        /// field, the variable will get the Completion Date text box value. If it has neither, returned date will be null
        /// </summary>
        /// <param name="actStartDt">(Optional). Default = today</param>
        /// <param name="actCompletionDt">(Optional). Default = today</param>
        /// <param name="actDateOfReflection">(Optional). Default = today</param>
        /// <returns></returns>
        private string EnterThenReturnActivityDate(DateTime actStartDt = default(DateTime),
            DateTime actCompletionDt = default(DateTime),
            DateTime actDateOfReflection = default(DateTime))
        {
            // Create a variable that is going to represent the activity date (either completion date or date of 
            // reflection, so then we can return it at the end of the method
            string actDate = null;

            // If the form has this element, enter the date
            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.ActivityStartDateTxt,
            ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
            {
                // If the user didnt specify a date, set the date to today. 
                if (actStartDt == default(DateTime))
                {
                   
                    actStartDt = currentDatetime;
                }
                Help.ClearTextBox(ActivityStartDateTxt);
                ActivityStartDateTxt.SendKeys(actStartDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            }
            // Else if the form doesnt have this element but user specified a date 
            else if (!Browser.Exists(Bys.EnterACPDActivityDetailsPage.ActivityStartDateTxt) &&
                actStartDt != default(DateTime))
            {
                throw new Exception("This activity type does not have a Start Date element");
            }

            // If the form has this element, enter the date
            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.ActivityCompletionDateTxt,
            ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
            {
                // If the user didnt specify a date, set the date to today. 
                if (actCompletionDt == default(DateTime))
                {
                    actCompletionDt = currentDatetime;
                        //DateTime.Today;
                }
                Help.ClearTextBox(ActivityCompletionDateTxt);
                ActivityCompletionDateTxt.SendKeys(actCompletionDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
                actDate = ActivityCompletionDateTxt.GetAttribute("value");
            }
            // Else if the form doesnt have this element but user specified a date 
            else if (!Browser.Exists(Bys.EnterACPDActivityDetailsPage.ActivityCompletionDateTxt) &&
                actCompletionDt != default(DateTime))
            {
                throw new Exception("This activity type does not have a Completion Date element");
            }

            // If the form has this element, enter the date
            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.DateOfReflectionTxt,
            ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
            {
                // If the user didnt specify a date, set the date to today. 
                if (actDateOfReflection == default(DateTime))
                {
                    actDateOfReflection = currentDatetime;
                        //DateTime.Today;
                }
                DateOfReflectionTxt.SendKeys(actDateOfReflection.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
                actDate = DateOfReflectionTxt.GetAttribute("value");
            }
            // Else if the form doesnt have this element but user specified a date 
            else if (!Browser.Exists(Bys.EnterACPDActivityDetailsPage.DateOfReflectionTxt) &&
                actDateOfReflection != default(DateTime))
            {
                throw new Exception("This activity type does not have a Date of Reflection element");
            }

            return actDate;
        }

        /// <summary>
        /// Selects/Enters the activity title in the cooresponding activity title field. This can be a select element 
        /// or a text box. It can be disabled and we wont enter anything. It can have BOTH a text box and a select element
        /// that are defined to represent the activity title (inside the PageBy class), but the method will correctly 
        /// discern which is truly representing the activity title per the activity type form. It will then return the 
        /// activity title. This title can be used to verify the activity on various different tables in Mainpro
        /// </summary>
        /// <param name="actTitle">(Optional). You can specify an activity title if the type of activity allows for it. 
        /// Default = "TestAuto_CurrentDate"</param>
        /// <param name="keepExistingActTitle">(Optional). If you want to keep the default activity title. This is mainly 
        /// for Sessions, as they prepopulate the activity title text field</param>
        /// <returns></returns>
        private string EnterThenReturnActivityTitle(string actTitle = null, bool keepExistingActTitle = false)
        {
            // Select the title of the activity if the activity's form has a Select Element for the title
            if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.ProgramTitleSelElemBtn))
            {
                if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                    BrowserNames.Firefox)
                {
                    ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ProgramTitleSelElemBtn,
                        ProgramTitleSelElem.Options[1].Text);
                }
                else
                {
                    ProgramTitleSelElem.SelectByText(ProgramTitleSelElem.Options[1].Text);
                }
                actTitle = ProgramTitleSelElemBtn.GetAttribute("innerText");

                // Some forms will have the above Select Element which represents the title, but will ALSO have the
                // ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt text box, but does not represent the title. 
                // For this condition, we need to fill this text box in because its still a required field
                if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt,
                ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
                {
                    Help.ClearTextBox(ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt);
                    ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt.SendKeys(GetActivityNameWithDate());
                }
            }

            // Else if the foerm does not have a Select Element representing the title, then enter the title of the activity 
            // in the text box which represents the title. Note that some are disabled and prepopulated
            else if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt,
                ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
            {
                if (!keepExistingActTitle)
                {
                    actTitle = string.IsNullOrEmpty(actTitle) ? GetActivityNameWithDate() : actTitle;
                    Help.ClearTextBox(ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt);
                    ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt.SendKeys(actTitle);
                }
                else
                {
                    actTitle = ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt.GetAttribute("value");
                }
            }

            // Else if the form does not have an enabled Title text box AND does not have a Title select element, then it
            // will have a disabled Title text box. If this is the case, then just store the value within the text box into
            // the actName variable
            else if (Browser.Exists(Bys.EnterACPDActivityDetailsPage.ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt))
            {
                actTitle = ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt.GetAttribute("value");
            }

            // This condition is for activity forms that have both a Select Element and Text Box from the above 
            // If statement, BUT the Title of the activity is derived from the text box, not the Select Element
            string actType = Browser.Exists(Bys.EnterACPDActivityDetailsPage.ActivityTypeSelElem) ?
                ActivityTypeSelElem.SelectedOption.Text : ActivityTypeRdo.Text;
            if (actType == Const_Mainpro.ActivityType.GRPLRNING_CERT_AAFPandABFMActivities_L.GetDescription() ||
                actType == Const_Mainpro.ActivityType.SELFLRNING_CERT_AAFPandABFMActivities_L.GetDescription())
            {
                actTitle = ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt.GetAttribute("value");
            }

            // If the form does have one of the above, BUT the Title of the activity is supposed to be the 
            // text from the Activity TYPE select element, not from either of the elements in the above If statements
            if (actType == Const_Mainpro.ActivityType.ASMT_CERT_ClinicalSupervisor_Max30_VR_DR.GetDescription())
            {
                actTitle = ActivityTypeSelElemBtn.GetAttribute("innerText");
            }

            // Article activities
            if (actType == Const_Mainpro.ActivityType.SELFLRNING_CERT_CFPMainproArticles_V.GetDescription())
            {
                actTitle = ArticleTxt.GetAttribute("value");
            }

            return actTitle;
        }

        /// <summary>
        /// Enters random strings into all of the required random generic text boxes.
        /// </summary>
        public void EnterRandomTextIntoGenericTextFields()
        {
            foreach (var elemBy in Const_Mainpro.TextBoxCharacterAndIntegerElemBys)
            {
                if (Browser.Exists(elemBy, ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
                {
                    // Only if the text box does NOT have text, then we enter text. I do this because all of the 
                    // different xpaths and locators of these text boxes caused a few duplicates, and so it would
                    // enter text twice. This caused problems when this occured to the text box which represented 
                    // the title of the activity. It appended random string onto the end of the activity title, 
                    // which has been coded in the beginning of this method to only be TestAuto_Date
                    if (Browser.FindElement(elemBy).GetAttribute("value") == "")
                    {
                        Help.ClearTextBox(Browser.FindElement(elemBy));
                        Browser.FindElement(elemBy).SendKeys(DataUtils.GetRandomString(5));
                        this.WaitForInitialize();
                    }
                }
            }
        }

        /// <summary>
        /// Enters 1 into the integer-only text fields
        /// </summary>
        public void EnterIntegerTextIntoIntegerOnlyTextField()
        {
            foreach (var elemBy in Const_Mainpro.TextBoxIntegerOnlyElemBys)
            {
                if (Browser.Exists(elemBy, ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
                {
                    Browser.FindElement(elemBy).SendKeys(1.ToString());
                }
            }
        }

        /// <summary>
        /// Click the appropriate (The one that will allow us to Submit) radio button for all of the random radio buttons
        /// </summary>
        public void ClickAllAppropriateRadioButtons()
        {
            Thread.Sleep(500);
            foreach (var elemBy in Const_Mainpro.RadioButtonElemBys)
            {
                if (Browser.Exists(elemBy, ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
                {
                    // I dont know why, but Firefox has an issue with clicking the I Perceived Bias radio button while 
                    // filling out the GRPLRNING_CERT_AAFPandABFMActivities_L form. So going to use Javascript to click it
                    if (Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        Browser.FindElement(elemBy).ClickJS(Browser);
                    }
                    // Now Chrome had an issue saying "ElementNotInteractableException : element not interactable: element has zero size"
                    // Found that if you use a scroll, it works
                    // https://stackoverflow.com/questions/62003082/elementnotinteractableexception-element-not-interactable-element-has-zero-size
                    else
                    {
                        ElemSet.ScrollToElement(Browser, Browser.FindElement(elemBy));
                        Browser.FindElement(elemBy).Click();
                    }
                    this.WaitForInitialize();
                }
            }
        }

        /// <summary>
        /// Click the appropriate (The one that will allow us to Submit) check boxes for all of the random radio buttons
        /// </summary>
        public void ClickAllAppropriateCheckBoxes()
        {
            foreach (var elemBy in Const_Mainpro.ChkElemBys)
            {
                if (Browser.Exists(elemBy, ElementCriteria.IsVisible, ElementCriteria.IsEnabled))
                {
                    Browser.FindElement(elemBy).Click();
                    this.WaitForInitialize();
                }
            }
        }

        /// <summary>
        /// Selects the first item in the list for all of the random generic Select Elements
        /// </summary>
        public void SelectFirstItemInRandomGenericSelectElement()
        {
            foreach (var elemBy in Const_Mainpro.SelectElementBys)
            {
                if (Browser.Exists(elemBy))
                {
                    SelectElement selElem = new SelectElement(Browser.FindElement(elemBy));
                    SelectAndWait(selElem, selElem.Options[1].Text);
                    this.WaitForInitialize();
                }
            }
        }

        #endregion methods: filling activity forms

    
        /// <summary>
        /// Returns a random date to enter for the FillActvityForm method
        /// </summary>
        /// <returns></returns>
        private string GetActivityNameWithDate()
        {
            DateTime dt = currentDatetime;                //DateTime.Now; changed this for converting Date to EST
            int currentDay = dt.Day;
            int currentMonth = dt.Month;
            int currentYear = dt.Year;
            int currentHour = dt.Hour;
            int currentMinute = dt.Minute;

            string actName = "TestAutoAct" + DataUtils.GetRandomString(3) + "_" + currentMonth + "_" + currentDay + "_" +
                currentYear + "-" + currentHour + ":" + currentMinute + ":" + DataUtils.GetRandomString(3);

            return actName; ;
        }

        /// <summary>
        /// 
        /// </summary>
        private void WaitForCreditRefresh(TimeSpan waitTime = default(TimeSpan))
        {
            waitTime = waitTime == default(TimeSpan) ? TimeSpan.FromSeconds(60) : waitTime;

            // We need to wait for the indicator telling us that the Credit Service has completed, so that 
            // the credits will appear on other pages from this activity.
            // See https://code.premierinc.com/issues/browse/MAINPROREW-893 and see...
            // https://code.premierinc.com/issues/browse/CFPC-2903

            Browser.WaitForElement(Bys.EnterACPDActivityDetailsPage.CreditRefreshIsCompleteLbl, waitTime);

            // 5/24/21: After waiting for the Credit Refresh label to say Complete, then verifying the 
            // Annual Requirements table, a test failed (CreditSummaryWidgetsVerificationReqMet). Per DEV,
            // we have to wait an additional 3 seconds before checking Annual table because the Annual table
            // waits for an API (an API that gets executed after Credit Refresh = Complete). This API populates
            // the Annual table. So tester clicks Submit, tester waits for Credit Service = Complete, tester
            // waits for 3 seconds, tester verifies Annual table
            Thread.Sleep(3000);
        }
    }

                                               


    #endregion methods: page specific


}


