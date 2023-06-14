using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class ActFrontMatterPageCriteria
    {
        public readonly ICriteria<ActFrontMatterPage> FrontMatterTblBodyVisible = new Criteria<ActFrontMatterPage>(p =>
        {
            return p.Exists(Bys.ActFrontMatterPage.FrontMatterTblBody, ElementCriteria.IsVisible);

        }, "Add Front Matter table visible");

        public readonly ICriteria<ActFrontMatterPage> PageReady;
        public ActFrontMatterPageCriteria()
        {
            PageReady = FrontMatterTblBodyVisible;
        }
    }
}
