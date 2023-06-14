using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActCertificatePageCriteria
    {
        public readonly ICriteria<ActCertificatePage> CertificateObjectVisible = new Criteria<ActCertificatePage>(p =>
        {
            return p.Exists(Bys.ActCertificatePage.CertificateObject, ElementCriteria.IsVisible);

        }, "Certificate object visible");

        public readonly ICriteria<ActCertificatePage> NoCertificatesGeneratedLblVisible = new Criteria<ActCertificatePage>(p =>
        {
            return p.Exists(Bys.ActCertificatePage.NoCertificatesGeneratedLbl, ElementCriteria.IsVisible);

        }, "No Certificates Generated label visible");

        public readonly ICriteria<ActCertificatePage> FinishBtnVisibleAndEnabled = new Criteria<ActCertificatePage>(p =>
        {
            return p.Exists(Bys.ActCertificatePage.FinishBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Finish button visible and enabled");

        public readonly ICriteria<ActCertificatePage> LoadIconNotExists = new Criteria<ActCertificatePage>(p =>
        {
            return !p.Exists(Bys.LMSPage.LoadIcon);

        }, "Load icon not exists");

        public readonly ICriteria<ActCertificatePage> ContinueBtnVisibleAndEnabled = new Criteria<ActCertificatePage>(p =>
        {
            return p.Exists(Bys.ActCertificatePage.ContinueBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Continue button visible and enabled");

        public readonly ICriteria<ActCertificatePage> PageReady;

        public ActCertificatePageCriteria()
        {
            PageReady = (CertificateObjectVisible.AND(LoadIconNotExists).AND(FinishBtnVisibleAndEnabled))
                .OR(CertificateObjectVisible.AND(LoadIconNotExists).AND(ContinueBtnVisibleAndEnabled))
                .OR(NoCertificatesGeneratedLblVisible.AND(LoadIconNotExists).AND(FinishBtnVisibleAndEnabled))
                .OR(NoCertificatesGeneratedLblVisible.AND(LoadIconNotExists).AND(ContinueBtnVisibleAndEnabled));
        }
    }
}
