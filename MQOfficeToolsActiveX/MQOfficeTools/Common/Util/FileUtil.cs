using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MQOfficeTools.Common.Util;
using Wrapper.Common.Utils;

namespace MQOfficeTools.Common.Util
{
    class FileUtil
    {
        public static bool CompressJsonFile(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                object jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.None);
                File.WriteAllText(path, output);
                return true;
            }
            catch(Exception ex) {
                Logger.error("FileUtil", "Error compress file.\r\n" + ex.Message);
                return false;
            }
        }

        public static bool IsValidJSON(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                object jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.None);
                return true;
            }
            catch (Exception ex)
            {
                Logger.error("IsValidJSON", "Invalid json file.\r\n" + ex.Message);
                return false;
            }
        }

        public static bool ConvertTo(Encoding orgEncoding, Encoding dstEncoding, string srcPath ,string destPath)
        {
            try
            {
                string txt = File.ReadAllText(srcPath, orgEncoding);
                File.WriteAllText(destPath, txt, dstEncoding);
                return true;
            }
            catch (Exception ex)
            {
                Logger.error("FileUtil", "Faild to convert encoding.\r\n" + ex.Message);
                return false;
            }
        }
    }
}
