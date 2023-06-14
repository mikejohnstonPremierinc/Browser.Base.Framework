using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMSAdmin.AppFramework.HelperMethods;
using System.Globalization;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class ActMainPage : Page, IDisposable
    {
        #region constructors
        public ActMainPage(IWebDriver driver) : base(driver)
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
        public SelectElement TimeLocationTabStateSelElem { get { return new SelectElement(this.FindElement(Bys.ActMainPage.TimeLocationTabStateSelElem)); } }
        public SelectElement TimeLocationTabCountrySelElem { get { return new SelectElement(this.FindElement(Bys.ActMainPage.TimeLocationTabCountrySelElem)); } }

        public IWebElement TimeLocationTabSaveBtn { get { return this.FindElement(Bys.ActMainPage.TimeLocationTabSaveBtn); } }

        public IWebElement TimeLocationTabPostalCode { get { return this.FindElement(Bys.ActMainPage.TimeLocationTabPostalCode); } }
        public IWebElement TimeLocationTabCity { get { return this.FindElement(Bys.ActMainPage.TimeLocationTabCity); } }
        public IWebElement TimeLocationTabAddressLine1Txt { get { return this.FindElement(Bys.ActMainPage.TimeLocationTabAddressLine1Txt); } }
        public IWebElement TimeLocationTabLocationNameTxt { get { return this.FindElement(Bys.ActMainPage.TimeLocationTabLocationNameTxt); } }
        public IWebElement DetailsTabShortLabelTxt { get { return this.FindElement(Bys.ActMainPage.DetailsTabShortLabelTxt); } }
        public IWebElement DetailsTabDescriptionTxt { get { return this.FindElement(Bys.ActMainPage.DetailsTabDescriptionTxt); } }
        public IWebElement DetailsTabPublishbtn { get { return this.FindElement(Bys.ActMainPage.DetailsTabPublishbtn); } }
        public IWebElement DetailsTabPublishConfirmbtn { get { return this.FindElement(Bys.ActMainPage.DetailsTabPublishConfirmbtn); } }
        public IWebElement EditPortalFormSaveBtn { get { return this.FindElement(Bys.ActMainPage.EditPortalFormSaveBtn); } }
        public IWebElement EditPortalFormCustomFeeTxt { get { return this.FindElement(Bys.ActMainPage.EditPortalFormCustomFeeTxt); } }
        public IWebElement PubDetailsTabPortalsTblBodyRow { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabPortalsTblBodyRow); } }
        public IWebElement PubDetailsTabPortalsTblBody { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabPortalsTblBody); } }
        public IWebElement PubDetailsTabPortalsTbl { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabPortalsTbl); } }
        public IWebElement PubDetailsTabSelCatTblBody { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabSelCatTblBody); } }
        public IWebElement PubDetailsTabSelCatTblBodyRow { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabSelCatTblBodyRow); } }
        public IWebElement PubDetailsTabAvailCatTblAddCatLoadElem { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatTblAddCatLoadElem); } }
        public IWebElement PubDetailsTabSelectedCatTblRemoveCatLoadElem { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElem); } }
        public IWebElement PubDetailsTabAvailCatTblNextBtn { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatTblNextBtn); } }
        public IWebElement PubDetailsTabAvailCatTblFirstBtn { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatTblFirstBtn); } }
        public IWebElement PubDetailsTabAvailCatTblBodyRow { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatTblBodyRow); } }
        public IWebElement PubDetailsTabAvailCatTblBody { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatTblBody); } }
        public IWebElement DetailsTab { get { return this.FindElement(Bys.ActMainPage.DetailsTab); } }
        public IWebElement TimeLocationTab { get { return this.FindElement(Bys.ActMainPage.TimeLocationTab); } }

        public IWebElement PubDetailsTabAvailCatTblSearchCatLoadElem { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatTblSearchCatLoadElem); } }
        public IWebElement PubDetailsTabSelCatTbl { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabSelCatTbl); } }
        public IWebElement PubDetailsTabAvailCatTbl { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatTbl); } }
        public IWebElement PubDetailsTabAvailCatSearchTxt { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatSearchTxt); } }
        public IWebElement PubDetailsTabAvailCatSearchBtn { get { return this.FindElement(Bys.ActMainPage.PubDetailsTabAvailCatSearchBtn); } }
        public IWebElement PubDetailsTab { get { return this.FindElement(Bys.ActMainPage.PubDetailsTab); } }
        public IWebElement DetailsTabUnPublishBtn { get { return this.FindElement(Bys.ActMainPage.DetailsTabUnPublishBtn); } }
        public IWebElement DetailsTabUnPublishConfirmBtn { get { return this.FindElement(Bys.ActMainPage.DetailsTabUnPublishConfirmBtn); } }
        public IWebElement DetailsTabSavebtn { get { return this.FindElement(Bys.ActMainPage.DetailsTabSavebtn); } }
        public SelectElement DetailsTabStageSelElem { get { return new SelectElement(this.FindElement(Bys.ActMainPage.DetailsTabStageSelElem)); } }
        public IWebElement DetailsTabActivityTitleTxt { get { return this.FindElement(Bys.ActMainPage.DetailsTabActivityTitleTxt); } }
        public IWebElement DetailsTabActivityNumberLbl { get { return this.FindElement(Bys.ActMainPage.DetailsTabActivityNumberLbl); } }
        

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActMainPage.PageReady);
            // Adding a little sleep here, because in Firefox, there is a weird little delay which makes clicking on the Publishing
            // Details tab fail without this sleep
            Thread.Sleep(0500);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.ActMainPage.PubDetailsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == PubDetailsTab.GetAttribute("outerHTML"))
                {
                    // Sometimes Firefox fails to click this tab. So I have implemented javascript click. This works a lot more consistently, but I once
                    // saw that even this failed to click it. I am now adding a try catch. Monitor going forward
                    try
                    {
                        buttonOrLinkElem.ClickJS(Browser);
                        this.WaitUntil(TimeSpan.FromSeconds(240), Criteria.ActMainPage.PubDetailsTabAvailCatTblVisible);
                    }
                    catch
                    {
                        buttonOrLinkElem.ClickJS(Browser);
                        this.WaitUntil(TimeSpan.FromSeconds(240), Criteria.ActMainPage.PubDetailsTabAvailCatTblVisible);
                    }

                    return null;
                }
            }

            
            if (Browser.Exists(Bys.ActMainPage.DetailsTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTab.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.ClickJS(Browser);
                    this.WaitUntil(Criteria.ActMainPage.PageReady);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.TimeLocationTab))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TimeLocationTab.GetAttribute("outerHTML"))
                {
                    TimeLocationTab.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActMainPage.TimeLocationTabPostalCode, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.TimeLocationTabSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TimeLocationTabSaveBtn.GetAttribute("outerHTML"))
                {
                    TimeLocationTabSaveBtn.ClickJS(Browser);
                    Thread.Sleep(1000);
                    //this.WaitUntil(Criteria.ActMainPage.PageReady);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.PubDetailsTabAvailCatSearchBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == PubDetailsTabAvailCatSearchBtn.GetAttribute("outerHTML"))
                {
                    try
                    {
                        buttonOrLinkElem.Click();                   
                        this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActMainPage.PubDetailsTabAvailCatTblSearchCatLoadElemVisible);
                        this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActMainPage.PubDetailsTabAvailCatTblSearchCatLoadElemNotVisible);
                    }
                    catch
                    {
                        buttonOrLinkElem.ClickJS(Browser);
                        this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActMainPage.PubDetailsTabAvailCatTblSearchCatLoadElemVisible);
                        this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActMainPage.PubDetailsTabAvailCatTblSearchCatLoadElemNotVisible);
                    }

                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.DetailsTabUnPublishBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabUnPublishBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ActMainPage.DetailsTabUnPublishConfirmBtn, ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.DetailsTabUnPublishConfirmBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabUnPublishConfirmBtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ActMainPage.DetailsTabSavebtn, TimeSpan.FromSeconds(240), ElementCriteria.IsEnabled, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.DetailsTabPublishbtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabPublishbtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ActMainPage.DetailsTabPublishConfirmbtn, TimeSpan.FromSeconds(240), ElementCriteria.IsVisible, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.DetailsTabPublishConfirmbtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabPublishConfirmbtn.GetAttribute("outerHTML"))
                {
                    buttonOrLinkElem.Click();
                    Browser.WaitForElement(Bys.ActMainPage.DetailsTabUnPublishBtn, TimeSpan.FromSeconds(240), ElementCriteria.IsVisible, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.DetailsTabSavebtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == DetailsTabSavebtn.GetAttribute("outerHTML"))
                {
                    DetailsTabSavebtn.Click();
                    // ToDo: Need to add some type of wait criteria here. A test failed in firefox once so far because a 1 second wait wasnt long enough
                    Thread.Sleep(4000);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActMainPage.EditPortalFormSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == EditPortalFormSaveBtn.GetAttribute("outerHTML"))
                {
                    EditPortalFormSaveBtn.Click();
                    this.WaitUntil(TimeSpan.FromSeconds(240), Criteria.ActMainPage.EditPortalFormCustomFeeTxtNotVisible);
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose LoginPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Checks to see if the Unpublish button is appearing on the Details tab (which means the activity is already published yet).
        /// If so, then this method does nothing. If not, then it checks to see if the Publish button is appearing. If so, then it 
        /// clicks the Publish button, then clicks the Publish Confirm button. If not, then it sets the stage to Construction
        /// Complete, and then publishes the activity
        /// </summary>
        public void PublishActivity()
        {
            ClickAndWait(DetailsTab);

            if (!Browser.Exists(Bys.ActMainPage.DetailsTabUnPublishBtn, ElementCriteria.IsVisible))
            {
                if (!Browser.Exists(Bys.ActMainPage.DetailsTabPublishbtn, ElementCriteria.IsVisible))
                {
                    ChangeActivityStage(Constants.ActStage.ConstructionComplete);
                }

                ClickAndWait(DetailsTabPublishbtn);
                ClickAndWait(DetailsTabPublishConfirmbtn);
            }
        }

        /// <summary>
        /// Clicks on the Publishing Details tab, enters text in the Available Catalogs search box, clicks on Search, then
        /// waits a static amount of time. 
        /// </summary>
        /// <param name="searchText"></param>
        public void SearchForAvailableCatalog(string searchText)
        {
            ClickAndWait(PubDetailsTab);

            PubDetailsTabAvailCatSearchTxt.SendKeys(searchText);
            Thread.Sleep(1000);

            ClickAndWait(PubDetailsTabAvailCatSearchBtn);
        }

        /// <summary>
        /// Clicks on the Details tab, then sets the activity to a stage of your choice. Note that this will unpublish an activity
        /// if it is published
        /// </summary>
        /// <param name="stage">The text from one of the items in the Stage select element</param>
        public void ChangeActivityStage(Constants.ActStage activityStage)
        {
            ClickAndWait(DetailsTab);

            switch (activityStage)
            {
                case Constants.ActStage.UnderConstruction:
                    if (Browser.Exists(Bys.ActMainPage.DetailsTabUnPublishBtn, ElementCriteria.IsVisible))
                    {
                        ClickAndWait(DetailsTabUnPublishBtn);
                        ClickAndWait(DetailsTabUnPublishConfirmBtn);
                    }

                    else if (DetailsTabStageSelElem.SelectedOption.Text == "Construction Complete")
                    {
                        DetailsTabStageSelElem.SelectByText("Under Construction");
                        ClickAndWait(DetailsTabSavebtn);
                        this.WaitUntil(TimeSpan.FromSeconds(90),Criteria.ActMainPage.ConstructionCompleteMessageLblNotVisible);
                    }
                    break;

                case Constants.ActStage.UnderReview:

                    break;

                case Constants.ActStage.ConstructionComplete:
                    DetailsTabStageSelElem.SelectByText("Construction Complete");
                    ClickAndWait(DetailsTabSavebtn);
                    this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActMainPage.ConstructionCompleteMessageLblVisible);
                    //this.WaitUntil(Criteria.ActMainPage.DetailsTabPublishbtnVisible); it does not applicable in all cases ; like activity with session type
                    break;
            }
        }

        /// <summary>
        /// Goes to the Publishing Details tab. If the catalog is not added to the activity, then this searches for the catalog in the available table,
        /// clicks on the plus icon of this catalog, and then waits for catalog to be added to the Selected Catalogs table
        /// </summary>
        /// <param name="catalogName">The name of the activity</param>
        /// <returns></returns>
        public void AddCatalogToActivity(string catalogName)
        {
            ClickAndWait(PubDetailsTab);

            // If the catalog is not in the selected table, then add it
            if (!ElemGet.Grid_ContainsRecord(Browser, PubDetailsTabSelCatTbl, Bys.ActMainPage.PubDetailsTabSelCatTblBodyRow, 1, catalogName, "td"))
            {
                // If the catalog is not showing on the 1st page of the available table, then search for it
                if (!ElemGet.Grid_ContainsRecord(Browser, PubDetailsTabAvailCatTbl, Bys.ActMainPage.PubDetailsTabAvailCatTblBodyRow, 1,
                    catalogName, "td"))
                {
                    SearchForAvailableCatalog(catalogName);
                }

                IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowName(PubDetailsTabAvailCatTbl, Bys.ActMainPage.PubDetailsTabAvailCatTblBodyRow, catalogName, "td");

                ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "input", "Add");

                // Sometimes it fails to click, so doing a try catch
                try
                {
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActMainPage.PubDetailsTabAvailCatTblAddCatLoadElemVisible);
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActMainPage.PubDetailsTabAvailCatTblAddCatLoadElemNotVisible);
                }
                catch
                {
                    ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "input", "Add");

                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActMainPage.PubDetailsTabAvailCatTblAddCatLoadElemVisible);
                    this.WaitUntil(TimeSpan.FromSeconds(180), Criteria.ActMainPage.PubDetailsTabAvailCatTblAddCatLoadElemNotVisible);
                }

                Thread.Sleep(1000);

                if (Browser.Exists(Bys.ActMainPage.YouDoNotHaveEditingRightsWarningLbl, ElementCriteria.IsVisible))
                {
                    throw new Exception(string.Format("Your user does not have the ability to add the catalog {0} to any activity. You will need " +
                        "to login with a LMSAdmin user that has this privelege, or you will have to edit this user's privelege. To do this, you must login " +
                        "to LMSAdmin with the overall 'admin' account, then follow these steps: 1. Go to the Distribution tab 2. Click the Catalogs link 3. " +
                        "Click to edit the catalog 4. Go to the Stakeholders tab 5. Click the Users tab 6. Click the plus sign next to the account to " +
                        "add it as a stakeholder", catalogName));
                }
            }
        }

        /// <summary>
        /// Goes to the Publishing Details tab. If the catalog is added to the activity, this will click on the X icon of that user-specified catalog in
        /// the  Selected Catalogs table and then waits for catalog to be added to the Available Catalogs table
        /// </summary>
        /// <param name="catalogName">The name of the activity</param>
        /// <returns></returns>
        public void RemoveCatalogFromActivity(string catalogName)
        {
            ClickAndWait(PubDetailsTab);

            // If the catalog is in the Selected Catalogs table, remove it
            if (ElemGet.Grid_ContainsRecord(Browser, PubDetailsTabSelCatTbl, Bys.ActMainPage.PubDetailsTabSelCatTblBodyRow, 1, catalogName, "td"))
            {
                IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowName(PubDetailsTabSelCatTbl, Bys.ActMainPage.PubDetailsTabSelCatTblBodyRow,
                catalogName, "td");
                ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "input", "Remove");
                this.WaitUntil(TimeSpan.FromSeconds(240), Criteria.ActMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElemVisible);
                this.WaitUntil(TimeSpan.FromMinutes(3), Criteria.ActMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElemNotVisible);
            }
        }

        /// <summary>
        /// Goes to the Publishing Details tab, clicks on the pencil icon of a user-specified portal in the Portal table, enters user-specified text 
        /// into the Custom Fee field, then clicks
        /// on Save
        /// </summary>
        /// <param name="portalName">the portal name</param>
        /// <param name="customFee">The custom fee you want to add</param>
        public void ChangeCustomFee(string portalName, string customFee)
        {
            ClickAndWait(PubDetailsTab);

            IWebElement row = ElemGet_LMSAdmin.Grid_GetRowByRowName(PubDetailsTabPortalsTbl, Bys.ActMainPage.PubDetailsTabPortalsTblBodyRow,
                portalName, "td");

            ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(row, "input", "Edit");

            this.WaitUntil(Criteria.ActMainPage.EditPortalFormCustomFeeTxtVisible);

            EditPortalFormCustomFeeTxt.Clear();

            EditPortalFormCustomFeeTxt.SendKeys(customFee);

            ClickAndWait(EditPortalFormSaveBtn);
        }

        /// <summary>
        /// Checks to see if the Unpublish button is appearing on the Details tab (which means the activity is published). If so, this
        /// clicks the Unpublish button, then clicks the Unpublish Confirm button,
        /// </summary>
        public void UnpublishActivity()
        {
            ClickAndWait(DetailsTab);
            if (Browser.Exists(Bys.ActMainPage.DetailsTabUnPublishBtn, ElementCriteria.IsVisible))
            {
                ClickAndWait(DetailsTabUnPublishBtn);
                ClickAndWait(DetailsTabUnPublishConfirmBtn);
            }
        }

        /// <summary>
        /// Adds a location to an activity
        /// </summary>
        /// <param name="locationName">(Optional). Will generate a random string if you dont provide one</param>
        /// <param name="addLine1">(Optional). Will use our office building address if you dont provide one</param>
        /// <param name="city">(Optional). Will use our office building address if you dont provide one</param>
        /// <param name="state">(Optional). Will use our office building address if you dont provide one</param>
        /// <param name="country">(Optional). Will use our office building address if you dont provide one</param>
        /// <param name="zipcode">(Optional). Will use our office building address if you dont provide one</param>
        /// <returns></returns>
        internal Location AddLocation(string locationName = null, string addLine1 = null, string city = null, string state = null, string country = null,
            string zipcode = null)
        {
            addLine1 = string.IsNullOrEmpty(addLine1) ? "285 E Waterfront Dr #100" : addLine1;
            city = string.IsNullOrEmpty(city) ? "Homestead" : city;
            country = string.IsNullOrEmpty(country) ? "United States" : country;
            state = string.IsNullOrEmpty(state) ? "Pennsylvania" : state;
            zipcode = string.IsNullOrEmpty(zipcode) ? "15120" : zipcode;


            string timeStamp = string.Format("{0}", DateTime.UtcNow.ToString("MM-dd-yy HH:mm:ss.fff", CultureInfo.InvariantCulture));
            locationName = string.IsNullOrEmpty(locationName) ? string.Format("Location {0}", timeStamp) : locationName;

            ClickAndWaitBasePage(TreeLinks_MainActivity);
            ClickAndWait(TimeLocationTab);

            TimeLocationTabLocationNameTxt.SendKeys(locationName);
            TimeLocationTabAddressLine1Txt.SendKeys(addLine1);
            TimeLocationTabCity.SendKeys(city);
            TimeLocationTabPostalCode.SendKeys(zipcode);
            TimeLocationTabCountrySelElem.SelectByText(country);
            TimeLocationTabStateSelElem.SelectByText(state);

            ClickAndWait(TimeLocationTabSaveBtn);

            return new Location(locationName, addLine1, city, country, state, zipcode);
        }

        #endregion methods: page specific



    }
}