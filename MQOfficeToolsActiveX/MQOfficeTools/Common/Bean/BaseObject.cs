using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using MQOfficeTools.Common.Util;

namespace MQOfficeTools.Common.Bean
{
    public abstract class BaseObject
    {
        private const string TAG = "BaseObject";

        public string ToJson()
        {
            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, jSetting);
        }

        public string ToXML()
        {
            return ToXML(Encoding.UTF8);
        }

        public string ToXML(Encoding encode)
        {
            string objName = this.GetType().Name;

            string xmlJsonString = "{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"" + encode.ToString() + "\"},\"" + objName + "\":" + ToJson() + "}";

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(xmlJsonString);

            string responseXML = XmlDoc2Str(xmlDoc, encode);

            if (encode != Encoding.UTF8)
                responseXML = ConvertEncode(responseXML, Encoding.Default, encode);

            return responseXML;
        }

        private string XmlDoc2Str(XmlDocument xmlDoc, Encoding encoding)
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
                Logger.error("XmlDoc2Str", "BaseObject::XmlDoc2Str -> " + ex.Message);
            }

            return xmlString;
        }

        private string ConvertEncode(string src, Encoding orgEncode, Encoding dstEncode)
        {

            string sResult = string.Empty;

            try
            {
                byte[] srcBytes = orgEncode.GetBytes(src);

                sResult = dstEncode.GetString(srcBytes);
            }
            catch (Exception ex)
            {
                Logger.error("ConvertEncode", "BaseObject::ConvertEncode -> " + ex.Message);
            }

            return sResult;
        }

    }
}
