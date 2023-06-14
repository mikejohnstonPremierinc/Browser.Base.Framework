
using OpenQA.Selenium;


namespace Mainpro.AppFramework
{
    public class EnterACPDActivityDetailsPageBys
    {
        // Buttons
        public readonly By SubmitBtn = By.XPath("//div[@aria-label='Submit']");
        public readonly By SendToHoldingAreaBtn = By.XPath("//span[text()='Send to Holding Area']");
        public readonly By SaveProgressBtn = By.XPath("//span[text()='Save Progress']");
        public readonly By CancelBtn = By.XPath("//div[contains(@class, 'ecpdCancelButton')]//span[text()='Cancel']");
        public readonly By YourActivityHasBeenSubmittedFormGoToCPDActBtn = By.XPath("//span[text()='GO TO CPD ACTIVITIES'] | //button[text()='GO TO CPD ACTIVITIES']");
        public readonly By YourActivityHasBeenSubmittedFormEnterAnotherCPDActBtn = By.XPath("//span[text()='ENTER ANOTHER CPD ACTIVITY'] | //button[text()='ENTER ANOTHER CPD ACTIVITY']");
        public readonly By YourActivityHasBeenSavedGoToHoldingAreaBtn = By.XPath("//*[text()='GO TO HOLDING AREA']");
        public readonly By YourActivityHasBeenSubmittedCloseBtn = By.XPath("//div[@class='modal-window linkingActivityModalView']//div[@class='modal-close ']");
        public readonly By UploadFilesBtn = By.XPath("//input[@type='file']");
        public readonly By CancelFormYesBtn = By.XPath("//div[contains(@class, 'CancelConfirmYesButton')]//span[text()='Yes']");
        public readonly By CancelFormNoBtn = By.XPath("//div[contains(@class, 'CancelConfirmNoButton')]//span[text()='No']");
        public readonly By MaxCreditReachedFormAddNonCertActBtn = By.XPath("//span[text()='ADD NON-CERTIFIED ACTIVITY']");
        public readonly By MaxCreditReachedFormUpdateCurrentFormBtn = By.XPath("//span[text()='UPDATE CURRENT FORM']");
        public readonly By YourActivityHasBeenSavedBannerXBtn = By.XPath("//div[@data-attach-point='dismissContainer']");


        // Charts

        // Check boxes        
        public readonly By PeerReviewedArticlesChk = By.XPath("//input[@type='checkbox']/following-sibling::span[text()='Peer-reviewed articles']");
        public readonly By WhatSourcesFirstChk = By.XPath("//input[@type='checkbox']/following-sibling::span[text()='Discussion with a colleague']");
        public readonly By AssignedReadingChk = By.XPath("//input[@type='checkbox']/following-sibling::span[text()='Assigned reading']");
        public readonly By CollaboratorChk = By.XPath("//input[@type='checkbox']/following-sibling::span[text()='Collaborator']");
        public readonly By ConferenceOrWorkshopChk = By.XPath("//input[@type='checkbox']/following-sibling::span[text()='Conference or workshop']");
        public readonly By ClinicalPreceptorChk = By.XPath("//input[@type='checkbox']/..//span[contains(text(), 'Clinical preceptor')]");

        // Labels                                              
        public readonly By CreditApprovalRequiredAndDocUploadOptionalLbl = By.XPath("//b[text()='Credit Approval Required and Documentation Optional']");
        public readonly By CreditApprovalRequiredAndDocUploadRequiredLbl = By.XPath("//b[text()='Credit Approval and Documentation Required']");
        public readonly By DocUploadRequiredLbl = By.XPath("//b[text()='Documentation Required']");
        public readonly By YourActivityHasBeenSavedBannerLbl = By.XPath("//span[text()='Your activity has been saved in the Holding area.']");
        public readonly By MaxCreditReachedFormClaimedLbl = By.XPath("//span[contains(text(), 'credits claimed for this activity')]/following-sibling::span");
        public readonly By MaxCreditReachedFormUpdateAppliedLbl = By.XPath("//span[contains(text(), 'credits applied for this activity')]/following-sibling::span");
        public readonly By MaxCreditReachedFormUpdateNotAppliedLbl = By.XPath("//b[contains(text(), 'not applied')]/../following-sibling::span");
        public readonly By CreditRefreshIsCompleteLbl = By.XPath("//span[text()='Credit Refresh is Completed']");
        public readonly By UploadedFilesLbl = By.XPath("//span[text()='UPLOADED FILES ']");
        public readonly By CPDActivityPageLbl = By.XPath("//h1[@class='dashboard-heading']");
        public readonly By CPDActivityPageLoadingIndicator = By.XPath("//div[@id='app'][@aria-hidden='true']");



        // Links


        // Menu Items    

        // Radio buttons
        public readonly By CertifiedRdo = By.XPath("//div[contains(@aria-labelledby, 'ecpdActivityForm')]//span[text()='Certified']/preceding-sibling::input");
        public readonly By ChooseOneQuesToAnswerArticleFirstRdo = By.XPath("//div[contains(text(), 'Choose one ques')]/..//input");
        public readonly By DidYouPerceiveYesRdo = By.XPath("//div[contains(text(), 'Did you perceive')]/..//input");
        public readonly By DidthisActivityYesRdo = By.XPath("//div[contains(text(), 'Did this activity')]/..//input");
        public readonly By DoYouAnticipateThisExperienceBenefitingYesRdo = By.XPath("//div[contains(text(), 'Do you anticipate this experience benefiting your practice')]/..//input");
        public readonly By DoYouForeseeYesRdo = By.XPath("//div[contains(text(), 'Do you foresee')]/..//input");
        public readonly By HowSuccessfullVerySuccessfullRdo = By.XPath("//div[contains(text(), 'How successful')]/..//input");
        public readonly By HowWillThisQuestionChangeYourPractice1TheChangeWillBeLargeRdo = By.XPath("((//div[text()='How will this question change your practice?'])[1]/..//input)[1]");
        public readonly By HowWillThisQuestionChangeYourPractice2TheChangeWillBeLargeRdo = By.XPath("((//div[text()='How will this question change your practice?'])[2]/..//input)[1]");
        public readonly By HowWillThisQuestionChangeYourPractice3TheChangeWillBeLargeRdo = By.XPath("((//div[text()='How will this question change your practice?'])[3]/..//input)[1]");
        public readonly By HowWillThisQuestionChangeYourPractice4TheChangeWillBeLargeRdo = By.XPath("((//div[text()='How will this question change your practice?'])[4]/..//input)[1]");
        public readonly By HowWillThisQuestion2ChangeYourPractice1TheChangeWillBeLargeRdo = By.XPath("//div[text()='Question 2']/following::input[@type='radio'][1]");
        public readonly By HowWillThisQuestion2ChangeYourPractice2TheChangeWillBeLargeRdo = By.XPath("//div[text()='Question 2']/following::input[@type='radio'][2]");
        public readonly By HowWillThisQuestion2ChangeYourPractice3TheChangeWillBeLargeRdo = By.XPath("//div[text()='Question 2']/following::input[@type='radio'][3]");
        public readonly By HowWillThisQuestion2ChangeYourPractice4TheChangeWillBeLargeRdo = By.XPath("//div[text()='Question 2']/following::input[@type='radio'][4]");
        public readonly By IfYouAreSubmitting1Rdo = By.XPath("//div[contains(text(), 'If you are submitting')]/..//input");
        public readonly By IAmMotivatedToLearnMoreYesRdo = By.XPath("(//div[contains(text(), 'I am motivated to learn more')]/..//input)[1]");
        public readonly By ILearnedSomethingNewYesRdo = By.XPath("(//div[contains(text(), ' I learned something new')]/..//input)[1]");
        public readonly By IndicateYourRoleForThisActivityAssessmentOfSelfRdo = By.XPath("(//div[contains(text(), 'Indicate your role for this activity')]/..//input)[1]");
        public readonly By IPerceivedBiasYesRdo = By.XPath("//div[contains(text(), 'I perceived bias')]/..//input");
        public readonly By IPerceivedBiasNoRdo = By.XPath("(//div[contains(text(), 'I perceived bias')]/..//input)[2]");
        public readonly By IPerceiveAnyDegreeofBiasYesRdo = By.XPath("//div[contains(text(), 'perceive any degree of bias')]/following::input[1]");

        public readonly By IsThisActivityAccreditedByAnotherOrgNoRdo = By.XPath("(//div[contains(text(), 'Is this activity accredited by another organization?')]/..//input)[2]");
        public readonly By IWasDissatisfiedYesRdo = By.XPath("//div[contains(text(), 'I was dissatisfied for another reason')]/..//input");
        public readonly By IWasDissatisfiedNoRdo = By.XPath("(//div[contains(text(), 'I was dissatisfied for another reason')]/..//input)[2]");
        public readonly By MyPracticeWillBeChangedAndImprovedYesRdo = By.XPath("//div[contains(text(), 'My practice will be changed')]/..//input");
        public readonly By MyPracticeWillBeChangedAndImprovedNoRdo = By.XPath("(//div[contains(text(), 'My practice will be changed')]/..//input)[2]");
        public readonly By NonCertifiedRdo = By.XPath("//div[contains(@aria-labelledby, 'ecpdActivityForm')]//span[text()='Non-Certified']/preceding-sibling::input");
        public readonly By PleaseIndicateYourRoleInThisAssessmentAssessorRdo = By.XPath("//div[contains(text(), 'Please indicate your role in this assessment activity:')]/..//input");
        public readonly By PleaseIndicateYourRoleInThisAssessmentAssessedRdo = By.XPath("(//div[contains(text(), 'Please indicate your role in this assessment activity:')]/..//input)[2]");
        public readonly By ProfessionalActivitiesFirstRdo = By.XPath("(//div[contains(text(), 'Professional activities that can stimulate thinking about your practice and/or work')]/..//input)[1]");
        public readonly By TheExperienceConfirmedIAmDoingTheRightThingYesRdo = By.XPath("(//div[contains(text(), 'This experience confirmed I am doing the right thing')]/..//input)[1]");
        public readonly By TypeOfFeedback1FirstRdo = By.XPath("//div[contains(text(), 'Type of Feedback')]/..//input");
        public readonly By TypeOfFeedback2FirstRdo = By.XPath("(//div[contains(text(), 'Type of Feedback')])[2]/..//input");
        public readonly By UponWhatKindFirstRdo = By.XPath("//div[contains(text(), 'Upon what kind')]/..//input");
        public readonly By WereTheResultsYesRdo = By.XPath("//div[contains(text(), 'Were the results')]/..//input");
        public readonly By WhatTypeOfActivityPatientEncounterRdo = By.XPath("//div[contains(text(), 'What type of activity')]/..//input");
        public readonly By WhoDidtheLiteratureSearchMyselfRdo = By.XPath("//div[contains(text(), 'Who did the literature search?')]/..//input");

        
        // Select Element
        public readonly By ActivityTypeSelElem = By.XPath("//div[contains(@class, 'form-section-page1')]//div[contains(text(), 'Activity Type')]/following-sibling::div//select");
        public readonly By ActivityTypeSelElemBtn = By.XPath("//div[contains(@class, 'form-section-page1')]//div[contains(text(), 'Activity Type')]/following-sibling::div//button");
        public readonly By CategorySelElem = By.XPath("//div[contains(@class, 'form-section-page1')]//div[contains(text(), 'Category')]/following-sibling::div//select");
        //public readonly By IndicateYourRoleInThisActSelElem = By.XPath("//div[contains(text(), 'Indicate your role in this activity.')]/following-sibling::div//select");
       // public readonly By IndicateYourRoleInThisActSelElemBtn = By.XPath("//div[contains(text(), 'Indicate your role in this activity.')]/following-sibling::div//button");
        public readonly By IdentifyTheTypeOfAssessmentActivitySelElem = By.XPath("//div[contains(text(), 'Identify the type of assessment activity')]/following-sibling::div//select");
        public readonly By IdentifyTheTypeOfAssessmentActivitySelElemBtn = By.XPath("//div[contains(text(), 'Identify the type of assessment activity')]/following-sibling::div//button");
        public readonly By ProvinceSeleElem = By.XPath("//div[contains(text(), 'Province')]/following-sibling::div//select");
        public readonly By ProvinceSeleElemBtn = By.XPath("//div[contains(text(), 'Province')]/following-sibling::div//button");
        public readonly By DidYouParticipateInSelElem = By.XPath("//div[contains(text(), 'Did you participate in')]/following-sibling::div//select");
        public readonly By DidYouParticipateInSelElemBtn = By.XPath("//div[contains(text(), 'Did you participate in')]/following-sibling::div//button");


        /// The following By represents all of the different activity form's Select Elementss for the activity title/id/description/etc.
        /// Meaning each activity form will have different title/id/description/etc. fields, but I am lumping them into one By because
        /// it will simplify the methods to fill out the forms, and it will be easy to indicate which fields represent the activity name
        public readonly By ProgramTitleSelElem = By.XPath("//div[contains(text(), 'Program Title')]/following-sibling::div//select | //div[contains(text(), 'Name of the exam?')]/following-sibling::div//select | //div[contains(text(), 'For which examination did you prepare?')]/following-sibling::div//select | //div[contains(text(), 'Indicate your role in this activity.')]/following-sibling::div//select");
        public readonly By ProgramTitleSelElemBtn = By.XPath("//div[contains(text(), 'Program Title')]/following-sibling::div//button| //div[contains(text(), 'Name of the exam?')]/following-sibling::div//button| //div[contains(text(), 'For which examination did you prepare?')]/following-sibling::div//button | //div[contains(text(), 'Indicate your role in this activity.')]/following-sibling::div//button");

        // Tables 


        // Tabs

        // Text boxes
        public readonly By AssessTheInformationTxt = By.XPath("//textarea[contains(@aria-label, 'Assess the information')]");
        public readonly By AudienceTxt = By.XPath("//input[contains(@aria-label, 'Audience:')]");
        public readonly By ArticleTxt = By.XPath("//input[contains(@aria-label, 'Article')]");
        public readonly By SettingTxt = By.XPath("//input[contains(@aria-label, 'Setting:')]");
        public readonly By BasedOnTheFeedbackTxt = By.XPath("//textarea[contains(@aria-label, 'Based on the feedback')]");
        public readonly By BasedOnWhatYouLearnedTxt = By.XPath("//textarea[contains(@aria-label, 'Based on what you learned')] | //textarea[contains(@aria-label, 'Based on what you have learned')]");
        public readonly By BasedOnThisFeedback1Txt = By.XPath("//textarea[contains(@aria-label, 'Based on this feedback')]");
        public readonly By BasedOnThisFeedback2Txt = By.XPath("(//textarea[contains(@aria-label, 'Based on this feedback')])[2]");
        public readonly By BasedOnYourAppraisalTxt = By.XPath("//input[contains(@aria-label, 'Based on your appraisal')]");
        public readonly By BasedUponTxt = By.XPath("//textarea[contains(@aria-label, 'Based upon')]");
        public readonly By BrieflyDescribeTheAssessmentTxt = By.XPath("//textarea[contains(@aria-label, 'Briefly describe the assessment process')]");
        public readonly By BrieflyDescribeTheOutcomeTxt = By.XPath("//textarea[contains(@aria-label, 'Briefly describe the outcome of the assessment')]");
        public readonly By BrieflyDescribeTheAuditTxt = By.XPath("//textarea[contains(@aria-label, 'Briefly describe the audit')]");
        public readonly By BrieflyDescribeTheFindingsTxt = By.XPath("//textarea[contains(@aria-label, 'Briefly describe the findings')]");
        public readonly By BrieflyDescribeTheExamActInWhichYouParticipatedTxt = By.XPath("//textarea[contains(@aria-label, 'Briefly describe the exam activity in which you participated')]");
        public readonly By CityTxt = By.XPath("//input[@aria-label='City']");
        public readonly By DateOfReflectionTxt = By.XPath("//div[contains(text(), 'Date of Reflection')]/..//input | //div[contains(text(), 'Date of Refection')]/..//input");
        public readonly By DescribeAnyKeySuccessesTxt = By.XPath("//textarea[contains(@aria-label, 'Describe any key successes')]");    
        public readonly By DescribeHowThisCourseTxt = By.XPath("//textarea[contains(@aria-label, 'Describe how this course')]");
        public readonly By DescribeHowYourAdminstrativeTxt = By.XPath("//textarea[contains(@aria-label, 'Describe how your administrative')]");
        public readonly By DescribeHowParticipatingInThisCourseTxt = By.XPath("//textarea[contains(@aria-label, 'Describe how participating in this course')]");
        public readonly By DescribeTheActivityTxt = By.XPath("//textarea[contains(@aria-label, 'Describe the learning activity')] | //input[@aria-label='Describe the learning activity.'] | //textarea[contains(@aria-label, 'Describe the activity')] | //input[contains(@aria-label, 'Describe the research activity')]");
        public readonly By DescribeTheCourseTxt = By.XPath("//textarea[contains(@aria-label, 'Describe the course')]");
        public readonly By DescribeTheCriticalTxt = By.XPath("//textarea[contains(@aria-label, 'Describe the critical')]");
        public readonly By DescribeTheFeedbackTxt = By.XPath("//textarea[contains(@aria-label, 'Describe the feedback')]");
        public readonly By DescribeTheNatureOfYourPracticeTxt = By.XPath("//textarea[contains(@aria-label, 'Describe the nature of your practice')] | //textarea[contains(@aria-label, 'Describe the nature of the practice')]");
        public readonly By DescribeTheOutcomeTxt = By.XPath("//textarea[contains(@aria-label, 'Describe the outcome')]");
        public readonly By DescribeTheProcessTxt = By.XPath("//textarea[contains(@aria-label, 'Describe the process')]");
        public readonly By DescribeTheTeachingTxt = By.XPath("//textarea[contains(@aria-label, 'Describe the Teaching')]");
        public readonly By DescribeWhatYouHaveLearnedTxt = By.XPath("//textarea[contains(@aria-label, 'Describe what you have learned from this activity')]");
        public readonly By DescribeWhatWasLearnedAsAResultTxt = By.XPath("//textarea[contains(@aria-label, 'Describe what was learned as a result of participating in the activity.')]");
        public readonly By DescribeYourActionPlanTxt = By.XPath("//*[@aria-label='Describe your action plan based on review of your data and/or feedback:'] | //textarea[contains(@aria-label, 'Describe your action plan based on review of your data and/or feedback')]");
        public readonly By DescribeYourCriticalAppraisalTxt = By.XPath("//textarea[contains(@aria-label, 'Describe your critical appraisal')]");
        public readonly By DescribeYourRoleTxt = By.XPath("//input[contains(@aria-label, 'Describe your role')]");
        public readonly By ExplainToWhatTxt = By.XPath("//textarea[contains(@aria-label, 'Explain to what')]");
        public readonly By FormatOfFeedback1Txt = By.XPath("//textarea[contains(@aria-label, 'Format of Feedback')]");
        public readonly By FormatOfFeedback2Txt = By.XPath("(//textarea[contains(@aria-label, 'Format of Feedback')])[2]");
        public readonly By ForThePurposeOfThisExerciseTxt = By.XPath("//textarea[contains(@aria-label, 'For the purpose of this exercise')]");
        public readonly By FromTheFeedback1Txt = By.XPath("//textarea[contains(@aria-label, 'From the feedback')]");
        public readonly By FromTheFeedback2Txt = By.XPath("(//textarea[contains(@aria-label, 'From the feedback')])[2]");
        public readonly By FurtherDescribeTheActivityTxt = By.XPath("//textarea[contains(@aria-label, 'Further describe the activity')]");
        public readonly By HowSuccessfullTxt = By.XPath("//*[contains(@aria-label, 'Gather information')]");
        public readonly By GatherInformationTxt = By.XPath("//textarea[contains(@aria-label, 'How successful')]");
        public readonly By HowWillYouIntegrateTxt = By.XPath("//textarea[contains(@aria-label, 'How will you integrate')]");
        public readonly By HowWouldThisExperienceTxt = By.XPath("//textarea[contains(@aria-label, 'How would this experience')]");
        public readonly By HowWillYouIncorporateTheKnowledgeYouGainedTxt = By.XPath("//textarea[contains(@aria-label, 'How will you incorporate the knowledge you gained')]");
       // public readonly By InGeneralTermsTxt = By.XPath("//input[contains(@aria-label, 'In general terms')]");
        public readonly By InTheTextBoxBelowTxt = By.XPath("//textarea[contains(@aria-label, 'In the text box below')] |//input[contains(@aria-label, 'In the text box below')]");
        public readonly By LocationsTxt = By.XPath("//textarea[@aria-label='Location(s)']");
        public readonly By ListTheJournalArticlesTxt = By.XPath("//textarea[contains(@aria-label, 'List the journal articles')]");
        public readonly By IfYouAreSubmittingTxt = By.XPath("//textarea[contains(@aria-label, 'If you are submitting')]");
        public readonly By IfYesWhatMightTheseBenefitsBeTxt = By.XPath("//textarea[contains(@aria-label, 'If yes, what might these benefits be')]");
        public readonly By IndicateYourRoleForThisActTxt = By.XPath("//input[contains(@aria-label, 'Indicate your role for this activity.')]");
        public readonly By NameOfCourseTxt = By.XPath("//input[@aria-label='Name of Course']");
        public readonly By NameOfUniversityTxt = By.XPath("//input[@aria-label='Name of University']");
        public readonly By OversightOrganizationTxt = By.XPath("//textarea[contains(@aria-label, 'Oversight organization')]");
        public readonly By ObtainFeedbackTxt = By.XPath("//input[contains(@aria-label, 'Obtain feedback')]");
        public readonly By PlanningOrganizationTxt = By.XPath("//input[@aria-label='Planning Organization']");
        public readonly By PleaseExplainTxt = By.XPath("//textarea[contains(@aria-label, 'Please explain')]");
        public readonly By PleaseDescribeTheTeachingTxt = By.XPath("//*[contains(@aria-label, 'Please describe the teaching')]");
        public readonly By PleaseDescribeYourReflectionsTxt = By.XPath("//textarea[contains(@aria-label, 'Please describe your reflections')]");
        public readonly By ProgramActivityIDTxt = By.XPath("//input[@aria-label='Program/Activity ID']");
        public readonly By SupervisorTxt = By.XPath("//input[@aria-label='Supervisor']");
        public readonly By WhatApproachOrToolsTxt = By.XPath("//textarea[contains(@aria-label, 'What approach or tools')]");
        public readonly By WhatChangesTxt = By.XPath("//textarea[contains(@aria-label, 'What changes')]");
        public readonly By WhatDidYouLearnTxt = By.XPath("//textarea[contains(@aria-label, 'What did you learn')]");
        public readonly By WhatFeedbackTxt = By.XPath("//textarea[contains(@aria-label, 'What feedback')]");
        // Need this extra one for Linking Learning To Research activity. There are 2 What Feedback text boxes on that one
        public readonly By WhatFeedbackHaveYouReceivedFromReviewersTxt = By.XPath("//textarea[contains(@aria-label, 'What feedback have you received from reviewers')]");
        public readonly By WhatImpactTxt = By.XPath("//textarea[contains(@aria-label, 'What impact')] | //input[contains(@aria-label, 'What impact')]");
        public readonly By WhatIsYourassessmentTxt = By.XPath("//textarea[contains(@aria-label, 'What is your assessment')]");
        public readonly By WhatKindOfInformationTxt = By.XPath("//textarea[contains(@aria-label, 'What kind of information')]");
        public readonly By WhatMeshTxt = By.XPath("//textarea[contains(@aria-label, 'What MeSH')]");
        public readonly By WhatMustYouDoTxt = By.XPath("//textarea[contains(@aria-label, 'What must you do')]");
        public readonly By WhatOpportunitiesTxt = By.XPath("//textarea[contains(@aria-label, 'What opportunities')]");
        public readonly By WhatResourcesWouldSupportYouTxt = By.XPath("//textarea[contains(@aria-label, 'What resources would support you')]");
        public readonly By WhatResourcesWouldSupportYouInAdvancingYourUnderstandingOfThisAssessmentTxt = By.XPath("//textarea[contains(@aria-label, 'What resources would support you in advancing your understanding of this Assessment activity?')]");
        public readonly By WhatStepTxt = By.XPath("//textarea[contains(@aria-label, 'What step')]");
        public readonly By WhatTriggeredThisQuestionTxt = By.XPath("//textarea[contains(@aria-label, 'What triggered this question?')]");
        public readonly By WhatWasTheOriginTxt = By.XPath("//textarea[contains(@aria-label, 'What was the origin')]");
        public readonly By WhatWasYourRoleTxt = By.XPath("//textarea[contains(@aria-label, 'What was your role')]");
        public readonly By WhatIstheSpecificAreaTxt = By.XPath("//textarea[contains(@aria-label, 'What is the specific area')]");
        public readonly By WhatWasYourAssessmentTxt = By.XPath("//textarea[contains(@aria-label, 'What was your assessment')]");
        public readonly By WhatWereYourLearningTxt = By.XPath("//textarea[contains(@aria-label, 'What were your learning')]");
        public readonly By WhatWereYourReasonsForWritingTxt = By.XPath("//textarea[contains(@aria-label, 'What were your reasons for writing')]");
        public readonly By WhatWereYourSpecificQuestionsTxt = By.XPath("//textarea[contains(@aria-label, 'What were your specific questions')] | //textarea[contains(@aria-label, 'What was your specific question')]");
        public readonly By WhatWillYouTakeTxt = By.XPath("//textarea[contains(@aria-label, 'What will you take')]");
        public readonly By WhatWillYouHaveToDoTxt = By.XPath("//textarea[contains(@aria-label, 'What will you have to do')]");
        public readonly By WhatWouldYouDoDifferentlyTxt = By.XPath("//textarea[contains(@aria-label, 'What would you do differently next time')]");
        public readonly By WhyDidYouAddressTxt = By.XPath("//textarea[contains(@aria-label, 'Why did you address')]");
        public readonly By WhoWasInvolvedTxt = By.XPath("//textarea[contains(@aria-label, 'Who was involved')]");
        public readonly By AssessedMaxCreditsTxt = By.XPath("//input[contains(@aria-label, 'Assessed Credits Claimed Cannot Exceed')]");
        public readonly By AssessorMaxCreditsTxt = By.XPath("//input[contains(@aria-label, 'Assessor Credits Claimed Cannot Exceed')]");

        // Special case elements
        /// The following Bys represents represent a many-to-one (many elements in one By) relationship. For example, activities 
        /// may have different labels (which results in different xpaths) to represent the activities actual title. 
        /// I am lumping those cases into one By because it will simplify the methods to fill out the 
        /// forms, and it will be easy to indicate which fields represent the title/credit/etc.
        public readonly By CreditsRequestedOrClaimedTxt = By.XPath("//input[@aria-label='Credits Claimed'] | //input[@aria-label='Credits Requested'] | //input[contains(@aria-label, 'Credits requested')] | //input[contains(@aria-label, 'Credits requested (you may request one credit per hour)')] |  //input[contains(@aria-label, 'Credits Requested')]");
        public readonly By HoursTxt = By.XPath("//input[@aria-label='Total Hours of Participation'] | //input[@aria-label='Approximate Number of Hours Involved?'] | //input[@aria-label='Total Hours of Participation'] | //input[@aria-label='Approximate Number of Hours Involved?'] | //input[contains(@aria-label, 'Number of Hours Spent on This Exercise to Date')] | //input[contains(@aria-label, 'How many hours did you spend personally in the process?')] | //input[contains(@aria-label, 'Approximate number of hours involved in the learning activity')] | //input[contains(@aria-label, 'Duration')] | //input[contains(@aria-label, 'Approximate Number of Hours Involved')]");
        public readonly By ArticlesTxt = By.XPath("//input[@aria-label='How many articles did you yield?']");
        public readonly By ProgramActivityTitleOrIDOrDescriptionOrSessionEtcTxt = By.XPath("//input[@aria-label='Program/Activity Title'] | //input[@aria-label='Program Title or Activity Name (or session)'] | //input[contains(@aria-label, 'Program Title or Activity Type (or session)')] | //*[@aria-label='Program or Activity Description'] | //input[@aria-label='Program Title'] | //input[@aria-label='Activity Title/Name'] | //textarea[contains(@aria-label, 'Briefly describe the Assessment activity in which you participated')] | //textarea[contains(@aria-label, 'Briefly describe the Assessment activity in which you participated')] | //textarea[contains(@aria-label, 'Briefly describe the assessment activity in which you participated')] | //input[@aria-label='What is your specific practice-based clinical question?'] | //input[@aria-label='Name of Program'] | //textarea[contains(@aria-label, 'Name and Type of Degree')] | //input[contains(@aria-label, 'Describe the administrative')] | //textarea[contains(@aria-label, 'Describe your role in the research process')] | //input[contains(@aria-label, 'Description')] | //input[contains(@aria-label, 'Program Title or Activity Type')] | //textarea[contains(@aria-label, 'Program or Activity Description')] | //input[contains(@aria-label, 'Name of Journal')] | //textarea[contains(@aria-label, 'Describe the nature of the practice to which this audit')] | //input[contains(@aria-label, 'In general terms, describe your traineeship')] | //textarea[contains(@aria-label, 'Describe the nature of your practice and/or work to which this exercise applies')] | //input[contains(@aria-label, 'Audience:')] | //input[contains(@aria-label, 'Volume')] | //textarea[contains(@aria-label, 'Describe the assessment activity that stimulated this reflection exercise')] | //textarea[contains(@aria-label, 'Describe the activity that stimulated')] | //*[contains(@aria-label, 'Please describe the teaching activity, audience')]");
        public readonly By ActivityCompletionDateTxt = By.XPath("//div[contains(text(), 'Assessment Completion Date')]/..//input | //div[contains(text(), 'Assessment completion date')]/..//input | //div[contains(text(), 'Activity Completion Date')]/..//input | //div[contains(text(), 'Activity completion date')]/..//input | //div[contains(text(), 'Date of Submission')]/..//input | //div[contains(text(), 'Course End Date')]/..//input | //div[contains(text(), 'Course end date')]/..//input | //div[text()='End Date']/..//input");
        public readonly By ActivityStartDateTxt = By.XPath("//div[contains(text(), 'Assessment Start Date')]/..//input | //div[contains(text(), 'Assessment start date')]/..//input | //div[contains(text(), 'Activity Start Date')]/..//input | //div[contains(text(), 'Activity start date')]/..//input | //div[contains(text(), 'Course Start Date')]/..//input | //div[contains(text(), 'Course start date')]/..//input  | //div[contains(text(), 'Activity Start date')]/..//input | //div[contains(text(), 'Start Date')]/..//input");
        public readonly By CompleteActivitySummaryInCompleteActivitiesBtn = By.XPath("//table[contains(@aria-labelledby,'cpdSumaryTabIncompleteActivitiesGrid')]//tbody//tr[1]/td[4]/following::span[@class='button-label'][1]");

        /// This radio button appears whenever the Pearls activity type is selected. It shows a radio button instead of a select 
        /// element for some reason. I think this is a bug. Anyway, I need to locate this radio button in order for the Activity 
        /// Type property to be populated within the Activity object, when calling the FillActivityForm method. See the top of that 
        /// method for the code that uses this element
        public readonly By ActivityTypeRdo = By.XPath("//div[contains(@class, 'form-section-page1')]//div[contains(text(), 'Activity Type')]/following-sibling::div/span//span");


    }
}
