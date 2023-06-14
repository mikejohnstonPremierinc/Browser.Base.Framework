using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace LMSAdmin.AppFramework
{
    public abstract class LegacyModel
    {
        public abstract byte[] ToByteArray();
    }

    /// <summary>
    /// XtensibleInfo item
    /// </summary>
    [Serializable]
    public class Field
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// this model is used as a pass through to the old API to register a user
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "User")]
    public class UserInfo_TP
    {
        public UserInfo_TP()
        {
            Fields = new Field[] { };
        }

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
        public Field[] Fields { get; set; }

    }
}




