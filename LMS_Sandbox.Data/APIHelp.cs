using Browser.Core.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LMS.Data
{
    public static class APIHelp
    {

        #region properties

        #endregion properties        

        #region methods

        /// <summary>
        /// Deletes/Unregisters an activity for a user, so that he/she can register and take it again. If you get a 500 error, then 
        /// you need to do 1 or 2 things to fix this
        /// 1. Add the proper endpoint to your API Account's list of endpoints. To do that, login to LTS and go to Administration tab, 
        /// then go to Manage API Account and click on Endpoints for your API Account. Add this in the 
        /// text box /api/activityparticipant/(.*)/delete [DELETE]
        /// 2. Execute the following script to set your API account to the proper account type:
        /// UPDATE Security.api.Account SET AccountType = 3 WHERE[Key] = 'TestAuto_CMECA'
        /// </summary>
        /// <param name="username">The LMS username you want to get the access token for</param>
        /// <param name="activityTitle"></param>
        /// <param name="siteCode"><see cref="Constants.SiteCodes"/></param>
        public static string DeleteActivityForUser(string username, string activityTitle, Constants.SiteCodes siteCode,
            int timeToWaitInMilliseconds = 0)
        {
            //// Optionally sleeping here until the Delete Activity bug below is resolved. Until then, the API will timeout if 
            //// PIM tests are executed in parallel, so the tester can delay the API call if he passes the parameter in this method
            //// https://code.premierinc.com/issues/browse/LMSREW-1168
            //// https://code.premierinc.com/issues/browse/LMSREW-387
            //Thread.Sleep(timeToWaitInMilliseconds);

            if (AppSettings.Config["APIUrl"] == null) throw new Exception("Appsetting 'APIUrl' is missing.");

            String fullAPIUrl = String.Format("{0}api/activityparticipant/{1}/delete", AppSettings.Config["APIUrl"].ToString(),
                DBUtils.GetActivityID(siteCode, activityTitle));

             string accountKey = "testauto_premier";
           // string accountKey = string.Format("{0}_{1}", AppSettings.Config["APIAccountKey"], siteCode);

            string userAccessToken = APIHelp.GetUserAccessToken(siteCode, accountKey,
                AppSettings.Config["APIAccountPassword"].ToString(), username);

            string response = string.Empty;
            string token = string.IsNullOrEmpty(userAccessToken) ? GetToken(AppSettings.Config["APIUrl"].ToString(), siteCode.ToString(),
                                                            AppSettings.Config["APIAccountKey"],
                                                            AppSettings.Config["APIAccountPassword"])
                                                            : userAccessToken;

            // We are using a try catch here until the Delete Activity bug below is resolved. Until then, the API
            // will timeout if PIM tests are executed in parallel
            // https://code.premierinc.com/issues/browse/LMSREW-1168
            // https://code.premierinc.com/issues/browse/LMSREW-387
            for (int i = 0; i < 24; i++)
            {
                try
                {
                    response = APIUtils.ExecuteAPI_Delete(fullAPIUrl, token);
                    break;
                }
                catch
                {
                    // Only execute this if on the 23rd instance. Meaning if we tried to delete the activity for
                    // a user 22 times and if failed every time, then this will fail the test. If it failed on
                    // 21st times, but was successful on the 22nd try, it will proceed with the test
                    if (i == 23)
                    {
                        response = APIUtils.ExecuteAPI_Delete(fullAPIUrl, token);
                    }
                }
                Thread.Sleep(10000);
            }

            return response;
        }

        /// <summary>
        /// Gets the access token for you to be able to execute any LMS API
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="siteCode"></param>
        /// <param name="accountKey"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static String GetToken(string baseURL, String siteCode, String accountKey, String password)
        {
            var bodyData = new { AccountKey = accountKey, Password = password, SiteCode = siteCode };
            string body = JsonConvert.SerializeObject(bodyData);
            string fullAPIUrl = baseURL + "api/accessToken";
            var tokenResponse = new { Token = "", Expiration = "" };

            var tokenModel = APIUtils.ExecuteAPI_Post(fullAPIUrl, body);

            var tokenAnon = JsonConvert.DeserializeAnonymousType(tokenModel, tokenResponse);

            return tokenAnon.Token;
        }

        /// <summary>
        /// Gets the user access token for you to be able to specify a user for any API call that is user-related 
        /// (i.e. delete/unregister a user
        /// from an activity)
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="siteCode"></param>
        /// <param name="accountKey"></param>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static String GetUserAccessToken(Constants.SiteCodes siteCode, String accountKey, String password, String username)
        {
            var bodyData = new { AccountKey = accountKey, Password = password, SiteCode = siteCode.GetDescription(), UserName = username };
            string body = JsonConvert.SerializeObject(bodyData);
            String fullAPIUrl = String.Format("{0}api/accessToken", AppSettings.Config["APIUrl"].ToString());
            var tokenResponse = new { Token = "", Expiration = "" };

            var tokenModel = APIUtils.ExecuteAPI_Post(fullAPIUrl, body);

            var tokenAnon = JsonConvert.DeserializeAnonymousType(tokenModel, tokenResponse);

            return tokenAnon.Token;
        }

        /// <summary>
        /// Gets the user access token from the browser cookie. Useful if you dont have the username and/or other things 
        /// necessary to get this token when using <see cref="GetUserAccessToken(Constants.SiteCodes, string, string, string)"/>
        /// from an activity)
        /// </summary>
        /// <param name="Browser"></param>
        /// <returns></returns>
        public static string GetUserAccessTokenFromCookie(IWebDriver Browser)
        {
            var token = Browser.Manage().Cookies.GetCookieNamed("APIAccessToken");
            return token.Value;
        }


        /// <summary>
        /// Gets the username of the currently logged in user
        /// </summary>
        /// <returns></returns>
        public static string GetUserName(IWebDriver Browser)
        {
            string token = GetUserAccessTokenFromCookie(Browser);
            String fullAPIUrl = String.Format("{0}api/user/name", AppSettings.Config["APIUrl"].ToString());

            string username = APIUtils.ExecuteAPI_Get(fullAPIUrl, token);
            string usernameDoubleQuotesRemoved = username.Replace('"', ' ').Trim();
            return usernameDoubleQuotesRemoved;
        }



        #endregion methods

        #region API Objects


        #endregion API Objects


    }

}
