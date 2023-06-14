using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActivitiesInProgressPageCriteria
    {
        public readonly ICriteria<ActivitiesInProgressPage> ActivitiesinProgressLblVisible = new Criteria<ActivitiesInProgressPage>(p =>
        {
            return p.Exists(Bys.ActivitiesInProgressPage.ActivitiesInProgressLbl, ElementCriteria.IsVisible);

        }, "My Activities in Progress Label visible");

        public readonly ICriteria<ActivitiesInProgressPage> LoadIconNotExists = new Criteria<ActivitiesInProgressPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActivitiesInProgressPage> NoDataAvailableLblVisible = new Criteria<ActivitiesInProgressPage>(p =>
        {
            return p.Exists(Bys.ActivitiesInProgressPage.YouAreCurrentlyNotParticipatingLbl);

        }, "No data available label visible");

        public readonly ICriteria<ActivitiesInProgressPage> ActivitiesTblFirstLnkVisible = new Criteria<ActivitiesInProgressPage>(p =>
        {
            return p.Exists(Bys.ActivitiesInProgressPage.ActivitiesTblFirstLnk, ElementCriteria.IsVisible);

        }, "My Activities table, first row visible");

        public readonly ICriteria<ActivitiesInProgressPage> PageReady;

        public ActivitiesInProgressPageCriteria()
        {
            PageReady = ActivitiesinProgressLblVisible.AND(LoadIconNotExists).AND(ActivitiesTblFirstLnkVisible).
                OR(ActivitiesinProgressLblVisible.AND(LoadIconNotExists).AND(NoDataAvailableLblVisible));
        }
    }
}
