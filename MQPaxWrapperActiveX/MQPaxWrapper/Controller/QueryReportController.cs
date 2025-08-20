using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MQPaxWrapper.Model; 

namespace MQPaxWrapper
{
    public class QueryReportController
    {
        private ReportParam _args;
        private POSLink.PosLink _poslink;
        public delegate void ResponseDelegate(string paxResultCode, string paxResultTxt, string paxTransResultMsg, CommResponse response);
        public ResponseDelegate responseDelegate;
        private string paxResultCode, paxResultTxt, paxTransResultMsg;
        private const string TAG = "QueryReportController";

        public QueryReportController(POSLink.PosLink poslink, ReportParam reportParam)
        {
            _args = reportParam;
            _poslink = poslink;
        }

        public void QueryDetailReport()
        {
            _args.transType = "LOCALDETAILREPORT";
            _args.edcType = "ALL";
            QueryReport();
        }

        public void QueryReport()
        {
            paxResultCode = string.Empty;
            paxResultTxt = string.Empty;
            paxTransResultMsg = string.Empty;

            POSLink.ReportRequest reportRequest = new POSLink.ReportRequest();
            reportRequest.TransType = reportRequest.ParseTransType(_args.transType);
            reportRequest.EDCType = reportRequest.ParseEDCType(_args.edcType);
            reportRequest.ECRRefNum = _args.EcrRefNum;

            _poslink.ReportRequest = reportRequest;
            POSLink.ProcessTransResult result = null;

            try
            {
                result = _poslink.ProcessTrans();
            }
            catch (Exception ex)
            { 
                DearMing.Common.Utils.Log.e(TAG, ex.Message);
                TriggerCallback(new CommResponse() { Code = ResultCode.ERR_YOUREERRMSG3_OF_PAX, Msg = "An exception occurred after process trans.", Data = string.Empty }); 
            }

            if (null == result)
            {
                return;
            } 

            paxTransResultMsg = result.Msg;
            if (result.Code == POSLink.ProcessTransResultCode.OK)
            {
                POSLink.ReportResponse reportResponse = _poslink.ReportResponse;
                if (reportResponse != null && reportResponse.ResultCode != null)
                {
                    paxResultCode = reportResponse.ResultCode;
                    paxResultTxt = reportResponse.ResultTxt;

                    TriggerCallback(new CommResponse()
                    {
                        Code = ResultCode.SUCC_REPORT,
                        Msg = result.Msg,
                        Data = PaxResponseUtil.parseReportResponse(reportResponse)
                    });
                }
                else
                {
                    TriggerCallback(new CommResponse() { Code = ResultCode.PAX_RESPONSE_NULL, Msg = "Unknown error: poslink.ReportResponse is null.", Data = string.Empty });
                }
            }
            else if (result.Code == POSLink.ProcessTransResultCode.TimeOut)
            {
                TriggerCallback(new CommResponse() { Code = ResultCode.PAX_TIME_OUT, Msg = "Action Timeout.", Data = string.Empty });
            }
            else
            {
                TriggerCallback(new CommResponse() { Code = ResultCode.PAX_INCORRECT_RESULT_TYPE, Msg = "Error Processing Report.", Data = string.Empty });
            }
        }

        private void TriggerCallback(CommResponse response)
        {
            if (responseDelegate != null)
            {
                responseDelegate(paxResultCode, paxResultTxt, paxTransResultMsg, response);
            }
        }

        public class ReportParam
        {
            public string transType { get; set; }
            public string edcType { get; set; }
            public string EcrRefNum { get; set; }
        }
    }
}
