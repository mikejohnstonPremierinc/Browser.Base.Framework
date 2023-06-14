using Browser.Core.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using LOG4NET = log4net.ILog;


namespace Mainpro.AppFramework
{
    public class CACCreditSummaryPage : MainproPage, IDisposable
    {
        #region constructors
        public CACCreditSummaryPage(IWebDriver driver) : base(driver)
        {
        }

        #endregion constructors

        #region properties

        private static readonly LOG4NET _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Keep track of the requests that I start so I can clean them up at the end.
        private List<string> activeRequests = new List<string>();

        public override string PageUrl { get { return "Default.aspx"; } }

        #endregion properties

        #region elements
        public IWebElement EnterCPDActBtn { get { return this.FindElement(Bys.CACCreditSummaryPage.EnterCPDActBtn); } }
        public IWebElement AppliedCreditsLbl { get { return this.FindElement(Bys.CACCreditSummaryPage.AppliedCreditsLbl); } }


        #endregion elements

        #region methods: per page



        public override void WaitForInitialize()
        {
           
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
            catch (Exception ex) { _log.ErrorFormat("Failed to dispose CreditSummaryPge", activeRequests.Count, ex); }
        }


        #endregion methods: per page

        #region methods: page specific

       

        /// <summary>
        /// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        /// depending on the button that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {


            if (buttonOrLinkElem.GetAttribute("text") == CACCPDActivitiesListTab.GetAttribute("text"))
                {
                buttonOrLinkElem.Click();
                CACCPDActivitiesList CCAL = new CACCPDActivitiesList(Browser);
                return CCAL;
                }
           
            else
            {
                throw new Exception("No button or link was found with your passed parameter");
            }

            //return null;
        }

      
        #endregion methods: page specific


    }
}
