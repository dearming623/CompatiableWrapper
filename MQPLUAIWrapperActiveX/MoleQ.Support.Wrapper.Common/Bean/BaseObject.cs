using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using MoleQ.Support.Wrapper.Common.Util;
using System.Text.RegularExpressions;

namespace MoleQ.Support.Wrapper.Common.Bean
{
    public abstract class BaseObject
    {
        private const string TAG = "BaseObject";

        private const int TYPE_XML = 0;
        private const int TYPE_JSON = 1;

        public string ToJson()
        {
            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, jSetting);
        }

        public string ToString(int type)
        {
            string res = "";
            switch (type)
            {
                case TYPE_XML:
                    res = ToXML();
                    break; 
                case TYPE_JSON:
                    res = ToJson();
                    break;
                default:
                    res = ToXML();
                    break;
            }
            return res;
        }

        //public string ToString() {
        //    return ToString(TYPE_XML);
        //}

        public string ToXML()
        {
            return ToXML(Encoding.UTF8);
        }

        public string ToXML(Encoding encode)
        {
            //var type = this.GetType();
            //string objName = type.Name;
            //Match match = Regex.Match(objName, @"^.*?(?=`)");
            //if (match.Success)
            //{
            //    objName = match.Groups[0].Value;
            //}

            Type type = this.GetType();
            string objName = type.Name;
            if (type.IsGenericType)
            {
                Type g = type.GetGenericTypeDefinition();
                objName = g.Name.Remove(g.Name.IndexOf('`'));
            } 

            string encodeStr = encode.HeaderName;
            string xmlJsonString = "{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"" + encodeStr + "\"},\"" + objName + "\":" + ToJson() + "}";

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
                Logger.Error("XmlDoc2Str", "BaseObject::XmlDoc2Str -> " + ex.Message);
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
                Logger.Error("ConvertEncode", "BaseObject::ConvertEncode -> " + ex.Message);
            }

            return sResult;
        }

    }
}
