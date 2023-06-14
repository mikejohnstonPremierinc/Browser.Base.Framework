using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using LOG4NET = log4net.ILog;
using System.Globalization;
using System.Linq;

namespace LMS.AppFramework
{
    public class RegistrationPage : LMSPage, IDisposable
    {
        #region constructors
        public RegistrationPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that WE start so we can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "registration.aspx"; } }

        #endregion properties

        #region elements

        
        public IWebElement ParticipantId { get { return this.FindElement(Bys.RegistrationPage.ParticipantId); } }

        public IWebElement FirstNameTxt { get { return this.FindElement(Bys.RegistrationPage.FirstNameTxt); } }
        public IWebElement LastNameTxt { get { return this.FindElement(Bys.RegistrationPage.LastNameTxt); } }
        public SelectElement StateSelElem { get { return new SelectElement(this.FindElement(Bys.RegistrationPage.StateSelElem)); } }
        public IWebElement ContinueBtn { get { return this.FindElement(Bys.RegistrationPage.ContinueBtn); } }
        public IWebElement AreYouCHESNoRdo { get { return this.FindElement(Bys.RegistrationPage.AreYouCHESNoRdo); } }
        public IWebElement AreYouCHESYesRdo { get { return this.FindElement(Bys.RegistrationPage.AreYouCHESYesRdo); } }
        public IWebElement Address01Txt { get { return this.FindElement(Bys.RegistrationPage.Address01Txt); } }
        public IWebElement CityTxt { get { return this.FindElement(Bys.RegistrationPage.CityTxt); } }
        public IWebElement PostalCodeTxt { get { return this.FindElement(Bys.RegistrationPage.PostalCodeTxt); } }
        public IWebElement EmailTxt { get { return this.FindElement(Bys.RegistrationPage.EmailTxt); } }
        public IWebElement OrganiztionorCompanyTxt { get { return this.FindElement(Bys.RegistrationPage.OrganiztionorCompanyTxt); } }
        public SelectElement CountrySelElem { get { return new SelectElement(this.FindElement(Bys.RegistrationPage.CountrySelElem)); } }
        public SelectElement ProfessionSelElem { get { return new SelectElement(this.FindElement(Bys.RegistrationPage.ProfessionSelElem)); } }
        public IWebElement PasswordTxt { get { return this.FindElement(Bys.RegistrationPage.PasswordTxt); } }
        public IWebElement UsernameTxt { get { return this.FindElement(Bys.RegistrationPage.UsernameTxt); } }
        public IWebElement ConfirmPasswordTxt { get { return this.FindElement(Bys.RegistrationPage.ConfirmPasswordTxt); } }
        public IWebElement SecurityAnswerTxt { get { return this.FindElement(Bys.RegistrationPage.SecurityAnswerTxt); } }
        public IWebElement IAgreeChk { get { return this.FindElement(Bys.RegistrationPage.IAgreeChk); } }
        public SelectElement YourPrimFuncRoleSelElem { get { return new SelectElement(this.FindElement(Bys.RegistrationPage.YourPrimFuncRoleSelElem)); } }
        public IWebElement HispanicLatinoNoRdo { get { return this.FindElement(Bys.RegistrationPage.HispanicLatinoNoRdo); } }
        public SelectElement PrimaryPositionSelElem { get { return new SelectElement(this.FindElement(Bys.RegistrationPage.PrimaryPositionSelElem)); } }
        public SelectElement PrimarySpecialtySelElem { get { return new SelectElement(this.FindElement(Bys.RegistrationPage.PrimarySpecialtySelElem)); } }
        public SelectElement PrimaryWorkSettingSelElem { get { return new SelectElement(this.FindElement(Bys.RegistrationPage.PrimaryWorkSettingSelElem)); } }
        public IWebElement AreYouOncologyNurseYesRdo { get { return this.FindElement(Bys.RegistrationPage.AreYouOncologyNurseYesRdo); } }




        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.RegistrationPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(90));
        }

        /// <summary>
        /// Clicks the user-specified button, link, tab, etc. and then waits for a window/element to close or open, or a page to load,
        /// depending on the element that was clicked. Once the Wait Criteria is satisfied, the test continues, and the method returns
        /// either a new Page class instance or nothing at all (hence the 'dynamic' return type). For a thorough explanation of how this
        /// type of method works, and how to use this method <see cref="LMSPage.ClickAndWaitBasePage(IWebElement)"/>
        /// </summary>
        /// <param name="elem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement elem)
        {
            if (Browser.Exists(Bys.RegistrationPage.AreYouCHESNoRdo))
            {
                if (elem.GetAttribute("outerHTML") == AreYouCHESNoRdo.GetAttribute("outerHTML"))
                {
                    AreYouCHESNoRdo.Click(Browser);
                    //this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationVisible); Doesnt work in FF and IE, so just pausing instead
                    Thread.Sleep(500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.HispanicLatinoNoRdo))
            {
                if (elem.GetAttribute("outerHTML") == HispanicLatinoNoRdo.GetAttribute("outerHTML"))
                {
                    HispanicLatinoNoRdo.Click(Browser);
                    Thread.Sleep(500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.ContinueBtn))
            {
                if (elem.GetAttribute("outerHTML") == ContinueBtn.GetAttribute("outerHTML"))
                {
                    ContinueBtn.ClickJS(Browser);
                    try
                    {
                        Browser.WaitForElement(Bys.RegistrationPage.PasswordTxt, ElementCriteria.IsVisible);
                    }
                    catch
                    {
                        ContinueBtn.ClickJS(Browser);
                        Browser.WaitForElement(Bys.RegistrationPage.PasswordTxt, ElementCriteria.IsVisible);
                    }

                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.AreYouOncologyNurseYesRdo))
            {
                if (elem.GetAttribute("outerHTML") == AreYouOncologyNurseYesRdo.GetAttribute("outerHTML"))
                {
                    AreYouOncologyNurseYesRdo.Click(Browser);
                    Thread.Sleep(500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
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
        /// <returns></returns>
        public dynamic SelectAndWait(SelectElement selectElement, string selection)
        {
            if (Browser.Exists(Bys.RegistrationPage.ProfessionSelElem))
            {
                if (selectElement.Options[1].Text == ProfessionSelElem.Options[1].Text)
                {
                    ProfessionSelElem.SelectByText(selection);
                    Thread.Sleep(0500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.CountrySelElem))
            {
                if (selectElement.Options[1].Text == CountrySelElem.Options[1].Text)
                {
                    CountrySelElem.SelectByText(selection);
                    Thread.Sleep(0500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.StateSelElem))
            {
                if (selectElement.Options[1].Text == StateSelElem.Options[1].Text)
                {
                    StateSelElem.SelectByText(selection);
                    Thread.Sleep(0500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.YourPrimFuncRoleSelElem))
            {
                if (selectElement.Options[1].Text == YourPrimFuncRoleSelElem.Options[1].Text)
                {
                    YourPrimFuncRoleSelElem.SelectByText(selection);
                    Thread.Sleep(0500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.PrimaryPositionSelElem))
            {
                if (selectElement.Options[1].Text == PrimaryPositionSelElem.Options[1].Text)
                {
                    PrimaryPositionSelElem.SelectByText(selection);
                    Thread.Sleep(0500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.PrimarySpecialtySelElem))
            {
                if (selectElement.Options[1].Text == PrimarySpecialtySelElem.Options[1].Text)
                {
                    PrimarySpecialtySelElem.SelectByText(selection);
                    Thread.Sleep(0500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.RegistrationPage.PrimaryWorkSettingSelElem))
            {
                if (selectElement.Options[1].Text == PrimaryWorkSettingSelElem.Options[1].Text)
                {
                    PrimaryWorkSettingSelElem.SelectByText(selection);
                    Thread.Sleep(0500);
                    this.WaitUntil(Criteria.RegistrationPage.LoadIcon_RegistrationNotVisible);
                    return null;
                }
            }

            return null;
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
        /// Fills in all required fields on the Registration form, then clicks Continue and fills in the Password and clicks I Agree,
        /// and finally clicks Continue and waits for the Home page
        /// </summary>
        /// <param name="email">(Optional). Will choose an email for you if blank</param>
        /// <param name="username">(Optional). Will choose an (username) for you if blank</param>
        /// <param name="profession"><see cref="Constants_UAMS.Profession"/>(Optional). Will choose a profession for you if blank</param>
        /// <returns></returns>
        public HomePage RegisterUser(string email = null, string username = null, Constants.Profession? profession = null)
        {
            FirstNameTxt.SendKeys(DataUtils.GetRandomString(5));
            LastNameTxt.SendKeys(DataUtils.GetRandomString(7));

            Address01Txt.SendKeys(DataUtils.GetRandomString(12));
            CityTxt.SendKeys(DataUtils.GetRandomString(8));
            SelectAndWait(StateSelElem, StateSelElem.Options[1].Text);

            PostalCodeTxt.SendKeys(DataUtils.GetRandomString(5));
            SelectAndWait(CountrySelElem, CountrySelElem.Options[1].Text);

            // If the portal only allows emails as username, then warn the tester that username and email must match if he sent username that 
            // is different from email
            if (Constants.SitesWith_EmailAndUsernameMustBeTheSame.Any(s => Browser.Url.Contains(s)))
            {
                if (!username.IsNullOrEmpty() && email != username)
                {
                    throw new Exception("This portal uses your email for a username. Remove your username parameter from your method call");
                }
            }

            // Set the email
            string currentDate = string.Format("{0}", DateTime.Now.ToString("Mdyyyy", CultureInfo.InvariantCulture));
            email = string.IsNullOrEmpty(email) ? string.Format("TA_{0}_{1}@mailinator.com", currentDate, DataUtils.GetRandomString(6)) : email;
            EmailTxt.SendKeys(email);

            if (!profession.HasValue)
            {
                SelectAndWait(ProfessionSelElem, ProfessionSelElem.Options[2].Text);
            }
            else
            {
                SelectAndWait(ProfessionSelElem, profession.GetDescription());
            }

            OrganiztionorCompanyTxt.SendKeysIfElemExists(Browser, Bys.RegistrationPage.OrganiztionorCompanyTxt);

            // UAMS specific elements:
            // For some reason, right now, the CR environment does not show the "Credit Types Needed" radio buttons
            if (Browser.Exists(By.XPath("//label[text()='AMA PRA Category 1']")))
            {
                ElemSet.RdoBtn_ClickByText(Browser, "AMA PRA Category 1");
            }

            if (Browser.Exists(By.XPath("//label[text()='Asian']")))
            {
                ElemSet.RdoBtn_ClickByText(Browser, "Asian");
            }

            // Only appears on this form in CR
            if (Browser.Exists(Bys.RegistrationPage.HispanicLatinoNoRdo))
            {
                ClickAndWait(HispanicLatinoNoRdo);
            }

            // Only appears on this form in CR
            if (Browser.Exists(Bys.RegistrationPage.AreYouCHESNoRdo))
            {
                ClickAndWait(AreYouCHESNoRdo);
            }

            // This only exists on CR
            if (Browser.Exists(Bys.RegistrationPage.YourPrimFuncRoleSelElem))
            {
                SelectAndWait(YourPrimFuncRoleSelElem, YourPrimFuncRoleSelElem.Options[1].Text);
            }

            // ONS specific elements
            ParticipantId.SendKeysIfElemExists(Browser, Bys.RegistrationPage.ParticipantId);

            if (Browser.Exists(Bys.RegistrationPage.AreYouOncologyNurseYesRdo))
            {
                ClickAndWait(AreYouOncologyNurseYesRdo);
            }

            if (Browser.Exists(Bys.RegistrationPage.PrimaryPositionSelElem))
            {
                SelectAndWait(PrimaryPositionSelElem, PrimaryPositionSelElem.Options[1].Text);
            }

            if (Browser.Exists(Bys.RegistrationPage.PrimarySpecialtySelElem))
            {
                SelectAndWait(PrimarySpecialtySelElem, PrimarySpecialtySelElem.Options[1].Text);
            }

            if (Browser.Exists(Bys.RegistrationPage.PrimaryWorkSettingSelElem))
            {
                SelectAndWait(PrimaryWorkSettingSelElem, PrimaryWorkSettingSelElem.Options[1].Text);
            }

            ClickAndWait(ContinueBtn);

            // If the portal does not allow emails as username, then warn the tester if he sent an email for username
            if (Constants.SitesWith_UsernameCanNotContainEmail.Any(s => Browser.Url.Contains(s)))
            {
                if (username.Contains("@"))
                {
                    throw new Exception("This portal does not accept emails for usernames. Modify your username so it does not include " +
                        "special characters");
                }
            }

            UsernameTxt.SendKeysIfElemExists(Browser, Bys.RegistrationPage.UsernameTxt, username);
            PasswordTxt.SendKeys("test");
            ConfirmPasswordTxt.SendKeys("test");
            SecurityAnswerTxt.SendKeys("automation");
            IAgreeChk.Click(Browser);

            ContinueBtn.Click(Browser);

            // Temporary code for ONS. Next sprint ONS will properly redirect to fireball page. For now, it redirects to legacy community page
            // So wait for legacy page, then go to fireball explicitly. Remove this next sprint
            if (GetSiteCodeFromURL() == "onslt")
            {
                Browser.WaitForElement(By.XPath("//h2[text()='Curriculum']"));
                Navigation.GoToHomePage(Browser, Constants.SiteCodes.ONSLT);
            }

            HomePage Page = new HomePage(Browser);
            Page.WaitForInitialize();
            return Page;
        }

        #endregion methods: page specific



    }
}