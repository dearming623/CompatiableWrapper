using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Drawing.Imaging;
using System.Xml;
using MQOfficeTools.Models;
using System.Net;
using Newtonsoft.Json;
using MQOfficeTools.Common.Web;
using MQOfficeTools.Common.Util;
using Wrapper.Common.Utils;
using MoleQ.QRCode;
using MoleQ.QRCode.Util;
using MQOfficeTools.Utils;  

namespace MQOfficeTools
{
    [Guid("3763E3FA-C4C2-4b09-92E6-C8EFED5A79EC")]
    public interface AxMQActiveXCtrl
    {
        #region Properties

        string ErrorMsg { get; set; }
        long ErrorNum { get; set; }

        #endregion

        #region Methods

        //int csv2xmls(string csvFileName, string xmlsFileName, ref string errorMsg);

        void csv2xlsx(string csvFileName, string xlsxFileName);

        //long Convert(ref long lWidth, ref long lHeight, ref long lQual, ref string targPath);

        //long LoadPicture(ref string sPath);

        //void picResize(string sourcePath, string savePath, int destWidth, int destHeight, string bgColorHex);

        int picResize(string sourcePath, string savePath, int destWidth, int destHeight);

        void GetCentralItem(string xmlRequest); /* v1.0.0.4 added */

        void GetImage(string imageUrl, string savePath); /* v1.0.0.4 added */

        bool SetDefaultPrinter(string strPrinter); /* v1.0.0.5 added */

        string GetLocalPrinters(); /* v1.0.0.5 added */

        bool CheckFileExtension(string filePath, string ext); /* v1.0.0.8 added */

        //void JsonToExcelLegacy(string url, string body, string saveFile); /* v1.0.0.9 added */

        void FileConversion(string url, string jsonFile, string saveFile); /* v1.0.0.9 added */

        bool FileConvertToUTF8(string srcPath); /* v1.0.0.9 added */

        string ReplaceStr4pb7(string orgStr, string oldStr, string newStr); /* v1.0.0.9 added */

        void SetEnableLogger(bool enable);

        /* v1.0.11 added */
        int GenerateQRCode(string content, string path, int width, int height);

        #endregion
    }


    [Guid("2AA0D4BE-E597-4a3b-AFF3-E94DE2841E47")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface AxMQActiveXCtrlEvents
    {
        #region Events

        [DispId(1)]
        void DataError(string errMsg);
        [DispId(2)]
        void DataReceived(string result);
        [DispId(3)]
        void ImageReceived(int code, string result);
        [DispId(4)]
        void FileCnvtDone(string result);
        [DispId(5)]
        void FileCnvtError(string error_msg);

        #endregion
    }

    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(AxMQActiveXCtrlEvents))]
    [Guid("DA930176-EA6D-41a8-A503-08D606F67DB0")]
    public partial class Tools : UserControl, AxMQActiveXCtrl, IFileCnvtCallback
    {

        private const string TAG = "MQOfficeTools";


        #region ActiveX Control Registration

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

        public Tools()
        {
            InitializeComponent();

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
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            base.WndProc(ref m);
        }

        // Ensures that tabbing across the container and the .NET controls
        // works as expected
        void CSActiveXCtrl_LostFocus(object sender, EventArgs e)
        {
            ActiveXCtrlHelper.HandleFocus(this);
        }

        #region Properties

        public string ErrorMsg { get; set; }
        public long ErrorNum { get; set; }

        #endregion

        #region Methods

        ProcessingForm processingDialog = null;

        public string csvFileName;

        public string xlsxFileName;

        public int csv2xmls(string csvFileName, string xmlsFileName, ref string errorMsg)
        {
            //CSVファイルが選択したかどうかを判断する
            if (string.IsNullOrEmpty(csvFileName.Trim()))
            {
                //  MessageBox.Show("CSVファイルを選択してください！");
                return -1;
            }

            if (!File.Exists(csvFileName))
                return -2;

            ////保存先を選択する
            //SaveFileDialog saveDialog = new SaveFileDialog();
            //saveDialog.Filter = "Excle文件(*.xlsx)|*.xlsx";
            //if (saveDialog.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}

            errorMsg = "";
            Cursor.Current = Cursors.WaitCursor;
            ApplicationClass excelApp = null;
            Workbook excelWorkbook = null;

            try
            {
                excelApp = new ApplicationClass();

                // Workbookを開く
                excelWorkbook = excelApp.Workbooks.Open(csvFileName,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                ((Microsoft.Office.Interop.Excel._Worksheet)excelWorkbook.Worksheets.get_Item(1)).Activate();
                // 保存提示メッセージを禁止する
                //excelApp.Application.DisplayAlerts = false;

                // データを保存する  
                excelWorkbook.SaveAs(xmlsFileName, XlFileFormat.xlWorkbookDefault, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //  MessageBox.Show("エクセルファイルへ変換しました");

                File.Delete(csvFileName);

                return 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                errorMsg = ex.Message;
                return -3;
            }
            finally
            {
                //リリース釈放
                if (excelApp != null)
                {
                    try
                    {
                        excelWorkbook.Close(true, Type.Missing, Type.Missing);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (excelWorkbook != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                        }
                    }

                    try
                    {
                        excelApp.Quit();
                    }
                    finally
                    {
                        if (excelApp != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                        }
                    }

                    excelWorkbook = null;
                    excelApp = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                Cursor.Current = Cursors.Default;
            }
        }

        public void csv2xlsx(string csvFileName, string xmlsFileName)
        {
            if (processingDialog == null || processingDialog.IsDisposed)
            {
                processingDialog = new ProcessingForm();
                processingDialog.Show();
            }
            else
            {
                processingDialog.Activate();

                MessageBox.Show("Exporting file. Please try again later !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //processingDialog.Show();

            this.csvFileName = csvFileName;
            this.xlsxFileName = xmlsFileName;

            ThreadStart entry1 = new ThreadStart(Run1);
            Thread process1 = new Thread(entry1);
            process1.IsBackground = true;
            process1.Start();

        }

        public void Run1()
        {

            Cursor.Current = Cursors.WaitCursor;
            ApplicationClass excelApp = null;
            Workbook excelWorkbook = null;

            try
            {
                excelApp = new ApplicationClass();

                // Workbookを開く
                excelWorkbook = excelApp.Workbooks.Open(csvFileName,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                ((Microsoft.Office.Interop.Excel._Worksheet)excelWorkbook.Worksheets.get_Item(1)).Activate();
                // 保存提示メッセージを禁止する
                //excelApp.Application.DisplayAlerts = false;


                // データを保存する  
                excelWorkbook.SaveAs(xlsxFileName, XlFileFormat.xlWorkbookDefault, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //MessageBox.Show("エクセルファイルへ変換しました");

                //excelWorkbook.SaveCopyAs(xlsxFileName);//用了这个方法整个COM对象Crash了

                disposeProcessingForm();

                MessageBox.Show("File saved successfully !", "Save");

            }
            catch (Exception ex)
            {
                disposeProcessingForm();

                MessageBox.Show(ex.Message);

            }
            finally
            {

                if (File.Exists(csvFileName))
                {
                    File.Delete(csvFileName);
                }

                //リリース釈放
                if (excelApp != null)
                {
                    try
                    {
                        excelWorkbook.Close(true, Type.Missing, Type.Missing);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (excelWorkbook != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                        }
                    }

                    try
                    {
                        excelApp.Quit();
                    }
                    finally
                    {
                        if (excelApp != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                        }
                    }

                    excelWorkbook = null;
                    excelApp = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                Cursor.Current = Cursors.Default;
            }
        }

        public void disposeProcessingForm()
        {
            if (processingDialog != null)
            {
                processingDialog.Dispose();
            }
        }
        private string ipAddress;
        private string memberID;
        public void pdcxGetPointByMberID(string ipAddress, string memberID)
        {

            this.ipAddress = ipAddress;
            this.memberID = memberID;
            if (null != LoyaltyPointReturn)
            {

                Thread thread = new Thread(invokeWebService);
                thread.Start();

                //bool noerror = true;
                //string re = "0";
                //try
                //{
                //    string url = "http://" + ipAddress + "/loyaltyWebService.asmx";

                //    string[] args = new string[1];
                //    args[0] = memberID;

                //    object result = WebServiceHelper.InvokeWebService(url, "getPointsByMberID", args);

                //    re = Convert.ToString(result);
                //}
                //catch (Exception ex)
                //{
                //    noerror = false;
                //    re = ex.Message;
                //}

                //LoyaltyPointReturn(re, ref noerror);
            }

        }

        public void invokeWebService()
        {
            bool noerror = true;
            string re = "0";
            try
            {
                string url = "http://" + ipAddress + "/loyaltyWebService.asmx";

                string[] args = new string[1];
                args[0] = memberID;

                object result = WebServiceHelper.InvokeWebService(url, "getPointsByMberID", args);

                re = Convert.ToString(result);
            }
            catch (Exception ex)
            {
                noerror = false;
                re = ex.Message;
            }

            LoyaltyPointReturn(re, ref noerror);
        }

        private void OnEventDataError(string msg)
        {
            if (null != DataError)
                DataError(msg);
        }

        private void OnEventDataReceived(string msg)
        {
            if (null != DataReceived)
                DataReceived(msg);
        }

        private void OnEventImageReceived(int code, string msg)
        {
            if (null != ImageReceived)
                ImageReceived(code, msg);
        }

        private void OnEventFileCnvtDone(string result)
        {
            if (null != FileCnvtDone)
                FileCnvtDone(result);
        }

        private void OnEventFileCnvtError(string error_msg)
        {
            if (null != FileCnvtError)
                FileCnvtError(error_msg);
        }

        /*
        /* 获取打印机列表 */
        public string GetLocalPrinters()
        {
            string res = "";

            int cnt = 1;
            foreach (var item in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (cnt > 1)
                {
                    res += "|" + item;
                }
                else
                {
                    res += item;
                }
                cnt++;
            }

            return res;
        }

        /* 设置默认打印机 */
        public bool SetDefaultPrinter(string strPrinter)
        {
            //if (Environment.OSVersion.Version.Major < 5) // xp,window2000之前的版本
            //{
            //    return false;
            //}
            //else
            //{
            //    return LibWrapper.SetDefaultPrinter(strPrinter);
            //}

            return Win32API.SetDefaultPrinter(strPrinter);
        }

        /// <summary>
        /// 根据文件头判断文件类型
        /// </summary>
        /// <param name="filePath">filePath是文件的完整路径 </param>
        /// <param name="ext">ext是文件扩展名类型( 255216是jpg; 7173是gif; 6677是BMP,13780是PNG; 7790是exe, 8297是rar )</param>
        /// <returns>返回true或false</returns>
        public bool CheckFileExtension(string filePath, string ext)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(ext))
                return false;

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);
                string fileClass;
                byte buffer;
                buffer = reader.ReadByte();
                fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                fileClass += buffer.ToString();
                reader.Close();
                fs.Close();
                if (fileClass == ext)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //测试编码转换方法
        public void TestFun(string xmlRequest)
        {
            Logger.error("org_str: ", "ORG(" + xmlRequest + ")"); //测试使用

            //转换编码
            // xmlRequest =Utility.ConvertISO88591ToEncoding(xmlRequest, Encoding.UTF8);

            byte[] byteArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(xmlRequest);

            string ssss = "";
            foreach (var item in byteArray)
            {
                ssss += " " + item.ToString("X");
            }
            Logger.error("org_strx2: ", "ISO-8859-1(" + ssss + ")"); //测试使用

            Encoding defaultEncoding = Encoding.Default;
            Encoding pb7Encoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding CurrentSystemcomEncoding = Encoding.GetEncoding("big5");

            MessageBox.Show("" + defaultEncoding.CodePage);

            Encoding big5 = Encoding.GetEncoding("big5");
            Encoding ISO88591Encoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding GB2312Encoding = Encoding.GetEncoding("GB2312");

            string res = "";

            res = Utility.ConvertEncode(xmlRequest, big5, Encoding.UTF8);

            Logger.error("big5TOutf8: ", res); //测试使用

            string testStr = "餅";

            Logger.info("big5TOutf8: ", "繁體(" + testStr + ")"); //测试使用
            byte[] byteArray2 = big5.GetBytes(testStr);
            ssss = "";
            foreach (var item in byteArray2)
            {
                ssss += " " + item.ToString("X");
            }
            Logger.error("org_strx2: ", "繁體HEX(" + ssss + ")"); //测试使用


            string pbStr = ISO88591Encoding.GetString(byteArray2);
            OnEventDataReceived(pbStr);

            string res_11 = "";
            res_11 = Utility.ConvertEncode(xmlRequest, ISO88591Encoding, big5);
            Logger.error("org_strx2: ", "ISO88591->big5(" + res_11 + ")"); //测试使用

            byte[] fanti = new byte[2];
            fanti[1] = 0x05;
            fanti[0] = 0x99;
            Logger.error("org_strx2: ", "fanti(" + big5.GetString(fanti) + ")"); //测试使用

            fanti[0] = 0x05;
            fanti[1] = 0x99;
            Logger.error("org_strx2: ", "fanti(" + big5.GetString(fanti) + ")"); //测试使用
            //  Encoding gbk = Encoding.GetEncoding("GBK");

            //  byte[] buffer = gbk.GetBytes(xmlRequest);
            //  string text = gbk.GetString(buffer);

            //// string text = StringToISO_8859_1(xmlRequest);

            //  Logger.error("22222", text);
        }

        public void TestFun2(string xmlRequest)
        {
            Logger.error("AA", "Request(" + xmlRequest + ")"); //测试使用

            Encoding CurrentSystemcomEncoding = Encoding.Default;
            Encoding pb7Encoding = Encoding.GetEncoding("ISO-8859-1");

            xmlRequest = Utility.ConvertToSpecifyEncode(xmlRequest, pb7Encoding, CurrentSystemcomEncoding, Encoding.UTF8);

            Logger.error("AA", "Converted Request(" + xmlRequest + ")"); //测试使用

            string res = Utility.ConvertEncode(xmlRequest, CurrentSystemcomEncoding, pb7Encoding);

            // string res = Utility.ConvertToSpecifyEncode(xmlRequest, Encoding.UTF8, CurrentSystemcomEncoding,  pb7Encoding);

            Logger.error("AA", "Converted Response(" + res + ")"); //测试使用

            OnEventDataReceived(res);

            //string res = xmlRequest;

            //byte[] byteArray2 = CurrentSystemcomEncoding.GetBytes(res);

            //string pbStr = pb7Encoding.GetString(byteArray2);

            //Logger.error("AA", "Converted Response(" + pbStr + ")"); //测试使用

            //OnEventDataReceived(pbStr);
            return;
        }

        /// <summary>
        /// 获取Central Item的信息
        ///  added v1.0.0.4
        /// </summary>
        /// <param name="xmlRequest"></param>
        public void GetCentralItem(string xmlRequest)
        {
            //Logger.SetEnableLogger(true);

            //DebugFunc(xmlRequest);
            //DebugFunc2(xmlRequest);
            string encodingName = Utility.GetNodeText(xmlRequest, "<Encoding>", "</Encoding>");
            if (string.IsNullOrEmpty(encodingName))
                encodingName = "ISO-8859-1";

            Encoding specifiedEncoding = Encoding.GetEncoding(encodingName);
            int codepage = specifiedEncoding.CodePage; //CodePage check the comparison table
            switch (codepage)
            {
                case 65001: // UTF-8
                    //
                    break;
                case 28591:// ISO-8859-1 ( for PB7 )
                    xmlRequest = Utility.ConvertToSpecifyEncode(xmlRequest, CodePage.ISO_8859_1, CodePage.SYS_DEFAULT, CodePage.UTF8);
                    break;
                default:
                    xmlRequest = Utility.ConvertToSpecifyEncode(xmlRequest, specifiedEncoding, CodePage.SYS_DEFAULT, CodePage.UTF8);
                    break;
            }

            // Logger.debug("DEBUG", "\r\n=======================  Request ================== \r\n" + xmlRequest + "\r\n  =====================================================\r\n"); //测试使用


            //解析xml中的数据
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(xmlRequest);
            }
            catch (Exception ex)
            {
                OnEventDataError(ex.Message);
                return;
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii;

            string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            // DataError(json);//测试使用

            XMLRoot xmlRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<XMLRoot>(json);

            MQCentralItem centralItem = xmlRoot.MQCentralItem;

            if (!IsValidRequestInput(centralItem))
            {
                return;
            }

            BackgroundInvoke(centralItem, specifiedEncoding);

        }

        public void GetImage(string imageUrl, string saveFileName)
        {

            if (!Utility.IsUrl(imageUrl))
            {
                OnEventDataError("Invalid URL in the \"GetImage\" method.");
                return;
            }

            //if (!Utility.IsValidFileName3(saveFileName))
            //{
            //    OnEventDataError("Invalid file name in the \"GetImage\" method.");
            //    return;
            //}

            if (File.Exists(saveFileName))
            {
                File.Delete(saveFileName);
            }

            Thread saveImageThread = new Thread(delegate() { BackgroudDownlaodImage(imageUrl, saveFileName); });
            saveImageThread.Start();

        }

        private bool IsValidRequestInput(MQCentralItem MQCentralItem)
        {

            Response response = new Response();
            response.ResultCode = "1100";
            response.Message = "FORMAT_ERROR";

            string err = "Invalid request input.";

            if (MQCentralItem == null)
            {
                response.Message = err + " \"MQCentralItem\" tag not found. ";

                //OnEventDataError(Utility.Object2XMLString(response));
                OnEventDataError(response.ToXML());

                return false;
            }


            string[] properties = { "Gateway", "LocalImagePath" };
            string res = Utility.ValidationClassSpecifyProperties(MQCentralItem, new List<string>(properties));
            if (res != null)
            {

                response.Message = err + res;
                //OnEventDataError(Utility.Object2XMLString(response));
                OnEventDataError(response.ToXML());

                return false;
            }

            //移除保存图片到本地路径设置
            //string path = MQCentralItem.LocalImagePath;
            //if (!Directory.Exists(path))
            //{

            //    response.Message = "Image path does not exist.";
            //    OnEventDataError(Utility.Object2XMLString(response));

            //    return false;
            //}

            if (MQCentralItem.Request == null)
            {
                response.Message = err + " \"Request\" tag not found. ";
                //OnEventDataError(Utility.Object2XMLString(response));
                OnEventDataError(response.ToXML());
                return false;
            }



            string[] noNeedCheckProps = { "NumOfItem" };
            res = Utility.ValidationClassProperties(MQCentralItem.Request, new List<string>(noNeedCheckProps));
            if (res != null)
            {
                response.Message = err + res;
                //OnEventDataError(Utility.Object2XMLString(response));
                OnEventDataError(response.ToXML());
                return false;
            }

            return true;

        }

        private void DownloadImage(string url, string path)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            req.Method = "GET";
            req.KeepAlive = true;
            req.ContentType = "image/*";
            req.Timeout = 30 * 1000;
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            System.IO.Stream stream = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                Image.FromStream(stream).Save(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.error(TAG, "Failed to download image. URL: " + url);
                //Logger.error(TAG, ex.Message);
                throw ex;
            }
            finally
            {
                // 释放资源
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }


        }

        private void SaveImages(List<Product> products, string localImagePath)
        {

            if (products != null)
            {
                foreach (var product in products)
                {

                    if (!string.IsNullOrEmpty(product.ItemImage))
                    {

                        string fileName = localImagePath + "\\" + product.UPC + ".jpg";

                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }



                        //ImageParameter imageParm = new ImageParameter();
                        //imageParm.url = product.ItemImage;
                        //imageParm.path = "";

                        //Thread t = new Thread(new ParameterizedThreadStart(DownloadImage));
                        //t.Start(imageParm);

                        try
                        {
                            DownloadImage(product.ItemImage, fileName);

                            OnEventImageReceived(0, product.UPC + "|" + fileName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);

                            Logger.error(TAG, ex.Message);

                            OnEventImageReceived(1, product.UPC + "|" + ex.Message);
                        }
                    }

                }
            }
        }

        private void BackgroudDownlaodImage(string imageUrl, string saveFileName)
        {

            try
            {
                DownloadImage(imageUrl, saveFileName);

                OnEventImageReceived(0, "FilePath|" + saveFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Logger.error(TAG, ex.Message);

                //OnEventImageReceived(1, "ERROR|" + ex.Message);

                OnEventImageReceived(1, "ERROR|" + "Failed to retrieve image."); //v1.07
            }
        }

        private void BackgroundInvoke(MQCentralItem centralItem, Encoding encoding)
        {

            ThreadStart threadStart = new ThreadStart(delegate()
            {

                // string body = Utility.Object2XMLString(MQCentralItem);
                int numOfItem = Convert.ToInt32(centralItem.Request.NumOfItem);

                string body = Newtonsoft.Json.JsonConvert.SerializeObject(centralItem.Request);

                string result = HttpUtil.HttpPost(centralItem.Gateway, body);
                Logger.info("BackgroundInvoke", "Tools::BackgroundInvoke -> " + result);

                Response response = new Response();
                response.ResultCode = "1101";

                //string errMsg = "unknown";
                bool responseError = false;
                List<string> errorLst = null;

                /************* check result whether error *************/
                try
                {
                    errorLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result);

                    responseError = (errorLst != null && errorLst.Count > 0) ? true : false;

                    if (responseError)
                    {
                        StringBuilder sb = new StringBuilder();

                        if (errorLst != null && errorLst.Count > 0)
                        {
                            for (int i = 0; i < errorLst.Count; i++)
                            {
                                sb.Append(errorLst[0] + " ");
                            }
                        }

                        response.Message = sb.ToString();

                        //OnEventDataError(Utility.Object2XMLString(response));
                        OnEventDataError(response.ToXML(encoding));

                        return;
                    }
                }
                catch (Exception ex)
                {
                    //responseError = false;

                    //errMsg = ex.Message;

                    //response.Message = errMsg;

                    //OnEventDataError(Utility.Object2XMLString(response));

                    Logger.error("BackgroundInvoke", "Tools::BackgroundInvoke \r\n" + ex.Message);
                }
                /***********************************************/

                if (!responseError)
                {
                    List<Product> products = null;

                    response.ResultCode = "0";
                    response.Message = "Succeed";
                    //string fixResult = "{\"Products\":" + result + "}";

                    try
                    {
                        // products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(fixResult);

                        //AllProducts mAllProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<AllProducts>(fixResult);
                        //if (mAllProducts.Products != null && mAllProducts.Products.Count > 0)
                        //    response.Products = mAllProducts.Products;


                        products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(result);


                        if (products != null && products.Count > 0)
                        {


                            //移除保存图片到本地路径设置
                            //foreach (var item in products)
                            //{
                            //    if (string.IsNullOrEmpty(item.ItemImage))
                            //        continue;

                            //    item.ImageFile = MQCentralItem.LocalImagePath + "\\" + item.UPC + ".jpg";

                            //}

                            Products prods = new Products();
                            //返回大量Item, 选择前N个Item, numOfItem 默认为10
                            if (products.Count > numOfItem)
                            {
                                prods.Product = products.GetRange(0, numOfItem);

                            }
                            else
                            {
                                prods.Product = products;

                            }

                            response.Products = prods;

                            //移除保存图片到本地路径设置
                            //Thread saveImageThread = new Thread(delegate() { SaveImages(products, MQCentralItem.LocalImagePath); });
                            //saveImageThread.Start();


                        }
                        else
                        {
                            response.Message = "No Item.";
                        }

                        //OnEventDataReceived(Utility.Object2XMLString(response));
                        OnEventDataReceived(response.ToXML(encoding));

                    }
                    catch (Exception ex)
                    {
                        //返回结果不是产品信息
                        //response.Message = string.Format(TAG + ":{0}", ex.Message);
                        response.ResultCode = "1101";
                        response.Message = result;

                        //OnEventDataError(Utility.Object2XMLString(response));
                        OnEventDataError(response.ToXML(encoding));
                        Logger.error("BackgroundInvoke", "Tools::BackgroundInvoke \r\n" + ex.Message);
                    }

                }


            });

            Thread thread = new Thread(threadStart);

            thread.Start();//多线程启动匿名方法
        }

        /// <summary>
        /// 按照指定图片尺寸进行缩放 ( 版本:v1.02 日期: 2017-05-18 )
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="savePath"></param>
        /// <param name="destWidth"></param>
        /// <param name="destHeight"></param>
        /// <returns></returns>
        public int picResize(string sourcePath, string savePath, int destWidth, int destHeight)
        {
            return picResize(sourcePath, savePath, destWidth, destHeight, "#ffffff");
        }

        /// <summary>
        /// 图片缩放 ( 版本:v1.02 日期: 2017-05-18 )
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="savePath"></param>
        /// <param name="destWidth"></param>
        /// <param name="destHeight"></param>
        /// <returns></returns>
        public int picResize(string sourcePath, string savePath, int destWidth, int destHeight, string bgColorHex)
        {
            int errorCode = 0;
            ErrorMsg = "";

            if (!File.Exists(sourcePath))
            {
                errorCode = -1;
                ErrorMsg = "No image file.";
                return errorCode;
            }

            if (File.Exists(savePath))
            {
                try
                {
                    File.Delete(savePath);
                }
                catch (Exception ex)
                {
                    errorCode = -2;
                    ErrorMsg = ex.Message;
                    return errorCode;
                }
            }

            try
            {
                Image img = resizeImage(destWidth, destHeight, sourcePath, bgColorHex);

                img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                img.Dispose();
            }
            catch (Exception ex)
            {
                errorCode = -3;
                ErrorMsg = ex.Message;
            }

            return errorCode;
        }

        /// <summary>
        /// 图片等比缩放
        /// </summary>
        /// <param name="postedfile">原图</param>
        /// <param name="savepath">缩略图存放地址</param>
        /// <param name="targetwidth">指定的最大宽度</param>
        /// <param name="targetheight">指定的最大高度</param>
        public void zoomauto2(string initpath, string savepath, double targetwidth, double targetheight)
        {
            Logger.info("", string.Format("zoomauto is called. {0} {1} {2} {3}", initpath, savepath, targetwidth, targetheight));

            try
            {
                //创建目录
                string dir = Path.GetDirectoryName(savepath);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            catch (Exception ex)
            {
                Logger.info("", "create path error -> " + ex.Message);
                return;
            }


            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            Image initimage = Image.FromFile(initpath);

            Logger.info("", "load image file -> " + initpath);

            //原图宽高均小于模版，不作处理，直接保存
            if (initimage.Width <= targetwidth && initimage.Height <= targetheight)
            {
                //保存
                initimage.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //缩略图宽、高计算
                double newwidth = initimage.Width;
                double newheight = initimage.Height;
                //宽大于高或宽等于高（横图或正方）
                if (initimage.Width > initimage.Height || initimage.Width == initimage.Height)
                {
                    //如果宽大于模版
                    if (initimage.Width > targetwidth)
                    {
                        //宽按模版，高按比例缩放
                        newwidth = targetwidth;
                        newheight = initimage.Height * (targetwidth / initimage.Width);
                    }
                }
                //高大于宽（竖图）
                else
                {
                    //如果高大于模版
                    if (initimage.Height > targetheight)
                    {
                        //高按模版，宽按比例缩放
                        newheight = targetheight;
                        newwidth = initimage.Width * (targetheight / initimage.Height);
                    }
                }

                //生成新图
                //新建一个bmp图片
                Image newimage = new Bitmap((int)newwidth, (int)newheight);
                //新建一个画板
                Graphics newg = Graphics.FromImage(newimage);
                //设置质量
                newg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //置背景色
                newg.Clear(Color.White);
                //画图
                newg.DrawImage(initimage,
                    new System.Drawing.Rectangle(0, 0, newimage.Width, newimage.Height),
                    new System.Drawing.Rectangle(0, 0, initimage.Width, initimage.Height),
                    GraphicsUnit.Pixel);
                //保存缩略图
                newimage.Save(savepath + "1122.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //释放资源
                newg.Dispose();
                newimage.Dispose();
                initimage.Dispose();
            }
        }

        public Image resizeImage(int newWidth, int newHeight, string stPhotoPath, string bgColorHex)
        {
            Image imgPhoto = Image.FromFile(stPhotoPath);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            //Consider vertical pics
            //if (sourceWidth < sourceHeight)
            //{
            //    int buff = newWidth;

            //    newWidth = newHeight;
            //    newHeight = buff;
            //}

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(System.Drawing.ColorTranslator.FromHtml(bgColorHex)); // 设置背景
            grPhoto.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
                new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();

            return bmPhoto;
        }

        /// <summary>
        /// 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap ReadImageFile(string path)
        {
            FileStream fs = File.OpenRead(path); //OpenRead
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] image = new Byte[filelength]; //建立一个字节数组 
            fs.Read(image, 0, filelength); //按字节流读取 
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            Bitmap bit = new Bitmap(result);
            return bit;
        }

        //等比例缩放图片  
        private Bitmap ZoomImage(Bitmap bitmap, int destHeight, int destWidth)
        {
            try
            {
                System.Drawing.Image sourImage = bitmap;
                int width = 0, height = 0;
                //按比例缩放             
                int sourWidth = sourImage.Width;
                int sourHeight = sourImage.Height;
                if (sourHeight > destHeight || sourWidth > destWidth)
                {
                    if ((sourWidth * destHeight) > (sourHeight * destWidth))
                    {
                        width = destWidth;
                        height = (destWidth * sourHeight) / sourWidth;
                    }
                    else
                    {
                        height = destHeight;
                        width = (sourWidth * destHeight) / sourHeight;
                    }
                }
                else
                {
                    width = sourWidth;
                    height = sourHeight;
                }
                Bitmap destBitmap = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);
                //设置画布的描绘质量           
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourImage, new System.Drawing.Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height), 0, 0, sourImage.Width, sourImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                //设置压缩质量       
                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                sourImage.Dispose();
                return destBitmap;
            }
            catch
            {
                return bitmap;
            }
        }

        //此方法没有开放到外部使用
        public void JsonToExcelLegacy(string url, string body, string saveFile)
        {
            Logger.info("ExcelFile", string.Format("CreateExcelFile {0} to {1}, save file:{2}", url, body, saveFile));

            Dictionary<string, string> parameters = new Dictionary<string, string>();    //参数列表
            parameters.Add("fileName", Guid.NewGuid().ToString().Replace("-", ""));
            parameters.Add("fileType", "xlsx");

            string query = HttpUtil.BuildQuery(parameters, Encoding.UTF8.ToString());
            string urlWithQuery = url + "?" + query;

            ThreadPool.QueueUserWorkItem(new WaitCallback(HttpPostDataGenerateFile), new string[] { urlWithQuery, body, saveFile });
        }

        public bool FileConvertToUTF8(string srcPath)
        {
            string destPath = srcPath;
            return FileConvertToUTF8(srcPath, destPath);
        }

        /// <summary>
        ///  根据当前文本文件的编码转成UTF8编码
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <param name="isOverride"></param>
        /// <returns></returns>
        public bool FileConvertToUTF8(string srcPath, string destPath)
        {
            if (!File.Exists(srcPath))
            {
                OnEventFileCnvtError(string.Format("'{0}' file not found.", srcPath));
                return false;
            }

            if (ValidationUtil.IsValidPath(destPath))
            {
                OnEventFileCnvtError(string.Format("'{0}' Incorrect file path.", destPath));
                return false;
            }

            Encoding currentFileEncoding = TxtFileEncoder.GetEncoding(srcPath);
            //Encoding currentFileEncoding = EncodingDetector.GetEncoding2(srcPath);

            //bool isOverride = false;
            //if (srcPath == destPath)
            //{
            //    isOverride = true;
            //}

            //if (isOverride)
            //{
            //    destPath = srcPath;
            //}
            //else
            //{
            //    FileInfo fileInfo = new FileInfo(srcPath);
            //    string fileName = fileInfo.Name;
            //    int pos = fileName.IndexOf(fileInfo.Extension);
            //    string prefix = fileName.Substring(0, fileName.Length - fileInfo.Extension.Length);
            //    destPath = srcPath.Replace(fileInfo.Name, prefix + "_tmp" + fileInfo.Extension);
            //}

            //if (succ)
            //{
            //    OnEventFileCnvtDone("The file encoding was successfully converted.");
            //}
            //else
            //{
            //    OnEventFileCnvtError("Fail to convert file encoding. ");
            //} 
            Logger.info("FileConvertToUTF8", string.Format("Original code {0}, Convert to code {1}", currentFileEncoding.EncodingName, Encoding.UTF8.EncodingName));
            Encoding utf8WithoutBom = new UTF8Encoding(false); //set utf-8 without bom( Byte Order Mark )
            return FileUtil.ConvertTo(currentFileEncoding, utf8WithoutBom, srcPath, destPath);
        }


        /// <summary>
        /// 文件转换 外部方法，上存文件到服务器返回一个转换后的文件(例如:返回xlsx文件)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonFile"></param>
        /// <param name="saveFile"></param>
        public void FileConversion(string url, string jsonFile, string saveFile)
        {
            /********* 文件内容转码(转码后带BOM) *********/
            //FileConvertToUTF8(jsonFile, false);
            /******************************************************/

            //Logger.SetEnableLogger(true);
            //OnEventFileCnvtDone(Directory.GetCurrentDirectory());
            //OnEventFileCnvtError("Test EventFileCnvtError is ok.");
            //OnEventFileCnvtDone("Test EventFileCnvtDone is ok.");
            Logger.info("FileConversion", string.Format("FileConversion {0} to {1}, save file:{2}", url, jsonFile, saveFile));
            if (string.IsNullOrEmpty(url.Trim()) || string.IsNullOrEmpty(jsonFile.Trim()) || string.IsNullOrEmpty(saveFile.Trim()))
            {
                OnEventFileCnvtError("Parameter cannot be empty.");
                return;
            }

            if (ValidationUtil.IsValidURL(url))
            {
                OnEventFileCnvtError(string.Format("'{0}' invalid URL.", url));
                return;
            }

            if (!File.Exists(jsonFile))
            {
                OnEventFileCnvtError(string.Format("'{0}' file not found.", jsonFile));
                return;
            }

            //压缩Json文件
            //bool succ = FileUtil.CompressJsonFile(jsonFile);
            //if (!succ)
            //{
            //    OnEventFileCnvtError(string.Format("Fail to compress '{0}' file.", jsonFile));
            //    return;
            //}

            //验证是否合法的Json文件
            bool succ = FileUtil.IsValidJSON(jsonFile);
            if (!succ)
            {
                OnEventFileCnvtError(string.Format("Incorrect JSON format in '{0}' file.", jsonFile));
                return;
            }

            /********* 文件内容转码(测试可以正常使用) *********/
            //Encoding BIG5 = Encoding.GetEncoding("big5");
            //Encoding ISO88591 = Encoding.GetEncoding("ISO-8859-1");
            //Encoding GB2312 = Encoding.GetEncoding("GB2312");
            //Encoding UTF8 = Encoding.UTF8;
            //FileUtil.ConvertTo(GB2312, UTF8, jsonFile, jsonFile.Replace(".json", "_temp.json"));
            /******************************************************/



            //URL添加参数
            //Dictionary<string, string> parameters = new Dictionary<string, string>();    //参数列表
            //parameters.Add("fileName", Guid.NewGuid().ToString().Replace("-", ""));
            //parameters.Add("fileType", "xlsx");

            //string query = HttpUtil.BuildQuery(parameters, Encoding.UTF8.ToString());
            //string urlWithQuery = url + "?" + query;

            ThreadPool.QueueUserWorkItem(new WaitCallback(HttpPostJsonFileGenerateExcelFile), new string[] { url, jsonFile, saveFile });
        }

        /// <summary>
        /// 打开/关闭 日志功能
        /// </summary>
        /// <param name="enable"></param>
        public void SetEnableLogger(bool enable)
        {
            Logger.SetEnableLogger(enable);
        }

        public string ReplaceStr4pb7(string orgStr, string oldStr, string newStr)
        {
            Logger.info("ReplaceStr4pb7", string.Format("Before replace from PB string orgStr:{0} oldStr: {1} newStr:{2}", orgStr, oldStr, newStr));
            string orgVal = Utility.ConvertToSpecifyEncode(orgStr, CodePage.ISO_8859_1, CodePage.SYS_DEFAULT, Encoding.UTF8);//string str = Utility.ConvertToSpecifyEncode(orgStr, Utility.pb7Encoding, Utility.CurrentSystemEncoding, Encoding.UTF8);
            string oldVal = Utility.ConvertToSpecifyEncode(oldStr, CodePage.ISO_8859_1, CodePage.SYS_DEFAULT, Encoding.UTF8);
            string newVal = Utility.ConvertToSpecifyEncode(newStr, CodePage.ISO_8859_1, CodePage.SYS_DEFAULT, Encoding.UTF8);
            Logger.info("ReplaceStr4pb7", string.Format("After convert encoding orgStr:{0} oldStr: {1} newStr:{2}", orgVal, oldVal, newVal));
            string afterVal = orgVal.Replace(oldVal, newVal);
            Logger.info("ReplaceStr4pb7", string.Format("After replace {0}  ", afterVal));
            return Utility.ConvertEncode(afterVal, CodePage.SYS_DEFAULT, CodePage.ISO_8859_1);
        }

        private void HttpPostDataGenerateFile(object obj)
        {
            string[] param = (string[])obj;
            string url = param[0];
            string body = param[1];
            string saveFile = param[2];

            Logger.info("HttpPostDataGenerateFile", string.Format("{0} to {1}, save file:{2}", url, body, saveFile));
            try
            {
                HttpUtil.PostDataGenerateFile(url, body, saveFile);
                OnEventFileCnvtDone(saveFile);
            }
            catch (Exception ex)
            {
                OnEventFileCnvtError(ex.Message);
            }
        }

        private void HttpPostJsonFileGenerateExcelFile(object obj)
        {
            string contentType = "application/json";
            string[] param = (string[])obj;
            string url = param[0];
            string jsonFile = param[1];
            string saveFile = param[2];

            Logger.info("HttpPostJsonFileGenerateExcelFile", string.Format("{0} to {1}, save file:{2}", url, jsonFile, saveFile));

            HttpUtil.PostJsonFileGenerateExcel(url, jsonFile, "file", contentType, null, saveFile, this);
        }


        /// <summary>
        /// Generate QRCode - added v1.0.11
        /// </summary>
        /// <param name="content">条码的内容</param>
        /// <param name="path">生成文件的路径,文件为jpg</param>
        /// <param name="width">条码图片的宽度</param>
        /// <param name="height">条码图片的高度</param>
        /// <returns> 
        /// 0 - Success 
        /// 1 - Incorrect path
        /// 2 - Incorrent content
        /// 3 - 预留
        /// 4 - Incorrent image size
        /// 5 - Fail to generate qrcode
        /// 6 - Invalid extension
        /// </returns>
        public int GenerateQRCode(string content, string path, int width, int height)
        {  
            if (string.IsNullOrEmpty(content))
            {
                return ResultCode.INCORRECT_CONTENT;
            }

            if (!DirUtil.VaildatePath(path))
                return ResultCode.INCORRECT_PATH;

            if (width <= 0 && height <= 0)
            {
                return ResultCode.INCORRECT_SIZE;
            }

            var ext = Path.GetExtension(path);
            ImageFormat format = DirUtil.GetImageFormat(ext);
            if (format == null)
            {
                return ResultCode.INVALID_EXTENSION;
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            try
            {
                var bitmap = BarcodeGenerator.CreateQRCode(content, width, height);
                bitmap.Save(path, format);
                bitmap.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("log -> " + ex.ToString());
                return ResultCode.FAIL_GENERATE_QRCODE;
            }

            return ResultCode.SUCCESS;


        }


        #endregion

        #region Events

        [ComVisible(false)]
        public delegate void LoyaltyPointReturnEventHandler(string points, ref bool noError);
        public event LoyaltyPointReturnEventHandler LoyaltyPointReturn = null;

        [ComVisible(false)]
        public delegate void DataReceivedEventHandler(string result);
        public event DataReceivedEventHandler DataReceived = null;

        [ComVisible(false)]
        public delegate void DataErrordEventHandler(string errMsg);
        public event DataErrordEventHandler DataError = null;

        [ComVisible(false)]
        public delegate void ImageReceivedEventHandler(int code, string result);
        public event ImageReceivedEventHandler ImageReceived = null;

        [ComVisible(false)]
        public delegate void FileCnvtDoneEventHandler(string result);
        public event FileCnvtDoneEventHandler FileCnvtDone = null;

        [ComVisible(false)]
        public delegate void FileCnvtErrorEventHandler(string error_msg);
        public event FileCnvtErrorEventHandler FileCnvtError = null;


        #endregion



        #region IFileCnvtCallback 成员

        void IFileCnvtCallback.FileConvertError(string errorMessage)
        {
            OnEventFileCnvtError(errorMessage);
        }

        void IFileCnvtCallback.FileConvertSuccess(string result)
        {
            OnEventFileCnvtDone(result);
        }

        #endregion
    }
}
