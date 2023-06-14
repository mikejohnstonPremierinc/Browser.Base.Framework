using Browser.Core.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using LMS.AppFramework;
using LMS.AppFramework.LMSHelperMethods;
using LMS.AppFramework.Constants_;
using System.Configuration;

namespace LMS.AppFramework
{
    public static class DBUtils_UAMS
    {
        private static readonly DataAccess _WebAppDbAccess = new DataAccess(new SqlServerDataAccessProvider(AppSettings.Config["SQLConnectionString"]));
        //private static readonly DataAccess _WebAppDbAccess = new DataAccess(new MySqlDataAccessProvider(AppSettings.Config["SQLConnectionString"]));


        #region Queries









        #endregion Queries
    }
}



