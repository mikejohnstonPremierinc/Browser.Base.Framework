using Browser.Core.Framework;
using LMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using LMSAdmin.AppFramework.ConstantsLMSAdmin;


namespace LMSAdmin.AppFramework
{
    public static class DbUtils_LMSAdmin
    {
        private static readonly DataAccess _WebAppDbAccess = new DataAccess(new SqlServerDataAccessProvider(Constants_LMSAdmin.SQLconnString));
        //private static readonly DataAccess _WebAppDbAccess = new DataAccess(new MySqlDataAccessProvider(Constants.SQLconnString));



        #region Methods

        /// <summary>
        /// Gets catalog name that has the most activities for a given portal/site. This is usually the catalog that is the most widely used on a portal
        /// </summary>
        /// <param name="siteCode">The code of the portal/site. i.e. 'UAMS' or 'TP'. Specifically, you can query the site_site table in the DB to get this code</param>
        /// <returns></returns>
        public static string GetCatalogForSite(string siteCode)
        {
            string query = string.Format(@";with cte AS
(
SELECT ROW_NUMBER() OVER (PARTITION BY r.site_code ORDER BY r.site_code, r.act_count DESC) [row], r.name, r.site_code, r.SiteName, r.act_count
FROM (
SELECT c.name, ss.site_code, ss.name [SiteName], count(ca.activity_seq) [act_count]
from 
[catalog] c with(nolock)
JOIN catalog_distribution_source cds with(nolock) on cds.catalog_seq=c.catalog_seq
JOIN distribution_source ds on cds.distribution_source_seq=cds.distribution_source_seq
JOIN site_site ss on ds.distribution_source_seq=ss.distribution_source_seq and ss.distribution_source_seq=cds.distribution_source_seq 
--AND ss.organization_seq = c.organization_seq -- Josh said to remove this condition
JOIN catalog_activity ca ON ca.catalog_seq = c.catalog_seq
where [c].[published_ind]='N' and ss.[published_version_ind]='N' 
AND c.active_ind='Y' AND c.deleted_ind = 'N' AND c.system_ind='N'
AND cds.active_ind='Y' AND ds.active_ind='Y' AND ds.deleted_ind='N'
AND ss.site_code='{0}'
GROUP BY c.name, ss.site_code, ss.name
)r
)

SELECT name [CatalogName], site_code, SiteName, act_count FROM cte
WHERE row=1
ORDER BY site_code
", siteCode);

            DataRow queryResult = _WebAppDbAccess.GetDataRow(query, 90);

            string catalog = queryResult.Field<string>("CatalogName");

            return catalog;
        }


        #endregion Methods


    }
}



