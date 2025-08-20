using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQPaxWrapper.Exceptions
{
    public class ActionTimeoutExcepiton :Exception
    {

        public ActionTimeoutExcepiton(string msg):base(msg)
        { 

        }
    }
}
