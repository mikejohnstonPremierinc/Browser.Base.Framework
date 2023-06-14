using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using LOG4NET = log4net.ILog;
using NUnit.Framework;

namespace LMSAdmin.AppFramework
{
    public class ActCompletionPathwayPage : Page, IDisposable
    {
        #region constructors
        public ActCompletionPathwayPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return ""; } }

        #endregion properties

        #region elements

        public IWebElement ScenarioSettingsTab { get { return this.FindElement(Bys.ActCompletionPathwayPage.ScenarioSettingsTab); } }
        public IWebElement DeliverySettingsTab { get { return this.FindElement(Bys.ActCompletionPathwayPage.DeliverySettingsTab); } }
        public IWebElement ScenarioSettingsAssessmentsTbl { get { return this.FindElement(Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTbl); } }
        public IWebElement DeliverySettingsAssessmentsTbl { get { return this.FindElement(Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTbl); } }
        public IWebElement ScenarioSettingsSaveBtn { get { return this.FindElement(Bys.ActCompletionPathwayPage.ScenarioSettingsSaveBtn); } }
        public IWebElement DeliverySettingsSaveBtn { get { return this.FindElement(Bys.ActCompletionPathwayPage.DeliverySettingsSaveBtn); } }
        public IWebElement OrderOfAssessmentSelElemDropdownBtn { get { return this.FindElement(Bys.ActCompletionPathwayPage.OrderOfAssessmentSelElemDropdownBtn); } }

        public IWebElement AssessmentDisplayChkbox { get { return this.FindElement(Bys.ActCompletionPathwayPage.AssessmentDisplayChkbox); } }
        public IWebElement AsessmentDisplayAllBtn { get { return this.FindElement(Bys.ActCompletionPathwayPage.AsessmentDisplayAllBtn); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActCompletionPathwayPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(180));
        }
        /// <summary>
        /// Highlighting the element by outlining it with Yellow backgroud and border it with red
        /// Useful while debug or error screenshots if this method is used
        /// </summary>
        /// <param name="element"></param>
        public void highLighterMethod( IWebElement element)
        {
            ElemSet.ScrollToElement(Browser, element, false);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Browser;
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", element);
        }

            /// <summary>
            /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
            /// </summary>
            /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {

            if (Browser.Exists(Bys.ActCompletionPathwayPage.ScenarioSettingsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ScenarioSettingsTab.GetAttribute("outerHTML"))
                {
                    ScenarioSettingsTab.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActCompletionPathwayPage.ScenarioSettingsTitleLbl, ElementCriteria.IsVisible);
                    this.WaitUntil(TimeSpan.FromSeconds(320),Criteria.ActCompletionPathwayPage.LoadIconNotExists);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActCompletionPathwayPage.DeliverySettingsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DeliverySettingsTab.GetAttribute("outerHTML"))
                {
                    DeliverySettingsTab.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActCompletionPathwayPage.DeliverySettingsTitleLbl, ElementCriteria.IsVisible);
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActCompletionPathwayPage.LoadIconNotExists);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActCompletionPathwayPage.ScenarioSettingsSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ScenarioSettingsSaveBtn.GetAttribute("outerHTML"))
                {                    
                    ScenarioSettingsSaveBtn.ClickJS(Browser);                 
                    Browser.WaitForElement(Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTbl, ElementCriteria.IsEnabled);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActCompletionPathwayPage.DeliverySettingsSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DeliverySettingsSaveBtn.GetAttribute("outerHTML"))
                {
                    DeliverySettingsSaveBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTbl, ElementCriteria.IsEnabled);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
                    return null;
                }
            }


            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                buttonOrLinkElem.GetAttribute("innerText")));
        }

       
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActCompletionPathwayPage", activeRequests.Count, ex); }
        }



        #endregion methods: repeated per page

        #region methods: page specific
        /// <summary>
        /// First, Find the specific Scenario section by using given scenario name and accreditation body( it is required because same scenario
        /// can be present in other accreditation bodies also)
        /// Then, Search for the given Assessment Row under the scenario section 
        /// Returns the Assessment Row if its present   
        /// </summary>
        /// <param name="scenarioName"> Exact Name of the Scenario under which going to search the assessment </param>
        /// <param name="accreditationBody">Exact Name of the Accreditation Body under which the Scenario is present </param>
        /// <param name="assessmentName">Assessment Title which needs to be checked </param>
        /// <param name="assessmentType">Type of the Assessment like "Pre-Test Assessment"</param>
        /// <returns>Assessment Row</returns>
        public IWebElement GetAssessmentRowPresentInScenario(IWebElement SettingsAssessmentsTbl, By SettingsAssessmentsTblBodyRow, string scenarioName, string accreditationBody, string assessmentName, string assessmentType )
        {
            IWebElement parentRow = ElemGet.Grid_GetRowByRowNameAndAdditionalCellName(Browser, SettingsAssessmentsTbl, SettingsAssessmentsTblBodyRow,
                scenarioName, "td", accreditationBody, "td");            
            
            string parentRowNum = parentRow.GetAttribute("data-idx");

            string childRowsXpath = string.Format(".//following-sibling::tr[contains(@class, 'child')][starts-with(@data-idx,'{0}')]", parentRowNum);

            IList<IWebElement> childElementRows = parentRow.FindElements(By.XPath(childRowsXpath));

            string assessmentCellxpath = string.Format(".//td[contains(., '{0}') and contains(., '{1}')]", assessmentName,assessmentType);
            foreach (IWebElement childRow in childElementRows)
            {
                if (childRow.FindElements(By.XPath(assessmentCellxpath)).Count==1)
                {
                    highLighterMethod(childRow); // used this method since the page shows more rows, highlighting the row is catchy for user
                    return childRow;
                }                
            }
            throw new Exception(String.Format("Row of given assessment [ {0} ]  could not be found " +
                "under the specified Scenario [ {1} ] section in the Settings table ",assessmentName,scenarioName));     
        }

        /// <summary>
        /// Fills the Scenario settings field values for given assessment to the specified scenario and Saves it 
        /// Using <see cref="GetAssessmentRowPresentInScenario"/> gets the specific assessment row, and fill all the fields
        /// </summary>
        /// /// <param name="scenarioSettingsTbl"> Scenario Settings Table element in the page </param>
        /// <param name="scenarioSettingsAssessmentsTblBodyRow"> Send a generic row 'tableId/tr' of Scenario settings Table  </param>
        /// <param name="scenarioName">Exact Name of the Scenario under which going to search the assessment</param>
        /// <param name="accreditationBody">Exact Name of the Accreditation Body under which the Scenario is present</param>
        /// <param name="assessmentName">Assessment Title which needs to be checked</param>
        /// <param name="assessmentType">Type of the Assessment like "Pre-Test Assessment"</param>
        /// " get the specific assessment row" <see cref="GetAssessmentRowPresentInScenario"></see>
        /// <param name="numOfAttempts"> Number of Attempts,  User can do the assessment </param>
        /// <param name="numOfGradedQuesToPass">Number of Questions Which is required for user to pass the assessment</param>
        /// <param name="assessmentTobeDisplayed">Availability of assessment for the scenario: by default,set to be checked for display ; Send "false" if user dont want to display the assessment </param>
        /// <param name="orderOftheAssessment">Send Order Number for which assessment to appear </param>
        /// <param name="completionRequired">by default,set to be "Yes" for completionRequired ; Send "No" if user dont need the assessment to be completed</param>
        public void FillAndSaveAsessmentScenarioSettings(string scenarioName, string accreditationBody, string assessmentName, string assessmentType,
               string numOfAttempts = "0", int numOfGradedQuesToPass = 0, bool assessmentTobeDisplayed = true, string orderOftheAssessment = null, string completionRequired = "Yes")
        {
            // Find the given assessment row under specified scenario section
            IWebElement assessmentRow = GetAssessmentRowPresentInScenario(ScenarioSettingsAssessmentsTbl,Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, assessmentName, assessmentType);
            
            //if scenario section is collapsed , then expand it
            if (!assessmentRow.Displayed) { ElemSet_LMSAdmin.Grid_ExpandOrCollapse(assessmentRow, "expand", "button"); }

            if (assessmentTobeDisplayed)
            {
                ElemSet_LMSAdmin.Grid_TickCheckBox(assessmentRow, 1);

                //   If assessmentTobeDisplayed == checked , then only orderOftheAssessment element will be enabled 
                if (orderOftheAssessment.IsNullOrEmpty())
                {
                    ElemSet.DropdownSingle_Fireball_SelectByIndex(Browser, assessmentRow.FindElement(Bys.ActCompletionPathwayPage.OrderOfAssessmentSelElemDropdownBtn), 1);
                }
                else
                {                    
                    ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, assessmentRow.FindElement(Bys.ActCompletionPathwayPage.OrderOfAssessmentSelElemDropdownBtn), orderOftheAssessment);
                }
            }
                        
           // int indexCompletionRequired = String.Equals(completionRequired, "Yes", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
            IWebElement CompletionRequiredSelElemDropdown = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.CompletionRequiredSelElemDropdownBtn);
            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, CompletionRequiredSelElemDropdown, completionRequired);
          
            
            //If number of GradedQuestions to pass the assessment is given then fill the value
            if (numOfGradedQuesToPass != 0)
            {
                IWebElement NumOfGradedQuesToPassSelElemDropdown = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.NumOfGradedQuesToPassSelElemDropdownBtn);
                ElemSet.DropdownSingle_Fireball_SelectByText(Browser, NumOfGradedQuesToPassSelElemDropdown, numOfGradedQuesToPass.ToString());
            }

            //If number of Attempts allowed to take the assessment is given then fill the value
            if (numOfAttempts != "0")
            {
                IWebElement NumOfAttemptsAllowedTxt = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.NumOfAttemptsAllowedTxt);
                ElemSet.TextBox_EnterText(Browser, NumOfAttemptsAllowedTxt, true,numOfAttempts);                    
            }

            ClickAndWait(ScenarioSettingsSaveBtn);            
        }

        /// <summary>
        /// Verify the Saved Assessment Settings Field values for Scenario are displayed in UI
        /// The Fields are filled and saved using <see cref="FillAndSaveAsessmentScenarioSettings"></see>
        /// </summary>
        /// <param name="scenarioName">Exact Name of the Scenario under which going to search the assessment</param>
        /// <param name="accreditationBody">Exact Name of the Accreditation Body under which the Scenario is present</param>
        /// <param name="assessmentName">Assessment Title which needs to be checked</param>
        /// <param name="assessmentType">Type of the Assessment like "Pre-Test Assessment"</param>
        /// " get the specific assessment row" <see cref="GetAssessmentRowPresentInScenario"></see>
        /// <param name="numOfAttempts"> Number of Attempts,  User can do the assessment </param>
        /// <param name="numOfGradedQuesToPass">Number of Questions Which is required for user to pass the assessment</param>
        /// <param name="assessmentTobeDisplayed">Availability of assessment for the scenario ( true or false ) </param>
        /// <param name="orderOftheAssessment">Order Number for which assessment to appear </param>
        /// <param name="completionRequired"> completion of assessment required : either Yes or No</param>
        public void VerifyTheSavedAsessmentScenarioSettings(string scenarioName, string accreditationBody, string assessmentName, string assessmentType,
               string numOfAttempts = "", int numOfGradedQuesToPass = 0, bool assessmentTobeDisplayed = false, string orderOftheAssessment = "", 
               string completionRequired = "No")
        {
            // Find the given assessment row under specified scenario section
            IWebElement assessmentRow = GetAssessmentRowPresentInScenario(ScenarioSettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.ScenarioSettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, assessmentName, assessmentType);

            //Verify Display field
            string strAssessmentTobeDisplayed = assessmentTobeDisplayed ? "true" : null;
            IWebElement assessTobeDisplayedElement = ElemGet_LMSAdmin.Grid_GetButtonOrLinkWithoutTextWithinRow(assessmentRow, "input[@type='checkbox']");            
            StringAssert.AreEqualIgnoringCase(strAssessmentTobeDisplayed,assessTobeDisplayedElement.GetAttribute("checked"), String.Format("[ {0} ] - Verifying Display field check ",assessmentName));

            //Verify Order field
            SelectElement OrderOfAssessmentSelElemDropdown = new SelectElement(assessmentRow.FindElement(Bys.ActCompletionPathwayPage.OrderOfAssessmentSelElemDropdown));
            StringAssert.AreEqualIgnoringCase(orderOftheAssessment,OrderOfAssessmentSelElemDropdown.SelectedOption.Text, String.Format("[ {0} ] - Verifying orderOftheAssessment field", assessmentName));

            //Verify completionRequired field
            IWebElement CompletionRequiredSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.CompletionRequiredSelElemDropdownBtn);
            StringAssert.AreEqualIgnoringCase(completionRequired, CompletionRequiredSelElemDropdownBtn.Text.Trim(), String.Format("[ {0} ] - Verifying completionRequired field", assessmentName));

            //Verify numOfGradedQuesToPass field
            IWebElement NumOfGradedQuesToPassSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.NumOfGradedQuesToPassSelElemDropdownBtn);
            StringAssert.AreEqualIgnoringCase(numOfGradedQuesToPass.ToString(), NumOfGradedQuesToPassSelElemDropdownBtn.Text.Trim(), String.Format("[ {0} ] - Verifying numOfGradedQuesToPass field", assessmentName));

            //Verify NumOfAttemptsAllowed field
            IWebElement NumOfAttemptsAllowedTxt = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.NumOfAttemptsAllowedTxt);
            StringAssert.AreEqualIgnoringCase(numOfAttempts, NumOfAttemptsAllowedTxt.GetAttribute("value"), String.Format("[ {0} ] - Verifying numOfAttempts field", assessmentName));
            
        }

        /// <summary>
        /// Fills the Fields of given assessment to the specified scenario and Save the Delivery settings
        /// Using <see cref="GetAssessmentRowPresentInScenario"/> gets the specific assessment row, and fill all the fields
        /// </summary>
        /// <param name="deliverySettingsTbl"> Delivery Settings Table element in the page </param>
        /// <param name="deliverySettingsAssessmentsTblBodyRow"> Send a generic row 'tableId/tr' of Delivery settings Table  </param>
        /// <param name="scenarioName">Exact Name of the Scenario under which going to search the assessment</param>
        /// <param name="accreditationBody">Exact Name of the Accreditation Body under which the Scenario is present</param>
        /// <param name="assessmentName"> Title of the Assessment which needs to be configured</param>
        /// <param name="assessmentType"> Type of the Assessment like "Pre-Test Assessment"</param>
        /// " get the specific assessment row" <see cref="GetAssessmentRowPresentInScenario"></see>
        /// <param name="actionType">action trigger type on which the assessment will be available; ex : participant registration only  </param>
        /// <param name="timingType"> Send Timing of the assessment availability as "Immediate" or "After"  </param>
        /// <param name="conditionType"> Send assessment status condition as "Passed" or "Failed" or "Pass or Fail" </param>
        /// <param name="numberoftimelimit"> Send the number of units which has to be delayed the assessment </param>
        /// <param name="units">Send the timing delayed type, ex: "Days" </param>
        public void FillAndSaveAsessmentDeliverySettings(string scenarioName, string accreditationBody, string assessmentName, string assessmentType,
               string actionType,  string timingType , string conditionType = null, string numberoftimelimit =null, 
               string units =null, string notificationTemplate =null)
        {
            // Find the given assessment row under specified scenario section
            IWebElement assessmentRow = GetAssessmentRowPresentInScenario(DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, assessmentName, assessmentType);
           
            //if scenario section is collapsed , then expand it
            if (!assessmentRow.Displayed) { ElemSet_LMSAdmin.Grid_ExpandOrCollapse(assessmentRow, "expand", "button"); }

            IWebElement ActionTriggerSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.ActionTriggerSelElemDropdownBtn);
            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, ActionTriggerSelElemDropdownBtn, actionType);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));

            if (!conditionType.IsNullOrEmpty())
            {
                IWebElement ConditionTypeSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.ConditionTypeSelElemDropdownBtn);
                ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, ConditionTypeSelElemDropdownBtn, conditionType);                
            }              

            IWebElement TimingTypeSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.TimingTypeSelElemDropdownBtn);
            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, TimingTypeSelElemDropdownBtn, timingType);

            if (!numberoftimelimit.IsNullOrEmpty())
            {
                IWebElement NumberofUnitsTxt = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.NumberofUnitsTxt);
                ElemSet.TextBox_EnterText(Browser, NumberofUnitsTxt, true, numberoftimelimit);
            }

            if (!units.IsNullOrEmpty())
            {
                IWebElement UnitsTypeSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.UnitsTypeSelElemDropdownBtn);
                ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, UnitsTypeSelElemDropdownBtn, units);
            }

            if (!notificationTemplate.IsNullOrEmpty())
            {
                IWebElement AssessmentNotificationSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.AssessmentNotificationSelElemDropdownBtn);
                ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, AssessmentNotificationSelElemDropdownBtn, notificationTemplate);
            }

            DeliverySettingsSaveBtn.Click();
            Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTbl, ElementCriteria.IsEnabled);
            this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.ActCompletionPathwayPage.LoadIconNotExists);
        }

        /// <summary>
        /// Verify the Saved Assessment's Delivery Settings Field values for specified scenario are displayed in UI
        /// The Fields are filled and saved using <see cref="FillAndSaveAsessmentDeliverySettings"> 
        /// </summary>
        /// <param name="deliverySettingsTbl"> Delivery Settings Table element in the page </param>
        /// <param name="deliverySettingsAssessmentsTblBodyRow"> Send a generic row 'tableId/tr' of Delivery settings Table  </param>
        /// <param name="scenarioName">Exact Name of the Scenario under which going to search the assessment</param>
        /// <param name="accreditationBody">Exact Name of the Accreditation Body under which the Scenario is present</param>
        /// <param name="assessmentName"> Title of the Assessment which needs to be configured</param>
        /// <param name="assessmentType"> Type of the Assessment like "Pre-Test Assessment"</param>
        /// " get the specific assessment row" <see cref="GetAssessmentRowPresentInScenario"></see>
        /// <param name="actionType">action trigger type on which the assessment will be available; ex : participant registration only  </param>
        /// <param name="timingType"> Timing of the assessment availability as "Immediate" or "After"  </param>
        /// <param name="conditionType">  assessment status condition as "Passed" or "Failed" or "Pass or Fail" </param>
        /// <param name="numberoftimelimit">  number of units which has to be delayed the assessment </param>
        /// <param name="units"> timing delayed type, ex: "Days" </param>
        public void VerifyTheSavedAsessmentDeliverySettings(string scenarioName, string accreditationBody, string assessmentName, string assessmentType,
             string actionType, string conditionType = null, string timingType = null, string numberoftimelimit = "",
               string units = null, string notificationTemplate = null)

        {
            // Find the given assessment row under specified scenario section
            IWebElement assessmentRow = GetAssessmentRowPresentInScenario(DeliverySettingsAssessmentsTbl, Bys.ActCompletionPathwayPage.DeliverySettingsAssessmentsTblBodyRow, scenarioName, accreditationBody, assessmentName, assessmentType);
            ElemSet_LMSAdmin.highLighterMethod(Browser, assessmentRow);

            //Verifying Action field
            IWebElement ActionTriggerSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.ActionTriggerSelElemDropdownBtn);
            StringAssert.AreEqualIgnoringCase(actionType, ActionTriggerSelElemDropdownBtn.Text.Trim(), String.Format("[ {0} ] - Verifying Action field ", assessmentName));

            //Verifying Condition field
            conditionType = conditionType.IsNullOrEmpty() ? "Select" : conditionType;
            IWebElement ConditionTypeSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.ConditionTypeSelElemDropdownBtn);
                StringAssert.AreEqualIgnoringCase(conditionType, ConditionTypeSelElemDropdownBtn.Text.Trim(), String.Format("[ {0} ] - Verifying Condition field ", assessmentName));

            //Verifying Timing field
            timingType = timingType.IsNullOrEmpty() ? "Select" : timingType;
            IWebElement TimingTypeSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.TimingTypeSelElemDropdownBtn);
            StringAssert.AreEqualIgnoringCase(timingType, TimingTypeSelElemDropdownBtn.Text.Trim(), String.Format("[ {0} ] - Verifying Timing field ", assessmentName));

            //Verifying Number field           
            IWebElement NumberofUnitsTxt = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.NumberofUnitsTxt);
            StringAssert.AreEqualIgnoringCase(numberoftimelimit, NumberofUnitsTxt.GetAttribute("value"), String.Format("[ {0} ] - Verifying Number field ", assessmentName));

            //Verifying Units field
            units = units.IsNullOrEmpty() ? "Select" : units;
            IWebElement UnitsTypeSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.UnitsTypeSelElemDropdownBtn);
            StringAssert.AreEqualIgnoringCase(units, UnitsTypeSelElemDropdownBtn.Text.Trim(), String.Format("[ {0} ] - Verifying Units field ", assessmentName));

            //Verifying Assessment Notification field
                       notificationTemplate = notificationTemplate.IsNullOrEmpty() ? "Select" : notificationTemplate;            
            IWebElement AssessmentNotificationSelElemDropdownBtn = assessmentRow.FindElement(Bys.ActCompletionPathwayPage.AssessmentNotificationSelElemDropdownBtn);
            StringAssert.AreEqualIgnoringCase(notificationTemplate, AssessmentNotificationSelElemDropdownBtn.Text.Trim(), String.Format("[ {0} ] - Verifying Assessment Notification field ", assessmentName));
            
        }


        #endregion methods: page specific


    }


}