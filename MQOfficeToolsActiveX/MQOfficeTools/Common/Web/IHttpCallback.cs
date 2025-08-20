using System;
using System.Collections.Generic;
using System.Text;

namespace MQOfficeTools.Common.Web
{
    public interface IHttpCallback
    {
        void OnError(string errorMessage);

        void OnSuccess(string result);
    }
}
