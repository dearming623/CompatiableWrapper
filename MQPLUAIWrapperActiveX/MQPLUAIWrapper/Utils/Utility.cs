using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MoleQ.Support.Wrapper.Common.Util;

namespace Wrapper.Common.Utils
{
    public class Utility
    {
        public static Encoding CurrentSystemEncoding = Encoding.Default;

        public static Encoding pb7Encoding = Encoding.GetEncoding("ISO-8859-1");

        public static string GetNodeText(string oriStr, string startStr, string endStr)
        {
            return Regex.Match(oriStr, string.Concat(new string[]
            {
                "(?<=",
                startStr,
                ").*?(?=",
                endStr,
                ")"
            })).Value;
        }

        /// <summary>
        /// C#反射遍历对象属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象</param>
        public static void ForeachClassProperties<T>(T model)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);
            }
        }

        public static string ValidationClassProperties<T>(T model)
        {
            return ValidationClassProperties(model, null);
        }

        public static string ValidationClassProperties<T>(T model, List<string> filterProperties)
        {
            string res = null;

            if (model == null)
                return " \"" + model.GetType().Name + "\" tag not found. "; ;

            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);

                if (string.IsNullOrEmpty(value as string))
                {
                    if (filterProperties != null && filterProperties.Contains(name))
                        continue;

                    res = " \"" + name + "\" tag not found. ";
                    break;
                }
            }

            return res;
        }

        public static string ValidationClassSpecifyProperties<T>(T model, List<string> specifyProperties)
        {
            string res = null;

            if (model == null)
                return " \"" + model.GetType().Name + "\" tag not found. "; ;

            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);

                if (specifyProperties != null && specifyProperties.Contains(name))
                {
                    if (string.IsNullOrEmpty(value as string))
                    { 
                        res = " \"" + name + "\" tag not found. ";
                        break;
                    }
                }
            }

            return res;
        }

        public static string Xml2String(XmlDocument xmlDoc, Encoding encoding)
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
                Console.WriteLine(ex.Message);
            }

            return xmlString;
        }

        public static string Object2XMLString(object obj , bool hasDeclar)
        {
            Encoding encode = System.Text.Encoding.UTF8;

            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, jSetting);

            string objName = obj.GetType().Name;

            //string format = "{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"utf-8\"},\"{0}\":{1}}";
            //string xmlJsonString = string.Format(format, objName, json);
            
            string xmlJsonString = "{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"" + encode.HeaderName + "\"},\"" + objName + "\":" + json + "}";

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(xmlJsonString);

            string result = Xml2String(xmlDoc, encode);

            if (!hasDeclar)
            {
                string oldchar = "<?xml version=\"1.0\" encoding=\"" + encode.HeaderName + "\"?>";
                result = result.Replace(oldchar, "");
            }

            return result;
        }

        public static string Object2XMLString(object obj)
        {
            Encoding encode = System.Text.Encoding.UTF8;
            // Encoding encode = Encoding.GetEncoding("ISO-8859-1");
            //Encoding encode = Encoding.GetEncoding("big5");

            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, jSetting);

            string objName = obj.GetType().Name;

            //string format = "{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"utf-8\"},\"{0}\":{1}}";
            //string xmlJsonString = string.Format(format, objName, json);

            string xmlJsonString = "{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"" + encode.ToString() + "\"},\"" + objName + "\":" + json + "}";

            //Logger.error("aaaaaa", "befor -> " + xmlJsonString); //测试使用

            // xmlJsonString = Utility.ConvertEncodingToISO88591(xmlJsonString, Encoding.UTF8);

            // Logger.error("aaaaaa", "after -> " + xmlJsonString); //测试使用

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(xmlJsonString);

            string responseXML = Xml2String(xmlDoc, encode);

            //Logger.error("aaaaaa", "befor -> " + responseXML); //测试使用

            responseXML = Utility.ConvertEncode(responseXML, CurrentSystemEncoding, pb7Encoding);

            //Logger.error("aaaaaa", "after -> " + responseXML); //测试使用

            //Logger.debug("DEBUG", "\r\n=======================  Response ================== \r\n" + responseXML + "\r\n  =====================================================\r\n"); //测试使用

            return responseXML;
        }

        public static string Object2XMLString2(object obj, Encoding encoding)
        {
           Encoding encode = System.Text.Encoding.UTF8;

            var jSetting = new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented,jSetting);
         
            string objName = obj.GetType().Name;

            string xmlJsonString = "{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"" + encode.ToString() + "\"},\"" + objName + "\":" + json + "}";

            XmlDocument xmlDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(xmlJsonString);

            string responseXML = Xml2String(xmlDoc, encode);

            int codepage = encoding.CodePage; //CodePage check the comparison table
            switch (codepage)
            {
                case 65001: // UTF-8
                    //
                    break;
                case 28591: // ISO-8859-1 ( for PB7 )
                    responseXML = Utility.ConvertEncode(responseXML, CurrentSystemEncoding, encoding);
                    break;
                default:
                    break;
            }

            return responseXML;
        }

        public static T XMLString2Object<T>(string xml)
        {
           
            XmlDocument xmlDoc = new XmlDocument();
            string json = "";
            try
            {
                xmlDoc.LoadXml(xml);

                if (xmlDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                    xmlDoc.RemoveChild(xmlDoc.FirstChild);

                json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Utility.XMLString2Object throw exception. ->" + ex.Message);
                T obj = default(T);
                obj = Activator.CreateInstance<T>();
                return obj;
            }

            T resObj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);

            return resObj;
            
        }

        public static string ConvertISO88591ToEncoding(string srcString, Encoding dstEncode)
        {

            String sResult;

            Encoding ISO88591Encoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding GB2312Encoding = Encoding.GetEncoding("GB2312"); //这个地方很特殊，必须利用GB2312编码
            byte[] srcBytes = ISO88591Encoding.GetBytes(srcString);

            //将原本存储ISO-8859-1的字节数组当成GB2312转换成目标编码(关键步骤)
            byte[] dstBytes = Encoding.Convert(GB2312Encoding, dstEncode, srcBytes);

            char[] dstChars = new char[dstEncode.GetCharCount(dstBytes, 0, dstBytes.Length)];

            dstEncode.GetChars(dstBytes, 0, dstBytes.Length, dstChars, 0);//利用char数组存储字符

            sResult = new string(dstChars);

            return sResult;

        }

        public static string ConvertToSpecifyEncode(string srcString, Encoding srcEncode, Encoding systemEncode,Encoding dstEncode)
        {

            string sResult;

            byte[] srcBytes = srcEncode.GetBytes(srcString);

            byte[] dstBytes = Encoding.Convert(systemEncode, dstEncode, srcBytes);

            char[] dstChars = new char[dstEncode.GetCharCount(dstBytes, 0, dstBytes.Length)];

            dstEncode.GetChars(dstBytes, 0, dstBytes.Length, dstChars, 0);//利用char数组存储字符

            sResult = new string(dstChars);

            return sResult;

        }

        public static string ConvertEncode(string src,Encoding orgEncode,Encoding dstEncode){

            string  sResult;

            byte[] srcBytes = orgEncode.GetBytes(src);

            sResult = dstEncode.GetString(srcBytes);

            return sResult;

        }

        public static string ConvertEncodingToISO88591(string srcString, Encoding srcEncode)
        {

            string sResult;

            Encoding ISO88591Encoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding GB2312Encoding = Encoding.GetEncoding("GB2312"); //这个地方很特殊，必须利用GB2312编码

            byte[] srcBytes = srcEncode.GetBytes(srcString);

            byte[] dstBytes = Encoding.Convert(srcEncode,GB2312Encoding, srcBytes);

            char[] dstChars = new char[ISO88591Encoding.GetCharCount(dstBytes, 0, dstBytes.Length)];

            ISO88591Encoding.GetChars(dstBytes, 0, dstBytes.Length, dstChars, 0);//利用char数组存储字符

            sResult = new string(dstChars);

            return sResult;

        }

        /// <summary>
        /// 判断一个字符串是否为url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUrl(string str)
        {
            try
            {
                string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
                return Regex.IsMatch(str, Url);
            }
            catch (Exception ex)
            {
                Logger.Error("BackgroundInvoke", "Tools::BackgroundInvoke \r\n" + ex.Message); 
                return false;
            }
        }

        public static bool IsValidFilename(string testName)
        {
            string strTheseAreInvalidFileNameChars = new string(System.IO.Path.GetInvalidFileNameChars());
            Regex regInvalidFileName = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");

            if (regInvalidFileName.IsMatch(testName)) { return false; };

            return true;
        }

        public static bool IsValidFilename2(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            if (fi.Attributes == FileAttributes.Normal)
            {
                return true;
            }

            return false;
        }

        public static bool IsValidFileName3(string name)
        {
            return
                !string.IsNullOrEmpty(name) &&
                name.IndexOfAny(Path.GetInvalidFileNameChars()) < 0 &&
                !Path.GetFullPath(name).StartsWith(@"\\.\");
        }

        /// <summary>
        /// 将原始字串转换为格式为&#....;&#....
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        private string StringToISO_8859_1(string srcText)
        {
            string dst = "";
            char[] src = srcText.ToCharArray();
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] > 127)
                {
                    string str = @"&#" + (int)src[i] + ";";
                    dst += str;
                }
                else
                {
                    dst += src[i];
                }
            }
            return dst;
        }
  
    }
}