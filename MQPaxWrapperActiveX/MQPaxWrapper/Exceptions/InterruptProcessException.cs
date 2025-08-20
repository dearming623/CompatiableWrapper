using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQPaxWrapper.Exceptions
{
    public class InterruptProcessException : Exception
    {
        public InterruptProcessException(string msg)
            : base(msg)
        {
        }
    }
}
