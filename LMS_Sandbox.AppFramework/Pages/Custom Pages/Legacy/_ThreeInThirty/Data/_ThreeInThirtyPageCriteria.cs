using Browser.Core.Framework;

namespace LMS.AppFramework
{
    /// <summary>
    /// We can define various different criteria to wait for in this class. For example, we can wait for a Select Element to have items, or wait
    /// for an element to be visible, or to not be visible (wait for it to disappear). We then used this defined criteria in our Page classes or
    /// our Test classes. Note that this is a lot of copy and paste, and then modifying that paste to your specific element and the ElementCriteria
    /// that you want to use. Also make sure to add the custom exception messages as shown below. 
    /// </summary>
    public class _ThreeInThirtyPageCriteria
    {
        /// <summary>
        /// To get an understanding of the way this works, if this Wait Criteria fails, then the exception will say "Timed out waiting for 1 
        /// Criteria 'custom error text shown below' (False)"
        /// </summary>
        public readonly ICriteria<_ThreeInThirtyPage> ComingSoon1TblFirstLnkVisible = new Criteria<_ThreeInThirtyPage>(p =>
        {
            return p.Exists(Bys._ThreeInThirtyPage.ComingSoon1TblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Coming Soon 1 table exists and is visible");

        public readonly ICriteria<_ThreeInThirtyPage> ComingSoon2TblFirstLnkVisible = new Criteria<_ThreeInThirtyPage>(p =>
        {
            return p.Exists(Bys._ThreeInThirtyPage.ComingSoon2TblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Coming Soon 2 table exists and is visible");

        public readonly ICriteria<_ThreeInThirtyPage> ComingSoon3TblFirstLnkVisible = new Criteria<_ThreeInThirtyPage>(p =>
        {
            return p.Exists(Bys._ThreeInThirtyPage.ComingSoon3TblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Coming Soon 3 table exists and is visible");

        public readonly ICriteria<_ThreeInThirtyPage> SearchBtnVisible = new Criteria<_ThreeInThirtyPage>(p =>
        {
            return p.Exists(Bys.LMSPage.SearchTxt, ElementCriteria.IsVisible);

        }, "Search button exists and is visible");

        public readonly ICriteria<_ThreeInThirtyPage> Mobile_SearchMagnifyingGlassBtnVisible = new Criteria<_ThreeInThirtyPage>(p =>
        {
            return p.Exists(Bys.LMSPage.Mobile_SearchMagnifyingGlassBtn, ElementCriteria.IsVisible);

        }, "Search Magnifying button on emulation exists and is visible");

        public readonly ICriteria<_ThreeInThirtyPage> LoadIconNotExists = new Criteria<_ThreeInThirtyPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        /// <summary>
        /// We can define a PageReady property, which can be used in the WaitForInitialize method inside a given Page class, to wait for the entire
        /// page to be loaded. Note below that I am using the AND condition to combine the 3 criteria above. <see cref="_ThreeInThirtyPage.WaitForInitialize"/>
        /// If only 1 of the Criteria fails to be met below, then the exception will say "Timed out waiting for 3 Criteria:
        /// 'Main Header Lbl visible' (False) AND 'Search text box visible' (True) AND 'Hate HTML form, X button exists and is visible'
        /// </summary>
        public readonly ICriteria<_ThreeInThirtyPage> PageReady;

        public _ThreeInThirtyPageCriteria()
        {
            PageReady = ComingSoon1TblFirstLnkVisible.AND(ComingSoon2TblFirstLnkVisible).
                AND(ComingSoon3TblFirstLnkVisible).AND(LoadIconNotExists);
        }
    }
}
