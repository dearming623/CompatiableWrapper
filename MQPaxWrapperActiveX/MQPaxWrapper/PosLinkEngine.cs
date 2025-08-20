#region <<Notes>>
/*----------------------------------------------------------------
 * Copy right (c) 2024  All rights reserved
 * CLR Ver: 4.0.30319.42000
 * Computer: MOLEQ-MING
 * Company: 
 * namespace: MQPaxWrapper
 * Unique: 941576a8-66c1-4068-8a21-a762590e6af0
 * File name: PosLinkEngine2
 * Domain: MOLEQ-MING
 * 
 * @author: Ming
 * @email: t8ming@live.com
 * @date: 7/17/2024 11:24:28
 *----------------------------------------------------------------*/
#endregion <<Notes>>
using MQPaxWrapper.Exceptions;
using MQPaxWrapper.Model; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using POSLink;

namespace MQPaxWrapper
{
    public class PosLinkEngine
    {
        private static PosLinkEngine instance = null;
        private static readonly PosLink poslink = new PosLink();
        private PosLinkWrapper posLinkWrapper;
        private string sigSavePath = "";
        private string sigPicName = "";
        private Func<ManageRequest, ManageResponse, bool> interceptor = null;
        public bool isCancelTask = false;

        public static PosLinkEngine GetInstance()
        {
            lock (poslink)
            {
                if (instance == null)
                {
                    instance = new PosLinkEngine();
                }
                return instance;
            }
        }

        public PosLink GetPosLinkInstance()
        {
            return poslink;
        }

        public void SetContext(PosLinkWrapper wrapper)
        {
            this.posLinkWrapper = wrapper;
        }

        public void SetVar(string varName, string varValue)
        {
            ResetPoslLinkParams();

            posLinkWrapper.errorCode = 0;
            posLinkWrapper.manageRequestTransType = "SETVAR";
            posLinkWrapper.manageRequestEDCType = "ALL";
            posLinkWrapper.manageRequestVarName = varName;
            posLinkWrapper.manageRequestVarValue = varValue;
            posLinkWrapper.manageRequestUpload = "0";
            posLinkWrapper.manageRequestTimeout = "900";

            //set var 不触发data receive函数
            interceptor = (manageRequest, manageResponse) => { return true; };
            ProcessIntenalOnDirect(new ThreadParam() { succCode = ResultCode.SUCC_SET_VAR, succMsg = "Set Var completed.", errCode = ResultCode.ERR_SET_VAR });

        }

        public void GetSignature(int upload, string sigSavePath, string picName, int timeout)
        {
            ResetPoslLinkParams();

            posLinkWrapper.manageRequestTransType = "GETSIGNATURE";
            posLinkWrapper.manageRequestEDCType = "ALL";
            posLinkWrapper.manageRequestUpload = Convert.ToString(upload);
            //manageRequestTimeout = "900";

            if (timeout <= 0)
            {
                posLinkWrapper.manageRequestTimeout = "900";
            }
            else
            {
                posLinkWrapper.manageRequestTimeout = Convert.ToString(timeout * 10);//timeout时间以 0.1s 为单位
            }

            posLinkWrapper.manageRequestHRefNum = "";
            posLinkWrapper.manageRequestSigSavePath = sigSavePath;

            sigPicName = picName;

            interceptor = (manageRequest, manageResponse) =>
            {
                string afterConvertSigFileName = manageRequest.SigSavePath + "\\" + sigPicName;
                string devOutputSigFileName = manageRequest.SigSavePath + "\\" + manageResponse.SigFileName;
                string tips = "Error Convert Sig To Pic.";

                DearMing.Common.Utils.Log.i("GetSignature", "Start conversion.");

                try
                {
                    int k = manageRequest.ConvertSigToPic(devOutputSigFileName, "jpg", afterConvertSigFileName);

                    if (k == 0)
                    {
                        if (File.Exists(devOutputSigFileName))
                            File.Delete(devOutputSigFileName);

                        DearMing.Common.Utils.Log.i("GetSignature", "End conversion.");

                        //process succ return false
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    DearMing.Common.Utils.Log.i("GetSignature", "Conversion error.");

                    tips = "Get signature conversion error. -> " + ex.Message;
                }

                // error - return custome error message
                posLinkWrapper.errorCode = 7;
                posLinkWrapper.errorMessage = tips;
                posLinkWrapper.ReportDataReceived(new CommResponse() { Code = ResultCode.ERR_MANAGE, Msg = tips, Data = string.Empty });
                
                return true;
            };

            ProcessIntenalOnBackground(new ThreadParam() { succCode = ResultCode.SUCC_MANAGE, succMsg = "Get Signature completed.", errCode = ResultCode.ERR_MANAGE });

        }

        public void DoSignature(int upload, int timeout)
        {
            ResetPoslLinkParams();

            posLinkWrapper.manageRequestTransType = "DOSIGNATURE";
            posLinkWrapper.manageRequestEDCType = "ALL";
            posLinkWrapper.manageRequestUpload = Convert.ToString(upload);
            posLinkWrapper.manageRequestHRefNum = "";
            //manageRequestTimeout = "300"; // 签名timeout时间以 0.1s 为单位
            if (timeout <= 0)
            {
                posLinkWrapper.manageRequestTimeout = "300"; // 0.1s
            }
            else
            {
                posLinkWrapper.manageRequestTimeout = Convert.ToString(timeout * 10);
            }
            interceptor = (manageRequest, manageResponse) =>
            {
                posLinkWrapper.errorCode = 0;
                return false;
            };

            ProcessIntenalOnBackground(new ThreadParam() { succCode = ResultCode.SUCC_DO_SIGNATURE, succMsg = "Do signature completed.", errCode = ResultCode.ERR_DO_SIGNATURE });
        }

        public void ReadCard(int timeout)
        {
            ResetPoslLinkParams();

            posLinkWrapper.manageRequestTransType = "INPUTACCOUNT";
            posLinkWrapper.manageRequestEDCType = "GIFT";
            posLinkWrapper.manageRequestTrans = "UNKNOWN";
            posLinkWrapper.manageRequestMagneticSwipeEntryFlag = "1";
            posLinkWrapper.manageRequestManualEntryFlag = "0";
            posLinkWrapper.manageRequestHRefNum = "";

            if (timeout <= 0)
            {
                posLinkWrapper.manageRequestTimeout = "300"; // 0.1s
            }
            else
            {
                posLinkWrapper.manageRequestTimeout = Convert.ToString(timeout * 10);
            }
            ProcessIntenalOnBackground(new ThreadParam() { succCode = ResultCode.SUCC_READ_CARD, succMsg = "Read card completed.", errCode = ResultCode.ERR_READ_CARD });
        }


        public void RemoveCard(int timeout)
        {
            ResetPoslLinkParams();

            if (timeout > 0)
            {
                posLinkWrapper.Setting_TimeOut = Convert.ToString(timeout * 1000);
            }

            //v1.14 调整remove card方法 ,按照Kevin说法，这个方法名字不是原来的意思
            //manageRequestTransType = "REMOVECARD";
            posLinkWrapper.manageRequestTransType = "CARDINSERTDETECTION";
            interceptor = (manageRequest, manageResponse) =>
                {
                    posLinkWrapper.manageResponseVarValue = manageResponse.CardInsertStatus; //v1.14 因为不想增加新的变量。所以直接把状态复制到varvalue 
                    return false;
                };
            ProcessIntenalOnBackground(new ThreadParam() { succCode = ResultCode.SUCC_REMOVE_CARD, succMsg = "Remove card completed.", errCode = ResultCode.REMOVE_CARD_ERR });
        }

        private void InvokeProcessManageAction(object args)
        {
            ThreadParam parameters = args as ThreadParam;

            posLinkWrapper.errorCode = 0;
            posLinkWrapper.transResultMsg = "";

            ManageRequest manageRequest = CreateManageRequest(posLinkWrapper);
            poslink.ManageRequest = manageRequest;

            CommResponse commResp = new CommResponse()
            {
                Code = parameters.errCode,
                Msg = "Unknown error: mg.manageResponse is null.",
                Data = string.Empty
            };

            try
            {
                ManageResponse manageResponse = UseManageRequestProcessTran(manageRequest);
                if (manageResponse != null)
                {
                    FillManageResponseToWrapper(manageResponse);
                    if (null != interceptor)
                    {
                        bool processed = interceptor(manageRequest, manageResponse);
                        if (processed)
                        {
                            return;
                        }
                    }

                    commResp = new CommResponse()
                    {
                        Code = parameters.succCode,
                        Msg = parameters.succMsg,
                        Data = string.Empty
                    };
                }
            }
            catch (FailProcessException ex)
            {
                posLinkWrapper.errorCode = 4;
                posLinkWrapper.errorMessage = ex.Message;
                commResp = new CommResponse()
                {
                    Code = parameters.errCode,
                    Msg = "Unknown error: mg.manageResponse is null.",
                    Data = string.Empty
                };
            }
            catch (ActionTimeoutExcepiton ex)
            {
                posLinkWrapper.errorCode = 5;
                posLinkWrapper.errorMessage = ex.Message;
                commResp = new CommResponse()
                {
                    Code = parameters.errCode,
                    Msg = "Action Timeout.",
                    Data = string.Empty
                };
            }
            catch (NullResponseException ex)
            {
                posLinkWrapper.errorCode = 6;
                posLinkWrapper.errorMessage = ex.Message;
                commResp = new CommResponse()
                {
                    Code = parameters.errCode,
                    Msg = "Error Processing Manage.",
                    Data = string.Empty
                };
            }

            posLinkWrapper.ReportDataReceived(commResp);

        }
        private void InvokeProcessPaymentAction()
        {

            posLinkWrapper.errorCode = 0;
            posLinkWrapper.transResultMsg = "";

            PaymentRequest paymentRequest = new PaymentRequest();

            paymentRequest.TenderType = paymentRequest.ParseTenderType(posLinkWrapper.paymentRequestTenderType);
            paymentRequest.TransType = paymentRequest.ParseTransType(posLinkWrapper.paymentRequestTransType);

            #region Set PayLink Properties

            // Set the only required field: Amount
            string amount = posLinkWrapper.paymentRequestAmount;
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

                string retstr = Convert.ToString(ret);
                paymentRequest.Amount = retstr;
            }


            // All these below are optionals
            paymentRequest.OrigRefNum = posLinkWrapper.paymentRequestOrigRefNum;
            paymentRequest.InvNum = posLinkWrapper.paymentRequestInvNum;
            //paymentRequest.UserID = posLinkWrapper.paymentRequestUserID;     // api 弃用
            //paymentRequest.PassWord = posLinkWrapper.paymentRequestPassWord; // api 弃用
            paymentRequest.ClerkID = posLinkWrapper.paymentRequestClerkID;
            //paymentRequest.ServerID = posLinkWrapper.paymentRequestServerID; // api 弃用

            if (posLinkWrapper.paymentRequestTipAmt.Length > 0) paymentRequest.TipAmt = posLinkWrapper.paymentRequestTipAmt;
            if (posLinkWrapper.paymentRequestTaxAmt.Length > 0) paymentRequest.TaxAmt = posLinkWrapper.paymentRequestTaxAmt;
            if (posLinkWrapper.paymentRequestCashbackAmt.Length > 0) paymentRequest.CashBackAmt = posLinkWrapper.paymentRequestCashbackAmt;
            //if (posLinkWrapper.paymentRequestMisc1Amt.Length > 0) paymentRequest.Misc1Amt = posLinkWrapper.paymentRequestMisc1Amt; // api 弃用
            //if (posLinkWrapper.paymentRequestMisc2Amt.Length > 0) paymentRequest.Misc2Amt = posLinkWrapper.paymentRequestMisc2Amt; // api 弃用
            //if (posLinkWrapper.paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = posLinkWrapper.paymentRequestMisc3Amt; // api 弃用
            if (posLinkWrapper.paymentRequestECRefNum.Length > 0)
            {
                paymentRequest.ECRRefNum = posLinkWrapper.paymentRequestECRefNum;
            }
            if (posLinkWrapper.paymentRequestCustomFields.Length > 0 || posLinkWrapper.paymentRequestCustomerName.Length > 0)
            {

            }
            //if (posLinkWrapper.paymentRequestMisc3Amt.Length > 0) paymentRequest.Misc3Amt = posLinkWrapper.paymentRequestMisc3Amt; // api 弃用
            if (posLinkWrapper.paymentRequestSurchargeAmt.Length > 0) paymentRequest.SurchargeAmt = posLinkWrapper.paymentRequestSurchargeAmt;
            paymentRequest.PONum = posLinkWrapper.paymentRequestPONum;
            paymentRequest.Street = posLinkWrapper.paymentRequestStreet;
            paymentRequest.Zip = posLinkWrapper.paymentRequestZIP;
            //paymentRequest.CssPath = posLinkWrapper.paymentRequestCssPath; // api 弃用
            paymentRequest.ExtData = posLinkWrapper.paymentRequestExtData;
            paymentRequest.AuthCode = posLinkWrapper.paymentRequestAuthCode;
            paymentRequest.SigSavePath = sigSavePath;
            #endregion

            CommResponse commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Unknown error: pg.PaymentResponse is null.", Data = string.Empty };
            try
            {
                POSLink.PaymentResponse paymentResponse = UsePaymentRequestProcessTran(paymentRequest);
                if (paymentResponse != null)
                {
                    posLinkWrapper.paymentResponseResultCode = paymentResponse.ResultCode;
                    posLinkWrapper.paymentResponseResultTxt = paymentResponse.ResultTxt;
                    posLinkWrapper.paymentResponseRefNum = paymentResponse.RefNum;
                    //posLinkWrapper.paymentResponseRawResponse = paymentResponse.RawResponse;
                    posLinkWrapper.paymentResponseAvsResponse = paymentResponse.AvsResponse;
                    posLinkWrapper.paymentResponseCvResponse = paymentResponse.CvResponse;
                    posLinkWrapper.paymentResponseTimestamp = paymentResponse.Timestamp;
                    posLinkWrapper.paymentResponseHostCode = paymentResponse.HostCode;
                    posLinkWrapper.paymentResponseRequestedAmt = paymentResponse.RequestedAmount;
                    posLinkWrapper.paymentResponseApprovedAmt = paymentResponse.ApprovedAmount;
                    posLinkWrapper.paymentResponseRemainingBalance = paymentResponse.RemainingBalance;
                    posLinkWrapper.paymentResponseExtraBalance = paymentResponse.ExtraBalance;
                    posLinkWrapper.paymentResponseHostResponse = paymentResponse.HostResponse;
                    posLinkWrapper.paymentResponseBogusAccountNum = paymentResponse.BogusAccountNum;
                    posLinkWrapper.paymentResponseCardType = paymentResponse.CardType;
                    posLinkWrapper.paymentResponseMessage = paymentResponse.Message;
                    posLinkWrapper.paymentResponseExtData = paymentResponse.ExtData;
                    posLinkWrapper.paymentResponseAuthCode = paymentResponse.AuthCode;
                    posLinkWrapper.paymentResponseExtData += "<SigFileName>" + paymentResponse.SigFileName + "</SigFileName>";

                    commResp = new CommResponse() { Code = ResultCode.SUCC_PAYMENT, Msg = "Payment completed.", Data = string.Empty };

                }
            }
            catch (InterruptProcessException ex)
            {
                posLinkWrapper.errorCode = 5;
                posLinkWrapper.errorMessage = ex.Message;
                commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = ex.Message, Data = string.Empty };
            }
            catch (FailProcessException ex)
            {
                posLinkWrapper.errorCode = 1;
                posLinkWrapper.errorMessage = ex.Message;
                commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Unknown error: pg.PaymentResponse is null.", Data = string.Empty };
            }
            catch (ActionTimeoutExcepiton ex)
            {
                posLinkWrapper.errorCode = 2;
                posLinkWrapper.errorMessage = ex.Message;
                commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Action Timeout.", Data = string.Empty };
            }
            catch (NullResponseException ex)
            {
                posLinkWrapper.errorCode = 3;
                posLinkWrapper.errorMessage = ex.Message;
                commResp = new CommResponse() { Code = ResultCode.ERR_PAYMENT, Msg = "Error Processing Payment.", Data = string.Empty };
            }
            process2.Abort();
            posLinkWrapper.ReportDataReceived(commResp);
        }
        private void FillManageResponseToWrapper(ManageResponse manageResponse)
        {
            posLinkWrapper.manageResponseResultCode = manageResponse.ResultCode;
            posLinkWrapper.manageResponseResultTxt = manageResponse.ResultTxt;
            posLinkWrapper.manageResponseSN = manageResponse.SN;
            posLinkWrapper.manageResponseVarValue = manageResponse.VarValue;
            //manageResponseBottonNum = manageResponse.BottonNum;//v1.09 removed
            posLinkWrapper.manageResponseSigFileName = manageResponse.SigFileName;
            posLinkWrapper.manageResponsePinBlock = manageResponse.PinBlock;
            posLinkWrapper.manageResponseKSN = manageResponse.KSN;
            posLinkWrapper.manageResponseTrack1Data = manageResponse.Track1Data;
            posLinkWrapper.manageResponseTrack2Data = manageResponse.Track2Data;
            posLinkWrapper.manageResponseTrack3Data = manageResponse.Track3Data;
            posLinkWrapper.manageResponsePAN = manageResponse.PAN;
            posLinkWrapper.manageResponseQRCode = manageResponse.QRCode;
            posLinkWrapper.manageResponseEntryMode = manageResponse.EntryMode;
            posLinkWrapper.manageResponseExpiryDate = manageResponse.ExpiryDate;
            posLinkWrapper.manageResponseText = manageResponse.Text;
        }

        //private CommSetting CreateSetting(PosLinkWrapper posLinkWrapper)
        //{
        //    return new CommSetting()
        //    {
        //        CommType = posLinkWrapper.Setting_CommType,
        //        TimeOut = posLinkWrapper.Setting_TimeOut,
        //        SerialPort = posLinkWrapper.Setting_SerialPort,
        //        DestIP = posLinkWrapper.Setting_DestIP,
        //        DestPort = posLinkWrapper.Setting_DestPort,
        //        BaudRate = posLinkWrapper.Setting_BaudRate,
        //    };
        //}


        private ManageRequest CreateManageRequest(PosLinkWrapper wrapper)
        {
            ManageRequest manageRequest = new ManageRequest();

            if (StringUtils.IsNumeric(wrapper.manageRequestEDCType))
            {
                manageRequest.EDCType = Convert.ToInt32(wrapper.manageRequestEDCType);
            }
            else
            {
                manageRequest.EDCType = manageRequest.ParseEDCType(wrapper.manageRequestEDCType);
            }

            manageRequest.TransType = manageRequest.ParseTransType(wrapper.manageRequestTransType);

            if (string.IsNullOrEmpty(wrapper.manageRequestTrans))
            {
                manageRequest.Trans = manageRequest.ParseTrans("UNKNOWN");
            }
            else
            {
                manageRequest.Trans = manageRequest.ParseTrans(wrapper.manageRequestTrans);
            }

            manageRequest.VarName = wrapper.manageRequestVarName;
            manageRequest.VarValue = wrapper.manageRequestVarValue;
            manageRequest.Title = wrapper.manageRequestTitle;
            manageRequest.Button1 = wrapper.manageRequestButton1;
            manageRequest.Button2 = wrapper.manageRequestButton2;
            manageRequest.Button3 = wrapper.manageRequestButton3;
            manageRequest.Button4 = wrapper.manageRequestButton4;
            manageRequest.DisplayMessage = wrapper.manageRequestDisplayMessage;
            manageRequest.ImagePath = wrapper.manageRequestImagePath;
            manageRequest.ImageName = wrapper.manageRequestImageName;
            if (string.IsNullOrEmpty(wrapper.manageRequestUpload))
                manageRequest.Upload = Convert.ToInt32("0");
            else
                manageRequest.Upload = Convert.ToInt32(wrapper.manageRequestUpload);
            //manageRequest.Upload = RT_TB_Upload.Text;
            manageRequest.HRefNum = wrapper.manageRequestHRefNum;
            manageRequest.Timeout = wrapper.manageRequestTimeout;
            manageRequest.ThankYouTitle = wrapper.manageRequestThankYouTitle;
            manageRequest.ThankYouMessage1 = wrapper.manageRequestThankYouMessage1;
            manageRequest.ThankYouMessage2 = wrapper.manageRequestThankYouMessage2;
            manageRequest.ThankYouTimeout = wrapper.manageRequestThankYouTimeout;
            manageRequest.AccountNumber = wrapper.manageRequestAccountNumber;
            manageRequest.EncryptionType = "1";
            manageRequest.KeySlot = wrapper.manageRequestKeySlot;
            manageRequest.PinMaxLength = wrapper.manageRequestPinMaxLength;
            manageRequest.PinMinLength = wrapper.manageRequestPinMinLength;
            manageRequest.NullPin = wrapper.manageRequestNullPin;
            manageRequest.PinAlgorithm = wrapper.manageRequestPinAlgorithm;
            manageRequest.MagneticSwipeEntryFlag = wrapper.manageRequestMagneticSwipeEntryFlag;
            manageRequest.ManualEntryFlag = wrapper.manageRequestManualEntryFlag;
            manageRequest.ContactlessEntryFlag = wrapper.manageRequestContactlessEntryFlag;
            manageRequest.ScannerEntryFlag = wrapper.manageRequestScannerEntryFlag;
            manageRequest.EncryptionFlag = wrapper.manageRequestEncryptionFlag;
            manageRequest.MAXAccountLength = wrapper.manageRequestMAXAccountLength;
            manageRequest.MINAccountLength = wrapper.manageRequestMINAccountLength;
            manageRequest.ExpiryDatePrompt = wrapper.manageRequestExpiryDatePrompt;
            manageRequest.InputType = wrapper.manageRequestInputType;
            manageRequest.DefaultValue = wrapper.manageRequestDefaultValue;
            manageRequest.MAXLength = wrapper.manageRequestMAXLength;
            manageRequest.MINLength = wrapper.manageRequestMINLength;
            manageRequest.FileName = wrapper.manageRequestFileName;

            string path = wrapper.manageRequestSigSavePath;
            if (path.EndsWith(":"))
            {
                path = path + "\\";
            }
            manageRequest.SigSavePath = path;

            return manageRequest;
        }

        private ManageResponse UseManageRequestProcessTran(ManageRequest manageRequest)
        {
            InitPoslinkConfig();

            poslink.ManageRequest = manageRequest;
            ProcessTransResult result = null;
            try
            {
                result = poslink.ProcessTrans();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw new FailProcessException("An exception occurred after process trans.");
            }

            if (null == result)
            {
                throw new NullResponseException("Incorrect trans result. The \"ProcessTransResult\" should not be null.");
            }

            if (result.Code == ProcessTransResultCode.OK)
            {
                ManageResponse manageResponse = poslink.ManageResponse;
                if (manageResponse != null && manageResponse.ResultCode != null)
                {
                    return manageResponse;
                }
                else
                {
                    throw new NullResponseException("Incorrect manage response. The \"ManageResponse\" should not be null.");
                }
            }
            else if (result.Code == ProcessTransResultCode.TimeOut)
            {
                throw new ActionTimeoutExcepiton("Manage request timeout.");
            }
            else
            {
                throw new FailProcessException("No result is retruned after processing transaction on manage request.");
            }
        }

        public void InitPoslinkConfig()
        {
            CommSetting commSetting = new CommSetting()
            {
                CommType = posLinkWrapper.Setting_CommType,
                TimeOut = posLinkWrapper.Setting_TimeOut,
                SerialPort = posLinkWrapper.Setting_SerialPort,
                DestIP = posLinkWrapper.Setting_DestIP,
                DestPort = posLinkWrapper.Setting_DestPort,
                BaudRate = posLinkWrapper.Setting_BaudRate,
            };
            poslink.CommSetting = commSetting;
        }

        private void ProcessIntenalOnBackground(ThreadParam parameters)
        {
            ParameterizedThreadStart entry1 = new ParameterizedThreadStart(InvokeProcessManageAction);
            Thread process1 = new Thread(entry1);
            process1.IsBackground = true;
            process1.Start(parameters);
        }

        private void ProcessIntenalOnDirect(ThreadParam parameters)
        {
            InvokeProcessManageAction(parameters);
        }

        public void Reset()
        {
            ResetPoslLinkParams();

            posLinkWrapper.manageRequestTransType = "RESET";
            ProcessIntenalOnBackground(new ThreadParam() { succCode = ResultCode.SUCC_RESET, succMsg = "Reset completed.", errCode = ResultCode.ERR_RESET });
        }

        public void RequestInputText(string title, int inputType, int timeout)
        {
            ResetPoslLinkParams();

            posLinkWrapper.manageRequestTransType = "INPUTTEXT";
            posLinkWrapper.manageRequestTitle = title;
            posLinkWrapper.manageRequestInputType = Convert.ToString(inputType);

            if (timeout <= 0)
            {
                posLinkWrapper.manageRequestTimeout = "300"; // 0.1s
            }
            else
            {
                posLinkWrapper.manageRequestTimeout = Convert.ToString(timeout * 10);
            }

            ProcessIntenalOnBackground(new ThreadParam() { succCode = ResultCode.SUCC_INPUT_TEXT, succMsg = "Request input text completed.", errCode = ResultCode.ERR_INPUT_TEXT });

        }

        public void GetVar(int EDCType, string varName)
        {
            ResetPoslLinkParams();
            posLinkWrapper.manageRequestTransType = "GETVAR";
            posLinkWrapper.manageRequestEDCType = "" + EDCType;
            posLinkWrapper.manageRequestVarName = varName;
            posLinkWrapper.manageRequestUpload = "0";
            posLinkWrapper.manageRequestTimeout = "900";
            interceptor = (manageRequest, manageResponse) =>
            {
                Console.WriteLine(manageResponse.ToString());
                return false;
            };
            ProcessIntenalOnDirect(new ThreadParam() { succCode = ResultCode.SUCC_GET_VAR, succMsg = "GETVAR complete.", errCode = ResultCode.ERR_GET_VAR });
        }

        private Thread process2;
        public void ProcessTrans(string TenderType, string TransType, string Amount, string ECRefNum, string ExtData, string CashbackAmt, int timeout)
        {
            ResetPoslLinkParams();


            if (timeout > 0)
            {
                posLinkWrapper.Setting_TimeOut = Convert.ToString(timeout * 1000);
            }

            posLinkWrapper.paymentRequestTenderType = TenderType;
            posLinkWrapper.paymentRequestTransType = TransType;
            posLinkWrapper.paymentRequestAmount = Amount;
            posLinkWrapper.paymentRequestECRefNum = ECRefNum;
            posLinkWrapper.paymentRequestExtData = ExtData;
            posLinkWrapper.paymentRequestCashbackAmt = CashbackAmt;

            //v1.14拆开extdata为传参变量和pax的extdata
            posLinkWrapper.paymentRequestExtData = StringUtils.GetPaxExtData(ExtData);
            if (string.IsNullOrEmpty(posLinkWrapper.paymentRequestExtData))
            {
                posLinkWrapper.paymentRequestExtData = ExtData;
            }
            else
            {
                sigSavePath = StringUtils.GetXmlTagValue(ExtData, "SigSavePath");
            }

            ThreadStart entry1 = new ThreadStart(InvokeProcessPaymentAction);
            Thread process1 = new Thread(entry1);
            process1.IsBackground = true;
            process1.Start();

            ThreadStart entry2 = new ThreadStart(StatusChangedRun);
            process2 = new Thread(entry2);
            process2.IsBackground = true;
            process2.Start();

        }

        private void ResetPoslLinkParams()
        {
            posLinkWrapper.paymentResponseResultCode = "";
            posLinkWrapper.paymentResponseResultTxt = "";
            posLinkWrapper.paymentResponseRefNum = "";
            posLinkWrapper.paymentResponseRawResponse = "";
            posLinkWrapper.paymentResponseAvsResponse = "";
            posLinkWrapper.paymentResponseCvResponse = "";
            posLinkWrapper.paymentResponseTimestamp = "";
            posLinkWrapper.paymentResponseHostCode = "";
            posLinkWrapper.paymentResponseRequestedAmt = "";
            posLinkWrapper.paymentResponseApprovedAmt = "";
            posLinkWrapper.paymentResponseRemainingBalance = "";
            posLinkWrapper.paymentResponseExtraBalance = "";
            posLinkWrapper.paymentResponseHostResponse = "";
            posLinkWrapper.paymentResponseBogusAccountNum = "";
            posLinkWrapper.paymentResponseCardType = "";
            posLinkWrapper.paymentResponseMessage = "";
            posLinkWrapper.paymentResponseExtData = "";
            posLinkWrapper.paymentResponseAuthCode = "";


            // v1.19 修复crash bug
            posLinkWrapper.manageRequestTrans = "UNKNOWN";
            posLinkWrapper.manageRequestSigSavePath = "";

            posLinkWrapper.manageResponseResultCode = "";
            posLinkWrapper.manageResponseResultTxt = "";
            posLinkWrapper.manageResponseSN = "";
            posLinkWrapper.manageResponseVarValue = "";
            //manageResponseBottonNum = "";//v1.09 removed
            posLinkWrapper.manageResponseSigFileName = "";
            posLinkWrapper.manageResponsePinBlock = "";
            posLinkWrapper.manageResponseKSN = "";
            posLinkWrapper.manageResponseTrack1Data = "";
            posLinkWrapper.manageResponseTrack2Data = "";
            posLinkWrapper.manageResponseTrack3Data = "";
            posLinkWrapper.manageResponsePAN = "";
            posLinkWrapper.manageResponseQRCode = "";
            posLinkWrapper.manageResponseEntryMode = "";
            posLinkWrapper.manageResponseExpiryDate = "";
            posLinkWrapper.manageResponseText = "";

            isCancelTask = false;

            if (poslink != null)
            {
                poslink.ManageRequest = null;
                poslink.PaymentRequest = null;
                poslink.ReportRequest = null;
            }

            interceptor = null;

        }

        private void StatusChangedRun()
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

                if ((a >= 0 && a <= 4))
                {
                    posLinkWrapper.ReportStatusChanged(a, msg);
                }

                //Thread.Sleep(500);
            }
        }


        private PaymentResponse UsePaymentRequestProcessTran(PaymentRequest paymentRequest)
        {
            InitPoslinkConfig();
            poslink.PaymentRequest = paymentRequest;
            ProcessTransResult result = null;
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

            posLinkWrapper.transResultMsg = result.Msg;
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
            else if (result.Code == ProcessTransResultCode.TimeOut)
            {
                throw new ActionTimeoutExcepiton("Payment request timeout.");
            }
            else
            {
                throw new FailProcessException("No result is retruned after processing transaction on payment request.");
            }
        }

        public void CancelTrans()
        {
            isCancelTask = true;
            if (poslink != null)
                poslink.CancelTrans();
        }

        public void SetEnableLog(bool enable)
        {
            LogManagement log = new LogManagement();
            log.LogSwitchMode = enable;
            log.LogLevel = 1;
            log.LogFilePath = "";
            log.LogFileName = "";
            log.saveFile();
        }

        public void Init()
        {
            ResetPoslLinkParams();
            posLinkWrapper.errorCode = 0;

            posLinkWrapper.manageRequestTransType = "INIT";
            posLinkWrapper.manageRequestEDCType = "ALL";
            posLinkWrapper.manageRequestUpload = "0";
            posLinkWrapper.manageRequestTimeout = "900";

            interceptor = (manageRequest, manageResponse) =>
            {
                posLinkWrapper.SerialNumber = manageResponse.SN;
                return true;
            };
            ProcessIntenalOnDirect(new ThreadParam() { succCode = ResultCode.SUCC_MANAGE, succMsg = "Init completed.", errCode = ResultCode.ERR_MANAGE });
        }
    }
}
