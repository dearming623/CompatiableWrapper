using System;
using System.Collections.Generic;
using System.Text;

namespace MQWebApi.Util
{
    public class JSON
    { 
        public static T ParseObject<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJSONString(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
