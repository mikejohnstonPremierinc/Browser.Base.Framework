using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace Browser.Core.Framework
{
    [Serializable]
    public class UserModel
    {
        public UserModel()
        {
            Fields = new Field[] { };
        }

        // All of the user information that is shared for all LMS sites
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public dynamic Guid { get; set; }
        public string Degree { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string Profession { get; set; }
        public Field[] Fields { get; set; }
    }

    /// <summary>
    /// XtensibleInfo item. Used when we can specify multiple different Values for the Name/Key. This is used for the custom-per-LMSsite user
    /// information. These Name/Value's will be built inside your application's UserUtils class
    /// </summary>
    [Serializable]
    public class Field
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}




