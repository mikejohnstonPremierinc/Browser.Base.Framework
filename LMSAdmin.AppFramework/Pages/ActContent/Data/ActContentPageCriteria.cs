using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class ActContentPageCriteria
    {
        public readonly ICriteria<ActContentPage>ContentTitleVisible = new Criteria<ActContentPage>(p =>
        {
            return p.Exists(Bys.ActContentPage.ContentTitleLbl, ElementCriteria.IsVisible);
          
        }, "Content Title is visible");

        public readonly ICriteria<ActContentPage> ContentStepsVisible = new Criteria<ActContentPage>(p =>
        {
            return p.Exists(Bys.ActContentPage.ContentLbl, ElementCriteria.IsVisible);
           
         }, "Content Steps visible");
        public readonly ICriteria<ActContentPage> LoadIconNotExists = new Criteria<ActContentPage>(p =>
        {
            return !p.Exists(Bys.Page.LoadIcon);

        }, "Load icon not exists");
        public readonly ICriteria<ActContentPage> PageReady;
        public ActContentPageCriteria()
        {
            PageReady = ContentTitleVisible.AND(ContentStepsVisible);

        }

    }
}
