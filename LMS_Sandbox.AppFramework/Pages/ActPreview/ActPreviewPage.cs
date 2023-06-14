using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMS.AppFramework.LMSHelperMethods;
using System.Linq;

namespace LMS.AppFramework
{
    public class ActPreviewPage : LMSPage, IDisposable
    {
        #region constructors
        public ActPreviewPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();
        public override string PageUrl { get { return "lms/#/activity"; } }

        LMSHelperMethods.LMSHelperMethods Help = new LMSHelperMethods.LMSHelperMethods();
        #endregion properties

        #region elements

        public IWebElement RegisterBtn { get { return this.FindElement(Bys.ActPreviewPage.RegisterBtn); } }
        public IWebElement LaunchBtn { get { return this.FindElement(Bys.ActPreviewPage.LaunchBtn); } }
        public IWebElement ResumeBtn { get { return this.FindElement(Bys.ActPreviewPage.ResumeBtn); } }
        public IWebElement LaunchOrRegisterOrResumeBtn { get { return this.FindElement(Bys.ActPreviewPage.LaunchOrRegisterOrResumeBtn); } }

        public IWebElement NotAvailableBtn { get { return this.FindElement(Bys.ActPreviewPage.NotAvailableBtn); } }
        public IWebElement InclTheseActsTab { get { return this.FindElement(Bys.ActPreviewPage.InclTheseActsTab); } }
        public IWebElement IncTheseActsTab_BundlesTbl { get { return this.FindElement(Bys.ActPreviewPage.IncTheseActsTab_BundlesTbl); } }
        public IWebElement IncTheseActsTab_BundlesTblBody { get { return this.FindElement(Bys.ActPreviewPage.IncTheseActsTab_BundlesTblBody); } }
        public IList<IWebElement> IncTheseActsTab_BundlesTblBodyActivityLnks { get { return this.FindElements(Bys.ActPreviewPage.IncTheseActsTab_BundlesTblBodyActivityLnks); } }
        public IWebElement CityStateZipCountryLbl { get { return this.FindElement(Bys.ActPreviewPage.CityStateZipCountryLbl); } }
        public IWebElement StreetAddressLbl { get { return this.FindElement(Bys.ActPreviewPage.StreetAddressLbl); } }
        public IWebElement StartDateValueLbl { get { return this.FindElement(Bys.ActPreviewPage.StartDateValueLbl); } }
        public IWebElement EndDateValueLbl { get { return this.FindElement(Bys.ActPreviewPage.EndDateValueLbl); } }
        public IList<IWebElement> AccreditationBodyNameLbls { get { return this.FindElements(Bys.ActPreviewPage.AccreditationBodyNameLbls); } }
        public IList<IWebElement> AccreditationRows { get { return this.FindElements(Bys.ActPreviewPage.AccreditationRows); } }
        public IWebElement ReleaseDateValueLbl { get { return this.FindElement(Bys.ActPreviewPage.ReleaseDateValueLbl); } }
        public IWebElement ExpirationDateValueLbl { get { return this.FindElement(Bys.ActPreviewPage.ExpirationDateValueLbl); } }
        public IWebElement ActivityMaterialTabBtn { get { return this.FindElement(Bys.ActPreviewPage.ActivityMaterialTabBtn); } }
        public IList<IWebElement> ActivityMaterialFileExtensionLbls { get { return this.FindElements(Bys.ActPreviewPage.ActivityMaterialFileExtensionLbls); } }
        public IWebElement AccessCodeFormAccessCodeTxt { get { return this.FindElement(Bys.ActPreviewPage.AccessCodeFormAccessCodeTxt); } }
        public IWebElement AccessCodeFormContinueBtn { get { return this.FindElement(Bys.ActPreviewPage.AccessCodeFormContinueBtn); } }
        public IWebElement AccessCodeFormRequiredLbl { get { return this.FindElement(Bys.ActPreviewPage.AccessCodeFormRequiredLbl); } }
        public IWebElement PageWarningMessageLbl { get { return this.FindElement(Bys.ActPreviewPage.PageWarningMessageLbl); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActPreviewPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            // For some portals (SNMMI and CAP), after clicking Register or Resume in UAT/Prod, the user will be redirected 
            // to the vanity/client site. We dont want this. for a workaround, we are creating a browser Cookie, which stores 
            // a key/value pair, then adding it to the current browser session. LMS DEV then has a check in place to check 
            // for this cookie, and if its present and has the correct password, it will not redirect to vanity URL
            // MJ 11/17/20: This failed in IE, skipping it in IE
            if (Browser.GetCapabilities().GetCapability("browserName").ToString() != "internet explorer")
            {
                // MJ 4/26/21: Firefox throws an error...
                // Cookies may only be set for the current domain (acr.cmeuatf.premierinc.com)
                // if a test is executed NOT on SNMMI and we try to set cookies. So I am adding the below if
                // condition to only add cookies if we are on SNMMI
                if (Browser.Url.Contains("snmmi"))
                {
                    string key = "isAutomationTest";
                    string value = "Pr3m1er@1";
                    if (Help.EnvironmentEquals(Constants.Environments.UAT))
                    {
                        Cookie cookieUAT = new Cookie(key, value, "snmmi.cmeuatf.premierinc.com", "/", null);
                        Browser.Manage().Cookies.AddCookie(cookieUAT);
                    }
                    if (Help.EnvironmentEquals(Constants.Environments.Production))
                    {
                        Cookie cookieProd = new Cookie(key, value, "snmmi.community360.net", "/", null);
                        Browser.Manage().Cookies.AddCookie(cookieProd);
                    }
                }
            }

            if (Browser.Exists(Bys.ActPreviewPage.LaunchBtn))
            {
                if (elem.Text == LaunchBtn.Text)
                {
                    // If activity requires an Access Code
                    if (elem.GetAttribute("data-attach-event") == "click|collectAccessCode")
                    {
                        LaunchBtn.ClickJS(Browser);
                        Browser.WaitForElement(Bys.ActPreviewPage.AccessCodeFormAccessCodeTxt, ElementCriteria.IsVisible);
                        this.WaitUntil(Criteria.ActPreviewPage.LoadIconNotExists);
                        return null;
                    }
                    else
                    {
                        LaunchBtn.ClickJS(Browser);
                    }

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Preview);
                }
            }

            if (Browser.Exists(Bys.ActPreviewPage.RegisterBtn))
            {
                if (elem.Text == RegisterBtn.Text)
                {
                    // If activity requires an Access Code, then we have to wait for the access code window after clicking Register. 
                    // On newer application code, we have to check by getting the parent element (Button tag) of the PageBy mapped 
                    // element for the Register button, then checking to see if it has the attribute of collectAccessCode. On older 
                    // this attribute was direcly on the PageBy mapped element (span tag). Right now we are in between code on Prod
                    // and UAT, so we will add an if statement to determine whether new or old code is there...
                    // If we find that the element does have a button tag element for its parent (new application code)...
                    if (elem.FindElements(By.XPath("ancestor::button")).Count > 0)
                    {
                        if (elem.FindElement(By.XPath("ancestor::button")).GetAttribute("data-attach-event") == "click|collectAccessCode")
                        {
                            // Using javascript click here for the following reason. When we use a regular click, IE then doesnt
                            // load the page fully for some reason. This is not reproducable manually
                            // MJ 4/5/21: After clicking with JScript click on the Register button then the Access Code form
                            // Continue button, a never ending load icon appears. This always worked before and this works in
                            // Prod right now but does not work in UAT right now. Suman Pathapi said the Register button is
                            // not getting the last Focus after using the Javascript click on it, and that is causing the
                            // issue. They are investigating, but for right now I am switching ALL javascript clicks
                            // back to just a regular click since we dont execure in IE anymore
                            RegisterBtn.ClickJS(Browser);
                            Browser.WaitForElement(Bys.ActPreviewPage.AccessCodeFormAccessCodeTxt, TimeSpan.FromSeconds(120),
                                ElementCriteria.IsVisible);
                            this.WaitUntil(Criteria.ActPreviewPage.LoadIconNotExists);
                            return null;
                        }
                        else
                        {
                            RegisterBtn.ClickJS(Browser);
                            return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Preview);
                        }
                    }
                    else
                    {
                        if (elem.GetAttribute("data-attach-event") == "click|collectAccessCode")
                        {
                            RegisterBtn.ClickJS(Browser);
                            Browser.WaitForElement(Bys.ActPreviewPage.AccessCodeFormAccessCodeTxt, ElementCriteria.IsVisible);
                            this.WaitUntil(Criteria.ActPreviewPage.LoadIconNotExists);
                            return null;
                        }
                        else
                        {
                            RegisterBtn.ClickJS(Browser);
                            return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Preview);
                        }
                    }
                }
            }

            if (Browser.Exists(Bys.ActPreviewPage.ResumeBtn))
            {
                if (elem.Text == ResumeBtn.Text)
                {
                    // If activity requires an Access Code, then we have to wait for the access code window after clicking Register. 
                    // On newer application code, we have to check by getting the parent element (Button tag) of the PageBy mapped 
                    // element for the Register button, then checking to see if it has the attribute of collectAccessCode. On older 
                    // this attribute was direcly on the PageBy mapped element (span tag). Right now we are in between code on Prod
                    // and UAT, so we will add an if statement to determine whether new or old code is there...
                    // If we find that the element does have a button tag element for its parent (new application code)...
                    if (elem.FindElements(By.XPath("ancestor::button")).Count > 0)
                    {
                        if (elem.FindElement(By.XPath("ancestor::button")).GetAttribute("data-attach-event") == "click|collectAccessCode")
                        {
                            ResumeBtn.ClickJS(Browser);
                            Browser.WaitForElement(Bys.ActPreviewPage.AccessCodeFormAccessCodeTxt, ElementCriteria.IsVisible);
                            this.WaitUntil(Criteria.ActPreviewPage.LoadIconNotExists);
                            return null;
                        }
                        else
                        {
                            ResumeBtn.ClickJS(Browser);
                            return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Preview);
                        }
                    }
                    else
                    {
                        if (elem.GetAttribute("data-attach-event") == "click|collectAccessCode")
                        {
                            ResumeBtn.ClickJS(Browser);
                            Browser.WaitForElement(Bys.ActPreviewPage.AccessCodeFormAccessCodeTxt, ElementCriteria.IsVisible);
                            this.WaitUntil(Criteria.ActPreviewPage.LoadIconNotExists);
                            return null;
                        }
                        else
                        {
                            ResumeBtn.ClickJS(Browser);
                            return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Preview);
                        }
                    }
                }
            }

            if (Browser.Exists(Bys.ActPreviewPage.AccessCodeFormContinueBtn))
            {
                // Using javascript click here for the following reason. When we use a regular click, IE then doesnt load 
                // the page fully for some reason. This is not reproducable manually
                // MJ 4/5/21: After clicking with JScript click on the Register button, then clicking the Access Code form
                // Continue button, a never ending load icon appeared. This always worked before and this works in Prod right now
                // but does not work in UAT right now. Suman Pathapi said the Register button is not getting the last Focus
                // (or he mentioned last active element also) after 
                // using the Javascript click on it, and that is causing the the never ending load icon. They have implemented 
                // a fix and will be in next build. If I ever encounter something like this again, run the test, put a breakpoint
                // before the click of the button, then go into the Console of DEV tools, click the button with automation, then 
                // you can see an Uncaught TypeError: t is not a function at Object.unfocusModal. There is a JQuery you can 
                // execute (document.activeElement.tagName;) that maybe represents the last active element, but I think its
                // just representing the active element. If this happens again, try to find a way to see the last active element
                // within the DOM so I can determine if its the same cause.
                // See https://stackoverflow.com/questions/7329141/how-do-i-get-the-previously-focused-element-in-javascriptwhat 
                Thread.Sleep(1000);
                AccessCodeFormContinueBtn.ClickJS(Browser);

                return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Preview);
            }

            if (Browser.Exists(Bys.ActPreviewPage.InclTheseActsTab))
            {
                if (elem.Text == InclTheseActsTab.Text)
                {
                    InclTheseActsTab.Click(Browser);
                    Browser.WaitForElement(Bys.ActPreviewPage.IncTheseActsTab_BundlesTblBodyActivityLnks, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActPreviewPage.ActivityMaterialTabBtn))
            {
                if (elem.GetAttribute("outerHTML") == ActivityMaterialTabBtn.GetAttribute("outerHTML"))
                {
                    ActivityMaterialTabBtn.Click(Browser);
                    try
                    {
                        Browser.WaitForElement(Bys.ActPreviewPage.ActivityMaterialFileExtensionLbls, ElementCriteria.IsVisible);
                    }
                    catch (Exception)
                    {
                        ActivityMaterialTabBtn.Click(Browser);
                        Browser.WaitForElement(Bys.ActPreviewPage.ActivityMaterialFileExtensionLbls, ElementCriteria.IsVisible);
                    }
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActivityPreviewPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessCode"></param>
        public dynamic SubmitAccessCode(string accessCode)
        {
            ClickAndWait(LaunchOrRegisterOrResumeBtn);
            AccessCodeFormAccessCodeTxt.SendKeys(accessCode);
            return ClickAndWait(AccessCodeFormContinueBtn);
        }


        /// <summary>
        /// Returns all information that is displayed on this page for an activity 
        /// </summary>
        /// <returns></returns>
        public Constants.Activity GetActivityDetails()
        {
            Constants.Activity Activity = new Constants.Activity();
            Constants.AddressAndLocation addAndLoc = null; ;

            // Set title and front matter
            Activity.ActivityTitle = ActivityTitleLbl.Text;
            Activity.Release_Date = ReleaseDateValueLbl.Text;
            Activity.Expiration_Date = ExpirationDateValueLbl.Text;

            // Set accreditations
            List<Constants.Accreditation> Accreditations = new List<Constants.Accreditation>();
            for (var i = 0; i < AccreditationRows.Count; i++)
            {
                Constants.Accreditation Accreditation = new Constants.Accreditation();

                // If there is no accreditation, then the row will not have an element for AccreditationBodyNameLbls in this row,
                // it will have a different xpath for this NONACCR element. So we will manually insert 'NONACCR' if that is the case
                Accreditation.BodyName = AccreditationRows[i].Exists(Bys.ActPreviewPage.AccreditationBodyNameLbls)
                    ? AccreditationBodyNameLbls[i].Text.Substring(2)
                    : "NONACCR";

                Accreditation.CreditAmount = double.Parse(AccreditationRows[i].FindElement(By.XPath("descendant::b")).Text);
                Accreditation.CreditUnit = AccreditationRows[i].FindElement(By.XPath("descendant::span")).Text;

                Accreditations.Add(Accreditation);
            }

            Accreditations.ToList().OrderBy(x => x.BodyName);

            Activity.Accreditations = Accreditations.ToList();

            // Set location if applicable
            if (Browser.Exists(Bys.ActOverviewPage.StartDateValueLbl))
            {
                addAndLoc = new Constants.AddressAndLocation();

                addAndLoc.Addr_Line_01 = StreetAddressLbl.Text;
                addAndLoc.StartDate = StartDateValueLbl.Text;
                addAndLoc.EndDate = EndDateValueLbl.Text;
            }

            Activity.AddressAndLocation = addAndLoc;

            return Activity;
        }

        /// <summary>
        /// Clicks on a user-specified activity on the Include These Activities tab, then waits for that activities Preview or Overview page to load
        /// </summary>
        /// <param name="activityName"></param>
        /// <returns></returns>
        public dynamic ClickActivity(string activityName)
        {
            ElemSet.Grid_ClickButtonOrLinkWithinRow(Browser, IncTheseActsTab_BundlesTbl, Bys.ActPreviewPage.IncTheseActsTab_BundlesTbl,
                activityName, "h4", activityName, "h4");

            // Wait until the page URL loads 
            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(91));
            wait.Until(Browser => { return Browser.Url.Contains("activity_overview?") || Browser.Url.Contains("activity?"); });

            // If this click takes us to the Activity Overview page
            if (Browser.Url.Contains("activity_overview"))
            {
                ActOverviewPage OP = new ActOverviewPage(Browser);
                OP.WaitForInitialize();
                Thread.Sleep(300);
                return OP;
            }

            // Else if this click takes us to the Preview page, we will first have wait for the activity title to appear. We can not wait for the
            // Page to load first, because the page we are already on is the same page. So wait for the new activities' title to appear on the 
            // page first
            else
            {
                Browser.WaitForElement(By.XPath("//h2[text()='Bundle - Access Code and Payment Required, One Child Required']"));
                ActPreviewPage FP = new ActPreviewPage(Browser);
                FP.WaitForInitialize();
                Thread.Sleep(300);
                return FP;
            }
        }

        #endregion methods: page specific



    }
}