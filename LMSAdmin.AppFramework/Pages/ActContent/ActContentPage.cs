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
    public class ActContentPage : Page, IDisposable
    {
        #region constructors
        public ActContentPage(IWebDriver driver) : base(driver)
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
        public IWebElement AddContentBtn { get { return this.FindElement(Bys.ActContentPage.AddContentBtn); } }
        public IWebElement EditContentFormSaveBtn { get { return this.FindElement(Bys.ActContentPage.EditContentFormSaveBtn); } }
        public IWebElement AddContentFormAddBtn { get { return this.FindElement(Bys.ActContentPage.AddContentFormAddBtn); } }
        public IWebElement ContentRequiredEnabledChkbox { get { return this.FindElement(Bys.ActContentPage.ContentRequiredEnabledChkbox); } }
        public IWebElement ContentTitleLbl { get { return this.FindElement(Bys.ActContentPage.ContentTitleLbl); } }
        public IWebElement ContentTypeSelElemBtn { get { return this.FindElement(Bys.ActContentPage.ContentTypeSelElemBtn); } }
        public IWebElement ContentTypeSelElemOptionsDropdown { get { return this.FindElement(Bys.ActContentPage.ContentTypeSelElemOptionsDropdown); } }
        public IWebElement AddContentFormDisplayNameTxt { get { return this.FindElement(Bys.ActContentPage.AddContentFormDisplayNameTxt); } }
        public IWebElement EditContentFormDisplayNameTxt { get { return this.FindElement(Bys.ActContentPage.EditContentFormDisplayNameTxt); } }
        public IWebElement AddContentFormDescriptionTxt { get { return this.FindElement(Bys.ActContentPage.AddContentFormDescriptionTxt); } }
        public IWebElement BrowseToUploadLnk { get { return this.FindElement(Bys.ActContentPage.BrowseToUploadLnk); } }
        public IWebElement ContentSaveBtn { get { return this.FindElement(Bys.ActContentPage.ContentSaveBtn); } }
        public IWebElement BrowseToUploadFileLnk { get { return this.FindElement(Bys.ActContentPage.BrowseToUploadFileLnk); } }
        public IWebElement AddContentFormEnterURLTxt { get { return this.FindElement(Bys.ActContentPage.AddContentFormEnterURLTxt); } }
        public IWebElement ContentsTbl { get { return this.FindElement(Bys.ActContentPage.ContentsTbl); } }
        public IWebElement ContentLbl { get { return this.FindElement(Bys.ActContentPage.ContentLbl); } }
        public IWebElement ContentDeleteBtn { get { return this.FindElement(Bys.ActContentPage.ContentDeleteBtn); } }
        public IWebElement ContentLoadingImage { get { return this.FindElement(Bys.ActContentPage.ContentLoadingImage); } }
        public IWebElement ContentFirstrow { get { return this.FindElement(Bys.ActContentPage.ContentFirstrow); } }
        public IWebElement ViewContentFormCloseBtn { get { return this.FindElement(Bys.ActContentPage.ViewContentFormCloseBtn); } }
        public IWebElement ContentFileDownloadLnk { get { return this.FindElement(Bys.ActContentPage.ContentFileDownloadLnk); } }
        public IWebElement ContentPreviewFormCloseBtn { get { return this.FindElement(Bys.ActContentPage.ContentPreviewFormCloseBtn); } }
        #endregion elements

        #region methods: repeated per page

        public override void WaitForInitialize()
        {

            this.WaitUntil(TimeSpan.FromSeconds(30), Criteria.ActContentPage.PageReady);
            Browser.WaitJSAndJQuery(TimeSpan.FromSeconds(120));
        }
        /// <summary>
        /// Clicks the user-specified element, and then waits for a window to close or open, or a page to load, depending on the element that was clicked
        /// </summary>
        /// <param name="buttonOrLinkElem">The element to click on</param>
        public dynamic ClickAndWait(IWebElement buttonOrLinkElem)
        {
            if (Browser.Exists(Bys.ActContentPage.AddContentBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddContentBtn.GetAttribute("outerHTML"))
                {
                    AddContentBtn.ClickJS(Browser);
                    Browser.WaitJSAndJQuery();
                    Browser.WaitForElement(Bys.ActContentPage.ContentTypeSelElemBtn, ElementCriteria.IsVisible,ElementCriteria.IsEnabled);
                    return null;
                }
            }
            if (Browser.Exists(Bys.ActContentPage.ContentLbl))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ContentLbl.GetAttribute("outerHTML"))
                {
                    ContentLbl.Click();
                    ActContentPage page = new ActContentPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }
            if (Browser.Exists(Bys.ActContentPage.ContentSaveBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == ContentLbl.GetAttribute("outerHTML"))
                {
                    ContentSaveBtn.Click();
                    ActContentPage page = new ActContentPage(Browser);
                    page.WaitForInitialize();
                    return page;
                }
            }

            if (Browser.Exists(Bys.ActContentPage.AddContentFormAddBtn))
            {
                if (buttonOrLinkElem.GetAttribute("outerHTML") == AddContentFormAddBtn.GetAttribute("outerHTML"))
                {
                    AddContentFormAddBtn.ClickJS(Browser);
                    this.WaitUntil(TimeSpan.FromMinutes(5), Criteria.ActContentPage.LoadIconNotExists);
                    Browser.WaitForElement(Bys.ActContentPage.ContentSaveBtn, ElementCriteria.IsVisible);
                    return null;
                }
            }

            throw new Exception(string.Format("No element was found with your passed parameter, which was the '{0}' element. You either need to add " +
                "this element to a new If statement, or if the element is already added, then the page you were on did not contain the element.",
                buttonOrLinkElem.GetAttribute("innerText")));
        }
        /// <summary>
        /// Clicks to browse a SCORM file 11162017_UDS6B2017.zip, The file is uploaded and saved as new content type.  
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
        /// This method waits till the upload has completed verifying the uploading spinning option false. 
        public void ContentLoadingComplete()
        {
            try
            {  
                Browser.WaitForElement(Bys.ActContentPage.ContentLoadingImage, TimeSpan.FromSeconds(5), ElementCriteria.IsVisible);
            }
            catch { }
            while (Browser.FindElements(Bys.ActContentPage.ContentLoadingImage).IsNullOrEmpty() == false) ;
        }

        /// <summary>
        /// uploads a file from the given path 
        /// Default file "C:\seleniumupload_lmsadmin\11162017_UDS6B2017.zip" will be taken if user not sends the file path 
        /// </summary>
        /// <param name="filepath"> send full file path </param>
        public void UploadSCORM(string filepath = null)
        {
            // if filepath empty then default file will be taken
            filepath = filepath.IsNullOrEmpty() ? filepath = @"C:\seleniumupload_lmsadmin\11162017_UDS6B2017.zip" : "@"+filepath;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            FileUtils.UploadFileUsingSendKeys(Browser, BrowseToUploadLnk, filepath);
            Browser.WaitJSAndJQuery();
        }
        /// <summary>
        /// uploads a file from the given path 
        /// Default file "C:\seleniumupload_lmsadmin\apple.jpg " will be taken if user not sends the file path 
        /// </summary>
        /// <param name="filepath"> send full file path </param>
        public void UploadFile(string filepath=null)
        {
            // if filepath empty then default file will be taken
            filepath = filepath.IsNullOrEmpty() ? filepath = @"C:\seleniumupload_lmsadmin\apple.jpg" : "@"+filepath;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            
            //select the  file to upload
            FileUtils.UploadFileUsingSendKeys(Browser, BrowseToUploadFileLnk, filepath);
            Browser.WaitJSAndJQuery();
        }
        
        /// <summary>
        /// From Content Table , Deletes multiple content rows If contentName CONTAINS the search name given by user
        /// </summary>
        /// <param name="contentName">send content name to be searched </param>
        public void DeleteMultipleContentRecords(string contentName)
        {
            IList<IWebElement> rows = Browser.FindElements(Bys.ActContentPage.ContentsTblFirstRow);
            if (!rows[0].GetAttribute("class").Contains("no-data"))
            {
                Boolean flag_save = false;
                foreach (IWebElement row in rows)
                {
                    /// search the name for all content row records
                    string text = row.FindElement(By.XPath("./td[2]//div[@class='cell-title']")).Text;
                    if (text.Contains(contentName))
                    {
                        /// click trash/delete icon
                        ElemSet.Grid_ClickButtonOrLinkWithoutTextWithinRow(Browser, row, "span[@class='glyphicon glyphicon-trash']");
                        flag_save = true;
                    }
                }
                /// if atleast one search record found , then it save the changes
                if (flag_save)
                {
                    ContentSaveBtn.Click();
                    Browser.WaitForElement(Bys.Page.ConfirmtationPopUpMsg, ElementCriteria.TextContains("save the changes"));
                    confirmMsgPopupOkBtn.Click();
                    Browser.WaitForElement(Bys.Page.AlertNotificationIconMsg, ElementCriteria.TextContains("success"));
                }
            }
        }

        /// <summary>
        /// Clicks AddContent button, Selects "Scorm" Content type, enter the name & Description & upload the given scorm file then click add
        /// </summary>
        /// <param name="ScormContentName">(Optional) send content name ;else script generates name</param>
        /// <param name="ContDescriptionTxt">(Optional) send content description ;else script type some text</param>
        /// <param name="filepath">(Optional) send file path ; else Default file "C:\seleniumupload_lmsadmin\11162017_UDS6B2017.zip" will be taken</param>
        public dynamic AddContentScorm( string ScormContentName=null, string ContDescriptionTxt =null,string filepath = null)
        {          
            ClickAndWait(AddContentBtn);

            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, ContentTypeSelElemBtn, "SCORM");

            // if user not gives the name for the content , script generates like ex:  AutoTestData_Scorm_04/28/2020
            string todaysdate = DateTime.UtcNow.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string AutoGeneratedName = String.Format("{0}_{1}", "AutoTestData_Scorm", todaysdate);
            ScormContentName = ScormContentName.IsNullOrEmpty() ? AutoGeneratedName : ScormContentName;

            ContDescriptionTxt = ContDescriptionTxt.IsNullOrEmpty() ? "AutomationTest ScormContent Description" : ContDescriptionTxt;

            // Enter Content Name, Description, And upload a file
            AddContentFormDescriptionTxt.SendKeys(ContDescriptionTxt);
            AddContentFormDisplayNameTxt.SendKeys(ScormContentName);

            // if no filepath given ,  default scorm file will be taken by script
            UploadSCORM(filepath);

            ClickAndWait(AddContentFormAddBtn);

            return ScormContentName;            
        }

        /// <summary>
        /// Clicks AddContent button, Selects "FileUpload" Content type, enter the name & Description & upload the given file then click add
        /// </summary>
        /// <param name="FileContentName"> (Optional) send content name ;else script generates name</param>
        /// <param name="ContDescriptionTxt">(Optional) send content description ;else script type some text</param>
        /// <param name="filepath"> (Optional) send file path ;else Default file "C:\\seleniumupload_lmsadmin\\apple.jpg " will be taken</param>
        public dynamic AddContentFile(string FileContentName=null, string ContDescriptionTxt=null,string filepath=null)
        {          
            ClickAndWait(AddContentBtn);

            // if user not gives the name for the content , script generates like ex:  AutoTestData_File_04/28/2020
            string todaysdate = DateTime.UtcNow.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string AutoGeneratedName = String.Format("{0}_{1}", "AutoTestData_File", todaysdate);
            FileContentName = FileContentName.IsNullOrEmpty() ? AutoGeneratedName : FileContentName;

            ContDescriptionTxt = ContDescriptionTxt.IsNullOrEmpty() ? "AutomationTest FileContent Description" : ContDescriptionTxt;

            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, ContentTypeSelElemBtn, "File Upload");
            Browser.WaitJSAndJQuery();

            // Enter Content Name, Description, And upload a file
            AddContentFormDescriptionTxt.SendKeys(ContDescriptionTxt);
            AddContentFormDisplayNameTxt.SendKeys(FileContentName);

            // if no filepath given ,  default file will be taken by script
            UploadFile(filepath);

            ClickAndWait(AddContentFormAddBtn);

            return FileContentName;
           
        }


        /// <summary>
        /// Clicks AddContent button, Selects "Url" Content type, enter the name & Description & URL link then click add
        /// </summary>
        /// <param name="HttpURL">send URL address ; format ex: http://www.google.com</param>
        /// <param name="URLContentName">(Optional) send content name ;else script generates name</param>
        /// <param name="ContDescriptionTxt">(Optional) send content description ;else script type some text</param>
        public dynamic AddContentURL(string HttpURL,string URLContentName=null, string ContDescriptionTxt=null )
        {
            // if user not gives the name for the content , script generates like ex:  AutoTestData_URL_04/28/2020
            string todaysdate = DateTime.UtcNow.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); 
            string AutoGeneratedName = String.Format("{0}_{1}", "AutoTestData_URL", todaysdate);
            URLContentName= URLContentName.IsNullOrEmpty() ? AutoGeneratedName : URLContentName;

            ContDescriptionTxt = ContDescriptionTxt.IsNullOrEmpty() ? "AutomationTest URLContent Description" : ContDescriptionTxt;

            ClickAndWait(AddContentBtn);

            // selects URL type
            ElemSet_LMSAdmin.DropdownSingle_Fireball_SelectByText(Browser, ContentTypeSelElemBtn, "Url");
            Browser.WaitJSAndJQuery();

            //enter name, description, URL 
            AddContentFormDescriptionTxt.SendKeys(ContDescriptionTxt);
            AddContentFormDisplayNameTxt.SendKeys(URLContentName);
            AddContentFormEnterURLTxt.SendKeys(HttpURL);

            ClickAndWait(AddContentFormAddBtn);

            return URLContentName;

        }

        #endregion methods: page specific


    }

}