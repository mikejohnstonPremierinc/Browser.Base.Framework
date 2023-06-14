using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class ProjectsPageCriteria
    {
        public readonly ICriteria<ProjectsPage> ManageActivitiesLnkVisible = new Criteria<ProjectsPage>(p =>
        {
            return p.Exists(Bys.ProjectsPage.ManageActivitiesLnk, ElementCriteria.IsVisible);

        }, "Manage Activities link visible");


        public readonly ICriteria<ProjectsPage> PageReady;

        public ProjectsPageCriteria()
        {
            PageReady = ManageActivitiesLnkVisible;
        }
    }
}
