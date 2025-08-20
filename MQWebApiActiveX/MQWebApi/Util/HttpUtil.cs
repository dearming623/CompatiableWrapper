using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MQWebApi
{
    public class HttpUtil
    {
        //组装请求参数
        public static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode  == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }
    }
}
