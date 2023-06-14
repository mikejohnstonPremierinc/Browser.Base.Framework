using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ActMainPageBys
    {

        // Buttons
        public readonly By PubDetailsTabAvailCatSearchBtn = By.Id("ctl00_btnSearch");
        public readonly By DetailsTabUnPublishBtn = By.Id("ctl00_UnPublish");
        public readonly By DetailsTabUnPublishConfirmBtn = By.Id("ctl00_UnPublishConfirmBtn");
        public readonly By DetailsTabPublishbtn = By.Id("ctl00_Publish");
        public readonly By DetailsTabPublishConfirmbtn = By.Id("ctl00_PublishConfirmBtn");
        public readonly By DetailsTabSavebtn = By.Id("ctl00_AddNode");
        public readonly By TimeLocationTabSaveBtn = By.Id("ctl00_AddNode");





        // Charts

        // Check boxes


        // General
        public readonly By PubDetailsTabAvailCatTblAddCatLoadElem = By.Id("ctl00_availableUpdateProgress");
        public readonly By PubDetailsTabSelectedCatTblRemoveCatLoadElem = By.Id("ctl00_selectedUpdateProgress");
        public readonly By PubDetailsTabAvailCatTblSearchCatLoadElem = By.Id("ctl00_SearchUpdateProgress");
        public readonly By EditPortalFormSaveBtn = By.Id("ctl00_btnSaveCustomFee");
        public readonly By ConstructionCompleteMessageLbl = By.XPath("//td[strong='Locked:'][contains(text(),'Construction Complete')]");





        // Labels                                                   
        public readonly By DetailsTabActivityNumberLbl = By.Id("ctl00_CECityActivityNumber");
        public readonly By YouDoNotHaveEditingRightsWarningLbl = By.XPath("//span[text()='You do not have editing rights.']");

        

        // Links

        // Menu Items    

        // Radio buttons

        // select elements
        public readonly By DetailsTabStageSelElem = By.Id("ctl00_ActivityStatus");
        public readonly By TimeLocationTabCountrySelElem = By.Id("ctl00_ddlCountry");
        public readonly By TimeLocationTabStateSelElem = By.Id("ctl00_ddlTerritory");

        
            
        // Tables   
        public readonly By PubDetailsTabSelCatTbl = By.XPath("//div[@id='ctl00_SelectedCatalogsUpdatePanel']/table");
        public readonly By PubDetailsTabSelCatTblBody = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/tbody");
        public readonly By PubDetailsTabSelCatTblBodyRow = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");
        public readonly By PubDetailsTabAvailCatTbl = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table");
        public readonly By PubDetailsTabAvailCatTblBody = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/tbody");
        public readonly By PubDetailsTabAvailCatTblBodyRow = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");
        public readonly By PubDetailsTabAvailCatTblFirstBtn = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/descendant::a[text()='1']");
        public readonly By PubDetailsTabAvailCatTblNextBtn = By.XPath("//div[@id='ctl00_AvailableCatalogsUpdatePanel']/table/descendant::a[@id='ctl00_AvailableCatalogsPager_dlPaging_ctl00_lbPage']");
        public readonly By PubDetailsTabPortalsTbl = By.XPath("//div[@id='ctl00_PortalsUpdatePanel']/table");
        public readonly By PubDetailsTabPortalsTblBody = By.XPath("//div[@id='ctl00_PortalsUpdatePanel']/table/tbody");
        public readonly By PubDetailsTabPortalsTblBodyRow = By.XPath("//div[@id='ctl00_PortalsUpdatePanel']/table/tbody/tr[@class='ccTableRow' or @class='ccTableRowAlt']");


        // Tabs
        public readonly By PubDetailsTab = By.XPath("//span[text()='Publishing Details']");
        public readonly By DetailsTab = By.XPath("//span[text()='Details']");
        public readonly By TimeLocationTab = By.XPath("//span[text()='Time/Location']");

        
        // Text boxes
        public readonly By DetailsTabActivityTitleTxt = By.Id("ctl00_txtActivityName");
        public readonly By DetailsTabShortLabelTxt = By.Id("ctl00_txtShortLabel");
        public readonly By DetailsTabDescriptionTxt = By.Id("ctl00_txtDescription");
        public readonly By PubDetailsTabAvailCatSearchTxt = By.Id("ctl00_txtAvailableCatalogs");
        public readonly By EditPortalFormCustomFeeTxt = By.Id("ctl00_txtCustomFee");
        public readonly By TimeLocationTabLocationNameTxt = By.Id("ctl00_txtLocationName");
        public readonly By TimeLocationTabAddressLine1Txt = By.Id("ctl00_txtAddressLine1");
        public readonly By TimeLocationTabCity = By.Id("ctl00_txtCity");
        public readonly By TimeLocationTabPostalCode = By.Id("ctl00_txtPostalCode");


    }
}