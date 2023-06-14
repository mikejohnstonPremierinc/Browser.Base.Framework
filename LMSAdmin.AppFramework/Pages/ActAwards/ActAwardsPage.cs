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
    public class ActAwardsPage : Page, IDisposable
    {
        #region constructors
        public ActAwardsPage(IWebDriver driver) : base(driver)
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


        public IWebElement BackToAwardsLnk { get { return this.FindElement(Bys.ActAwardsPage.BackToAwardsLnk); } }

        public IWebElement AwardNameTxt { get { return this.FindElement(Bys.ActAwardsPage.AwardNameTxt); } }
        public IWebElement TemplateNameTxt { get { return this.FindElement(Bys.ActAwardsPage.TemplateNameTxt); } }
        public IWebElement ScenariosTblFirstRow { get { return this.FindElement(Bys.ActAwardsPage.ScenariosTblFirstRow); } }
        public IWebElement ScenariosTblBody { get { return this.FindElement(Bys.ActAwardsPage.ScenariosTblBody); } }
        public IWebElement ScenariosTbl { get { return this.FindElement(Bys.ActAwardsPage.ScenariosTbl); } }
        public IWebElement TemplatesTblFirstRow { get { return this.FindElement(Bys.ActAwardsPage.TemplatesTblFirstRow); } }
        public IWebElement TemplatesTblBody { get { return this.FindElement(Bys.ActAwardsPage.TemplatesTblBody); } }
        public IWebElement TemplatesTbl { get { return this.FindElement(Bys.ActAwardsPage.TemplatesTbl); } }
        public IWebElement AwardsTblFirstRow { get { return this.FindElement(Bys.ActAwardsPage.AwardsTblFirstRow); } }
        public IWebElement AwardsTbl { get { return this.FindElement(Bys.ActAwardsPage.AwardsTbl); } }
        public IWebElement AddAwardBtn { get { return this.FindElement(Bys.ActAwardsPage.AddAwardBtn); } }
        public IWebElement SaveAwardBtn { get { return this.FindElement(Bys.ActAwardsPage.SaveAwardBtn); } }
        public IWebElement CloseAwardBtn { get { return this.FindElement(Bys.ActAwardsPage.CloseAwardBtn); } }
        public IWebElement PortraitTypeRdoBtn { get { return this.FindElement(Bys.ActAwardsPage.PortraitTypeRdoBtn); } }
        public IWebElement LandscapeTypeRdoBtn { get { return this.FindElement(Bys.ActAwardsPage.LandscapeTypeRdoBtn); } }
        public IWebElement CustomiseAwardHtmlLnk { get { return this.FindElement(Bys.ActAwardsPage.CustomiseAwardHtmlLnk); } }
        public IWebElement ViewHtmlFormCloseBtn { get { return this.FindElement(Bys.ActAwardsPage.ViewHtmlFormCloseBtn); } }
        public IWebElement TemplatePreviewFormCloseBtn { get { return this.FindElement(Bys.ActAwardsPage.TemplatePreviewFormCloseBtn); } }
    

        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {
            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActAwardsPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
        }

        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.ActAwardsPage.AddAwardBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddAwardBtn.GetAttribute("outerHTML"))
                {
                    AddAwardBtn.ClickJS(Browser);
                   // Browser.WaitJSAndJQuery();
                    this.WaitUntil(Criteria.ActAwardsPage.AddAwardTitleLblVisible);
                    Browser.WaitForElement(Bys.ActAwardsPage.AwardNameTxt, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAwardsPage.SaveAwardBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == SaveAwardBtn.GetAttribute("outerHTML"))
                {
                    SaveAwardBtn.ClickJS(Browser);
                    Browser.WaitForElement(Bys.Page.confirmMsgPopupOkBtn, ElementCriteria.IsVisible);
                    confirmMsgPopupOkBtn.ClickJS(Browser);
                    return null;
                }
            }
            if (Browser.Exists(Bys.ActAwardsPage.CloseAwardBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == CloseAwardBtn.GetAttribute("outerHTML"))
                {
                    CloseAwardBtn.ClickJS(Browser);
                    this.WaitUntil(Criteria.ActAwardsPage.AwardsTitleLblVisible);
                    Browser.WaitForElement(Bys.ActAwardsPage.AwardsTbl, ElementCriteria.IsVisible);
                    return null;
                }
            }

            if (Browser.Exists(Bys.ActAwardsPage.BackToAwardsLnk))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == BackToAwardsLnk.GetAttribute("outerHTML"))
                {
                    BackToAwardsLnk.ClickJS(Browser);
                    Browser.WaitForElement(Bys.ActAwardsPage.AwardsTblBody, ElementCriteria.IsVisible);
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
        /// Clicks Add Award button,  enter the name, select the template, Check all scenario checkboxes so this award shows 
        /// for all scenarios (in the future, we can code for choosing specific scenarios), then click Save award button ,
        /// Returns AwardName
        /// </summary>        
        /// <param name="awardName">(Optional). If null, then it will generate a random string; Else send Name</param>
        /// <param name="scenarioName">(Optional).currently it selects all scenarios ; added this parameter for future use</param>
        /// <param name="templateName">(Optional). If null, then it will pick the first item in the table; Else send exact template name  to choose </param>
        /// <param name="OrientationType">(Optional). If null, then default to "Portrait" type ; Else send "Landscape"</param>
        public dynamic AddAward(string awardName = null, string scenarioName = null, string templateName = null,string OrientationType = null)
        {
            ClickAndWait(AddAwardBtn);

            // Awardname - If null, then generate random string or set send value
            int randomNumber = DataUtils.GetRandomInteger(1000);
            string randomString = DataUtils.GetRandomString(2);
            string currentdate = string.Format("{0}", DateTime.UtcNow.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            awardName = string.IsNullOrEmpty(awardName) ? string.Format("AutoGenAward_{0}_{1}_{2}", randomNumber, randomString,currentdate) : awardName;
            this.WaitUntil(TimeSpan.FromSeconds(60), Criteria.ActAwardsPage.LoadIconNotExists);
            //Enter awardname
            ElemSet.TextBox_EnterText(Browser, AwardNameTxt,true, awardName);
           
            // If the user didnt specify a template, then just choose the first index
            IWebElement templateRow = null;
            if (templateName.IsNullOrEmpty())
            {
                templateRow = Browser.FindElement(Bys.ActAwardsPage.TemplatesTblFirstRow);
            }
            else
            {
                //Filter by the user specified template, should show one result 
                ElemSet.TextBox_EnterText(Browser, TemplateNameTxt, true, templateName);
                Thread.Sleep(050);
                if (Browser.FindElements(Bys.ActAwardsPage.TemplatesTblFirstRow).Count > 1)
                { 
                    throw new Exception("Template Search filter reults are not correct"); 
                }
                templateRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(TemplatesTbl, Bys.ActAwardsPage.TemplatesTblFirstRow, templateName, "td");

            }
            
            ElemSet_LMSAdmin.Grid_SelectradioOpt(templateRow, 1);
            
            Browser.WaitForElement(Bys.ActAwardsPage.ScenariosTbl, ElementCriteria.IsEnabled);
            
            // By default, Portrait should be checked , if user specify then choose "Landscape "
            if (OrientationType.Equals("Landscape"))
            {
                LandscapeTypeRdoBtn.Click();
                Browser.WaitForElement(Bys.ActAwardsPage.LandscapeTypeRdoBtn, ElementCriteria.HasAttribute("checked"));
            }
            else
            {
                Browser.WaitForElement(Bys.ActAwardsPage.PortraitTypeRdoBtn, ElementCriteria.HasAttribute("checked"));
            }
            
            // select all scenarios 
            IList<IWebElement> scenarioRows = Browser.FindElements(Bys.ActAwardsPage.ScenariosTblFirstRow);
            foreach (IWebElement scenarioRow in scenarioRows)
            {
                ElemSet_LMSAdmin.Grid_TickCheckBox(scenarioRow, 1);
            }

            // save award
            ClickAndWait(SaveAwardBtn);
           
            return awardName;

        }

        /// <summary>
        ///  Click on the specified award name from Awards Table for Editing, enter updated name , template name,
        ///  scenario selection (its coded to additionally choose the specified scenario,
        ///  not coded for uncheck the already selected; will do it infuture if needed) and save the award
        ///  </summary>
        /// <param name="awardName"> send the awardname which has to be edited </param>
        /// <param name="newname"> (Optional). if null , it just append "_Edited" text with previous name; Else, send the new awardname </param>
        /// <param name="templateName">(Optional).if null, no changes; Else, send the template name to be choosen</param>
        /// <param name="scenarioName">(Optional).if null, no changes; Else, send the scenario name to be choosen additionally, it will not 
        /// uncheck the already selected scenarios</param>
        /// <returns> updated Awardname </returns>
        public dynamic EditAward(string awardName , string newname =null,string templateName=null, string scenarioName = null)
        {
            // Click the awardname for which award's to be edited
            Browser.WaitForElement(Bys.ActAwardsPage.AwardsTbl, ElementCriteria.IsVisible);
            IWebElement awardRow =  ElemGet.Grid_GetRowByRowName(Browser, AwardsTbl, Bys.ActAwardsPage.AwardsTblFirstRow, awardName, "td");
            ElemSet.Grid_ClickCellByColIndex(Browser, awardRow, 0, "//td");
            Browser.WaitJSAndJQuery();
            Browser.WaitForElement(Bys.ActAwardsPage.EditAwardTitleLbl, ElementCriteria.IsVisible);
            Browser.WaitForElement(Bys.ActAwardsPage.AwardNameTxt, ElementCriteria.IsVisible);

            // Enter new award name
            newname = newname.IsNullOrEmpty() ? string.Format("{0}_Edited", awardName):newname  ;
            if (!newname.IsNullOrEmpty())
            {
                ElemSet.TextBox_EnterText(Browser, AwardNameTxt, true, newname);
            }

            // Choose new template if specified
            if (!templateName.IsNullOrEmpty())
            {
                IWebElement templateRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(TemplatesTbl, Bys.ActAwardsPage.TemplatesTblFirstRow, templateName, "td");            
                ElemSet_LMSAdmin.Grid_TickCheckBox(templateRow, 1);
            }

            // select additionally new scenario if specified
            if (!scenarioName.IsNullOrEmpty())
            {
                IWebElement scenarioRow = ElemGet_LMSAdmin.Grid_GetRowByRowName(ScenariosTbl, Bys.ActAwardsPage.ScenariosTblFirstRow, scenarioName, "td");
                ElemSet_LMSAdmin.Grid_TickCheckBox(scenarioRow, 1);
            }
            //save award
            ClickAndWait(SaveAwardBtn);
            return newname;
        }

        /// <summary>
        /// Deletes the given award from  Awards Table
        /// </summary>
        /// <param name="awardName"> send the awardname which has to be deleted</param>
        public void DeleteAward(string awardName )
        {
            // get the  specific award row 
            Browser.WaitForElement(Bys.ActAwardsPage.AwardsTbl, ElementCriteria.IsVisible);
            IWebElement awardRow = ElemGet.Grid_GetRowByRowName(Browser, AwardsTbl, Bys.ActAwardsPage.AwardsTblFirstRow, awardName, "td");
            
            // click on delete button
            ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, awardRow, "button[@aria-label='click to delete']");

            // click ok for delete confirmation popup
            Browser.WaitForElement(Bys.Page.ConfirmtationPopUpMsg, ElementCriteria.TextContains("delete this award"));
            confirmMsgPopupOkBtn.Click();
            
            // deleted notification displayed
            Browser.WaitForElement(Bys.Page.AlertNotificationIconMsg, ElementCriteria.TextContains("deleted Award"));            
        }

        #endregion methods: page specific


    }
}