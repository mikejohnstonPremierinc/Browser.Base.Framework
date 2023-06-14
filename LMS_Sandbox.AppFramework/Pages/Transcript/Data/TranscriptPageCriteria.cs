using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class TranscriptPageCriteria
    {
        public readonly ICriteria<TranscriptPage> TranscriptLblVisible = new Criteria<TranscriptPage>(p =>
        {
            return p.Exists(Bys.TranscriptPage.TranscriptLbl, ElementCriteria.IsVisible);

        }, "Transcript Label visible");

        public readonly ICriteria<TranscriptPage> LoadIconNotExists = new Criteria<TranscriptPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<TranscriptPage> ActivitiesTblFirstLnkVisible = new Criteria<TranscriptPage>(p =>
        {
            return p.Exists(Bys.TranscriptPage.ActivitiesTblFirstLnk, ElementCriteria.IsVisible);

        }, "Activities table, first row visible");

        public readonly ICriteria<TranscriptPage> YouDoNotHaveAnyComplActsLblVisible = new Criteria<TranscriptPage>(p =>
        {
            return p.Exists(Bys.TranscriptPage.YouDoNotHaveAnyComplActsLbl, ElementCriteria.IsVisible);

        }, "No Data Available label visible");

        public readonly ICriteria<TranscriptPage> PageReady;

        public TranscriptPageCriteria()
        {
            PageReady = TranscriptLblVisible.AND(LoadIconNotExists).AND(ActivitiesTblFirstLnkVisible).
                OR(TranscriptLblVisible.AND(LoadIconNotExists).AND(YouDoNotHaveAnyComplActsLblVisible));
        }
    }
}
