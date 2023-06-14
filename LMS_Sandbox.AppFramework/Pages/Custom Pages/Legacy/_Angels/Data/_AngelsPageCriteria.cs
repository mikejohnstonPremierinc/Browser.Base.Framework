﻿using Browser.Core.Framework;

namespace LMS.AppFramework
{
    /// <summary>
    /// We can define various different criteria to wait for in this class. For example, we can wait for a Select Element to have items, or wait
    /// for an element to be visible, or to not be visible (wait for it to disappear). We then used this defined criteria in our Page classes or
    /// our Test classes. Note that this is a lot of copy and paste, and then modifying that paste to your specific element and the ElementCriteria
    /// that you want to use. Also make sure to add the custom exception messages as shown below. 
    /// </summary>
    public class _AngelsPageCriteria
    {
        /// <summary>
        /// To get an understanding of the way this works, if this Wait Criteria fails, then the exception will say "Timed out waiting for 1 
        /// Criteria 'custom error text shown below' (False)"
        /// </summary>
        public readonly ICriteria<_AngelsPage> ProfessionalEducationTblFirstLnkVisible = new Criteria<_AngelsPage>(p =>
        {
            return p.Exists(Bys._AngelsPage.ProfessionalEducationTblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Professional Education table exists and is visible");

        public readonly ICriteria<_AngelsPage> MyRecentActivitiesTblFirstLnkVisible = new Criteria<_AngelsPage>(p =>
        {
            return p.Exists(Bys._AngelsPage.MyRecentActivitiesTblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the My Recent Activities table exists and is visible");

        public readonly ICriteria<_AngelsPage> FeaturedActivitiesTblFirstLnkVisible = new Criteria<_AngelsPage>(p =>
        {
            return p.Exists(Bys._AngelsPage.FeaturedActivitiesTblFirstLnk, ElementCriteria.IsVisible);

        }, "The first link from the Featured Activities table exists and is visible");

        public readonly ICriteria<_AngelsPage> SearchBtnVisible = new Criteria<_AngelsPage>(p =>
        {
            return p.Exists(Bys.LMSPage.SearchTxt, ElementCriteria.IsVisible);

        }, "Search button exists and is visible");

        public readonly ICriteria<_AngelsPage> Mobile_SearchMagnifyingGlassBtnVisible = new Criteria<_AngelsPage>(p =>
        {
            return p.Exists(Bys.LMSPage.Mobile_SearchMagnifyingGlassBtn, ElementCriteria.IsVisible);

        }, "Search Magnifying button on emulation exists and is visible");

        public readonly ICriteria<_AngelsPage> LoadIconNotExists = new Criteria<_AngelsPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        /// <summary>
        /// We can define a PageReady property, which can be used in the WaitForInitialize method inside a given Page class, to wait for the entire
        /// page to be loaded. Note below that I am using the AND condition to combine the 3 criteria above. <see cref="_AngelsPage.WaitForInitialize"/>
        /// If only 1 of the Criteria fails to be met below, then the exception will say "Timed out waiting for 3 Criteria:
        /// 'Main Header Lbl visible' (False) AND 'Search text box visible' (True) AND 'Hate HTML form, X button exists and is visible'
        /// </summary>
        public readonly ICriteria<_AngelsPage> PageReady;

        public _AngelsPageCriteria()
        {
            PageReady = ProfessionalEducationTblFirstLnkVisible.AND(MyRecentActivitiesTblFirstLnkVisible).
                AND(FeaturedActivitiesTblFirstLnkVisible).AND(LoadIconNotExists);
        }
    }
}
