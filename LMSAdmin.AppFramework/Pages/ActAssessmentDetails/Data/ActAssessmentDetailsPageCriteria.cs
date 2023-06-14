using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class ActAssessmentDetailsPageCriteria
    {
        public readonly ICriteria<ActAssessmentDetailsPage> DetailsTabAssessTypeSelElemVisible = new Criteria<ActAssessmentDetailsPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentDetailsPage.DetailsTabAssTypeSelElem, ElementCriteria.IsVisible);

        }, "Details tab, Assessment Type select element visible");

        public readonly ICriteria<ActAssessmentDetailsPage> PageReady;
        public ActAssessmentDetailsPageCriteria()
        {
            PageReady = DetailsTabAssessTypeSelElemVisible;
        }
    }
}
