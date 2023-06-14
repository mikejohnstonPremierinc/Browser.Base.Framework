using OpenQA.Selenium;

namespace LMSAdmin.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class Distribution_CatalogsPageBys
    {

        // Buttons
        public readonly By DetailsTabSaveBtn = By.Id("ctl00$btnSave");
        public readonly By DetailsTabCancelBtn = By.Id("ctl00_btnCancel");

        // Charts

        // Check boxes

        // Labels                                              


        // Links

        public readonly By AddNewCatalogLnk = By.XPath("//strong[text()='Add New Catalog']");





        // Menu Items    

        // Radio buttons

        // Tables   



        // Tabs


        // Text boxes
        public readonly By DetailsTabCatalogNameTxt = By.Id("ctl00_catalogName");
        public readonly By DetailsTabShortLabelTxt = By.Id("ctl00_shortLabel");
        public readonly By DetailsTabDescriptionTxt = By.Id("ctl00_description");



    }
}