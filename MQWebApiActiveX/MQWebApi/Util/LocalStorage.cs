using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MQWebApi.Util;
using Newtonsoft.Json.Linq;

namespace MQWebApi
{
    public class LocalStorage
    {
        static string fileName = Environment.CurrentDirectory + "\\setting.json";

        public static object GetItem(string key)
        {
            JObject jsonObj = LoadConfig(); 
            return jsonObj[key]; 
        }

        public static void SetItem(string key, object val)
        {
            JObject jsonObj = LoadConfig();
            if (jsonObj.ContainsKey(key))
            {
               JToken token = jsonObj[key]  ;
               token.Replace(JToken.FromObject(val));
            }
            else
            {
                jsonObj.Add(key, JToken.FromObject(val));
            }
           
            SaveConfig(jsonObj); 
        }

        private static JObject LoadConfig()
        {
            JObject jsonObject = new JObject();
            if (File.Exists(fileName))
            {
                string str = File.ReadAllText(fileName);
                string jsonStr = str;
                //var config = JSON.ParseObject<String>(jsonStr);
                jsonObject = JObject.Parse(jsonStr);
            } 
            return jsonObject;
        }


        private static void SaveConfig(JObject jsonObj)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            string json = jsonObj.ToString();

            File.WriteAllText(fileName, json, Encoding.UTF8);
            //隐藏文件
            //File.SetAttributes(fileName,FileAttributes.Hidden);
        }

        
    }
}
