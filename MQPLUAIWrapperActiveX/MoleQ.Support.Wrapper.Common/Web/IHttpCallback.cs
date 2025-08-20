using System;
using System.Collections.Generic;
using System.Text;

namespace MoleQ.Support.Wrapper.Common.Web
{
    public interface IHttpCallback
    {
        void OnError(string errorMessage);

        void OnSuccess(string result);
    }
}
