using Browser.Core.Framework;

namespace LMSAdmin.AppFramework
{
    public class ActAccreditationPageCriteria
    {
        public readonly ICriteria<ActAccreditationPage> BackToActivityBtnVisible = new Criteria<ActAccreditationPage>(p =>
        {
            return p.Exists(Bys.Page.BackToActivityBtn, ElementCriteria.IsVisible);

        }, "BackToActivityBtn is visible");

        public readonly ICriteria<ActAccreditationPage> AccreditationTabVisible = new Criteria<ActAccreditationPage>(p =>
        {
            return p.Exists(Bys.ActAccreditationPage.AccreditationPageTitleLbl, ElementCriteria.IsVisible);

        }, "AccreditationPageTitleLbl is visible");

        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>
        public readonly ICriteria<ActAccreditationPage> AdditionalProviderSelElemOptionsDropdownNotExists = new Criteria<ActAccreditationPage>(p =>
        {
            return !p.Exists(Bys.ActAccreditationPage.AdditionalProviderSelElemOptionsDropdown);

        }, "Add Accreditation Form , Additional Providers Dropdown Options Not Exists");

        public readonly ICriteria<ActAccreditationPage> EligibleProfessionElemOptionsDropdownHasMoreThanOneValue = new Criteria<ActAccreditationPage>(p =>
        {
            return p.Exists(Bys.ActAccreditationPage.EligibleProfessionElemOptionsDropdown, ElementCriteria.SelectElementHasMoreThan1Item);

        }, "Add Scenario Form , EligibleProfession Dropdown Option Has More Than One Item");

        public readonly ICriteria<ActAccreditationPage> LoadIconNotExists = new Criteria<ActAccreditationPage>(p =>
        {
            return !p.Exists(Bys.Page.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActAccreditationPage> EditAccreditationFormEditAccreditationLbl = new Criteria<ActAccreditationPage>(p =>
        {           
            return p.Exists(Bys.ActAccreditationPage.EditAccreditationFormEditAccreditationLbl, ElementCriteria.IsVisible);

        }, "EditAccreditationForm is loaded with EditAccreditationLbl is visible");


        public readonly ICriteria<ActAccreditationPage> PageReady;       

        public ActAccreditationPageCriteria()
        {
            PageReady = BackToActivityBtnVisible.AND(AccreditationTabVisible);
        }
    }
}
