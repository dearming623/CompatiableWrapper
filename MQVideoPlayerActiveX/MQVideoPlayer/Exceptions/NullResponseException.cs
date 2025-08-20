using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQVideoPlayer.Exceptions
{
    public class NullResponseException : Exception
    {
        public NullResponseException(string msg)
            : base(msg)
        {
        }
    }
}
