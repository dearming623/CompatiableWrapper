using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQPaxWrapper
{
    class PaxResponseUtil
    {
        public static string parseReportResponse(POSLink.ReportResponse reportResponse)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ReportResponse>");
            //sb.AppendFormat("<ResultCode>{0}</ResultCode>", reportResponse.ResultCode);
            //sb.AppendFormat("<ResultTxt>{0}</ResultTxt>", reportResponse.ResultTxt);
            sb.AppendFormat("<TotalRecord>{0}</TotalRecord>", reportResponse.TotalRecord);
            sb.AppendFormat("<RecordNumber>{0}</RecordNumber>", reportResponse.RecordNumber);
            sb.AppendFormat("<HostResponse>{0}</HostResponse>", reportResponse.HostResponse);
            sb.AppendFormat("<Message>{0}</Message>", reportResponse.Message);
            sb.AppendFormat("<AuthCode>{0}</AuthCode>", reportResponse.AuthCode);
            sb.AppendFormat("<HostCode>{0}</HostCode>", reportResponse.HostCode);
            sb.AppendFormat("<EDCType>{0}</EDCType>", reportResponse.EDCType);
            sb.AppendFormat("<PaymentType>{0}</PaymentType>", reportResponse.PaymentType);
            sb.AppendFormat("<ApprovedAmount>{0}</ApprovedAmount>", reportResponse.ApprovedAmount);
            sb.AppendFormat("<RemainingBalance>{0}</RemainingBalance>", reportResponse.RemainingBalance);
            sb.AppendFormat("<ExtraBalance>{0}</ExtraBalance>", reportResponse.ExtraBalance);
            sb.AppendFormat("<BogusAccountNum>{0}</BogusAccountNum>", reportResponse.BogusAccountNum);
            sb.AppendFormat("<CardType>{0}</CardType>", reportResponse.CardType);
            sb.AppendFormat("<RefNum>{0}</RefNum>", reportResponse.RefNum);
            sb.AppendFormat("<ECRRefNum>{0}</ECRRefNum>", reportResponse.ECRRefNum);
            sb.AppendFormat("<Timestamp>{0}</Timestamp>", reportResponse.Timestamp);
            sb.AppendFormat("<ExtData>{0}</ExtData>", reportResponse.ExtData);
            sb.Append("</ReportResponse>");

         
            return sb.ToString();
        }
    }
}
