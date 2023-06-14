using Browser.Core.Framework;

namespace LMSAdmin.AppFramework
{
    public class ActCompletionPathwayPageCriteria
    {
        public readonly ICriteria<ActCompletionPathwayPage> ScenarioSettingsTabVisible = new Criteria<ActCompletionPathwayPage>(p =>
        {
            return p.Exists(Bys.ActCompletionPathwayPage.ScenarioSettingsTab, ElementCriteria.IsVisible);

        }, "ScenarioSettingsTab is visible");

        public readonly ICriteria<ActCompletionPathwayPage> DeliverySettingsTabVisible = new Criteria<ActCompletionPathwayPage>(p =>
        {
            return p.Exists(Bys.ActCompletionPathwayPage.DeliverySettingsTab, ElementCriteria.IsVisible);

        }, "DeliverySettingsTab is visible");


        /// <summary>
        /// The following criteria does not need to be met for the page load. But we can define more criteria below to use
        /// throughout our framework for specific instances where one of them needs to be met
        /// </summary>

        public readonly ICriteria<ActCompletionPathwayPage> LoadIconNotExists = new Criteria<ActCompletionPathwayPage>(p =>
        {
            return !p.Exists(Bys.Page.LoadIcon);

        }, "Load icon not exists");


        public readonly ICriteria<ActCompletionPathwayPage> PageReady;

        public ActCompletionPathwayPageCriteria()
        {
            PageReady = ScenarioSettingsTabVisible.AND(DeliverySettingsTabVisible);
        }
    }
}
