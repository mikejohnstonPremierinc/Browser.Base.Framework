 using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using LMSAdmin.AppFramework.HelperMethods;
using System.Globalization;

namespace LMSAdmin.AppFramework
{
    public class ActAccreditationPage : Page, IDisposable
    {
        #region constructors
        public ActAccreditationPage(IWebDriver driver) : base(driver)
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

        public IWebElement AddAccreditationBtn { get { return this.FindElement(Bys.ActAccreditationPage.AddAccreditationBtn); } }
        public IWebElement AddScenarioBtn { get { return this.FindElement(Bys.ActAccreditationPage.AddScenarioBtn); } }
        public IWebElement AnyDialogCommonCloseBtn { get { return this.FindElement(Bys.ActAccreditationPage.AnyDialogCommonCloseBtn); } }
        public IWebElement AccreditationBodyandTypeSelElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.AccreditationBodyandTypeSelElemBtn); } }
        public SelectElement AccreditationBodyandTypeSelElemOptionsDropdown { get { return new SelectElement(this.FindElement(Bys.ActAccreditationPage.AccreditationBodyandTypeSelElemOptionsDropdown)); } }
        public IWebElement PrimaryProviderSelElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.PrimaryProviderSelElemBtn); } }
        public SelectElement PrimaryProviderSelElemOptionsDropdown { get { return new SelectElement(this.FindElement(Bys.ActAccreditationPage.PrimaryProviderSelElemOptionsDropdown)); } }
        public IWebElement AdditionalProviderSelElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.AdditionalProviderSelElemBtn); } }
        public IWebElement AddAccreditationFormAddAccreditationBtn { get { return this.FindElement(Bys.ActAccreditationPage.AddAccreditationFormAddAccreditationBtn); } }
        public IWebElement AccreditationDetailsTbl { get { return this.FindElement(Bys.ActAccreditationPage.AccreditationDetailsTbl); } }        
        public IWebElement ScenarionameTxt { get { return this.FindElement(Bys.ActAccreditationPage.ScenarionameTxt); } }
        public IWebElement CodeNumberTxt { get { return this.FindElement(Bys.ActAccreditationPage.CodeNumberTxt); } }
        public IWebElement ReleaseDateTxt { get { return this.FindElement(Bys.ActAccreditationPage.ReleaseDateTxt); } }
        public IWebElement ExpirationDateTxt { get { return this.FindElement(Bys.ActAccreditationPage.ExpirationDateTxt); } }
        public IWebElement FixedCreditTxt { get { return this.FindElement(Bys.ActAccreditationPage.FixedCreditTxt); } }
        public IWebElement EquivalentCreditTxt { get { return this.FindElement(Bys.ActAccreditationPage.EquivalentCreditTxt); } }
        public IWebElement MaximumCreditsTxt { get { return this.FindElement(Bys.ActAccreditationPage.MaximumCreditsTxt); } }
        public IWebElement MinimumCreditsTxt { get { return this.FindElement(Bys.ActAccreditationPage.MinimumCreditsTxt); } }
        public IWebElement CreditIncrementsTxt { get { return this.FindElement(Bys.ActAccreditationPage.CreditIncrementsTxt); } }
        public IWebElement CustomTextTxt { get { return this.FindElement(Bys.ActAccreditationPage.CustomTextTxt); } }
        public IWebElement EligibleSpecialitiesSelElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.EligibleSpecialitiesSelElemBtn); } }
        public IWebElement EligibleCountriesSelElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.EligibleCountriesSelElemBtn); } }
        public IWebElement EligibleProfessionElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.EligibleProfessionElemBtn); } }
        public SelectElement EligibleProfessionElemOptionsDropdown { get { return new SelectElement(this.FindElement(Bys.ActAccreditationPage.EligibleProfessionElemOptionsDropdown)); } }
        public IWebElement EditAccreditationFormPrimaryProviderSelElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.EditAccreditationFormPrimaryProviderSelElemBtn); } }
        public IWebElement FixedCreditUnitSelElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.FixedCreditUnitSelElemBtn); } }
        public IWebElement EquivalentCreditUnitSelElemBtn { get { return this.FindElement(Bys.ActAccreditationPage.EquivalentCreditUnitSelElemBtn); } }
        public IWebElement AddScenarioFormSaveScenarioBtn { get { return this.FindElement(Bys.ActAccreditationPage.AddScenarioFormSaveScenarioBtn); } }
        public IWebElement ScenarioDetailsTbl { get { return this.FindElement(Bys.ActAccreditationPage.ScenarioDetailsTbl); } }
        public IWebElement ClaimCreditEnabledChkbox { get { return this.FindElement(Bys.ActAccreditationPage.ClaimCreditEnabledChkbox); } }
        public IWebElement AccreditationDeleteFormOkBtn { get { return this.FindElement(Bys.ActAccreditationPage.AccreditationDeleteFormOkBtn); } }        
        public IWebElement ScenarioDeleteFormOkBtn { get { return this.FindElement(Bys.ActAccreditationPage.ScenarioDeleteFormOkBtn); } }       
        public IWebElement EditAccreditationFormSaveAccreditationBtn { get { return this.FindElement(Bys.ActAccreditationPage.EditAccreditationFormSaveAccreditationBtn); } }
        public IWebElement EditScenarioFormSaveScenarioBtn { get { return this.FindElement(Bys.ActAccreditationPage.EditScenarioFormSaveScenarioBtn); } }
        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActAccreditationPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(180));
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {

            if (Browser.Exists(Bys.ActAccreditationPage.AddAccreditationBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddAccreditationBtn.GetAttribute("outerHTML"))
                {   
                    AddAccreditationBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAccreditationPage.AccreditationBodyandTypeSelElemBtn, ElementCriteria.IsVisible);
                    Browser.WaitForElement(Bys.ActAccreditationPage.AddAccreditationFormAddAccreditationLbl, ElementCriteria.IsVisible);
                    return null;
                }
            }
            if (Browser.Exists(Bys.ActAccreditationPage.AddScenarioBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddScenarioBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, AddScenarioBtn, false);
                    AddScenarioBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAccreditationPage.ScenarionameTxt, ElementCriteria.IsVisible);
                    Browser.WaitForElement(Bys.ActAccreditationPage.AddScenarioFormAddScenarioLbl, ElementCriteria.IsVisible);
                    return null;
                }
            }
            if (Browser.Exists(Bys.ActAccreditationPage.AddScenarioFormSaveScenarioBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddScenarioFormSaveScenarioBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, AddScenarioFormSaveScenarioBtn, false);
                    AddScenarioFormSaveScenarioBtn.ClickJS(Browser);                    
                    Browser.WaitForElement(Bys.Page.BackToActivityBtn, ElementCriteria.IsVisible);
                    this.WaitUntil(Criteria.ActAccreditationPage.LoadIconNotExists);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
                    Thread.Sleep(TimeSpan.FromSeconds(15));
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAccreditationPage.AccreditationDeleteFormOkBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AccreditationDeleteFormOkBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, AccreditationDeleteFormOkBtn, false);
                    AccreditationDeleteFormOkBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Page.BackToActivityBtn, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
                    this.WaitUntil(Criteria.ActAccreditationPage.LoadIconNotExists);
                    Thread.Sleep(1000);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAccreditationPage.ScenarioDeleteFormOkBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ScenarioDeleteFormOkBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, ScenarioDeleteFormOkBtn, false);
                    ScenarioDeleteFormOkBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Page.BackToActivityBtn, ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
                    this.WaitUntil(Criteria.ActAccreditationPage.LoadIconNotExists);
                    Thread.Sleep(1000);
                    return null;
                }
            }
           
            if (Browser.Exists(Bys.ActAccreditationPage.AddAccreditationFormAddAccreditationBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddAccreditationFormAddAccreditationBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, AddAccreditationFormAddAccreditationBtn, false);
                    AddAccreditationFormAddAccreditationBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Page.BackToActivityBtn, ElementCriteria.IsVisible);                    
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
                    Thread.Sleep(4000);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAccreditationPage.EditAccreditationFormSaveAccreditationBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == EditAccreditationFormSaveAccreditationBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, EditAccreditationFormSaveAccreditationBtn, false);
                    EditAccreditationFormSaveAccreditationBtn.ClickJS(Browser);
                    this.WaitUntil(Criteria.ActAccreditationPage.LoadIconNotExists);
                    Browser.WaitForElement(Bys.ActAccreditationPage.AccreditationPageTitleLbl, ElementCriteria.IsVisible);
                    this.WaitUntil(Criteria.ActAccreditationPage.PageReady);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAccreditationPage.EditScenarioFormSaveScenarioBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == EditScenarioFormSaveScenarioBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ScrollToElement(Browser, EditScenarioFormSaveScenarioBtn, false);
                    EditScenarioFormSaveScenarioBtn.ClickJS(Browser);
                    this.WaitUntil(Criteria.ActAccreditationPage.LoadIconNotExists);
                    Browser.WaitForElement(Bys.ActAccreditationPage.AccreditationPageTitleLbl, ElementCriteria.IsVisible);
                    this.WaitUntil(Criteria.ActAccreditationPage.PageReady);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAccreditationPage.ClaimCreditEnabledChkbox))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ClaimCreditEnabledChkbox.GetAttribute("outerHTML"))
                {
                    ClaimCreditEnabledChkbox.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAccreditationPage.ClaimCreditEnabledChkbox, ElementCriteria.AttributeValueContains("checked","true"));
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActAccreditationPage", activeRequests.Count, ex); }
        }



        #endregion methods: repeated per page

        #region methods: page specific


        public void AddAllAccreditation()
        {
            ClickAndWait(AddAccreditationBtn);
            int TotalAccreditations = AccreditationBodyandTypeSelElemOptionsDropdown.Options.Count;
            for (int accrdcount= 1; accrdcount < TotalAccreditations; accrdcount++)
            {                
                Browser.WaitForElement(Bys.ActAccreditationPage.AccreditationBodyandTypeSelElemBtn, ElementCriteria.IsEnabled);
                ElemSet.DropdownMulti_Fireball_SelectByIndex(Browser, AccreditationBodyandTypeSelElemBtn, 1);
                Browser.WaitForElement(Bys.ActAccreditationPage.PrimaryProviderSelElemBtn, ElementCriteria.IsEnabled);

                PrimaryProviderSelElemBtn.Click();
                //Browser.FindElement(By.XPath("//div[@class='dropdown-menu open']//div[@aria-label='PRIMARY PROVIDER*]/ul//li[2]")).Click();
                Browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/ul/li[2]/a/span[2]")).Click();
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(240));
                //ElemSet.DropdownMulti_Fireball_SelectByIndex(Browser, PrimaryProviderSelElemBtn, 1);
                ClickAndWait(AddAccreditationFormAddAccreditationBtn);
                RefreshPage(true);
                if (accrdcount + 1 == TotalAccreditations) { }
                else { ClickAndWait(AddAccreditationBtn); Thread.Sleep(TimeSpan.FromSeconds(5)); }
            }           
        }

        /// <summary>
        /// Add accreditation to activity and verifies that added accreditation is displayed
        /// </summary>
        /// <param name="accreditationBody">By default, accreditation body set to "Non-Accredited" value. If you want to add specific accreditation body, then send the exact accreditation body text from the span tag of the select element</param>
        /// <param name="primaryProvider">If parameter is Empty, then it will select the first index provider value . If you want specific provider then send the exact primary provider text from the span tag of the select element</param>
        /// <param name="claimCreditEnabled">By default, claimCreditEnabled set to "false". Send "true" if you want to enable it</param>
        /// <returns></returns>
        public void AddAccreditation(string accreditationBody = "Non-Accredited", string primaryProvider = null, bool claimCreditEnabled = false)
        {
            if (Browser.FindElements(Bys.ActAccreditationPage.AccreditationDetailsTbl).Count > 0)
            {
                if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                {
                    throw new Exception(string.Format(accreditationBody + " is already added to this activity; Delete this accreditation body or " +
                        "  change the different accreditation body input , because once one accreditation added  the same can not be added again " +
                        "to the same activity "));
                }
            }

            ClickAndWait(AddAccreditationBtn);

            // Choose an accreditation type:
            ElemSet.DropdownMulti_Fireball_SelectByText(Browser, AccreditationBodyandTypeSelElemBtn, accreditationBody);
            Browser.WaitForElement(Bys.ActAccreditationPage.PrimaryProviderSelElemBtn, ElementCriteria.IsEnabled);

            // Choose PrimaryProvider:
            // If the PrimaryProvider is empty, then choose a body for the tester depending on different conditions...          
            if (primaryProvider.IsNullOrEmpty())
            {
                PrimaryProviderSelElemBtn.Click();
                Thread.Sleep(500);
                //Browser.FindElement(By.XPath("//div[@class='dropdown-menu open']//div[@aria-label='PRIMARY PROVIDER*]/ul//li[2]")).Click();
                //Browser.FindElement(By.XPath("/html/body/div[3]/div/div[2]/ul/li[2]/a/span[2]")).Click();
                Browser.FindElement(By.XPath("//div[@aria-label='PRIMARY PROVIDER']/ul[@class='dropdown-menu inner ']/li[2]/a")).Click();
            
                               // ElemSet.DropdownMulti_Fireball_SelectByIndex(Browser, PrimaryProviderSelElemBtn, 1);
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(2400));
            }
            else if (primaryProvider.Contains("Empty"))
            {
                // Do not add any primary provider
            }
            // else choose the user-specified provider
            else
            {
                ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, PrimaryProviderSelElemBtn, primaryProvider);
                //ElemSet.DropdownMulti_Fireball_SelectByText(Browser, PrimaryProviderSelElemDropdown, primaryProvider);
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(2400));
                Thread.Sleep(TimeSpan.FromSeconds(15));
            }

            
             //Choose Additional Providers;        
           // if (!PrimaryProviderSelElemBtn.GetAttribute("title").Contains("Select"))
            //{ 
              //  if (PrimaryProviderSelElemOptionsDropdown.Options.Count - 1 > 1)
                //{
                // // Additional Provider Dropdown Options should be visible if there is more than one provider in the list                
                //Browser.WaitForElement(Bys.ActAccreditationPage.AdditionalProviderSelElemBtn, ElementCriteria.IsEnabled);
                 //ElemSet.DropdownMulti_Fireball_SelectRandomItems(Browser, AdditionalProviderSelElemBtn, 1);
               //}
               // else
               // {
                  //  // Additional Provider Dropdown Options should not be visible if there is only one provider in the list                       
                  //  this.WaitUntil(Criteria.ActAccreditationPage.AdditionalProviderSelElemOptionsDropdownNotExists);
               // }
            //}

            if (claimCreditEnabled)
            {
                ClickAndWait(ClaimCreditEnabledChkbox);                
            }

            Thread.Sleep(3000);
            ClickAndWait(AddAccreditationFormAddAccreditationBtn);

            //verify that created accreditation is displayed in UI
            if (!ElemGet_LMSAdmin.Grid_CellTextFound(Browser, AccreditationDetailsTbl, 
                Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn ,accreditationBody))
            {
                throw new Exception(string.Format( "Accreditation [{0}] is not added to the activity", accreditationBody));
            }
           
        }

        /// <summary>
        /// Add Scenario to the accreditation and verifies that scenario added to the given accreditation
        /// </summary>
        /// <param name="accreditationBody">By default, accreditationbody set to "Non-Accredited" value. If you want to add scenario for specific accreditationbody, then send the exact accreditationbody text from the span tag of the select element</param>
        /// <param name="eligibleProfession">By default, EligibleProfession set to "Physician" profession. If you want specific profession for the scenario then send the exact profession Text </param>
        /// <param name="fixedCredits">(Optional) Credits Fixed for Scenario which user can claim on completion; By default, fixedCredits set to "5". Send value if you want to change </param>
        /// <param name="EquivalentCredits">(Optional) Equivalent Credit for Fixed credit </param>
        /// <param name="accrClaimCreditEnabled">(Optional) by default set to "false"; If Credit Claim enabled "true" for Given accreditation then Max,Min,CreditIncr Fields will appear </param>
        /// <param name="MaximumCredits">(Optional) Maximum Credits which user can claim. By default set to "4". Send value if you want to change </param>
        /// <param name="MinimumCredits">(Optional) Minimum Credits which user can claim. By default set to "1". Send value if you want to change </param>
        /// <param name="CreditIncrements">(Optional) IncrementRate By default set to "0.25". Send value if you want to change </param>
        /// <param name="eligibleSpecialities">(Optional) Send eligible speciality if user wants to select. this field will appear only for confirgured activity </param>
        /// <param name="eligibleCountries">(Optional) Send eligible countries if user wants to select. this field will appear only for confirgured activity </param>
        public string AddScenario(string accreditationBody = "Non-Accredited", bool accrClaimCreditEnabled = false, string eligibleProfession = "Physician", double fixedCredits = 5,
            double EquivalentCredits = 5, double MaximumCredits =4, double MinimumCredits = 1, double CreditIncrements = 0.25,
            string eligibleSpecialities = null, string eligibleCountries = null )
        {                    
            // If we are not having the specific Accreditation's body then throw exception
            if (!ElemGet_LMSAdmin.Grid_CellTextFound(Browser, AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
            {
                throw new Exception(string.Format(accreditationBody + " is not added to this activity;  add the accreditation first ," +
                    "then add the scenario to it "));
            }

            // finding the AddScenario Button corresponding to the respective accreditation body type
            IWebElement AddScenarioBtn = Browser.FindElement(By.XPath(string.Format("(//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'chart')][2]//following-sibling::div//span[text()='Add scenario'])[1]",
                accreditationBody)));
            ElemSet.ScrollToElement(Browser, AddScenarioBtn, false);
            AddScenarioBtn.Click();
            Browser.WaitForElement(Bys.ActAccreditationPage.AddScenarioFormAddScenarioLbl, ElementCriteria.IsVisible);
            Browser.WaitForElement(Bys.ActAccreditationPage.ScenarionameTxt, ElementCriteria.IsEnabled);

            // Choose an Scenario:           
            int randomNumber= DataUtils.GetRandomInteger(1000);
            string randomString = DataUtils.GetRandomString(2);
            string currentdate = string.Format("{0}", DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            string scenarioName = string.Format("AutoGen_Scenario_{0}_{1}_{2}", currentdate, randomNumber, randomString); // e.g format "AutoGen_Scenario_2019-09-11_448_ab"
            ScenarionameTxt.SendKeys(scenarioName);
            
            //If code number setup is enabled for the organisation then this text box will appear ; it is optional field only
            if(Browser.Exists(Bys.ActAccreditationPage.CodeNumberTxt, ElementCriteria.IsVisible))
            {
                CodeNumberTxt.SendKeys(DataUtils.GetRandomInteger(1000).ToString());
            }            

            string releasedate_as_currentdate = string.Format("{0}", DateTime.UtcNow.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            ReleaseDateTxt.SendKeys(releasedate_as_currentdate);
            
            string expirationDate = string.Format("{0}", DateTime.UtcNow.AddYears(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            ExpirationDateTxt.SendKeys(expirationDate);

            FixedCreditTxt.SendKeys(fixedCredits.ToString());
            EquivalentCreditTxt.SendKeys(EquivalentCredits.ToString());

            if (accrClaimCreditEnabled)
            {
                MaximumCreditsTxt.SendKeys(MaximumCredits.ToString());
                MinimumCreditsTxt.SendKeys(MinimumCredits.ToString());
                CreditIncrementsTxt.SendKeys(CreditIncrements.ToString());
            }
                        
            //this.WaitUntil(Criteria.ActAccreditationPage.EligibleProfessionElemOptionsDropdownHasMoreThanOneValue);            
            ElemSet.DropdownMulti_Fireball_SelectByText(Browser, EligibleProfessionElemBtn, eligibleProfession);
            if (!eligibleSpecialities.IsNullOrEmpty())
            {
                ElemSet.DropdownMulti_Fireball_SelectByText(Browser, EligibleSpecialitiesSelElemBtn, eligibleSpecialities);
               
                EligibleProfessionElemBtn.Click();
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(2400));
                Browser.FindElement(By.XPath("/html/body/div[5]/div/div[2]/div/button[1]")).Click();
                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(2400));
            }
            if (!eligibleCountries.IsNullOrEmpty())
            {
                //ElemSet.DropdownMulti_Fireball_SelectByText(Browser, EligibleCountriesSelElemBtn, eligibleCountries);
            }

            ClickAndWait(AddScenarioFormSaveScenarioBtn);

            //verify that created Scenario is added to the given accreditation and is displayed in UI                                
            IWebElement specifiedAccreditationScenarioTbl = Browser.FindElement(By.XPath(string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'accreditationGridChart')]/following-sibling::div[contains(@class,'scenarioGridChart')]",accreditationBody)));
            if (!ElemGet_LMSAdmin.Grid_CellTextFound(Browser, specifiedAccreditationScenarioTbl, Bys.ActAccreditationPage.ScenarioDetailsTblScenNameColumn, scenarioName))
            {               
                throw new Exception(string.Format("Scenario [{0}] is not added to the [{1}] accreditation body",scenarioName,accreditationBody));                
            }

            return scenarioName;
        }

        /// <summary>
        /// Delete Scenario from the accreditation and verifies that scenario deleted under the given accreditation
        /// </summary>
        /// <param name="accreditationBody">By default, accreditationbody set to "Non-Accredited" value. If you want to delete scenario from specific accreditationbody, then send the exact accreditationbody text from the td element</param>
        /// <param name="scenarioName">Send the Name of Scenario to be deleted</param>
        public void DeleteScenario(string accreditationBody = "Non-Accredited",string scenarioName =null )
        {
            //verify that  Scenario is available to the given accreditation and is displayed in UI                                
            IWebElement specifiedAccreditationScenarioTbl = Browser.FindElement(By.XPath(string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'accreditationGridChart')]/following-sibling::div[contains(@class,'scenarioGridChart')]", accreditationBody)));
            if (!ElemGet_LMSAdmin.Grid_CellTextFound(Browser, specifiedAccreditationScenarioTbl, Bys.ActAccreditationPage.ScenarioDetailsTblScenNameColumn, scenarioName))
            {
                throw new Exception(string.Format("Scenario [{0}] is not available under the [{1}] accreditation body", scenarioName, accreditationBody));
            }

            IWebElement DeleteScenarioBtn = specifiedAccreditationScenarioTbl.FindElement(By.XPath(string.Format("//td[contains(@class,'column-scenarios') and text()='{0}']/following-sibling::td//button[@aria-label='click to delete']", scenarioName)));
            ElemSet.ScrollToElement(Browser, DeleteScenarioBtn, false);
            DeleteScenarioBtn.ClickJS(Browser);

            Browser.WaitForElement(Bys.ActAccreditationPage.ScenarioDeleteFormOkBtn, ElementCriteria.IsVisible);

            Browser.WaitForElement(Bys.ActAccreditationPage.DeleteScenarioFormDeleteScenarioConfirmMessage, 
                ElementCriteria.Text("Are you sure you want to delete this scenario? By selecting 'ok' your Scenario(s) will lose its association to awards, front matter, and requirements", true));

            ClickAndWait(ScenarioDeleteFormOkBtn);

            //verify that  Scenario is deleted from the given accreditation and is not displayed in UI                                
            specifiedAccreditationScenarioTbl = Browser.FindElement(By.XPath(string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/ancestor::div[contains(@class, 'accreditationGridChart')]/following-sibling::div[contains(@class,'scenarioGridChart')]", accreditationBody)));
            if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, specifiedAccreditationScenarioTbl, Bys.ActAccreditationPage.ScenarioDetailsTblScenNameColumn, scenarioName))
            {
                throw new Exception(string.Format("Scenario [{0}] is not deleted from the given [{1}] accreditation body", scenarioName, accreditationBody));
            }

        }

        /// <summary>
        ///Delete the given accreditation and verifies that accreditation deleted from the activity
        /// </summary>
        /// <param name="accreditationBody">By default, accreditationbody set to "Non-Accredited" value. If you want to delete specific accreditationbody, then send the exact accreditationbody text from the td element</param>
        public void DeleteAccreditation(string accreditationBody = "Non-Accredited")
        {
            //verify that accreditationbody  is available in the given activity and is displayed in UI           
            if (Browser.FindElements(Bys.ActAccreditationPage.AccreditationDetailsTbl).Count < 0 ||
                !ElemGet_LMSAdmin.Grid_CellTextFound(Browser, AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
            {
                throw new Exception(string.Format("Accreditation [{0}] is not available in the activity", accreditationBody));
            }
            
            IWebElement DeleteAccreditationButton = Browser.FindElement(By.XPath(string.Format("//td[contains(@class,'accBodyType') and text()='{0}']/following-sibling::td//button[@aria-label='click to delete']",
                accreditationBody)));
            ElemSet.ScrollToElement(Browser, DeleteAccreditationButton, false);
            DeleteAccreditationButton.Click();

            Browser.WaitForElement(Bys.ActAccreditationPage.AccreditationDeleteFormOkBtn, ElementCriteria.IsVisible);

            Browser.WaitForElement(Bys.ActAccreditationPage.DeleteAccreditationFormDeleteAccreditationConfirmMessage, ElementCriteria.Text("Are you sure you want to delete " +
                "this accreditation and its scenarios? By selecting 'ok' your Scenarios will lose their association to awards, front matter, and completion requirements", true));

            ClickAndWait(AccreditationDeleteFormOkBtn);

            //verify that accreditationbody  is deleted from the given activity and is not displayed in UI 
            if (Browser.FindElements(Bys.ActAccreditationPage.AccreditationDetailsTbl).Count > 0)
            {
                if (ElemGet_LMSAdmin.Grid_CellTextFound(Browser, AccreditationDetailsTbl, Bys.ActAccreditationPage.AccreditationTblAccBodyTypeColumn, accreditationBody))
                {
                    throw new Exception(string.Format("Accreditation [{0}] is not deleted from the activity", accreditationBody));
                }
            }
        }


            #endregion methods: page specific


        }
}