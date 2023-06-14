using Browser.Core.Framework;

namespace LMS.AppFramework
{
    /// <summary>
    /// We can define various different criteria to wait for in this class. For example, we can wait for a Select Element to have items, or wait
    /// for an element to be visible, or to not be visible (wait for it to disappear). We then used this defined criteria in our Page classes or
    /// our Test classes. Note that this is a lot of copy and paste, and then modifying that paste to your specific element and the ElementCriteria
    /// that you want to use. Also make sure to add the custom exception messages as shown below. 
    /// </summary>
    public class _PCGAPageCriteria
    {
        /// <summary>
        /// To get an understanding of the way this works, if this Wait Criteria fails, then the exception will say "Timed out waiting for 1 
        /// Criteria 'custom error text shown below' (False)"
        /// </summary>
        public readonly ICriteria<_PCGAPage> ProfessionalEduTblFirstLnkVisible = new Criteria<_PCGAPage>(p =>
        {
            return p.Exists(Bys._PCGAPage.ProfessionalEduTblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Professional Edu 1 table exists and is visible");

        public readonly ICriteria<_PCGAPage> ProfessionalEducation3TblFirstLnkVisible = new Criteria<_PCGAPage>(p =>
        {
            return p.Exists(Bys._PCGAPage.ProfessionalEducation3TblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Professional Edu 3 table exists and is visible");

        public readonly ICriteria<_PCGAPage> ProfessionalEducation2TblFirstLnkVisible = new Criteria<_PCGAPage>(p =>
        {
            return p.Exists(Bys._PCGAPage.ProfessionalEducation2TblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Professional Edu 2 table exists and is visible");

        public readonly ICriteria<_PCGAPage> SearchBtnVisible = new Criteria<_PCGAPage>(p =>
        {
            return p.Exists(Bys.LMSPage.SearchTxt, ElementCriteria.IsVisible);

        }, "Search button exists and is visible");

        public readonly ICriteria<_PCGAPage> Mobile_SearchMagnifyingGlassBtnVisible = new Criteria<_PCGAPage>(p =>
        {
            return p.Exists(Bys.LMSPage.Mobile_SearchMagnifyingGlassBtn, ElementCriteria.IsVisible);

        }, "Search Magnifying button on emulation exists and is visible");

        public readonly ICriteria<_PCGAPage> LoadIconNotExists = new Criteria<_PCGAPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        /// <summary>
        /// We can define a PageReady property, which can be used in the WaitForInitialize method inside a given Page class, to wait for the entire
        /// page to be loaded. Note below that I am using the AND condition to combine the 3 criteria above. <see cref="_PCGAPage.WaitForInitialize"/>
        /// If only 1 of the Criteria fails to be met below, then the exception will say "Timed out waiting for 3 Criteria:
        /// 'Main Header Lbl visible' (False) AND 'Search text box visible' (True) AND 'Hate HTML form, X button exists and is visible'
        /// </summary>
        public readonly ICriteria<_PCGAPage> PageReady;

        public _PCGAPageCriteria()
        {
            PageReady = ProfessionalEduTblFirstLnkVisible.AND(ProfessionalEducation3TblFirstLnkVisible).
                AND(ProfessionalEducation2TblFirstLnkVisible).AND(LoadIconNotExists);
        }
    }
}
