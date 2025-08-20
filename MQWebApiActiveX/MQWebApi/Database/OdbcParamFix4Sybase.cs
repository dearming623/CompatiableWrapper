using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Text.RegularExpressions;


namespace MoleQ.Market.Database
{
    public class OdbcParamFix4Sybase
    {
        public static void Rewrite(ref string sql, ref OdbcParameter[] sqlparams)
        {
            //获取sql语句中@变量列表
            var paramNames = GetParamsNames2(sql);

            //将SQL语句中的@变量替换成?
            sql = Regex.Replace(sql, "(@\\w*)", "?");

            //根据变量列表顺序对parms进行重新排序
            var newParameters = new List<OdbcParameter>();

            foreach (var name in paramNames)
            {
                foreach (var odbcparameter in sqlparams)
                {
                    if (name == odbcparameter.ParameterName)
                    {
                        string newname = odbcparameter.ParameterName + "_" + newParameters.Count.ToString();
                        newParameters.Add(new OdbcParameter(name, odbcparameter.Value));
                        break;
                    }
                   
                }
            }

            sqlparams = newParameters.ToArray();

        }

        private static List<string> GetParamsNames(string sql)
        {
            
            var paramNames = new  List<string>();

            string tempStr = sql;
            int start = -1;
            int end = -1;

            while ((start = tempStr.IndexOf("@")) >= 0)
            {
                end = tempStr.Substring(start).IndexOf(" ");

                if (end < 0)
                    end = tempStr.Length;
                else
                    end = start + end;
                    
                paramNames.Add(tempStr.Substring(start, end - start));

                tempStr = tempStr.Substring(end);
            }
          

            return paramNames;
        }

        private static List<string> GetParamsNames2(string sql)
        {

            //var paramNames = Regex.Split(sql, "(@\\w*)", RegexOptions.);

            //return new List<string>(paramNames);

            var paramNames = new List<string>();

            foreach (Match mch in Regex.Matches(sql, "(@\\w*)"))
            {
                paramNames.Add( mch.Value.Trim());
            }

            return paramNames;
        }
    }
}
