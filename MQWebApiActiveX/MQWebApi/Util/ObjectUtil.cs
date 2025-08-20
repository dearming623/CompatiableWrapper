using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using MoleQ.Support.Wrapper.Common.Util;

namespace MQWebApi.Util
{
    public class ObjectUtil
    {
        private const string TAG = "ObjectUtil";

        public static string ToJson(object obj)
        {
            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, jSetting);
        }

        public static string ToXML(object obj)
        {
            return ToXML(obj, Encoding.UTF8);
        }

        public static string ToXML(object obj, Encoding encode)
        {
            string objName = obj.GetType().Name;
            var json = ToJson(obj);
            string xmlJsonString = "{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"" + encode.ToString() + "\"},\"" + objName + "\":" + json + "}";

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(xmlJsonString);

            string responseXML = XmlDoc2Str(xmlDoc, encode);

            if (encode != Encoding.UTF8)
                responseXML = ConvertEncode(responseXML, Encoding.Default, encode);

            return responseXML;
        }

        private static string XmlDoc2Str(XmlDocument xmlDoc, Encoding encoding)
        {
            string xmlString = "";

            try
            {
                MemoryStream stream = new MemoryStream();

                XmlTextWriter writer = new XmlTextWriter(stream, encoding);

                writer.Formatting = Formatting.Indented;
                //writer.Formatting = Formatting.None; 

                xmlDoc.Save(writer);

                StreamReader sr = new StreamReader(stream, encoding);
                stream.Position = 0;

                xmlString = sr.ReadToEnd();
                sr.Close();
                stream.Close();

                xmlString = xmlString.Replace("&lt;", "<")
                                    .Replace("&amp;", "&")
                                    .Replace("&gt;", ">")
                                    .Replace("&quot;", "\"")
                                    .Replace("&apos;", "'");

            }
            catch (Exception ex)
            {
                Logger.Error(TAG, "ObjectUtil::XmlDoc2Str -> " + ex.Message);
            }

            return xmlString;
        }

        private static string ConvertEncode(string src, Encoding orgEncode, Encoding dstEncode)
        {
            string sResult = string.Empty;
            try
            {
                byte[] srcBytes = orgEncode.GetBytes(src);
                sResult = dstEncode.GetString(srcBytes);
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, "ObjectUtil::ConvertEncode -> " + ex.Message);
            }
            return sResult;
        }

    }
}
