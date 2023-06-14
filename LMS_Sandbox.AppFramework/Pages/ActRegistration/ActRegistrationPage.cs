using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMS.AppFramework.LMSHelperMethods;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LMS.AppFramework
{
    public class ActRegistrationPage : LMSPage, IDisposable
    {
        #region constructors
        public ActRegistrationPage(IWebDriver driver) : base(driver)
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
        public IWebElement DidThisProgramMeetRdo { get { return this.FindElement(Bys.ActRegistrationPage.DidThisProgramMeetRdo); } }
        public IWebElement IsThereARegForm { get { return this.FindElement(Bys.ActRegistrationPage.IsThereARegForm); } }
        public IWebElement PIM_MemberStatus_MemberRdo { get { return this.FindElement(Bys.ActRegistrationPage.PIM_MemberStatus_MemberRdo); } }
        public IWebElement AreYouCHESNoRdo { get { return this.FindElement(Bys.ActRegistrationPage.AreYouCHESNoRdo); } }
        public SelectElement CountrySelElem { get { return new SelectElement(this.FindElement(Bys.ActRegistrationPage.CountrySelElem)); } }
        public IWebElement CountrySelElemBtn { get { return this.FindElement(Bys.ActRegistrationPage.CountrySelElemBtn); } }

        public IWebElement FirstNameTxt { get { return this.FindElement(Bys.ActRegistrationPage.FirstNameTxt); } }
        public IWebElement MiddleInitialTxt { get { return this.FindElement(Bys.ActRegistrationPage.MiddleInitialTxt); } }
        public IWebElement LastNameTxt { get { return this.FindElement(Bys.ActRegistrationPage.LastNameTxt); } }
        public IWebElement AddLine1Txt { get { return this.FindElement(Bys.ActRegistrationPage.AddLine1Txt); } }
        public IWebElement CityTxt { get { return this.FindElement(Bys.ActRegistrationPage.CityTxt); } }
        public IWebElement PostalCodeTxt { get { return this.FindElement(Bys.ActRegistrationPage.PostalCodeTxt); } }
        public IWebElement OrganiztionorCompanyTxt { get { return this.FindElement(Bys.ActRegistrationPage.OrganiztionorCompanyTxt); } }
        public SelectElement ProfessionSelElem { get { return new SelectElement(this.FindElement(Bys.ActRegistrationPage.ProfessionSelElem)); } }
        public IWebElement ProfessionSelElemBtn { get { return this.FindElement(Bys.ActRegistrationPage.ProfessionSelElemBtn); } }

        public SelectElement SecondaryProfessionSelElem { get { return new SelectElement(this.FindElement(Bys.ActRegistrationPage.SecondaryProfessionSelElem)); } }
        public IWebElement SecondaryProfessionSelElemBtn { get { return this.FindElement(Bys.ActRegistrationPage.SecondaryProfessionSelElemBtn); } }
        public SelectElement StateProvinceSelElem { get { return new SelectElement(this.FindElement(Bys.ActRegistrationPage.StateProvinceSelElem)); } }
        public IWebElement StateProvinceSelElemBtn { get { return this.FindElement(Bys.ActRegistrationPage.StateProvinceSelElemBtn); } }
        public IWebElement ParticipantIDTxt { get { return this.FindElement(Bys.ActRegistrationPage.ParticipantIDTxt); } }
        public IWebElement EmailTxt { get { return this.FindElement(Bys.ActRegistrationPage.EmailTxt); } }
        public IWebElement RegisterBtn { get { return this.FindElement(Bys.ActRegistrationPage.RegisterBtn); } }
        public SelectElement GenderSelElem { get { return new SelectElement(this.FindElement(Bys.ActRegistrationPage.GenderSelElem)); } }
        public IWebElement GenderSelElemBtn { get { return this.FindElement(Bys.ActRegistrationPage.GenderSelElemBtn); } }
        public IList<IWebElement> SelectCreditTypesChks { get { return this.FindElements(Bys.ActRegistrationPage.SelectCreditTypesChks); } }
        public IList<IWebElement> EthnicityChks { get { return this.FindElements(Bys.ActRegistrationPage.EthnicityChks); } }


        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ActRegistrationPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));

            // Sometimes this button appears on this page after you click the Resume/Register button on the Preview page
            if (Browser.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn))
            {
                ClickAndWait(VerifyYourProfessionFormSubmitBtn);
            }
        }

        /// <summary>
        /// Clicks the user-specified element and then waits for a window to close or open, or a page to load,
        /// depending on the element that was clicked
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.ActRegistrationPage.AreYouCHESNoRdo))
            {
                if (elem.GetAttribute("outerHTML") == AreYouCHESNoRdo.GetAttribute("outerHTML"))
                {
                    AreYouCHESNoRdo.ClickJS(Browser);
                    //this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationVisible); Doesnt work in FF and IE, so just pausing instead
                    Thread.Sleep(1000);
                    Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                    return null;
                }
            }

            if (Browser.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn))
            {
                if (elem.Text == VerifyYourProfessionFormSubmitBtn.Text)
                {
                    VerifyYourProfessionFormSubmitBtn.ClickJS(Browser);
                    try
                    {
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                        this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.ActRegistrationPage.VerifyYourProfessionFormSubmitBtnNotExists);
                        Thread.Sleep(1000);
                    }
                    catch
                    {
                        VerifyYourProfessionFormSubmitBtn.ClickJS(Browser);
                        Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
                        this.WaitUntilAll(TimeSpan.FromSeconds(60), Criteria.ActRegistrationPage.VerifyYourProfessionFormSubmitBtnNotExists);
                        Thread.Sleep(1000);
                    }
                    // This verify your profession button showed up in front of the payment page once. So
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.RegisterBtn))
            {
                if (elem.Text == RegisterBtn.Text)
                {
                    // For some reason, we have to click this button twice for the application to advance. I cant reproduce this manually,
                    // so I am just going to add a Try Catch
                    // Using javascript click here for the following reason. When we use a regular click, IE then doesnt load 
                    // the page fully for some reason. This is not reproducable manually
                    RegisterBtn.Click(Browser);
                    try
                    {
                        var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(120));
                        wait.Until(browser =>
                        {
                            return !browser.Url.Contains(Constants.PageURLs.Activity_Registration.GetDescription().ToLower());
                        });
                    }
                    catch (Exception)
                    {
                        RegisterBtn.Click(Browser);
                        var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(120));
                        wait.Until(browser =>
                        {
                            return !browser.Url.Contains(Constants.PageURLs.Activity_Registration.GetDescription().ToLower());
                        });
                    }

                    return Help.WaitForNextPage(Browser, Constants.PageURLs.Activity_Registration);
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                elem.GetAttribute("innerText")));
        }

        /// <summary>
        /// Selects an item from a user-specified select element, then waits for a criteria to load fully
        /// </summary>
        /// <param name="selectElement">The select element to manipulate</param>
        /// <param name="selection">The exact text you want to choose from the item in the select elements</param>
        /// <param name="selElemBtn">Send the button version of the Select Element only if you encounter an issue where there are 2 Select Elements with the same exact options on the same page</param>
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string selection, IWebElement selElemBtn = null)
        {
            if (Browser.Exists(Bys.ActRegistrationPage.ProfessionSelElem))
            {
                if (selectElement.Options[3].Text == ProfessionSelElem.Options[3].Text
                    && selElemBtn.Text == ProfessionSelElemBtn.Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, ProfessionSelElemBtn, selection);
                    }
                    else
                    {
                        ProfessionSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.SecondaryProfessionSelElem))
            {
                if (selectElement.Options[3].Text == SecondaryProfessionSelElem.Options[3].Text
                    && selElemBtn.Text == SecondaryProfessionSelElemBtn.Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, SecondaryProfessionSelElemBtn, selection);
                    }
                    else
                    {
                        SecondaryProfessionSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.CountrySelElem))
            {
                if (selectElement.Options[3].Text == CountrySelElem.Options[3].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, CountrySelElemBtn, selection);
                    }
                    else
                    {
                        CountrySelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.StateProvinceSelElem))
            {
                if (selectElement.Options[3].Text == StateProvinceSelElem.Options[3].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, StateProvinceSelElemBtn, selection);
                    }
                    else
                    {
                        StateProvinceSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.GenderSelElem))
            {
                if (selectElement.Options[3].Text == GenderSelElem.Options[3].Text)
                {
                    if (Browser.MobileEnabled() || Browser.GetCapabilities().GetCapability("browserName").ToString() == BrowserNames.Firefox)
                    {
                        ElemSet.DropdownMulti_Fireball_SelectByText(Browser, GenderSelElemBtn, selection);
                    }
                    else
                    {
                        GenderSelElem.SelectByText(selection);
                    }
                    this.WaitForInitialize();
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element."));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            try { activeRequests.Clear(); }
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose ActivityRegistrationPage", activeRequests.Count, ex); }
        }

        #endregion methods: repeated per page

        #region methods: page specific

        /// <summary>
        /// Early stages of this method. Right now, we are just going to click the required CHES radio button, then click Submit
        /// </summary>
        public dynamic RegisterForActivity(string userName)
        {
            if (Browser.Exists(Bys.ActRegistrationPage.ParticipantIDTxt, ElementCriteria.IsEnabled))
            {
                if (ParticipantIDTxt.GetAttribute("value") == "")
                {
                    ParticipantIDTxt.SendKeys(DataUtils.GetRandomString(5));
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.FirstNameTxt, ElementCriteria.IsEnabled))
            {
                if (FirstNameTxt.GetAttribute("value") == "")
                {
                    FirstNameTxt.SendKeys(DataUtils.GetRandomString(5));
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.MiddleInitialTxt, ElementCriteria.IsEnabled))
            {
                if (MiddleInitialTxt.GetAttribute("value") == "")
                {
                    MiddleInitialTxt.SendKeys(DataUtils.GetRandomString(5));
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.LastNameTxt, ElementCriteria.IsEnabled))
            {
                if (LastNameTxt.GetAttribute("value") == "")
                {
                    LastNameTxt.SendKeys(DataUtils.GetRandomString(5));
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.AreYouCHESNoRdo, ElementCriteria.IsEnabled))
            {
                ClickAndWait(AreYouCHESNoRdo);
            }

            if (Browser.Exists(Bys.ActRegistrationPage.AddLine1Txt, ElementCriteria.IsEnabled))
            {
                if (AddLine1Txt.GetAttribute("value") == "")
                {
                    AddLine1Txt.SendKeys(DataUtils.GetRandomString(5));
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.CityTxt, ElementCriteria.IsEnabled))
            {
                if (CityTxt.GetAttribute("value") == "")
                {
                    CityTxt.SendKeys(DataUtils.GetRandomString(5));
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.StateProvinceSelElem, ElementCriteria.IsEnabled))
            {
                SelectAndWait(StateProvinceSelElem, StateProvinceSelElem.Options[3].Text);
            }

            if (Browser.Exists(Bys.ActRegistrationPage.PostalCodeTxt, ElementCriteria.IsEnabled))
            {
                if (PostalCodeTxt.GetAttribute("value") == "")
                {
                    PostalCodeTxt.SendKeys(DataUtils.GetRandomString(5));
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.CountrySelElem, ElementCriteria.IsEnabled))
            {
                SelectAndWait(CountrySelElem, CountrySelElem.Options[3].Text);
            }

            if (Browser.Exists(Bys.ActRegistrationPage.EmailTxt, ElementCriteria.IsEnabled))
            {
                if (EmailTxt.GetAttribute("value") == "")
                {
                    string currentDate = string.Format("{0}", DateTime.Now.ToString("Mdyyyy", CultureInfo.InvariantCulture));
                    string email = string.Format("TA_{0}_{1}@mailinator.com", currentDate, DataUtils.GetRandomString(3));
                    EmailTxt.SendKeys(email);
                }

            }

            if (Browser.Exists(Bys.ActRegistrationPage.OrganiztionorCompanyTxt, ElementCriteria.IsEnabled))
            {
                if (OrganiztionorCompanyTxt.GetAttribute("value") == "")
                {
                    OrganiztionorCompanyTxt.SendKeys(DataUtils.GetRandomString(5));
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.ProfessionSelElem, ElementCriteria.IsEnabled))
            {
                string profession = null;

                // If this method is called while already logged in and previously registered, then that means it was called from one of the
                // wrapper methods. In this case, the user already registered, but the registration page comes up due to activities requiring
                // the user to fill out the form again, or the user was created from the API and certain fields were not filled in. If this
                // is the case, then if we are using one of our static hard-coded users, then make sure we get the correct profession name 
                // of that static user
                foreach (Constants.Profession prof in (Constants.Profession[])Enum.GetValues(typeof(Constants.Profession)))
                {
                    string professionWithoutWhiteSpace = Regex.Replace(prof.GetDescription(), @"\s+", "");

                    if (userName.Contains(professionWithoutWhiteSpace))
                    {                       
                        profession = prof.GetDescription();
                        break;
                    }
                }

                SelectAndWait(ProfessionSelElem, profession, ProfessionSelElemBtn);
            }

            if (Browser.Exists(Bys.ActRegistrationPage.SecondaryProfessionSelElem, ElementCriteria.IsEnabled))
            {
                SelectAndWait(SecondaryProfessionSelElem, SecondaryProfessionSelElem.Options[3].Text, SecondaryProfessionSelElemBtn);
            }

            if (Browser.Exists(Bys.ActRegistrationPage.GenderSelElem, ElementCriteria.IsEnabled))
            {
                SelectAndWait(GenderSelElem, GenderSelElem.Options[3].Text);
            }

            if (Browser.Exists(Bys.ActRegistrationPage.SelectCreditTypesChks, ElementCriteria.IsEnabled))
            {
                for (int i = 0; i < 4; i++)
                {
                    SelectCreditTypesChks[i].Click();
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.EthnicityChks, ElementCriteria.IsEnabled))
            {
                for (int i = 0; i < 2; i++)
                {
                    EthnicityChks[i].Click();
                }
            }

            if (Browser.Exists(Bys.ActRegistrationPage.PIM_MemberStatus_MemberRdo, ElementCriteria.IsEnabled))
            {
                PIM_MemberStatus_MemberRdo.Click();
                // Need a pause here
                Thread.Sleep(1000);
            }

            if (Browser.Exists(Bys.ActRegistrationPage.PIM_MemberStatus_MemberRdo, ElementCriteria.IsEnabled))
            {
                PIM_MemberStatus_MemberRdo.Click();
                // Need a pause here
                Thread.Sleep(1000);
            }

            if (Browser.Exists(Bys.ActRegistrationPage.PIM_MemberStatus_MemberRdo, ElementCriteria.IsEnabled))
            {
                PIM_MemberStatus_MemberRdo.Click();
                // Need a pause here
                Thread.Sleep(1000);
            }

            return ClickAndWait(RegisterBtn);
        }


        #endregion methods: page specific



    }
}