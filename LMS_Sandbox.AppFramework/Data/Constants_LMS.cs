using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using Browser.Core.Framework;

namespace LMS.AppFramework.Constants_
{
    public static class Constants_LMS
    {
        /// <summary>
        /// The connection string to the database of UAMS
        /// </summary>
        public static readonly string SQLconnString = AppSettings.Config["SQLConnectionString"];
    }

}