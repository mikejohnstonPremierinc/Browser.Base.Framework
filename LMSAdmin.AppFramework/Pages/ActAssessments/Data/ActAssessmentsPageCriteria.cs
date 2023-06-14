using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class ActAssessmentsPageCriteria
    {
        public readonly ICriteria<ActAssessmentsPage> AddNewAssessmentLnkVisible = new Criteria<ActAssessmentsPage>(p =>
        {
            return p.Exists(Bys.ActAssessmentsPage.AddNewAssessmentLnk, ElementCriteria.IsVisible);

        }, "Add New Assessment Link visible");

        public readonly ICriteria<ActAssessmentsPage> PageReady;
        public ActAssessmentsPageCriteria()
        {
            PageReady = AddNewAssessmentLnkVisible;
        }
    }
}
