using Browser.Core.Framework;

namespace LMSAdmin.AppFramework
{
    public class ActMainPageCriteria
    {
        public readonly ICriteria<ActMainPage> PubDetailsTabAvailCatTblVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.PubDetailsTabAvailCatTbl, ElementCriteria.IsVisible);

        }, "Available Catalogs table visible");

        public readonly ICriteria<ActMainPage> PubDetailsTabVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.PubDetailsTab, ElementCriteria.IsVisible);

        }, "Publishing Details tab visible");

        public readonly ICriteria<ActMainPage> PubDetailsTabAvailCatTblSearchCatLoadElemVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.PubDetailsTabAvailCatTblSearchCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Publishing Details tab, search available catalogs, load element visible");

        public readonly ICriteria<ActMainPage> PubDetailsTabAvailCatTblSearchCatLoadElemNotVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.PubDetailsTabAvailCatTblSearchCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Publishing Details tab, search available catalogs, load element not visible");

        public readonly ICriteria<ActMainPage> PubDetailsTabAvailCatTblAddCatLoadElemVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.PubDetailsTabAvailCatTblAddCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Publishing Details tab, add catalog, load element visible");

        public readonly ICriteria<ActMainPage> PubDetailsTabAvailCatTblAddCatLoadElemNotVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.PubDetailsTabAvailCatTblAddCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Publishing Details tab, add catalog, load element not visible");

        public readonly ICriteria<ActMainPage> PubDetailsTabSelectedCatTblRemoveCatLoadElemVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Publishing Details tab, remove catalog, load element visible");

        public readonly ICriteria<ActMainPage> PubDetailsTabSelectedCatTblRemoveCatLoadElemNotVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.PubDetailsTabSelectedCatTblRemoveCatLoadElem, ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Publishing Details tab, remove catalog, load element not visible");

        public readonly ICriteria<ActMainPage> EditPortalFormCustomFeeTxtVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.EditPortalFormCustomFeeTxt, ElementCriteria.IsVisible);

        }, "Edit Portal form, Custom Fee text box visible");

        public readonly ICriteria<ActMainPage> EditPortalFormCustomFeeTxtNotVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.EditPortalFormCustomFeeTxt, ElementCriteria.IsNotVisible);

        }, "Edit Portal form, Custom Fee text box not visible");

        public readonly ICriteria<ActMainPage> DetailsTabPublishbtnVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.DetailsTabPublishbtn, ElementCriteria.IsVisible);

        }, "Details tab, Publish button visible");

        public readonly ICriteria<ActMainPage> ConstructionCompleteMessageLblVisible = new Criteria<ActMainPage>(p =>
        {
            return p.Exists(Bys.ActMainPage.ConstructionCompleteMessageLbl, ElementCriteria.IsVisible);

        }, "Stage set to ConstructionComplete Message is visible");
       
        public readonly ICriteria<ActMainPage> ConstructionCompleteMessageLblNotVisible = new Criteria<ActMainPage>(p =>
        {
            return !p.Exists(Bys.ActMainPage.ConstructionCompleteMessageLbl);

        }, "Stage set to ConstructionComplete Message is not visible");


        public readonly ICriteria<ActMainPage> PageReady;
        public ActMainPageCriteria()
        {
            PageReady = PubDetailsTabVisible;
        }
    }
}
