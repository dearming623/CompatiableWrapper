using System;
using System.Collections.Generic;
using System.Text;

namespace MQWebApi.Core
{
    public interface DataReceiverAction
    {
        void OnProcess();
    }
}
