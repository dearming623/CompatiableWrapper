/****************************** Module Header ******************************\
* Module Name:  CSActiveXCtrl.cs
* Project:      CSActiveX
* Copyright (c) Microsoft Corporation.
* 
* The sample demonstrates an ActiveX control written in C#. ActiveX controls
* (formerly known as OLE controls) are small program building blocks that can 
* work in a variety of different containers, ranging from software development 
* tools to end-user productivity tools. For example, it can be used to create 
* distributed applications that work over the Internet through web browsers. 
* ActiveX controls can be written in MFC, ATL, C++, C#, Borland Delphi and 
* Visual Basic. In this sample, we focus on writing an ActiveX control using 
* C#. We will go through the basic steps of adding UI, properties, methods,  
* and events to the control.
* 
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/en-us/openness/resources/licenses.aspx#MPL.
* All other rights reserved.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Specialized;
using MoleQ.Support.Wrapper.Common.Util;
using System.Xml;
using MoleQ.Wrapper.WebApi.Service;
using MoleQ.Wrapper.WebApi.Service.Impl;
using MQWebApi.Util;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using MQWebApi.Core;
using Newtonsoft.Json;
#endregion


namespace MQWebApi
{
    #region Interfaces

    /// <summary>
    /// AxCSActiveXCtrl describes the COM interface of the coclass 
    /// </summary>
    [Guid("3629E7C2-C575-41d9-B168-FB2B54FBC736")]
    public interface AxCSActiveXCtrl
    {
        #region Properties

        //bool Visible { get; set; }          // Typical control property
        //bool Enabled { get; set; }          // Typical control property
        //int ForeColor { get; set; }         // Typical control property
        //int BackColor { get; set; }         // Typical control property
        //float FloatProperty { get; set; }   // Custom property 

        #endregion

        #region Methods

        void OnPost(string url, string body);

        void OnPost2(string url, string body, int timeout);

        void OnGet(string url, int timeout);

        void UploadFile(string url, string file, string contentType); //v1.0.0.5

        void DownloadFile(string url, string saveFile); //v1.0.0.5

        void EnableLogger(bool enable); //v1.0.0.5

        //void CreateExcelFile(string url, string body, string saveFile); //v1.0.0.5 //已转移到MQOfficeTool

        string EncryptMD5(string str);

        string GetTextForPB7(string txt); // 测试方法

        void SetConfig(string xmlConfig); // v1.06 

        /*********** v1.08 增加监听消息的功能 ***********/
        void OnStartMonitor();
        void OnStopMonitor();
        void OnBindMonitor(string folderPath);
        string ConvertJson2xml(string json);
        /************************************************/
        

        #endregion
    }

    /// <summary>
    /// AxCSActiveXCtrlEvents describes the events the coclass can sink
    /// </summary>
    [Guid("B8E50A11-2340-4d28-BE19-08C32C07F849")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    // The public interface describing the events of the control
    public interface AxCSActiveXCtrlEvents
    {
        #region Events

        // Must explicitly define DISPID for each event, otherwise, the 
        // callback address cannot be found when the event is fired.
        [DispId(1)]
        void DataError(string errMsg);
        [DispId(2)]
        void DataReceived(string result);

        #endregion
    }

    #endregion

    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(AxCSActiveXCtrlEvents))]
    [Guid("3A53CB6C-30ED-4d42-9B96-A9E1D536AFAF")]
    public partial class Tools : UserControl, AxCSActiveXCtrl
    {
        #region ActiveX Control Registration

        // These routines perform the additional COM registration needed by 
        // ActiveX controls

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComRegisterFunction()]
        public static void Register(Type t)
        {
            try
            {
                ActiveXCtrlHelper.RegasmRegisterControl(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw;  // Re-throw the exception
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComUnregisterFunction()]
        public static void Unregister(Type t)
        {
            try
            {
                ActiveXCtrlHelper.RegasmUnregisterControl(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw; // Re-throw the exception
            }
        }

        #endregion


        #region Initialization

        public Tools()
        {
            InitializeComponent();

            // For the Click event that is re-defined.

            // These functions are used to handle Tab-stops for the ActiveX 
            // control (including its child controls) when the control is 
            // hosted in a container.
            this.LostFocus += new EventHandler(CSActiveXCtrl_LostFocus);
            this.ControlAdded += new ControlEventHandler(
                CSActiveXCtrl_ControlAdded);

            // Raise custom Load event
            this.OnCreateControl();
        }

        // This event will hook up the necessary handlers
        void CSActiveXCtrl_ControlAdded(object sender, ControlEventArgs e)
        {
            // Register tab handler and focus-related event handlers for 
            // the control and its child controls.
            ActiveXCtrlHelper.WireUpHandlers(e.Control, ValidationHandler);
        }

        // Ensures that the Validating and Validated events fire properly
        internal void ValidationHandler(object sender, System.EventArgs e)
        {
            if (this.ContainsFocus) return;

            this.OnLeave(e); // Raise Leave event

            if (this.CausesValidation)
            {
                CancelEventArgs validationArgs = new CancelEventArgs();
                this.OnValidating(validationArgs);

                if (validationArgs.Cancel && this.ActiveControl != null)
                    this.ActiveControl.Focus();
                else
                    this.OnValidated(e); // Raise Validated event
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand,
            Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_SETFOCUS = 0x7;
            const int WM_PARENTNOTIFY = 0x210;
            const int WM_DESTROY = 0x2;
            const int WM_LBUTTONDOWN = 0x201;
            const int WM_RBUTTONDOWN = 0x204;

            if (m.Msg == WM_SETFOCUS)
            {
                // Raise Enter event
                this.OnEnter(System.EventArgs.Empty);
            }
            else if (m.Msg == WM_PARENTNOTIFY && (
                m.WParam.ToInt32() == WM_LBUTTONDOWN ||
                m.WParam.ToInt32() == WM_RBUTTONDOWN))
            {
                if (!this.ContainsFocus)
                {
                    // Raise Enter event
                    this.OnEnter(System.EventArgs.Empty);
                }
            }
            else if (m.Msg == WM_DESTROY &&
                !this.IsDisposed && !this.Disposing)
            {
                // Used to ensure the cleanup of the control
                this.Dispose();
            }

            base.WndProc(ref m);
        }

        // Ensures that tabbing across the container and the .NET controls
        // works as expected
        void CSActiveXCtrl_LostFocus(object sender, EventArgs e)
        {
            ActiveXCtrlHelper.HandleFocus(this);
        }

        #endregion


        #region Properties

        //public new int ForeColor
        //{
        //    get { return ActiveXCtrlHelper.GetOleColorFromColor(base.ForeColor); }
        //    set { base.ForeColor = ActiveXCtrlHelper.GetColorFromOleColor(value); }
        //}

        //public new int BackColor
        //{
        //    get { return ActiveXCtrlHelper.GetOleColorFromColor(base.BackColor); }
        //    set { base.BackColor = ActiveXCtrlHelper.GetColorFromOleColor(value); }
        //}

        //private float fField = 0;

        /// <summary>
        /// A custom property with both get and set accessor methods.
        /// </summary>
        //public float FloatProperty
        //{
        //    get { return this.fField; }
        //    set 
        //    {
        //        bool cancel = false;
        //        // Raise the event FloatPropertyChanging
        //        if (null != FloatPropertyChanging)
        //            FloatPropertyChanging(value, ref cancel);
        //        if (!cancel)
        //        {
        //            this.fField = value;
        //            this.lbFloatProperty.Text = value.ToString();
        //        }
        //    }
        //}

        //public string charset { get; set; }
        //public bool isWriteResp2DB { get; set; }
        //public string databasePath = "";
        public string wrapperConfig { get; set; }

        private MessageReceiver _messageReceiver = new MessageReceiver();

        #endregion


        #region Methods


        public void OnPost(string url, string body)
        {
            //if (null != DataReceived && null != DataError)
            //{
            //    WebParameter parms = new WebParameter();
            //    parms.url = url.Trim();
            //    parms.body = body.Trim();

            //    Thread t = new Thread(new ParameterizedThreadStart(DoWork));
            //    t.Start(parms);
            //}
            OnPost2(url, body, 0); //when timeout set 0 ,http request timeout set default
        }

        public void OnPost2(string url, string body, int timeout)
        {
            //Logger.Debug("CodePage", "System CodePage: " + Encoding.Default.CodePage.ToString());
            //vaildate charset
            //bool isNeedEncoding = false;
            //if (!string.IsNullOrEmpty(this.charset))
            //{
            //    if (!IsVaildCharSet(this.charset))
            //    {
            //        this.charset = "";
            //        DataError("Invaild \"CharSet\". eg:\"UTF-8 \" or \"ISO-8859-1\". ");
            //        return;
            //    }
            //    //isNeedEncoding = true;
            //}

            if (!string.IsNullOrEmpty(this.wrapperConfig))
            {
                string xmlConfig = this.wrapperConfig;
                string charSet = XMLUtil.ExtractTagContent(xmlConfig, "CharSet");
                if (!string.IsNullOrEmpty(charSet))
                {
                    if (!IsVaildCharSet(charSet))
                    {
                        TriggerDataError("Invaild \"CharSet\". eg:\"UTF-8 \" or \"ISO-8859-1\". ");
                        return;
                    }
                }

                if (!ValidateWrapperConfig(xmlConfig))
                {
                    return;
                }

                HttpPostSpecialAsync(url, body, timeout, xmlConfig);
                return;
            }

            HttpPostAsync(url, body, timeout);
        }

        private bool ValidateWrapperConfig(string xmlConfig)
        {
            string dsn = XMLUtil.ExtractTagContent(xmlConfig, "Dsn");
            if (string.IsNullOrEmpty(dsn))
            {
                TriggerDataError("Invaild \"Dsn\" tag. eg:\"market \" ");
                return false;
            }

            string uid = XMLUtil.ExtractTagContent(xmlConfig, "Uid");
            if (string.IsNullOrEmpty(uid))
            {
                TriggerDataError("The 'Uid' tag cannot be empty.");
                return false;
            }

            string pwd = XMLUtil.ExtractTagContent(xmlConfig, "Pwd");
            if (string.IsNullOrEmpty(pwd))
            {
                TriggerDataError("The 'Pwd' tag cannot be empty.");
                return false;
            }

            string stn = XMLUtil.ExtractTagContent(xmlConfig, "Stn");
            if (string.IsNullOrEmpty(pwd))
            {
                TriggerDataError("The 'Stn' tag cannot be empty.");
                return false;
            }

            if (!IsNumber(stn))
            {
                TriggerDataError("Invaild 'Stn' tag cannot be empty.");
                return false;
            }

            return true;
        }

        public void HttpPostSpecial(string url, string body, int timeout, string xmlConfig)
        {
            string charSet = string.Empty;
            string dsn;
            string uid;
            string pwd;
            string stnStr;
            int stn = -1;
            LocalService localService = null;

            if (!string.IsNullOrEmpty(xmlConfig))
            {
                charSet = XMLUtil.ExtractTagContent(xmlConfig, "CharSet");
                dsn = XMLUtil.ExtractTagContent(xmlConfig, "Dsn");
                uid = XMLUtil.ExtractTagContent(xmlConfig, "Uid");
                pwd = XMLUtil.ExtractTagContent(xmlConfig, "Pwd");
                stnStr = XMLUtil.ExtractTagContent(xmlConfig, "Stn");
                stn = Convert.ToInt32(stnStr);
                localService = new LocalServiceImpl(dsn, uid, pwd);
                localService.Remove(stn);
            }

            try
            {

                string resp = HttpPost2(url, body, timeout);

                if (!string.IsNullOrEmpty(xmlConfig) && null != localService && stn > 0)
                {
                    //charSet = XMLUtil.ExtractTagContent(xmlConfig, "CharSet");
                    //string dsn = XMLUtil.ExtractTagContent(xmlConfig, "Dsn");
                    //string uid = XMLUtil.ExtractTagContent(xmlConfig, "Uid");
                    //string pwd = XMLUtil.ExtractTagContent(xmlConfig, "Pwd");
                    //string stnStr = XMLUtil.ExtractTagContent(xmlConfig, "Stn");
                    //int stn = Convert.ToInt32(stnStr);



                    //set database connection
                    //string connectionString = string.Format("Dsn={0};Uid={1};Pwd={2}", dsn, uid, pwd);

                    //svr.RemoveAll();

                    //string str = ExtractTagContent(res, "ShipToDescription") + "-" + ExtractTagContent(res, "BillToDescription");
                    //bool isOk = localService.RemoveOrInsert(stn, resp);//调整逻辑，删除记录放到OnPost函数当中
                    bool isOk = localService.SaveOrUpdate2(stn, resp);
                    if (isOk)
                    {
                        Logger.Info("Response", "Write response to database successfully.");
                    }
                    else
                    {
                        Logger.Error("Response", "Failed to write response to database.");
                    }

                    ClearWrapperConfig();
                    /*********** Kevin 不需要特殊返回处理，直接返回原来的内容  ************/
                    //                    string spResp = @"  <Response>
                    //                                            <Result>8888</Result>
                    //                                            <Message>The response content is obtained in the specified database.</Message>
                    //                                            <Stn>{0}</Stn>
                    //                                        </Response>";
                    //                    spResp = string.Format(spResp, stnStr);
                    //                    TriggerDataReceived(spResp);
                    //                    return;
                    /**********************************************************************/
                }

                //如果只charset则进行编码转换
                if (!string.IsNullOrEmpty(charSet))
                {
                    //res = EncodingConverter.ToCodePage(res, Encoding.Default, EncodingConverter.ISO_8859_1);
                    //res = EncodingConverter.ToCodePage(res, systemEncoding , Encoding.GetEncoding(charset)); // is ok

                    //res = EncodingConverter.ConvertToCodePage(res, Encoding.UTF8, Encoding.Default);
                    resp = PageCodeConverter.ConvertToCodePage(resp, Encoding.Default, Encoding.GetEncoding(charSet));

                    //res = EncodingConverter.ToCodePage(res, Encoding.UTF8, Encoding.GetEncoding(charset));

                    //MessageBox.Show( res,Encoding.GetEncoding(charset).EncodingName); 
                    Logger.Debug("Response", "[Encoding to " + charSet + "]\r\n" + string.Format(resp));
                }

                TriggerDataReceived(resp);
            }
            catch (Exception ex)
            {
                TriggerDataError(ex.Message);
            }
        }

        public void ClearWrapperConfig()
        {
            this.wrapperConfig = string.Empty;
        }

        /// <summary>
        /// 保存响应到sqlite 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="timeout"></param>
        /// <param name="dbPath"></param>
        public void OnPostSPThenToSqliteDB(string url, string body, int timeout, string dbPath)
        {

            if (!FileUtil.IsValidPath(dbPath))
            {
                TriggerDataError("Invaild path.");
                return;
            }

            if (!File.Exists(dbPath))
            {
                if (!Directory.Exists(dbPath))
                {
                    TriggerDataError("Invaild directory.");
                    return;
                }

                TriggerDataError("Database file not found.");
                return;
            }

            //isWriteResp2DB = true;
            //databasePath = dbPath;
            OnPost2(url, body, timeout);
        }



        public void OnGet(string url, int timeout)
        {
            //if (null != DataReceived && null != DataError)
            //{
            //    Thread th = new Thread(new ThreadStart(delegate()
            //    {
            //        HttpGet(url, timeout);
            //    }));
            //    th.Start();
            //}

            HttpGetAsync(url, timeout);

        }

        public void HttpGetAsync(string url, int timeout)
        {
            if (null != DataReceived && null != DataError)
            {
                Thread th = new Thread(new ThreadStart(delegate()
                {
                    HttpGet(url, timeout);
                }));
                th.Start();
            }
        }

        public void HttpPostAsync(string url, string body, int timeout)
        {
            if (null != DataReceived && null != DataError)
            {
                Thread th = new Thread(new ThreadStart(delegate()
                {
                    HttpPost(url, body, timeout);
                }));
                th.Start();
            }
        }

        public void HttpPostAsync(string url, string body)
        {
            HttpPostAsync(url, body, 30 * 1000);
        }

        public void HttpPostSpecialAsync(string url, string body, int timeout, string xmlCfg)
        {
            if (null != DataReceived && null != DataError)
            {
                Thread th = new Thread(new ThreadStart(delegate()
                {
                    HttpPostSpecial(url, body, timeout, xmlCfg);
                }));
                th.Start();
            }
        }

        /// <summary>
        /// upload file 
        /// </summary>
        /// <param name="url">http://your.server.com/upload</param>
        /// <param name="file">C:\test\test.jpg</param>
        /// <param name="contentType">image/jpeg</param>
        public void UploadFile(string url, string file, string contentType)
        {
            Logger.Info("UploadFile", string.Format("Uploading {0} to {1}", file, url));
            if (null != DataReceived && null != DataError)
            {
                //HttpUploadFile("http://your.server.com/upload",  @"C:\test\test.jpg", "file", "image/jpeg", nvc);
                Thread th = new Thread(new ThreadStart(delegate()
                {
                    HttpUploadFile(url, file, "file", contentType, null);
                }));
                th.Start();
            }
        }

        public void DownloadFile(string url, string saveFile)
        {
            if (null != DataReceived && null != DataError)
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Timeout = 30 * 1000;
                webRequest.BeginGetResponse(new AsyncCallback(PlayResponeAsync), new object[] { webRequest, saveFile });
            }
        }

        public void EnableLogger(bool enable)
        {
            Logger.SetEnableLogger(enable);
        }

        public void CreateExcelFile(string url, string body, string saveFile)
        {
            Logger.Info("ExcelFile", string.Format("CreateExcelFile {0} to {1}, save file:{2}", url, body, saveFile));

            Dictionary<string, string> parameters = new Dictionary<string, string>();    //参数列表
            parameters.Add("fileName", Guid.NewGuid().ToString().Replace("-", ""));
            parameters.Add("fileType", "xlsx");

            string query = HttpUtil.BuildQuery(parameters, Encoding.UTF8.ToString());

            if (null != DataReceived && null != DataError)
            {
                Thread th = new Thread(new ThreadStart(delegate()
                {
                    HttpPostDataCreateFile(url + "?" + query, body, saveFile);

                }));
                th.Start();
            }
        }


        public void HttpGet(string url, int timeout)
        {
            string result = "";
            try
            {
                Encoding uTF = Encoding.UTF8;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                //Setting KeepAlive to false
                //httpWebRequest.KeepAlive = false;

                httpWebRequest.Method = "GET";
                httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";
                httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Timeout = 30 * 1000;     // build v1.0.0.2 removed 移除timeout
                if (timeout > 0)
                    httpWebRequest.Timeout = timeout * 1000;

                //byte[] bytes = uTF.GetBytes(body);
                //httpWebRequest.ContentLength = (long)bytes.Length;
                //httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), uTF))
                {
                    result = streamReader.ReadToEnd();
                }

                DataReceived(result);

            }
            catch (Exception ex)
            {
                result = ex.Message;
                DataError(result);
            }
        }

        public void HttpPost(string url, string body, int timeout)
        {
            url = ChangehttpsTohttp(url);
            string result = "";

            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls| (SecurityProtocolType)3072;

                Encoding utf8 = Encoding.UTF8;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                //Setting KeepAlive to false
                //httpWebRequest.KeepAlive = false;

                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";
                httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Timeout = 30 * 1000;     // build v1.0.0.2 removed 移除timeout
                if (timeout > 0)
                    httpWebRequest.Timeout = timeout * 1000;

                byte[] bytes = utf8.GetBytes(body);
                httpWebRequest.ContentLength = (long)bytes.Length;
                httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), utf8))
                {
                    result = streamReader.ReadToEnd();
                }
                //Logger.Debug("Response", string.Format(result));
                TriggerDataReceived(result);

            }
            catch (Exception ex)
            {
                result = ex.Message;
                DataError(result);
            }
        }

        public string HttpPost2(string url, string body, int timeout)
        {
            string result = "";

            Encoding utf8 = Encoding.UTF8;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //Setting KeepAlive to false
            //httpWebRequest.KeepAlive = false;

            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";
            httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Timeout = 30 * 1000;     // build v1.0.0.2 removed 移除timeout
            if (timeout > 0)
                httpWebRequest.Timeout = timeout * 1000;

            byte[] bytes = utf8.GetBytes(body);
            httpWebRequest.ContentLength = (long)bytes.Length;
            httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), utf8))
            {
                result = streamReader.ReadToEnd();
            }
            Logger.Debug("Response", string.Format(result));
            return result;
        }

        //尽量使用HttpPost方法替代
        public void DoWork(object parameters)
        {
            WebParameter parms = parameters as WebParameter;
            string result = "";
            try
            {
                Encoding utf8 = Encoding.UTF8;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(parms.url);
                //Setting KeepAlive to false
                //httpWebRequest.KeepAlive = false;

                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";
                httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Timeout = 30 * 1000;     // build v1.0.0.2 removed 移除timeout
                if (parms.timeout > 0)
                    httpWebRequest.Timeout = parms.timeout * 1000;

                byte[] bytes = utf8.GetBytes(parms.body);
                httpWebRequest.ContentLength = (long)bytes.Length;
                httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), utf8))
                {
                    result = streamReader.ReadToEnd();
                }
                Logger.Debug("Response", string.Format(result));

                TriggerDataReceived(result);

            }
            catch (Exception ex)
            {
                result = ex.Message;
                DataError(result);
            }

        }

        public string EncryptMD5(string encryptStr)
        {
            byte[] bytes = Encoding.Default.GetBytes(encryptStr);
            MD5 mD = new MD5CryptoServiceProvider();
            byte[] value = mD.ComputeHash(bytes);
            return BitConverter.ToString(value).Replace("-", "");
        }

        public void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection formFields)
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
                DataError(ex.Message);
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
                DataReceived(result);
            }
            catch (Exception ex)
            {
                Logger.Error("UploadFile", "Error uploading file:" + ex.Message);
                DataError(ex.Message);
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

        private void PlayResponeAsync(IAsyncResult asyncResult)
        {
            object[] parms = (object[])asyncResult.AsyncState;
            HttpWebRequest webRequest = (HttpWebRequest)parms[0];
            string saveFile = (string)parms[1];
            long total = 0;
            long received = 0;
            //HttpWebRequest webRequest = (HttpWebRequest)asyncResult.AsyncState;
            try
            {
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asyncResult))
                {

                    byte[] buffer = new byte[1024];

                    string s = saveFile.Substring(0, saveFile.LastIndexOf('\\'));
                    Directory.CreateDirectory(s);//如果文件夹不存在就创建它

                    FileStream fileStream = File.OpenWrite(saveFile);
                    using (Stream input = webResponse.GetResponseStream())
                    {
                        //total = input.Length;

                        //int size = input.Read(buffer, 0, buffer.Length);
                        //while (size > 0)
                        //{
                        //    fileStream.Write(buffer, 0, size);
                        //    received += size;

                        //    size = input.Read(buffer, 0, buffer.Length);
                        //}

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

                    DataReceived("{\"code\":\"0\",\"status\":\"DOWNLOAD_FILE\",\"msg\":\"Download file completed.\"}");
                }
            }
            catch (Exception ex)
            {
                DataError("{\"code\":\"10000\",\"status\":\"DOWNLOAD_FILE\",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        private void HttpPostDataCreateFile(string url, string body, string saveFile)
        {
            Logger.Info("HttpPostReturnFile", string.Format("{0} to {1}, save file:{2}", url, body, saveFile));
            try
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

                DataReceived("{\"code\":\"0\",\"status\":\"success\",\"msg\":\"Download file completed.\"}");

            }
            catch (Exception ex)
            {
                DataError("{\"code\":\"10000\",\"status\":\"fail\",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        //测试字符串正确转换返回给PB7
        public string GetTextForPB7(string txt)
        {
            string str = "11CDE[繁體-五穀豐登-飯堂說話][简体-五谷丰登-饭堂说话]我是一个中国人]ABC22";

            var pb7Encoding = Encoding.GetEncoding("ISO-8859-1");
            var res = PageCodeConverter.ToCodePage(str, Encoding.Default, PageCodeConverter.ISO_8859_1);

            var unicodeStr = PageCodeConverter.StringToUnicode(str);
            var orgStr = PageCodeConverter.UnicodeToString(unicodeStr);

            return res;

        }

        public void SetConfig(string xmlConfig)
        {
            this.wrapperConfig = xmlConfig;
        }


        public bool IsVaildCharSet(string charset)
        {
            if (string.IsNullOrEmpty(charset))
            {
                return false;
            }

            try
            {
                Encoding encodingInfo = Encoding.GetEncoding(charset);
            }
            catch (ArgumentException ex)
            {
                Logger.Error("Vaildation", ex.Message);
                return false;
            }
            return true;
        }

        public void TriggerDataReceived(string res)
        {


            //var hasBig5 = ContainsBig5(res); 
            //MessageBox.Show(res, "hasBig5 -> " + hasBig5);

            //var systemEncoding = Encoding.GetEncoding("big5");
            //var systemEncoding = Encoding.Default;

            //MessageBox.Show(res, "org");

            //转换成系统设置的编码
            //res = EncodingConverter.ToCodePage(res, Encoding.UTF8, Encoding.Default); 

            //MessageBox.Show( res ,systemEncoding.EncodingName);



            //var rand = new Random();
            //var k = rand.Next(10);
            //string str = ExtractTagContent(res, "ShipToDescription") + "-" + ExtractTagContent(res, "BillToDescription");
            //string sql = "INSERT INTO DBA.client   (ccode,cust_active,cust_unlimit_fl,cname) VALUES  ('UTF8-" + k + "','N','N','" + str + "'" + ")";
            //MessageBox.Show(res);
            //MessageBox.Show(str);

            //MessageBox.Show(sql);
            //OdbcHelper.ExecuteSql(sql);

            //res = ConvertToSpecifyEncode(res, Encoding.UTF8, Encoding.Default, EncodingConverter.ISO_8859_1);


            //如果设置了charset的时候，转换成指定的编码
            //if (!string.IsNullOrEmpty(this.charset))
            //{
            //    //res = EncodingConverter.ToCodePage(res, Encoding.Default, EncodingConverter.ISO_8859_1);
            //    //res = EncodingConverter.ToCodePage(res, systemEncoding , Encoding.GetEncoding(charset)); // is ok

            //    //res = EncodingConverter.ConvertToCodePage(res, Encoding.UTF8, Encoding.Default);
            //    res = EncodingConverter.ConvertToCodePage(res, Encoding.Default, Encoding.GetEncoding(charset));

            //    //res = EncodingConverter.ToCodePage(res, Encoding.UTF8, Encoding.GetEncoding(charset));

            //    //MessageBox.Show( res,Encoding.GetEncoding(charset).EncodingName);

            //    Logger.Debug("Response", "[Encoding to " + this.charset + "]\r\n" + string.Format(res));
            //    this.charset = "";
            //}

            if (null != DataReceived)
            {
                DataReceived(res);
            }
        }

        public void TriggerDataError(string res)
        {
            if (null != DataError)
            {
                DataError(res);
            }
        }

        public static bool ContainsBig5(string input)
        {
            Encoding big5 = Encoding.GetEncoding("big5");
            byte[] bytes = big5.GetBytes(input);
            string output = Encoding.Default.GetString(bytes);
            return output != input;
        }

        /// <summary>
        /// 校验是否number
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsNumber(string strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber) &&
                   !objTwoMinusPattern.IsMatch(strNumber) &&
                   objNumberPattern.IsMatch(strNumber);
        }



        public static string ConvertToSpecifyEncode(string input, Encoding srcEncode, Encoding sysEncode, Encoding dstEncode)
        {

            string res;

            byte[] srcBytes = srcEncode.GetBytes(input);

            byte[] dstBytes = Encoding.Convert(sysEncode, dstEncode, srcBytes);

            char[] dstChars = new char[dstEncode.GetCharCount(dstBytes, 0, dstBytes.Length)];

            dstEncode.GetChars(dstBytes, 0, dstBytes.Length, dstChars, 0);//利用char数组存储字符

            res = new string(dstChars);

            return res;

        }

        private static string ChangehttpsTohttp(string URL)
        {
            if (string.IsNullOrEmpty(URL))
            {
                throw new Exception("hisURI错误，为=" + URL);
            }
            URL = URL.ToLower().TrimStart();
            if (URL.StartsWith("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                }
                catch (Exception ex)
                {/* 部分电脑的错误
                    System.NotSupportedException: The requested security protocol is not supported.
                    */
                    Logger.Debug("ChangehttpsTohttp", ex.Message);
                    Logger.Debug("ChangehttpsTohttp", "https协议错误：hisURI" + URL);
                }
            }

            return URL;
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                Logger.Debug("ValidateServerCertificate", "https协议错误：sslPolicyErrors=" + sslPolicyErrors);
            }
            return true;
        }

        public void OnStartMonitor()
        {
            if (_messageReceiver != null)
            {
                _messageReceiver.OnStart();
            }
        }

        public void OnStopMonitor()
        {
            if (_messageReceiver != null)
            {
                _messageReceiver.OnStop();
            }
        }

        public void OnBindMonitor(string folderPath)
        {
            if (_messageReceiver != null)
            {
                _messageReceiver.BindFolder(folderPath);
                _messageReceiver.MessageReceive = message =>
                {
                    //var obj = JsonConvert.DeserializeObject(message);
                    //var newMessage = ObjectUtil.ToXML(obj);
                    //TriggerDataReceived(newMessage);
                    TriggerDataReceived(message);
                };
            }
        }

        public string ConvertJson2xml(string json)
        {
            var obj = JsonConvert.DeserializeObject(json);
            var xmlStr = ObjectUtil.ToXML(obj);
            return xmlStr;
        }

        #endregion


        #region Events

        // This section shows the examples of exposing a control's events.
        // Typically, you just need to
        // 1) Declare the event as you want it.
        // 2) Raise the event in the appropriate control event.

        [ComVisible(false)]
        public delegate void DataReceivedEventHandler(string result);
        public event DataReceivedEventHandler DataReceived = null;

        [ComVisible(false)]
        public delegate void DataErrordEventHandler(string errMsg);
        public event DataErrordEventHandler DataError = null;

        #endregion


    }

} // namespace CSActiveX
