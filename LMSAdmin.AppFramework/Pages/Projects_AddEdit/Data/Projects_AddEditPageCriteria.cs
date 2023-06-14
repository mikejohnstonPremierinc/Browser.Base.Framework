using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class Projects_AddEditPageCriteria
    {

        public readonly ICriteria<Projects_AddEditPage> ProjectNameTxtVisible = new Criteria<Projects_AddEditPage>(p =>
        {
            return p.Exists(Bys.Projects_AddEditPage.ProjectNameTxt, ElementCriteria.IsVisible);

        }, "Project Name text box visible");

        public readonly ICriteria<Projects_AddEditPage> PageReady;

        public Projects_AddEditPageCriteria()
        {
            PageReady = ProjectNameTxtVisible ;
        }
    }
}
