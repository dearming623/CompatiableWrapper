using System;
using System.Collections.Generic;
using System.Text;

namespace MQWebApi.Database
{
    public class DBUtil
    {
        public static string FixableContent(string content)
        {
            content = content.Replace("\"", "\"\"");
            content = content.Replace("'", "''");
            return content;
        }
    }
}
