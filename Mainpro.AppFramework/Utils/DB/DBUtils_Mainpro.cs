using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Browser.Core.Framework;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Queries/scripts for Mainpro
    /// </summary>
    public static class DBUtils_Mainpro
    {
        #region properties

        //private static readonly DataAccess _WebAppDbAccess = new DataAccess(new SqlServerDataAccessProvider(AppSettings.Config["SQLConnectionString"]));

        private static DataAccess _WebAppDbAccess = null;

        private static string p_SQLconnString = string.Empty;
        public static string SQLconnString
        {
            get
            {
                return p_SQLconnString;
            }
            set
            {
                p_SQLconnString = value;
                _WebAppDbAccess = new DataAccess(new SqlServerDataAccessProvider(value));

            }
        }

        public static TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        public static DateTime currentDatetime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, est);

        #endregion properties

        #region methods

        /// <summary>
        /// Sets the CFPCMainPro+ GracePeriodEndDate to yesterday's date, so that if the user complete the current cycle, 
        /// it will be automatically rollover to next applicable cycle  
        /// NOTE: We can NOT not use this method for UAT /Production Environemnts. And everytime we use this query, 
        /// afterward we have to set the date back to the default date. See the method titled 
        /// SetGracePeriodEndDateToDefaultDate to set it back. That method will be called at the test class level
        /// after every test that uses this method
        /// </summary>       
        public static void SetGracePeriodEndDateToYesterdayDate()
        {
            String Yesterdaydate = currentDatetime.AddDays(-1).ToString("MM-dd");  //DateTime.Now ;changed this for converting Date to EST

            string query = string.Format(@"
UPDATE oa
SET oa.attribute_value = '{0}'
FROM dbo.organization_attribute oa
    INNER JOIN dbo.organization_attribute_available oaa
        ON oa.organization_attribute_seq = oaa.organization_attribute_seq
WHERE oaa.attribute_name = 'CFPCMainPro+GracePeriodEndDate'
AND oa.organization_seq = 926", Yesterdaydate);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }
		
		        /// <summary>
        /// Removes all requests from the Requests tab on LTST. Execute this after running any auto test in prod
        /// </summary>       
        public static void RemoveLTSTRequests()
        {
            string query = string.Format(@"
update apv
set apv.IsValid = 1
from CommandCenter.dbo.activity_participant_validation apv with(nolock)
inner join commandcenter.dbo.activity_participant ap with(nolock) ON apv.activity_participant_seq = ap.activity_participant_seq
inner join commandcenter.dbo.activity a with(nolock) ON ap.activity_seq = a.activity_seq
inner join commandcenter.dbo.site_participant sp with(nolock) ON ap.participant_seq = sp.participant_Seq
where apv.IsSubmittedForValidation = 1 and apv.IsValid is null and IsAdditionalMaterialExpected = 0 and sp.username like '%TestAuto%' AND sp.site_seq = 7438");

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        /// <summary>
        /// Sets the CFPCMainPro+ GracePeriodEndDate back to original default date which is August 15th. In some tests,
        /// we are setting this date to before august 15th. We do this to hack the system for advancing a user to 
        /// his/her next cycle. The system only advances a user on or after august 15th, so to test, we have to 
        /// manually make this date to current date - 1 day inside our tests
        /// IMPORTANT: We can NOT not use this method for UAT /Production Environemnts because it may alter the client's
        /// test data/users cycles. Also, do NOT call this method inside 2 different classes that would execute 
        /// in parallel with eachother, as the tests will interfere with eachother when changing this DB value
        ///</summary>
        public static void SetGracePeriodEndDateToDefaultDate()
        {
            String DefaultDate = "08-15";

            string query = string.Format(@"
UPDATE oa
SET oa.attribute_value = '{0}'
FROM dbo.organization_attribute oa
    INNER JOIN dbo.organization_attribute_available oaa
        ON oa.organization_attribute_seq = oaa.organization_attribute_seq
WHERE oaa.attribute_name = 'CFPCMainPro+GracePeriodEndDate'
AND oa.organization_seq = 926", DefaultDate);

            _WebAppDbAccess.ExecuteNonQuery(query, 120);
        }

        #endregion methods
    }
}



