using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActPaymentPageCriteria
    {
        public readonly ICriteria<ActPaymentPage> LoadIcon_RegistrationVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.LoadIcon_Registration, ElementCriteria.IsVisible);// ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Registration page load icon visible");

        public readonly ICriteria<ActPaymentPage> LoadIcon_RegistrationNotVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.LoadIcon_Registration, ElementCriteria.IsNotVisible);// ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Registration page load icon not visible");

        public readonly ICriteria<ActPaymentPage> LoadIcon_PaymentVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.LoadIcon_Payment, ElementCriteria.IsVisible);// ElementCriteria.AttributeValue("aria-hidden", "false"));

        }, "Payment page load icon visible");

        public readonly ICriteria<ActPaymentPage> LoadIcon_PaymentNotVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.LoadIcon_Payment, ElementCriteria.IsNotVisible);// ElementCriteria.AttributeValue("aria-hidden", "true"));

        }, "Payment page load icon not visible");

        public readonly ICriteria<ActPaymentPage> UseADiscountCodeLnkVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.UseADiscountCodeLnk, ElementCriteria.IsVisible);

        }, "Use A Discount Code link visible");

        public readonly ICriteria<ActPaymentPage> SubmitRegistrationBtnVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.SubmitRegistrationBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Submit registration button link visible and Enabled");

        public readonly ICriteria<ActPaymentPage> SubmitPaymentBtnvisibled = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.SubmitBtn, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Submit payment button link visible and Enabled");

        public readonly ICriteria<ActPaymentPage> OkBtnVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.OkBtn, ElementCriteria.IsVisible);

        }, "Ok button visible");

        public readonly ICriteria<ActPaymentPage> AccessCodeTxtVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.AccessCodeTxt, ElementCriteria.IsVisible);

        }, "Access Code text box visible");

        public readonly ICriteria<ActPaymentPage> VerifyYourProfessionFormSubmitBtnVisible = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn, ElementCriteria.IsVisible);

        }, "Verify Your Profession form Submit button visible");

        public readonly ICriteria<ActPaymentPage> VerifyYourProfessionFormSubmitBtnNotExists = new Criteria<ActPaymentPage>(p =>
        {
            return !p.Exists(Bys.LMSPage.VerifyYourProfessionFormSubmitBtn);

        }, "Verify your profession form, Submit button not exists");

        public readonly ICriteria<ActPaymentPage> AreYouCHESNoRdoVvisibledAndEnabled = new Criteria<ActPaymentPage>(p =>
        {
            return p.Exists(Bys.ActPaymentPage.AreYouCHESNoRdo, ElementCriteria.IsVisible, ElementCriteria.IsEnabled);

        }, "Are you CHES radio button visible and Enabled");

        public readonly ICriteria<ActPaymentPage> PageReady;

        public ActPaymentPageCriteria()
        {
            PageReady = SubmitRegistrationBtnVisible.OR(SubmitPaymentBtnvisibled).OR(OkBtnVisible).OR(AccessCodeTxtVisible)
                .OR(VerifyYourProfessionFormSubmitBtnVisible);
        }
    }
}
