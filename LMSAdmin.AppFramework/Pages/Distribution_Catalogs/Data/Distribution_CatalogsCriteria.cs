using Browser.Core.Framework;
using LMS.Data;

namespace LMSAdmin.AppFramework
{
    public class Distribution_CatalogsPageCriteria
    {
      

        public readonly ICriteria<Distribution_CatalogsPage> AddNewCatalogLnkVisible = new Criteria<Distribution_CatalogsPage>(p =>
        {
            return p.Exists(Bys.Distribution_CatalogsPage.AddNewCatalogLnk, ElementCriteria.IsVisible);

        }, "AddnewCatalog Link visible");

        public readonly ICriteria<Distribution_CatalogsPage> DetailsTabCatalogNameTxtVisible = new Criteria<Distribution_CatalogsPage>(p =>
        {
            return p.Exists(Bys.Distribution_CatalogsPage.DetailsTabCatalogNameTxt, ElementCriteria.IsVisible);

        }, "Details tab, Catalog Name textbox visible");


        public readonly ICriteria<Distribution_CatalogsPage> PageReady;
        
        public Distribution_CatalogsPageCriteria()
        {
            PageReady = AddNewCatalogLnkVisible;
        }
    }
}
