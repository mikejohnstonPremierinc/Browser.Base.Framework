using Browser.Core.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Data;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Browser.Core.Framework.Utils;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using LMSAdmin.AppFramework;
using System.ComponentModel.DataAnnotations; // need to add a nuget package (System.ComponentModel.DataAnnotations) for this for it to work. https://stackoverflow.com/questions/10174420/why-cant-i-reference-system-componentmodel-dataannotations
using System.Reflection;
using System.Configuration;
using OpenQA.Selenium.Interactions;
using System.Globalization;
using System.Linq;
using LMS.Data;

namespace LMSAdmin.AppFramework.HelperMethods
{
    /// <summary>
    /// A class that consists of methods which combine custom page methods to accomplish various tasks for this application. This is mainly
    /// called/used when a tester is automating another application, and needs to also access this application to setup data or verify functionality
    /// </summary>
    public class LMSAdminHelperGeneral
    {
        #region methods

        /// <summary>
        /// Creates an activity. Specifically this adds a new project (Clicks on the Projects tab, clicks Manage Projects, selects the project 
        /// type, fills in the required fields with random strings and clicks Save), then adds an activity to this project (Fills in all required 
        /// fields, then clicks Save). Note: For an activity to be set to Construction Complete, you need to add an Accreditation.
        /// This method returns <see cref="Activity"/>, which will allow you to add an accreditation
        /// </summary>
        /// <param name="Browser">The driver instance</param>
        /// <param name="projType">(Optional) The project type. If null, then will default to standalone activity <see cref="Constants_LMSAdmin.ProjectType"/> </param>
        /// <param name="actName">(Optional) Your activity name. If null, then will generate one for you</param>
        /// <returns></returns>
        public Activity CreateActivity(IWebDriver Browser, Constants.ProjectType projType = Constants.ProjectType.StandaloneActivity,
            string actName = null, Constants_LMSAdmin.LoginUserNames userName = Constants_LMSAdmin.LoginUserNames.AAFPPN, string password = null)
        {
            if (!Browser.Exists(Bys.Page.LogoutLnk, ElementCriteria.IsVisible))
            {
                LoginPage LP = Navigation.GoToLoginPage(Browser);
                LP.Login(userName, password);
            }

            // If actName is null, use a random string, else use the activity name that the tester passed
            string timeStamp = string.Format("{0}", DateTime.UtcNow.ToString("MM-dd-yy HH:mm:ss.fff", CultureInfo.InvariantCulture));
            actName = string.IsNullOrEmpty(actName) ? string.Format("AutoAct {0}", timeStamp) : actName;

            MyDashboardPage MDP = new MyDashboardPage(Browser);

            // Create a new project
            ProjectsPage PP = MDP.ClickAndWaitBasePage(MDP.ProjectsTab);
            Projects_ManagePage PMP = PP.ClickAndWait(PP.ManageProjectsLnk);
            Projects_AddEditPage AEP = PMP.GoToAddNewProjectPage(projType.GetDescription());
            AEP.FillAddProjectForm();
            AEP.ClickAndWait(AEP.SaveBtn);

            // Go to the main activity page, change the name of the activity and save
            ActMainPage AMP = AEP.ClickAndWaitBasePage(AEP.TreeLinks_MainActivity);
            AMP.DetailsTabActivityTitleTxt.Clear();
            AMP.DetailsTabActivityTitleTxt.SendKeys(actName);
            AMP.DetailsTabShortLabelTxt.Clear();
            AMP.DetailsTabShortLabelTxt.SendKeys(actName);
            AMP.DetailsTabDescriptionTxt.Clear();
            AMP.DetailsTabDescriptionTxt.SendKeys("This activity is created and configuration setup done for Automation Testing usage");
            AMP.ClickAndWait(AMP.DetailsTabSavebtn);

            string AID = AMP.DetailsTabActivityNumberLbl.Text;

            return new Activity(Browser, projType.GetDescription(), actName, AID, null, new Location(), new List<Accreditation>(), 
                new List<Assessment>());
        }

        /// <summary>
        /// Logs in. This Login method should only be called/used from within this class (mainly when coding tests on another application).
        /// Otherwise, the Login page class's Login method should be used
        /// </summary>
        /// <param name="browser">The driver instance</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Page Login(IWebDriver browser, Constants_LMSAdmin.LoginUserNames username, string password)
        {
            string url = string.Format("{0}{1}", AppSettings.Config["CME360URL"].ToString(), "SignIn.aspx");
            browser.Navigate().GoToUrl(url);

            LoginPage LP = new LoginPage(browser);
            LP.Login(username, password);
            return LP;
        }

        public List<Activity> CreateActivities(IWebDriver Browser, Constants.SiteCodes sitecode, int numOfActivities)
        {
            List<Activity> Activities = new List<Activity>();

            for (int i = 0; i < numOfActivities; i++)
            {
                Activities.Add(CreateActivity(Browser).
                AddAccreditation().
                AddFrontMatter().
                AddAssessment().
                AddAward().
                Save().
                Publish().
                AddCatalog(sitecode));
            }
            return Activities;
        }

        

        #endregion methods
    }

    public class Activity
    {
        #region properties

        public IWebDriver Browser { get; set; }
        public string ProjType { get; set; }
        public string ActName { get; set; }
        public string ActID { get; set; }
        public string FrontMatterText { get; set; }
        public List<Accreditation> Accreditations { get; set; }
        public List<Assessment> Assessments { get; set; }
        public Location Location { get; set; }



        #endregion properties

        #region constructors

        public Activity(IWebDriver browser, string projType, string actName, string actID, string frontMatterText, Location location, 
            List<Accreditation> accreditations,
            List<Assessment> assessments)
        {
            Browser = browser;
            ProjType = projType;
            ActName = actName;
            ActID = actID;
            FrontMatterText = frontMatterText;
            Location = location;
            Accreditations = accreditations;
            Assessments = assessments;
        }

        #endregion constructors

        #region methods

        /// <summary>
        /// Adds an accreditation to an activity that was created with 
        /// <see cref="LMSAdminHelperGeneral.CreateActivity(IWebDriver, Constants_LMSAdmin.ProjectType, string)"/>. 
        /// Specifically this clicks on the Accreditation tree link, clicks on the Add Accreditation Type link, 
        /// clicks on the user-specified accreditation radio button. Note that if you dont specify an accreditation, then this will choose the 
        /// first accredited type in the table for you (If there is no types that are accredited, it will choose the first non-accredited type). 
        /// Clicks Add Scenario, fills in the scenario name with random text, adds all professionals to be eligible to this scenario, enters in the 
        /// user-specified amount of credits, then saves the accrediation
        /// </summary>
        /// <param name="accredited">'true' if you want this accreditation to be accredited. By default, this is true. Note that if there are no accredited options available for this activity, or if the accredited radio button is disabled, then it will choose a non-accredited one</param>
        /// <param name="amountOfCredits">(Optional) The amount of credits you want to add. If null, then will default to 5</param>
        /// <param name="accreditationBody">(Optional) The exact text from the Accreditation Body column in the Accreditations table. If null, 
        /// then this will choose the first accreditated option in the table</param>
        /// <param name="accreditationType">(Optional) The exact text from the Accreditation Type column in the Accreditations table. If null, 
        /// then this will choose the first accreditated option in the table</param>
        /// <returns></returns>
        public Activity AddAccreditation(bool accredited = true, int amountOfCredits = 5, string accreditationBody = null, 
            string accreditationType = null)
        {
            if (!Assessments.IsNullOrEmpty())
            {
                throw new Exception("You have already added assessment(s). This code is only setup to allow addition of Accreditations first, then " +
                    "Assessments. Rearrange the order in which you are calling AddAcreditation and AddAssessment, then run this again");
            }

            ActMainPage AMP = new ActMainPage(Browser);
            Legacy_ActAccreditationPage AccPage = new Legacy_ActAccreditationPage(Browser);
            
            AMP.ClickAndWaitLegacyBasePage(AMP.TreeLinks_Legacy_Accreditation);
            AccPage.ClickAndWait(AccPage.AddAccreditationTypeLnk);

            // Choose an accreditation type:

            // If the accreditationBody is empty, then choose a body for the tester depending on different conditions...
            IWebElement accreditedRadioBtn = null;
            if (accreditationBody.IsNullOrEmpty())
            {
                // This xpath is for the second row's radio button (if it exists (i.e. accredited row))
                string firstAccreditedTypeXpath = "//div[@id='ctl00_Panel_NonAccrRow']/../table[2]/tbody/tr[3]/td/input[not(@disabled)]";

                // If tester specified he/she wants it to be accredited and there is an accredited option available AND it is enabled, choose it
                if (accredited && Browser.FindElements(By.XPath(firstAccreditedTypeXpath)).Count > 0)
                {
                    accreditedRadioBtn = Browser.FindElement(By.XPath(firstAccreditedTypeXpath));
                }
                // else, choose non-accredited option
                else // (!accredited || Browser.FindElements(By.XPath(firstAccreditedTypeXpath)).Count == 0) 
                {
                    accreditedRadioBtn = Browser.FindElement(By.XPath("//div[@id='ctl00_Panel_NonAccrRow']/../table[2]/tbody/tr[2]/td/input"));
                }

                accreditedRadioBtn.Click();
            }
            // else choose the user-specified body
            else
            {
                IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowNameAndAdditionalCellName(Browser, AccPage.AvailableAccreditationTypesTbl,
                    Bys.Legacy_ActAccreditationPage.AvailableAccreditationTypesTblFirstRow, accreditationBody, "td", accreditationType, "td");

                accreditedRadioBtn = ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "input", "Radio");
            }

            // Get the accreditation body and Type text to use later when we add a scenario
            accreditationBody = accreditedRadioBtn.FindElement(By.XPath("ancestor::tr[1]/descendant::td[2]")).Text;
            accreditationType = accreditedRadioBtn.FindElement(By.XPath("ancestor::tr[1]/descendant::td[3]")).Text;

            AccPage.ClickAndWait(AccPage.AvailableAccreditationTypesContinueBtn);

            // Add a scenario
            Scenario scenario = AccPage.AddScenarioToAccreditation(accreditationBody, accreditationType, amountOfCredits);
            // Convert the scenario to a list of scenarios
            List<Scenario> scenarios = new List<Scenario>() { scenario };

            // Load the accreditations constructor
            Accreditation accreditation = new Accreditation(amountOfCredits, accreditationBody, accreditationType, scenarios);

            Accreditations.Add(accreditation);
            return new Activity(Browser, ProjType, ActName, ActID, FrontMatterText, Location, Accreditations, Assessments);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accreditationBody"></param>
        /// <param name="accreditationType"></param>
        /// <param name="amountOFCredits"></param>
        /// <returns></returns>
        public Scenario AddScenarioToAccreditation(string accreditationBody, string accreditationType, int amountOfCredits)
        {
            if (Assessments.IsNullOrEmpty() || Accreditations[0].Scenarios[0].Awards.IsNullOrEmpty())
            {
                throw new Exception("You already have either an assessment, an award, or both, added to your activity. This automation code is not " +
                    "conditioned for you to add any scenarios AFTER you add assessments or awards. The logic cant handle that. Reorder your calls, " +
                    "meaning add all accreditations and scenarios first, then rerun.");
            }

            Legacy_ActAccreditationPage AccPage = new Legacy_ActAccreditationPage(Browser);
            return AccPage.AddScenarioToAccreditation(accreditationBody, accreditationType, amountOfCredits);
        }

        /// <summary>
        /// Adds an assessment to an activity that was created with <see cref="LMSAdminHelperGeneral.CreateActivity(IWebDriver, Constants_LMSAdmin.ProjectType, string)"/>
        /// After the assessment is created, it will evaluate the completion pathway and return your activity with this assessment and 
        /// also rebuild and return your scenario with the Completion Pathway/Assessment Rules
        /// </summary>
        /// <param name="assType">(Optional) If null, then will default to pre-assessment. <see cref="Constants_LMSAdmin.AssessmentTypes"/></param>
        /// <param name="numberOfQuestionsPerQuestionType">(Optional). The number of questions you want to add for your assessment for each question type. If both this parameter and questionTypes are null, then this will default to 3 questions for the MultipleChoiceSingleAnswer question type</param>
        /// <param name="assTitle">(Optional). You can specify your own title</param>
        /// <param name="questionTypes">(Optional) The question types for your questions. <see cref="Constants_LMSAdmin.QuestionTypes"/></param>
        /// <returns></returns>
        public Activity AddAssessment(Constants.AssessmentTypes assType = Constants.AssessmentTypes.PreTestAssessment,
            int numberOfQuestionsPerQuestionType = 3, string assTitle = null, string assTemplate = null, params Constants.QuestionTypes[] questionTypes)
        {

            List<(string Question, string Answer)> questionsAndAnswers = null;
            if (Accreditations.IsNullOrEmpty())
            {
                throw new Exception("You must add an accreditation scenario before you can add assessments and completion pathway");
            }

            // If tester did not pass a value for question type, then choose MultipleChoiceSingleAnswer
            if (questionTypes.IsNullOrEmpty())
            {
                questionTypes = new Constants.QuestionTypes[] { Constants.QuestionTypes.MultipleChoiceSingleAnswer };
            }

            // Instantiate all page classes that will be needed
            ActMainPage AMP = new ActMainPage(Browser);
            ActAssessmentsPage AssPage = new ActAssessmentsPage(Browser);
            ActAssessmentDetailsPage AssDetailsPage = new ActAssessmentDetailsPage(Browser);

            // Go to the Assessment page if we are not there. Note that If this is the second (or more) assessment you are adding, then the application will
            // be on the Assessment page already. 
            if (!Browser.Exists(Bys.ActAssessmentsPage.AddNewAssessmentLnk, ElementCriteria.IsVisible))
            {
                AMP.ClickAndWaitBasePage(AMP.TreeLinks_Assessments);
            }

            // Go to the Assessment Details page, fill all required fields, and Save
            AssPage.GoToActAssessmentDetailsPage(assTemplate);

            if (assTemplate != null) { AssDetailsPage.ClickAndWait(AssDetailsPage.DetailsTabSaveBtn); }
            else
            {
                AssDetailsPage.FillAndSaveDetailsTab(assType, assTitle);

                // Add all questions
                questionsAndAnswers = AssDetailsPage.Addquestions(numberOfQuestionsPerQuestionType, questionTypes);                
            }
            
            AssDetailsPage.ClickAndWait(AssDetailsPage.BackToAssessmentsLnk);
            // Go to the Completion Pathway tab, choose each scenario, check the following checkboxes: "Display for this scenario" 
            // "Completion Required" "# of attempts allowed" and "# of graded questions needed to pass". ToDo: Convert these 
            // to parammeters so the tester can specify if he wants to check these checkboxes or not.
            // Also get the Completion Pathway info returned to us. Then add assessmentRules scenarios per accreditation
            // in our activity up until this point. These scenarios were originally built with the AddAccreditation method, and
            // will now be updated to include Completion Pathway (Assessment rules).
            foreach (var accreditation in Accreditations)
            {
                // For each scenario
                for (int i = 0; i < accreditation.Scenarios.Count; i++)
                {
                    string scenarioName = accreditation.Scenarios[i].ScenarioName;

                    // Get the list of assessment rules associated to this scenario
                    List<AssessmentRule> assessmentRules = AssPage.EditAndGetCompletionPathways(scenarioName, numberOfQuestionsPerQuestionType);

                    // Background Note: Now that we are adding assessments to our activity, we have to account for Completion Pathway 
                    // (Assessment Rules). The above line of code retreives those rules, and the below line inserts these rules 
                    // into their respective scenario (Scenario object) that was already built with the AddAccreditation method. 
                    // We will just now update that object to include the assessment rules

                    // This says find the scenario with the specified scenarioName, then insert assesmentRules into the AssesmentRules property
                    accreditation.Scenarios.FirstOrDefault(x => x.ScenarioName == scenarioName).AssessmentRules = assessmentRules;
                }
            }

            // Load the assessment type and questions into the Assessment object
            Assessment assessment = new Assessment(assType.GetDescription(), (questionsAndAnswers));

            Assessments.Add(assessment);
            return new Activity(Browser, ProjType, ActName, ActID, FrontMatterText, Location, Accreditations, Assessments);
        }

        /// <summary>
        /// Add Front Matter to an activity that was created with <see cref="LMSAdminHelperGeneral.CreateActivity(IWebDriver, Constants_LMSAdmin.ProjectType, string)"/>
        /// </summary>
        /// <param name="frontMatterText">(Optional) If null, then will generate random text for you</param>
        /// <returns></returns>
        public Activity AddFrontMatter(string frontMatterText = null)
        {
            // Must pass all preconditions before we can add Front Matter
            if (Accreditations.IsNullOrEmpty())
            {
                throw new Exception("You must add an accreditation scenario before you can add Front Matter");
            }

            if (!FrontMatterText.IsNullOrEmpty())
            {
                throw new Exception("You already added Front Matter :) However, we can always add more code for multiple " +
                    "Front Matters. See an automation person to do that");
            }

            // Populate the frontMatterText property
            string timeStamp = string.Format("{0}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            frontMatterText = string.IsNullOrEmpty(frontMatterText) ? string.Format("Frontmatter {0}", timeStamp) : frontMatterText;

            // Instantiate all page classes that will be needed, also go to the Front Matter page
            ActMainPage AMP = new ActMainPage(Browser);
            ActFrontMatterPage FMP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_FrontMatter);

            // Go to the Assessment Details page, fill all required fields, and Save
            FMP.AddFrontMatter(frontMatterText);

            return new Activity(Browser, ProjType, ActName, ActID, frontMatterText, Location, Accreditations, Assessments);
        }

        /// <summary>
        /// Adds Awards to an activity that was created with <see cref="LMSAdminHelperGeneral.CreateActivity(IWebDriver, Constants_LMSAdmin.ProjectType, string)"/>
        /// </summary>
        /// <param name="awardType"></param>
        /// <param name="awardName"></param>
        /// <param name="emailTemplate"></param>
        /// <param name="templateLibrary"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public Activity AddAward(string awardType = null, string awardName = null, string emailTemplate = null, string
            templateLibrary = null)
        {
            // Must pass all preconditions before we can add Awards
            if (Accreditations.IsNullOrEmpty())
            {
                throw new Exception("You must add an accreditation scenario before you can add Awards");
            }

            // Instantiate all page classes that will be needed, also go to the Awards page
            ActMainPage AMP = new ActMainPage(Browser);
            Legacy_ActAwardsPage AP = AMP.ClickAndWaitBasePage(AMP.TreeLinks_Awards);

            // Add the award, assign it to all scenarios, return the award object. 
            Award award = AP.AddAward(awardType, awardName, emailTemplate, templateLibrary);

            // In the next lines of code all the way to the end of the foreach loop, we will now put that list into each Scenario object:

            // We put awards into each Scenario object by adding to the scenarios (per accreditation) in our activity up until this point. 
            // These scenarios were originally built with the AddAccreditation method, and will now be updated to include awards
            foreach (var accreditation in Accreditations)
            {
                // For each scenario
                for (int i = 0; i < accreditation.Scenarios.Count; i++)
                {

                    string scenarioName = accreditation.Scenarios[i].ScenarioName;

                    // Background Note: Now that we are adding awards to our activity, we have to account for them in the Scenario object.
                    // The AddAward method above retrieves that award object, and the below line inserts or adds the award object into all 
                    // scenarios (Scenario object) that was already built with the AddAccreditation method. We will just now update that 
                    // Scenario object to include the award


                    // This says find the scenario with the specified scenarioName, then add the new award created above into the 
                    // Awards property. 
                    accreditation.Scenarios.FirstOrDefault(x => x.ScenarioName == scenarioName).Awards.Add(award);
                }
            }

            return new Activity(Browser, ProjType, ActName, ActID, FrontMatterText, Location, Accreditations, Assessments);
        }

        /// <summary>
        /// Adds a location to an activity that was created with <see cref="LMSAdminHelperGeneral.CreateActivity(IWebDriver, Constants_LMSAdmin.ProjectType, string)"/>
        /// </summary>
        /// <param name="locationName">(Optional). Will generate a random string if you dont provide one</param>
        /// <param name="addLine1">(Optional). Will use our office building address if you dont provide one</param>
        /// <param name="city">(Optional). Will use our office building address if you dont provide one</param>
        /// <param name="state">(Optional). Will use our office building address if you dont provide one</param>
        /// <param name="country">(Optional). Will use our office building address if you dont provide one</param>
        /// <param name="zipcode">(Optional). Will use our office building address if you dont provide one</param>
        /// <returns></returns>
        public Activity AddLocationToLiveActivity(string locationName = null, string addLine1 = "285 E Waterfront Dr #100", string city = "Homestead",
            string state = "Pennsylvania", string country = "United States",  string zipcode = "15120")
        {
            if (!Location.State.IsNullOrEmpty())
            {
                throw new Exception("You already added a location. You can not add 2");
            }

            if (ProjType != "Stand-alone Live Meeting")
            {
                throw new Exception("You cant add a lcoation to a non-live activity");
            }

            // Instantiate all page classes that will be needed, also go to the Awards page
            ActMainPage AMP = new ActMainPage(Browser);

            // Go to the Time/Location tab of the Activity main page
            Location location = AMP.AddLocation(locationName, addLine1, city, state, country, zipcode);

            return new Activity(Browser, ProjType, ActName, ActID, FrontMatterText, location, Accreditations, Assessments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Activity Save()
        {
            // Go to the main activity page and save
            ActMainPage Page = new ActMainPage(Browser);
            Page.ClickAndWaitBasePage(Page.TreeLinks_MainActivity);
            Page.ClickAndWait(Page.DetailsTabSavebtn);

            return new Activity(Browser, ProjType, ActName, ActID, FrontMatterText, Location, Accreditations, Assessments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Activity Publish()
        {
            if (Accreditations.Count > 0)
            {
                ActMainPage Page = new ActMainPage(Browser);
                Page.ClickAndWaitBasePage(Page.TreeLinks_MainActivity);
                Page.PublishActivity();

                return new Activity(Browser, ProjType, ActName, ActID, FrontMatterText, Location, Accreditations, Assessments);
            }
            else
            {
                throw new Exception("You must add an accreditation before the activity can be published");
            }
        }

        /// <summary>
        /// Adds a catalog to your activity. Specifically this clicks on the activity name in the left side tree, checks to make sure the activity
        /// is published, then if it is, then it goes to the Publishing Details tab. If the catalog is not added to the activity, then this searches
        /// for the catalog in the available table, clicks on the plus icon of this catalog, and then waits for catalog to be added to the Selected
        /// Catalogs table
        /// </summary>
        /// <param name="siteCode">The code of the portal/site. <see cref="Constants_LMSAdmin.SiteCodes"/></param>
        /// <param name="catalogName">(Optional). If null, it will choose the catalog with the most activities from siteCode</param>
        /// <returns></returns>
        public Activity AddCatalog(Constants.SiteCodes siteCode, string catalogName = null)
        {
            ActMainPage Page = new ActMainPage(Browser);
            Page.ClickAndWaitBasePage(Page.TreeLinks_MainActivity);

            if (Page.DetailsTabStageSelElem.SelectedOption.Text == "Published")
            {
                catalogName = string.IsNullOrEmpty(catalogName) ? DbUtils_LMSAdmin.GetCatalogForSite(siteCode.GetDescription()) : catalogName;

                Page.AddCatalogToActivity(catalogName);
                return new Activity(Browser, ProjType, ActName, ActID, FrontMatterText, Location, Accreditations, Assessments);
            }
            else
            {
                throw new Exception("The activity must be published before you can add a catalog");
            }
        }


        #endregion methods

    }

    public class Accreditation
    {
        public string AccreditingBody { get; set; }
        public string AccreditationType { get; set; }
        public List<Scenario> Scenarios { get; set; }
        public int AmountOfCredits { get; set; }


        public Accreditation(int amountOfCredits, string accreditingBody, string accreditationType, List<Scenario> scenarios)
        {
            AmountOfCredits = amountOfCredits;
            AccreditingBody = accreditingBody;
            AccreditationType = accreditationType;
            Scenarios = scenarios;
        }
    }

    public class Scenario
    {
        public string ScenarioName { get; set; }
        public List<AssessmentRule> AssessmentRules { get; set; }
        public List<Award> Awards { get; set; }


        public Scenario(string scenarioName, List<AssessmentRule> assessmentRules, List<Award> awards)
        {
            ScenarioName = scenarioName;
            AssessmentRules = assessmentRules;
            Awards = awards;
        }
    }

    public class Assessment
    {
        string AssType { get; set; }
        public List<(string Question, string Answer)> QuestionsAndRightAnswers { get; set; }


        public Assessment(string assType, List<(string Question, string Answer)> questionsAndRightAnswers)
        {
            AssType = assType;
            QuestionsAndRightAnswers = questionsAndRightAnswers;
        }
    }

    public class AssessmentRule
    {
        public int AssessmentOrderNumber { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public bool DisplayForThisScenario { get; set; }
        public bool CompletionRequired { get; set; }
        public int NumOfGradedQuestions { get; set; }
        public int NumOfAttemptsAllowed { get; set; }

        public int NumOfQuestionsNeededToPass { get; set; }
        public string Delivery { get; set; }


        public AssessmentRule(int assessmentOrderNumber, string assessmentName, string assessmentType, bool displayForThisScenario,
            bool completionRequired, int numOFAttemptsAllowed, int numOfGradedQuestions, int numOfQuestionsNeededToPass, string delivery)
        {
            AssessmentOrderNumber = assessmentOrderNumber;
            AssessmentName = assessmentName;
            AssessmentType = assessmentType;
            DisplayForThisScenario = displayForThisScenario;
            CompletionRequired = completionRequired;
            NumOfAttemptsAllowed = numOFAttemptsAllowed;
            NumOfGradedQuestions = numOfGradedQuestions;
            NumOfQuestionsNeededToPass = numOfQuestionsNeededToPass;
            Delivery = delivery;
        }
    }

    public class Award
    {
        public string AwardType { get; set; }
        public string AwardName { get; set; }
        public string TemplateLibrary { get; set; }
        public string Scenario_AccreditationBody { get; set; }

        public Award(string awardType, string awardName, string templateLibrary, string scenario_AccreditationBody)
        {
            AwardType = awardType;
            AwardName = awardName;
            TemplateLibrary = templateLibrary;
            Scenario_AccreditationBody = scenario_AccreditationBody;

        }
    }

    public class Location
    {
        public string FrontMatterText { get; set; }
        public string LocatioName { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public Location()
        {

        }

        public Location(string locatioName, string addressLine1, string city, string country, string state, string postalCode)
        {
            LocatioName = locatioName;
            AddressLine1 = addressLine1;
            City = city;
            Country = country;
            State = state;
            PostalCode = postalCode;

        }
    }




}

