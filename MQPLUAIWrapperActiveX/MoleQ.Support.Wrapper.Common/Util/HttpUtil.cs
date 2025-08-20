using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using MoleQ.Support.Wrapper.Common.Web;
using System.Collections.Specialized;
using MoleQ.Support.Wrapper.Common.Bean;

namespace MoleQ.Support.Wrapper.Common.Util
{
    public class HttpUtil
    {
        //组装请求参数
        public static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        
        public static void PostDataGenerateFile(string url, string body, string saveFile)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 30 * 1000;
            httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";
            httpWebRequest.ContentType = "application/json";

            byte[] bytes = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = (long)bytes.Length;
            httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            byte[] buffer = new byte[1024];

            string s = saveFile.Substring(0, saveFile.LastIndexOf('\\'));
            Directory.CreateDirectory(s);//如果文件夹不存在就创建它

            FileStream fileStream = File.OpenWrite(saveFile);
            using (Stream input = httpWebResponse.GetResponseStream())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024];
                        count = input.Read(buf, 0, 1024);
                        ms.Write(buf, 0, count);
                    } while (input.CanRead && count > 0);
                    buffer = ms.ToArray();

                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
            fileStream.Flush();
            fileStream.Close();
        }

        public static void PostJsonFileGenerateExcel(string url, string file, string paramName, string contentType,
            NameValueCollection formFields,string saveFile,
            IFileCnvtCallback callback)
        {
            Logger.Debug("PostJsonFileGenerateExcel", string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = null;

            try
            {
                wr = (HttpWebRequest)WebRequest.Create(url);
            }
            catch (Exception ex)
            {
                Logger.Error("PostJsonFileGenerateExcel", "Fail to uploading file:" + ex.Message);
                callback.FileConvertError(ex.Message);
                return;
            }

            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = null;
            try
            {
                rs = wr.GetRequestStream();
            }
            catch (Exception ex)
            {
                Logger.Error("PostJsonFileGenerateExcel", "Fail to uploading file:" + ex.Message);
                callback.FileConvertError(ex.Message);
                return;
            }
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (formFields != null)
            {
                foreach (string key in formFields.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, formFields[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }

            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();
            Logger.Debug("PostJsonFileGenerateExcel", "Upload file completed.");

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                //Stream stream2 = wresp.GetResponseStream();
                //StreamReader reader2 = new StreamReader(stream2);
                //string result = reader2.ReadToEnd();
                //Logger.debug("UploadFile", string.Format("File uploaded, server response is: {0}", result));

                HttpWebResponse httpWebResponse = (HttpWebResponse)wresp;
                buffer = new byte[1024];

                string s = saveFile.Substring(0, saveFile.LastIndexOf('\\'));
                Directory.CreateDirectory(s);//如果文件夹不存在就创建它

                fileStream = File.OpenWrite(saveFile);
                using (Stream input = httpWebResponse.GetResponseStream())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int count = 0;
                        do
                        {
                            byte[] buf = new byte[1024];
                            count = input.Read(buf, 0, 1024);
                            ms.Write(buf, 0, count);
                        } while (input.CanRead && count > 0);
                        buffer = ms.ToArray();

                        fileStream.Write(buffer, 0, buffer.Length);
                    }
                }
                fileStream.Flush();
                fileStream.Close();

                Logger.Debug("PostJsonFileGenerateExcel", "Receive file completed.");
                callback.FileConvertSuccess(saveFile);
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    if (httpResponse.StatusCode == (HttpStatusCode)600)
                    {
                        Logger.Debug("PostJsonFileGenerateExcel", "Reponse error message.");
                        string errMsg = "Unknow error from Wrapper.";
                        using (var stream = httpResponse.GetResponseStream())
                        using (var reader = new StreamReader(stream))
                        {
                            string jsonStr = reader.ReadToEnd();
                            Logger.Debug("PostJsonFileGenerateExcel", jsonStr);
                            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<CommonResult<object>>(jsonStr);
                            errMsg = res.Msg;
                        }
                        callback.FileConvertError(errMsg);
                        return;
                    }
                    else
                    {
                        callback.FileConvertError(e.Message);
                    }
                    if (wresp != null)
                    {
                        wresp.Close();
                        wresp = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PostJsonFileGenerateExcel", "Fail to uploading file:" + ex.Message);
                callback.FileConvertError(ex.Message);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        public static void HttpUploadFile(string url, string file, string paramName, string contentType, 
            NameValueCollection formFields,
            IHttpCallback callback)
        { 
            Logger.Debug("UploadFile", string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);

            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = null;
            try
            {
                rs = wr.GetRequestStream();
            }
            catch (Exception ex)
            {
                Logger.Error("UploadFile", "Error uploading file:" + ex.Message);
                callback.OnError(ex.Message);
                return;
            }
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (formFields != null)
            {
                foreach (string key in formFields.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, formFields[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }

            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                string result = reader2.ReadToEnd();
                Logger.Debug("UploadFile", string.Format("File uploaded, server response is: {0}", result));
                callback.OnSuccess(result);
            }
            catch (Exception ex)
            {
                Logger.Error("UploadFile", "Error uploading file:" + ex.Message);
                callback.OnError(ex.Message);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }


        public static string HttpPost(string url, string body)
        {
            string result;
            try
            {
                Encoding uTF = Encoding.UTF8;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Timeout = 30 * 1000;
                byte[] bytes = uTF.GetBytes(body);
                httpWebRequest.ContentLength = (long)bytes.Length;
                httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("HttpPost", "HttpUtil::HttpPost -> " + ex.Message);
                result = ex.Message;
            }
            return result;
        }

        public static string GetRequest(Stream st)
        {
            byte[] b = new byte[st.Length];
            st.Read(b, 0, (int)st.Length);
            string postStr = Encoding.UTF8.GetString(b);
            //return postStr.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
            return postStr;
        }

    }
}
