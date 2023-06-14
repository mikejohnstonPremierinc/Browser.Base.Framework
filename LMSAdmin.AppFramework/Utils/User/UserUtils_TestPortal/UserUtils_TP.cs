using Browser.Core.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LMSAdmin.AppFramework
{
    public static class UserUtils_TP
    {
        #region properties

        #region static users

        public static string TestPortalUser_UserName1 = "testauto_user1";

        #endregion static users


        static string baseAPIUrl = AppSettings.Config["APIUrl"].ToString();

        // I changed this to be a static token, less room for test failure if we dont have to call generatetoken every single time
        // If the static token doesnt work, get a new static token from DEV or do it manually
        static string token = AppSettings.Config["APIToken_TestPortal"].ToString();

        #endregion properties

        #region methods

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="customUserName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customEmailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customFirstName">(Optional) If needed, you can specify a first name of your choice. If not needed, leave this null and the first name will be generated for you</param>
        /// <param name="customLastName">(Optional) If needed, you can specify a last name of your choice. If not needed, leave this null and the last name will be generated for you</param>
        /// <returns>An object that contains all the user's information, such as username, full name (first and last name), email, etc.</returns>
        public static UserInfo_TP CreateUser(string customUserName = "", string customEmailAddress = "", string customFirstName = null, string customLastName = null)
        {
            string resp = string.Empty;

            UserInfo_TP newUserModel = BuildUserModel(customUserName, customEmailAddress, customFirstName, customLastName);

            using (var wc = new WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                String fullAPIUrl = String.Format("{0}api/user", baseAPIUrl);
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Token", token);
                string body = JsonConvert.SerializeObject(newUserModel);
                resp = wc.UploadString(fullAPIUrl, body);
            }

            // Add the guid to the userinfo object
            dynamic data = JObject.Parse(resp);
            newUserModel.Guid = data.Id.ToString();

            return newUserModel;
        }

        /// <summary>
        /// Builds the user information that then gets plugged into the API request's body
        /// </summary>
        /// <param name="customUserName">(Optional) If needed, you can specify a username of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customEmailAddress">(Optional) If needed, you can specify an email of your choice. If not needed, leave this null and the username will be generated for you</param>
        /// <param name="customFirstName">(Optional) If needed, you can specify a first name of your choice. If not needed, leave this null and the first name will be generated for you</param>
        /// <param name="customLastName">(Optional) If needed, you can specify a last name of your choice. If not needed, leave this null and the last name will be generated for you</param>
        /// <returns></returns>
        private static UserInfo_TP BuildUserModel(string customUserName = "", string customEmailAddress = "", string customFirstName = null, string customLastName = null)
        {
            string userName = "";
            string emailAddress = "";
            string firstName = "";
            string lastName = "";
            string nationality = "";
            string currentDate = string.Format("{0}_", DateTime.Now.ToString("MdyyyyHHmmss", CultureInfo.InvariantCulture));

            // Set the email address, user name, first and last name
            if (string.IsNullOrEmpty(customEmailAddress))
            {
                emailAddress = "NotARealEmail@mailinator.com";
            }
            else
            {
                emailAddress = customEmailAddress;
            }

            if (string.IsNullOrEmpty(customUserName))
            {
                userName = "TA_" + currentDate + DataUtils.GetRandomString(3);
            }
            else
            {
                userName = customUserName;
            }

            if (string.IsNullOrEmpty(customFirstName))
            {
                firstName = userName.Substring(0, 11);
            }
            else
            {
                firstName = customFirstName;
            }

            if (string.IsNullOrEmpty(customLastName))
            {
                lastName = userName.Substring(11);
            }
            else
            {
                lastName = customLastName;
            }

            UserInfo_TP newUserModel = new UserInfo_TP();
            newUserModel.Username = userName;
            newUserModel.Password = "test";
            newUserModel.FirstName = firstName;
            newUserModel.LastName = lastName;
            newUserModel.FullName = string.Format("{0} {1}", newUserModel.FirstName, newUserModel.LastName);
            newUserModel.Degree = "test";
            newUserModel.EmailAddress = emailAddress;
            newUserModel.Address = "Address01";
            newUserModel.Address2 = "Address02";
            newUserModel.City = "Pittsburgh";
            newUserModel.State = "Pennsylvania";
            newUserModel.PostalCode = "15206";
            newUserModel.CountryCode = "United States";
            newUserModel.Fields = new Field[] {
                new Field() { Name = "profession", Value = "PHY" },
                new Field() { Name = "language", Value = nationality },
                new Field() { Name = "PARTICIPANT_ID", Value = userName },
                new Field() { Name = "Specialty", Value = "Phy" }
            };

            return newUserModel;
        }

        private static String GetToken(String siteCode = null, String accountKey = null, String password = null)
        {
            if (AppSettings.Config["APIUrl"] == null) throw new Exception("Appsetting 'APIUrl' is missing.");
            accountKey = accountKey ?? AppSettings.Config["APIAccountKey"];
            password = password ?? AppSettings.Config["APIAccountPassword"];
            string baseUrl = AppSettings.Config["APIUrl"].ToString();
            var postData = new { AccountKey = accountKey, Password = password, SiteCode = siteCode };

            var postString = JsonConvert.SerializeObject(postData);
            var tokenResponse = new { Token = "", Expiration = "" };
            using (var wc = new WebClient())
            {
                String url =
                    String.Format("{0}/api/AccessToken", baseUrl);
                wc.Headers.Add("content-type", "application/json");
                wc.Headers.Add("accept", "application/json");
                var tokenModel = wc.UploadString(url, postString);
                var tokenAnon = JsonConvert.DeserializeAnonymousType(tokenModel, tokenResponse);
                return tokenAnon.Token;
            }
        }

        #endregion methods


    }
}
