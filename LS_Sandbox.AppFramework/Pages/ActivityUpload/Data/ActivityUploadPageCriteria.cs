using Browser.Core.Framework;

namespace LS.AppFramework
{
    public class ActivityUploadPageCriteria
    {
        public readonly ICriteria<ActivityUploadPage> ChooseFileUploadElemExists = new Criteria<ActivityUploadPage>(p =>
        {
            return p.Exists(Bys.ActivityUploadPage.ChooseFileUploadElem);

        }, "Choose File upload element exists");

        public readonly ICriteria<ActivityUploadPage> UploadTblVisible = new Criteria<ActivityUploadPage>(p =>
        {
            return p.Exists(Bys.ActivityUploadPage.UploadTbl, ElementCriteria.IsVisible);

        }, "Upload table visible");

        public readonly ICriteria<ActivityUploadPage> PageReady;

        public ActivityUploadPageCriteria()
        {
            PageReady = ChooseFileUploadElemExists.AND(UploadTblVisible);
        }
    }
}
