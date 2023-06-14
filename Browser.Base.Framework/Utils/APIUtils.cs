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
using System.Threading;
using System.Threading.Tasks;
//

namespace Browser.Core.Framework
{
    public static class APIUtils
    {

        #region properties

        #endregion properties        

        #region methods

        /// <summary>
        /// hguygyhg
        /// </summary>
        /// <param name="fullAPIUrl"></param>
        /// <param name="body"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string ExecuteAPI_Post(string fullAPIUrl, string body, string token = null)
        {
            // This is the shared code inside Browser.Base.Framework that you can call to pass your API URL and body.
            // This will execute the API, and return the response of the API (if you want a response). Please return to the
            // CreateUser method for more notes
            string response = string.Empty;

            using (var wc = new WebDownload())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                wc.Headers.Add("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(token))
                {
                    wc.Headers.Add("Token", token);
                }
                wc.Headers.Add("Cookie", "a78e046d-cb30-474d-8ede-23aa9a340702");
                wc.Headers.Add("X-_af", "a78e046d-cb30-474d-8ede-23aa9a340702");
                response = wc.UploadString(fullAPIUrl, body);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullAPIUrl"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string ExecuteAPI_Get(string fullAPIUrl, string token)
        {
            string response = string.Empty;
            
            using (var wc = new WebDownload())
            {
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("Token", token);
                response = wc.DownloadString(fullAPIUrl);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullAPIUrl"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string ExecuteAPI_Delete(string fullAPIUrl, string token)
        {
            var response = string.Empty;

            using (var wc = new WebDownload())
            {
                wc.Headers.Add("ContentType", "application/json");
                wc.Headers.Add("Token", token);
                wc.Headers.Add("Cookie", "a78e046d-cb30-474d-8ede-23aa9a340702");
                wc.Headers.Add("X-_af", "a78e046d-cb30-474d-8ede-23aa9a340702");
                // https://stackoverflow.com/questions/14052218/connection-that-was-expected-to-be-kept-alive-was-closed-by-the-server-in-asp-ne
                System.Net.ServicePointManager.Expect100Continue = false;
                response = wc.UploadString(fullAPIUrl, "DELETE", string.Empty);
            }

            return response;
        }


        #endregion methods

        #region API Objects


        #endregion API Objects


    }

    /// <summary>
    /// This is just setting a timeout
    /// https://stackoverflow.com/questions/1789627/how-to-change-the-timeout-on-a-net-webclient-object
    /// </summary>
    public class WebDownload : WebClient
    {
        /// <summary>
        /// Time in milliseconds
        /// </summary>
        public int Timeout { get; set; }

        // in milliseconds
        public WebDownload() : this(240000) { }

        public WebDownload(int timeout)
        {
            this.Timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = this.Timeout;
            }
            return request;
        }
    }

}
