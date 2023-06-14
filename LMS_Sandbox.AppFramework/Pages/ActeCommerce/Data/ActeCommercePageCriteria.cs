using Browser.Core.Framework;

namespace LMS.AppFramework
{
    public class ActeCommercePageCriteria
    {
        public readonly ICriteria<ActeCommercePage> CountrySelElemHasMoreThan1Item = new Criteria<ActeCommercePage>(p =>
        {
            return p.Exists(Bys.ActeCommercePage.CountrySelElem, ElementCriteria.SelectElementHasMoreThan1Item);

        }, "Country select element has more than 1 item");


        public readonly ICriteria<ActeCommercePage> PageReady;

        public ActeCommercePageCriteria()
        {
            PageReady = CountrySelElemHasMoreThan1Item;
        }
    }
}
