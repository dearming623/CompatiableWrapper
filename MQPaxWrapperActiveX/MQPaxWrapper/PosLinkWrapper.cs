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
using System.IO;
using System.Collections;
using System.Reflection;
using System.Drawing.Imaging;
using MQPaxWrapper.Model;
using MQPaxWrapper.Exceptions;

namespace MQPaxWrapper
{
    [Guid("9EF41F36-BC61-4c41-B82E-4B98B1479D64")]
    public interface AxMQPAXActiveXCtrl
    {
        #region Properties

        int errorCode { get; set; }
        string errorMessage { get; set; }
        string transResultMsg { get; set; }
        bool enableWrapperLog { get; set; }
        //string respInputText { get; set; } // v1.16 remove

        string SerialNumber { get; set; }
        //string VarValue { get; set; }
        string ResponseData { get; set; }

        //string Setting_CommType { get; set; }
        //string Setting_TimeOut { get; set; }
        //string Setting_SerialPort { get; set; }
        //string Setting_DestIP { get; set; }
        //string Setting_DestPort { get; set; }
        //string Setting_BaudRate { get; set; } 

        //string paymentRequestTenderType { get; set; }
        //string paymentRequestTransType { get; set; }
        //string paymentRequestAmount { get; set; }
        //string paymentRequestOrigRefNum { get; set; }
        //string paymentRequestInvNum { get; set; }
        //string paymentRequestUserID { get; set; }
        //string paymentRequestPassWord { get; set; }
        //string paymentRequestClerkID { get; set; }
        //string paymentRequestServerID { get; set; }
        //string paymentRequestTipAmt { get; set; }
        //string paymentRequestTaxAmt { get; set; }
        //string paymentRequestCashbackAmt { get; set; }
        //string paymentRequestMisc1Amt { get; set; }
        //string paymentRequestMisc2Amt { get; set; }
        //string paymentRequestMisc3Amt { get; set; }
        //string paymentRequestECRefNum { get; set; }
        //string paymentRequestCustomFields { get; set; }
        //string paymentRequestCustomerName { get; set; }
        //string paymentRequestSurchargeAmt { get; set; }
        //string paymentRequestPONum { get; set; }
        //string paymentRequestStreet { get; set; }
        //string paymentRequestZIP { get; set; }
        //string paymentRequestCssPath { get; set; }
        //string paymentRequestExtData { get; set; }
        //string paymentRequestAuthCode { get; set; }


        string paymentResponseResultCode { get; set; }
        string paymentResponseResultTxt { get; set; }
        string paymentResponseRefNum { get; set; }
        string paymentResponseRawResponse { get; set; }
        string paymentResponseAvsResponse { get; set; }
        string paymentResponseCvResponse { get; set; }
        string paymentResponseTimestamp { get; set; }
        string paymentResponseHostCode { get; set; }
        string paymentResponseRequestedAmt { get; set; }
        string paymentResponseApprovedAmt { get; set; }
        string paymentResponseRemainingBalance { get; set; }
        string paymentResponseExtraBalance { get; set; }
        string paymentResponseHostResponse { get; set; }
        string paymentResponseBogusAccountNum { get; set; }
        string paymentResponseCardType { get; set; }
        string paymentResponseMessage { get; set; }
        string paymentResponseExtData { get; set; }
        string paymentResponseAuthCode { get; set; }


        //string manageRequestEDCType { get; set; }
        //string manageRequestTransType { get; set; }
        //string manageRequestTrans { get; set; }
        //string manageRequestVarName { get; set; }
        //string manageRequestVarValue { get; set; }
        //string manageRequestTitle { get; set; }
        //string manageRequestButton1 { get; set; }
        //string manageRequestButton2 { get; set; }
        //string manageRequestButton3 { get; set; }
        //string manageRequestButton4 { get; set; }
        //string manageRequestDisplayMessage { get; set; }
        //string manageRequestImagePath { get; set; }
        //string manageRequestImageName { get; set; }
        //string manageRequestUpload { get; set; }
        //string manageRequestHRefNum { get; set; }
        //string manageRequestTimeout { get; set; }
        //string manageRequestThankYouTitle { get; set; }
        //string manageRequestThankYouMessage1 { get; set; }
        //string manageRequestThankYouMessage2 { get; set; }
        //string manageRequestThankYouTimeout { get; set; }
        //string manageRequestAccountNumber { get; set; }
        //string manageRequestEncryptionType { get; set; }
        //string manageRequestKeySlot { get; set; }
        //string manageRequestPinMaxLength { get; set; }
        //string manageRequestPinMinLength { get; set; }
        //string manageRequestNullPin { get; set; }
        //string manageRequestPinAlgorithm { get; set; }
        //string manageRequestMagneticSwipeEntryFlag { get; set; }
        //string manageRequestManualEntryFlag { get; set; }
        //string manageRequestContactlessEntryFlag { get; set; }
        //string manageRequestScannerEntryFlag { get; set; }
        //string manageRequestEncryptionFlag { get; set; }
        //string manageRequestMAXAccountLength { get; set; }
        //string manageRequestMINAccountLength { get; set; }
        //string manageRequestExpiryDatePrompt { get; set; }
        //string manageRequestInputType { get; set; }
        //string manageRequestDefaultValue { get; set; }
        //string manageRequestMAXLength { get; set; }
        //string manageRequestMINLength { get; set; }
        //string manageRequestFileName { get; set; }
        //string manageRequestSigSavePath { get; set; }

        string manageResponseResultCode { get; set; }
        string manageResponseResultTxt { get; set; }
        string manageResponseSN { get; set; }
        string manageResponseVarValue { get; set; }
        //string manageResponseBottonNum { get; set; } //v1.09 removed
        string manageResponseSigFileName { get; set; }
        string manageResponsePinBlock { get; set; }
        string manageResponseKSN { get; set; }
        string manageResponseTrack1Data { get; set; }
        string manageResponseTrack2Data { get; set; }
        string manageResponseTrack3Data { get; set; }
        string manageResponsePAN { get; set; }
        string manageResponseQRCode { get; set; }
        string manageResponseEntryMode { get; set; }
        string manageResponseExpiryDate { get; set; }
        string manageResponseText { get; set; }

        #endregion

        #region Methods

        /*

        //void Test(string val);
        void setCommSetting(string CommType, string TimeOut, string SerialPort, string DestIP, string DestPort, string BaudRate);
        //void processTrans(string TenderType, string TransType, string Amount, string OrigRefNum, string InvNum,
        //                            string UserID, string PassWord, string ClerkID, string ServerID, string TipAmt, string TaxAmt,
        //                            string CashbackAmt, string Misc1Amt, string Misc2Amt, string Misc3Amt, string ECRefNum,
        //    string CustomFields, string CustomerName, string SurchargeAmt, string PONum, string Street, string ZIP, string CssPath,
        //    string ExtData, string AuthCode);

        void processTrans();
        void doProcessTrans();
        MQPaxWrapper.PosLinkWrapper.PersonStruct test1(MQPaxWrapper.PosLinkWrapper.PersonStruct requet);
        MQPaxWrapper.PosLinkWrapper.PersonClass test2(MQPaxWrapper.PosLinkWrapper.PersonClass requet);
        ArrayList testReturnArrayList();
        string[] testReturnStringArray();
        Dictionary<string,string> testRetrunMap();
        void getSignature();
         
         */

        //void Init(string CommType, string TimeOut, string DestIP, string DestPort);
        int Init(string CommType, int timeout, string DestIP, string DestPort);
        void ProcessTrans(string TenderType, string TransType, string Amount, string ECRefNum, string ExtData, string CashbackAmt, int timeout);
        void GetSignature(int upload, string sigSavePath, string picName, int timeout);
        void DoSignature(int upload, int timeout);
        void RemoveCard(int timeout);
        int IsOnline();
        void EnableLog(int k);
        void ReadCard(int timeout);
        void Reset(); // v1.10 added
        void RequestInputText(string title, int inputType, int timeout);
        void CancelTrans();
        int ConvertPNG2JPG(string pngPath, int deletePNG);// v1.14 added 
        void GetVar(int EDCType, string varName);//v1.16 added
        void QueryDetailReport(string EcrRefNum);// v.1.18 added
        #endregion
    }


    [Guid("641F612F-7EFD-40ef-94C0-93AA12E0F7D8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface AxMQPAXActiveXCtrlEvents
    {
        #region Events

        // v1.16 added
        [DispId(1)]
        void OnResponse(string result);
        [DispId(2)]
        void onStatusChanged(int statuscode, string msg);

        //************ v1.16 removed ***************//
        //[DispId(1)]
        //void onPaymentCompleted();
        //[DispId(2)]
        //void onManageCompleted();
        //[DispId(3)]
        //void onError();
        //[DispId(4)]
        //void onStatusChanged(int statuscode, string msg);
        //[DispId(5)]
        //void onDoSignatureCompleted();
        //[DispId(6)]
        //void onRemoveCardCompleted();
        //[DispId(7)]
        //void onReadCardCompleted();

        //// v1.10 added
        //[DispId(8)]
        //void onResetCompleted();

        //// v1.11 added
        ////[DispId(9)]
        ////void onRequestInputTextCompleted(string res);
        //[DispId(9)]
        //void onRequestInputTextCompleted();

        //********************************************//

        //[DispId(1)]
        //void onReturnReportedStatus(int status);
        //[DispId(2)]
        //void onProcessError(string msg);
        //[DispId(3)]
        //void onActionTimeOut();
        //[DispId(4)]
        //void onProcessTransComplete();
        //[DispId(5)]
        //void onProcessManageComplete();
        //[DispId(6)]
        //void onSignatureReceived(string SigFileName);




        #endregion
    }

    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(AxMQPAXActiveXCtrlEvents))]
    [Guid("AB3D7D70-A59D-463f-9A45-770189557A2A")]
    public partial class PosLinkWrapper : UserControl, AxMQPAXActiveXCtrl, CommWrapper
    {

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

        public PosLinkWrapper()
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

        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public string transResultMsg { get; set; }
        public bool enableWrapperLog { get; set; }

        //public string respInputText { get; set; } // v1.16 remove
        public bool isCancelTask = false;

        public string SerialNumber { get; set; }
        //public string VarValue { get; set; }
        public string ResponseData { get; set; }

        public string Setting_CommType { get; set; }
        public string Setting_TimeOut { get; set; }
        public string Setting_SerialPort { get; set; }
        public string Setting_DestIP { get; set; }
        public string Setting_DestPort { get; set; }
        public string Setting_BaudRate { get; set; }

        #region payment request parameter
        public string paymentRequestTenderType { get; set; }
        public string paymentRequestTransType { get; set; }
        public string paymentRequestAmount { get; set; }
        public string paymentRequestOrigRefNum { get; set; }
        public string paymentRequestInvNum { get; set; }
        public string paymentRequestUserID { get; set; }
        public string paymentRequestPassWord { get; set; }
        public string paymentRequestClerkID { get; set; }
        public string paymentRequestServerID { get; set; }
        public string paymentRequestTipAmt { get; set; }
        public string paymentRequestTaxAmt { get; set; }
        public string paymentRequestCashbackAmt { get; set; }
        public string paymentRequestMisc1Amt { get; set; }
        public string paymentRequestMisc2Amt { get; set; }
        public string paymentRequestMisc3Amt { get; set; }
        public string paymentRequestECRefNum { get; set; }
        public string paymentRequestCustomFields { get; set; }
        public string paymentRequestCustomerName { get; set; }
        public string paymentRequestSurchargeAmt { get; set; }
        public string paymentRequestPONum { get; set; }
        public string paymentRequestStreet { get; set; }
        public string paymentRequestZIP { get; set; }
        public string paymentRequestCssPath { get; set; }
        public string paymentRequestExtData { get; set; }
        public string paymentRequestAuthCode { get; set; }


        #endregion

        #region payment response parameter
        public string paymentResponseResultCode { get; set; }
        public string paymentResponseResultTxt { get; set; }
        public string paymentResponseRefNum { get; set; }
        public string paymentResponseRawResponse { get; set; }
        public string paymentResponseAvsResponse { get; set; }
        public string paymentResponseCvResponse { get; set; }
        public string paymentResponseTimestamp { get; set; }
        public string paymentResponseHostCode { get; set; }
        public string paymentResponseRequestedAmt { get; set; }
        public string paymentResponseApprovedAmt { get; set; }
        public string paymentResponseRemainingBalance { get; set; }
        public string paymentResponseExtraBalance { get; set; }
        public string paymentResponseHostResponse { get; set; }
        public string paymentResponseBogusAccountNum { get; set; }
        public string paymentResponseCardType { get; set; }
        public string paymentResponseMessage { get; set; }
        public string paymentResponseExtData { get; set; }
        public string paymentResponseAuthCode { get; set; }


        #endregion

        #region manage request parameter
        public string manageRequestEDCType { get; set; }
        public string manageRequestTransType { get; set; }
        public string manageRequestTrans { get; set; }
        public string manageRequestVarName { get; set; }
        public string manageRequestVarValue { get; set; }
        public string manageRequestTitle { get; set; }
        public string manageRequestButton1 { get; set; }
        public string manageRequestButton2 { get; set; }
        public string manageRequestButton3 { get; set; }
        public string manageRequestButton4 { get; set; }
        public string manageRequestDisplayMessage { get; set; }
        public string manageRequestImagePath { get; set; }
        public string manageRequestImageName { get; set; }
        public string manageRequestUpload { get; set; }
        public string manageRequestHRefNum { get; set; }
        public string manageRequestTimeout { get; set; }
        public string manageRequestThankYouTitle { get; set; }
        public string manageRequestThankYouMessage1 { get; set; }
        public string manageRequestThankYouMessage2 { get; set; }
        public string manageRequestThankYouTimeout { get; set; }
        public string manageRequestAccountNumber { get; set; }
        public string manageRequestEncryptionType { get; set; }
        public string manageRequestKeySlot { get; set; }
        public string manageRequestPinMaxLength { get; set; }
        public string manageRequestPinMinLength { get; set; }
        public string manageRequestNullPin { get; set; }
        public string manageRequestPinAlgorithm { get; set; }
        public string manageRequestMagneticSwipeEntryFlag { get; set; }
        public string manageRequestManualEntryFlag { get; set; }
        public string manageRequestContactlessEntryFlag { get; set; }
        public string manageRequestScannerEntryFlag { get; set; }
        public string manageRequestEncryptionFlag { get; set; }
        public string manageRequestMAXAccountLength { get; set; }
        public string manageRequestMINAccountLength { get; set; }
        public string manageRequestExpiryDatePrompt { get; set; }
        public string manageRequestInputType { get; set; }
        public string manageRequestDefaultValue { get; set; }
        public string manageRequestMAXLength { get; set; }
        public string manageRequestMINLength { get; set; }
        public string manageRequestFileName { get; set; }
        public string manageRequestSigSavePath { get; set; }
        #endregion

        #region manage response parameter
        public string manageResponseResultCode { get; set; }
        public string manageResponseResultTxt { get; set; }
        public string manageResponseSN { get; set; }
        public string manageResponseVarValue { get; set; }
        //public string manageResponseBottonNum { get; set; } //v1.09 removed
        public string manageResponseSigFileName { get; set; }
        public string manageResponsePinBlock { get; set; }
        public string manageResponseKSN { get; set; }
        public string manageResponseTrack1Data { get; set; }
        public string manageResponseTrack2Data { get; set; }
        public string manageResponseTrack3Data { get; set; }
        public string manageResponsePAN { get; set; }
        public string manageResponseQRCode { get; set; }
        public string manageResponseEntryMode { get; set; }
        public string manageResponseExpiryDate { get; set; }
        public string manageResponseText { get; set; }
        #endregion

        #endregion

        private const string TAG = "PosLinkWrapper";
        private string sigSavePath = "";
        private string sigPicName = "";

        #region Methods

        public void Test(string val)
        {
            MessageBox.Show("Object is OK。 i receive value: " + val);
        }

        public struct PersonStruct
        {
            public string name;
            public int age;
        }

        public class PersonClass
        {
            public string name;
            public int age;
        }

        public PersonStruct test1(PersonStruct requet)
        {
            MessageBox.Show("test1 is OK。 ");
            PersonStruct person = new PersonStruct();
            person.age = 30;
            person.name = "PersonStruct Deraming ";
            return person;
        }

        public PersonClass test2(PersonClass requet)
        {
            MessageBox.Show("test1 is OK。 ");
            PersonClass person = new PersonClass();
            person.age = 25;
            person.name = "PersonClass Deraming ";
            return person;
        }

        public ArrayList testReturnArrayList()
        {
            ArrayList lst = new ArrayList();
            lst.Add("Function -> testReturnArrayList");
            lst.Add("Type -> ArrayList");
            lst.Add("Time -> " + DateTime.Now.ToShortTimeString());
            return lst;
        }
        public string[] testReturnStringArray()
        {
            string[] str_array = new string[3];
            str_array[0] = "Function -> testReturnStringArray";
            str_array[1] = "Type -> string[]";
            str_array[2] = "OK";
            return str_array;
        }
        public Dictionary<string, string> testRetrunMap()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            map.Add("Function", "testRetrunMap");
            map.Add("Type", "hashmap");
            return map;
        }

        //public void setCommSetting(string CommType, string TimeOut, string SerialPort, string DestIP, string DestPort, string BaudRate)
        //{
        //    string errorMsg = "";

        //    // 1. First create a POSLink Commsetting Page

        //    POSLink.PosLink cg = new POSLink.PosLink();
        //    POSLink.CommSetting commSetting = new POSLink.CommSetting();

        //    // 2. Next Set the PayLink Properties, the only required field is 
        //    #region Set PayLink Properties

        //    // Set the required field: none

        //    // All these below are optionals
        //    commSetting.CommType = CommType;
        //    commSetting.TimeOut = TimeOut;
        //    commSetting.SerialPort = SerialPort;
        //    commSetting.DestIP = DestIP;
        //    commSetting.DestPort = DestPort;
        //    commSetting.BaudRate = BaudRate;
        //    commSetting.saveFile();

        //    #endregion

        //    //save the setting into file, therefore other classes will use the setting communication
        //    cg.CommSetting = commSetting;
        //    //POSLink.ProcessTransResult result = cg.ProcessTrans();

        //    // 4. Show the result none

        //    StreamWriter log = new StreamWriter(@"C:\\log.txt", true);

        //    log.WriteLine(@"CommType: " + CommType + "\r\n" +
        //    "TimeOut: " + TimeOut + "\r\n" +
        //    "SerialPort: " + SerialPort + "\r\n" +
        //    "DestIP: " + DestIP + "\r\n" +
        //    "DestPort: " + DestPort + "\r\n" +
        //    "BaudRate: " + BaudRate + "\r\n" +
        //    "Error Message: " + errorMsg + "\r\n ");

        //    log.Close();
        //}


        //POSLink.PosLink poslink = new POSLink.PosLink();
        POSLink.PosLink poslink = PosLinkEngine.GetInstance().GetPosLinkInstance();
        POSLink.ProcessTransResult result = new POSLink.ProcessTransResult();



        Thread process2;
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void ProcessTransRunV119()
        {

            errorCode = 0;
            transResultMsg = "";

            POSLink.PaymentRequest paymentRequest = new POSLink.PaymentRequest();

            paymentRequest.TenderType = paymentRequest.ParseTenderType(paymentRequestTenderType);
            paymentRequest.TransType = paymentRequest.ParseTransType(paymentRequestTransType);

            #region Set PayLink Properties

            // Set the only required field: Amount
            string amount = paymentRequestAmount;
            // amount.to
            if (amount == "")
            {
                paymentRequest.Amount = "";
            }
            else
            {
                //amount = amount.Remove(amount.Length - 3, 1);
                double ret = Convert.ToDouble(amount);
                // ret = ret * 100;

                String retstr = Convert.ToString(ret);
                paymentRequest.Amount = retstr;
            }


            // All these below are optionals
            paymentRequest.OrigRefNum = paymentRequestOrigRefNum;
            paymentRequest.InvNum = paymentRequestInvNum;
            paymentRequest.UserID = paymentRequestUserID;
            paymentRequest.PassWord = paymentRequestPassWord;
            paymentRequest.ClerkID = paymentRequestClerkID;
            paymentRequest.ServerID = paymentRequestServerID;
            if (paymentRequestTipAmt.Length > 0) paymentRequest.TipAmt = paymentRequestTipAmt;
            if (paymentRequestTaxAmt.Length > 0) paymentRequest.TaxAmt = paymentRequestTaxAmt;
            if (paymentRequestCashbackAmt.Length > 0) paymentRequest.CashBackAmt = paymentRequestCashbackAmt;
            if (paymentRequestMisc1Amt.Length > 0) paymentRequest.Misc1Amt = paymentRequestMisc1Amt;
            if (paymentRequestMisc2Amt.Length > 0) paymentRequest.Misc2Amt = paymentRequestMisc2Amt;
            if (paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = paymentRequestMisc3Amt;
            if (paymentRequestECRefNum.Length > 0)
            {
                paymentRequest.ECRRefNum = paymentRequestECRefNum;
            }
            if (paymentRequestCustomFields.Length > 0 || paymentRequestCustomerName.Length > 0)
            {

            }
            if (paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = paymentRequestMisc3Amt;
            if (paymentRequestSurchargeAmt.Length > 0) paymentRequest.SurchargeAmt = paymentRequestSurchargeAmt;
            paymentRequest.PONum = paymentRequestPONum;
            paymentRequest.Street = paymentRequestStreet;
            paymentRequest.Zip = paymentRequestZIP;
            paymentRequest.CssPath = paymentRequestCssPath;
            paymentRequest.ExtData = paymentRequestExtData;
            paymentRequest.AuthCode = paymentRequestAuthCode;
            paymentRequest.SigSavePath = sigSavePath;
            #endregion

            CommResponse commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Unknown error: pg.PaymentResponse is null.", Data = string.Empty };
            try
            {
                POSLink.PaymentResponse paymentResponse = ProcessPaymentRequestRun(paymentRequest);
                if (paymentResponse != null)
                {
                    paymentResponseResultCode = paymentResponse.ResultCode;
                    paymentResponseResultTxt = paymentResponse.ResultTxt;
                    paymentResponseRefNum = paymentResponse.RefNum;
                    paymentResponseRawResponse = paymentResponse.RawResponse;
                    paymentResponseAvsResponse = paymentResponse.AvsResponse;
                    paymentResponseCvResponse = paymentResponse.CvResponse;
                    paymentResponseTimestamp = paymentResponse.Timestamp;
                    paymentResponseHostCode = paymentResponse.HostCode;
                    paymentResponseRequestedAmt = paymentResponse.RequestedAmount;
                    paymentResponseApprovedAmt = paymentResponse.ApprovedAmount;
                    paymentResponseRemainingBalance = paymentResponse.RemainingBalance;
                    paymentResponseExtraBalance = paymentResponse.ExtraBalance;
                    paymentResponseHostResponse = paymentResponse.HostResponse;
                    paymentResponseBogusAccountNum = paymentResponse.BogusAccountNum;
                    paymentResponseCardType = paymentResponse.CardType;
                    paymentResponseMessage = paymentResponse.Message;
                    paymentResponseExtData = paymentResponse.ExtData;
                    paymentResponseAuthCode = paymentResponse.AuthCode;
                    paymentResponseExtData += "<SigFileName>" + paymentResponse.SigFileName + "</SigFileName>";

                    commResp = new CommResponse() { Code = ResultCode.SUCC_PAYMENT, Msg = "Payment completed.", Data = string.Empty };

                }
            }
            catch (InterruptProcessException ex)
            {
                errorCode = 5;
                errorMessage = ex.Message;
                commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = ex.Message, Data = string.Empty };
            }
            catch (FailProcessException ex)
            {
                errorCode = 1;
                errorMessage = "Unknown error: pg.PaymentResponse is null.";
                commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Unknown error: pg.PaymentResponse is null.", Data = string.Empty };
            }
            catch (ActionTimeoutExcepiton ex)
            {
                errorCode = 2;
                errorMessage = "Action Timeout.";
                commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Action Timeout.", Data = string.Empty };
            }
            catch (NullResponseException ex)
            {
                errorCode = 3;
                errorMessage = "Error Processing Payment.";
                commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Error Processing Payment.", Data = string.Empty };
            }

            process2.Abort();

            ReportDataReceived(commResp.GetCode());
        }
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void ProcessTransRun()
        {

            errorCode = 0;
            transResultMsg = "";
            POSLink.CommSetting commSetting = new POSLink.CommSetting();

            commSetting.CommType = Setting_CommType;
            commSetting.TimeOut = Setting_TimeOut;
            commSetting.SerialPort = Setting_SerialPort;
            commSetting.DestIP = Setting_DestIP;
            commSetting.DestPort = Setting_DestPort;
            commSetting.BaudRate = Setting_BaudRate;
            //commSetting.saveFile();
            poslink.CommSetting = commSetting;

            // 1. First create a POSLink Payment Page

            POSLink.PaymentRequest paymentRequest = new POSLink.PaymentRequest();

            paymentRequest.TenderType = paymentRequest.ParseTenderType(paymentRequestTenderType);
            paymentRequest.TransType = paymentRequest.ParseTransType(paymentRequestTransType);

            // 2. Next Set the PayLink Properties, the only required field is Amount
            #region Set PayLink Properties

            // Set the only required field: Amount
            string amount = paymentRequestAmount;
            // amount.to
            if (amount == "")
            {
                paymentRequest.Amount = "";

            }
            else
            {
                //amount = amount.Remove(amount.Length - 3, 1);
                double ret = Convert.ToDouble(amount);
                // ret = ret * 100;

                String retstr = Convert.ToString(ret);
                paymentRequest.Amount = retstr;
            }


            // All these below are optionals
            paymentRequest.OrigRefNum = paymentRequestOrigRefNum;
            paymentRequest.InvNum = paymentRequestInvNum;
            paymentRequest.UserID = paymentRequestUserID;
            paymentRequest.PassWord = paymentRequestPassWord;
            paymentRequest.ClerkID = paymentRequestClerkID;
            paymentRequest.ServerID = paymentRequestServerID;
            if (paymentRequestTipAmt.Length > 0) paymentRequest.TipAmt = paymentRequestTipAmt;
            if (paymentRequestTaxAmt.Length > 0) paymentRequest.TaxAmt = paymentRequestTaxAmt;
            if (paymentRequestCashbackAmt.Length > 0) paymentRequest.CashBackAmt = paymentRequestCashbackAmt;
            if (paymentRequestMisc1Amt.Length > 0) paymentRequest.Misc1Amt = paymentRequestMisc1Amt;
            if (paymentRequestMisc2Amt.Length > 0) paymentRequest.Misc2Amt = paymentRequestMisc2Amt;
            if (paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = paymentRequestMisc3Amt;
            if (paymentRequestECRefNum.Length > 0)
            {
                paymentRequest.ECRRefNum = paymentRequestECRefNum;
            }
            if (paymentRequestCustomFields.Length > 0 || paymentRequestCustomerName.Length > 0)
            {

            }
            if (paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = paymentRequestMisc3Amt;
            if (paymentRequestSurchargeAmt.Length > 0) paymentRequest.SurchargeAmt = paymentRequestSurchargeAmt;
            paymentRequest.PONum = paymentRequestPONum;
            paymentRequest.Street = paymentRequestStreet;
            paymentRequest.Zip = paymentRequestZIP;
            paymentRequest.CssPath = paymentRequestCssPath;
            paymentRequest.ExtData = paymentRequestExtData;
            paymentRequest.AuthCode = paymentRequestAuthCode;
            paymentRequest.SigSavePath = sigSavePath;
            #endregion

            //3 execute the function
            poslink.PaymentRequest = paymentRequest;

            result = poslink.ProcessTrans();

            if (isCancelTask)
                return;

            // 5. Show the result
            #region Show the PayLink Result
            transResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.PaymentResponse paymentResponse = poslink.PaymentResponse;
                if (paymentResponse != null && paymentResponse.ResultCode != null)
                {
                    paymentResponseResultCode = paymentResponse.ResultCode;
                    paymentResponseResultTxt = paymentResponse.ResultTxt;
                    paymentResponseRefNum = paymentResponse.RefNum;
                    paymentResponseRawResponse = paymentResponse.RawResponse;
                    paymentResponseAvsResponse = paymentResponse.AvsResponse;
                    paymentResponseCvResponse = paymentResponse.CvResponse;
                    paymentResponseTimestamp = paymentResponse.Timestamp;
                    paymentResponseHostCode = paymentResponse.HostCode;
                    paymentResponseRequestedAmt = paymentResponse.RequestedAmount;
                    paymentResponseApprovedAmt = paymentResponse.ApprovedAmount;
                    paymentResponseRemainingBalance = paymentResponse.RemainingBalance;
                    paymentResponseExtraBalance = paymentResponse.ExtraBalance;
                    paymentResponseHostResponse = paymentResponse.HostResponse;
                    paymentResponseBogusAccountNum = paymentResponse.BogusAccountNum;
                    paymentResponseCardType = paymentResponse.CardType;
                    paymentResponseMessage = paymentResponse.Message;
                    paymentResponseExtData = paymentResponse.ExtData;
                    paymentResponseAuthCode = paymentResponse.AuthCode;

                    //v1.14后将返回变量参数加入到 extdata当中
                    paymentResponseExtData += "<SigFileName>" + paymentResponse.SigFileName + "</SigFileName>";

                    //********** v1.16 增加参数MerchantId输出 **********/
                    //if (paymentResponse.MultiMerchant != null)
                    //{
                    //    paymentResponseExtData += string.Format("<{0}>{1}</{0}>", "MerchantId", paymentResponse.MultiMerchant.Id);
                    //    paymentResponseExtData += string.Format("<{0}>{1}</{0}>", "MerchantName", paymentResponse.MultiMerchant.Name); 
                    //}
                    /**************************************************/

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onPaymentCompleted )
                    //{
                    //    onPaymentCompleted();
                    //}

                    //if (paymentResponse.ResultCode.Equals("000000"))
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.SUCC_PAYMENT, Msg = "Payment completed.", Data = string.Empty }.GetCode());
                    //}
                    //else
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.ERR_PAYMENT, Msg = "Payment completed.", Data = string.Empty }.GetCode());
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.SUCC_PAYMENT, Msg = "Payment completed.", Data = string.Empty }.GetCode());
                    /**************************************************/
                }
                else
                {
                    // MessageBox.Show("Unknown error: pg.PaymentResponse is null.");
                    errorCode = 1;
                    errorMessage = "Unknown error: pg.PaymentResponse is null.";

                    //*************** v1.16 调整event返回结果 ***************/
                    //if (null != onError)
                    //{
                    //    onError();
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Unknown error: pg.PaymentResponse is null.", Data = string.Empty }.GetCode());
                    /**************************************************/

                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                //MessageBox.Show("Action Timeout.");
                errorCode = 2;
                errorMessage = "Action Timeout.";

                //*************** v1.16 调整event返回结果 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Action Timeout.", Data = string.Empty }.GetCode());
                /**************************************************/

            }
            else
            {
                //MessageBox.Show(result.Msg, "Error Processing Payment", MessageBoxButtons.OK);
                errorCode = 3;
                errorMessage = "Error Processing Payment.";

                //*************** v1.16 调整event返回结果 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Error Processing Payment.", Data = string.Empty }.GetCode());
                /**************************************************/
            }

            #endregion

            process2.Abort();
        }
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void SignatureRun()
        {
            errorCode = 0;
            transResultMsg = "";
            POSLink.CommSetting commSetting = new POSLink.CommSetting();

            commSetting.CommType = Setting_CommType;
            commSetting.TimeOut = Setting_TimeOut;
            commSetting.SerialPort = Setting_SerialPort;
            commSetting.DestIP = Setting_DestIP;
            commSetting.DestPort = Setting_DestPort;
            commSetting.BaudRate = Setting_BaudRate;
            //commSetting.saveFile();
            poslink.CommSetting = commSetting;

            POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            manageRequest.EDCType = manageRequest.ParseEDCType(manageRequestEDCType);
            manageRequest.TransType = manageRequest.ParseTransType(manageRequestTransType);

            // 2. Next Set the PayLink Properties, the only required field is Amount
            #region Set PayLink Properties

            // Set the required field: none

            // All these below are optionals
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);
            manageRequest.VarName = manageRequestVarName;
            manageRequest.VarValue = manageRequestVarValue;
            manageRequest.Title = manageRequestTitle;
            manageRequest.Button1 = manageRequestButton1;
            manageRequest.Button2 = manageRequestButton2;
            manageRequest.Button3 = manageRequestButton3;
            manageRequest.Button4 = manageRequestButton4;
            manageRequest.DisplayMessage = manageRequestDisplayMessage;
            manageRequest.ImagePath = manageRequestImagePath;
            manageRequest.ImageName = manageRequestImageName;
            if (String.IsNullOrEmpty(manageRequestUpload))
                manageRequest.Upload = Convert.ToInt32("0");
            else
                manageRequest.Upload = Convert.ToInt32(manageRequestUpload);
            //manageRequest.Upload = RT_TB_Upload.Text;
            manageRequest.HRefNum = manageRequestHRefNum;
            manageRequest.Timeout = manageRequestTimeout;
            manageRequest.ThankYouTitle = manageRequestThankYouTitle;
            manageRequest.ThankYouMessage1 = manageRequestThankYouMessage1;
            manageRequest.ThankYouMessage2 = manageRequestThankYouMessage2;
            manageRequest.ThankYouTimeout = manageRequestThankYouTimeout;
            manageRequest.AccountNumber = manageRequestAccountNumber;
            manageRequest.EncryptionType = "1";
            manageRequest.KeySlot = manageRequestKeySlot;
            manageRequest.PinMaxLength = manageRequestPinMaxLength;
            manageRequest.PinMinLength = manageRequestPinMinLength;
            manageRequest.NullPin = manageRequestNullPin;
            manageRequest.PinAlgorithm = manageRequestPinAlgorithm;
            manageRequest.MagneticSwipeEntryFlag = manageRequestMagneticSwipeEntryFlag;
            manageRequest.ManualEntryFlag = manageRequestManualEntryFlag;
            manageRequest.ContactlessEntryFlag = manageRequestContactlessEntryFlag;
            manageRequest.ScannerEntryFlag = manageRequestScannerEntryFlag;
            manageRequest.EncryptionFlag = manageRequestEncryptionFlag;
            manageRequest.MAXAccountLength = manageRequestMAXAccountLength;
            manageRequest.MINAccountLength = manageRequestMINAccountLength;
            manageRequest.ExpiryDatePrompt = manageRequestExpiryDatePrompt;
            manageRequest.InputType = manageRequestInputType;
            manageRequest.DefaultValue = manageRequestDefaultValue;
            manageRequest.MAXLength = manageRequestMAXLength;
            manageRequest.MINLength = manageRequestMINLength;
            manageRequest.FileName = manageRequestFileName;

            String path = manageRequestSigSavePath;
            if (path.EndsWith(":"))
            {
                path = path + "\\";
            }
            manageRequest.SigSavePath = path;

            #endregion

            //3 execute the function
            poslink.ManageRequest = manageRequest;

            //if (enableWrapperLog)
            //    Logger.info("GetSignature", "Start get signature");

            result = poslink.ProcessTrans();

            //logwrite("path: " + path);


            // 5. Show the result
            #region Show the PayLink Result
            transResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.ManageResponse manageResponse = poslink.ManageResponse;
                if (manageResponse != null && manageResponse.ResultCode != null)
                {
                    manageResponseResultCode = manageResponse.ResultCode;
                    manageResponseResultTxt = manageResponse.ResultTxt;
                    manageResponseSN = manageResponse.SN;
                    manageResponseVarValue = manageResponse.VarValue;
                    //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
                    manageResponseSigFileName = manageResponse.SigFileName;
                    manageResponsePinBlock = manageResponse.PinBlock;
                    manageResponseKSN = manageResponse.KSN;
                    manageResponseTrack1Data = manageResponse.Track1Data;
                    manageResponseTrack2Data = manageResponse.Track2Data;
                    manageResponseTrack3Data = manageResponse.Track3Data;
                    manageResponsePAN = manageResponse.PAN;
                    manageResponseQRCode = manageResponse.QRCode;
                    manageResponseEntryMode = manageResponse.EntryMode;
                    manageResponseExpiryDate = manageResponse.ExpiryDate;
                    manageResponseText = manageResponse.Text;

                    //if (enableWrapperLog)
                    //    Logger.info("GetSignature", "End get signature.");

                    string afterConvertSigFileName = manageRequest.SigSavePath + "\\" + sigPicName;
                    string devOutputSigFileName = manageRequest.SigSavePath + "\\" + manageResponse.SigFileName;

                    //if (enableWrapperLog)
                    //    Logger.info("GetSignature", "Start conversion.");

                    try
                    {
                        int k = manageRequest.ConvertSigToPic(devOutputSigFileName, "jpg", afterConvertSigFileName);

                        if (k == 0)
                        {
                            if (File.Exists(devOutputSigFileName))
                                File.Delete(devOutputSigFileName);

                            //if (enableWrapperLog)
                            //    Logger.info("GetSignature", "End conversion.");
                        }
                        else
                        {
                            errorCode = 7;
                            errorMessage = "error Convert Sig To Pic.";
                            //*************** v1.16 调整event返回 ***************/
                            //if (null != onError)
                            //{
                            //    onError();
                            //}
                            ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Error Convert Sig To Pic.", Data = string.Empty }.GetCode());
                            //*********************************************/
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        //if (enableWrapperLog)
                        //    Logger.info("GetSignature", "Conversion error.");

                        errorCode = 7;
                        errorMessage = ex.Message;
                        //*************** v1.16 调整event返回 ***************/
                        //if (null != onError)
                        //{
                        //    onError();
                        //}
                        ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Get signature conversion error.", Data = string.Empty }.GetCode());
                        //*********************************************/
                        return;
                    }

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onManageCompleted)
                    //{
                    //    onManageCompleted();
                    //}

                    //if (manageResponse.ResultCode.Equals("000000"))
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.SUCC_MANAGE, Msg = "Manage completed.", Data = string.Empty }.GetCode());
                    //}
                    //else {
                    //    DataReceived(new Response() { Code = ResultCode.ERR_MANAGE, Msg = "Manage completed.", Data = string.Empty }.GetCode());
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.SUCC_MANAGE, Msg = "Manage completed.", Data = string.Empty }.GetCode());
                    //*********************************************/

                }
                else
                {
                    //MessageBox.Show("Unknown error: mg.manageResponse is null.");
                    errorCode = 4;
                    errorMessage = "Unknown error: mg.manageResponse is null.";

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onError)
                    //{
                    //    onError();
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
                    //*********************************************/
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                //MessageBox.Show("Action Timeout.");
                errorCode = 5;
                errorMessage = "Action Timeout.";
                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Action Timeout.", Data = string.Empty }.GetCode());
                //*********************************************/
            }
            else
            {
                // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
                errorCode = 6;
                errorMessage = "Error Processing Manage";
                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Error Processing Manage", Data = string.Empty }.GetCode());
                //*********************************************/
            }

            #endregion

        }
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void DoSignatureRun()
        {
            errorCode = 0;
            transResultMsg = "";
            POSLink.CommSetting commSetting = new POSLink.CommSetting();

            commSetting.CommType = Setting_CommType;
            commSetting.TimeOut = Setting_TimeOut;
            commSetting.SerialPort = Setting_SerialPort;
            commSetting.DestIP = Setting_DestIP;
            commSetting.DestPort = Setting_DestPort;
            commSetting.BaudRate = Setting_BaudRate;
            //commSetting.saveFile();
            poslink.CommSetting = commSetting;

            POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            manageRequest.EDCType = manageRequest.ParseEDCType(manageRequestEDCType);
            manageRequest.TransType = manageRequest.ParseTransType(manageRequestTransType);

            // 2. Next Set the PayLink Properties, the only required field is Amount
            #region Set PayLink Properties

            // Set the required field: none

            // All these below are optionals
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);
            manageRequest.VarName = manageRequestVarName;
            manageRequest.VarValue = manageRequestVarValue;
            manageRequest.Title = manageRequestTitle;
            manageRequest.Button1 = manageRequestButton1;
            manageRequest.Button2 = manageRequestButton2;
            manageRequest.Button3 = manageRequestButton3;
            manageRequest.Button4 = manageRequestButton4;
            manageRequest.DisplayMessage = manageRequestDisplayMessage;
            manageRequest.ImagePath = manageRequestImagePath;
            manageRequest.ImageName = manageRequestImageName;
            if (String.IsNullOrEmpty(manageRequestUpload))
                manageRequest.Upload = Convert.ToInt32("0");
            else
                manageRequest.Upload = Convert.ToInt32(manageRequestUpload);
            //manageRequest.Upload = RT_TB_Upload.Text;
            manageRequest.HRefNum = manageRequestHRefNum;
            manageRequest.Timeout = manageRequestTimeout;
            manageRequest.ThankYouTitle = manageRequestThankYouTitle;
            manageRequest.ThankYouMessage1 = manageRequestThankYouMessage1;
            manageRequest.ThankYouMessage2 = manageRequestThankYouMessage2;
            manageRequest.ThankYouTimeout = manageRequestThankYouTimeout;
            manageRequest.AccountNumber = manageRequestAccountNumber;
            manageRequest.EncryptionType = "1";
            manageRequest.KeySlot = manageRequestKeySlot;
            manageRequest.PinMaxLength = manageRequestPinMaxLength;
            manageRequest.PinMinLength = manageRequestPinMinLength;
            manageRequest.NullPin = manageRequestNullPin;
            manageRequest.PinAlgorithm = manageRequestPinAlgorithm;
            manageRequest.MagneticSwipeEntryFlag = manageRequestMagneticSwipeEntryFlag;
            manageRequest.ManualEntryFlag = manageRequestManualEntryFlag;
            manageRequest.ContactlessEntryFlag = manageRequestContactlessEntryFlag;
            manageRequest.ScannerEntryFlag = manageRequestScannerEntryFlag;
            manageRequest.EncryptionFlag = manageRequestEncryptionFlag;
            manageRequest.MAXAccountLength = manageRequestMAXAccountLength;
            manageRequest.MINAccountLength = manageRequestMINAccountLength;
            manageRequest.ExpiryDatePrompt = manageRequestExpiryDatePrompt;
            manageRequest.InputType = manageRequestInputType;
            manageRequest.DefaultValue = manageRequestDefaultValue;
            manageRequest.MAXLength = manageRequestMAXLength;
            manageRequest.MINLength = manageRequestMINLength;
            manageRequest.FileName = manageRequestFileName;

            String path = manageRequestSigSavePath;
            if (path.EndsWith(":"))
            {
                path = path + "\\";
            }
            manageRequest.SigSavePath = path;

            #endregion

            //3 execute the function
            poslink.ManageRequest = manageRequest;

            result = poslink.ProcessTrans();

            // 5. Show the result
            #region Show the PayLink Result
            transResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.ManageResponse manageResponse = poslink.ManageResponse;
                if (manageResponse != null && manageResponse.ResultCode != null)
                {
                    manageResponseResultCode = manageResponse.ResultCode;
                    manageResponseResultTxt = manageResponse.ResultTxt;
                    manageResponseSN = manageResponse.SN;
                    manageResponseVarValue = manageResponse.VarValue;
                    //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
                    manageResponseSigFileName = manageResponse.SigFileName;
                    manageResponsePinBlock = manageResponse.PinBlock;
                    manageResponseKSN = manageResponse.KSN;
                    manageResponseTrack1Data = manageResponse.Track1Data;
                    manageResponseTrack2Data = manageResponse.Track2Data;
                    manageResponseTrack3Data = manageResponse.Track3Data;
                    manageResponsePAN = manageResponse.PAN;
                    manageResponseQRCode = manageResponse.QRCode;
                    manageResponseEntryMode = manageResponse.EntryMode;
                    manageResponseExpiryDate = manageResponse.ExpiryDate;
                    manageResponseText = manageResponse.Text;


                    errorCode = 0; //v1.19

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onDoSignatureCompleted)
                    //{
                    //    onDoSignatureCompleted();
                    //}

                    //if (manageResponse.ResultCode.Equals("000000"))
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.SUCC_DO_SIGNATURE, Msg = "Do signature completed.", Data = string.Empty }.GetCode());
                    //}
                    //else
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.ERR_DO_SIGNATURE, Msg = "Do signature completed.", Data = string.Empty }.GetCode());
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.SUCC_DO_SIGNATURE, Msg = "Do signature completed.", Data = string.Empty }.GetCode());
                    //*********************************************/

                }
                else
                {
                    //MessageBox.Show("Unknown error: mg.manageResponse is null.");
                    errorCode = 4;
                    errorMessage = "Unknown error: mg.manageResponse is null.";
                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onError)
                    //{
                    //    onError();
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_DO_SIGNATURE, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
                    //*********************************************/
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                //MessageBox.Show("Action Timeout.");
                errorCode = 5;
                errorMessage = "Action Timeout.";

                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_DO_SIGNATURE, Msg = "Action Timeout.", Data = string.Empty }.GetCode());
                //*********************************************/
            }
            else
            {
                // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
                errorCode = 6;
                errorMessage = "Error Processing Manage";

                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_DO_SIGNATURE, Msg = "Unknown error from PAX.", Data = string.Empty }.GetCode());
                //*********************************************/
            }

            #endregion
        }
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void RemoveCardRun()
        {
            errorCode = 0;
            transResultMsg = "";
            POSLink.CommSetting commSetting = new POSLink.CommSetting();

            commSetting.CommType = Setting_CommType;
            commSetting.TimeOut = Setting_TimeOut;
            commSetting.SerialPort = Setting_SerialPort;
            commSetting.DestIP = Setting_DestIP;
            commSetting.DestPort = Setting_DestPort;
            commSetting.BaudRate = Setting_BaudRate;
            //commSetting.saveFile();
            poslink.CommSetting = commSetting;

            POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            manageRequest.EDCType = manageRequest.ParseEDCType(manageRequestEDCType);
            manageRequest.TransType = manageRequest.ParseTransType(manageRequestTransType);

            // 2. Next Set the PayLink Properties, the only required field is Amount
            #region Set PayLink Properties

            // Set the required field: none

            // All these below are optionals
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);
            manageRequest.VarName = manageRequestVarName;
            manageRequest.VarValue = manageRequestVarValue;
            manageRequest.Title = manageRequestTitle;
            manageRequest.Button1 = manageRequestButton1;
            manageRequest.Button2 = manageRequestButton2;
            manageRequest.Button3 = manageRequestButton3;
            manageRequest.Button4 = manageRequestButton4;
            manageRequest.DisplayMessage = manageRequestDisplayMessage;
            manageRequest.ImagePath = manageRequestImagePath;
            manageRequest.ImageName = manageRequestImageName;
            if (String.IsNullOrEmpty(manageRequestUpload))
                manageRequest.Upload = Convert.ToInt32("0");
            else
                manageRequest.Upload = Convert.ToInt32(manageRequestUpload);
            //manageRequest.Upload = RT_TB_Upload.Text;
            manageRequest.HRefNum = manageRequestHRefNum;
            manageRequest.Timeout = manageRequestTimeout;
            manageRequest.ThankYouTitle = manageRequestThankYouTitle;
            manageRequest.ThankYouMessage1 = manageRequestThankYouMessage1;
            manageRequest.ThankYouMessage2 = manageRequestThankYouMessage2;
            manageRequest.ThankYouTimeout = manageRequestThankYouTimeout;
            manageRequest.AccountNumber = manageRequestAccountNumber;
            manageRequest.EncryptionType = "1";
            manageRequest.KeySlot = manageRequestKeySlot;
            manageRequest.PinMaxLength = manageRequestPinMaxLength;
            manageRequest.PinMinLength = manageRequestPinMinLength;
            manageRequest.NullPin = manageRequestNullPin;
            manageRequest.PinAlgorithm = manageRequestPinAlgorithm;
            manageRequest.MagneticSwipeEntryFlag = manageRequestMagneticSwipeEntryFlag;
            manageRequest.ManualEntryFlag = manageRequestManualEntryFlag;
            manageRequest.ContactlessEntryFlag = manageRequestContactlessEntryFlag;
            manageRequest.ScannerEntryFlag = manageRequestScannerEntryFlag;
            manageRequest.EncryptionFlag = manageRequestEncryptionFlag;
            manageRequest.MAXAccountLength = manageRequestMAXAccountLength;
            manageRequest.MINAccountLength = manageRequestMINAccountLength;
            manageRequest.ExpiryDatePrompt = manageRequestExpiryDatePrompt;
            manageRequest.InputType = manageRequestInputType;
            manageRequest.DefaultValue = manageRequestDefaultValue;
            manageRequest.MAXLength = manageRequestMAXLength;
            manageRequest.MINLength = manageRequestMINLength;
            manageRequest.FileName = manageRequestFileName;

            String path = manageRequestSigSavePath;
            if (path.EndsWith(":"))
            {
                path = path + "\\";
            }
            manageRequest.SigSavePath = path;

            #endregion

            //3 execute the function
            poslink.ManageRequest = manageRequest;

            result = poslink.ProcessTrans();

            // 5. Show the result
            #region Show the PayLink Result
            transResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.ManageResponse manageResponse = poslink.ManageResponse;
                if (manageResponse != null && manageResponse.ResultCode != null)
                {
                    manageResponseResultCode = manageResponse.ResultCode;
                    manageResponseResultTxt = manageResponse.ResultTxt;
                    manageResponseSN = manageResponse.SN;
                    manageResponseVarValue = manageResponse.VarValue;
                    //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
                    manageResponseSigFileName = manageResponse.SigFileName;
                    manageResponsePinBlock = manageResponse.PinBlock;
                    manageResponseKSN = manageResponse.KSN;
                    manageResponseTrack1Data = manageResponse.Track1Data;
                    manageResponseTrack2Data = manageResponse.Track2Data;
                    manageResponseTrack3Data = manageResponse.Track3Data;
                    manageResponsePAN = manageResponse.PAN;
                    manageResponseQRCode = manageResponse.QRCode;
                    manageResponseEntryMode = manageResponse.EntryMode;
                    manageResponseExpiryDate = manageResponse.ExpiryDate;
                    manageResponseText = manageResponse.Text;

                    manageResponseVarValue = manageResponse.CardInsertStatus; //v1.14 因为不想增加新的变量。所以直接把状态复制到varvalue

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onRemoveCardCompleted)
                    //{
                    //    onRemoveCardCompleted();
                    //}

                    //if (manageResponse.ResultCode.Equals("000000"))
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.SUCC_REMOVE_CARD, Msg = "Remove card completed.", Data = string.Empty }.GetCode());
                    //}
                    //else
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.REMOVE_CARD_ERR, Msg = "Remove card completed.", Data = string.Empty }.GetCode());
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.SUCC_REMOVE_CARD, Msg = "Remove card completed.", Data = string.Empty }.GetCode());
                    //*********************************************/

                }
                else
                {
                    //MessageBox.Show("Unknown error: mg.manageResponse is null.");
                    errorCode = 4;
                    errorMessage = "Unknown error: mg.manageResponse is null.";

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onError)
                    //{
                    //    onError();
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.REMOVE_CARD_ERR, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
                    //*********************************************/
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                //MessageBox.Show("Action Timeout.");
                errorCode = 5;
                errorMessage = "Action Timeout.";
                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.REMOVE_CARD_ERR, Msg = "Action Timeout..", Data = string.Empty }.GetCode());
                //*********************************************/
            }
            else
            {
                // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
                errorCode = 6;
                errorMessage = "Error Processing Manage.";
                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.REMOVE_CARD_ERR, Msg = "Error Processing Manage.", Data = string.Empty }.GetCode());
                //*********************************************/
            }

            #endregion
        }

        /**
         * v1.19 重写方法。用于捕获process trans存在异常的情况
         * 还在试验当中
         */
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void ReadCardRunV119()
        {
            errorCode = 0;
            transResultMsg = "";


            POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            manageRequest.EDCType = manageRequest.ParseEDCType(manageRequestEDCType);
            manageRequest.TransType = manageRequest.ParseTransType(manageRequestTransType);
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);

            // All these below are optionals
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);
            manageRequest.VarName = manageRequestVarName;
            manageRequest.VarValue = manageRequestVarValue;
            manageRequest.Title = manageRequestTitle;
            manageRequest.Button1 = manageRequestButton1;
            manageRequest.Button2 = manageRequestButton2;
            manageRequest.Button3 = manageRequestButton3;
            manageRequest.Button4 = manageRequestButton4;
            manageRequest.DisplayMessage = manageRequestDisplayMessage;
            manageRequest.ImagePath = manageRequestImagePath;
            manageRequest.ImageName = manageRequestImageName;
            if (String.IsNullOrEmpty(manageRequestUpload))
                manageRequest.Upload = Convert.ToInt32("0");
            else
                manageRequest.Upload = Convert.ToInt32(manageRequestUpload);
            //manageRequest.Upload = RT_TB_Upload.Text;
            manageRequest.HRefNum = manageRequestHRefNum;
            manageRequest.Timeout = manageRequestTimeout;
            manageRequest.ThankYouTitle = manageRequestThankYouTitle;
            manageRequest.ThankYouMessage1 = manageRequestThankYouMessage1;
            manageRequest.ThankYouMessage2 = manageRequestThankYouMessage2;
            manageRequest.ThankYouTimeout = manageRequestThankYouTimeout;
            manageRequest.AccountNumber = manageRequestAccountNumber;
            manageRequest.EncryptionType = "1";
            manageRequest.KeySlot = manageRequestKeySlot;
            manageRequest.PinMaxLength = manageRequestPinMaxLength;
            manageRequest.PinMinLength = manageRequestPinMinLength;
            manageRequest.NullPin = manageRequestNullPin;
            manageRequest.PinAlgorithm = manageRequestPinAlgorithm;
            manageRequest.MagneticSwipeEntryFlag = manageRequestMagneticSwipeEntryFlag;
            manageRequest.ManualEntryFlag = manageRequestManualEntryFlag;
            manageRequest.ContactlessEntryFlag = manageRequestContactlessEntryFlag;
            manageRequest.ScannerEntryFlag = manageRequestScannerEntryFlag;
            manageRequest.EncryptionFlag = manageRequestEncryptionFlag;
            manageRequest.MAXAccountLength = manageRequestMAXAccountLength;
            manageRequest.MINAccountLength = manageRequestMINAccountLength;
            manageRequest.ExpiryDatePrompt = manageRequestExpiryDatePrompt;
            manageRequest.InputType = manageRequestInputType;
            manageRequest.DefaultValue = manageRequestDefaultValue;
            manageRequest.MAXLength = manageRequestMAXLength;
            manageRequest.MINLength = manageRequestMINLength;
            manageRequest.FileName = manageRequestFileName;

            String path = manageRequestSigSavePath;
            if (path.EndsWith(":"))
            {
                path = path + "\\";
            }
            manageRequest.SigSavePath = path;

            CommResponse commResp = new CommResponse()
            {
                Code = ResultCode.ERR_READ_CARD,
                Msg = "Unknown error: mg.manageResponse is null.",
                Data = string.Empty
            };

            try
            {
                POSLink.ManageResponse manageResponse = ProcessManageRequestRun(manageRequest);
                if (manageResponse != null)
                {
                    manageResponseResultCode = manageResponse.ResultCode;
                    manageResponseResultTxt = manageResponse.ResultTxt;
                    manageResponseSN = manageResponse.SN;
                    manageResponseVarValue = manageResponse.VarValue;
                    manageResponseSigFileName = manageResponse.SigFileName;
                    manageResponsePinBlock = manageResponse.PinBlock;
                    manageResponseKSN = manageResponse.KSN;
                    manageResponseTrack1Data = manageResponse.Track1Data;
                    manageResponseTrack2Data = manageResponse.Track2Data;
                    manageResponseTrack3Data = manageResponse.Track3Data;
                    manageResponsePAN = manageResponse.PAN;
                    manageResponseQRCode = manageResponse.QRCode;
                    manageResponseEntryMode = manageResponse.EntryMode;
                    manageResponseExpiryDate = manageResponse.ExpiryDate;
                    manageResponseText = manageResponse.Text;


                    commResp = new CommResponse()
                    {
                        Code = ResultCode.SUCC_READ_CARD,
                        Msg = "Read card completed.",
                        Data = string.Empty
                    };
                }
            }
            catch (FailProcessException ex)
            {
                errorCode = 4;
                errorMessage = "Unknown error: mg.manageResponse is null.";
                commResp = new CommResponse()
                {
                    Code = ResultCode.ERR_READ_CARD,
                    Msg = "Unknown error: mg.manageResponse is null.",
                    Data = string.Empty
                };
            }
            catch (ActionTimeoutExcepiton ex)
            {
                errorCode = 5;
                errorMessage = "Action Timeout.";
                commResp = new CommResponse()
                {
                    Code = ResultCode.ERR_READ_CARD,
                    Msg = "Action Timeout.",
                    Data = string.Empty
                };
            }
            catch (NullResponseException ex)
            {
                errorCode = 6;
                errorMessage = "Error Processing Manage";
                commResp = new CommResponse()
                {
                    Code = ResultCode.ERR_READ_CARD,
                    Msg = "Error Processing Manage.",
                    Data = string.Empty
                };
            }

            ReportDataReceived(commResp.GetCode());

        }
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void ReadCardRun()
        {
            errorCode = 0;
            transResultMsg = "";
            POSLink.CommSetting commSetting = new POSLink.CommSetting();

            commSetting.CommType = Setting_CommType;
            commSetting.TimeOut = Setting_TimeOut;
            commSetting.SerialPort = Setting_SerialPort;
            commSetting.DestIP = Setting_DestIP;
            commSetting.DestPort = Setting_DestPort;
            commSetting.BaudRate = Setting_BaudRate;
            //commSetting.saveFile();
            poslink.CommSetting = commSetting;

            POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            manageRequest.EDCType = manageRequest.ParseEDCType(manageRequestEDCType);
            manageRequest.TransType = manageRequest.ParseTransType(manageRequestTransType);
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);

            // 2. Next Set the PayLink Properties, the only required field is Amount
            #region Set PayLink Properties

            // Set the required field: none

            // All these below are optionals
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);
            manageRequest.VarName = manageRequestVarName;
            manageRequest.VarValue = manageRequestVarValue;
            manageRequest.Title = manageRequestTitle;
            manageRequest.Button1 = manageRequestButton1;
            manageRequest.Button2 = manageRequestButton2;
            manageRequest.Button3 = manageRequestButton3;
            manageRequest.Button4 = manageRequestButton4;
            manageRequest.DisplayMessage = manageRequestDisplayMessage;
            manageRequest.ImagePath = manageRequestImagePath;
            manageRequest.ImageName = manageRequestImageName;
            if (String.IsNullOrEmpty(manageRequestUpload))
                manageRequest.Upload = Convert.ToInt32("0");
            else
                manageRequest.Upload = Convert.ToInt32(manageRequestUpload);
            //manageRequest.Upload = RT_TB_Upload.Text;
            manageRequest.HRefNum = manageRequestHRefNum;
            manageRequest.Timeout = manageRequestTimeout;
            manageRequest.ThankYouTitle = manageRequestThankYouTitle;
            manageRequest.ThankYouMessage1 = manageRequestThankYouMessage1;
            manageRequest.ThankYouMessage2 = manageRequestThankYouMessage2;
            manageRequest.ThankYouTimeout = manageRequestThankYouTimeout;
            manageRequest.AccountNumber = manageRequestAccountNumber;
            manageRequest.EncryptionType = "1";
            manageRequest.KeySlot = manageRequestKeySlot;
            manageRequest.PinMaxLength = manageRequestPinMaxLength;
            manageRequest.PinMinLength = manageRequestPinMinLength;
            manageRequest.NullPin = manageRequestNullPin;
            manageRequest.PinAlgorithm = manageRequestPinAlgorithm;
            manageRequest.MagneticSwipeEntryFlag = manageRequestMagneticSwipeEntryFlag;
            manageRequest.ManualEntryFlag = manageRequestManualEntryFlag;
            manageRequest.ContactlessEntryFlag = manageRequestContactlessEntryFlag;
            manageRequest.ScannerEntryFlag = manageRequestScannerEntryFlag;
            manageRequest.EncryptionFlag = manageRequestEncryptionFlag;
            manageRequest.MAXAccountLength = manageRequestMAXAccountLength;
            manageRequest.MINAccountLength = manageRequestMINAccountLength;
            manageRequest.ExpiryDatePrompt = manageRequestExpiryDatePrompt;
            manageRequest.InputType = manageRequestInputType;
            manageRequest.DefaultValue = manageRequestDefaultValue;
            manageRequest.MAXLength = manageRequestMAXLength;
            manageRequest.MINLength = manageRequestMINLength;
            manageRequest.FileName = manageRequestFileName;

            String path = manageRequestSigSavePath;
            if (path.EndsWith(":"))
            {
                path = path + "\\";
            }
            manageRequest.SigSavePath = path;

            #endregion

            //3 execute the function
            poslink.ManageRequest = manageRequest;

            result = poslink.ProcessTrans();

            // 5. Show the result
            #region Show the PayLink Result
            transResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.ManageResponse manageResponse = poslink.ManageResponse;
                if (manageResponse != null && manageResponse.ResultCode != null)
                {
                    manageResponseResultCode = manageResponse.ResultCode;
                    manageResponseResultTxt = manageResponse.ResultTxt;
                    manageResponseSN = manageResponse.SN;
                    manageResponseVarValue = manageResponse.VarValue;
                    //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
                    manageResponseSigFileName = manageResponse.SigFileName;
                    manageResponsePinBlock = manageResponse.PinBlock;
                    manageResponseKSN = manageResponse.KSN;
                    manageResponseTrack1Data = manageResponse.Track1Data;
                    manageResponseTrack2Data = manageResponse.Track2Data;
                    manageResponseTrack3Data = manageResponse.Track3Data;
                    manageResponsePAN = manageResponse.PAN;
                    manageResponseQRCode = manageResponse.QRCode;
                    manageResponseEntryMode = manageResponse.EntryMode;
                    manageResponseExpiryDate = manageResponse.ExpiryDate;
                    manageResponseText = manageResponse.Text;

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onReadCardCompleted)
                    //{
                    //    onReadCardCompleted();
                    //}

                    //if (manageResponse.ResultCode.Equals("000000"))
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.SUCC_READ_CARD, Msg = "Read card completed.", Data = string.Empty }.GetCode());
                    //}
                    //else
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.ERR_READ_CARD, Msg = "Read card completed.", Data = string.Empty }.GetCode());
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.SUCC_READ_CARD, Msg = "Read card completed.", Data = string.Empty }.GetCode());
                    //*********************************************/

                }
                else
                {
                    //MessageBox.Show("Unknown error: mg.manageResponse is null.");
                    errorCode = 4;
                    errorMessage = "Unknown error: mg.manageResponse is null.";
                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onError)
                    //{
                    //    onError();
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_READ_CARD, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
                    //*********************************************/
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                //MessageBox.Show("Action Timeout.");
                errorCode = 5;
                errorMessage = "Action Timeout.";
                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_READ_CARD, Msg = "Action Timeout.", Data = string.Empty }.GetCode());
                //*********************************************/
            }
            else
            {
                // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
                errorCode = 6;
                errorMessage = "Error Processing Manage";

                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_READ_CARD, Msg = "Error Processing Manage.", Data = string.Empty }.GetCode());
                //*********************************************/
            }

            #endregion
        }

        // v1.10 added
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void ResetRun()
        {
            errorCode = 0;
            transResultMsg = "";
            POSLink.CommSetting commSetting = new POSLink.CommSetting();

            commSetting.CommType = Setting_CommType;
            commSetting.TimeOut = Setting_TimeOut;
            commSetting.SerialPort = Setting_SerialPort;
            commSetting.DestIP = Setting_DestIP;
            commSetting.DestPort = Setting_DestPort;
            commSetting.BaudRate = Setting_BaudRate;
            //commSetting.saveFile();
            poslink.CommSetting = commSetting;

            POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            manageRequest.EDCType = manageRequest.ParseEDCType(manageRequestEDCType);
            manageRequest.TransType = manageRequest.ParseTransType(manageRequestTransType);

            // 2. Next Set the PayLink Properties, the only required field is Amount
            #region Set PayLink Properties

            // Set the required field: none

            // All these below are optionals
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);
            manageRequest.VarName = manageRequestVarName;
            manageRequest.VarValue = manageRequestVarValue;
            manageRequest.Title = manageRequestTitle;
            manageRequest.Button1 = manageRequestButton1;
            manageRequest.Button2 = manageRequestButton2;
            manageRequest.Button3 = manageRequestButton3;
            manageRequest.Button4 = manageRequestButton4;
            manageRequest.DisplayMessage = manageRequestDisplayMessage;
            manageRequest.ImagePath = manageRequestImagePath;
            manageRequest.ImageName = manageRequestImageName;
            if (String.IsNullOrEmpty(manageRequestUpload))
                manageRequest.Upload = Convert.ToInt32("0");
            else
                manageRequest.Upload = Convert.ToInt32(manageRequestUpload);
            //manageRequest.Upload = RT_TB_Upload.Text;
            manageRequest.HRefNum = manageRequestHRefNum;
            manageRequest.Timeout = manageRequestTimeout;
            manageRequest.ThankYouTitle = manageRequestThankYouTitle;
            manageRequest.ThankYouMessage1 = manageRequestThankYouMessage1;
            manageRequest.ThankYouMessage2 = manageRequestThankYouMessage2;
            manageRequest.ThankYouTimeout = manageRequestThankYouTimeout;
            manageRequest.AccountNumber = manageRequestAccountNumber;
            manageRequest.EncryptionType = "1";
            manageRequest.KeySlot = manageRequestKeySlot;
            manageRequest.PinMaxLength = manageRequestPinMaxLength;
            manageRequest.PinMinLength = manageRequestPinMinLength;
            manageRequest.NullPin = manageRequestNullPin;
            manageRequest.PinAlgorithm = manageRequestPinAlgorithm;
            manageRequest.MagneticSwipeEntryFlag = manageRequestMagneticSwipeEntryFlag;
            manageRequest.ManualEntryFlag = manageRequestManualEntryFlag;
            manageRequest.ContactlessEntryFlag = manageRequestContactlessEntryFlag;
            manageRequest.ScannerEntryFlag = manageRequestScannerEntryFlag;
            manageRequest.EncryptionFlag = manageRequestEncryptionFlag;
            manageRequest.MAXAccountLength = manageRequestMAXAccountLength;
            manageRequest.MINAccountLength = manageRequestMINAccountLength;
            manageRequest.ExpiryDatePrompt = manageRequestExpiryDatePrompt;
            manageRequest.InputType = manageRequestInputType;
            manageRequest.DefaultValue = manageRequestDefaultValue;
            manageRequest.MAXLength = manageRequestMAXLength;
            manageRequest.MINLength = manageRequestMINLength;
            manageRequest.FileName = manageRequestFileName;

            String path = manageRequestSigSavePath;
            if (path.EndsWith(":"))
            {
                path = path + "\\";
            }
            manageRequest.SigSavePath = path;

            #endregion

            //3 execute the function
            poslink.ManageRequest = manageRequest;

            result = poslink.ProcessTrans();

            // 5. Show the result
            #region Show the PayLink Result
            transResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.ManageResponse manageResponse = poslink.ManageResponse;
                if (manageResponse != null && manageResponse.ResultCode != null)
                {
                    manageResponseResultCode = manageResponse.ResultCode;
                    manageResponseResultTxt = manageResponse.ResultTxt;
                    manageResponseSN = manageResponse.SN;
                    manageResponseVarValue = manageResponse.VarValue;
                    //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
                    manageResponseSigFileName = manageResponse.SigFileName;
                    manageResponsePinBlock = manageResponse.PinBlock;
                    manageResponseKSN = manageResponse.KSN;
                    manageResponseTrack1Data = manageResponse.Track1Data;
                    manageResponseTrack2Data = manageResponse.Track2Data;
                    manageResponseTrack3Data = manageResponse.Track3Data;
                    manageResponsePAN = manageResponse.PAN;
                    manageResponseQRCode = manageResponse.QRCode;
                    manageResponseEntryMode = manageResponse.EntryMode;
                    manageResponseExpiryDate = manageResponse.ExpiryDate;
                    manageResponseText = manageResponse.Text;

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onResetCompleted)
                    //{
                    //    onResetCompleted();
                    //}

                    //if (manageResponse.ResultCode.Equals("000000"))
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.SUCC_RESET, Msg = "Reset completed.", Data = string.Empty }.GetCode());
                    //}
                    //else
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.ERR_RESET, Msg = "Reset completed.", Data = string.Empty }.GetCode());
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.SUCC_RESET, Msg = "Reset completed.", Data = string.Empty }.GetCode());
                    //*********************************************/

                }
                else
                {
                    //MessageBox.Show("Unknown error: mg.manageResponse is null.");
                    errorCode = 4;
                    errorMessage = "Unknown error: mg.manageResponse is null.";
                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onError)
                    //{
                    //    onError();
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_RESET, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
                    //*********************************************/
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                //MessageBox.Show("Action Timeout.");
                errorCode = 5;
                errorMessage = "Action Timeout.";
                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_RESET, Msg = "Action Timeout.", Data = string.Empty }.GetCode());
                //*********************************************/
            }
            else
            {
                // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
                errorCode = 6;
                errorMessage = "Error Processing Manage";
                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_RESET, Msg = "Error Processing Manage.", Data = string.Empty }.GetCode());
                //*********************************************/
            }

            #endregion
        }

        //v1.11 added
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        void RequestInputTextRun()
        {
            errorCode = 0;
            transResultMsg = "";
            POSLink.CommSetting commSetting = new POSLink.CommSetting();

            commSetting.CommType = Setting_CommType;
            commSetting.TimeOut = Setting_TimeOut;
            commSetting.SerialPort = Setting_SerialPort;
            commSetting.DestIP = Setting_DestIP;
            commSetting.DestPort = Setting_DestPort;
            commSetting.BaudRate = Setting_BaudRate;
            //commSetting.saveFile();
            poslink.CommSetting = commSetting;

            POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            manageRequest.EDCType = manageRequest.ParseEDCType("ALL");// manageRequest.ParseEDCType(manageRequestEDCType);
            manageRequest.TransType = manageRequest.ParseTransType(manageRequestTransType);

            // 2. Next Set the PayLink Properties, the only required field is Amount
            #region Set PayLink Properties

            // Set the required field: none

            // All these below are optionals
            manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);
            manageRequest.VarName = manageRequestVarName;
            manageRequest.VarValue = manageRequestVarValue;
            manageRequest.Title = manageRequestTitle;
            manageRequest.Button1 = manageRequestButton1;
            manageRequest.Button2 = manageRequestButton2;
            manageRequest.Button3 = manageRequestButton3;
            manageRequest.Button4 = manageRequestButton4;
            manageRequest.DisplayMessage = manageRequestDisplayMessage;
            manageRequest.ImagePath = manageRequestImagePath;
            manageRequest.ImageName = manageRequestImageName;
            if (String.IsNullOrEmpty(manageRequestUpload))
                manageRequest.Upload = Convert.ToInt32("0");
            else
                manageRequest.Upload = Convert.ToInt32(manageRequestUpload);
            //manageRequest.Upload = RT_TB_Upload.Text;
            manageRequest.HRefNum = manageRequestHRefNum;
            manageRequest.Timeout = manageRequestTimeout;
            manageRequest.ThankYouTitle = manageRequestThankYouTitle;
            manageRequest.ThankYouMessage1 = manageRequestThankYouMessage1;
            manageRequest.ThankYouMessage2 = manageRequestThankYouMessage2;
            manageRequest.ThankYouTimeout = manageRequestThankYouTimeout;
            manageRequest.AccountNumber = manageRequestAccountNumber;
            manageRequest.EncryptionType = "1";
            manageRequest.KeySlot = manageRequestKeySlot;
            manageRequest.PinMaxLength = manageRequestPinMaxLength;
            manageRequest.PinMinLength = manageRequestPinMinLength;
            manageRequest.NullPin = manageRequestNullPin;
            manageRequest.PinAlgorithm = manageRequestPinAlgorithm;
            manageRequest.MagneticSwipeEntryFlag = manageRequestMagneticSwipeEntryFlag;
            manageRequest.ManualEntryFlag = manageRequestManualEntryFlag;
            manageRequest.ContactlessEntryFlag = manageRequestContactlessEntryFlag;
            manageRequest.ScannerEntryFlag = manageRequestScannerEntryFlag;
            manageRequest.EncryptionFlag = manageRequestEncryptionFlag;
            manageRequest.MAXAccountLength = manageRequestMAXAccountLength;
            manageRequest.MINAccountLength = manageRequestMINAccountLength;
            manageRequest.ExpiryDatePrompt = manageRequestExpiryDatePrompt;
            manageRequest.InputType = manageRequestInputType;
            manageRequest.DefaultValue = manageRequestDefaultValue;
            manageRequest.MAXLength = manageRequestMAXLength;
            manageRequest.MINLength = manageRequestMINLength;
            manageRequest.FileName = manageRequestFileName;

            String path = manageRequestSigSavePath;
            if (path.EndsWith(":"))
            {
                path = path + "\\";
            }
            manageRequest.SigSavePath = path;

            #endregion

            //3 execute the function
            poslink.ManageRequest = manageRequest;

            result = poslink.ProcessTrans();

            if (isCancelTask)
                return;

            // 5. Show the result
            #region Show the PayLink Result
            transResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.ManageResponse manageResponse = poslink.ManageResponse;
                if (manageResponse != null && manageResponse.ResultCode != null)
                {
                    manageResponseResultCode = manageResponse.ResultCode;
                    manageResponseResultTxt = manageResponse.ResultTxt;
                    manageResponseSN = manageResponse.SN;
                    manageResponseVarValue = manageResponse.VarValue;
                    //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
                    manageResponseSigFileName = manageResponse.SigFileName;
                    manageResponsePinBlock = manageResponse.PinBlock;
                    manageResponseKSN = manageResponse.KSN;
                    manageResponseTrack1Data = manageResponse.Track1Data;
                    manageResponseTrack2Data = manageResponse.Track2Data;
                    manageResponseTrack3Data = manageResponse.Track3Data;
                    manageResponsePAN = manageResponse.PAN;
                    manageResponseQRCode = manageResponse.QRCode;
                    manageResponseEntryMode = manageResponse.EntryMode;
                    manageResponseExpiryDate = manageResponse.ExpiryDate;
                    manageResponseText = manageResponse.Text;

                    //*************** v1.16 调整event返回 ***************/
                    //if (null != onRequestInputTextCompleted)
                    //{
                    //    //onRequestInputTextCompleted(manageResponseText);
                    //    respInputText = manageResponseText;
                    //    onRequestInputTextCompleted();

                    //}

                    //respInputText = manageResponseText; //v1.16 remove
                    //if (manageResponse.ResultCode.Equals("000000"))
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.SUCC_INPUT_TEXT, Msg = "Request input text completed.", Data = string.Empty }.GetCode());
                    //}
                    //else
                    //{
                    //    DataReceived(new Response() { Code = ResultCode.ERR_INPUT_TEXT, Msg = "Request input text completed.", Data = string.Empty }.GetCode());
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.SUCC_INPUT_TEXT, Msg = "Request input text completed.", Data = string.Empty }.GetCode());
                    //*********************************************/

                }
                else
                {
                    //MessageBox.Show("Unknown error: mg.manageResponse is null.");
                    errorCode = 4;
                    errorMessage = "Unknown error: mg.manageResponse is null.";
                    //************ v1.16 调整event返回 ************/
                    //if (null != onError)
                    //{
                    //    onError();
                    //}
                    ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_INPUT_TEXT, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
                    //*********************************************/
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                //MessageBox.Show("Action Timeout.");
                errorCode = 5;
                errorMessage = "Action Timeout.";

                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_INPUT_TEXT, Msg = "Action Timeout.", Data = string.Empty }.GetCode());
                //*********************************************/
            }
            else
            {
                // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
                errorCode = 6;
                errorMessage = "Error Processing Manage";
                //*************** v1.16 调整event返回 ***************/
                //if (null != onError)
                //{
                //    onError();
                //}
                ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_INPUT_TEXT, Msg = "Unknown error from PAX.", Data = string.Empty }.GetCode());
                //*********************************************/
            }

            #endregion
        }
        [Obsolete("Move PosLinkEngine class. This method will be removed in v1.19.")]
        public void StatusChangedRun()
        {
            int a = 9;
            string msg = "";

            while (true)
            {
                a = poslink.GetReportedStatus();

                if (a == -1)
                {
                    msg = "No status returned.";
                }
                else if (a == 0)
                {
                    msg = "Ready for swipe card/input account.";
                }
                else if (a == 1)
                {
                    msg = "Ready for PIN entry.";
                }
                else if (a == 2)
                {
                    msg = "Ready for Signature.";
                }
                else if (a == 3)
                {
                    msg = "Ready for Online Processing.";
                }
                else if (a == 4)
                {
                    msg = "Ready for second card input.";
                }

                if ((a >= 0 && a <= 4) && null != onStatusChanged)
                {
                    onStatusChanged(a, msg); 
                }

                //Thread.Sleep(500);
            }
        }

        private POSLink.ManageResponse ProcessManageRequestRun(POSLink.ManageRequest manageRequest)
        {
            InitPoslinkConfig();
            poslink.ManageRequest = manageRequest;
            try
            {
                result = poslink.ProcessTrans();
            }
            catch (Exception ex)
            {
                throw new FailProcessException("An exception occurred after process trans.");
            }

            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.ManageResponse manageResponse = poslink.ManageResponse;
                if (manageResponse != null && manageResponse.ResultCode != null)
                {
                    return manageResponse;
                }
                else
                {
                    throw new NullResponseException("Incorrect manage response. The \"ManageResponse\" is null.");
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                throw new ActionTimeoutExcepiton("Manage request timeout.");
            }
            else
            {
                throw new FailProcessException("No result is retruned after processing transaction on manage request.");
            }
        }

        private POSLink.PaymentResponse ProcessPaymentRequestRun(POSLink.PaymentRequest paymentRequest)
        {
            InitPoslinkConfig();
            poslink.PaymentRequest = paymentRequest;

            try
            {
                result = poslink.ProcessTrans();
            }
            catch (Exception ex)
            {
                throw new FailProcessException("An exception occurred after proces trans.");
            }

            if (isCancelTask)
            {
                throw new InterruptProcessException("Cancelled transaction.");
            }

            transResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.PaymentResponse paymentResponse = poslink.PaymentResponse;
                if (paymentResponse != null && paymentResponse.ResultCode != null)
                {
                    return paymentResponse;
                }
                else
                {
                    throw new NullResponseException("Incorrect payment response. The \"PaymentResponse\" is null.");
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                throw new ActionTimeoutExcepiton("Payment request timeout.");
            }
            else
            {
                throw new FailProcessException("No result is retruned after processing transaction on payment request.");
            }
        }

        //****************   v1.16 注释 **********************//
        //void Run1()
        //{
        //    // myDelegate1 md1 = new myDelegate1(threadTrans);

        //    /* POSLink.LogManagement log = new POSLink.LogManagement();
        //     POSLink.CommSetting com = new POSLink.CommSetting();

        //     log.LogFilePath = "D:\\New folder (2)";
        //     log.LogSwitchMode = true;
        //     log.LogLevel = 1;

        //     com.CommType = "TCP";
        //     com.DestIP = "127.0.0.1";
        //     com.DestPort = "10009";
        //     com.TimeOut = "30000";

        //     pg.CommSetting = com;
        //     pg.LogManageMent = log;
        //     */
        //    result = pg.ProcessTrans();

        //    //this.Invoke(md1);

        //    // 5. Show the result
        //    #region Show the PayLink Result
        //    if (result.Code == POSLink.ProcessTransResultCode.OK)
        //    {
        //        POSLink.PaymentResponse paymentResponse = pg.PaymentResponse;
        //        if (paymentResponse != null && paymentResponse.ResultCode != null)
        //        {
        //            paymentResponseResultCode = paymentResponse.ResultCode;
        //            paymentResponseResultTxt = paymentResponse.ResultTxt;
        //            paymentResponseRefNum = paymentResponse.RefNum;
        //            paymentResponseRawResponse = paymentResponse.RawResponse;
        //            paymentResponseAvsResponse = paymentResponse.AvsResponse;
        //            paymentResponseCvResponse = paymentResponse.CvResponse;
        //            paymentResponseTimestamp = paymentResponse.Timestamp;
        //            paymentResponseHostCode = paymentResponse.HostCode;
        //            paymentResponseRequestedAmt = paymentResponse.RequestedAmount;
        //            paymentResponseApprovedAmt = paymentResponse.ApprovedAmount;
        //            paymentResponseRemainingBalance = paymentResponse.RemainingBalance;
        //            paymentResponseExtraBalance = paymentResponse.ExtraBalance;
        //            paymentResponseHostResponse = paymentResponse.HostResponse;
        //            paymentResponseBogusAccountNum = paymentResponse.BogusAccountNum;
        //            paymentResponseCardType = paymentResponse.CardType;
        //            paymentResponseMessage = paymentResponse.Message;
        //            paymentResponseExtData = paymentResponse.ExtData;
        //            paymentResponseAuthCode = paymentResponse.AuthCode;

        //            if (null != onProcessTransComplete)
        //            {
        //                onProcessTransComplete();
        //            }
        //        }
        //        else
        //        {
        //            // MessageBox.Show("Unknown error: pg.PaymentResponse is null.");
        //            if (null != onProcessError)
        //            {
        //                onProcessError("Unknown error: pg.PaymentResponse is null.");
        //            }

        //        }
        //    }
        //    else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
        //    {
        //        //MessageBox.Show("Action Timeout.");
        //        if (null != onActionTimeOut)
        //        {
        //            onActionTimeOut();
        //        }


        //    }
        //    else
        //    {
        //        //MessageBox.Show(result.Msg, "Error Processing Payment", MessageBoxButtons.OK);
        //        if (null != onProcessError)
        //        {
        //            onProcessError("Error Processing Payment");
        //        }
        //    }

        //    #endregion

        //    process2.Abort();
        //}


        //void Run2()
        //{
        //    int a = 9;

        //    while (true)
        //    {
        //        a = pg.GetReportedStatus();
        //        if (null != onReturnReportedStatus)
        //        {
        //            onReturnReportedStatus(a);
        //        }

        //        Thread.Sleep(500);

        //        /*
        //        if (a == 0)
        //        {
        //            //MessageBox.Show("Ready for swipe card/input account.");
        //            onReadyForSwipeCardInput();


        //        }
        //        else if (a == 1)
        //        {
        //            //MessageBox.Show("Ready for PIN entry.");
        //            onReadyForPinEntry();
        //        }
        //        else if (a == 2)
        //        {
        //            //MessageBox.Show("Ready for Signature.");
        //            onReadyForSignature();
        //        }
        //        else if (a == 3)
        //        {
        //            //MessageBox.Show("Ready for Online Processing.");
        //            onReadyForOnlineProcessing();
        //        }
        //        else if (a == 4)
        //        {
        //            //MessageBox.Show("Ready for second card input.");
        //            onReadyForSecondCardInput();
        //        }
        //        */
        //    }
        //    //    this.Invoke(md2);
        //}
        //*************************************************//

        //void ManageRun1()
        //{

        //    result = pg.ProcessTrans();

        //    logwrite("ManageRun1 show the result ");

        //    // 5. Show the result
        //    #region Show the PayLink Result

        //    if (result.Code == POSLink.ProcessTransResultCode.OK)
        //    {
        //        POSLink.ManageResponse manageResponse = pg.ManageResponse;
        //        if (manageResponse != null && manageResponse.ResultCode != null)
        //        {
        //            manageResponseResultCode = manageResponse.ResultCode;
        //            manageResponseResultTxt = manageResponse.ResultTxt;
        //            manageResponseSN = manageResponse.SN;
        //            manageResponseVarValue = manageResponse.VarValue;
        //            //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
        //            manageResponseSigFileName = manageResponse.SigFileName;
        //            manageResponsePinBlock = manageResponse.PinBlock;
        //            manageResponseKSN = manageResponse.KSN;
        //            manageResponseTrack1Data = manageResponse.Track1Data;
        //            manageResponseTrack2Data = manageResponse.Track2Data;
        //            manageResponseTrack3Data = manageResponse.Track3Data;
        //            manageResponsePAN = manageResponse.PAN;
        //            manageResponseQRCode = manageResponse.QRCode;
        //            manageResponseEntryMode = manageResponse.EntryMode;
        //            manageResponseExpiryDate = manageResponse.ExpiryDate;
        //            manageResponseText = manageResponse.Text;

        //            if (null != onProcessManageComplete)
        //            {
        //                onProcessManageComplete();
        //            }

        //            logwrite(" manageResponse.ResultCode: " + manageResponseResultCode);

        //            switch (manageRequestTransType)
        //            {
        //                case "GETSIGNATURE":
        //                    /* method 1 */
        //                    //saveSigImage(System.Environment.CurrentDirectory +"\\"+ manageResponse.SigFileName);

        //                    /* method 2 */
        //                    POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();
        //                    string fileName = "Sig" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
        //                    string sigFileName = System.Environment.CurrentDirectory + "\\" + manageResponse.SigFileName;

        //                    int k = manageRequest.ConvertSigToPic(manageResponse.SigFileName, "jpg", fileName);
        //                    if (k == 0)
        //                    {
        //                        if (File.Exists(sigFileName))
        //                            File.Delete(sigFileName);

        //                        if (onSignatureReceived != null)
        //                        {
        //                            onSignatureReceived(System.Environment.CurrentDirectory + "\\" + fileName);
        //                        }
        //                    }

        //                    break;
        //                default:
        //                    break;
        //            }


        //        }
        //        else
        //        {
        //            //MessageBox.Show("Unknown error: mg.manageResponse is null.");
        //            if (null != onProcessError)
        //            {
        //                onProcessError("Unknown error: mg.manageResponse is null.");
        //            }
        //        }
        //    }
        //    else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
        //    {
        //        //MessageBox.Show("Action Timeout.");
        //        if (null != onActionTimeOut)
        //        {
        //            onActionTimeOut();
        //        }
        //    }
        //    else
        //    {
        //        // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
        //        if (null != onProcessError)
        //        {
        //            onProcessError("Error Processing Manage");
        //        }
        //    }

        //    #endregion


        //}

        void InitRun()
        {
            errorCode = 0;

            if (errorCode != 0)
                return;

            ManageRequestCommand_SETVAR("hostRspTimeout", "0");

            //Thread.Sleep(2000);

            if (errorCode != 0)
                return;

            ManageRequestCommand_SETVAR("swipeAnyTime", "N");
        }

        //public void processTrans(string TenderType, string TransType, string Amount, string OrigRefNum, string InvNum,
        //                            string UserID, string PassWord, string ClerkID, string ServerID, string TipAmt, string TaxAmt,
        //                            string CashbackAmt, string Misc1Amt, string Misc2Amt, string Misc3Amt, string ECRefNum,
        //    string CustomFields, string CustomerName, string SurchargeAmt, string PONum, string Street, string ZIP, string CssPath,
        //    string ExtData, string AuthCode)

        public void EnableLog(int k)
        {
            /************ v1.19 优化代码 **********/
            //POSLink.LogManagement log = new POSLink.LogManagement(); 
            //switch (k)
            //{
            //    case 0:
            //        log.LogSwitchMode = false;
            //        enableWrapperLog = false;
            //        break;
            //    case 1:
            //        log.LogSwitchMode = true;
            //        enableWrapperLog = true;
            //        break;
            //    default:
            //        break;
            //}
            //log.LogLevel = 1;
            //log.LogFilePath = "";
            //log.LogFileName = "";
            //log.saveFile();
            /**********************************/

            bool enabled = k == 1 ? true : false;
            SetEnableLocalLog(enabled);
            DearMing.Common.Utils.Log.SetEnable(enabled);
        }

        private void SetEnableLocalLog(bool enabled)
        {
            enableWrapperLog = enabled;

            //POSLink.LogManagement log = new POSLink.LogManagement();
            //log.LogSwitchMode = enabled;
            //log.LogLevel = 1;
            //log.LogFilePath = "";
            //log.LogFileName = "";
            //log.saveFile();

            PosLinkEngine.GetInstance().SetEnableLog(enabled);
        }

        public void CancelTrans()
        {
            //isCancelTask = true;
            //if (poslink != null)
            //    poslink.CancelTrans();

            PosLinkEngine.GetInstance().CancelTrans();
        }

        /**
         * PNG图片转JPG图片
         */
        public int ConvertPNG2JPG(string pngPath, int deletePNG)
        {
            //Logger.info("ConvertPNG2JPG", "pngPath: " + pngPath + " delete: " + deletePNG);
            if (File.Exists(pngPath))
            {
                //Logger.info("ConvertPNG2JPG", "png file is exist.");
                string extension = System.IO.Path.GetExtension(pngPath);
                if (extension.ToLower() == ".png")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(pngPath);
                    var ret = pngPath.Substring(0, pngPath.LastIndexOf(".")) + ".jpg";
                    if (File.Exists(ret)) File.Delete(ret);

                    try
                    {
                        //Logger.error("ConvertPNG2JPG", "start convert -> " + ret);
                        img.Save(ret, System.Drawing.Imaging.ImageFormat.Jpeg);
                        img.Dispose();
                        img = null;
                    }
                    catch (Exception ex)
                    {
                        DearMing.Common.Utils.Log.e("ConvertPNG2JPG", ex.ToString());

                        return FAILED;
                    }

                    if (deletePNG == 1)
                    {
                        File.Delete(pngPath);
                    }
                }
            }
            else
            {
                DearMing.Common.Utils.Log.i("ConvertPNG2JPG", "PNG file not found. (" + pngPath + ")");
                return FAILED;
            }

            return SUCCESS;
        }

        private const int SUCCESS = 0;
        private const int FAILED = -1;

        public int IsOnline()
        {
            //ResetAllProperties();

            errorCode = 0;

            ManageRequestCommand_SETVAR("hostRspTimeout", "0");

            if (errorCode != 0)
                return 0;

            return 1;
        }

        public int Init(string CommType, int timeout, string DestIP, string DestPort)
        {
            //ResetAllProperties();

            //v1.00 只支持TCP连接方式
            //Setting_CommType = CommType;
            //Setting_TimeOut = Convert.ToString(timeout * 1000);
            //Setting_DestIP = DestIP;
            //Setting_DestPort = DestPort;

            //Setting_BaudRate = "";
            //Setting_SerialPort = "";

            //v1.06 修改根据commtype设置连接方式
            switch (CommType.ToUpper())
            {
                case "UART":
                    Setting_CommType = CommType;
                    Setting_TimeOut = Convert.ToString(timeout * 1000);
                    Setting_DestIP = "";
                    Setting_DestPort = "";

                    Setting_BaudRate = "9600";
                    Setting_SerialPort = DestIP;// 例如变量中参数 COM1,COM2,COM3
                    break;
                case "TCP":
                    Setting_CommType = CommType;
                    Setting_TimeOut = Convert.ToString(timeout * 1000);
                    Setting_DestIP = DestIP;
                    Setting_DestPort = DestPort;

                    Setting_BaudRate = "";
                    Setting_SerialPort = "";
                    break;


                default:
                    break;
            }

            errorCode = 0;

            /***
             * hostRspTimeout
             * “Host Approval” or “Declined” Response “timeout in 100ms increments. A “0” setting disables screen.
             ***/
            /***** v1.15 fixed issue "card insert dectection" ****/
            //ManageRequestCommand_SETVAR("hostRspTimeout", "0"); 

            //if (errorCode != 0)
            //    return -1; 
            /********************************************/

            ManageRequestCommand_SETVAR("swipeAnyTime", "N");

            if (errorCode != 0)
                return -1;

            Thread.Sleep(1000);

            //********** v1.16 调用 ManageRequest Command INIT  **********/
            ManageRequestCommand_INIT();
            if (errorCode != 0)
                return -1;
            /**************************************************************/

            return 0;

        }

        //public void Init2(string CommType, string TimeOut, string DestIP, string DestPort)
        //{
        //    ResetAllProperties();

        //    Setting_CommType = CommType;
        //    Setting_TimeOut = TimeOut;
        //    Setting_DestIP = DestIP;
        //    Setting_DestPort = DestPort;

        //    Setting_BaudRate = "";
        //    Setting_SerialPort = "";

        //    ThreadStart entry1 = new ThreadStart(InitRun);
        //    Thread process1 = new Thread(entry1);
        //    process1.IsBackground = true;
        //    process1.Start();

        //}

        //v1.14 后 extdata中包含pb传入的其他参数和 pax的extdata,需要进行拆分。
        //v1.06 增加timeout入口参数
        public void ProcessTrans(string TenderType, string TransType, string Amount, string ECRefNum, string ExtData, string CashbackAmt, int timeout)
        {

            //ResetAllProperties();

            //if (timeout > 0)
            //{
            //    Setting_TimeOut = Convert.ToString(timeout * 1000);
            //}

            //paymentRequestTenderType = TenderType;
            //paymentRequestTransType = TransType;
            //paymentRequestAmount = Amount;
            //paymentRequestECRefNum = ECRefNum;
            //paymentRequestExtData = ExtData;
            //paymentRequestCashbackAmt = CashbackAmt;

            ////v1.14拆开extdata为传参变量和pax的extdata
            //paymentRequestExtData = StringUtils.GetPaxExtData(ExtData);
            //if (string.IsNullOrEmpty(paymentRequestExtData))
            //    paymentRequestExtData = ExtData;
            //sigSavePath = StringUtils.GetXmlTagValue(ExtData, "SigSavePath");


            //ThreadStart entry1 = new ThreadStart(ProcessTransRunV119);
            //Thread process1 = new Thread(entry1);
            //process1.IsBackground = true;
            //process1.Start();

            //ThreadStart entry2 = new ThreadStart(StatusChangedRun);
            //process2 = new Thread(entry2);
            //process2.IsBackground = true;
            //process2.Start();



            PosLinkEngine.GetInstance().SetContext(this);
            PosLinkEngine.GetInstance().ProcessTrans(TenderType, TransType, Amount, ECRefNum, ExtData, CashbackAmt, timeout);
        }

        //v1.06 timeout时间作为入口参数
        public void GetSignature(int upload, string sigSavePath, string picName, int timeout)
        {
            //ResetAllProperties();

            //manageRequestTransType = "GETSIGNATURE";
            //manageRequestEDCType = "ALL";
            //manageRequestUpload = Convert.ToString(upload);
            ////manageRequestTimeout = "900";

            //if (timeout <= 0)
            //{
            //    manageRequestTimeout = "900";
            //}
            //else
            //{
            //    manageRequestTimeout = Convert.ToString(timeout * 10);//timeout时间以 0.1s 为单位
            //}

            //manageRequestHRefNum = "";
            //manageRequestSigSavePath = sigSavePath;

            //sigPicName = picName;

            //ThreadStart entry1 = new ThreadStart(SignatureRun);
            //Thread process1 = new Thread(entry1);
            //process1.IsBackground = true;
            //process1.Start();


            PosLinkEngine.GetInstance().SetContext(this);
            PosLinkEngine.GetInstance().GetSignature(upload, sigSavePath, picName, timeout);

        }

        // v1.06 方法增加一个入口参数 time out
        // v1.02 新增签名方法
        public void DoSignature(int upload, int timeout)
        {
            //ResetAllProperties();

            //manageRequestTransType = "DOSIGNATURE";
            //manageRequestEDCType = "ALL";
            //manageRequestUpload = Convert.ToString(upload);
            //manageRequestHRefNum = "";
            ////manageRequestTimeout = "300"; // 签名timeout时间以 0.1s 为单位
            //if (timeout <= 0)
            //{
            //    manageRequestTimeout = "300"; // 0.1s
            //}
            //else
            //{
            //    manageRequestTimeout = Convert.ToString(timeout * 10);
            //}

            //ThreadStart entry1 = new ThreadStart(DoSignatureRun);
            //Thread process1 = new Thread(entry1);
            //process1.IsBackground = true;
            //process1.Start();
 
            PosLinkEngine.GetInstance().SetContext(this);
            PosLinkEngine.GetInstance().DoSignature(upload, timeout);


        }

        // v1.03 新增remove card方法
        // v1.06 增加itmeout 入口参数
        public void RemoveCard(int timeout)
        {
            //ResetAllProperties();

            //if (timeout > 0)
            //{
            //    Setting_TimeOut = Convert.ToString(timeout * 1000);
            //}

            ////v1.14 调整remove card方法 ,按照Kevin说法，这个方法名字不是原来的意思
            ////manageRequestTransType = "REMOVECARD";
            //manageRequestTransType = "CARDINSERTDETECTION";

            //ThreadStart entry1 = new ThreadStart(RemoveCardRun);
            //Thread process1 = new Thread(entry1);
            //process1.IsBackground = true;
            //process1.Start();

            PosLinkEngine engine = PosLinkEngine.GetInstance();
            engine.SetContext(this);
            engine.RemoveCard(timeout);

        }

        //v1.08 added read card function
        public void ReadCard(int timeout)
        {
            //ResetAllProperties();

            //manageRequestTransType = "INPUTACCOUNT";
            //manageRequestEDCType = "GIFT";
            //manageRequestTrans = "UNKNOWN";
            //manageRequestMagneticSwipeEntryFlag = "1";
            //manageRequestManualEntryFlag = "0";
            //manageRequestHRefNum = "";

            //if (timeout <= 0)
            //{
            //    manageRequestTimeout = "300"; // 0.1s
            //}
            //else
            //{
            //    manageRequestTimeout = Convert.ToString(timeout * 10);
            //}

            //ThreadStart entry1 = new ThreadStart(ReadCardRunV119);
            //Thread process1 = new Thread(entry1);
            //process1.IsBackground = true;
            //process1.Start();


            PosLinkEngine engine = PosLinkEngine.GetInstance();
            engine.SetContext(this);
            engine.ReadCard(timeout);
        }

        // v1.10 added
        public void Reset()
        {
            //ResetAllProperties();

            //manageRequestTransType = "RESET";

            //ThreadStart entry1 = new ThreadStart(ResetRun);
            //Thread process1 = new Thread(entry1);
            //process1.IsBackground = true;
            //process1.Start();


            PosLinkEngine.GetInstance().SetContext(this);
            PosLinkEngine.GetInstance().Reset();

        }

        //v1.11 added
        public void RequestInputText(string title, int inputType, int timeout)
        {
            //ResetAllProperties();

            //manageRequestTransType = "INPUTTEXT";
            //manageRequestTitle = title;
            //manageRequestInputType = Convert.ToString(inputType);

            //if (timeout <= 0)
            //{
            //    manageRequestTimeout = "300"; // 0.1s
            //}
            //else
            //{
            //    manageRequestTimeout = Convert.ToString(timeout * 10);
            //}

            //ThreadStart entry1 = new ThreadStart(RequestInputTextRun);
            //Thread process1 = new Thread(entry1);
            //process1.IsBackground = true;
            //process1.Start();

            PosLinkEngine.GetInstance().SetContext(this);
            PosLinkEngine.GetInstance().RequestInputText(title, inputType, timeout);
        }

        //public void GetVar(string EDCType, string varName)
        //{
        //    POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();
        //    int EDCTypeKey = manageRequest.ParseTransType(EDCType);
        //    GetVar(EDCTypeKey, varName);
        //}

        //v1.16 added
        public void GetVar(int EDCType, string varName)
        {
            errorCode = 0;

            //POSLink.CommSetting commSetting = new POSLink.CommSetting();

            //commSetting.CommType = Setting_CommType;
            //commSetting.TimeOut = Setting_TimeOut;
            //commSetting.SerialPort = Setting_SerialPort;
            //commSetting.DestIP = Setting_DestIP;
            //commSetting.DestPort = Setting_DestPort;
            //commSetting.BaudRate = Setting_BaudRate;
            ////commSetting.saveFile();
            //poslink.CommSetting = commSetting;

            //POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();
            //manageRequest.EDCType = EDCType;//manageRequest.ParseEDCType("ALL");
            //manageRequest.TransType = manageRequest.ParseTransType("GETVAR");
            //manageRequest.VarName = varName;
            //manageRequest.Upload = 0;
            //manageRequest.Timeout = "900";
            //poslink.ManageRequest = manageRequest;

            //result = poslink.ProcessTrans();
            //transResultMsg = result.Msg;
            //if (result.Code == POSLink.ProcessTransResultCode.OK)
            //{
            //    POSLink.ManageResponse manageResponse = poslink.ManageResponse;
            //    if (manageResponse != null && manageResponse.ResultCode != null)
            //    {
            //        manageResponseResultCode = manageResponse.ResultCode;
            //        manageResponseResultTxt = manageResponse.ResultTxt;
            //        manageResponseSN = manageResponse.SN;
            //        manageResponseVarValue = manageResponse.VarValue;
            //        //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
            //        manageResponseSigFileName = manageResponse.SigFileName;
            //        manageResponsePinBlock = manageResponse.PinBlock;
            //        manageResponseKSN = manageResponse.KSN;
            //        manageResponseTrack1Data = manageResponse.Track1Data;
            //        manageResponseTrack2Data = manageResponse.Track2Data;
            //        manageResponseTrack3Data = manageResponse.Track3Data;
            //        manageResponsePAN = manageResponse.PAN;
            //        manageResponseQRCode = manageResponse.QRCode;
            //        manageResponseEntryMode = manageResponse.EntryMode;
            //        manageResponseExpiryDate = manageResponse.ExpiryDate;
            //        manageResponseText = manageResponse.Text;

            //        //VarValue = manageResponse.VarValue;
            //        string outData = string.Format("<VarValue>{0}</VarValue>", manageResponse.VarValue);

            //        //if (manageResponse.ResultCode.Equals("000000"))
            //        //{
            //        //    DataReceived(new Response() { Code = ResultCode.SUCC_GET_VAR, Msg = "GETVAR complete.", Data = outData }.GetCode());
            //        //}
            //        //else
            //        //{
            //        //    DataReceived(new Response() { Code = ResultCode.ERR_GET_VAR, Msg = "GETVAR complete.", Data = outData }.GetCode());
            //        //}
            //        DataReceived(new CommResponse() { Code = ResultCode.SUCC_GET_VAR, Msg = "GETVAR complete.", Data = outData }.GetCode());
            //    }
            //    else
            //    {
            //        errorCode = 1;
            //        errorMessage = "Unknown error: mg.manageResponse is null.";

            //        DataReceived(new CommResponse() { Code = ResultCode.ERR_GET_VAR, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
            //    }
            //}
            //else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            //{
            //    errorCode = 2;
            //    errorMessage = "Action TimeOut";

            //    DataReceived(new CommResponse() { Code = ResultCode.ERR_GET_VAR, Msg = "Action TimeOut", Data = string.Empty }.GetCode());
            //}
            //else
            //{
            //    errorCode = 3;
            //    errorMessage = "Error Processing Manage";

            //    DataReceived(new CommResponse() { Code = ResultCode.ERR_GET_VAR, Msg = "Error Processing Manage", Data = string.Empty }.GetCode());
            //}

            //ResetAllProperties();
            PosLinkEngine.GetInstance().SetContext(this);
            PosLinkEngine.GetInstance().GetVar(EDCType, varName);
        }

        public void ManageRequestCommand_INIT()
        {
            //errorCode = 0;

            //POSLink.CommSetting commSetting = new POSLink.CommSetting();

            //commSetting.CommType = Setting_CommType;
            //commSetting.TimeOut = Setting_TimeOut;
            //commSetting.SerialPort = Setting_SerialPort;
            //commSetting.DestIP = Setting_DestIP;
            //commSetting.DestPort = Setting_DestPort;
            //commSetting.BaudRate = Setting_BaudRate;
            ////commSetting.saveFile();
            //poslink.CommSetting = commSetting;

            //POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            //manageRequest.EDCType = manageRequest.ParseEDCType("ALL");
            //manageRequest.TransType = manageRequest.ParseTransType("INIT");
            //manageRequest.Upload = 0;
            //manageRequest.Timeout = "900";
            //poslink.ManageRequest = manageRequest;

            //result = poslink.ProcessTrans();

            //if (result.Code == POSLink.ProcessTransResultCode.OK)
            //{
            //    POSLink.ManageResponse manageResponse = poslink.ManageResponse;
            //    if (manageResponse != null && manageResponse.ResultCode != null)
            //    {
            //        manageResponseResultCode = manageResponse.ResultCode;
            //        manageResponseResultTxt = manageResponse.ResultTxt;
            //        manageResponseSN = manageResponse.SN;
            //        manageResponseVarValue = manageResponse.VarValue;
            //        manageResponseSigFileName = manageResponse.SigFileName;
            //        manageResponsePinBlock = manageResponse.PinBlock;
            //        manageResponseKSN = manageResponse.KSN;
            //        manageResponseTrack1Data = manageResponse.Track1Data;
            //        manageResponseTrack2Data = manageResponse.Track2Data;
            //        manageResponseTrack3Data = manageResponse.Track3Data;
            //        manageResponsePAN = manageResponse.PAN;
            //        manageResponseQRCode = manageResponse.QRCode;
            //        manageResponseEntryMode = manageResponse.EntryMode;
            //        manageResponseExpiryDate = manageResponse.ExpiryDate;
            //        manageResponseText = manageResponse.Text;

            //        //if (null != onProcessManageComplete)
            //        //{
            //        //    onProcessManageComplete();
            //        //}

            //        SerialNumber = manageResponse.SN;
            //        //ResultOf(ResultCode.COMM_SUCC, "init succ.", string.Format("<SerialNumber>{0}</SerialNumber>", manageResponse.SN));

            //    }
            //    else
            //    {
            //        //MessageBox.Show("Unknown error: mg.manageResponse is null.");
            //        errorCode = 1;
            //        errorMessage = "Unknown error: mg.manageResponse is null.";

            //        //*************** v1.16 调整event返回 ***************/
            //        //if (null != onError)
            //        //{
            //        //    onError();
            //        //} 
            //        DataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
            //        //*********************************************/
            //    }
            //}
            //else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            //{
            //    errorCode = 2;
            //    errorMessage = "Action TimeOut";
            //    //*************** v1.16 调整event返回 ***************/
            //    //if (null != onError)
            //    //{
            //    //    onError();
            //    //}
            //    DataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Action TimeOut.", Data = string.Empty }.GetCode());
            //    //*********************************************/
            //}
            //else
            //{

            //    // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
            //    errorCode = 3;
            //    errorMessage = "Error Processing Manage";
            //    //*************** v1.16 调整event返回 ***************/
            //    //if (null != onError)
            //    //{
            //    //    onError();
            //    //}
            //    DataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Error Processing Manage", Data = string.Empty }.GetCode());
            //    //*********************************************/
            //}

            PosLinkEngine.GetInstance().SetContext(this);
            PosLinkEngine.GetInstance().Init();

        }

        public void ManageRequestCommand_SETVAR(string VarName, string VarValue)
        {
            //ResetAllProperties();
            //errorCode = 0;

            //POSLink.CommSetting commSetting = new POSLink.CommSetting();

            //commSetting.CommType = Setting_CommType;
            //commSetting.TimeOut = Setting_TimeOut;
            //commSetting.SerialPort = Setting_SerialPort;
            //commSetting.DestIP = Setting_DestIP;
            //commSetting.DestPort = Setting_DestPort;
            //commSetting.BaudRate = Setting_BaudRate;
            ////commSetting.saveFile();
            //poslink.CommSetting = commSetting;

            //POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

            //manageRequest.EDCType = manageRequest.ParseEDCType("ALL");
            //manageRequest.TransType = manageRequest.ParseTransType("SETVAR");
            //manageRequest.VarName = VarName;
            //manageRequest.VarValue = VarValue;
            //manageRequest.Upload = 0;
            //manageRequest.Timeout = "900";

            ////logwrite("Setting_CommType = " + Setting_CommType 
            ////    + " Setting_TimeOut =" + Setting_TimeOut
            ////    + " Setting_SerialPort =" + Setting_SerialPort
            ////     + " Setting_DestIP =" + Setting_DestIP
            ////      + " Setting_DestPort =" + Setting_DestPort
            ////       + " Setting_BaudRate =" + Setting_BaudRate);
            //poslink.ManageRequest = manageRequest;

            //result = poslink.ProcessTrans();

            ////logwrite("after pg.ProcessTrans() ");

            //if (result.Code == POSLink.ProcessTransResultCode.OK)
            //{

            //    POSLink.ManageResponse manageResponse = poslink.ManageResponse;
            //    if (manageResponse != null && manageResponse.ResultCode != null)
            //    {
            //        manageResponseResultCode = manageResponse.ResultCode;
            //        manageResponseResultTxt = manageResponse.ResultTxt;
            //        manageResponseSN = manageResponse.SN;
            //        manageResponseVarValue = manageResponse.VarValue;
            //        //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
            //        manageResponseSigFileName = manageResponse.SigFileName;
            //        manageResponsePinBlock = manageResponse.PinBlock;
            //        manageResponseKSN = manageResponse.KSN;
            //        manageResponseTrack1Data = manageResponse.Track1Data;
            //        manageResponseTrack2Data = manageResponse.Track2Data;
            //        manageResponseTrack3Data = manageResponse.Track3Data;
            //        manageResponsePAN = manageResponse.PAN;
            //        manageResponseQRCode = manageResponse.QRCode;
            //        manageResponseEntryMode = manageResponse.EntryMode;
            //        manageResponseExpiryDate = manageResponse.ExpiryDate;
            //        manageResponseText = manageResponse.Text;

            //        //if (null != onProcessManageComplete)
            //        //{
            //        //    onProcessManageComplete();
            //        //}

            //    }
            //    else
            //    {
            //        //MessageBox.Show("Unknown error: mg.manageResponse is null.");
            //        errorCode = 1;
            //        errorMessage = "Unknown error: mg.manageResponse is null.";
            //        //*************** v1.16 调整event返回 ***************/
            //        //if (null != onError)
            //        //{
            //        //    onError();
            //        //} 
            //        DataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Unknown error: mg.manageResponse is null.", Data = string.Empty }.GetCode());
            //        //*********************************************/
            //    }
            //}
            //else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            //{
            //    errorCode = 2;
            //    errorMessage = "Action TimeOut";
            //    //*************** v1.16 调整event返回 ***************/
            //    //if (null != onError)
            //    //{
            //    //    onError();
            //    //}
            //    DataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Action TimeOut", Data = string.Empty }.GetCode());
            //    //*********************************************/
            //}
            //else
            //{

            //    // MessageBox.Show(result.Msg, "Error Processing Manage", MessageBoxButtons.OK);
            //    errorCode = 3;
            //    errorMessage = "Error Processing Manage";

            //    //*************** v1.16 调整event返回 ***************/
            //    //if (null != onError)
            //    //{
            //    //    onError();
            //    //}
            //    DataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = "Error Processing Manage", Data = string.Empty }.GetCode());
            //    //*********************************************/
            //}

            PosLinkEngine.GetInstance().SetContext(this);
            PosLinkEngine.GetInstance().SetVar(VarName, VarValue);
        }


        //******************** v1.16注释 ********************//
        //public void processTrans()
        //{

        //    POSLink.CommSetting commSetting = new POSLink.CommSetting();

        //    commSetting.CommType = Setting_CommType;
        //    commSetting.TimeOut = Setting_TimeOut;
        //    commSetting.SerialPort = Setting_SerialPort;
        //    commSetting.DestIP = Setting_DestIP;
        //    commSetting.DestPort = Setting_DestPort;
        //    commSetting.BaudRate = Setting_BaudRate;
        //    //commSetting.saveFile();
        //    pg.CommSetting = commSetting;

        //    // 1. First create a POSLink Payment Page

        //    POSLink.PaymentRequest paymentRequest = new POSLink.PaymentRequest();

        //    paymentRequest.TenderType = paymentRequest.ParseTenderType(paymentRequestTenderType);
        //    paymentRequest.TransType = paymentRequest.ParseTransType(paymentRequestTransType);

        //    // 2. Next Set the PayLink Properties, the only required field is Amount
        //    #region Set PayLink Properties

        //    // Set the only required field: Amount
        //    string amount = paymentRequestAmount;
        //    // amount.to
        //    if (amount == "")
        //    {
        //        paymentRequest.Amount = "";

        //    }
        //    else
        //    {
        //        //amount = amount.Remove(amount.Length - 3, 1);
        //        double ret = Convert.ToDouble(amount);
        //        ret = ret * 100;

        //        String retstr = Convert.ToString(ret);
        //        paymentRequest.Amount = retstr;
        //    }


        //    // All these below are optionals
        //    paymentRequest.OrigRefNum = paymentRequestOrigRefNum;
        //    paymentRequest.InvNum = paymentRequestInvNum;
        //    paymentRequest.UserID = paymentRequestUserID;
        //    paymentRequest.PassWord = paymentRequestPassWord;
        //    paymentRequest.ClerkID = paymentRequestClerkID;
        //    paymentRequest.ServerID = paymentRequestServerID;
        //    if (paymentRequestTipAmt.Length > 0) paymentRequest.TipAmt = paymentRequestTipAmt;
        //    if (paymentRequestTaxAmt.Length > 0) paymentRequest.TaxAmt = paymentRequestTaxAmt;
        //    if (paymentRequestCashbackAmt.Length > 0) paymentRequest.CashBackAmt = paymentRequestCashbackAmt;
        //    if (paymentRequestMisc1Amt.Length > 0) paymentRequest.Misc1Amt = paymentRequestMisc1Amt;
        //    if (paymentRequestMisc2Amt.Length > 0) paymentRequest.Misc2Amt = paymentRequestMisc2Amt;
        //    if (paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = paymentRequestMisc3Amt;
        //    if (paymentRequestECRefNum.Length > 0)
        //    {
        //        paymentRequest.ECRRefNum = paymentRequestECRefNum;
        //    }
        //    if (paymentRequestCustomFields.Length > 0 || paymentRequestCustomerName.Length > 0)
        //    {

        //    }
        //    if (paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = paymentRequestMisc3Amt;
        //    if (paymentRequestSurchargeAmt.Length > 0) paymentRequest.SurchargeAmt = paymentRequestSurchargeAmt;
        //    paymentRequest.PONum = paymentRequestPONum;
        //    paymentRequest.Street = paymentRequestStreet;
        //    paymentRequest.Zip = paymentRequestZIP;
        //    paymentRequest.CssPath = paymentRequestCssPath;
        //    paymentRequest.ExtData = paymentRequestExtData;
        //    paymentRequest.AuthCode = paymentRequestAuthCode;
        //    #endregion

        //    //3 execute the function
        //    pg.PaymentRequest = paymentRequest;
        //    //POSLink.ProcessTransResult result = pg.ProcessTrans();  //move to tread
        //    ThreadStart entry1 = new ThreadStart(Run1);
        //    Thread process1 = new Thread(entry1);
        //    process1.IsBackground = true;
        //    process1.Start();

        //    ThreadStart entry2 = new ThreadStart(Run2);
        //    process2 = new Thread(entry2);
        //    process2.IsBackground = true;
        //    process2.Start();
        //}


        //public void doProcessTrans()
        //{

        //    // 1. First create a POSLink Payment Page

        //    POSLink.PaymentRequest paymentRequest = new POSLink.PaymentRequest();

        //    paymentRequest.TenderType = paymentRequest.ParseTenderType(paymentRequestTenderType);
        //    paymentRequest.TransType = paymentRequest.ParseTransType(paymentRequestTransType);

        //    // 2. Next Set the PayLink Properties, the only required field is Amount
        //    #region Set PayLink Properties

        //    // Set the only required field: Amount
        //    string amount = paymentRequestAmount;
        //    // amount.to
        //    if (amount == "")
        //    {
        //        paymentRequest.Amount = "";

        //    }
        //    else
        //    {
        //        //amount = amount.Remove(amount.Length - 3, 1);
        //        double ret = Convert.ToDouble(amount);
        //        ret = ret * 100;

        //        String retstr = Convert.ToString(ret);
        //        paymentRequest.Amount = retstr;
        //    }


        //    // All these below are optionals
        //    paymentRequest.OrigRefNum = paymentRequestOrigRefNum;
        //    paymentRequest.InvNum = paymentRequestInvNum;
        //    paymentRequest.UserID = paymentRequestUserID;
        //    paymentRequest.PassWord = paymentRequestPassWord;
        //    paymentRequest.ClerkID = paymentRequestClerkID;
        //    paymentRequest.ServerID = paymentRequestServerID;
        //    if (paymentRequestTipAmt.Length > 0) paymentRequest.TipAmt = paymentRequestTipAmt;
        //    if (paymentRequestTaxAmt.Length > 0) paymentRequest.TaxAmt = paymentRequestTaxAmt;
        //    if (paymentRequestCashbackAmt.Length > 0) paymentRequest.CashBackAmt = paymentRequestCashbackAmt;
        //    if (paymentRequestMisc1Amt.Length > 0) paymentRequest.Misc1Amt = paymentRequestMisc1Amt;
        //    if (paymentRequestMisc2Amt.Length > 0) paymentRequest.Misc2Amt = paymentRequestMisc2Amt;
        //    if (paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = paymentRequestMisc3Amt;
        //    if (paymentRequestECRefNum.Length > 0)
        //    {
        //        paymentRequest.ECRRefNum = paymentRequestECRefNum;
        //    }
        //    if (paymentRequestCustomFields.Length > 0 || paymentRequestCustomerName.Length > 0)
        //    {

        //    }
        //    if (paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = paymentRequestMisc3Amt;
        //    if (paymentRequestSurchargeAmt.Length > 0) paymentRequest.SurchargeAmt = paymentRequestSurchargeAmt;
        //    paymentRequest.PONum = paymentRequestPONum;
        //    paymentRequest.Street = paymentRequestStreet;
        //    paymentRequest.Zip = paymentRequestZIP;
        //    paymentRequest.CssPath = paymentRequestCssPath;
        //    paymentRequest.ExtData = paymentRequestExtData;
        //    paymentRequest.AuthCode = paymentRequestAuthCode;
        //    #endregion

        //    //3 execute the function
        //    pg.PaymentRequest = paymentRequest;
        //    //POSLink.ProcessTransResult result = pg.ProcessTrans();  //move to tread
        //    ThreadStart entry1 = new ThreadStart(Run1);
        //    Thread process1 = new Thread(entry1);
        //    process1.IsBackground = true;
        //    process1.Start();

        //    ThreadStart entry2 = new ThreadStart(Run2);
        //    process2 = new Thread(entry2);
        //    process2.IsBackground = true;
        //    process2.Start();
        //}



        //public void doProcessManage()
        //{
        //    POSLink.ManageRequest manageRequest = new POSLink.ManageRequest();

        //    manageRequest.EDCType = manageRequest.ParseEDCType(manageRequestEDCType);
        //    manageRequest.TransType = manageRequest.ParseTransType(manageRequestTransType);

        //    // 2. Next Set the PayLink Properties, the only required field is Amount
        //    #region Set PayLink Properties

        //    // Set the required field: none

        //    // All these below are optionals
        //    manageRequest.Trans = manageRequest.ParseTrans(manageRequestTrans);
        //    manageRequest.VarName = manageRequestVarName;
        //    manageRequest.VarValue = manageRequestVarValue;
        //    manageRequest.Title = manageRequestTitle;
        //    manageRequest.Button1 = manageRequestButton1;
        //    manageRequest.Button2 = manageRequestButton2;
        //    manageRequest.Button3 = manageRequestButton3;
        //    manageRequest.Button4 = manageRequestButton4;
        //    manageRequest.DisplayMessage = manageRequestDisplayMessage;
        //    manageRequest.ImagePath = manageRequestImagePath;
        //    manageRequest.ImageName = manageRequestImageName;
        //    if (String.IsNullOrEmpty(manageRequestUpload))
        //        manageRequest.Upload = Convert.ToInt32("0");
        //    else
        //        manageRequest.Upload = Convert.ToInt32(manageRequestUpload);
        //    //manageRequest.Upload = RT_TB_Upload.Text;
        //    manageRequest.HRefNum = manageRequestHRefNum;
        //    manageRequest.Timeout = manageRequestTimeout;
        //    manageRequest.ThankYouTitle = manageRequestThankYouTitle;
        //    manageRequest.ThankYouMessage1 = manageRequestThankYouMessage1;
        //    manageRequest.ThankYouMessage2 = manageRequestThankYouMessage2;
        //    manageRequest.ThankYouTimeout = manageRequestThankYouTimeout;
        //    manageRequest.AccountNumber = manageRequestAccountNumber;
        //    manageRequest.EncryptionType = "1";
        //    manageRequest.KeySlot = manageRequestKeySlot;
        //    manageRequest.PinMaxLength = manageRequestPinMaxLength;
        //    manageRequest.PinMinLength = manageRequestPinMinLength;
        //    manageRequest.NullPin = manageRequestNullPin;
        //    manageRequest.PinAlgorithm = manageRequestPinAlgorithm;
        //    manageRequest.MagneticSwipeEntryFlag = manageRequestMagneticSwipeEntryFlag;
        //    manageRequest.ManualEntryFlag = manageRequestManualEntryFlag;
        //    manageRequest.ContactlessEntryFlag = manageRequestContactlessEntryFlag;
        //    manageRequest.ScannerEntryFlag = manageRequestScannerEntryFlag;
        //    manageRequest.EncryptionFlag = manageRequestEncryptionFlag;
        //    manageRequest.MAXAccountLength = manageRequestMAXAccountLength;
        //    manageRequest.MINAccountLength = manageRequestMINAccountLength;
        //    manageRequest.ExpiryDatePrompt = manageRequestExpiryDatePrompt;
        //    manageRequest.InputType = manageRequestInputType;
        //    manageRequest.DefaultValue = manageRequestDefaultValue;
        //    manageRequest.MAXLength = manageRequestMAXLength;
        //    manageRequest.MINLength = manageRequestMINLength;
        //    manageRequest.FileName = manageRequestFileName;

        //    String path = manageRequestSigSavePath;
        //    if (path.EndsWith(":"))
        //    {
        //        path = path + "\\";
        //    }
        //    manageRequest.SigSavePath = path;


        //    #endregion

        //    //3 execute the function
        //    pg.ManageRequest = manageRequest;
        //    //POSLink.ProcessTransResult result = pg.ProcessTrans();  //move to tread
        //    ThreadStart entry1 = new ThreadStart(ManageRun1);
        //    Thread process1 = new Thread(entry1);
        //    process1.IsBackground = true;
        //    process1.Start();
        //}
        //*********************************************//

        //public void getSignature()
        //{
        //    manageRequestTransType = "GETSIGNATURE";
        //    manageRequestEDCType = "ALL";
        //    manageRequestUpload = "0";
        //    manageRequestTimeout = "900";
        //    manageRequestHRefNum = "";
        //    manageRequestSigSavePath = "";

        //    doProcessManage();
        //}

        //private void SaveSigImage(string SigFileName)
        //{
        //    int sig_width = 365;
        //    int sig_height = 192;
        //    if (String.IsNullOrEmpty(SigFileName))
        //    {
        //        //sigDetail.Show();
        //        MessageBox.Show("Sig File Path Is Null!", "Warning", MessageBoxButtons.OK);
        //        return;
        //    }
        //    String filePath;
        //    String fullPath;
        //    StreamReader sr;
        //    try
        //    {
        //        if (manageRequestSigSavePath != "")
        //        {
        //            //  filePath = this.RT_TB_SIGPATH.Text + "\\" + this.RS_TB_SigFileName.Text;
        //            String path = manageRequestSigSavePath;
        //            if (path.EndsWith(":"))
        //            {
        //                path = path + "\\";
        //            }
        //            fullPath = Path.GetFullPath(path);
        //            filePath = Path.Combine(fullPath, SigFileName);
        //        }
        //        else
        //        {
        //            String path;
        //            path = System.Windows.Forms.Application.StartupPath;

        //            // filePath = path + "\\" + this.SigFileName;
        //            filePath = Path.Combine(path, SigFileName);
        //        }
        //        sr = File.OpenText(filePath);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Logger.error("saveSigImage", ex.Message);
        //        MessageBox.Show("Sig File Path Error!", "Warning", MessageBoxButtons.OK);
        //        return;
        //    }

        //    String temp = sr.ReadToEnd();
        //    int len = temp.Length;


        //    //show sig image
        //    Bitmap bmp;
        //    bmp = new Bitmap(sig_width, sig_height);
        //    Graphics g = Graphics.FromImage(bmp);
        //    g.Clear(Color.White);//背景色
        //    char div = '^';
        //    string[] signature_divide = temp.Split(div);
        //    string x;
        //    string y;
        //    string x_y;
        //    int mt30Flag = 0;
        //    for (int i = 0; i < signature_divide.Length - 1; i++)
        //    {
        //        try
        //        {

        //            x_y = signature_divide[i];
        //            int pos = signature_divide[i].IndexOf(",");
        //            x = x_y.Substring(0, pos);
        //            // Console.WriteLine("x ={0]",x);
        //            y = x_y.Substring(pos + 1);

        //            if (Int32.Parse(y) == 65535)
        //            {
        //                mt30Flag = 1;
        //            }

        //            // Console.WriteLine("y ={0]",y);
        //            if (Int32.Parse(x) <= 480 && Int32.Parse(y) <= 320)
        //            {
        //                bmp.SetPixel(Int32.Parse(x), Int32.Parse(y), Color.Black);
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            //ignore incorrect point
        //            Logger.error("saveSigImage", ex.Message);
        //        }

        //    }

        //    //connect dots
        //    if (mt30Flag == 1)
        //    {
        //        Point p1 = new Point();
        //        Point p2 = new Point();
        //        Pen blackPen = new Pen(Color.Black, 2.0f);

        //        for (int i = 1; i < signature_divide.Length - 1; i++)
        //        {
        //            try
        //            {
        //                x_y = signature_divide[i - 1];
        //                int pos = signature_divide[i - 1].IndexOf(",");
        //                y = x_y.Substring(pos + 1);
        //                x = x_y.Substring(0, pos);
        //                if (Int32.Parse(y) == 65535)
        //                {
        //                    continue;
        //                }

        //                //bitmap->SetPixel(Int32::Parse(x)+magin-minx, Int32::Parse(y)+magin-miny, Color::Black);
        //                p1.X = Int32.Parse(x);
        //                p1.Y = Int32.Parse(y);

        //                x_y = signature_divide[i];
        //                pos = signature_divide[i].IndexOf(",");
        //                y = x_y.Substring(pos + 1);
        //                x = x_y.Substring(0, pos);
        //                if (Int32.Parse(y) == 65535)
        //                {

        //                    continue;
        //                }

        //                //bitmap->SetPixel(Int32::Parse(x)+magin-minx, Int32::Parse(y)+magin-miny, Color::Black);
        //                p2.X = Int32.Parse(x);
        //                p2.Y = Int32.Parse(y);

        //                g.DrawLine(blackPen, p1, p2);
        //            }
        //            catch (Exception)
        //            {
        //                return;
        //            }

        //        }
        //    }
        //    sr.Close();
        //    // gra.DrawImage(bmp, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

        //    string fileName = System.Environment.CurrentDirectory + "\\" + "Sig" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
        //    try
        //    {
        //        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);

        //        if (onSignatureReceived != null)
        //        {
        //            onSignatureReceived(fileName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        StreamWriter log = new StreamWriter(@"C:\\log.txt", true);

        //        log.WriteLine("Exception: " + ex.Message);

        //        log.Close();
        //    }
        //}


        private void ResultOf(string resultCode, string msg)
        {
            ResultOf(resultCode, msg, string.Empty);
        }

        private void ResultOf(string resultCode, string msg, string dataSet)
        {
            ReportDataReceived(new CommResponse() { Code = resultCode, Msg = msg, Data = dataSet }.GetCode());
        }

        public void ResetAllRequest()
        {
            if (poslink != null)
            {
                poslink.ManageRequest = null;
                poslink.PaymentRequest = null;
                poslink.ReportRequest = null;
            }
        }

        public void InitPoslinkConfig()
        {
            POSLink.CommSetting commSetting = new POSLink.CommSetting();
            commSetting.CommType = Setting_CommType;
            commSetting.TimeOut = Setting_TimeOut;
            commSetting.SerialPort = Setting_SerialPort;
            commSetting.DestIP = Setting_DestIP;
            commSetting.DestPort = Setting_DestPort;
            commSetting.BaudRate = Setting_BaudRate;
            //commSetting.saveFile();
            poslink.CommSetting = commSetting;
        }

        /**
         * v.1.18 added
         * 外部查询调用方法 
         * 查询pax机上的report
         */
        public void QueryDetailReport(string EcrRefNum)
        {
            ResponseData = string.Empty;
            transResultMsg = string.Empty;
            errorCode = 0;
            errorMessage = string.Empty;

            InitPoslinkConfig();
            ResetAllProperties();

            QueryDetailReportAsync(EcrRefNum);
        }

        public void QueryDetailReportAsync(string EcrRefNum)
        {
            /**************** 方式1 Kevin说获取不了结果 ****************/
            //string tranType = "LOCALDETAILREPORT";
            //string edcType = "ALL";

            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("TranType", tranType);
            //dic.Add("EDCType", edcType);
            //dic.Add("EcrRefNum", EcrRefNum);

            //WaitCallback cb = new WaitCallback(delegate(object state)
            //{
            //    try
            //    {
            //        Dictionary<string, object> param = (Dictionary<string, object>)state;
            //        string param1 = (string)param["TranType"];
            //        string param2 = (string)param["EDCType"];
            //        string param3 = (string)param["EcrRefNum"];
            //        QueryReport(param1, param2, param3);
            //    }
            //    catch (Exception ex)
            //    {
            //        Logger.error(TAG, string.Format("{0}::QueryDetailReport \r\n{1}", TAG, ex.Message));
            //        ResultOf(ResultCode.ERR_REPORT, ex.Message);
            //    }
            //});
            //ThreadPool.QueueUserWorkItem(cb, dic);
            /****************************************/


            QueryReportController.ReportParam args = new QueryReportController.ReportParam();
            args.EcrRefNum = EcrRefNum;
            QueryReportController controller = new QueryReportController(poslink, args);
            controller.responseDelegate += DataReceivedDelegate;
            Thread queryReportThread = new Thread(controller.QueryDetailReport);
            queryReportThread.IsBackground = true;
            queryReportThread.Start();
        }



        //public void QueryReport(string transType, string edcType, string EcrRefNum)
        //{
        //    ResponseData = string.Empty;
        //    transResultMsg = string.Empty;

        //    InitPoslinkConfig();
        //    POSLink.ReportRequest reportRequest = new POSLink.ReportRequest();
        //    reportRequest.TransType = reportRequest.ParseTransType(transType);
        //    reportRequest.EDCType = reportRequest.ParseEDCType(edcType);
        //    reportRequest.ECRRefNum = EcrRefNum;

        //    poslink.ReportRequest = reportRequest;
        //    result = poslink.ProcessTrans();

        //    transResultMsg = result.Msg;
        //    if (result.Code == POSLink.ProcessTransResultCode.OK)
        //    {
        //        POSLink.ReportResponse reportResponse = poslink.ReportResponse;
        //        if (reportResponse != null && reportResponse.ResultCode != null)
        //        {
        //            ResponseData = PaxResponseUtil.parseReportResponse(reportResponse);
        //            DataReceived(new Response() { Code = ResultCode.SUCC_REPORT, Msg = "Report completed.", Data = ResponseData }.GetCode());
        //        }
        //        else
        //        {
        //            DataReceived(new Response() { Code = ResultCode.ERR_REPORT, Msg = "Unknown error: poslink.ReportResponse is null.", Data = string.Empty }.GetCode());
        //        }
        //    }
        //    else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
        //    {
        //        DataReceived(new Response() { Code = ResultCode.ERR_REPORT, Msg = "Action Timeout.", Data = string.Empty }.GetCode());
        //    }
        //    else
        //    {
        //        DataReceived(new Response() { Code = ResultCode.ERR_REPORT, Msg = "Error Processing Report.", Data = string.Empty }.GetCode());
        //    }  
        //}

        private void ResetAllProperties()
        {
            paymentResponseResultCode = "";
            paymentResponseResultTxt = "";
            paymentResponseRefNum = "";
            paymentResponseRawResponse = "";
            paymentResponseAvsResponse = "";
            paymentResponseCvResponse = "";
            paymentResponseTimestamp = "";
            paymentResponseHostCode = "";
            paymentResponseRequestedAmt = "";
            paymentResponseApprovedAmt = "";
            paymentResponseRemainingBalance = "";
            paymentResponseExtraBalance = "";
            paymentResponseHostResponse = "";
            paymentResponseBogusAccountNum = "";
            paymentResponseCardType = "";
            paymentResponseMessage = "";
            paymentResponseExtData = "";
            paymentResponseAuthCode = "";


            // v1.19 修复crash bug
            manageRequestTrans = "UNKNOWN";
            manageRequestSigSavePath = "";

            manageResponseResultCode = "";
            manageResponseResultTxt = "";
            manageResponseSN = "";
            manageResponseVarValue = "";
            //manageResponseBottonNum = "";//v1.09 removed
            manageResponseSigFileName = "";
            manageResponsePinBlock = "";
            manageResponseKSN = "";
            manageResponseTrack1Data = "";
            manageResponseTrack2Data = "";
            manageResponseTrack3Data = "";
            manageResponsePAN = "";
            manageResponseQRCode = "";
            manageResponseEntryMode = "";
            manageResponseExpiryDate = "";
            manageResponseText = "";

            //respInputText = ""; //v1.13 added  // v1.16 remove
            isCancelTask = false;

            ResetAllRequest();
        }

        public void logwrite(string msg)
        {
            StreamWriter log = new StreamWriter(@"C:\\log.txt", true);
            log.WriteLine("Log Message: " + msg);
            log.Close();
        }

        private void DataReceivedDelegate(string paxResultCode, string paxResultTxt, string paxTransResultMsg, CommResponse response)
        {
            //errorMessage = response.Msg;
            ResponseData = response.Data;
            transResultMsg = paxTransResultMsg;
            manageResponseResultCode = paxResultCode;
            manageResponseResultTxt = paxResultTxt;

            string code = response.GetCode();
            switch (code)
            {
                case ResultCode.PAX_RESPONSE_NULL:
                    errorCode = Convert.ToInt32(ResultCode.PAX_RESPONSE_NULL);
                    errorMessage = "The response object is null.";
                    code = ResultCode.ERR_ANY;
                    break;
                case ResultCode.PAX_TIME_OUT:
                    errorCode = Convert.ToInt32(ResultCode.PAX_TIME_OUT);
                    errorMessage = "Device timeout.";
                    code = ResultCode.ERR_ANY;
                    break;
                case ResultCode.PAX_INCORRECT_RESULT_TYPE:
                    errorCode = Convert.ToInt32(ResultCode.PAX_INCORRECT_RESULT_TYPE);
                    errorMessage = "Incorrect result type.";
                    code = ResultCode.ERR_ANY;
                    break;
                default:
                    //
                    break;
            }

            ReportDataReceived(code);
        }

        public void ReportDataReceived(string res)
        {
            if (onResponse != null)
            {
                onResponse(res);
            }
        }

        public void ReportDataReceived(CommResponse resp)
        {
            ReportDataReceived(resp.GetCode());
        }

        public void ReportStatusChanged(int statuscode, string msg)
        {
            if(onStatusChanged != null)
            {
                onStatusChanged(statuscode, msg);
            }
        }
         

        /// <summary>
        /// 显示多种当前路径的方法
        /// </summary>
        public void ShowCurrentPath()
        {
            //获取包含清单的已加载文件的路径或 UNC 位置。
            logwrite("Assembly.GetExecutingAssembly().Location = " + Assembly.GetExecutingAssembly().Location);
            //result: X:\xxx\xxx\xxx.dll (.dll文件所在的目录+.dll文件名)


            //获取当前进程的完整路径，包含文件名(进程名)。
            logwrite("this.GetType ( ).Assembly.Location = " + this.GetType().Assembly.Location);
            //result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)


            //获取新的 Process 组件并将其与当前活动的进程关联的主模块的完整路径，包含文件名(进程名)。
            logwrite("System.Diagnostics.Process.GetCurrentProcess ( ).MainModule.FileName = " + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)


            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            logwrite("System.Environment.CurrentDirectory = " + System.Environment.CurrentDirectory);
            //result: X:\xxx\xxx (.exe文件所在的目录)


            //获取当前 Thread 的当前应用程序域的基目录，它由程序集冲突解决程序用来探测程序集。
            logwrite("System.AppDomain.CurrentDomain.BaseDirectory = " + System.AppDomain.CurrentDomain.BaseDirectory);
            //result: X:\xxx\xxx\ (.exe文件所在的目录+"\")


            //获取和设置包含该应用程序的目录的名称。
            logwrite("System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase = " + System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            //result: X:\xxx\xxx\ (.exe文件所在的目录+"\")


            //获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。
            logwrite("System.Windows.Forms.Application.StartupPath = " + System.Windows.Forms.Application.StartupPath);
            //result: X:\xxx\xxx (.exe文件所在的目录)


            //获取启动了应用程序的可执行文件的路径，包括可执行文件的名称。
            logwrite("System.Windows.Forms.Application.ExecutablePath = " + System.Windows.Forms.Application.ExecutablePath);
            //result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)


            //获取应用程序的当前工作目录(不可靠)。
            logwrite("System.IO.Directory.GetCurrentDirectory()  = " + System.IO.Directory.GetCurrentDirectory());
            //result: X:\xxx\xxx (.exe文件所在的目录)
        }
        #endregion


        #region Events


        [ComVisible(false)]
        public delegate void onStatusChangedEventHandler(int statuscode, string msg);
        public event onStatusChangedEventHandler onStatusChanged = null;

        //[ComVisible(false)]
        //public delegate void onReturnReportedStatusEventHandler(int status);
        //public event onReturnReportedStatusEventHandler onReturnReportedStatus = null;

        //[ComVisible(false)]
        //public delegate void onProcessErrorEventHandler(string msg);
        //public event onProcessErrorEventHandler onProcessError = null;

        //[ComVisible(false)]
        //public delegate void onActionTimeOutEventHandler();
        //public event onActionTimeOutEventHandler onActionTimeOut = null;

        //[ComVisible(false)]
        //public delegate void onProcessTransCompleteEventHandler();
        //public event onProcessTransCompleteEventHandler onProcessTransComplete = null;

        //[ComVisible(false)]
        //public delegate void onProcessManageCompleteEventHandler();
        //public event onProcessManageCompleteEventHandler onProcessManageComplete = null;

        //[ComVisible(false)]
        //public delegate void onSignatureReceivedEventHandler(string SigFileName);
        //public event onSignatureReceivedEventHandler onSignatureReceived = null;

        //v1.16 removed
        //[ComVisible(false)]
        //public delegate void onPaymentCompletedEventHandler();
        //public event onPaymentCompletedEventHandler onPaymentCompleted = null;

        //v1.16 removed
        //[ComVisible(false)]
        //public delegate void onManageCompletedEventHandler();
        //public event onManageCompletedEventHandler onManageCompleted = null;

        //v1.16 removed
        //[ComVisible(false)]
        //public delegate void onErrorEventHandler();
        //public event onErrorEventHandler onError = null;

        //v1.16 removed
        //[ComVisible(false)]
        //public delegate void onDoSignatureCompletedEventHandler();
        //public event onDoSignatureCompletedEventHandler onDoSignatureCompleted = null;

        //v1.16 removed
        //[ComVisible(false)]
        //public delegate void onRemoveCardCompletedEventHandler();
        //public event onRemoveCardCompletedEventHandler onRemoveCardCompleted = null;

        //v1.16 removed
        //[ComVisible(false)]
        //public delegate void onReadCardCompletedEventHandler();
        //public event onReadCardCompletedEventHandler onReadCardCompleted = null;

        //v1.16 removed
        //[ComVisible(false)]
        //public delegate void onResetCompletedEventHandler();
        //public event onResetCompletedEventHandler onResetCompleted = null;

        //****************** v1.16 removed ******************//
        //v1.11 added
        //[ComVisible(false)]
        //public delegate void onRequestInputTextCompletedEventHandler(string res);
        //public event onRequestInputTextCompletedEventHandler onRequestInputTextCompleted = null;
        //[ComVisible(false)]
        //public delegate void onRequestInputTextCompletedEventHandler();
        //public event onRequestInputTextCompletedEventHandler onRequestInputTextCompleted = null;
        //*************************************************//


        // v1.16 added
        [ComVisible(false)]
        public delegate void OnResponseEventHandler(string res);
        public event OnResponseEventHandler onResponse = null;

        #endregion
 
      
    }
}
