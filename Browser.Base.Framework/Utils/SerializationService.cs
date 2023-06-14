//using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//

namespace Browser.Core.Framework
{
    public class SerializationService
    {
        public static List<T> DeserializeJson<T>(string json)
        {
            //try
            //{
                List<T> ToBeReturned = new List<T>();
                if (json.Substring(0, 1) != "[")
                {
                // MJ 1/13/2022: I had to remove the Nancy package (which allowed me to use JavaScriptSerializer) because
                // Nancy wasnt updated and so it had a security risk in one of its libraries, per Blackduck scans. I have not
                // tested the code I added to replace it (JsonConvert), but I think it will work if/when needed. If not,
                // revert back to Nancy and see if it has new versions
                //var s = new JavaScriptSerializer();
                //ToBeReturned.Add(s.Deserialize<T>(json));

                ToBeReturned.Add(JsonConvert.DeserializeObject<T>(json));  
                }
                else
                {
                //var s = new JavaScriptSerializer();
                //ToBeReturned = s.Deserialize<List<T>>(json);
                ToBeReturned = JsonConvert.DeserializeObject<List<T>>(json);
                }
                return ToBeReturned;

            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
        }
    }
}
