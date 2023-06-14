using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using LOG4NET = log4net.ILog;
using System.Linq;
using LMS.Data;
using System.Threading;

namespace Mainpro.AppFramework
{
    public class EnterACPDActivityPage : MainproPage, IDisposable
    {
        #region constructors
        public EnterACPDActivityPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<string> activeRequests = new List<string>();

        MainproHelperMethods Help = new MainproHelperMethods();
        public override string PageUrl { get { return "cpd/enteractivity"; } }

        #endregion properties

        #region elements

        public IWebElement MaxCreditReachedFormStartOverBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.MaxCreditReachedFormStartOverBtn); } }
        public IWebElement MaxCreditReachedFormClaimedLbl { get { return this.FindElement(Bys.EnterACPDActivityPage.MaxCreditReachedFormClaimedLbl); } }
        public IWebElement MaxCreditReachedFormAddNonCertActBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.MaxCreditReachedFormAddNonCertActBtn); } }
        public IWebElement NeedHelpLnk { get { return this.FindElement(Bys.EnterACPDActivityPage.NeedHelpLnk); } }
        public SelectElement ActivityTypeSelElem { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.ActivityTypeSelElem)); } }
        public IWebElement ActivityTypeSelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityTypeSelElemBtn); } }
        public SelectElement CategorySelElem { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.CategorySelElem)); } }
        public IWebElement CategorySelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.CategorySelElemBtn); } }
        public IWebElement SearchResultsTblTooManyResultsLbl { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblTooManyResultsLbl); } }
        public IWebElement SearchResultsTblSessionIDColHdr { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblSessionIDColHdr); } }
        public IWebElement SearchResultsTblProgActTitleColHdr { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblProgActTitleColHdr); } }
        public IWebElement SearchResultsTblCityColHdr { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblCityColHdr); } }
        public IWebElement SearchResultsTbl { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTbl); } }
        public IWebElement SearchResultsTblBody { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblBody); } }
        public IWebElement SearchResultsTblFirstRow { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblFirstRow); } }
        public IWebElement ContinueBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.ContinueBtn); } }
        public IWebElement SearchResultsTblNoResultsLbl { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblNoResultsLbl); } }

        public IWebElement LiveInPersonRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.LiveInPersonRdo); } }
        public IWebElement OnlineRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.OnlineRdo); } }
        public IWebElement SearchResultsTblHdr { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblHdr); } }

        public IWebElement SearchBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchBtn); } }
        public IWebElement CertifiedRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.CertifiedRdo); } }
        public IWebElement NonCertifiedRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.NonCertifiedRdo); } }
        public IWebElement DoYouKnowYourSessionIDSearchTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.DoYouKnowYourSessionIDSearchTxt); } }
        public IWebElement DoYouKnowYourSessionIDContinueBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.DoYouKnowYourSessionIDContinueBtn); } }
        public SelectElement ArticleSelElem { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.ArticleSelElem)); } }
        public IWebElement ArticleSelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.ArticleSelElemBtn); } }
        public SelectElement VolumeSelElem { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.VolumeSelElem)); } }
        public IWebElement VolumeSelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.VolumeSelElemBtn); } }
        public IWebElement ProgramActivityTitleTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ProgramActivityTitleTxt); } }
        public IWebElement IAgreeRdo { get { return this.FindElement(Bys.EnterACPDActivityPage.IAgreeRdo); } }
        public SelectElement QuestionsSelElem { get { return new SelectElement(this.FindElement(Bys.EnterACPDActivityPage.QuestionsSelElem)); } }
        public IWebElement QuestionsSelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.QuestionsSelElemBtn); } }
        public IWebElement QuestionCloseBtnsSelElemBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.CloseBtn); } }
        public IWebElement ClickHereBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.ClickHereBtn); } }
        public IWebElement ResetSearchCriteraBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.ResetSearchCriteraBtn); } }
        public IWebElement ActivityDateTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.ActivityDateTxt); } }
        public IWebElement CityTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.CityTxt); } }
        public IWebElement SessionIDTxt { get { return this.FindElement(Bys.EnterACPDActivityPage.SessionIDTxt); } }
        public IWebElement SupportInfoFormCloseBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.SupportInfoFormCloseBtn); } }
        public IWebElement CloseBtn { get { return this.FindElement(Bys.EnterACPDActivityPage.CloseBtn); } }
        public IWebElement SearchResultsTblFirstCol { get { return this.FindElement(Bys.EnterACPDActivityPage.SearchResultsTblFirstCol); } }
        


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            if (Browser.Exists(Bys.MainproPage.WereSorryErrorLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("The application threw an error '{0}'. Check the Browsers console log for any " +
                    "javascript errors. Sometimes this error occurs intermittently and will not occur a second time when rerunning " +
                    "the same test", WereSorryErrorLbl.Text));
            }
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.EnterACPDActivityPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
            Thread.Sleep(500);
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.EnterACPDActivityPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(60));
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
            if (Browser.Exists(Bys.EnterACPDActivityPage.LiveInPersonRdo))
            {
                if (elem.GetAttribute("outerHTML") == LiveInPersonRdo.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    if (ActivityTypeSelElem.SelectedOption.Text == "CFPC Certified Mainpro+ Activities")
                    {
                        this.WaitUntilAll(Criteria.EnterACPDActivityPage.SearchBtnVisible,
                            Criteria.EnterACPDActivityPage.ContinueBtnExists,
                            Criteria.EnterACPDActivityPage.LoadIconNotExists);
                        Browser.WaitForElement(Bys.EnterACPDActivityPage.CityTxt, ElementCriteria.IsVisible);
                        Browser.WaitForElement(Bys.EnterACPDActivityPage.ActivityDateTxt, ElementCriteria.IsVisible);
                        return null;
                    }
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.CertifiedRdo))
            {
                if (elem.GetAttribute("outerHTML") == CertifiedRdo.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.NonCertifiedRdo))
            {
                if (elem.GetAttribute("outerHTML") == NonCertifiedRdo.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.OnlineRdo))
            {
                if (elem.GetAttribute("outerHTML") == OnlineRdo.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    if (ActivityTypeSelElem.SelectedOption.Text == "CFPC Certified Mainpro+ Activities")
                    {
                        this.WaitUntilAll(Criteria.EnterACPDActivityPage.SearchBtnVisible,
                            Criteria.EnterACPDActivityPage.ContinueBtnExists,
                            Criteria.EnterACPDActivityPage.LoadIconNotExists);
                        Browser.WaitForElement(Bys.EnterACPDActivityPage.SessionIDTxt, ElementCriteria.IsVisible);
                        Browser.WaitForElement(Bys.EnterACPDActivityPage.CityTxt, ElementCriteria.IsNotVisible);
                        Browser.WaitForElement(Bys.EnterACPDActivityPage.ActivityDateTxt, ElementCriteria.IsNotVisible);
                        return null;
                    }
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.IAgreeRdo))
            {
                if (elem.GetAttribute("outerHTML") == IAgreeRdo.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.MaxCreditReachedFormStartOverBtn))
            {
                if (elem.GetAttribute("outerHTML") == MaxCreditReachedFormStartOverBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.MaxCreditReachedFormAddNonCertActBtn))
            {
                if (elem.GetAttribute("outerHTML") == MaxCreditReachedFormAddNonCertActBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.ResetSearchCriteraBtn))
            {
                if (elem.GetAttribute("outerHTML") == ResetSearchCriteraBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Thread.Sleep(500);
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.ContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    ElemSet.ClickAfterScroll(Browser,elem);                   
                    EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
                    try
                    {
                        EADP.WaitForInitialize();
                    }
                    catch
                    {
                        elem.ClickJS(Browser);
                        EADP.WaitForInitialize();
                    }
                    return EADP;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.DoYouKnowYourSessionIDContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == DoYouKnowYourSessionIDContinueBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
                    EADP.WaitForInitialize();
                    return EADP;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.NeedHelpLnk))
            {
                if (elem.GetAttribute("outerHTML") == NeedHelpLnk.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.EnterACPDActivityPage.SupportInfoFormSupportInfoLbl,
                        ElementCriteria.IsVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.SupportInfoFormCloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == SupportInfoFormCloseBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.CloseBtn))
            {
                if (elem.GetAttribute("outerHTML") == CloseBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    Browser.WaitForElement(Bys.EnterACPDActivityPage.ProgramActivityTitleTxt, ElementCriteria.IsNotVisible);
                    Thread.Sleep(500);
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.ClickHereBtn))
            {
                if (elem.GetAttribute("outerHTML") == ClickHereBtn.GetAttribute("outerHTML"))
                {
                    elem.Click();
                    EnterACPDActivityDetailsPage EADP = new EnterACPDActivityDetailsPage(Browser);
                    EADP.WaitForInitialize();
                    return EADP;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.SearchBtn))
            {
                if (elem.GetAttribute("outerHTML") == SearchBtn.GetAttribute("outerHTML"))
                {
                    elem.ClickJS(Browser);
                    Thread.Sleep(300);
                    Browser.WaitJSAndJQuery();
                    this.WaitUntilAny(Criteria.EnterACPDActivityPage.SearchResultsTblFirstRowVisible,
                        Criteria.EnterACPDActivityPage.SearchResultsTblNoResultsLblVisible,
                        Criteria.EnterACPDActivityPage.SearchResultsTblTooManyResultsLblVisible);
                    Browser.WaitJSAndJQuery();
                    this.WaitUntilAny(Criteria.EnterACPDActivityPage.SearchResultsTblFirstRowVisible,
                    Criteria.EnterACPDActivityPage.SearchResultsTblNoResultsLblVisible,
                    Criteria.EnterACPDActivityPage.SearchResultsTblTooManyResultsLblVisible);
                    return null;
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
            if (Browser.Exists(Bys.EnterACPDActivityPage.CategorySelElem))
            {
                if (selectElement.Options[1].Text == CategorySelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, CategorySelElemBtn, selection);
                    }
                    else
                    {
                        CategorySelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.ActivityTypeSelElem))
            {
                if (selectElement.Options[1].Text == ActivityTypeSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ActivityTypeSelElemBtn, selection);
                    }
                    else
                    {
                        ActivityTypeSelElem.SelectByText(selection);
                    }
                    this.WaitUntilAny(Criteria.EnterACPDActivityPage.PageReady,
                        Criteria.EnterACPDActivityPage.MaxCreditReachedFormClaimedLblVisible);
                    Browser.WaitJSAndJQuery();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.ArticleSelElem))
            {
                if (selectElement.Options[1].Text == ArticleSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ArticleSelElemBtn, selection);
                    }
                    else
                    {
                        ArticleSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.VolumeSelElem))
            {
                if (selectElement.Options[1].Text == VolumeSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, VolumeSelElemBtn, selection);
                    }
                    else
                    {
                        VolumeSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    Browser.WaitForElement(Bys.EnterACPDActivityPage.QuestionsSelElem,
                        ElementCriteria.SelectElementHasAtLeast1Item);
                    return null;
                }
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.QuestionsSelElem))
            {
                if (selectElement.Options[1].Text == QuestionsSelElem.Options[1].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() ==
                        BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, QuestionsSelElemBtn, selection);
                    }
                    else
                    {
                        QuestionsSelElem.SelectByText(selection);
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

        /// <summary>
        /// Enters the user-specified text into the user-specified search field, clicks the Search button, then waits for 
        /// either: Records to return. Tthe Too Many Results label, or the No Results Found label
        /// </summary>
        /// <param name="searchField">The name of the activity you want to search for</param>
        /// <param name="searchText">The text you want to enter. For the ActivityDate searchField, you must enter in the format of MM/DD/YYYY</param>
        public void Search(Const_Mainpro.ActivitySearchField searchField, string searchText = null)
        {
            // Clear all fields
            ProgramActivityTitleTxt.Clear();
            SessionIDTxt.Clear();

            if (Browser.Exists(Bys.EnterACPDActivityPage.ActivityDateTxt, ElementCriteria.IsVisible))
            {
                CityTxt.Clear();
                ActivityDateTxt.Clear();
            }

            if (searchField == Const_Mainpro.ActivitySearchField.ProgramActivityTitle)
            {

                ProgramActivityTitleTxt.SendKeys(searchText);
            }

            if (searchField == Const_Mainpro.ActivitySearchField.SessionID)
            {
                SessionIDTxt.SendKeys(searchText);
            }

            if (searchField == Const_Mainpro.ActivitySearchField.City)
            {
                CityTxt.SendKeys(searchText);
            }

            if (searchField == Const_Mainpro.ActivitySearchField.ActivityDate)
            {
                // Check if the date the tester sent is in the correct format
                DateTime date = Convert.ToDateTime(searchText);
                string tempdate = date.ToString("MM/dd/yyyy");

                if (tempdate == searchText)
                {
                    ActivityDateTxt.SendKeys(searchText);
                    ActivityDateTxt.SendKeys(Keys.Tab);
                }
                else
                {
                    throw new Exception("You chose the ActivityDate searchField, so you must pass the correct date " +
                        "format inside the searchText parameter. Correct format is mm/dd/yyyy");
                }
            }

            ClickAndWait(SearchBtn);

            if (Browser.Exists(Bys.EnterACPDActivityPage.SearchResultsTblNoResultsLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("Your search text of '{0}' entered into the '{1}' search " +
                    "field returned no results", searchText, searchField.GetDescription()));
            }

            if (Browser.Exists(Bys.EnterACPDActivityPage.SearchResultsTblTooManyResultsLbl, ElementCriteria.IsVisible))
            {
                throw new Exception(string.Format("Your search text of '{0}' entered into the '{1}' search " +
                    "field returned too many results", searchText, searchField.GetDescription()));
            }
        }

        /// <summary>
        /// Selects 1. A Category. 2. A Certificate Type. 3. An Activity Type. 4. Activity Format (If applicable). 
        /// 5. If the activity type is article based, selects an article in the Article select element. If the select 
        /// element does not contain any items, it will throw an error. If it does contain items, but not the item 
        /// the user specifies, it will throw an error and tell you what items it does have
        /// 6. If the activity type is volume based, selects a volume then 2 questions in the select element. If you 
        /// dont specify any items, it will choose default items. If the select element does not contain any items, it 
        /// will create items for you through the API then it will select these items. If you passed specific 
        /// items, it will choose these items, but if it does not have your specific items, it will throw an error.
        /// </summary>
        /// <param name="category"><see cref="Const_Mainpro.ActivityCategory"/></param>
        /// <param name="certType"><see cref="Const_Mainpro.ActivityCertType"/></param>
        /// <param name="actType"><see cref="Const_Mainpro.ActivityType"/></param>
        /// <param name="actFormat"><see cref="Const_Mainpro.ActivityFormat"/></param>
        /// <param name="articleTitle">(Optional). Selects a user-specified article in the Article select element. 
        /// Default = The first indexed item</param>
        /// <param name="volumeTitleRequested">(Optional). Selects a user-specified volume in the Volume dropdown. If you 
        /// leave this argument null and are calling the Help.AddActivity method choosing an activity that prompts the 
        /// Volume select element to appear, but there are no Volumes for your current user, then this method will create 
        /// a volume (with questions) on-the-fly, refresh the page, and select this newly created Volume and questions.
        /// The title of the volume can not already be in the system, it has to be a new name. 
        /// Also, it must have the word "Volume" in it with a capital V, else the application fails to assign questions
        /// to the volume.
        /// Default = The first indexed volume</param>
        /// <param name="questionsRequested">(Optional). The exact text of the questions to choose from the Questions 
        /// dropdown after selecting the volume. See the argument volumeTitleRequested for further explanation of what 
        /// will occur if you leave this argument null. Default = The first 2 indexed ones</param>
        public void ChooseActivity(
            Const_Mainpro.ActivityCategory category,
            Const_Mainpro.ActivityCertType certType,
            Const_Mainpro.ActivityType actType,
            Const_Mainpro.ActivityFormat?
            actFormat = null,
            string articleTitle = null,
            string volumeTitleRequested = null,
            List<string> questionsRequested = null)
        {
            // If the tester chose an invalid category for the activity type that he/she chose
            if (actType.ToString().Contains("ASMT") && category != Const_Mainpro.ActivityCategory.Assessment)
            {
                throw new Exception("You chose an Assessment activity type, but did not choose the " +
                    "Assessment category");
            }
            if (actType.ToString().Contains("GRPLRNING") && category != Const_Mainpro.ActivityCategory.GroupLearning)
            {
                throw new Exception("You chose a Group Learning activity type, but did not choose the " +
                    "Group Learning category");
            }
            if (actType.ToString().Contains("SELFLRNING") && category != Const_Mainpro.ActivityCategory.SelfLearning)
            {
                throw new Exception("You chose a Self Learning activity type, but did not choose the " +
                    "Self Learning category");
            }

            // If the tester chose an invalid certification type for the activity type that he/she chose
            if (actType.ToString().Contains("_CERT_") && certType != Const_Mainpro.ActivityCertType.Certified)
            {
                throw new Exception("You chose the Non-Certified certification type, but did not choose a " +
                    "Non-Certified activity type");
            }
            if (actType.ToString().Contains("_NONCERT_") && certType != Const_Mainpro.ActivityCertType.NonCertified)
            {
                throw new Exception("You chose the Certified certification type, but did not choose a " +
                    "Certified activity type");
            }

            // If the tester chose to click the live/online radio button but did not choose an activity that has this 
            // option, or vice versa
            if (actType.ToString().Contains("_LO") && actFormat == null)
            {
                throw new Exception("You chose an activity that requires the option to choose either Live or Online, " +
                    "but you did not add the Live or Online parameter in your method call");
            }
            if (!actType.ToString().Contains("_LO") && actFormat != null)
            {
                throw new Exception("You chose an activity that does not have the option to choose either Live or Online, " +
                    "but you added the Live or Online parameter in your method call");
            }


            // Select the category
            SelectAndWait(CategorySelElem, category.GetDescription());

            // Select Certified or Non-Certified
            if (certType == Const_Mainpro.ActivityCertType.Certified)
            {
                ClickAndWait(CertifiedRdo);
            }
            else
            {
                ClickAndWait(NonCertifiedRdo);
            }

            // Select the activity type
            SelectAndWait(ActivityTypeSelElem, actType.GetDescription());

            // Select the activity format
            if (actFormat != null)
            {
                if (actFormat == Const_Mainpro.ActivityFormat.Live)
                {
                    ClickAndWait(LiveInPersonRdo);
                }
                else
                {
                    ClickAndWait(OnlineRdo);
                }
            }

            // If this is an activity that requires the user to click the I Agree radio button, click it
            if (actType == Const_Mainpro.ActivityType.GRPLRNING_NONCERT_OtherNonCertifiedGroupLearningActivities_LO_VR)
            {
                ClickAndWait(IAgreeRdo);
            }

            // If the activity includes articles, choose a tester-specified article or a random one if the tester didnt specify
            if (actType == Const_Mainpro.ActivityType.SELFLRNING_CERT_CFPMainproArticles_V)
            {
                ChooseArticle(articleTitle);
            }


            // If the activity includes volumes/questions, choose the tester-specified volume/questions. If the 
            // tester did not specify, select them at random
            if (actType == Const_Mainpro.ActivityType.SELFLRNING_CERT_SelfLearningProgramImpactAssessment_FC5)
            {
                ChooseVolumeAndQuestions(volumeTitleRequested, questionsRequested);
            }
        }

        /// <summary>
        /// Selects a volume and questions in the Volumes/Questions select elements. If you dont specify any and the 
        /// current user does have volumes/questions, it will choose these existing volumes/questions (choosing 
        /// random ones in the list). If the current user does not have any volumes/questions assigned, it will 
        /// create a volume with questions for you through the API then it will select them. If you passed a 
        /// specific volume/questions, it will choose them, but if it does not have these specific items, 
        /// it will throw an error warning you of this.
        /// </summary>
        /// <param name="volumeTitleRequested">(Optional). Selects a user-specified volume in the Volume dropdown. If you 
        /// leave this argument null and there are no Volumes for your current user, then this method will create 
        /// a Volume (with questions) on-the-fly, refresh the page, and select this newly created Volume and questions.
        /// The title of the volume can not already be in the system, it has to be a new name. 
        /// Also, it must have the word "Volume" in it with a capital V, else the application fails to assign questions
        /// to the volume.
        /// Default = The first indexed volume</param>
        /// <param name="questionsRequested">(Optional). The exact text of the questions to choose from the Questions 
        /// dropdown after selecting the volume. See the argument volumeTitleRequested for further explanation of what 
        /// will occur if you leave this argument null. Default = The first 2 indexed ones</param>
        private void ChooseVolumeAndQuestions(string volumeTitleRequested = null, List<string> questionsRequested = null)
        {
            // If the Volume Select Element has items, but does not have the item the user wants
            if (!string.IsNullOrEmpty(volumeTitleRequested))
            {
                List<string> allVolumesInSelElem = VolumeSelElem.Options.Select(option => option.Text).ToList();

                if (!allVolumesInSelElem.Contains(volumeTitleRequested))
                {
                    throw new Exception("The Volume Select element does not have the volume that you specified. You " +
                        "may have already added an activity with the volume you are trying to use. Delete the existing " +
                        "activity, then try again. Or check the spelling of your volume. Or pass null into the " +
                        "volumeTitle variable and this method will create a new volume/questions for your user");
                }
            }

            // If the user did not specify a volume title and the Volume select element does not contain any items, 
            // create a volume with questions and assign it to the user. NOTE: The Volume Select Element counts the 
            // "Please Select" entry as an item, so we will condition for less than 2 items...
            else if (VolumeSelElem.Options.Count < 2)
            {
                // Create a random volume title
                volumeTitleRequested = string.IsNullOrEmpty(volumeTitleRequested) ? "TestAutoVolume_" + DataUtils.GetRandomString(3) : volumeTitleRequested;
                UserUtils.CreateVolumesAndAddQuestions(volumeTitleRequested, questionsRequested);
                UserUtils.AssignVolumeToUser(APIHelp.GetUserName(Browser), volumeTitleRequested);
            }

            // If the above 2 conditions are not met, then that means we have sufficient volumes to choose. However, 
            // if the Else IF above was met, then we had to create new volumes/question. Because of that, we now have 
            // to refresh the page for these items to show in the select element. Before we refresh, get the values 
            // chosen in the elements, so then after refresh we can select them again
            string selectedCategory = CategorySelElemBtn.GetAttribute("title");
            string selectedActivityType = ActivityTypeSelElemBtn.GetAttribute("title");
            bool certifiedSelected = CertifiedRdo.Selected;

            this.RefreshPage();
            // Select the same items as we selected before we had to refresh the page
            SelectAndWait(CategorySelElem, selectedCategory);
            if (certifiedSelected)
            {
                ClickAndWait(CertifiedRdo);
            }
            else
            {
                ClickAndWait(NonCertifiedRdo);
            }
            SelectAndWait(ActivityTypeSelElem, selectedActivityType);


            volumeTitleRequested = string.IsNullOrEmpty(volumeTitleRequested) ? VolumeSelElem.Options[1].Text : volumeTitleRequested;
            SelectAndWait(VolumeSelElem, volumeTitleRequested);

            // If the user specified a set of questions to choose, then determine if the select element has all of the 
            // questions that the user specified. If it does not have the questions the user wants, throw an error
            if (questionsRequested != null)
            {
                int matchingquestionsFound = 0;
                var questionsInSelElem = QuestionsSelElem.Options.Select(option => option.Text).ToList();
                foreach (var question in questionsRequested)
                {
                    if (questionsInSelElem.ToList().Contains(question))
                    {
                        matchingquestionsFound = matchingquestionsFound + 1;
                    }
                }

                if (matchingquestionsFound < questionsRequested.Count)
                {
                    // Execute API here instead
                    throw new Exception(string.Format("The Question Select element does not have some (or all) of the " +
                        "questions that you specified. #Questions that you specified: {0}. #Questions found: {1} You may " +
                        "have already added an activity with the volume you are trying to use. Delete the existing " +
                        "activity, then try again. Or check the spelling of your volume. Question text in select element: {2}",
                        questionsRequested.Count, matchingquestionsFound, string.Join(",", questionsInSelElem.ToArray())));
                }
            }

            // If the user did not specify questions and the question select element does not contain any items, 
            // throw error
            else if (QuestionsSelElem.Options.Count < 2)
            {
                throw new Exception("The Questions Select Element is empty. Your volume is not associated to any " +
                    "questions. You must create a volume WITH questions first, THEN assign the volume to a user. " +
                    "See the following to add questions to your volume: " +
                    "https://code.premierinc.com/issues/browse/MAINPROREW-288");
            }

            // If the above 2 conditions are not met, then that means we have sufficient questions to choose. 
            // Choose tester-specified volume questions or if the tester did not specify, select the first 2 in the list 
            if (questionsRequested == null)
            {
                SelectAndWait(QuestionsSelElem, QuestionsSelElem.Options[0].Text);
                SelectAndWait(QuestionsSelElem, QuestionsSelElem.Options[1].Text);
            }
            else
            {
                foreach (var question in questionsRequested)
                {
                    SelectAndWait(QuestionsSelElem, question);
                }
            }
        }

        /// <summary>
        /// Selects an article in the Article select element. If the select element does not contain any items, it will
        /// throw an error. If it does contain items, but not the item the user specifies, it will throw an error and tell 
        /// you what items it does have
        /// </summary>
        /// <param name="articleTitle">(Optional). Selects a user-specified article in the Article select element. 
        /// Default = The first indexed item</param>
        private void ChooseArticle(string articleTitle = null)
        {
            // Throw an error if the article select element is empty
            if (ArticleSelElem.Options.Count < 2)
            {
                throw new Exception("The Articles Select element is empty. You may have already added an activity " +
                    "with the article you are trying to use. Delete the existing activity, then try again");
            }

            // If the Article Select Element does have items, but does not have the item the user wants
            else if (ArticleSelElem.Options.Select(option => option.Text).ToList().Contains(articleTitle))
            {
                throw new Exception("The Articles Select element does not have the article that you specified. You " +
                    "may have already added an activity with the article you are trying to use. Delete the existing " +
                    "activity, then try again. Or check the spelling of your article");
            }

            articleTitle = string.IsNullOrEmpty(articleTitle) ? ArticleSelElem.Options[1].Text : articleTitle;
            SelectAndWait(ArticleSelElem, articleTitle);
        }

        /// <summary>
        /// Selects 1. A Category. 2. A Certificate Type. 3. An Activity Type. 4. Activity Format (If applicable). 
        /// 5. If the activity type is article based, selects an article in the Article select element. If the select 
        /// element does not contain any items, it will throw an error. If it does contain items, but not the item 
        /// the user specifies, it will throw an error and tell you what items it does have
        /// 5. If the activity type is volume based, selects a volume then 2 questions in the select element. If you 
        /// dont specify any items, it will choose default items. If the select element does not contain any items, it 
        /// will create  items for you through the API then it will select these items. If you passed specific 
        /// items, it will choose these items, but if it does not have your specific items, it will throw an error. 
        /// Then clicks the Continue button and waits for the correct activity form page to load.
        /// </summary>
        /// <param name="category"><see cref="Const_Mainpro.ActivityCategory"/></param>
        /// <param name="certType"><see cref="Const_Mainpro.ActivityCertType"/></param>
        /// <param name="actType"><see cref="Const_Mainpro.ActivityType"/></param>
        /// <param name="actFormat"><see cref="Const_Mainpro.ActivityFormat"/></param>
        /// <param name="articleTitle">(Optional). Selects a user-specified article in the Article dropdown after selecting the activity type</param>
        /// <param name="volumeTitleRequested">(Optional). Selects a user-specified volume in the Volume dropdown. If you 
        /// leave this argument null and are calling the Help.AddActivity method choosing an activity that prompts the 
        /// Volume select element to appear, but there are no Volumes for your current user, then this method will create 
        /// a volume (with questions) on-the-fly, refresh the page, and select this newly created Volume and questions.
        /// The title of the volume can not already be in the system, it has to be a new name. 
        /// Also, it must have the word "Volume" in it with a capital V, else the application fails to assign questions
        /// to the volume.
        /// Default = The first indexed volume</param>
        /// <param name="questionsRequested">(Optional). The exact text of the questions to choose from the Questions 
        /// dropdown after selecting the volume. See the argument volumeTitleRequested for further explanation of what 
        /// will occur if you leave this argument null. Default = The first 2 indexed ones</param>
        public EnterACPDActivityDetailsPage ChooseActAndCntToDetailsPage(
            Const_Mainpro.ActivityCategory category,
            Const_Mainpro.ActivityCertType certType,
            Const_Mainpro.ActivityType actType,
            Const_Mainpro.ActivityFormat? actFormat = null,
            string articleTitle = null,
            string volumeTitleRequested = null,
            List<string> questionsRequested = null)
        {
            // Session activities can not be used with this method
            if (actType == Const_Mainpro.ActivityType.ASMT_CERT_CFPCCertifiedMainproActivities_LO ||
                actType == Const_Mainpro.ActivityType.GRPLRNING_CERT_CFPCCertifiedMainproActivities ||
                actType == Const_Mainpro.ActivityType.SELFLRNING_CERT_CFPCCertifiedMainproActivities_V)
            {
                throw new Exception("This activity type requires that you search for sessions first. Therefore, you " +
                    "cannot use this method to add this type of activity");
            }

            ChooseActivity(category, certType, actType, actFormat, articleTitle, volumeTitleRequested, questionsRequested);
            return ClickAndWait(ContinueBtn);
        }



        #endregion methods: page specific
    }

}


