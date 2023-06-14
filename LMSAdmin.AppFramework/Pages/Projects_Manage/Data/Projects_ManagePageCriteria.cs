using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class Projects_ManagePageCriteria
    {
        public readonly ICriteria<Projects_ManagePage> ActivitiesSearchTxtVisible = new Criteria<Projects_ManagePage>(p =>
        {
            return p.Exists(Bys.Projects_ManagePage.ManageSearchTxt, ElementCriteria.IsVisible);

        }, "Activity search text box visible");

        public readonly ICriteria<Projects_ManagePage> ManageActivitiesTblBodyRowNotExists = new Criteria<Projects_ManagePage>(p =>
        {
            return !p.Exists(Bys.Projects_ManagePage.ManageTblBodyRow);

        }, "Manage Search Results table, first row not exists");

        public readonly ICriteria<Projects_ManagePage> ManageTblBodyRowExistsAndVisible = new Criteria<Projects_ManagePage>(p =>
        {
            return p.Exists(Bys.Projects_ManagePage.ManageTblBodyRow, ElementCriteria.IsVisible);

        }, "Manage Search Results table, first row visible");
        


        public readonly ICriteria<Projects_ManagePage> PageReady;

        public Projects_ManagePageCriteria()
        {
            PageReady = ManageTblBodyRowExistsAndVisible;
        }
    }
}
