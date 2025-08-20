using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQVideoPlayer
{
    public class CommResponse
    {
        public string Code { set; get; }
        public string Msg { set; get; }
        public string Data { set; get; }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Response>");
            sb.AppendFormat("<Code>{0}</Code>", Code);
            sb.AppendFormat("<Msg>{0}</Msg>", Msg);
            if (!string.IsNullOrEmpty(Data))
            {
                sb.AppendFormat("<Data>{0}</Data>", Data);
            } 
            sb.Append("</Response>");
            return sb.ToString();
        }

        public string GetCode()
        {
            return this.Code;
        }
    }
}
