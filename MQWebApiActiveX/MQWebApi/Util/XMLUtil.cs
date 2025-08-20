using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MoleQ.Support.Wrapper.Common.Util;

namespace MQWebApi.Util
{
    public class XMLUtil
    {

        private const string TAG = "XMLUtil";
        /// <summary>
        /// 提取XML中的TAG字段内容
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <param name="tagName">TAG名字</param>
        /// <returns></returns>
        public static string ExtractTagContent(string xml, string tagName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                XmlNodeList nodes = xmlDoc.GetElementsByTagName(tagName);
                if (nodes.Count > 0)
                {
                    return nodes[0].InnerText;
                }
                else
                {
                    Logger.Error(TAG, "Tag not found."); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex.Message);
            }
            return string.Empty;
        }

        //public static string ExtractTagContent(string xml, string tagName)
        //{
        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.LoadXml(xml);

        //        XmlNodeList nodes = xmlDoc.GetElementsByTagName(tagName);
        //        if (nodes.Count > 0)
        //        {
        //            return nodes[0].InnerText;
        //        }
        //        else
        //        {
        //            return "Tag not found.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error: " + ex.Message;
        //    }
        //}
    }
}
