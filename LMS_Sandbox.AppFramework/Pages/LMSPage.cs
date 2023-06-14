using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;
using System.Configuration;
using System.Linq;

namespace LMS.AppFramework
{
    /// <summary>
    /// This is the base page for the UAMS website. It contains all elements that appear on EVERY page. So these elements are not specific to 
    /// 1 page. It also includes methods can be called which will work on every page. It extends the Page class inside Browser.Core.Framework.
    /// </summary>
    public abstract class LMSPage : Page
    {
        #region Constructors

        /// <summary>
        /// You will need this constructor for every page class that you create
        /// </summary>
        /// <param name="driver"></param>
        public LMSPage(IWebDriver driver) : base(driver) { }

        #endregion

        #region properties

        #endregion properties

        #region Elements       

        /// <summary>
        /// We are retreiving these elements from the PageBy class. Specifically, <see cref="LMSPageBys"/>. That class is where we locate all
        /// elements by using the By type (xpath, id's, class name, linktext, etc.). Once you locate a new element inside a PageBy class, you 
        /// then need to return it inside the respective Page class, as shown below.
        /// </summary>
        /// 
        
        public IWebElement ActivitiesView_PreAssessmentTab { get { return this.FindElement(Bys.LMSPage.ActivitiesView_PreAssessmentTab); } }

        public List<IWebElement> CustomPageHTMLComponentGroups { get { return this.FindElements(Bys.LMSPage.CustomPageHTMLComponentGroups).ToList(); } }
        public List<IWebElement> CustomPageHTMLComponentNonGroups { get { return this.FindElements(Bys.LMSPage.CustomPageHTMLComponentNonGroups).ToList(); } }
        public IWebElement NotificationPageNoDataErrorLbl { get { return this.FindElement(Bys.LMSPage.NotificationPageNoDataErrorLbl); } }
        public IWebElement NotificationInfoMessageLblXBtn { get { return this.FindElement(Bys.LMSPage.NotificationInfoMessageLblXBtn); } }
        public IWebElement NotificationErrorMessageLblXBtn { get { return this.FindElement(Bys.LMSPage.NotificationErrorMessageLblXBtn); } }
        public IWebElement NotificationInfoMessageLbl { get { return this.FindElement(Bys.LMSPage.NotificationInfoMessageLbl); } }
        public IWebElement NotificationErrorMessageLbl { get { return this.FindElement(Bys.LMSPage.NotificationErrorMessageLbl); } }
        public IWebElement NotificationWarnMessageLbl { get { return this.FindElement(Bys.LMSPage.NotificationWarnMessageLbl); } }
        public IWebElement UserInfoScript { get { return this.FindElement(Bys.LMSPage.UserInfoScript); } }

        public IWebElement Mobile_SearchMagnifyingGlassBtn { get { return this.FindElement(Bys.LMSPage.Mobile_SearchMagnifyingGlassBtn); } }

        public SelectElement VerifyYourProfessionFormProfessionSelElem { get { return new SelectElement(this.FindElement(Bys.LMSPage.VerifyYourProfessionFormProfessionSelElem)); } }
        public IWebElement VerifyYourProfessionFormSubmitBtn { get { return this.FindElement(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn); } }

        public IWebElement TermsOfServiceFormIAcceptBtn { get { return this.FindElement(Bys.LMSPage.TermsOfServiceFormIAcceptBtn); } }

        public IWebElement LoadIcon { get { return this.FindElement(Bys.LMSPage.LoadIcon); } }
        public IWebElement HomeTab { get { return this.FindElement(Bys.LMSPage.HomeTab); } }
        public IWebElement CatalogTab { get { return this.FindElement(Bys.LMSPage.CatalogTab); } }
        public IWebElement ActivitiesInProgressTab { get { return this.FindElement(Bys.LMSPage.ActivitiesInProgressTab); } }
        public IWebElement TranscriptTab { get { return this.FindElement(Bys.LMSPage.TranscriptTab); } }
        public IWebElement FullNameLbl { get { return this.FindElement(Bys.LMSPage.FullNameLbl); } }
        //public IWebElement UserNameLbl_Emulation { get { return this.FindElement(Bys.LMSPage.UserNameLbl_Emulation); } }
        public IWebElement SearchBtn { get { return this.FindElement(Bys.LMSPage.SearchBtn); } }
        public IWebElement ContactInformationLnk { get { return this.FindElement(Bys.LMSPage.ContactInformationLnk); } }
        public IWebElement ChangePasswordLnk { get { return this.FindElement(Bys.LMSPage.ChangePasswordLnk); } }
        public IWebElement ChangeSecurityQuestionLnk { get { return this.FindElement(Bys.LMSPage.ChangeSecurityQuestionLnk); } }
        public SelectElement SearchTypeSelElem { get { return new SelectElement(this.FindElement(Bys.LMSPage.SearchTypeSelElem)); } }
        public IWebElement LoginLnk { get { return this.FindElement(Bys.LMSPage.LoginLnk); } }
        public IWebElement Menu_UserProfile_DropDownBtn { get { return this.FindElement(Bys.LMSPage.Menu_UserProfile_DropDownBtn); } }
        public IWebElement RegisterLnk { get { return this.FindElement(Bys.LMSPage.RegisterLnk); } }
        public IWebElement SearchTxt { get { return this.FindElement(Bys.LMSPage.SearchTxt); } }
        public IWebElement LocationTxt { get { return this.FindElement(Bys.LMSPage.LocationTxt); } }
        public IWebElement SearchTypeBtn { get { return this.FindElement(Bys.LMSPage.SearchTypeBtn); } }
        public IWebElement Menu_UserProfile_AllReceiptsLnk { get { return this.FindElement(Bys.LMSPage.Menu_UserProfile_AllReceiptsLnk); } }
        public IWebElement Menu_UserProfile_SignOutBtn { get { return this.FindElement(Bys.LMSPage.Menu_UserProfile_SignOutBtn); } }
        public IWebElement ActivitiesView_ActivityOverviewTab { get { return this.FindElement(Bys.LMSPage.ActivitiesView_ActivityOverviewTab); } }

        public IWebElement ActivityTitleLbl { get { return this.FindElement(Bys.LMSPage.ActivityTitleLbl); } }
        public IWebElement ClientTitle { get { return this.FindElement(Bys.LMSPage.ClientTitle); } }
        public IWebElement WereSorryErrorLbl { get { return this.FindElement(Bys.LMSPage.WereSorryErrorLbl); } }
        
        #endregion Elements

        #region methods

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWaitBasePage(IWebElement elem)
        {
            if (Browser.Exists(Bys.LMSPage.NotificationInfoMessageLblXBtn))
            {
                if (elem.GetAttribute("outerHTML") == Browser.FindElement(Bys.LMSPage.NotificationInfoMessageLblXBtn).GetAttribute("outerHTML"))
                {
                    Browser.FindElement(Bys.LMSPage.NotificationInfoMessageLblXBtn).Click(Browser);
                    Thread.Sleep(500);
                    return null;
                }
            }

            if (Browser.Exists(Bys.LMSPage.NotificationErrorMessageLblXBtn))
            {
                if (elem.GetAttribute("outerHTML") == Browser.FindElement(Bys.LMSPage.NotificationErrorMessageLblXBtn).GetAttribute("outerHTML"))
                {
                    Browser.FindElement(Bys.LMSPage.NotificationErrorMessageLblXBtn).Click(Browser);
                    Thread.Sleep(500);
                    return null;
                }
            }

            // Error handler to make sure that the button that the tester passed in the parameter is actually on the page
            if (Browser.Exists(Bys.LMSPage.FullNameLbl))
            {
                if (elem.GetAttribute("outerHTML") == FullNameLbl.GetAttribute("outerHTML"))
                {
                    FullNameLbl.Click(Browser);
                    Browser.WaitForElement(Bys.LMSPage.Menu_UserProfile_SignOutBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.LMSPage.ActivitiesView_ActivityOverviewTab))
            {
                if (elem.GetAttribute("outerHTML") == ActivitiesView_ActivityOverviewTab.GetAttribute("outerHTML"))
                {
                    ActivitiesView_ActivityOverviewTab.Click(Browser);

                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    // Wait until the page URL loads
                    var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
                    wait.Until(Browser =>
                    {
                        return Browser.Url.Contains(Constants.PageURLs.Activity_Overview.GetDescription());
                    });

                    ActOverviewPage page = new ActOverviewPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.LMSPage.ActivitiesView_PreAssessmentTab))
            {
                if (elem.GetAttribute("outerHTML") == ActivitiesView_PreAssessmentTab.GetAttribute("outerHTML"))
                {
                    ActivitiesView_PreAssessmentTab.Click(Browser);

                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

                    // Wait until the page URL loads
                    var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
                    wait.Until(Browser =>
                    {
                        return Browser.Url.Contains(Constants.PageURLs.Activity_Pretest.GetDescription());
                    });

                    ActAssessmentPage page = new ActAssessmentPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.LMSPage.Menu_UserProfile_SignOutBtn))
            {
                if (elem.GetAttribute("outerHTML") == Menu_UserProfile_SignOutBtn.GetAttribute("outerHTML"))
                {
                    Menu_UserProfile_SignOutBtn.Click(Browser);

                    WaitUtils.WaitAndClickAlert(Browser, TimeSpan.FromSeconds(10));
                    return null;
                }
            }

            if (Browser.Exists(Bys.LMSPage.Menu_UserProfile_AllReceiptsLnk))
            {
                if (elem.GetAttribute("outerHTML") == Menu_UserProfile_AllReceiptsLnk.GetAttribute("outerHTML"))
                {
                    Menu_UserProfile_AllReceiptsLnk.Click(Browser);
                    AllReceiptsPage page = new AllReceiptsPage(Browser);
                    page.WaitForInitialize();
                    Thread.Sleep(300);
                    return page;
                }
            }


            if (Browser.Exists(Bys.LMSPage.SearchBtn))
            {
                if (elem.GetAttribute("outerHTML") == SearchBtn.GetAttribute("outerHTML"))
                {
                    SearchBtn.Click(Browser);
                    SearchPage SP = new SearchPage(Browser);
                    SP.WaitForInitialize();
                    Thread.Sleep(300);
                    return SP;
                }
            }

            if (Browser.Exists(Bys.LMSPage.ActivitiesInProgressTab))
            {
                if (elem.GetAttribute("outerHTML") == ActivitiesInProgressTab.GetAttribute("outerHTML"))
                {
                    ActivitiesInProgressTab.Click(Browser);
                    ActivitiesInProgressPage Page = new ActivitiesInProgressPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            if (Browser.Exists(Bys.LMSPage.TranscriptTab))
            {
                if (elem.GetAttribute("outerHTML") == TranscriptTab.GetAttribute("outerHTML"))
                {
                    TranscriptTab.Click(Browser);
                    TranscriptPage Page = new TranscriptPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            if (Browser.Exists(Bys.LMSPage.ActivitiesInProgressTab))
            {
                if (elem.GetAttribute("outerHTML") == ActivitiesInProgressTab.GetAttribute("outerHTML"))
                {
                    ActivitiesInProgressTab.Click(Browser);
                    ActivitiesInProgressPage Page = new ActivitiesInProgressPage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            if (Browser.Exists(Bys.LMSPage.HomeTab))
            {
                if (elem.GetAttribute("outerHTML") == HomeTab.GetAttribute("outerHTML"))
                {
                    HomeTab.Click(Browser);
                    HomePage Page = new HomePage(Browser);
                    Page.WaitForInitialize();
                    return Page;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Enters the user-specified text into the Search field, clicks the Search button, then waits for the page to return. 
        /// </summary>
        /// <param name="activityName">The name of the activity you want to search for</param>
        /// <param name="searchType"><see cref="Constants_UAMS.ActivitySearchType"/></param>
        /// <param name="location">The location text you want to enter</param>
        public SearchPage Search(Constants.ActivitySearchType searchType, string activityName = null, string location = null)
        {
            if (Browser.MobileEnabled())
            {
                Mobile_SearchMagnifyingGlassBtn.Click(Browser);
            }

            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
            Browser.WaitForElement(Bys.LMSPage.SearchTypeSelElem);
            Browser.WaitForElement(Bys.LMSPage.SearchTypeBtn, ElementCriteria.IsVisible);

            // If the search type select element doesnt match what the tester wants to select, then select it
            if (SearchTypeSelElem.SelectedOption.Text != searchType.GetDescription())
            {
                string currentBrowserName = Browser.GetCapabilities().GetCapability("browserName").ToString().ToString();

                if (currentBrowserName == BrowserNames.Firefox)
                {
                    ElemSet.DropdownSingle_Fireball_SelectByText(Browser, SearchTypeBtn, searchType.GetDescription());
                }
                else
                {
                    SearchTypeSelElem.SelectByText(searchType.GetDescription());
                }

                Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                //  Browser.WaitForElement(Bys.LMSPage.SearchTxt, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                //  Browser.WaitForElement(Bys.LMSPage.SearchBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
            }

            if (!string.IsNullOrEmpty(location))
            {
                //   Browser.WaitForElement(Bys.LMSPage.LocationTxt, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                LocationTxt.Clear();
                LocationTxt.SendKeys(location);
            }

            if (!string.IsNullOrEmpty(activityName))
            {
                //    Browser.WaitForElement(Bys.LMSPage.SearchBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                SearchTxt.Clear();
                SearchTxt.SendKeys(activityName);
            }

            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
            return ClickAndWaitBasePage(SearchBtn);
        }

        /// <summary>
        /// Clicks the top right-hand User Profile link then clicks the Sign Out link
        /// </summary>
        /// <returns></returns>
        public dynamic LogOut(Constants.SiteCodes siteCode)
        {
            HomePage HP = new HomePage(Browser);
            LoginPage LP = new LoginPage(Browser);

            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(60));

            ClickAndWaitBasePage(FullNameLbl);
            ClickAndWaitBasePage(Menu_UserProfile_SignOutBtn);

            // Have to condition different scenarios on Production for SSO clients
           if (Constants.CurrentEnvironment == Constants.Environments.Production.GetDescription())
            {
                switch (siteCode)
                {
                    case Constants.SiteCodes.ASNC:
                        Thread.Sleep(3000);
                        break;
                    case Constants.SiteCodes.AMA:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.AAFPRS:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.AAFPPN:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.AAPA:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.ABAM:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.ABOTO:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.ACR:
                        Browser.WaitForElement(By.XPath("//*[text()='Login'] | //*[@value='Login']"), ElementCriteria.IsVisible);
                        break;
                    case Constants.SiteCodes.AHA:
                        Thread.Sleep(3000);
                        break;
                    case Constants.SiteCodes.TP:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.PREM:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.DHMC:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.CAP:
                        // Goes to client site
                        //throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                        break;
                    case Constants.SiteCodes.CFPC:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.CMECA:
                        HP.WaitForInitialize();
                        break;
                    case Constants.SiteCodes.CMCD:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.CVSCE:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.ISUOG:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.MEDCONCERT:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.NBME:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.NCPALC:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.NOF:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.KP2P:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.RITEAID:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.SNMMI:
                        Thread.Sleep(1000);
                        break;
                    //throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.UAMS:
                        HP.WaitForInitialize();
                        return HP;
                    case Constants.SiteCodes.ONSLT:
                        Browser.WaitForElement(By.XPath("//div[contains(@id, 'Login')] | //div[contains(@class, 'Login')] | //div[contains(text(), 'Login')] | //span[contains(@id, 'Login')] | //span[contains(@class, 'Login')] | //span[contains(text(), 'Login')] | //button[contains(@id, 'Login')] | //button[contains(@class, 'Login')] | //button[contains(text(), 'Login')] | //div[contains(@id, 'Log In')] | //div[contains(@class, 'Log In')] | //div[contains(text(), 'Log In')] | //span[contains(@id, 'Log In')] | //span[contains(@class, 'Log In')] | //span[contains(text(), 'Log In')] | //button[contains(@id, 'Log In')] | //button[contains(@class, 'Log In')] | //button[contains(text(), 'Log In')]"));
                        break;
                    case Constants.SiteCodes.WILEY:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    default:
                        break;
                }
            }
            else
            {
                switch (siteCode)
                {
                    case Constants.SiteCodes.ASNC:
                        Thread.Sleep(3000);
                        break;
                    case Constants.SiteCodes.AMA:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.AAFPRS:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.AAFPPN:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.AAPA:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.ABAM:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.ABOTO:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.ACR:                      
                        Browser.WaitForElement(By.XPath("//*[text()='Login']"), ElementCriteria.IsVisible);
                        break;
                    case Constants.SiteCodes.AHA:
                        Thread.Sleep(3000);
                        break;
                    case Constants.SiteCodes.TP:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.PREM:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.DHMC:
                        //throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.CAP:
                        // Goes to client site
                        //throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                        break;
                    case Constants.SiteCodes.CFPC:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.CMECA:
                        HP.WaitForInitialize();
                        break;
                    case Constants.SiteCodes.CMCD:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.CVSCE:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.ISUOG:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.MEDCONCERT:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.NBME:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.NCPALC:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.NOF:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.KP2P:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.RITEAID:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.SNMMI:
                        Thread.Sleep(1000);
                        break;
                    //throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    case Constants.SiteCodes.UAMS:
                        HP.WaitForInitialize();
                        return HP;
                    case Constants.SiteCodes.ONSLT:
                        LP.WaitForInitialize();
                        return LP;
                    case Constants.SiteCodes.WILEY:
                        throw new Exception("This has not been coded yet. Please add your code if you are testing this site");
                    default:
                        break;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the logged in username by accessing the Script in the HTML that contains it
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            string scriptText = UserInfoScript.GetAttribute("innerHTML");

            string toBeSearched = "userName\":\"";

            int startIndex = scriptText.IndexOf(toBeSearched);
            int length = toBeSearched.Length;

            string startOfUsernamePlusTheRestOfTheScriptText = scriptText.Substring(startIndex + length);

            int endIndex = startOfUsernamePlusTheRestOfTheScriptText.IndexOf('"');
            length = endIndex - 0;

            string username = startOfUsernamePlusTheRestOfTheScriptText.Substring(0, length);

            return username;

            // Old way of getting username which worked on old application code
            //string scriptText = UserInfoScript.GetAttribute("innerHTML");

            //string toBeSearched = "userName':'";

            //string startOfUsernamePlusTheRestOfTheScriptText = scriptText.Substring(scriptText.IndexOf(toBeSearched) + toBeSearched.Length);

            //int endIndex = startOfUsernamePlusTheRestOfTheScriptText.IndexOf('}') - 1;
            //int lengt = endIndex - 0;

            //string username = startOfUsernamePlusTheRestOfTheScriptText.Substring(0, lengt);

            //return username;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSiteCodeFromURL()
        {
            int startIndex = Browser.Url.IndexOf("/") + 2;
            int endIndex = Browser.Url.IndexOf(".");
            int length = endIndex - startIndex;

            string siteCode = Browser.Url.Substring(startIndex, length);

            return siteCode;
        }

        #endregion methods
    }
}