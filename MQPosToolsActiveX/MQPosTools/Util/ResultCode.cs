using System;
using System.Collections.Generic;
using System.Text;

namespace MQPosTools.Util
{
    public class ResultCode
    {
        public static readonly int SUCCESS              = 0;
        public static readonly int INCORRECT_PATH       = 1;
        public static readonly int INCORRECT_CONTENT    = 2;

        public static readonly int INCORRECT_SIZE       = 4;
        public static readonly int FAIL_GENERATE_QRCODE = 5;
        public static readonly int INVALID_EXTENSION  = 6;
    }
}
