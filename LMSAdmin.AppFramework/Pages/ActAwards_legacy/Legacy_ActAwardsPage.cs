using Browser.Core.Framework;
using LMS.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using LOG4NET = log4net.ILog;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;
using OpenQA.Selenium.Interactions;
using LMSAdmin.AppFramework.HelperMethods;
using System.Globalization;

namespace LMSAdmin.AppFramework
{
    public class Legacy_ActAwardsPage : Page, IDisposable
    {
        #region constructors
        public Legacy_ActAwardsPage(IWebDriver driver) : base(driver)
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


        public IWebElement BackToAwardsLnk { get { return this.FindElement(Bys.Legacy_ActAwardsPage.BackToAwardsLnk); } }

        public IWebElement AwardNameTxt { get { return this.FindElement(Bys.Legacy_ActAwardsPage.AwardNameTxt); } }
        public IWebElement ScenariosTblFirstRow { get { return this.FindElement(Bys.Legacy_ActAwardsPage.ScenariosTblFirstRow); } }
        public IWebElement ScenariosTblBody { get { return this.FindElement(Bys.Legacy_ActAwardsPage.ScenariosTblBody); } }
        public IWebElement ScenariosTbl { get { return this.FindElement(Bys.Legacy_ActAwardsPage.ScenariosTbl); } }
        public IWebElement TemplatesTblFirstRow { get { return this.FindElement(Bys.Legacy_ActAwardsPage.TemplatesTblFirstRow); } }
        public IWebElement TemplatesTblBody { get { return this.FindElement(Bys.Legacy_ActAwardsPage.TemplatesTblBody); } }
        public IWebElement TemplatesTbl { get { return this.FindElement(Bys.Legacy_ActAwardsPage.TemplatesTbl); } }
        public IWebElement AwardsTblFirstRow { get { return this.FindElement(Bys.Legacy_ActAwardsPage.AwardsTblFirstRow); } }
        public IWebElement AwardsTbl { get { return this.FindElement(Bys.Legacy_ActAwardsPage.AwardsTbl); } }
        public IWebElement AddAwardLnk { get { return this.FindElement(Bys.Legacy_ActAwardsPage.AddAwardLnk); } }
        public IWebElement TemplateSaveBtn { get { return this.FindElement(Bys.Legacy_ActAwardsPage.TemplateSaveBtn); } }
        public IWebElement AddAwardSaveAndContBtn { get { return this.FindElement(Bys.Legacy_ActAwardsPage.AddAwardSaveAndContBtn); } }
        public IWebElement ScenarioSaveBtn { get { return this.FindElement(Bys.Legacy_ActAwardsPage.ScenarioSaveBtn); } }

        

        public SelectElement SelectTypeSelElem { get { return new SelectElement(this.FindElement(Bys.Legacy_ActAwardsPage.SelectTypeSelElem)); } }
        public SelectElement SelectEmailTempSelElem { get { return new SelectElement(this.FindElement(Bys.Legacy_ActAwardsPage.SelectEmailTempSelElem)); } }
        public SelectElement SelectTempLibrarySelElem { get { return new SelectElement(this.FindElement(Bys.Legacy_ActAwardsPage.SelectTempLibrarySelElem)); } }

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.Legacy_ActAwardsPage.PageReady);
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.Legacy_ActAwardsPage.AddAwardLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddAwardLnk.GetAttribute("outerHTML"))
                {
                    AddAwardLnk.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAwardsPage.SelectTypeSelElem, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAwardsPage.AddAwardSaveAndContBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddAwardSaveAndContBtn.GetAttribute("outerHTML"))
                {
                    AddAwardSaveAndContBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAwardsPage.ScenariosTblBody, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAwardsPage.ScenarioSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ScenarioSaveBtn.GetAttribute("outerHTML"))
                {
                    ScenarioSaveBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAwardsPage.TemplateSaveBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAwardsPage.TemplateSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == TemplateSaveBtn.GetAttribute("outerHTML"))
                {
                    TemplateSaveBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAwardsPage.ChangesSavedLbl, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAwardsPage.BackToAwardsLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == BackToAwardsLnk.GetAttribute("outerHTML"))
                {
                    BackToAwardsLnk.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Legacy_ActAwardsPage.AwardsTblBody, ElementCriteria.IsVisible);
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                buttonOrLinkElem.GetAttribute("innerText")));
        }


        /// <summary>
        /// Selects an item in a Select Element, and then waits for a window to close or open, or a page to load, depending on the element that was 
        /// selected
        /// </summary>
        /// <param name="elem">The select element</param>
        public dynamic SelectAndWait(SelectElement Elem, string selection)
        {
            if (Browser.Exists(Bys.Legacy_ActAwardsPage.SelectTempLibrarySelElem))
            {
                if (Elem.Options[1].Text == SelectTempLibrarySelElem.Options[1].Text)
                {
                    SelectTempLibrarySelElem.SelectByText(selection);
                    Browser.WaitForElement(Bys.Legacy_ActAwardsPage.TemplatesTblFirstRow);
                    return null;
                }
            }

            if (Browser.Exists(Bys.Legacy_ActAwardsPage.SelectTypeSelElem))
            {
                if (Elem.Options[1].Text == SelectTypeSelElem.Options[1].Text)
                {
                    SelectTypeSelElem.SelectByText(selection);
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element"));
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

        /// <summary>
        /// Clicks Add Award link, select the type, enter the name, select the email template and template library, then select the template, 
        /// then click Continute. 
        /// Check all scenario checkboxes so this award shows for all scenarios (in the future, we can code this so that we can choose specific
        /// scenarios), then click Save.
        /// Click Save on the last page to save the award
        /// </summary>
        /// <param name="awardType">(Optional). If null, then it will pick 'Certificate'</param>
        /// <param name="awardName">(Optional). If null, then it will generate a random string</param>
        /// <param name="templateLibrary">(Optional). If null, then it will pick the second item in the list</param>
        /// <param name="templateName">(Optional). If null, then it will pick the first item in the table</param>
        public Award AddAward(string awardType = null, string awardName = null, string templateLibrary = null, string templateName = null)
        {
            ClickAndWait(AddAwardLnk);

            // Populate the properties. If null, then generate random string or set default value
            awardType = string.IsNullOrEmpty(awardType) ? "Certificate" : awardType;
            string timeStamp = string.Format("{0}", DateTime.UtcNow.ToString("MM-dd-yy HH:mm:ss.fff", CultureInfo.InvariantCulture));
            awardName = string.IsNullOrEmpty(awardName) ? string.Format("AutoAward {0}", timeStamp) : awardName;
            //emailTemplate = string.IsNullOrEmpty(emailTemplate) ? SelectTempLibrarySelElem.Options[1].Text : emailTemplate;
            templateLibrary = string.IsNullOrEmpty(templateLibrary) ? SelectTempLibrarySelElem.Options[1].Text : templateLibrary;

            // Fill required fields
            SelectAndWait(SelectTypeSelElem, awardType);
            AwardNameTxt.SendKeys(awardName);
            SelectAndWait(SelectTempLibrarySelElem, templateLibrary);

            // If the user didnt specify a template, then just choose the first inde
            IWebElement templateRow = null;
            if (templateName.IsNullOrEmpty())
            {
                templateRow = ElemGet_LMSAdmin.Grid_GetRowByRowIndex(TemplatesTblBody, Bys.Legacy_ActAwardsPage.TemplatesTblFirstRow, 1);
            }
            else
            {
                templateRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(TemplatesTbl, Bys.Legacy_ActAwardsPage.TemplatesTblFirstRow, templateName, "td");
            }
            ElemSet_LMSAdmin.Grid_ClickElementWithoutTextInsideRow(templateRow, "input", "Radio");

            ClickAndWait(AddAwardSaveAndContBtn);

            // Check all scenario checkboxes
            IList<IWebElement> scenarioRows = ElemGet_LMSAdmin.Grid_GetRows(ScenariosTblBody, Bys.Legacy_ActAwardsPage.ScenariosTblFirstRow);
            foreach (var scenarioRow in scenarioRows)
            {
                ElemSet_LMSAdmin.Grid_TickCheckBox(scenarioRow, 1);
            }

            // Get the accreditation body. NOTE: For now, we are just choosing the first scenarios accreditation body. I ran out of time developing this.
            // If we need to revisit, we can refactor code, get unique accreditation bodies per scenario, so this logic would then
            // change here and at CMEHelperMethods.AddAward. My thoughts now is that we can maybe use a query to do this. So at the end of this
            // method will will return NULL in the scenario_accreditationBody (or even just remove that parameter totally from the Award object). Then
            // back at CMEHelperMethods.AddAward we can create a query that retreives the accreditation body based on sending it the given 
            // accreditingBody from the Accreditation object. Put that into a loop per scenario, and insert it into each scenario's award
            string scenario_AccreditationBody = ElemGet.Grid_GetCellTextByRowIndexAndColIndex(Browser, ScenariosTblBody, Bys.Legacy_ActAwardsPage.ScenariosTblFirstRow, 1, 2);

            ClickAndWait(ScenarioSaveBtn);
            ClickAndWait(TemplateSaveBtn);

            ClickAndWait(BackToAwardsLnk);

            return new Award(awardType, awardName, templateLibrary, scenario_AccreditationBody);
        }

        #endregion methods: repeated per page

        #region methods: page specific




        #endregion methods: page specific



    }
}