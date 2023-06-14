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
    public class CACHoldingArea : MainproPage, IDisposable
    {
        #region constructors
        public CACHoldingArea(IWebDriver driver) : base(driver)
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

      
        #endregion elements

        #region methods: per page

        //public variables for the Create goal page
        public int GoalCount = 0;


        public override void WaitForInitialize()
        {
           // this.WaitUntil(TimeSpan.FromSeconds(120), Criteria.CreditSummaryPage.PageReady);
        }
        public void Wait(int time)
        {
            System.Threading.Thread.Sleep(time);
        }



        /// <summary>
        /// This function delets all of the
        /// </summary>

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
        /// 
        /// </summary>
        /// <param name="dropDownElem"> This is the dropdown element to click</param>
        /// <param name="textOfItem">The exact text of the item. If the item is located in a <b> tag, only send the bolded text of the item</b></param>
        public void SelectItemInCustomDropDown(IWebElement dropDownElem, string textOfItem)
        {
            IWebElement itemToSelectInDropdown = null;
            ElemSet.ScrollToElement(Browser, dropDownElem);

            dropDownElem.Click();

            Thread.Sleep(1500);
            string xpathStringForSpanTag = string.Format("//span[text()='{0}']", textOfItem);
            string xpathStringForBTag = string.Format("//b[text()='{0}']", textOfItem);
            string xpathStringForLiTag = string.Format("//li[text()='{0}']", textOfItem);
            if (Browser.FindElements(By.XPath(xpathStringForSpanTag)).Count > 0)
            {
                itemToSelectInDropdown = Browser.WaitForElement(By.XPath(xpathStringForSpanTag), ElementCriteria.IsEnabled);

            }
            else if (Browser.FindElements(By.XPath(xpathStringForBTag)).Count > 0)
            {

                itemToSelectInDropdown = Browser.WaitForElement(By.XPath(xpathStringForBTag), ElementCriteria.IsEnabled);
            }
            else if (Browser.FindElements(By.XPath(xpathStringForLiTag)).Count > 0)
            {

                itemToSelectInDropdown = Browser.WaitForElement(By.XPath(xpathStringForLiTag), ElementCriteria.IsEnabled);
            }

            itemToSelectInDropdown.Click();


        }


        /// <summary>
        /// Clicks the user-specified button or link and then waits for a window to close or open, or a page to load,
        /// depending on the button that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {


            if (buttonOrLinkElem.GetAttribute("id") == ReportsTab.GetAttribute("id"))
                {
                    buttonOrLinkElem.Click();
                    //Browser.WaitForElement(Bys.EnterACPDActivityPage.CategoryDrpDn, TimeSpan.FromSeconds(20), ElementCriteria.IsVisible, ElementCriteria.IsEnabled);
                    ReportsPage rp = new ReportsPage(Browser);
                    //rp.WaitForInitialize();
                    return rp;
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
