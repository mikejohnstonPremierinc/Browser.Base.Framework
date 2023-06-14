using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    public class EntryCarouselPathwayPageCriteria
    {
        public readonly ICriteria<EntryCarouselPathwayPage> EnterBtnVisible = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return p.Exists(Bys.EntryCarouselPathwayPage.EnterBtn, ElementCriteria.IsVisible);

        }, "Enter button visible");

        public readonly ICriteria<EntryCarouselPathwayPage> PLPActivityOverviewLblVisible = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return p.Exists(Bys.EntryCarouselPathwayPage.PLPActivityOverviewLbl, ElementCriteria.IsVisible);

        }, "PLP Activity Overview label visible");

        public readonly ICriteria<EntryCarouselPathwayPage> SelfGuidedModalPleaseConfirmFormConfirmBtnVisible = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return p.Exists(Bys.EntryCarouselPathwayPage.SelfGuidedModalPleaseConfirmFormConfirmBtn, ElementCriteria.IsVisible);

        }, "Self Guided Please Confirm form Confirm button visible");

        public readonly ICriteria<EntryCarouselPathwayPage> PeerSupportedPleaseConfirmFormConfirmBtnVisible = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return p.Exists(Bys.EntryCarouselPathwayPage.PeerSupportedPleaseConfirmFormConfirmBtn, ElementCriteria.IsVisible);

        }, "Peer Supported Please Confirm form Confirm button visible");

        public readonly ICriteria<EntryCarouselPathwayPage> PeerSupportedBeginBtnVisible = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return p.Exists(Bys.EntryCarouselPathwayPage.PeerSupportedBeginBtn, ElementCriteria.IsVisible);

        }, "Peer Supported Begin button visible");

        public readonly ICriteria<EntryCarouselPathwayPage> ImageContainerNotVisible = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return p.Exists(Bys.EntryCarouselPathwayPage.ImageContainer, ElementCriteria.IsNotVisible);

        }, "Image Container not visible");

        public readonly ICriteria<EntryCarouselPathwayPage> LoadIconNotExists = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<EntryCarouselPathwayPage> LoadIconOverlayNotExists = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.LoadIconOverlay);

        }, "Load overlay not exists");

        public readonly ICriteria<EntryCarouselPathwayPage> WereSorryErrorLblNotExists = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return !p.Exists(Bys.MainproPage.WereSorryErrorLbl);

        }, "We're sorry, we cannot retrieve your information label not exists");

        public readonly ICriteria<EntryCarouselPathwayPage> PLPMenuDropDownExistsAndIsVisible = new Criteria<EntryCarouselPathwayPage>(p =>
        {
            return p.Exists(Bys.MainproPage.PLP_Menu_DropDownBtn, ElementCriteria.IsVisible);

        }, "PLP menu dropdown exists and is visible");


        public readonly ICriteria<EntryCarouselPathwayPage> PageReady;

        public EntryCarouselPathwayPageCriteria()
        {
            PageReady = 
                // Entry page
                (EnterBtnVisible.AND(LoadIconNotExists).AND(LoadIconOverlayNotExists)).
                // Any one of the pages after entry. After the user clicks the Enter button to go to PLP, the application 
                // can land on any page within PLP depending on where the user left off previously           
                OR(PLPMenuDropDownExistsAndIsVisible.AND(LoadIconNotExists).AND(LoadIconOverlayNotExists));
                //OR(PLPActivityOverviewLblVisible.AND(LoadIconNotExists).AND(LoadIconOverlayNotExists));
        }
    }
}
