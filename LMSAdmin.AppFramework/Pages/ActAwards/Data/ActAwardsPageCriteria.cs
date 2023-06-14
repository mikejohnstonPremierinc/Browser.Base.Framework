using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class ActAwardsPageCriteria
    {
        public readonly ICriteria<ActAwardsPage> AwardsTitleLblVisible = new Criteria<ActAwardsPage>(p =>
        {
            return p.Exists(Bys.ActAwardsPage.AwardsTitleLbl, ElementCriteria.IsVisible);

        }, "Awards Title visible");

        public readonly ICriteria<ActAwardsPage> AddAwardTitleLblVisible = new Criteria<ActAwardsPage>(p =>
        {
            return p.Exists(Bys.ActAwardsPage.AddAwardTitleLbl, ElementCriteria.IsVisible);

        }, "Awards Title visible");
        public readonly ICriteria<ActAwardsPage> LoadIconNotExists = new Criteria<ActAwardsPage>(p =>
        {
            return !p.Exists(Bys.Page.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActAwardsPage> PageReady;
        public ActAwardsPageCriteria()
        {
        
            PageReady = AwardsTitleLblVisible;
        }
    }
}
