using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using LOG4NET = log4net.ILog;
using System.Globalization;

namespace LMS.AppFramework
{
    public class ProfilePage : LMSPage, IDisposable
    {
        #region constructors
        public ProfilePage(IWebDriver driver) : base(driver)
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
        public IWebElement SaveBtn { get { return this.FindElement(Bys.ProfilePage.SaveBtn); } }

        
        public IWebElement ParticipantId { get { return this.FindElement(Bys.ProfilePage.ParticipantId); } }
        public IWebElement Backbtn { get { return this.FindElement(Bys.ProfilePage.Backbtn); } }

        public IWebElement FirstNameTxt { get { return this.FindElement(Bys.ProfilePage.FirstNameTxt); } }
        public IWebElement LastNameTxt { get { return this.FindElement(Bys.ProfilePage.LastNameTxt); } }
        public SelectElement StateSelElem { get { return new SelectElement(this.FindElement(Bys.ProfilePage.StateSelElem)); } }
          public IWebElement AreYouCHESNoRdo { get { return this.FindElement(Bys.ProfilePage.AreYouCHESNoRdo); } }
        public IWebElement AreYouCHESYesRdo { get { return this.FindElement(Bys.ProfilePage.AreYouCHESYesRdo); } }
        public IWebElement Address01Txt { get { return this.FindElement(Bys.ProfilePage.Address01Txt); } }
        public IWebElement CityTxt { get { return this.FindElement(Bys.ProfilePage.CityTxt); } }
        public IWebElement PostalCodeTxt { get { return this.FindElement(Bys.ProfilePage.PostalCodeTxt); } }
        public IWebElement EmailTxt { get { return this.FindElement(Bys.ProfilePage.EmailTxt); } }
        public IWebElement OrganiztionorCompanyTxt { get { return this.FindElement(Bys.ProfilePage.OrganiztionorCompanyTxt); } }
        public SelectElement CountrySelElem { get { return new SelectElement(this.FindElement(Bys.ProfilePage.CountrySelElem)); } }
        public SelectElement ProfessionSelElem { get { return new SelectElement(this.FindElement(Bys.ProfilePage.ProfessionSelElem)); } }







        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(90), Criteria.ProfilePage.PageReady);
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
            if (Browser.Exists(Bys.LMSPage.TermsOfServiceFormIAcceptBtn))
            {
                if (elem.GetAttribute("outerHTML") == TermsOfServiceFormIAcceptBtn.GetAttribute("outerHTML"))
                {
                    TermsOfServiceFormIAcceptBtn.Click(Browser);
                    Browser.WaitForElement(Bys.LMSPage.VerifyYourProfessionFormProfessionSelElem, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn))
            {
                if (elem.GetAttribute("outerHTML") == VerifyYourProfessionFormSubmitBtn.GetAttribute("outerHTML"))
                {
                    VerifyYourProfessionFormSubmitBtn.Click(Browser);
                    this.WaitUntilAll(Criteria.ProfilePage.FirstNameTxtVisible, Criteria.ProfilePage.BackBtnVisibleAndEnabled);
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
            if (Browser.Exists(Bys.LMSPage.VerifyYourProfessionFormProfessionSelElem))
            {
                if (selectElement.Options[1].Text == VerifyYourProfessionFormProfessionSelElem.Options[1].Text)
                {
                    VerifyYourProfessionFormProfessionSelElem.SelectByText(selection);

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

     

        #endregion methods: page specific



    }
}