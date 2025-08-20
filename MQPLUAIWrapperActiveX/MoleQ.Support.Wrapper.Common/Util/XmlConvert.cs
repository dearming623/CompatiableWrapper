using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MoleQ.Support.Wrapper.Common.Util
{

    public static class XmlConvert
    {
        public static string SerializeObject<T>(T dataObject)
        {
            if (dataObject == null)
            {
                return string.Empty;
            }
            try
            {
                using (StringWriter stringWriter = new Utf8StringWriter())
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stringWriter, dataObject, ns);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        public static T DeserializeObject<T>(string xml)
             where T : new()
        {
            if (string.IsNullOrEmpty(xml))
            {
                return new T();
            }
            try
            {
                using (var stringReader = new StringReader(xml))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex)
            {
                return new T();
            }
        }


        /// <summary>
        ///     A class only to override encoding with UTF8.
        /// </summary>
        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }
    }
}
