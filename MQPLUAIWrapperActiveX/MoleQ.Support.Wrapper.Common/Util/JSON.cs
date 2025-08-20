using System;
using System.Collections.Generic;
using System.Text;

namespace MoleQ.Support.Wrapper.Common.Util
{
    public class JSON
    {
        //public static string ToString(object obj)
        //{
        //    return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        //}

        public static T parseObject<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static string toJSONString(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
