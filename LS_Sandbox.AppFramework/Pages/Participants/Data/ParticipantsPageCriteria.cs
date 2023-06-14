using Browser.Core.Framework;

namespace LS.AppFramework
{
    public class ParticipantsPageCriteria
    {
        public readonly ICriteria<ParticipantsPage> DetailsTabGuidLblVisible = new Criteria<ParticipantsPage>(p =>
        {
            return p.Exists(Bys.ParticipantsPage.DetailsTabGuidLbl, ElementCriteria.IsVisible);

        }, "Details tab, Guid label visible");

        public readonly ICriteria<ParticipantsPage> ProgramsTabProgramTblBodyRowVisible = new Criteria<ParticipantsPage>(p =>
        {
            return p.Exists(Bys.ParticipantsPage.ProgramsTabProgramTblBodyRow, ElementCriteria.IsVisible);

        }, "Programs tab, program table visible");

        public readonly ICriteria<ParticipantsPage> ProgramsTabRecognitionFormNotExists = new Criteria<ParticipantsPage>(p =>
        {
            return !p.Exists(Bys.ParticipantsPage.ProgramsTabRecognitionFormStartDtTxt);

        }, "Programs tab, Recognition form not exists");

        public readonly ICriteria<ParticipantsPage> RecognitionProgramCycleDateChangeBannerNotExists = new Criteria<ParticipantsPage>(p =>
        {
            return !p.Exists(Bys.Page.GreenBanner);

        }, "Recognition Program Cycle Date Change Banner not exists");
        

        public readonly ICriteria<ParticipantsPage> PageReady;

        public ParticipantsPageCriteria()
        {
            PageReady = DetailsTabGuidLblVisible;
        }
    }
}
